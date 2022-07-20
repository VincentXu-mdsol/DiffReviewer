using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using DiffReviewer.Interfaces;
using SharpDiff;
using SharpDiff.Parsers.GitDiff;

namespace DiffReviewer.Importer
{
    public class DiffProcessor
    {
        private readonly IImportService _saveService;
        public DiffProcessor(IImportService saveService)
        {
            _saveService = saveService;
        }

        public DiffProcessorResponse Import(string diffContent, int pullRequestNumber)
        {
            var serverCreatedTime = DateTime.UtcNow;
            var masterDiffId = PersistRawDiffToStorage(diffContent, pullRequestNumber, serverCreatedTime);
            var splitDiffContents = Differ.SplitUnifiedDiff(diffContent);
            ImportIndividualDiffs(splitDiffContents, masterDiffId, serverCreatedTime);
            return new DiffProcessorResponse();
        }

        private void ImportIndividualDiffs(IEnumerable<string> splitDiffContents , int masterDiffId, DateTime createdDate)
        {
            foreach (var splitDiffContent in splitDiffContents)
            {
                var diff = Differ.LoadGitDiffSplit(splitDiffContent).FirstOrDefault();
                (var originalFileName, var modifiedFileName, var isNewFile, var isDelete, var isBinary, var hasHunks,
                    var rangeStartHash, var rangeEndHash, var mode, var kind) = GetDiffInfo(diff);             
                 var individualDiffId = PersistDiffToStorage(masterDiffId, splitDiffContent.Replace("\r\n", "\\r\\n"), createdDate, 
                     originalFileName, modifiedFileName, isNewFile, isDelete, isBinary, hasHunks,
                     rangeStartHash, rangeEndHash, mode, kind);
                
                if(hasHunks)
                {
                    ImportHunks(diff, individualDiffId, createdDate);
                }
            }
        }

        private void ImportHunks(Diff diff, int individualDiffId, DateTime createdDate)
        {
            foreach(var hunk in diff.Chunks)
            {
                var targetFileName = diff.Files[1].FileName;
                var hunkHash = GenerateHashString(hunk.ChunkText, targetFileName);
                var hunkId = _saveService.SaveHunk(individualDiffId, hunk.OriginalRange.StartLine, hunk.OriginalRange.LinesAffected,
                    hunk.NewRange.StartLine, hunk.NewRange.LinesAffected, hunkHash, hunk.ChunkText.Replace("\r\n", "\\r\\n"), createdDate);


                

                ImportSnippets(hunk, hunkId, createdDate);
            }
        }

        private void ImportSnippets(Chunk hunk, int hunkId, DateTime createdDate)
        {
            foreach(var snippet in hunk.Snippets)
            {
                var snippetTypeName = snippet.GetType().ToString();
                
                var snippetId = _saveService.SaveSnippet(hunkId, snippetTypeName, createdDate);
                ImportLines(snippet, snippetId, createdDate);
            }
        }

        private void ImportLines(ISnippet snippet, int snippetId, DateTime createdDate)
        {
            foreach(var line in snippet.OriginalLines)
            {
                var lineTypeName = line.GetType().ToString();
                var lineId = _saveService.SaveLine(snippetId, line.Value, lineTypeName, "Original", createdDate);

                ImportLineSpans(line, lineId, createdDate);
            }

            foreach (var line in snippet.AdditionLines)
            {
                var lineTypeName = line.GetType().ToString();
                var lineId = _saveService.SaveLine(snippetId, line.Value, lineTypeName, "Modified", createdDate);

                ImportLineSpans(line, lineId, createdDate);
            }
        }

        private void ImportLineSpans(ILine line, int lineId, DateTime createdDate)
        {
            foreach (var span in line.Spans)
            {
                _saveService.SaveLineSpan(lineId, span.Value, span.Kind.ToString(), createdDate);
            }
        }
        //private (string, string) GetFileNamesFromRawString(string rawFileDefs)
        //{
        //    Regex filter = new Regex(@"^([a-zA-Z0-9#\/.]+[a-z])");
        //    var matches = filter.Match(rawFileDefs).Groups;

        //    var originalFileName = "/dev/null";
        //    var modifiedFileName = "/dev/null";
        //    var index = 0;
        //    foreach(var match in matches)
        //    {
        //        if (index == 0)
        //        {
        //            originalFileName = match.ToString().Remove(0, 1);
        //            index++;
        //        }
        //        else
        //        {
        //            modifiedFileName = match.ToString().Remove(0, 1); ;
        //            break;
        //        }
        //    }
        //    return (originalFileName, modifiedFileName);
        //}

        private string GenerateHashString(string valueToHash, string salt)
        {
            if (string.IsNullOrEmpty(valueToHash))
                return string.Empty;

            using(var sha256 = new SHA256Managed())
            {
                var hashBytesArr = sha256.ComputeHash(Encoding.UTF8.GetBytes(valueToHash + salt));
                string hash = BitConverter.ToString(hashBytesArr).Replace("-", String.Empty);
                return hash;
            }
        }

        private (string, string) GetFileNamesFromRawString(string rawFileDefs)
        {
            var originalFileName = "/dev/null";
            var modifiedFileName = "/dev/null";
            var files = rawFileDefs.Split(" and ");

            var index = 0;
            foreach (var file in files)
            {
                if (index == 0)
                {
                    originalFileName = file.Remove(0, 1);
                    index++;
                }
                else
                {
                    var parts = file.Split(" ");
                    modifiedFileName = parts[0].Remove(0, 1); ;
                    break;
                }
            }
            return (originalFileName, modifiedFileName);
        }


        private (string, string, bool, bool, bool, bool, string, string, int, string) GetDiffInfo(Diff diff)
        {
            var originalFileName = string.Empty;
            var modifiedFileName = string.Empty;
            var isNewFile = false;
            var isDelete = false;
            var rangeStartHash = string.Empty;
            var rangeEndHash = string.Empty;
            var mode = -1;
            var kind = string.Empty;

            if(diff.IsBinary)
            {
                var rawFileDefs = diff.BinaryFiles.RawFileDefs;
                (originalFileName, modifiedFileName) = GetFileNamesFromRawString(rawFileDefs);

                if (string.Equals(originalFileName, "/dev/null"))
                    isNewFile = true;
                if (string.Equals(modifiedFileName, "/dev/null"))
                    isDelete = true;
                var headers = diff.Headers;
            }
            else
            {
                isNewFile = diff.IsNewFile;
                isDelete = diff.IsDeletion;
                originalFileName = diff.Files[0].FileName;
                modifiedFileName = diff.Files[1].FileName;

                var headers = diff.Headers;
                foreach (var header in headers)
                {
                    if (header.GetType() == typeof(IndexHeader)) //executed by File Diff
                    {
                        var indexHeader = ((IndexHeader)header);
                        rangeStartHash = indexHeader.Range.Start;
                        rangeEndHash = indexHeader.Range.End;
                    }
                    if (header.GetType() == typeof(ModeHeader)) //executed by File Diff
                    {
                        var modeHeader = ((ModeHeader)header);
                        mode = modeHeader.Mode;
                        kind = modeHeader.Kind;
                    }
                    //if (header.GetType() == typeof(SimilarityHeader))
                    //{
                    //    var x = ((SimilarityHeader)header);
                    //    var y = x.Index;
                    //    var z = x.Kind;
                    //}
                    //if (header.GetType() == typeof(ChunksHeader))
                    //{
                    //    var x = ((ChunksHeader)header);
                    //    var y = x.NewFile;
                    //    var z = x.OriginalFile;
                    //}
                    //if (header.GetType() == typeof(CopyRenameHeader))
                    //{
                    //    var x = ((CopyRenameHeader)header);
                    //    var y = x.Transaction;
                    //    var z = x.Direction;
                    //    var a = x.FileName;
                    //}
                    //if (header.GetType() == typeof(DiffHeader))
                    //{
                    //    var x = ((DiffHeader)header);
                    //    var y = x.Format.Name;
                    //    var z = x.Files;
                    //    var a = x.IsNewFile;
                    //    var b = x.IsDeletion;
                    //}
                }
            }
            return (originalFileName, modifiedFileName, isNewFile, isDelete, diff.IsBinary, diff.HasChunks, rangeStartHash, rangeEndHash, mode, kind);
        }

        private int PersistRawDiffToStorage(string diffContent, int pullRequestNumber, DateTime createdDate)
        {
            return _saveService.SaveOriginalDiff(diffContent, pullRequestNumber, createdDate);
        }

        private int PersistDiffToStorage(int masterDiffId, string diffContent, DateTime createdDate, string originalFileName, string modifiedFileName, 
            bool isNewFile, bool isDelete, bool isBinary, bool hasHunks, string rangeStartHash, string rangeEndHash, int mode, string kind)
        {
            return _saveService.SaveIndividualDiff(masterDiffId, diffContent, createdDate, originalFileName, modifiedFileName, isNewFile, isDelete, 
                isBinary, hasHunks, rangeStartHash, rangeEndHash, mode, kind);
        }
        
    }


    public class DiffProcessorResponse
    {
        bool Success;
        string Message;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DiffReviewer.Interfaces;

namespace DiffReviewer.ServiceLibrary
{
    public class ImportService : IImportService
    {
        private readonly IImportRepository _importRepository;
        public ImportService(IImportRepository importRepository)
        {
            _importRepository = importRepository;
        }
        public int SaveIndividualDiff(int masterDiffId, string diffContent, DateTime createdDate, string originalFileName, string modifiedFileName,
            bool isNewFile, bool isDelete, bool isBinary, bool hasHunks, string rangeStartHash, string rangeEndHash, int mode, string kind)
        {
            return _importRepository.SaveIndividualDiff(masterDiffId, diffContent, createdDate, originalFileName, modifiedFileName, isNewFile, 
                isDelete, isBinary, hasHunks, rangeStartHash, rangeEndHash, mode, kind);
        }

        public int SaveOriginalDiff(string diffContent, int pullRequestNumber, DateTime createdDate)
        {
            return _importRepository.SaveOriginalDiff(diffContent, pullRequestNumber, createdDate);
        }

        public int SaveHunk(int individualDiffId, int originalRangeStartLine, int originalRangeLinesAffected,
            int newRangeStartLine, int newRangeLinesAffected, string hunkHash, string hunkText, DateTime CreatedDate)
        {
            return _importRepository.SaveHunk(individualDiffId, originalRangeStartLine, originalRangeLinesAffected, newRangeStartLine,
                newRangeLinesAffected, hunkHash, hunkText, CreatedDate);
        }

        public int SaveSnippet(int hunkId, string snippetTypeName, DateTime createdDate)
        {
            var snippetTypeId = -1;
            switch (snippetTypeName)
            {
                case "SharpDiff.Parsers.GitDiff.AdditionSnippet":
                    snippetTypeId = 1;
                    break;
                case "SharpDiff.Parsers.GitDiff.SubtractionSnippet":
                    snippetTypeId = 2;
                    break;
                case "SharpDiff.Parsers.GitDiff.ModificationSnippet":
                    snippetTypeId = 3;
                    break;
                default: //ContextSnippet
                    snippetTypeId = 4;
                    break;
            }
            return _importRepository.SaveSnippet(hunkId, snippetTypeId, createdDate);
        }

        public int SaveLine(int hunkId, string value, string lineTypeName, string lineOriginName, DateTime createdDate)
        {
            var lineTypeId = -1;
            switch (lineTypeName)
            {
                case "SharpDiff.Parsers.GitDiff.AdditionLine":
                    lineTypeId = 1;
                    break;
                case "SharpDiff.Parsers.GitDiff.SubtractionLine":
                    lineTypeId = 2;
                    break;
                case "SharpDiff.Parsers.GitDiff.ModificationLine":
                    lineTypeId = 3;
                    break;
                case "SharpDiff.Parsers.GitDiff.ContextLine":
                    lineTypeId = 4;
                    break;
                default:// "NoNewLineAtEOFLine":
                    lineTypeId = 5;
                    break;
            }

            var lineOriginId = -1;
            switch(lineOriginName)
            {
                case "Original":
                    lineOriginId = 1;
                    break;
                default: //"Modified"
                    lineOriginId = 2;
                    break;
            }

            return _importRepository.SaveLine(hunkId, value, lineTypeId, lineOriginId, createdDate);
        }

        public int SaveLineSpan(int lineId, string value, string lineSpanTypeName, DateTime createdDate)
        {
            var lineSpanTypeId = -1;
            switch (lineSpanTypeName)
            {
                case "Equal":
                    lineSpanTypeId = 1;
                    break;
                case "Addition":
                    lineSpanTypeId = 2;
                    break;
                default:// "Deletion":
                    lineSpanTypeId = 3;
                    break;
            }
            return _importRepository.SaveLineSpan(lineId, value, lineSpanTypeId, createdDate);
        }
    }
}

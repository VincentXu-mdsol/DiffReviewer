using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpDiff;
using SharpDiff.Parsers.GitDiff;

namespace SharpDiff.TestApp {
    class Program {
        static void Main(string[] args) {
            if(args.Length < 1) {
                Console.WriteLine("Usage: SharpDiff.TestApp.exe file.diff");
                return;
            }

            string fileName = args[0];
            string content = System.IO.File.ReadAllText(fileName);
            var diffContents = Differ.SplitUnifiedDiff(content);
            foreach(var diffContent in diffContents)
            {
                Console.WriteLine(diffContent);
                Console.WriteLine("-------------------------------------------------------------------");
            }
            
            //IEnumerable<Diff> diffs = Differ.LoadGitDiffParallel(content);
            ////List<Diff> l = new List<Diff>();
            //int diffCount = 0;
            //foreach(var diff in diffs) {
            //    diffCount++;
            //    //l.Add(diff);
            //    Console.Write("+ {0} files", diff.Files.Count);
            //    //Console.Write(" Is Deletion: {0}", diff.IsDeletion); //throws exception can figure this out based on existence of file names 1 and 2
            //    //Console.Write(" Is New File: {0}", diff.IsNewFile);  //throws exception
            //    Console.Write(" || File1: {0}", diff.Files[0].FileName);
            //    Console.Write(" || File2: {0}", diff.Files[1].FileName);
            //    if (diff.HasChunks) {
            //        Console.WriteLine(", {0} chunks", diff.Chunks.Count());
            //        foreach(var chunk in diff.Chunks) {
            //            Console.WriteLine("  + {0} snippets", chunk.Snippets.Count());
            //            Console.WriteLine(" Lines Affected: {0}", chunk.OriginalRange.LinesAffected);


            //            //foreach (var snippit in chunk.Snippets)
            //            //{
            //            //    foreach (var originalLine in snippit.OriginalLines)
            //            //    {
            //            //        //Console.WriteLine(" Original: {0}", originalLine.Value);

            //            //        foreach(var span in originalLine.Spans)
            //            //        {
            //            //            var kind = span.Kind;
            //            //            Console.WriteLine("Original Spans: {0} : {1}", kind, span.Value);
            //            //        }
            //            //    }
            //            //    foreach (var modifiedLine in snippit.ModifiedLines)
            //            //    {
            //            //        foreach(var span in modifiedLine.Spans)
            //            //        {
            //            //            var kind = span.Kind;
            //            //            Console.WriteLine("Modified Spans: {0} : {1}", kind, span.Value);
            //            //        }
            //            //        //Console.WriteLine(" Modified: + {0}", modifiedLine.Value);
            //            //    }
            //            //}
            //        }
            //    } else if(diff.IsBinary) {
            //        Console.WriteLine(", binary");
            //    }
            //}
            //Console.WriteLine("{0} diffs", diffCount);
            //Console.WriteLine("{0} diffs", l.Count);

            Console.ReadLine();
        }
    }
}

using System;
using DiffReviewer.Importer;
using DiffReviewer.ServiceLibrary;

namespace DiffReviewer.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var importRepository = new ImportRepository();
            var importService = new ImportService(importRepository);
            var processor = new DiffProcessor(importService);

            string fileName = @"c:\projects\DiffReviewer\testfiles\pr3.diff";
            int pullRequestNumber = 3717;
            string content = System.IO.File.ReadAllText(fileName);
            var results = processor.Import(content , pullRequestNumber);
        }
    }
}

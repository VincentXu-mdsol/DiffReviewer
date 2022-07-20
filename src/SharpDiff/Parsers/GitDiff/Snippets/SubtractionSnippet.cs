﻿using System.Collections.Generic;
using System.Linq;

namespace SharpDiff.Parsers.GitDiff
{
    public class SubtractionSnippet : ISnippet
    {
        private readonly List<ILine> lines;

        public SubtractionSnippet()
        {
            lines = new List<ILine>();
        }

        public SubtractionSnippet(IEnumerable<ILine> lines)
        {
            this.lines = new List<ILine>(lines);
        }

        public void AddLine(ILine line)
        {
            lines.Add(line);
        }

        public IEnumerable<ILine> OriginalLines
        {
            get { return lines; }
        }

        public IEnumerable<ILine> ModifiedLines
        {
            get { yield break; }
        }

        public IEnumerable<ILine> AdditionLines
        {
            get { yield break; }
        }
    }
}
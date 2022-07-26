﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDiff.Parsers.GitDiff
{
    public class ModificationLine : ILine
    {
        public ModificationLine(IEnumerable<LineSpan> spans)
        {
            Spans = spans;
        }

        public string Value
        {
            get { return String.Join("", Spans.Select(x => x.Value).ToArray()) ; }
        }

        public IEnumerable<LineSpan> Spans { get; private set; }
    }
}
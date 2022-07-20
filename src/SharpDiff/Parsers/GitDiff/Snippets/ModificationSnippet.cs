﻿using System;
using System.Collections.Generic;
using System.Linq;
using DiffMatchPatch;

namespace SharpDiff.Parsers.GitDiff
{
    public class ModificationSnippet : ISnippet
    {
        private readonly List<ModificationLine> originalLines = new List<ModificationLine>();
        private readonly List<ModificationLine> modifiedLines = new List<ModificationLine>();
        private readonly List<ILine> additionLines = new List<ILine>();

        public ModificationSnippet(IEnumerable<ILine> originalLines, IEnumerable<ILine> modifiedLines)
        {
            additionLines = modifiedLines.ToList();
            CreateInlineDiffs(originalLines, modifiedLines);
        }

        private void CreateInlineDiffs(IEnumerable<ILine> originals, IEnumerable<ILine> modifieds)
        {
            if (originals.Count() != modifieds.Count())
            {
                originalLines.AddRange(
                    originals.Select(x => new ModificationLine(new[] { new LineSpan(x.Value, LineSpanKind.Deletion) }))
                );
                modifiedLines.AddRange(
                    modifieds.Select(x => new ModificationLine(new[] { new LineSpan(x.Value, LineSpanKind.Addition) }))
                );
                return;
            }

            var maxLines = Math.Max(originals.Count(), modifieds.Count());

            for (var i = 0; i < maxLines; i++)
            {
                var originalLine = originals.ElementAtOrDefault(i);
                var modifiedLine = modifieds.ElementAtOrDefault(i);

                if (originalLine == null && modifiedLine == null)
                    continue;
                if (originalLine != null && modifiedLine == null)
                    originalLines.Add(new ModificationLine(new[] { new LineSpan(originalLine.Value, LineSpanKind.Deletion) }));
                else if (originalLine == null)
                    modifiedLines.Add(new ModificationLine(new[] { new LineSpan(modifiedLine.Value, LineSpanKind.Addition) }));
                else
                {
                    var originalToModifiedChanges = DiffInline(originalLine, modifiedLine);

                    originalLines.Add(new ModificationLine(new[] { new LineSpan(originalLine.Value, LineSpanKind.Equal) }));
                    modifiedLines.Add(new ModificationLine(originalToModifiedChanges));
                }
            }
        }

        static IEnumerable<LineSpan> DiffInline(ILine originalLine, ILine modifiedLine)
        {
            var dmp = new diff_match_patch();
            var diffs = dmp.diff_main(originalLine.Value, modifiedLine.Value);

            dmp.diff_cleanupSemantic(diffs);

            return diffs
                .Select(x => new LineSpan(x.text, OperationToKind(x.operation)))
                .ToArray();
        }

        static LineSpanKind OperationToKind(Operation operation)
        {
            switch (operation)
            {
                default:
                    return LineSpanKind.Equal;
                case Operation.INSERT:
                    return LineSpanKind.Addition;
                case Operation.DELETE:
                    return LineSpanKind.Deletion;
            }
        }

        public IEnumerable<ILine> OriginalLines
        {
            get { return originalLines.Cast<ILine>(); }
        }

        public IEnumerable<ILine> ModifiedLines
        {
            get { return modifiedLines.Cast<ILine>(); }
        }

        public IEnumerable<ILine> AdditionLines
        {
            get { return additionLines; }
        }
    }
}
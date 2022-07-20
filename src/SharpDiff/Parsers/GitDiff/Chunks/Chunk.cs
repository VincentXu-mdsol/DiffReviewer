using System.Collections.Generic;

namespace SharpDiff.Parsers.GitDiff
{
    public class Chunk
    {
        private readonly ChunkRange range;

        private string chunkText;

        public string ChunkText
        {
            get { return string.IsNullOrEmpty(chunkText) ? string.Empty : chunkText; }
        }

        public Chunk(ChunkRange range, IEnumerable<ISnippet> snippets)
        {
            this.range = range;
            Snippets = new List<ISnippet>(snippets);
        }

        public Chunk(ChunkRange range, IEnumerable<ISnippet> snippets, string chunkText)
        {
            this.chunkText = chunkText.Trim('\'');
            this.range = range;
            Snippets = new List<ISnippet>(snippets);
        }

        public ChangeRange OriginalRange
        {
            get { return range.OriginalRange; }
        }

        public ChangeRange NewRange
        {
            get { return range.NewRange; }
        }

        public IEnumerable<ISnippet> Snippets { get; private set; }
    }
}
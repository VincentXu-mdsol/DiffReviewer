using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.Interfaces
{
    public interface IImportRepository
    {
        int SaveOriginalDiff(string diffContent, int pullRequestNumber, DateTime createdDate);

        int SaveIndividualDiff(int masterDiffId, string diffContent, DateTime createdDate, string originalFileName, string modifiedFileName, 
            bool isNewFile, bool isDelete, bool isBinary, bool hasHunks, string rangeStartHash, string rangeEndHash, int mode, string kind);

        int SaveHunk(int individualDiffId, int originalRangeStartLine, int originalRangeLinesAffected,
            int newRangeStartLine, int newRangeLinesAffected, string HunkHash, string HunkText, DateTime CreatedDate);

        int SaveSnippet(int hunkId, int snippetTypeId, DateTime createdDate);

        int SaveLine(int snippetId, string value, int lineTypeId, int lineOriginId, DateTime DateTimeCreated);

        int SaveLineSpan(int lineId, string value, int lineSpanTypeId, DateTime createdDate);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.Interfaces
{
    public interface IImportService
    {
        int SaveOriginalDiff(string diffContent, int pullRequestNumber, DateTime createdDate);

        int SaveIndividualDiff(int masterDiffId, string diffContent, DateTime createdDate, string originalFileName, string modifiedFileName, 
            bool isNewFile, bool isDelete, bool isBinary, bool hasHunks, string rangeStartHash, string rangeEndHash, int mode, string kind);

        int SaveHunk(int individualDiffId, int originalRangeStartLine, int originalRangeLinesAffected,
            int newRangeStartLine, int newRangeLinesAffected,  string hunkHash,  string hunkText, DateTime CreatedDate);

        int SaveSnippet(int hunkId, string snippetTypeName, DateTime createdDate);

        int SaveLine(int snippetId, string value, string lineTypeName, string lineOriginName, DateTime createdDate);

        int SaveLineSpan(int lineId, string value, string lineSpanTypeName, DateTime createdDate);
    }
}

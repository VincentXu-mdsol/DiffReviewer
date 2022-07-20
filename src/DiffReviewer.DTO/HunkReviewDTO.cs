using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.DTO
{
    public class HunkReviewDTO
    {
        public int MasterDiffId { get; set; }
        public int IndividualDiffId { get; set; }
        public string OriginalFileName { get; set; }
        public string ModifiedFileName { get; set; }
        public int HunkId { get; set; }
        public string HunkHash { get; set; }
        public string HunkText { get; set; }
        public bool IsCheckedOut { get; set; }
        public int AcceptedCount { get; set; }
        public int ReviewCount { get; set; }
        public int ApprovedCount { get; set; }
        public int ApprovalCount { get; set; }
        public bool IsFullyReviewed { get; set; }
        public bool IsFullyApproved { get; set; }
    }
}

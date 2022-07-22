using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.DTO
{
    public class ReviewStatsDTO
    {
        public int TotalHunksCheckedOut { get; set; }

        public int TotalAcceptedReviews { get; set; }

        public int TotalRejectedReviews { get; set; }

        public int TotalReviews { get; set; }

        public int TotalFullyReviewed { get; set; }

        public int TotalReviewsApproved { get; set; }

        public int TotalReviewsNotApproved { get; set; }

        public int TotalApprovals { get; set; }

        public int TotalFullyApproved { get; set; }

        public int HunkCount { get; set; }
    }
}

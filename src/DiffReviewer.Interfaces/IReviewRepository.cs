using DiffReviewer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.Interfaces
{
    public interface IReviewRepository
    {
        public CheckInResponseDTO CheckInHunk(string hunkHash, int userId);

        public CheckOutResponseDTO CheckOutHunk(string hunkHash, int userId);

        public CheckOutStatusResponseDTO GetCheckOutStatus(string hunkHash);

        public HunkReviewsDTO GetHunksToReview(int pullRequestNumber, bool showAll = true, bool showFullyReviewed = false, bool showFullyApproved = false);

        public HunkReviewResponseDTO SetHunkReviewStatus(string hunkHash, bool isAccepted, int userId, string comments);

        public HunkApprovalResponseDTO SetHunkApprovalStatus(string hunkHash, bool isApproved, int userId, string comments);

        public List<int> GetPullRequestNumbers();

        public ReviewStatsDTO GetReviewStats(int pullRequestNumber);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DiffReviewer.DTO;

namespace DiffReviewer.Interfaces
{
    public interface IReviewService
    {
        public CheckInResponseDTO CheckInHunk(string hunkHash, int userId);

        public CheckOutResponseDTO CheckOutHunk(string hunkHash, int userId);

        public CheckOutStatusResponseDTO GetCheckOutStatus(string hunkHash);

        public HunkReviewsDTO GetAllHunks(int pullRequestNumber);

        public HunkReviewsDTO GetUnreviewedHunks(int pullRequestNumber);

        public HunkReviewsDTO GetUnApprovedHunks(int pullRequestNumber);

        public HunkReviewsDTO GetCompletedHunks(int pullRequestNumber);

        public HunkReviewsDTO GetHunksToReview(int pullRequestNumber, bool showAll, bool showFullyReviewed, bool showFullyApproved);

        public HunkReviewResponseDTO SetHunkReviewStatus(string hunkHash, bool isAccepted, int userId, string comments);

        public HunkApprovalResponseDTO SetHunkApprovalStatus(string hunkHash, bool isApproved, int userId, string comments);

        public List<int> GetPullRequestNumbers();

        public ReviewStatsDTO GetReviewStats(int pullRequestNumber);
    }
}

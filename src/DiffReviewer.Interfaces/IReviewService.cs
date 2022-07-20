using System;
using System.Collections.Generic;
using System.Text;
using DiffReviewer.DTO;

namespace DiffReviewer.Interfaces
{
    public interface IReviewService
    {
        public CheckInResponseDTO CheckInHunk(int hunkId, int userId);

        public CheckOutResponseDTO CheckOutHunk(int hunkId, int userId);

        public CheckOutStatusResponseDTO GetCheckOutStatus(int hunkId);

        public HunkReviewsDTO GetAllHunks(int pullRequestNumber);

        public HunkReviewsDTO GetUnreviewedHunks(int pullRequestNumber);

        public HunkReviewsDTO GetUnApprovedHunks(int pullRequestNumber);

        public HunkReviewsDTO GetCompletedHunks(int pullRequestNumber);

        public HunkReviewsDTO GetHunksToReview(int pullRequestNumber, bool showAll, bool showFullyReviewed, bool showFullyApproved);

        public HunkReviewResponseDTO SetHunkReviewStatus(int hunkId, bool isAccepted, int userId, string comments);

        public HunkApprovalResponseDTO SetHunkApprovalStatus(int hunkId, bool isApproved, int userId, string comments);

        public List<int> GetPullRequestNumbers();
    }
}

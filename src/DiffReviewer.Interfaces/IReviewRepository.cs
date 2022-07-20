using DiffReviewer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.Interfaces
{
    public interface IReviewRepository
    {
        public CheckInResponseDTO CheckInHunk(int hunkId, int userId);

        public CheckOutResponseDTO CheckOutHunk(int hunkId, int userId);

        public CheckOutStatusResponseDTO GetCheckOutStatus(int hunkId);

        public HunkReviewsDTO GetHunksToReview(int pullRequestNumber, bool showAll = true, bool showFullyReviewed = false, bool showFullyApproved = false);

        public HunkReviewResponseDTO SetHunkReviewStatus(int hunkId, bool isAccepted, int userId, string comments);

        public HunkApprovalResponseDTO SetHunkApprovalStatus(int hunkId, bool isApproved, int userId, string comments);

        public List<int> GetPullRequestNumbers();
    }
}

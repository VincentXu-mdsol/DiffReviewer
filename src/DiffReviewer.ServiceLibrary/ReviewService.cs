using DiffReviewer.DTO;
using DiffReviewer.Interfaces;
using System;
using System.Collections.Generic;

namespace DiffReviewer.ServiceLibrary
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public CheckInResponseDTO CheckInHunk(string hunkHash, int userId)
        {
            return _reviewRepository.CheckInHunk(hunkHash, userId);
        }

        public CheckOutResponseDTO CheckOutHunk(string hunkHash, int userId)
        {
            return _reviewRepository.CheckOutHunk(hunkHash, userId);
        }

        public CheckOutStatusResponseDTO GetCheckOutStatus(string hunkHash)
        {
            return _reviewRepository.GetCheckOutStatus(hunkHash);
        }

        public HunkReviewsDTO GetAllHunks(int pullRequestNumber)
        {
            return _reviewRepository.GetHunksToReview(pullRequestNumber);
        }

        public HunkReviewsDTO GetCompletedHunks(int pullRequestNumber)
        {
            return _reviewRepository.GetHunksToReview(pullRequestNumber, false, true, true);
        }

        public HunkReviewsDTO GetHunksToReview(int pullRequestNumber, bool showAll, bool showFullyReviewed, bool showFullyApproved)
        {
            return _reviewRepository.GetHunksToReview(pullRequestNumber, showAll, showFullyReviewed, showFullyApproved);
        }

        public HunkReviewsDTO GetUnApprovedHunks(int pullRequestNumber)
        {
            return _reviewRepository.GetHunksToReview(pullRequestNumber, false, true, false);
        }

        public HunkReviewsDTO GetUnreviewedHunks(int pullRequestNumber)
        {
            return _reviewRepository.GetHunksToReview(pullRequestNumber, false, false, false);
        }

        public HunkReviewResponseDTO SetHunkReviewStatus(string hunkHash, bool isAccepted, int userId, string comments)
        {
            return _reviewRepository.SetHunkReviewStatus(hunkHash, isAccepted, userId, comments);
        }

        public HunkApprovalResponseDTO SetHunkApprovalStatus(string hunkHash, bool isApproved, int userId, string comments)
        {
            return _reviewRepository.SetHunkApprovalStatus(hunkHash, isApproved, userId, comments);
        }

        public List<int> GetPullRequestNumbers()
        {
            return _reviewRepository.GetPullRequestNumbers();
        }

        public ReviewStatsDTO GetReviewStats(int pullRequestNumber)
        {
            return _reviewRepository.GetReviewStats(pullRequestNumber);
        }
    }
}

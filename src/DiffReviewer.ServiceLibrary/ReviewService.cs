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

        public CheckInResponseDTO CheckInHunk(int hunkId, int userId)
        {
            return _reviewRepository.CheckInHunk(hunkId, userId);
        }

        public CheckOutResponseDTO CheckOutHunk(int hunkId, int userId)
        {
            return _reviewRepository.CheckOutHunk(hunkId, userId);
        }

        public CheckOutStatusResponseDTO GetCheckOutStatus(int hunkId)
        {
            return _reviewRepository.GetCheckOutStatus(hunkId);
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

        public HunkReviewResponseDTO SetHunkReviewStatus(int hunkId, bool isAccepted, int userId, string comments)
        {
            return _reviewRepository.SetHunkReviewStatus(hunkId, isAccepted, userId, comments);
        }

        public HunkApprovalResponseDTO SetHunkApprovalStatus(int hunkId, bool isApproved, int userId, string comments)
        {
            return _reviewRepository.SetHunkApprovalStatus(hunkId, isApproved, userId, comments);
        }

        public List<int> GetPullRequestNumbers()
        {
            return _reviewRepository.GetPullRequestNumbers();
        }
    }
}

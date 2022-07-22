using DiffReviewer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiffReviewer.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DiffReviewerController : ControllerBase
    {
        private readonly ILogger<DiffReviewerController> _logger;
        private readonly IReviewService _reviewService;

        public DiffReviewerController(ILogger<DiffReviewerController> logger, IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

        [HttpGet("{pullRequestNumber}")]
        public JsonResult GetAllHunks(int pullRequestNumber)
        {
            var retval = _reviewService.GetAllHunks(pullRequestNumber);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{pullRequestNumber}")]
        public JsonResult GetCompletedHunks(int pullRequestNumber)
        {
            var retval = _reviewService.GetCompletedHunks(pullRequestNumber);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{pullRequestNumber}/{showAll}/{showFullyReviewd}/{showFullyApproved}")]
        public JsonResult GetHunksToReview(int pullRequestNumber, bool showAll, bool showFullyReviewd, bool showFullyApproved)
        {
            var retval = _reviewService.GetHunksToReview(pullRequestNumber, showAll, showFullyReviewd, showFullyApproved);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{pullRequestNumber}")]
        public JsonResult GetUnApprovedHunks(int pullRequestNumber)
        {
            var retval = _reviewService.GetUnApprovedHunks(pullRequestNumber);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{pullRequestNumber}")]
        public JsonResult GetUnreviewedHunks(int pullRequestNumber)
        {
            var retval = _reviewService.GetUnreviewedHunks(pullRequestNumber);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{hunkHash}")]
        public JsonResult GetCheckOutStatus(string hunkHash)
        {
            var retval = _reviewService.GetCheckOutStatus(hunkHash);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpPost("{hunkHash}/{userId}")]
        public JsonResult CheckOutHunk(string hunkHash, int userId)
        {
            var retval = _reviewService.CheckOutHunk(hunkHash, userId);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpPost("{hunkHash}/{userId}")]
        public JsonResult CheckInHunk(string hunkHash, int userId)
        {
            var retval = _reviewService.CheckInHunk(hunkHash, userId);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpPost("{hunkHash}/{userId}/{IsAccepted}/{comments}")]
        public JsonResult SetHunkReviewStatus(string hunkHash, int userId, bool isAccepted, string comments)
        {
            var retval = _reviewService.SetHunkReviewStatus(hunkHash, isAccepted, userId, comments);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpPost("{hunkHash}/{userId}/{IsApproved}/{comments}")]
        public JsonResult SetHunkApprovalStatus(string hunkHash, int userId, bool isApproved, string comments)
        {
            var retval = _reviewService.SetHunkApprovalStatus(hunkHash, isApproved, userId, comments);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet]
        public JsonResult GetPullRequestNumbers()
        {
            var retval = _reviewService.GetPullRequestNumbers();
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet("{pullRequestNumber}")]
        public JsonResult GetReviewStats(int pullRequestNumber)
        {
            var retval = _reviewService.GetReviewStats(pullRequestNumber);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }
    }
}

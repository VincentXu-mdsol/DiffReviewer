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
    //[Route("[controller]")]
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

        [HttpGet("{pullrequestnumber}")]
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

        [HttpGet("{hunkId}")]
        public JsonResult GetCheckOutStatus(int hunkId)
        {
            var retval = _reviewService.GetCheckOutStatus(hunkId);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpPost("{hunkId}/{userId}")]
        public JsonResult CheckOutHunk(int hunkId, int userId)
        {
            var retval = _reviewService.CheckOutHunk(hunkId,userId);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpPost("{hunkId}/{userId}")]
        public JsonResult CheckInHunk(int hunkId, int userId)
        {
            var retval = _reviewService.CheckInHunk(hunkId, userId);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpPost("{hunkId}/{userId}/{IsAccepted}/{comments}")]
        public JsonResult SetHunkReviewStatus(int hunkId, int userId, bool isAccepted, string comments)
        {
            var retval = _reviewService.SetHunkReviewStatus(hunkId, isAccepted, userId, comments);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpPost("{hunkId}/{userId}/{IsApproved}/{comments}")]
        public JsonResult SetHunkApprovalStatus(int hunkId, int userId, bool isApproved, string comments)
        {
            var retval = _reviewService.SetHunkApprovalStatus(hunkId, isApproved, userId, comments);
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet]
        public JsonResult GetPullRequestNumbers()
        {
            var retval = _reviewService.GetPullRequestNumbers();
            return new JsonResult(retval, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }
    }
}

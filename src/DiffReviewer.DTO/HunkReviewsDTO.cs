using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.DTO
{
    public class HunkReviewsDTO
    {
        public int PullRequestNumber { get; set; }
        public List<HunkReviewDTO> Hunks { get; set; }
    }
}

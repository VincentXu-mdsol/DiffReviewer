using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.DTO
{
    public class HunkReviewResponseDTO
    {
        public string HunkHash { get; set; }

        public bool IsAccepted { get; set; }

        public int UserId { get; set; }

        public string Status { get; set; }
    }
}

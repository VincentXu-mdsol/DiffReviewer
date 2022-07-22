using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.DTO
{
    public class HunkApprovalResponseDTO
    {
        public string HunkHash { get; set; }

        public bool IsApproved { get; set; }

        public int UserId { get; set; }

        public string Status { get; set; }
    }
}

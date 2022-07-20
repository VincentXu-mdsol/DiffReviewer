using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.DTO
{
    public class CheckOutResponseDTO
    {
        public int HunkId { get; set; }

        public bool IsCheckedOutByOtherUser { get; set; }

        public int UserId { get; set; }

        public string Status { get; set; }
    }
}

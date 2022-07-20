using System;
using System.Collections.Generic;
using System.Text;

namespace DiffReviewer.DTO
{
    public class CheckOutStatusResponseDTO
    {
        public int HunkId { get; set; }

        public bool IsCheckedOut { get; set; }

        public int CheckOutUserId { get; set; }

        public DateTime? CheckOutDate { get; set; }
    }
}

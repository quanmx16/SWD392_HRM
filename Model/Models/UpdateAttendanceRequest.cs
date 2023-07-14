using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class UpdateAttendanceRequest
    {
        public int RequestId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public DateTime Date { get; set; }
        public DateTime? TimeIn { get; set; }
        public string? Reason { get; set; }
        public string? Hrid { get; set; }
        public string? Status { get; set; }
        public string? Comment { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Employee? Hr { get; set; }
    }
}

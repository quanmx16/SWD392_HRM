using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class LeaveRequest
    {
        public int RequestId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public DateTime? DateOff { get; set; }
        public int? DaysLeave { get; set; }
        public string? Reason { get; set; }
        public string? Hrid { get; set; }
        public string? Status { get; set; }
        public string? Comment { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Employee? Hr { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class TaxRequest
    {
        public int RequestId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public DateTime? RequestDate { get; set; }
        public int? TaxYear { get; set; }
        public decimal? TotalIncome { get; set; }
        public decimal? Deductions { get; set; }
        public decimal? TaxableIncome { get; set; }
        public decimal? TaxAmount { get; set; }
        public string? RequestStatus { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string? ApproverId { get; set; }
        public string? Comment { get; set; }

        public virtual Employee? Approver { get; set; }
        public virtual Employee Employee { get; set; } = null!;
    }
}

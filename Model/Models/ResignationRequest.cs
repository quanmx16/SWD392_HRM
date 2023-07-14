using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Model.Models
{
    public partial class ResignationRequest
    {
        public int RequestId { get; set; }
        public string EmployeeId { get; set; } = null!;
        [Display(Name = "Request Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? RequestDate { get; set; }
        [Display(Name = "Last Working Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? LastWorkingDate { get; set; }
        public string? Reason { get; set; }
        public string? RequestStatus { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string? ApproverId { get; set; }
        public string? Comment { get; set; }

        public virtual Employee? Approver { get; set; }
        public virtual Employee Employee { get; set; } = null!;
    }
}

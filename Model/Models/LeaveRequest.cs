using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public partial class LeaveRequest
    {
        public int RequestId { get; set; }
        public string EmployeeId { get; set; } = null!;

        [Display(Name = "Date Off")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
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

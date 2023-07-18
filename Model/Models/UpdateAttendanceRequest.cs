using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public partial class UpdateAttendanceRequest
    {
        public int RequestId { get; set; }
        public string EmployeeId { get; set; } = null!;

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
        [Display(Name = "Time In")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? TimeIn { get; set; }

        public string? Reason { get; set; }
        public string? Hrid { get; set; }
        public string? Status { get; set; }
        public string? Comment { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Employee? Hr { get; set; }
    }
}

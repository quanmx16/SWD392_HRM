namespace Model.Models
{
    public partial class Otrequest
    {
        public string EmployeeId { get; set; } = null!;
        public int RequestId { get; set; }
        public DateTime? DateRequest { get; set; }
        public DateTime? TimeStartOt { get; set; }
        public DateTime? TimeEndOt { get; set; }
        public string? TaskOt { get; set; }
        public string? RequestStatus { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string? ApproverId { get; set; }
        public string? Comment { get; set; }

        public virtual Employee? Approver { get; set; }
        public virtual Employee Employee { get; set; } = null!;
    }
}

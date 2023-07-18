namespace Model.Models
{
    public partial class ChangeWorkDepartmentRequest
    {
        public int RequestId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public string? ReasonForMoving { get; set; }
        public string? DepartmentMoveToId { get; set; }
        public DateTime? DateWantToMove { get; set; }
        public DateTime? DateCreateRequest { get; set; }
        public string? CurrentDepartmentId { get; set; }
        public string? Hrid { get; set; }
        public bool? ResponseRequest { get; set; }
        public DateTime? DateResponeRequest { get; set; }
        public DateTime? DateMoveToNewDepartment { get; set; }

        public virtual Department? CurrentDepartment { get; set; }
        public virtual Department? DepartmentMoveTo { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual Employee? Hr { get; set; }
    }
}

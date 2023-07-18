namespace Model.Models
{
    public partial class Attendance
    {
        public int AttendanceId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public DateTime? AttendanceDate { get; set; }
        public DateTime? TimeCheck { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}

namespace Model.Models
{
    public partial class Attendance
    {
        public int AttendanceId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public DateTime? AttendanceDate { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public decimal? DayIncome { get; set; }
        public DateTime? OttimeIn { get; set; }
        public DateTime? OttimeOut { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}

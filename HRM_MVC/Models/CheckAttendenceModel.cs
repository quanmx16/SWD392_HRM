using Model.Models;

namespace HRM_MVC.Models
{
    public class CheckAttendenceModel
    {
        public List<AttendanceModel> Attendances { get; set; }
        public DateTime DateCheck { get; set; }
    }
    public class AttendanceModel
    {
        public Employee Employee { get; set; }
        public bool IsChecked { get; set; } = false;
    }
}

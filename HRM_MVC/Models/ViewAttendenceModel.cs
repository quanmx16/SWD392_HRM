using Model.Models;

namespace HRM_MVC.Models
{
    public class ViewAttendenceModel
    {
        public int WeekNumb { get; set; }
        public int Year { get; set; }
        public List<DateInWeek> DateInWeek { get; set; }
        public List<Attendance> Attendances { get; set; }
    }
    public class DateInWeek
    {
        public int WeekNum { get; set; }
        public string WeekDisplay { get; set; }
    }
}

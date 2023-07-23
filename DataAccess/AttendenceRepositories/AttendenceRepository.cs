using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;

namespace DataAccess.AttendenceRepositories
{
    public interface IAttendenceRepository
    {
        void CheckAttendence(List<string> empId);
        void CheckOut(List<string> empId);
        List<Attendance> GetCheckOut(DateTime date);
        List<Attendance> GetAttendenceOfDate(DateTime date);
        List<Attendance> GetAttendenceOfEmp(string id, DateTime startDate, DateTime endDate);
    }
    public class AttendenceRepository : IAttendenceRepository
    {
        private readonly HRM_SWD392Context context;
        public AttendenceRepository()
        {
            context = new HRM_SWD392Context();
        }
        public void CheckAttendence(List<string> empId)
        {
            var lstAttend = GetAttendenceOfDate(DateTime.Now);
            foreach (string id in empId)
            {
                var check = false;
                foreach (Attendance attendance in lstAttend)
                {
                    if(attendance.EmployeeId.Trim() == id.Trim())
                    {
                        check = true;
                    }
                }
                if (!check)
                {
                    context.Attendances.Add(new Attendance
                    {
                        AttendanceDate = DateTime.Now.Date,
                        CheckInTime = DateTime.Now,
                        EmployeeId = id
                    });
                }
            }
            context.SaveChanges();
        }
        public void CheckOut(List<string> empId)
        {
            var lstAttend = GetAttendenceOfDate(DateTime.Now);
            foreach (string id in empId)
            {
                foreach (Attendance attendance in lstAttend)
                {
                    if (attendance.EmployeeId.Trim() == id.Trim() && attendance.CheckOutTime == null)
                    {
                        attendance.CheckOutTime = DateTime.Now;
                        double workHour = attendance.CheckOutTime.Value.Hour - attendance.CheckInTime.Value.Hour;
                        if (workHour > 8)
                        {
                            workHour = 8;
                        }
                        attendance.DayIncome = Math.Round((decimal)((attendance.Employee.Salary / 22m) / 8m * (decimal)workHour), 2);
                        context.Attendances.Update(attendance);
                    }
                }
            }
            context.SaveChanges();
        }
        public List<Attendance> GetCheckOut(DateTime date)
        {
            return context.Attendances.Where(x => x.CheckInTime != null && x.AttendanceDate.Value.Date == date.Date).Include(x => x.Employee).ToList();
        }
        public List<Attendance> GetAttendenceOfDate(DateTime date)
        {
            return context.Attendances.Where(x => x.AttendanceDate.Value.Date == date.Date).Include(x => x.Employee).ToList();
        }

        public List<Attendance> GetAttendenceOfEmp(string id, DateTime startDate, DateTime endDate)
        {
            return context.Attendances.Where(x => x.EmployeeId.Trim() == id.Trim() && x.AttendanceDate.Value >= startDate && x.AttendanceDate.Value <= endDate).ToList();
        }
    }
}

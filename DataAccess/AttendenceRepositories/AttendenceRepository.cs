using Model.Data;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AttendenceRepositories
{
    public interface IAttendenceRepository
    {
        void CheckAttendence(List<string> empId,DateTime dateCheck);
        List<Attendance> GetAttendenceOfDate(DateTime date);
        List<Attendance> GetAttendenceOfEmp(string id,DateTime startDate,DateTime endDate);
    }
    public class AttendenceRepository: IAttendenceRepository
    {
        private readonly HRM_SWD392Context context;
        public AttendenceRepository() { 
            context = new HRM_SWD392Context();
        }
        public void CheckAttendence(List<string> empId, DateTime dateCheck)
        {
            foreach (string id in empId)
            {
                Attendance attendance = new Attendance
                {
                    AttendanceDate = dateCheck.Date,
                    TimeCheck = DateTime.Now,
                    EmployeeId = id
                };
                context.Attendances.Add(attendance);
            }
            context.SaveChanges();
        }

        public List<Attendance> GetAttendenceOfDate(DateTime date)
        {
            return context.Attendances.Where(x => x.AttendanceDate == date.Date).ToList();
        }

        public List<Attendance> GetAttendenceOfEmp(string id, DateTime startDate, DateTime endDate)
        {
            return context.Attendances.Where(x => x.EmployeeId == id && x.AttendanceDate.Value >= startDate && x.AttendanceDate.Value <= endDate).ToList();
        }
    }
}

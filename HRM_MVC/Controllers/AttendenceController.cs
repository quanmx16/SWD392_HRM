using DataAccess.AttendenceRepositories;
using DataAccess.EmployeeRepositories;
using HRM_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HRM_MVC.Controllers
{
    public class AttendenceController : Controller
    {

        private readonly IEmployeeRepository employeeRepository;
        private readonly IAttendenceRepository attendenceRepository;
        public AttendenceController()
        {
            employeeRepository = new EmployeeRepository();
            attendenceRepository = new AttendenceRepository();
        }
        [HttpGet]
        public IActionResult Index(DateTime? dateCheck)
        {
            CheckAttendenceModel checkAttendenceModels = new CheckAttendenceModel();
            if (dateCheck == null)
            {
                dateCheck = DateTime.Now;
            }
            checkAttendenceModels.DateCheck = dateCheck.Value;
            var attendence = attendenceRepository.GetAttendenceOfDate(dateCheck.Value);
            var list = employeeRepository.GetAll();
            checkAttendenceModels.Attendances = new List<AttendanceModel>();
            foreach (var item in list)
            {
                bool check = false;
                foreach (var att in attendence)
                {
                    if (item.EmployeeId == att.EmployeeId)
                    {
                        checkAttendenceModels.Attendances.Add(new AttendanceModel
                        {
                            Employee = item,
                            IsChecked = true
                        });
                        check = true;
                    }
                }
                if (!check)
                {
                    checkAttendenceModels.Attendances.Add(new AttendanceModel
                    {
                        Employee = item,
                        IsChecked = false
                    });
                }
            }
            return View(checkAttendenceModels);
        }
        [HttpPost]
        public IActionResult SubmitAttendence(CheckAttendenceModel model)
        {
            List<string> list = new List<string>();
            foreach (var emp in model.Attendances)
            {
                if (emp.IsChecked)
                {
                    list.Add(emp.Employee.EmployeeId);
                }
            }
            attendenceRepository.CheckAttendence(list, model.DateCheck);
            return RedirectToAction("Index", model);
        }
        [HttpGet]
        public IActionResult ViewAttendence(string id)
        {

            return View();
        }
    }
}

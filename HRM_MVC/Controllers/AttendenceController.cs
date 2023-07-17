﻿using DataAccess.AttendenceRepositories;
using DataAccess.EmployeeRepositories;
using HRM_MVC.Common;
using HRM_MVC.Models;
using HRM_MVC.SessionManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.Models;
using System.Globalization;

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
            var user = AuthorAuthen();
            if (user == null || user.Role.Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Index", "Login");
            }
            else
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
        }
        [HttpPost]
        public IActionResult SubmitAttendence(CheckAttendenceModel model)
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Index", "Login");
            }
            else
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
        }
        [HttpGet]
        public IActionResult ViewAttendence()
        {
            var user = AuthorAuthen();
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewAttendenceModel model = new ViewAttendenceModel();
                model.WeekNumb = GetCurrentWeekNumber() + 1;
                model.Year = DateTime.Now.Year;
                model.DateInWeek = new List<DateInWeek>();
                for (int i = 0; i <= GetWeekNumbersInYear(DateTime.Now.Year).Count(); i++)
                {
                    DateTime startDate = GetFirstDateOfWeek(DateTime.Now.Year, i);
                    DateTime endDate = GetLastDateOfWeek(DateTime.Now.Year, i);
                    model.DateInWeek.Add(new DateInWeek
                    {
                        WeekNum = i,
                        WeekDisplay = startDate.Day + "/" + startDate.Month + " to " + endDate.Day + "/" + endDate.Month
                    });
                }
                DateTime start = GetFirstDateOfWeek(DateTime.Now.Year, GetCurrentWeekNumber() + 1);
                DateTime end = GetLastDateOfWeek(DateTime.Now.Year, GetCurrentWeekNumber() + 1);
                model.Attendances = new List<Attendance>();
                var atts = attendenceRepository.GetAttendenceOfEmp(user.EmployeeId, start, end);
                for (DateTime currentDate = start; currentDate <= end.AddDays(-2); currentDate = currentDate.AddDays(1))
                {
                    bool check = false;
                    foreach (var att in atts)
                    {
                        if (att.AttendanceDate == currentDate)
                        {
                            model.Attendances.Add(att);
                            check = true;
                        }
                    }
                    if (!check)
                    {
                        if (currentDate < user.DayOne)
                        {
                            model.Attendances.Add(new Attendance
                            {
                                AttendanceDate = currentDate,
                                EmployeeId = "Not in",
                                AttendanceId = -1,
                                Employee = null,
                                TimeCheck = DateTime.MinValue
                            });
                        }
                        else if (currentDate > DateTime.Now)
                        {
                            model.Attendances.Add(new Attendance
                            {
                                AttendanceDate = currentDate,
                                EmployeeId = "Not yet",
                                AttendanceId = -1,
                                Employee = null,
                                TimeCheck = DateTime.MinValue
                            });
                        }
                        else
                        {
                            model.Attendances.Add(new Attendance
                            {
                                AttendanceDate = currentDate,
                                EmployeeId = "",
                                AttendanceId = -1,
                                Employee = null,
                                TimeCheck = DateTime.MinValue
                            });
                        }
                    }
                }
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult ViewAttendence(int weekSelector, int yearSelector)
        {
            var user = AuthorAuthen();
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewAttendenceModel model = new ViewAttendenceModel();
                model.WeekNumb = weekSelector;
                model.Year = yearSelector;
                model.DateInWeek = new List<DateInWeek>();
                for (int i = 1; i <= GetWeekNumbersInYear(yearSelector).Count(); i++)
                {
                    DateTime startDate = GetFirstDateOfWeek(yearSelector, i);
                    DateTime endDate = GetLastDateOfWeek(yearSelector, i);
                    model.DateInWeek.Add(new DateInWeek
                    {
                        WeekNum = i,
                        WeekDisplay = startDate.Day + "/" + startDate.Month + " to " + endDate.Day + "/" + endDate.Month
                    });
                }
                DateTime start = GetFirstDateOfWeek(yearSelector, weekSelector);
                DateTime end = GetLastDateOfWeek(yearSelector, weekSelector);
                model.Attendances = new List<Attendance>();
                var atts = attendenceRepository.GetAttendenceOfEmp(user.EmployeeId, start, end);
                for (DateTime currentDate = start; currentDate <= end.AddDays(-2); currentDate = currentDate.AddDays(1))
                {
                    bool check = false;
                    foreach (var att in atts)
                    {
                        if (att.AttendanceDate == currentDate)
                        {
                            model.Attendances.Add(att);
                            check = true;
                        }
                    }
                    if (!check)
                    {
                        if (currentDate < user.DayOne)
                        {
                            model.Attendances.Add(new Attendance
                            {
                                AttendanceDate = currentDate,
                                EmployeeId = "Not in",
                                AttendanceId = -1,
                                Employee = null,
                                TimeCheck = DateTime.MinValue
                            });
                        }
                        else if (currentDate > DateTime.Now)
                        {
                            model.Attendances.Add(new Attendance
                            {
                                AttendanceDate = currentDate,
                                EmployeeId = "Not yet",
                                AttendanceId = -1,
                                Employee = null,
                                TimeCheck = DateTime.MinValue
                            });
                        }
                        else
                        {
                            model.Attendances.Add(new Attendance
                            {
                                AttendanceDate = currentDate,
                                EmployeeId = "",
                                AttendanceId = -1,
                                Employee = null,
                                TimeCheck = DateTime.MinValue
                            });
                        }
                    }
                }
                return View(model);
            }
        }
        private Employee AuthorAuthen()
        {
            LoginAccount? loginAccount = SessionHelper.GetObjectFromSession<LoginAccount>(HttpContext.Session, KeyConstants.ACCOUNT_KEY);
            if (loginAccount == null)
            {
                return null;
            }
            else
            {
                return loginAccount.Employee;
            }
        }
        private static List<int> GetWeekNumbersInYear(int year)
        {
            List<int> weekNumbers = new List<int>();
            CultureInfo ci = CultureInfo.CurrentCulture;
            Calendar cal = ci.Calendar;

            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = cal.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1)
            {
                firstWeekDay = firstWeekDay.AddDays(-7);
            }

            int numberOfWeeksInYear = cal.GetWeekOfYear(new DateTime(year, 12, 31), ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);

            for (int weekNumber = 1; weekNumber <= numberOfWeeksInYear; weekNumber++)
            {
                weekNumbers.Add(weekNumber);
            }

            return weekNumbers;
        }
        private static DateTime GetFirstDateOfWeek(int year, int weekNumber)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1)
            {
                firstWeekDay = firstWeekDay.AddDays(-7);
            }

            return firstWeekDay.AddDays((weekNumber - 1) * 7);
        }
        private static DateTime GetLastDateOfWeek(int year, int weekNumber)
        {
            DateTime firstDayOfWeek = GetFirstDateOfWeek(year, weekNumber);
            return firstDayOfWeek.AddDays(6);
        }
        private static int GetCurrentWeekNumber()
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            Calendar cal = ci.Calendar;
            int currentWeekNumber = cal.GetWeekOfYear(DateTime.Today, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            return currentWeekNumber;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.EmployeeRepositories;
using HRM_MVC.Common;
using HRM_MVC.Models;
using HRM_MVC.SessionManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;

namespace HRM_MVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HRM_SWD392Context _context;
        private readonly IEmployeeRepository employeeRepository;

        public EmployeesController(HRM_SWD392Context context)
        {
            _context = context;
            employeeRepository = new EmployeeRepository();
        }

        // GET: Employees
        public async Task<IActionResult> Index(string search)
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                EmpViewModel empViewModel = new EmpViewModel();
                if (string.IsNullOrEmpty(search))
                {
                    empViewModel.listEmp = employeeRepository.GetAll();
                }
                else
                {
                    empViewModel.listEmp = employeeRepository.Search(search);
                }
                return View(empViewModel);
            }
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (id == null || _context.Employees == null)
                {
                    return NotFound();
                }

                var employee = employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound();
                }

                return View(employee);
            }
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId");

                ViewData["ManagerId"] = new SelectList(employeeRepository.GetHROrHRM(), "EmployeeId", "EmplyeeName");

                return View();
            }
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmplyeeName,DateOfBirth,Gender,Email,Password,Role,DepartmentId,Phone,Address,Salary,TaxCode,Level,ManagerId,DayOne,LastDay")] Employee employee)
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    bool check = employeeRepository.CreateEmployee(employee);
                    if (check)
                    {
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                List<string> roles = new List<string>
                {
                    Roles.ROLE_EMPLOYEE,
                    Roles.ROLE_HR_MANAGER,
                    Roles.ROLE_HR
                };

                ViewData["Roles"] = new SelectList(roles, employee.Role);
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employee.DepartmentId);
                ViewData["ManagerId"] = new SelectList(employeeRepository.GetHROrHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
                return View(employee);
            }
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var employee = employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound();
                }
                else
                {
                    List<DropDownRole> roles = new List<DropDownRole>
                    {
                        new DropDownRole
                        {
                            RoleName = Roles.ROLE_EMPLOYEE,
                            Id = 1
                        },
                        new DropDownRole
                        {
                            RoleName = Roles.ROLE_HR,
                            Id = 2
                        },
                        new DropDownRole
                        {
                            RoleName = Roles.ROLE_HR_MANAGER,
                            Id = 3
                        }
                    };
                    ViewData["Roles"] = new SelectList(roles, "RoleName", "RoleName", employee.Role);
                    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employee.DepartmentId);
                    ViewData["ManagerId"] = new SelectList(employeeRepository.GetHROrHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
                    return View(employee);
                }
            }
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeId,EmplyeeName,DateOfBirth,Gender,Email,Password,Role,DepartmentId,Phone,Address,Salary,TaxCode,Level,ManagerId,DayOne,LastDay")] Employee employee)
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (id != employee.EmployeeId.Trim())
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var check = employeeRepository.UpdateEmployee(employee);
                    if (check)
                    {
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employee.DepartmentId);
                ViewData["ManagerId"] = new SelectList(employeeRepository.GetHROrHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
                return View(employee);
            }
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (id == null || _context.Employees == null)
                {
                    return NotFound();
                }

                var employee = employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound();
                }

                return View(employee);
            }
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (_context.Employees == null)
                {
                    return Problem("Entity set 'HRM_SWD392Context.Employees'  is null.");
                }
                employeeRepository.RemoveEmployee(id);
                return RedirectToAction(nameof(Index));
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
    }
}

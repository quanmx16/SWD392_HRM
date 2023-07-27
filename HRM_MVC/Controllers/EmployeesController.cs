using DataAccess.EmployeeRepositories;
using HRM_MVC.Common;
using HRM_MVC.Models;
using HRM_MVC.SessionManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Data;
using Model.Models;

namespace HRM_MVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HRM_SWD392Context _context;
        private readonly IEmployeeRepository employeeRepository;

        public EmployeesController(HRM_SWD392Context context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            this.employeeRepository = employeeRepository;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string search)
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Trim().Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Error");
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
        public async Task<IActionResult> Details()
        {
            var user = AuthorAuthen();
            if (user == null)
            {
                return RedirectToAction("Error");
            }
            else
            {

                var employee = employeeRepository.GetEmployeeById(user.EmployeeId);
                if (employee == null)
                {
                    return RedirectToAction("Error");
                }

                return View(employee);
            }
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Trim().Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Error");
            }
            else
            {
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");

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
            if (user == null || user.Role.Trim().Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Error");
            }
            else
            {
                List<string> roles = new List<string>
                {
                    Roles.ROLE_EMPLOYEE,
                    Roles.ROLE_HR_MANAGER,
                    Roles.ROLE_HR
                };
                if (ModelState.IsValid)
                {
                    var validate = true;
                    if (employee.Salary <= 0)
                    {
                        ViewData["ErrSalary"] = "Salary must be greater than 0";
                        validate = false;
                    }

                    if (validate)
                    {
                        bool check = employeeRepository.CreateEmployee(employee);
                        if (check)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }
                    else
                    {
                        ViewData["Roles"] = new SelectList(roles, employee.Role);
                        ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employee.DepartmentId);
                        ViewData["ManagerId"] = new SelectList(employeeRepository.GetHROrHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
                        return View("Create", employee);
                    }
                }
                else
                {
                    ViewData["Roles"] = new SelectList(roles, employee.Role);
                    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
                    ViewData["ManagerId"] = new SelectList(employeeRepository.GetHROrHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
                    return View("Create", employee);
                }
            }
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Trim().Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Error");
            }
            else
            {
                if (id == null)
                {
                    return RedirectToAction("Error");
                }

                List<string> roles = new List<string>
                {
                    Roles.ROLE_EMPLOYEE,
                    Roles.ROLE_HR_MANAGER,
                    Roles.ROLE_HR
                };
                var employee = employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return RedirectToAction("Error");
                }
                else
                {

                    ViewData["Roles"] = new SelectList(roles, employee.Role);
                    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
                    if (employee.Role.Trim() == Roles.ROLE_HR)
                    {
                        ViewData["ManagerId"] = new SelectList(employeeRepository.GetHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
                    }
                    else if (employee.Role.Trim() == Roles.ROLE_EMPLOYEE)
                    {
                        ViewData["ManagerId"] = new SelectList(employeeRepository.GetHROrHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
                    }
                    else
                    {
                        ViewData["ManagerId"] = null;
                    }
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
            if (user == null || user.Role.Trim().Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Error");
            }
            else
            {
                List<string> roles = new List<string>
                {
                    Roles.ROLE_EMPLOYEE,
                    Roles.ROLE_HR_MANAGER,
                    Roles.ROLE_HR
                };
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return RedirectToAction("Error");
                    }
                    var validate = true;
                    if (employee.Salary <= 0)
                    {
                        ViewData["ErrSalary"] = "Salary must be greater than 0";
                        validate = false;
                    }

                    if (validate)
                    {
                        var check = employeeRepository.UpdateEmployee(employee);
                        if (check)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }
                    else
                    {
                        ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
                        if (employee.Role.Trim() == Roles.ROLE_HR)
                        {
                            ViewData["ManagerId"] = new SelectList(employeeRepository.GetHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
                        }
                        else if (employee.Role.Trim() == Roles.ROLE_EMPLOYEE)
                        {
                            ViewData["ManagerId"] = new SelectList(employeeRepository.GetHROrHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
                        }
                        else
                        {
                            ViewData["ManagerId"] = null;
                        }
                        ViewData["Roles"] = new SelectList(roles, employee.Role);
                        return View("Edit", employee);
                    }
                }
                else
                {
                    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
                    if (employee.Role.Trim() == Roles.ROLE_HR)
                    {
                        ViewData["ManagerId"] = new SelectList(employeeRepository.GetHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
                    }
                    else if (employee.Role.Trim() == Roles.ROLE_EMPLOYEE)
                    {
                        ViewData["ManagerId"] = new SelectList(employeeRepository.GetHROrHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
                    }
                    else
                    {
                        ViewData["ManagerId"] = null;
                    }
                    ViewData["Roles"] = new SelectList(roles, employee.Role);
                    return View("Edit", employee);
                }
            }
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var user = AuthorAuthen();
            if (user == null || user.Role.Trim().Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Error");
            }
            else
            {
                if (id == null)
                {
                    return RedirectToAction("Error");
                }

                var employee = employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return RedirectToAction("Error");
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
            if (user == null || user.Role.Trim().Equals(Roles.ROLE_EMPLOYEE))
            {
                return RedirectToAction("Error");
            }
            else
            {
                if (_context.Employees == null)
                {
                    return RedirectToAction("Error");
                }
                employeeRepository.RemoveEmployee(id);
                return RedirectToAction("Index");
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
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}

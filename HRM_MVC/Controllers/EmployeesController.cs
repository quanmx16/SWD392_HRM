using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.EmployeeRepositories;
using HRM_MVC.Models;
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

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
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

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId");

            ViewData["ManagerId"] = new SelectList(employeeRepository.GetHROrHRM(), "EmployeeId", "EmplyeeName");

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmplyeeName,DateOfBirth,Gender,Email,Password,Role,DepartmentId,Phone,Address,Salary,TaxCode,Level,ManagerId,DayOne,LastDay")] Employee employee)
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employee.DepartmentId);
            ViewData["ManagerId"] = new SelectList(employeeRepository.GetHROrHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
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
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employee.DepartmentId);
                ViewData["ManagerId"] = new SelectList(employeeRepository.GetHROrHRM(), "EmployeeId", "EmplyeeName", employee.ManagerId);
                return View(employee);
            }
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeId,EmplyeeName,DateOfBirth,Gender,Email,Password,Role,DepartmentId,Phone,Address,Salary,TaxCode,Level,ManagerId,DayOne,LastDay")] Employee employee)
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

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
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

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'HRM_SWD392Context.Employees'  is null.");
            }
            employeeRepository.RemoveEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

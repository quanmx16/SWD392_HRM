using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.EmployeeRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;

namespace HRM_MVC.Controllers
{
    public class EmployeesController : Controller
    {
        protected IEmployeeRepository employeeRepository;
        protected readonly HRM_SWD392Context _context;
        public EmployeesController()
        {
            _context = new HRM_SWD392Context();
            employeeRepository = new EmployeeRepository(_context);
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.EmployeeRepositories;
using DataAccess.EmpRequestLeaveRepositories;
using HRM_MVC.Common;
using HRM_MVC.SessionManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Model.Data;
using Model.Models;

namespace HRM_MVC.Controllers
{
    public class EmpLeaveRequestsController : Controller
    {
        protected IEmpRequestLeaveRepository empRequestLeaveRepository;
        protected readonly HRM_SWD392Context _context;
        public EmpLeaveRequestsController()
        {
            _context = new HRM_SWD392Context();
            empRequestLeaveRepository = new EmpRequestLeaveRepository(_context);
        }

        //GET: EmpLeaveRequests
        //public async Task<IActionResult> Index()
        //{
        //    var hRM_SWD392Context = _context.LeaveRequests.Include(l => l.Employee).Include(l => l.Hr);
        //    return View(await hRM_SWD392Context.ToListAsync());
        //}

        //// GET: EmpLeaveRequests/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.LeaveRequests == null)
        //    {
        //        return NotFound();
        //    }

        //    var leaveRequest = await _context.LeaveRequests
        //        .Include(l => l.Employee)
        //        .Include(l => l.Hr)
        //        .FirstOrDefaultAsync(m => m.RequestId == id);
        //    if (leaveRequest == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(leaveRequest);
        //}

        // GET: EmpLeaveRequests/Create
        public IActionResult Create()
        {
            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            //ViewData["Hrid"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");

            return View();
        }

        // POST: EmpLeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateOff,DaysLeave,Reason,Status,Comment")] LeaveRequest leaveRequest)
        {
            //hard code get emp id after login
            
            LoginAccount? loginAccount = SessionHelper.GetObjectFromSession<LoginAccount>(HttpContext.Session, KeyConstants.ACCOUNT_KEY);
            string empId = loginAccount.Employee.EmployeeId;

            //string empId = "1111111111";
            //hard code get emp id after login

            leaveRequest.EmployeeId = empId;

            //if (ModelState.IsValid)
            //{
            //    //_context.Add(leaveRequest);
            //    //await _context.SaveChangesAsync();
            //    empRequestLeaveRepository.CreateRequestLeave(leaveRequest);
            //    return RedirectToAction(nameof(Index));
            //}
           

            if (leaveRequest.DateOff.HasValue && leaveRequest.DateOff.Value < DateTime.Now)
            {
                ModelState.AddModelError("DateOff", "The DateOff must be later than the current date and time.");
                return View(leaveRequest);
            }


            //if (!int.TryParse(leaveRequest.DaysLeave.ToString(), out int daysLeaveValue))
            //{
            //    ModelState.AddModelError("DaysLeave", "Invalid value. Please enter a valid integer.");
            //    return View(leaveRequest);
            //}

            try
            {
                leaveRequest.Status = RequestStatus.PENDDING;
                empRequestLeaveRepository.saveLeaveRequest(leaveRequest);
                ViewData["SuccessMessage"] = "Leave request created successfully.";
                return View(leaveRequest);
            }
            catch (Exception ex)
            {
                // Handle the exception and add an error message to ViewData
                ViewData["ErrorMessage"] = "Failed to create leave request: " + ex.Message;
                return View(leaveRequest);
            }
        }

        // GET: EmpLeaveRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LeaveRequests == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", leaveRequest.EmployeeId);
            ViewData["Hrid"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", leaveRequest.Hrid);
            return View(leaveRequest);
        }

        // POST: EmpLeaveRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,EmployeeId,DateOff,DaysLeave,Reason,Hrid,Status,Comment")] LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRequestExists(leaveRequest.RequestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", leaveRequest.EmployeeId);
            ViewData["Hrid"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", leaveRequest.Hrid);
            return View(leaveRequest);
        }

        // GET: EmpLeaveRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LeaveRequests == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests
                .Include(l => l.Employee)
                .Include(l => l.Hr)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // POST: EmpLeaveRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LeaveRequests == null)
            {
                return Problem("Entity set 'HRM_SWD392Context.LeaveRequests'  is null.");
            }
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest != null)
            {
                _context.LeaveRequests.Remove(leaveRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveRequestExists(int id)
        {
          return (_context.LeaveRequests?.Any(e => e.RequestId == id)).GetValueOrDefault();
        }
    }
}

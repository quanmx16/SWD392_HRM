using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.EmpRequestLeaveRepositories;
using DataAccess.EmpResignationRequestsRepositories;
using DataAccess.EmpUpdateAttendanceRequestRepositories;
using HRM_MVC.Common;
using HRM_MVC.SessionManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;

namespace HRM_MVC.Controllers
{
    public class EmpUpdateAttendanceRequestsController : Controller
    {
        protected IEmpUpdateAttendanceRequestRepositories empUpdateAttendanceRequest;
        protected readonly HRM_SWD392Context _context;
        public EmpUpdateAttendanceRequestsController()
        {
            _context = new HRM_SWD392Context();
            empUpdateAttendanceRequest = new EmpUpdateAttendanceRequestRepositories(_context);
        }
        // GET: EmpUpdateAttendanceRequests
        public async Task<IActionResult> Index()
        {
            var hRM_SWD392Context = _context.UpdateAttendanceRequests.Include(u => u.Employee).Include(u => u.Hr);
            return View(await hRM_SWD392Context.ToListAsync());
        }

        // GET: EmpUpdateAttendanceRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UpdateAttendanceRequests == null)
            {
                return NotFound();
            }

            var updateAttendanceRequest = await _context.UpdateAttendanceRequests
                .Include(u => u.Employee)
                .Include(u => u.Hr)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (updateAttendanceRequest == null)
            {
                return NotFound();
            }

            return View(updateAttendanceRequest);
        }

        // GET: EmpUpdateAttendanceRequests/Create
        public IActionResult Create()
        {
            //ViewData["ApproverId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");

            return View();
        }

        // POST: EmpUpdateAttendanceRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,TimeIn,Reason,Status,Comment")] UpdateAttendanceRequest updateAttendanceRequest)
        {
            //hard code get emp id after login
            //string empId = "1111111111";


            LoginAccount? loginAccount = SessionHelper.GetObjectFromSession<LoginAccount>(HttpContext.Session, KeyConstants.ACCOUNT_KEY);
            //string empId = loginAccount.Employee.EmployeeId;
            string empId = "1111111111";
            //hard code get emp id after login

            
            updateAttendanceRequest.EmployeeId = empId;

            //if (resignationRequest.RequestDate < DateTime.Now)
            //{
            //    ModelState.AddModelError(string.Empty, "Request date must be later than the current date.");
            //    return View(resignationRequest);
            //}
            //if (resignationRequest.RequestDate > resignationRequest.LastWorkingDate)
            //{
            //    ModelState.AddModelError(string.Empty, "Request date must be earlier than the last working date.");
            //    return View(resignationRequest);

            //}
            updateAttendanceRequest.Status = RequestStatus.PENDDING;
            updateAttendanceRequest.RequestId = GenerateUpdateRequestId.GenerateUniqueId();
            await Console.Out.WriteLineAsync(updateAttendanceRequest.TimeIn.ToString());
            TimeSpan timeComponent = updateAttendanceRequest.TimeIn?.TimeOfDay ?? TimeSpan.Zero; // Extract the time component from TimeIn or default to TimeSpan.Zero
            DateTime combinedDateTime = updateAttendanceRequest.Date + timeComponent; // Combine the extracted time component with the day component from Date
            updateAttendanceRequest.TimeIn = combinedDateTime;


         



            try
            {
                empUpdateAttendanceRequest.requestUpdateAttendance(updateAttendanceRequest);
                //ViewData["SuccessMessage"] = "resignation request created successfully.";
                ViewData["SuccessMessage"] = "empUpdate Attendance request created successfully.";
                return View(updateAttendanceRequest);
                
            }
            catch (Exception ex)
            {
                // Handle the exception and add an error message to ViewData
                ViewData["ErrorMessage"] = "Failed to create empUpdate Attendance request: " + ex.Message;
                return View(updateAttendanceRequest);
            }
        }

        // GET: EmpUpdateAttendanceRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UpdateAttendanceRequests == null)
            {
                return NotFound();
            }

            var updateAttendanceRequest = await _context.UpdateAttendanceRequests.FindAsync(id);
            if (updateAttendanceRequest == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", updateAttendanceRequest.EmployeeId);
            ViewData["Hrid"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", updateAttendanceRequest.Hrid);
            return View(updateAttendanceRequest);
        }

        // POST: EmpUpdateAttendanceRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,EmployeeId,Date,TimeIn,Reason,Hrid,Status,Comment")] UpdateAttendanceRequest updateAttendanceRequest)
        {
            if (id != updateAttendanceRequest.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updateAttendanceRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UpdateAttendanceRequestExists(updateAttendanceRequest.RequestId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", updateAttendanceRequest.EmployeeId);
            ViewData["Hrid"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", updateAttendanceRequest.Hrid);
            return View(updateAttendanceRequest);
        }

        // GET: EmpUpdateAttendanceRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UpdateAttendanceRequests == null)
            {
                return NotFound();
            }

            var updateAttendanceRequest = await _context.UpdateAttendanceRequests
                .Include(u => u.Employee)
                .Include(u => u.Hr)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (updateAttendanceRequest == null)
            {
                return NotFound();
            }

            return View(updateAttendanceRequest);
        }

        // POST: EmpUpdateAttendanceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UpdateAttendanceRequests == null)
            {
                return Problem("Entity set 'HRM_SWD392Context.UpdateAttendanceRequests'  is null.");
            }
            var updateAttendanceRequest = await _context.UpdateAttendanceRequests.FindAsync(id);
            if (updateAttendanceRequest != null)
            {
                _context.UpdateAttendanceRequests.Remove(updateAttendanceRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UpdateAttendanceRequestExists(int id)
        {
          return (_context.UpdateAttendanceRequests?.Any(e => e.RequestId == id)).GetValueOrDefault();
        }
    }
}

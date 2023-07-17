using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.EmpRequestLeaveRepositories;
using DataAccess.EmpResignationRequestsRepositories;
using HRM_MVC.Common;
using HRM_MVC.SessionManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;

namespace HRM_MVC.Controllers
{
    public class EmpResignationRequestsController : Controller
    {
        protected IEmpResignationRequestsRepository empResignationRequestsRepository;
        protected readonly HRM_SWD392Context _context;
        public EmpResignationRequestsController()
        {
            _context = new HRM_SWD392Context();
            empResignationRequestsRepository = new EmpResignationRequestsRepository(_context);
        }

        // GET: EmpResignationRequests
        //public async Task<IActionResult> Index()
        //{
        //    var hRM_SWD392Context = _context.ResignationRequests.Include(r => r.Approver).Include(r => r.Employee);
        //    return View(await hRM_SWD392Context.ToListAsync());
        //}

        //// GET: EmpResignationRequests/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.ResignationRequests == null)
        //    {
        //        return NotFound();
        //    }

        //    var resignationRequest = await _context.ResignationRequests
        //        .Include(r => r.Approver)
        //        .Include(r => r.Employee)
        //        .FirstOrDefaultAsync(m => m.RequestId == id);
        //    if (resignationRequest == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(resignationRequest);
        //}

        // GET: EmpResignationRequests/Create
        public IActionResult Create()
        {
            //ViewData["ApproverId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");

            return View();
        }

        // POST: EmpResignationRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestDate,LastWorkingDate,Reason,Comment")] ResignationRequest resignationRequest)
        {
            //hard code get emp id after login
            //string empId = "1111111111";


            LoginAccount? loginAccount = SessionHelper.GetObjectFromSession<LoginAccount>(HttpContext.Session, KeyConstants.ACCOUNT_KEY);
            string empId = loginAccount.Employee.EmployeeId;
            //string empId = "1111111111";
            //hard code get emp id after login

            resignationRequest.EmployeeId = empId;

            if (resignationRequest.RequestDate < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Request date must be later than the current date.");
                return View(resignationRequest);
            }
            if (resignationRequest.RequestDate > resignationRequest.LastWorkingDate)
            {
                ModelState.AddModelError(string.Empty, "Request date must be earlier than the last working date.");
                return View(resignationRequest);

            }
          




            try
            {
                resignationRequest.RequestStatus = RequestStatus.PENDDING;
                empResignationRequestsRepository.saveResgination(resignationRequest);
                ViewData["SuccessMessage"] = "resignation request created successfully.";
                return View(resignationRequest);
            }
            catch (Exception ex)
            {
                // Handle the exception and add an error message to ViewData
                ViewData["ErrorMessage"] = "Failed to create resignation request: " + ex.Message;
                return View(resignationRequest);
            }

            //return View(resignationRequest);
        }

        // GET: EmpResignationRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ResignationRequests == null)
            {
                return NotFound();
            }

            var resignationRequest = await _context.ResignationRequests.FindAsync(id);
            if (resignationRequest == null)
            {
                return NotFound();
            }
            ViewData["ApproverId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", resignationRequest.ApproverId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", resignationRequest.EmployeeId);
            return View(resignationRequest);
        }

        // POST: EmpResignationRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,EmployeeId,RequestDate,LastWorkingDate,Reason,RequestStatus,ApproveDate,ApproverId,Comment")] ResignationRequest resignationRequest)
        {
            if (id != resignationRequest.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resignationRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResignationRequestExists(resignationRequest.RequestId))
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
            ViewData["ApproverId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", resignationRequest.ApproverId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", resignationRequest.EmployeeId);
            return View(resignationRequest);
        }

        // GET: EmpResignationRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ResignationRequests == null)
            {
                return NotFound();
            }

            var resignationRequest = await _context.ResignationRequests
                .Include(r => r.Approver)
                .Include(r => r.Employee)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (resignationRequest == null)
            {
                return NotFound();
            }

            return View(resignationRequest);
        }

        // POST: EmpResignationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ResignationRequests == null)
            {
                return Problem("Entity set 'HRM_SWD392Context.ResignationRequests'  is null.");
            }
            var resignationRequest = await _context.ResignationRequests.FindAsync(id);
            if (resignationRequest != null)
            {
                _context.ResignationRequests.Remove(resignationRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResignationRequestExists(int id)
        {
          return (_context.ResignationRequests?.Any(e => e.RequestId == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.LeaveRequestRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;

namespace HRM_MVC.Controllers
{
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveRequestRepositories _leaveRequestRepo;

        public LeaveRequestsController()
        {
            _leaveRequestRepo = new LeaveRequestRepositories();
        }

        #region Get View & Detail
        // GET: LeaveRequests
        public async Task<IActionResult> Index()
        {
            var leaveRequests = await _leaveRequestRepo.GetAllLeaveRequest();
            return View(leaveRequests);
        }

        // GET: LeaveRequests/Approve/5
        public async Task<IActionResult> Approve(int? id)
        {
            var result = await _leaveRequestRepo.Approve(id!.Value);
            if (result)
            {
                TempData["Success"] = "Approve Successfully";
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
           
        }

        // GET: LeaveRequests/Deny/5
        public async Task<IActionResult> Deny(int? id)
        {
            var result = await _leaveRequestRepo.Deny(id!.Value);
            if (result)
            {
                TempData["Success"] = "Deny Successfully";
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        #endregion

        // GET: LeaveRequests/Create
        public async Task<IActionResult> Create()
        {
            ViewData["EmployeeId"] = new SelectList( await _leaveRequestRepo.GetAllEmployees(), "EmployeeId", "EmployeeId");
            ViewData["Hrid"] = new SelectList(await _leaveRequestRepo.GetAllHR() , "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: LeaveRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,DateOff,DaysLeave,Reason,Hrid,Comment")] LeaveRequest leaveRequest)
        {
            leaveRequest.Status = "Pending";
            var result= await _leaveRequestRepo.CreateLeaveRequest(leaveRequest);   
            if (result)
            {
                TempData["Success"] = "Create Successfully";
                return RedirectToAction(nameof(Create));
            }
            return RedirectToAction(nameof(Create));
        }

        #region Edit
        //// GET: LeaveRequests/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.LeaveRequests == null)
        //    {
        //        return NotFound();
        //    }

        //    var leaveRequest = await _context.LeaveRequests.FindAsync(id);
        //    if (leaveRequest == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", leaveRequest.EmployeeId);
        //    ViewData["Hrid"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", leaveRequest.Hrid);
        //    return View(leaveRequest);
        //}

        //// POST: LeaveRequests/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("RequestId,EmployeeId,DateOff,DaysLeave,Reason,Hrid,Status,Comment")] LeaveRequest leaveRequest)
        //{
        //    if (id != leaveRequest.RequestId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(leaveRequest);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LeaveRequestExists(leaveRequest.RequestId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", leaveRequest.EmployeeId);
        //    ViewData["Hrid"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", leaveRequest.Hrid);
        //    return View(leaveRequest);
        //}
        #endregion

        #region Delete
        // GET: LeaveRequests/Delete/5
        //public async Task<IActionResult> Delete(int? id)
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

        //// POST: LeaveRequests/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.LeaveRequests == null)
        //    {
        //        return Problem("Entity set 'HRM_SWD392Context.LeaveRequests'  is null.");
        //    }
        //    var leaveRequest = await _context.LeaveRequests.FindAsync(id);
        //    if (leaveRequest != null)
        //    {
        //        _context.LeaveRequests.Remove(leaveRequest);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool LeaveRequestExists(int id)
        //{
        //    return (_context.LeaveRequests?.Any(e => e.RequestId == id)).GetValueOrDefault();
        //}
        #endregion
    }
}

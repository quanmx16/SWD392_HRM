using DataAccess.EmpRequestLeaveRepositories;
using HRM_MVC.Common;
using HRM_MVC.SessionManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            empRequestLeaveRepository = new EmpRequestLeaveRepository();
        }

        //GET: EmpLeaveRequests
        public async Task<IActionResult> Index()
        {
            var user = AuthorAuthen();
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var lst = empRequestLeaveRepository.GetByUserId(user.EmployeeId.Trim());
                return View(lst);
            }
        }

        //// GET: EmpLeaveRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LeaveRequests == null)
            {
                return NotFound();
            }
            else
            {
                var user = AuthorAuthen();
                if (user == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    var leaveRequest = empRequestLeaveRepository.GetById(id.Value);
                    if (leaveRequest.EmployeeId.Trim() == user.EmployeeId.Trim())
                    {
                        if (leaveRequest == null)
                        {
                            return NotFound();
                        }
                        return View(leaveRequest);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        // GET: EmpLeaveRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpLeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateOff,DaysLeave,Reason,Status,Comment")] LeaveRequest leaveRequest)
        {
            var user = AuthorAuthen();
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                leaveRequest.EmployeeId = user.EmployeeId.Trim();
                if (!leaveRequest.DateOff.HasValue)
                {
                    ModelState.AddModelError("DateOff", "Please Choose Date off");
                    return View(leaveRequest);
                }

                if (leaveRequest.DateOff.HasValue && leaveRequest.DateOff.Value < DateTime.Now)
                {
                    ModelState.AddModelError("DateOff", "The DateOff must be later than the current date and time.");
                    return View(leaveRequest);
                }
                try
                {
                    leaveRequest.DaysLeave = int.Parse(leaveRequest.DaysLeave.ToString());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("DaysLeave", "Invalid value. Please enter a valid integer.");
                    return View(leaveRequest);
                }

                try
                {
                    leaveRequest.Status = RequestStatus.PENDDING;
                    empRequestLeaveRepository.saveLeaveRequest(leaveRequest);
                    ViewData["SuccessMessage"] = "Leave request created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Handle the exception and add an error message to ViewData
                    ViewData["ErrorMessage"] = "Failed to create leave request: " + ex.Message;
                    return View(leaveRequest);
                }
            }
        }

        // GET: EmpLeaveRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = AuthorAuthen();
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }
                var leaveRequest = empRequestLeaveRepository.GetById(id);
                if (leaveRequest == null)
                {
                    return NotFound();
                }
                else
                {
                    if (leaveRequest.EmployeeId.Trim() == user.EmployeeId.Trim() && leaveRequest.Status == RequestStatus.PENDDING)
                    {
                        return View(leaveRequest);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        // POST: EmpLeaveRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,EmployeeId,DateOff,DaysLeave,Reason,Hrid,Status,Comment")] LeaveRequest leaveRequest)
        {
            var user = AuthorAuthen();
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (leaveRequest.EmployeeId.Trim() == user.EmployeeId.Trim() && leaveRequest.Status == RequestStatus.PENDDING)
                {
                    leaveRequest.EmployeeId = user.EmployeeId.Trim();
                    if (!leaveRequest.DateOff.HasValue)
                    {
                        ModelState.AddModelError("DateOff", "Please Choose Date off");
                        return View(leaveRequest);
                    }

                    if (leaveRequest.DateOff.HasValue && leaveRequest.DateOff.Value < DateTime.Now)
                    {
                        ModelState.AddModelError("DateOff", "The DateOff must be later than the current date and time.");
                        return View(leaveRequest);
                    }
                    try
                    {
                        leaveRequest.DaysLeave = int.Parse(leaveRequest.DaysLeave.ToString());
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("DaysLeave", "Invalid value. Please enter a valid integer.");
                        return View(leaveRequest);
                    }

                    try
                    {
                        leaveRequest.Status = RequestStatus.PENDDING;
                        empRequestLeaveRepository.Update(leaveRequest);
                        ViewData["SuccessMessage"] = "Leave request edit successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception and add an error message to ViewData
                        ViewData["ErrorMessage"] = "Failed to edit leave request: " + ex.Message;
                        return View(leaveRequest);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // GET: EmpLeaveRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = AuthorAuthen();
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var leaveRequest = empRequestLeaveRepository.GetById(id.Value);
                if (leaveRequest == null)
                {
                    return NotFound();
                }
                else
                {
                    if (leaveRequest.EmployeeId.Trim() == user.EmployeeId.Trim())
                    {
                        return View(leaveRequest);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        // POST: EmpLeaveRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var user = AuthorAuthen();
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var leaveRequest = empRequestLeaveRepository.GetById(id);
                if (leaveRequest != null)
                {
                    if (leaveRequest.EmployeeId.Trim() == user.EmployeeId.Trim())
                    {
                        empRequestLeaveRepository.Delete(leaveRequest);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
        }

        private bool LeaveRequestExists(int id)
        {
            return (_context.LeaveRequests?.Any(e => e.RequestId == id)).GetValueOrDefault();
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

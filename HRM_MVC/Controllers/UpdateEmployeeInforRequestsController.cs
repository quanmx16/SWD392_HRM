using DataAccess.UpdateEmployeeInforRequestsRepositories;
using HRM_MVC.Common;
using HRM_MVC.SessionManager;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace HRM_MVC.Controllers
{
    public class UpdateEmployeeInforRequestsController : Controller
    {
        private readonly IUpdateInforRequestsRepo _requestsRepo;

        public UpdateEmployeeInforRequestsController()
        {
            _requestsRepo = new UpdateInforRequestsRepo();
        }

        // GET: UpdateEmployeeInforRequests
        public async Task<IActionResult> Index()
        {
            var user = AuthorAuthen();
            if (user == null || !user.Role.Trim().Equals(Roles.ROLE_HR_MANAGER))
            {
                return RedirectToAction("Error");
            }
            else
            {
                var list = await _requestsRepo.GetAllRequests();
                return View(list);
            }
        }

        // GET: UpdateEmployeeInforRequests/Approve/5
        public async Task<IActionResult> Approve(int? id)
        {
            var user = AuthorAuthen();
            if (user == null || !user.Role.Trim().Equals(Roles.ROLE_HR_MANAGER))
            {
                return RedirectToAction("Error");
            }
            else
            {
                var result = await _requestsRepo.Approve(id!.Value);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

        }

        // GET: UpdateEmployeeInforRequests/Deny/5
        public async Task<IActionResult> Deny(int? id)
        {
            var user = AuthorAuthen();
            if (user == null || !user.Role.Trim().Equals(Roles.ROLE_HR_MANAGER))
            {
                return RedirectToAction("Error");
            }
            else
            {
                var result = await _requestsRepo.Deny(id!.Value);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
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

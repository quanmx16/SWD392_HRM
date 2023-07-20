using DataAccess.EmployeeRepositories;
using DataAccess.RequestRepository;
using HRM_MVC.Common;
using HRM_MVC.Models;
using HRM_MVC.SessionManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Data;
using Model.Models;

namespace HRM_MVC.Controllers
{
    public class HRManagerController : Controller
    {
        protected IRequestRepository _requestRepository;
        protected readonly HRM_SWD392Context _context;
        protected readonly IEmployeeRepository _employeeRepository;
        public HRManagerController()
        {
            _context = new HRM_SWD392Context();
            _employeeRepository = new EmployeeRepository(_context);
            _requestRepository = new RequestRepository(_context, _employeeRepository);

        }
        public IActionResult HRRequests()
        {

            var user = AuthorAuthen();
            if (user == null || !user.Role.Equals(Roles.ROLE_HR_MANAGER))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                List<RequestDTO> list = _requestRepository.GetAllHRRequest();
                if (list.Count != null)
                {
                    HRManagerModel model = new HRManagerModel();
                    model.listRequests = list; return View(model);
                }
                return RedirectToAction("Error");
            }
        }


        public IActionResult ApproveRequest(int id, string type, HRManagerModel model)
        {
            var user = AuthorAuthen();
            if (user == null || !user.Role.Equals(Roles.ROLE_HR_MANAGER))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                _requestRepository.ApproveRequest(id, type, user.EmployeeId);
                return RedirectToAction("HRRequests", model);
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

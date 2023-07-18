using DataAccess.EmployeeRepositories;
using DataAccess.RequestRepository;
using HRM_MVC.Models;
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
            List<RequestDTO> list = _requestRepository.GetAllHRRequest();
            if (list.Count != null)
            {
                HRManagerModel model = new HRManagerModel();
                model.listRequests = list; return View(model);
            }
            return RedirectToAction("Error");
        }


        public IActionResult ApproveRequest (int id, string type,HRManagerModel model)
        {
            _requestRepository.ApproveRequest(id, type);
            return RedirectToAction("HRRequests", model);
        }
    }
}

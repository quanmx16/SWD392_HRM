using DataAccess.ResignationRequestRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;

namespace HRM_MVC.Controllers
{
    public class ResignationRequestController : Controller
    {
        protected IResignationRequestRepository resignationRequestRepository;

        public ResignationRequestController()
        {
            resignationRequestRepository = new ResignationRequestRepository();
        }

        [HttpGet]
        [Route("/ResignationRequest/ResignationRequests")]
        public IActionResult ResignationRequests()
        {
            List<ResignationRequest> listRequest = resignationRequestRepository.GetAllPendingResignationRequests();

            return View(listRequest);
        }

        [HttpGet]
        public IActionResult Approve(int id)
        {
            var request = resignationRequestRepository.GetResignationRequestById(id);

            if(request != null)
            {
                request.RequestStatus = "Approve";
                request.ApproveDate = DateTime.Now;
                resignationRequestRepository.UpdateResignationRequest(request);
            }

            return RedirectToAction("ResignationRequests");
        }
        [HttpGet]
        public IActionResult Deny(int id)
        {
            var request = resignationRequestRepository.GetResignationRequestById(id);

            if (request != null)
            {
                request.RequestStatus = "Deny";
                resignationRequestRepository.UpdateResignationRequest(request);
            }

            return RedirectToAction("ResignationRequests");
        }

        /*[HttpPost]
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
            ViewData["ManagerId"] = new SelectList(_context.Employees, "EmployeeId", "EmplyeeName", employee.ManagerId);
            return View(employee);
        }*/
    }
}

using Microsoft.AspNetCore.Mvc;
using DataAccess.RequestRepository;
using Model.Models;
using Model.Data;
using DataAccess.EmployeeRepositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRM_MVC.Controllers
{
    public class RequestController : Controller
    {
        protected IRequestRepository _requestRepository;
        protected readonly HRM_SWD392Context _context;
        protected readonly IEmployeeRepository _employeeRepository;
        public RequestController()
        { _context=new HRM_SWD392Context();
            _employeeRepository=new EmployeeRepository(_context);
            _requestRepository = new RequestRepository(_context,_employeeRepository);
           
        }
        public async Task<IActionResult> HRLeaveRequest() 
        {
           List<RequestDTO> list= _requestRepository.GetAllHRRequest(); 
            List<LeaveRequest> LeaveRequests = new List<LeaveRequest>();
            if (list.Count!=null)
            {  
                foreach (RequestDTO request in list)
                {
                    if (request.Typename.Equals("LeaveRequest"))
                    {
                        LeaveRequests.Add(request.LeaveRequest); break;
                    }          
                    }
                    ViewData["LeaveRequest"] = new SelectList(LeaveRequests, "RequestID");
                }
            return View("/HRManager/HRLeaveRequest");
        }
        public IActionResult ResignationRequest()
        {
            List<RequestDTO> list = _requestRepository.GetAllHRRequest();
            List<ResignationRequest> ResignationRequests = new List<ResignationRequest>();
            if (list.Count > 0)
            {

                foreach (RequestDTO request in list)
                {
                    if (request.Typename.Equals("ResignationRequest"))
                    {
                        ResignationRequests.Add(request.ResignationRequest); break;
                    }

                }

                ViewData["ResignationRequest"] = new SelectList(ResignationRequests, "RequestID");

            }
            return View(ResignationRequests);
        }
        public IActionResult UpdateEmployeeInforRequest()
        {
            List<RequestDTO> list = _requestRepository.GetAllHRRequest();
            List<UpdateEmployeeInforRequest> UpdateEmployeeInforRequests = new List<UpdateEmployeeInforRequest>();
            if (list.Count > 0)
            {

                foreach (RequestDTO request in list)
                {
                    if (request.Typename.Equals("UpdateEmployeeInforRequest"))
                    {
                        UpdateEmployeeInforRequests.Add(request.UpdateEmployeeInforRequest); break;
                    }

                }

                ViewData["UpdateEmployeeInforRequest"] = new SelectList(UpdateEmployeeInforRequests, "RequestID");

            }
            return View(UpdateEmployeeInforRequests);
        }

        /*  public async Task<IActionResult> ResponseRequest (string type, RequestDTO requestDTO)
          {
              if(type==null || id == null)
              {
                  return NotFound();
              }
              var requests = "";
              switch(type)
              {
                  case "ChangeWorkDepartmentRequest":
                      {
                          return View(requestDTO.ChangeWorkDepartmentRequest);
                      }

              }
          }*/
    }
}

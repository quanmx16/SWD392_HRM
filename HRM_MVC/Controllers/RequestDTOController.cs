using Microsoft.AspNetCore.Mvc;
using DataAccess.RequestRepository;
using Model.Models;
using Model.Data;
using DataAccess.EmployeeRepositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRM_MVC.Controllers
{
    public class RequestDTOController : Controller
    {
        protected IRequestRepository _requestRepository;
        protected readonly HRM_SWD392Context _context;
        protected readonly IEmployeeRepository _employeeRepository;
        public RequestDTOController()
        { _context=new HRM_SWD392Context();
            _employeeRepository=new EmployeeRepository(_context);
            _requestRepository = new RequestRepository(_context,_employeeRepository);
           
        }
        public IActionResult ViewAllHRRequest() 
        {
           List<RequestDTO> list= _requestRepository.GetAllHRRequest();
            if (list.Count > 0)
            {
                List<ChangeWorkDepartmentRequest> changeWorkDepartmentRequests= new List<ChangeWorkDepartmentRequest>();
                List<LeaveRequest> LeaveRequests = new List<LeaveRequest>();
                List<Otrequest> Otrequests = new List<Otrequest>();
                List<ResignationRequest> resignationRequests = new List<ResignationRequest>();
                List<TaxRequest> TaxRequest = new List<TaxRequest>();
                List<UpdateEmployeeInforRequest> UpdateEmployeeInforRequests = new List<UpdateEmployeeInforRequest>();
                foreach (RequestDTO request in list)
                {
                    String type = request.Typename;
                    switch (type)
                    {
                        case "ChangeWorkDepartmentRequest":
                            {
                            changeWorkDepartmentRequests.Add(request.ChangeWorkDepartmentRequest); break;
                            }
                        case "LeaveRequest":
                            {
                                LeaveRequests.Add(request.LeaveRequest); break;

                            }
                        case "Otrequest":
                            {
                                Otrequests.Add(request.Otrequest); break;

                            }
                        case "ResignationRequest":
                            {
                                resignationRequests.Add(request.ResignationRequest); break;

                            }
                        case "TaxRequest":
                            {
                                TaxRequest.Add(request.TaxRequest); break;

                            }
                        case "UpdateEmployeeInforRequest":
                            {
                                UpdateEmployeeInforRequests.Add(request.UpdateEmployeeInforRequest); break;
                            }
                    }
                    ViewData["ChangeWorkDepartmentRequest"] = new SelectList(changeWorkDepartmentRequests, "RequestID");

                    ViewData["LeaveRequest"] = new SelectList(LeaveRequests, "RequestID");
                    ViewData["Otrequest"] = new SelectList(Otrequests, "RequestID");
                    ViewData["ResignationRequest"] = new SelectList(resignationRequests, "RequestID");
                    ViewData["TaxRequest"] = new SelectList(TaxRequest, "RequestID");
                    ViewData["UpdateEmployeeInforRequest"] = new SelectList(UpdateEmployeeInforRequests, "RequestID");

                }
            }
            return View();
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

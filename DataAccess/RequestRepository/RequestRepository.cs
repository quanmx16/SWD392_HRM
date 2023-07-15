using DataAccess.EmployeeRepositories;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RequestRepository
{
    public interface IRequestRepository
    {
        List<RequestDTO> GetAllRequest();
        List<RequestDTO> GetAllHRRequest();
        bool ResponseRequest(RequestDTO requestDTO);
    }

    public class RequestRepository : IRequestRepository
    { protected readonly HRM_SWD392Context _context;
        public readonly IEmployeeRepository _employeeRepository;
        public RequestRepository(HRM_SWD392Context context, IEmployeeRepository employeeRepository)
        {
            if (_context == null || _employeeRepository==null)
            {
                this._context = context;
                this._employeeRepository= employeeRepository;
            }
        }
        public RequestRepository()
        {
            if (_context == null)
            {
                this._context =new HRM_SWD392Context();
                this._employeeRepository= new EmployeeRepository();
            }
        }
        public List<RequestDTO> GetAllRequest()
        {
           if(this._context != null)
            {
                List<RequestDTO> listRequests= new List<RequestDTO>();
                List<ChangeWorkDepartmentRequest> changeWorkDepartmentRequests = new List<ChangeWorkDepartmentRequest>();
                changeWorkDepartmentRequests=_context.ChangeWorkDepartmentRequests.ToList();
                List<LeaveRequest> LeaveRequest = new List<LeaveRequest>();
                LeaveRequest = _context.LeaveRequests.ToList();
                List<Otrequest> otrequests = new List<Otrequest>();
                otrequests = _context.Otrequests.ToList();
                List<ResignationRequest> ResignationRequest = new List<ResignationRequest>();
                ResignationRequest = _context.ResignationRequests.ToList();
                List<TaxRequest> TaxRequest = new List<TaxRequest>();
                TaxRequest = _context.TaxRequests.ToList();
                List<UpdateEmployeeInforRequest> UpdateEmployeeInforRequest = new List<UpdateEmployeeInforRequest>();
                UpdateEmployeeInforRequest = _context.UpdateEmployeeInforRequests.ToList();
                if (changeWorkDepartmentRequests.Count > 0)
                {
                    foreach (ChangeWorkDepartmentRequest request in changeWorkDepartmentRequests)
                    {
                            listRequests.Add(new RequestDTO(request.RequestId, "ChangeWorkDepartmentRequest", request, null, null, null, null, null));
                    }
                }
                if (LeaveRequest.Count > 0)
                {
                    foreach (LeaveRequest request in LeaveRequest)
                    {

                            listRequests.Add(new RequestDTO(request.RequestId, "LeaveRequest", null, request, null, null, null, null));
                    }
                }
                if (otrequests.Count > 0)
                {
                    foreach (Otrequest request in otrequests)
                    {

                            listRequests.Add(new RequestDTO(request.RequestId, "Otrequest", null, null, request, null, null, null));
                    }
                }
                if (ResignationRequest.Count > 0)
                {
                    foreach (ResignationRequest request in ResignationRequest)
                    {

                            listRequests.Add(new RequestDTO(request.RequestId, "ResignationRequest", null, null, null, request, null, null));
                    }
                }
                if (TaxRequest.Count > 0)
                {
                    foreach (TaxRequest request in TaxRequest)
                    {

                            listRequests.Add(new RequestDTO(request.RequestId, "TaxRequest", null, null, null, null, request, null));
                    }
                }
                if (UpdateEmployeeInforRequest.Count > 0)
                {
                    foreach (UpdateEmployeeInforRequest request in UpdateEmployeeInforRequest)
                    {

                            listRequests.Add(new RequestDTO(request.RequestId, "UpdateEmployeeInforRequest", null, null, null, null, null, request));
                    }
                }
                if (listRequests.Count > 0) return listRequests;
            }
            return null;
        }

        public List<RequestDTO> GetAllHRRequest()
        {
            if (this._context != null)
            {   List<Employee> listHR= new List<Employee>();
                List<RequestDTO> listRequests = new List<RequestDTO>();
                List<ChangeWorkDepartmentRequest> changeWorkDepartmentRequests = new List<ChangeWorkDepartmentRequest>();
                changeWorkDepartmentRequests = _context.ChangeWorkDepartmentRequests.ToList();
                List<LeaveRequest> LeaveRequest = new List<LeaveRequest>();
                LeaveRequest = _context.LeaveRequests.ToList();
                List<Otrequest> otrequests = new List<Otrequest>();
                otrequests = _context.Otrequests.ToList();
                List<ResignationRequest> ResignationRequest = new List<ResignationRequest>();
                ResignationRequest = _context.ResignationRequests.ToList();
                List<TaxRequest> TaxRequest = new List<TaxRequest>();
                TaxRequest = _context.TaxRequests.ToList();
                List<UpdateEmployeeInforRequest> UpdateEmployeeInforRequest = new List<UpdateEmployeeInforRequest>();
                UpdateEmployeeInforRequest = _context.UpdateEmployeeInforRequests.ToList();
                if (changeWorkDepartmentRequests.Count > 0)
                {
                    foreach (ChangeWorkDepartmentRequest request in changeWorkDepartmentRequests)
                    {
                        if(listHR.Contains(_employeeRepository.GetEmployeeById(request.EmployeeId)))
                        listRequests.Add(new RequestDTO(request.RequestId, "ChangeWorkDepartmentRequest",request,null,null,null,null,null));
                    }
                }
                if (LeaveRequest.Count > 0)
                {
                    foreach (LeaveRequest request in LeaveRequest)
                    {
                        if (listHR.Contains(_employeeRepository.GetEmployeeById(request.EmployeeId)))

                            listRequests.Add(new RequestDTO(request.RequestId, "LeaveRequest",null,request,null,null,null,null));
                    }
                }
                if (otrequests.Count > 0)
                {
                    foreach (Otrequest request in otrequests)
                    {
                        if (listHR.Contains(_employeeRepository.GetEmployeeById(request.EmployeeId)))

                            listRequests.Add(new RequestDTO(request.RequestId, "Otrequest",null,null,request,null,null,null));
                    }
                }
                if (ResignationRequest.Count > 0)
                {
                    foreach (ResignationRequest request in ResignationRequest)
                    {
                        if (listHR.Contains(_employeeRepository.GetEmployeeById(request.EmployeeId)))

                            listRequests.Add(new RequestDTO(request.RequestId, "ResignationRequest",null,null,null,request,null,null));
                    }
                }
                if (TaxRequest.Count > 0)
                {
                    foreach (TaxRequest request in TaxRequest)
                    {
                        if (listHR.Contains(_employeeRepository.GetEmployeeById(request.EmployeeId)))

                            listRequests.Add(new RequestDTO(request.RequestId, "TaxRequest",null,null,null,null,request,null));
                    }
                }
                if (UpdateEmployeeInforRequest.Count > 0)
                {
                    foreach (UpdateEmployeeInforRequest request in UpdateEmployeeInforRequest)
                    {
                        if (listHR.Contains(_employeeRepository.GetEmployeeById(request.EmployeeId)))

                            listRequests.Add(new RequestDTO(request.RequestId, "UpdateEmployeeInforRequest",null,null,null,null,null,request));
                    }
                }
                if (listRequests.Count > 0) return listRequests;
            }
            return null;
        }

        public bool ResponseRequest(RequestDTO requestDTO)
        {
           if(requestDTO != null)
            {
  switch ( requestDTO.Typename)
            {
                case "ChangeWorkDepartmentRequest":
                    {
                        var request= _context.ChangeWorkDepartmentRequests.Find(requestDTO.Id);
                        if (request != null)
                        {
                            _context.Entry(request).State = EntityState.Detached;
                            _context.Entry(requestDTO.ChangeWorkDepartmentRequest).State = EntityState.Modified;
                            _context.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return false;
                            throw new Exception("Request not exist");
                        }
                    }
                    case "LeaveRequest":
                        {
                            var request = _context.LeaveRequests.Find(requestDTO.Id);
                            if (request != null)
                            {
                                _context.Entry(request).State = EntityState.Detached;
                                _context.Entry(requestDTO.LeaveRequest).State = EntityState.Modified;
                                _context.SaveChanges();
                                return true;
                            }
                            else
                            {
                                return false;
                                throw new Exception("Request not exist");
                            }
                        }
                    case "Otrequest":
                        {
                            var request = _context.Otrequests.Find(requestDTO.Id);
                            if (request != null)
                            {
                                _context.Entry(request).State = EntityState.Detached;
                                _context.Entry(requestDTO.Otrequest).State = EntityState.Modified;
                                _context.SaveChanges();
                                return true;
                            }
                            else
                            {
                                return false;
                                throw new Exception("Request not exist");
                            }
                        }
                    case "ResignationRequest":
                        {
                            var request = _context.ResignationRequests.Find(requestDTO.Id);
                            if (request != null)
                            {
                                _context.Entry(request).State = EntityState.Detached;
                                _context.Entry(requestDTO.ResignationRequest).State = EntityState.Modified;
                                _context.SaveChanges();
                                return true;
                            }
                            else
                            {
                                return false;
                                throw new Exception("Request not exist");
                            }
                        }
                    case "TaxRequest":
                        {
                            var request = _context.TaxRequests.Find(requestDTO.Id);
                            if (request != null)
                            {
                                _context.Entry(request).State = EntityState.Detached;
                                _context.Entry(requestDTO.TaxRequest).State = EntityState.Modified;
                                _context.SaveChanges();
                                return true;
                            }
                            else
                            {
                                return false;
                                throw new Exception("Request not exist");
                            }
                        }
                    case "UpdateEmployeeInforRequest":
                        {
                            var request = _context.UpdateEmployeeInforRequests.Find(requestDTO.Id);
                            if (request != null)
                            {
                                _context.Entry(request).State = EntityState.Detached;
                                _context.Entry(requestDTO.UpdateEmployeeInforRequest).State = EntityState.Modified;
                                _context.SaveChanges();
                                return true;
                            }
                            else
                            {
                                return false;
                                throw new Exception("Request not exist");
                            }
                        }
                }
            }  return false;
        }

    }
}

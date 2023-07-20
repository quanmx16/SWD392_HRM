using DataAccess.EmployeeRepositories;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;

namespace DataAccess.RequestRepository
{
    public interface IRequestRepository
    {
        /*     List<RequestDTO> GetAllRequest();*/
        List<RequestDTO> GetAllHRRequest();
        bool ResponseRequest(RequestDTO requestDTO);
        bool ApproveRequest(int id, string type, string approverId);
    }

    public class RequestRepository : IRequestRepository
    {
        protected readonly HRM_SWD392Context _context;
        public readonly IEmployeeRepository _employeeRepository;
        public RequestRepository(HRM_SWD392Context context, IEmployeeRepository employeeRepository)
        {
            if (_context == null || _employeeRepository == null)
            {
                this._context = context;
                this._employeeRepository = employeeRepository;
            }
        }
        public RequestRepository()
        {
            if (_context == null)
            {
                this._context = new HRM_SWD392Context();
                this._employeeRepository = new EmployeeRepository();
            }
        }
        public List<RequestDTO> GetAllHRRequest()

        {
            if (this._context != null)
            {
                List<Employee> listHR = new List<Employee>();
                listHR = _employeeRepository.GetHR();
                List<RequestDTO> listRequests = new List<RequestDTO>();
                List<ChangeWorkDepartmentRequest> changeWorkDepartmentRequests = new List<ChangeWorkDepartmentRequest>();
                changeWorkDepartmentRequests = _context.ChangeWorkDepartmentRequests.Include(c => c.Hr).Include(c => c.DepartmentMoveTo).Include(c => c.CurrentDepartment).Include(c => c.Employee).ToList();
                List<LeaveRequest> LeaveRequest = new List<LeaveRequest>();
                LeaveRequest = _context.LeaveRequests.Include(c => c.Hr).Include(c => c.Employee).ToList();
                List<Otrequest> otrequests = new List<Otrequest>();
                otrequests = _context.Otrequests.Include(t => t.Employee).Include(c => c.Approver).ToList();
                List<ResignationRequest> ResignationRequest = new List<ResignationRequest>();
                ResignationRequest = _context.ResignationRequests.Include(t => t.Employee).Include(c => c.Approver).ToList();
                List<TaxRequest> TaxRequest = new List<TaxRequest>();
                TaxRequest = _context.TaxRequests.Include(t => t.Employee).Include(c => c.Approver).ToList();
                List<UpdateEmployeeInforRequest> UpdateEmployeeInforRequest = new List<UpdateEmployeeInforRequest>();
                UpdateEmployeeInforRequest = _context.UpdateEmployeeInforRequests.Include(t => t.Employee).Include(c => c.Approver).Include(c => c.Department).Include(c => c.Manager).ToList();
                if (changeWorkDepartmentRequests.Count > 0)
                {
                    foreach (ChangeWorkDepartmentRequest request in changeWorkDepartmentRequests)
                    {
                        if (listHR.Contains(request.Employee))
                            listRequests.Add(new RequestDTO(request.RequestId, "ChangeWorkDepartmentRequest", request, null, null, null, null, null));
                    }
                }
                if (LeaveRequest.Count > 0)
                {
                    foreach (LeaveRequest request in LeaveRequest)
                    {
                        if (listHR.Contains(request.Employee))

                            listRequests.Add(new RequestDTO(request.RequestId, "LeaveRequest", null, request, null, null, null, null));
                    }
                }
                if (otrequests.Count > 0)
                {
                    foreach (Otrequest request in otrequests)
                    {
                        if (listHR.Contains(request.Employee))

                            listRequests.Add(new RequestDTO(request.RequestId, "Otrequest", null, null, request, null, null, null));
                    }
                }
                if (ResignationRequest.Count > 0)
                {
                    foreach (ResignationRequest request in ResignationRequest)
                    {
                        if (listHR.Contains(request.Employee))

                            listRequests.Add(new RequestDTO(request.RequestId, "ResignationRequest", null, null, null, request, null, null));
                    }
                }
                if (TaxRequest.Count > 0)
                {
                    foreach (TaxRequest request in TaxRequest)
                    {
                        if (listHR.Contains(request.Employee))

                            listRequests.Add(new RequestDTO(request.RequestId, "TaxRequest", null, null, null, null, request, null));
                    }
                }
                if (UpdateEmployeeInforRequest.Count > 0)
                {
                    foreach (UpdateEmployeeInforRequest request in UpdateEmployeeInforRequest)
                    {
                        if (listHR.Contains(request.Employee))

                            listRequests.Add(new RequestDTO(request.RequestId, "UpdateEmployeeInforRequest", null, null, null, null, null, request));
                    }
                }
                if (listRequests.Count > 0) return listRequests;
            }
            Console.WriteLine("no");
            return new List<RequestDTO>();
        }

        public bool ApproveRequest(int id, string type, string approverId)
        {
            if (id != null && type != null)
            {
                switch (type)
                {
                    case "ChangeWorkDepartmentRequest":
                        {
                            var request = _context.ChangeWorkDepartmentRequests.Find(id);
                            if (request != null)
                            {
                                _context.Entry(request).State = EntityState.Detached;
                                request.ResponseRequest = true;
                                _context.Entry(request).State = EntityState.Modified;
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
                            var request = _context.LeaveRequests.Find(id);
                            if (request != null)
                            {
                                _context.Entry(request).State = EntityState.Detached;
                                request.Status = "Approve";
                                _context.Entry(request).State = EntityState.Modified;
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
                            var request = _context.Otrequests.Find(id);
                            if (request != null)
                            {
                                _context.Entry(request).State = EntityState.Detached;
                                request.RequestStatus = "Approve";
                                request.ApproverId = approverId;
                                _context.Entry(request).State = EntityState.Modified;
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
                            var request = _context.ResignationRequests.Find(id);
                            if (request != null)
                            {
                                _context.Entry(request).State = EntityState.Detached;
                                request.RequestStatus = "Approve";
                                request.ApproverId = approverId;
                                request.ApproveDate = DateTime.Now;
                                _context.Entry(request).State = EntityState.Modified;
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
                            var request = _context.TaxRequests.Find(id);
                            if (request != null)
                            {
                                _context.Entry(request).State = EntityState.Detached;
                                request.RequestStatus = "Approve";
                                request.ApproverId = approverId;
                                request.ApproveDate = DateTime.Now;
                                _context.Entry(request).State = EntityState.Modified;
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
                            var request = _context.UpdateEmployeeInforRequests.Find(id);
                            if (request != null)
                            {
                                _context.Entry(request).State = EntityState.Detached;
                                request.Status = "Approve";
                                request.ApproverId = approverId;
                                request.ApproveDate = DateTime.Now;
                                _context.Entry(request).State = EntityState.Modified;
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

            }
            return false;
        }

        public bool ResponseRequest(RequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }
    }

}

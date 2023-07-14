using DataAccess.ResignationRequestRepositories;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ResignationRequestRepositories
{
    public interface IResignationRequestRepository
    {
        ResignationRequest GetResignationRequestById(int id);
        List<ResignationRequest> GetAllPendingResignationRequests();
        void UpdateResignationRequest(ResignationRequest resignationRequest);
    }

    public class ResignationRequestRepository : IResignationRequestRepository 
    {
        protected readonly HRM_SWD392Context _context;
        public ResignationRequestRepository(HRM_SWD392Context context)
        {
            if (_context == null)
            {
                _context = context;
            }
        }

        public ResignationRequestRepository()
        {
            if (_context == null)
            {
                _context = new HRM_SWD392Context();
            }
        }

        public ResignationRequest GetResignationRequestById(int id)
        {
            return _context.ResignationRequests.SingleOrDefault(x => x.RequestId == id);
        }

        public List<ResignationRequest> GetAllPendingResignationRequests()
        {
            var request = _context.ResignationRequests.Where(x => x.RequestStatus == "Pending").ToList();
            return request;
        }

        public void UpdateResignationRequest(ResignationRequest resignationRequest)
        {
            var request = GetResignationRequestById(resignationRequest.RequestId);
            
            if(request != null)
            {
                _context.ResignationRequests.Update(resignationRequest);
                _context.SaveChanges();
            }
        }
    }
    
}

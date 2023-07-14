using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EmpRequestLeaveRepositories
{
    public interface IEmpRequestLeaveRepository
    {
        List<LeaveRequest>? GetAll();

        List<LeaveRequest>? GetByName(string nameUser);
        List<LeaveRequest> GetByNameAndHrId(string nameUser, int hrId);
        List<LeaveRequest>? GetByUserId(string userId);
        List<LeaveRequest>? GetByHrId(int? hrId);
        LeaveRequest GetById(int? id);



        void Update(LeaveRequest requestLeave);
        LeaveRequest saveLeaveRequest(LeaveRequest requestLeave);
    }


    public class EmpRequestLeaveRepository : IEmpRequestLeaveRepository
    {
        protected readonly HRM_SWD392Context _context;
        public EmpRequestLeaveRepository(HRM_SWD392Context context)
        {
            if (_context == null)
            {
                _context = context;
            }
        }
        public EmpRequestLeaveRepository()
        {
            if (_context == null)
            {
                _context = new HRM_SWD392Context();
            }
        }
        public LeaveRequest saveLeaveRequest(LeaveRequest requestLeave)
        {
            try
            {


                _context.LeaveRequests.Add(requestLeave);
                _context.SaveChanges();



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return requestLeave;
        }

        public List<LeaveRequest>? GetAll()
        {
            throw new NotImplementedException();
        }

        public List<LeaveRequest>? GetByHrId(int? hrId)
        {
            throw new NotImplementedException();
        }

        public LeaveRequest GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public List<LeaveRequest>? GetByName(string nameUser)
        {
            throw new NotImplementedException();
        }

        public List<LeaveRequest> GetByNameAndHrId(string nameUser, int hrId)
        {
            throw new NotImplementedException();
        }

        public List<LeaveRequest>? GetByUserId(string userId)
        {
            var requestLeaves = _context.LeaveRequests.Include(r => r.Hr).Where(c => c.EmployeeId == userId).ToList();
            return requestLeaves;
        }

        public void Update(LeaveRequest requestLeave)
        {
            throw new NotImplementedException();
        }
    }
}

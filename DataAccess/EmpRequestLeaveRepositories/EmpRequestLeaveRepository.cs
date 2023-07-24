using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;

namespace DataAccess.EmpRequestLeaveRepositories
{
    public interface IEmpRequestLeaveRepository
    {
        List<LeaveRequest>? GetByUserId(string userId);
        LeaveRequest GetById(int? id);
        void Update(LeaveRequest requestLeave);
        LeaveRequest saveLeaveRequest(LeaveRequest requestLeave);
    }


    public class EmpRequestLeaveRepository : IEmpRequestLeaveRepository
    {
        protected readonly HRM_SWD392Context _context;
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

        public LeaveRequest GetById(int? id)
        {
            return _context.LeaveRequests.Where(x => x.RequestId == id.Value).FirstOrDefault();
        }

        public List<LeaveRequest>? GetByUserId(string userId)
        {
            var requestLeaves = _context.LeaveRequests.Include(r => r.Hr).Where(c => c.EmployeeId.Trim() == userId.Trim()).ToList();
            return requestLeaves;
        }

        public void Update(LeaveRequest requestLeave)
        {
            try
            {
                _context.LeaveRequests.Update(requestLeave);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

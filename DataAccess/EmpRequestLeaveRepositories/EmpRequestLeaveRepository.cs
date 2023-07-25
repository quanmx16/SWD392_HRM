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
        void Delete(LeaveRequest requestLeave);
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
            return _context.LeaveRequests.Include(x => x.Employee).Include(x => x.Hr).Where(x => x.RequestId == id.Value).FirstOrDefault();
        }

        public List<LeaveRequest>? GetByUserId(string userId)
        {
            var requestLeaves = _context.LeaveRequests
                .Include(r => r.Hr)
                .Include(x => x.Employee)
                .Where(c => c.EmployeeId.Trim() == userId.Trim()).ToList();
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
        public void Delete(LeaveRequest requestLeave)
        {
            try
            {
                _context.LeaveRequests.Remove(requestLeave);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

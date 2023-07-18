using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;

namespace DataAccess.LeaveRequestRepositories
{
    public interface ILeaveRequestRepositories
    {
        Task<List<Employee>> GetAllEmployees();
        Task<List<Employee>> GetAllHR();
        Task<bool> CreateLeaveRequest(LeaveRequest leaveRequest);
        Task<List<LeaveRequest>> GetAllLeaveRequest();
        Task<bool> Approve(int id);
        Task<bool> Deny(int id);
    }

    public class LeaveRequestRepositories : ILeaveRequestRepositories
    {
        private readonly HRM_SWD392Context _context;
        public LeaveRequestRepositories()
        {
            _context = new HRM_SWD392Context();
        }

        public async Task<bool> Approve(int id)
        {
            var leaveRequest = await _context.LeaveRequests
                 .FirstOrDefaultAsync(m => m.RequestId == id);
            if (leaveRequest == null)
            {
                return false;
            }

            leaveRequest.Status = "Approve";
            _context.LeaveRequests.Update(leaveRequest);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateLeaveRequest(LeaveRequest leaveRequest)
        {
            await _context.LeaveRequests.AddAsync(leaveRequest);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Deny(int id)
        {
            var leaveRequest = await _context.LeaveRequests
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (leaveRequest == null)
            {
                return false;
            }

            leaveRequest.Status = "Deny";
            _context.LeaveRequests.Update(leaveRequest);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<List<Employee>> GetAllHR()
        {
            //Get theo role Khi nào biết role thì dùng cái này
            //return await _context.Employees.Where(x=>x.Role=="").ToListAsync();

            return await _context.Employees.ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetAllLeaveRequest()
        {
            return await _context.LeaveRequests
                .Include(l => l.Employee)
                .Include(l => l.Hr)
                .Where(x => x.Status == "Pending")
                .ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;

namespace DataAccess.UpdateEmployeeInforRequestsRepositories
{
    public interface IUpdateInforRequestsRepo
    {
        Task<List<UpdateEmployeeInforRequest>> GetAllRequests();
        Task<bool> Approve(int id);
        Task<bool> Deny(int id);
    }

    public class UpdateInforRequestsRepo : IUpdateInforRequestsRepo
    {
        private readonly HRM_SWD392Context _context;
        public UpdateInforRequestsRepo()
        {
            _context = new HRM_SWD392Context();
        }

        public async Task<bool> Approve(int id)
        {
            var requests = await _context.UpdateEmployeeInforRequests.FirstOrDefaultAsync(x => x.RequestId == id);
            if (requests == null)
            {
                return false;
            }
            requests.Status = "Approve";
            requests.ApproveDate = DateTime.Now;
            _context.UpdateEmployeeInforRequests.Update(requests);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Deny(int id)
        {
            var requests = await _context.UpdateEmployeeInforRequests.FirstOrDefaultAsync(x => x.RequestId == id);
            if (requests == null)
            {
                return false;
            }
            requests.Status = "Deny";
            _context.UpdateEmployeeInforRequests.Update(requests);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<UpdateEmployeeInforRequest>> GetAllRequests()
        {
            return await _context.UpdateEmployeeInforRequests
                 .Include(u => u.Approver)
                 .Include(u => u.Employee)
                 .Where(u => u.Status == "Pending")
                 .ToListAsync();
        }

    }
}

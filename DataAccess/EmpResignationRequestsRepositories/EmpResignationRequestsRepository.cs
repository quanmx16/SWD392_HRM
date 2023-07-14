using Model.Data;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EmpResignationRequestsRepositories
{

    public interface IEmpResignationRequestsRepository
    {
        ResignationRequest saveResgination(ResignationRequest resignationRequest);
    }
    public class EmpResignationRequestsRepository : IEmpResignationRequestsRepository
    {

        protected readonly HRM_SWD392Context _context;
        public EmpResignationRequestsRepository(HRM_SWD392Context context)
        {
            if (_context == null)
            {
                _context = context;
            }
        }
        public EmpResignationRequestsRepository()
        {
            if (_context == null)
            {
                _context = new HRM_SWD392Context();
            }
        }
        public ResignationRequest saveResgination(ResignationRequest resignationRequest)
        {
            try
            {


                _context.ResignationRequests.Add(resignationRequest);
                _context.SaveChanges();



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resignationRequest;
        }
    }
}

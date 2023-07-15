using Model.Data;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EmpUpdateAttendanceRequestRepositories
{
    public class EmpUpdateAttendanceRequestRepositories: IEmpUpdateAttendanceRequestRepositories
    {

        protected readonly HRM_SWD392Context _context;
        public EmpUpdateAttendanceRequestRepositories(HRM_SWD392Context context)
        {
            if (_context == null)
            {
                _context = context;
            }
        }
        public EmpUpdateAttendanceRequestRepositories()
        {
            if (_context == null)
            {
                _context = new HRM_SWD392Context();
            }
        }

        public UpdateAttendanceRequest requestUpdateAttendance(UpdateAttendanceRequest requestData)
        {
            try
            {


                _context.UpdateAttendanceRequests.Add(requestData);
                _context.SaveChanges();



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return requestData;
        }

    }

    public interface IEmpUpdateAttendanceRequestRepositories
    {
        UpdateAttendanceRequest requestUpdateAttendance(UpdateAttendanceRequest requestData);
    }
}

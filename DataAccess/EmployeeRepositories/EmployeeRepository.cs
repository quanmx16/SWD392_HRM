using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model.Data;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utility;

namespace DataAccess.EmployeeRepositories
{
    public interface IEmployeeRepository
    {
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        Employee GetEmployeeById(string id);
        Employee? GetEmployeeByEmail(string email, string password);
        List<Employee> GetHROrHRM();
        List<Employee> GetHRM();
        List<Employee> GetAll();
        List<Employee> Search(string search);
        bool RemoveEmployee(string id);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        protected readonly HRM_SWD392Context _context;
        public EmployeeRepository(HRM_SWD392Context context)
        {
            if (_context == null)
            {
                _context = context;
            }
        }
        public EmployeeRepository()
        {
            if (_context == null)
            {
                _context = new HRM_SWD392Context();
            }
        }
        public bool CreateEmployee(Employee employee)
        {
            try
            {
                CustomIdGenerator customIdGenerator = new CustomIdGenerator("EMP");
                EntityEntry<Employee> entry = _context.Entry(employee);
                string id = customIdGenerator.Next(entry);
                if (employee.DayOne == null)
                {
                    employee.DayOne = DateTime.Now;
                }
                employee.EmployeeId = id;
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                var emp = _context.Employees.Find(employee.EmployeeId);
                if (emp != null)
                {
                    _context.Entry(emp).State = EntityState.Detached;
                    _context.Entry(employee).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                    throw new Exception("Employee not exist");
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
        public Employee GetEmployeeById(string id)
        {
            return _context.Employees.Find(id);
        }
        public List<Employee> GetHROrHRM()
        {
            return _context.Employees.Where(x => x.Role.Trim() == "HR" || x.Role.Trim() == "HRManager").ToList();
        }

        public List<Employee> GetHRM()
        {
            return _context.Employees.Where(x => x.Role.Trim() == "HRManager").ToList();
        }
        public Employee? GetEmployeeByEmail(string email, string password)
        {
            var employee = _context.Employees.Where(x => x.Email.Equals(email) && x.Password.Equals(password) && (x.LastDay == null || x.LastDay > DateTime.Now)).FirstOrDefault();
            if (employee != null)
            {
                return TrimData(employee);
            }
            return null;
        }
        public List<Employee> GetAll()
        {
            List<Employee> lst = new List<Employee>();
            var lstEmps = _context.Employees.Include(e => e.Department).Include(e => e.Manager).Where(x => x.LastDay == null || x.LastDay > DateTime.Now).ToList();
            foreach (var emp in lstEmps)
            {
                lst.Add(TrimData(emp));
            }
            return lst;
        }
        public List<Employee> Search(string search)
        {
            List<Employee> lst = new List<Employee>();
            var lstEmps = _context.Employees.Include(e => e.Department).Include(e => e.Manager).Where(x => (x.LastDay == null || x.LastDay > DateTime.Now) && (x.EmplyeeName.ToLower().Contains(search.ToLower()) || x.EmployeeId.ToLower().Equals(search.ToLower()))).ToList();
            foreach (var emp in lstEmps)
            {
                lst.Add(TrimData(emp));
            }
            return lst;
        }
        public bool RemoveEmployee(string id)
        {
            try
            {
                var employee = _context.Employees.Find(id);
                if (employee != null)
                {
                    employee.LastDay = DateTime.Now;
                    _context.Entry(employee).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private Employee TrimData(Employee employee)
        {
            employee.EmployeeId = employee.EmployeeId?.Trim();
            employee.EmplyeeName = employee.EmplyeeName?.Trim();
            employee.Email = employee.Email?.Trim();
            employee.Password = employee.Password?.Trim();
            employee.Role = employee.Role?.Trim();
            employee.DepartmentId = employee.DepartmentId?.Trim();
            employee.Phone = employee.Phone?.Trim();
            employee.Address = employee.Address?.Trim();
            employee.TaxCode = employee.TaxCode?.Trim();
            employee.Level = employee.Level?.Trim();
            employee.ManagerId = employee.ManagerId?.Trim();
            return employee;
        }
    }
}

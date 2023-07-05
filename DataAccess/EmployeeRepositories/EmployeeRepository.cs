﻿using Microsoft.EntityFrameworkCore;
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
        List<Employee> GetHROrHRM();
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
                if(employee.DayOne == null)
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
            return _context.Employees.Where(x => x.Role == "HR" || x.Role == "HR Manager").ToList();
        }
    }
}
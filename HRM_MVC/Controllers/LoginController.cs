using DataAccess.EmployeeRepositories;
using HRM_MVC.SessionManager;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace HRM_MVC.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult LoginAction(string email, string password)
        {
            if (email == null || string.Empty == email || password == null || password == string.Empty)
            {
                ViewData["LoginError"] = "Email or password is incorrect. Try again!";
                return View("Index");
            }
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            Employee? employee = employeeRepository.GetEmployeeByEmail(email);
            if (employee == null)
            {
                ViewData["LoginError"] = "Email or password is incorrect. Try again!";
                ViewData["Email"] = email;
                return View("Index");
            }
            LoginAccount loginAccount =new LoginAccount();
            loginAccount.Employee = employee;
            SessionHelper.SerializeObjectToSession(HttpContext.Session, loginAccount, KeyConstants.ACCOUNT_KEY);
            return View();
        }
    }
}

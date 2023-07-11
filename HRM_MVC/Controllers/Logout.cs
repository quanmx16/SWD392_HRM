using HRM_MVC.SessionManager;
using Microsoft.AspNetCore.Mvc;

namespace HRM_MVC.Controllers
{
    public class Logout : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Remove(KeyConstants.ACCOUNT_KEY);
            return View();
        }
    }
}

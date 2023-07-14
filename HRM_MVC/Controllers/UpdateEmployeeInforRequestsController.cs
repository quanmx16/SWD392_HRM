using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.UpdateEmployeeInforRequestsRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Models;

namespace HRM_MVC.Controllers
{
    public class UpdateEmployeeInforRequestsController : Controller
    {
        private readonly IUpdateInforRequestsRepo _requestsRepo;

        public UpdateEmployeeInforRequestsController()
        {
            _requestsRepo = new UpdateInforRequestsRepo();
        }

        // GET: UpdateEmployeeInforRequests
        public async Task<IActionResult> Index()
        {
            var list = await _requestsRepo.GetAllRequests();
            return View(list);
        }

        // GET: UpdateEmployeeInforRequests/Approve/5
        public async Task<IActionResult> Approve(int? id)
        {
            var result = await _requestsRepo.Approve(id!.Value);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        // GET: UpdateEmployeeInforRequests/Deny/5
        public async Task<IActionResult> Deny(int? id)
        {
            var result = await _requestsRepo.Deny(id!.Value);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}

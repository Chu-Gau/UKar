using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UKAR.BL;
using UKAR.Pages;

namespace UKAR.Controllers.ViewColtrollers
{
    public class IndexController : Controller
    {
        public IndexController()
        {
        }
        // GET: Index
        [HttpGet]
        [Route("~/")]
        public ActionResult Index()
        {
            if (HttpContext.User == null || !HttpContext.User.Identity.IsAuthenticated)
            {
                return View("~/Pages/Login.cshtml", new LoginModel());
            }
            if (HttpContext.User.IsInRole("Admin")) return View("~/Pages/Admin.cshtml", new AdminModel());
            if (HttpContext.User.IsInRole("Driver") || HttpContext.User.IsInRole("Employer")) View("~/Pages/AccountManagement.cshtml");
            return NotFound();
        }
    }
}
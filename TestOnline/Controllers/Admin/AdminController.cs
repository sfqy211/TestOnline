using Microsoft.AspNetCore.Mvc;

namespace TestOnLine.Controllers.Admin
{
    public class AdminController : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View("~/Views/Admin/AdminDashboard.cshtml");
        }
    }
}

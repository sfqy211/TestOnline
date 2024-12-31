using Microsoft.AspNetCore.Mvc;

namespace TestOnLine.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View("~/Views/UI/AdminDashboard.cshtml");
        }
    }
}

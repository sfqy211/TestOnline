using Microsoft.AspNetCore.Mvc;

namespace TestOnLine.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult TeacherDashboard()
        {
            return View("~/Views/UI/TeacherDashboard.cshtml");
        }
    }
}

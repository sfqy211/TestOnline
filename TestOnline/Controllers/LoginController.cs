using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace TestOnLine.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISqlSugarClient _db;
        public LoginController(ISqlSugarClient db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Validate(string role, int id, string password)
        {
            if (role == "teacher")
            {
                if(id == 123456 && password == "admin")
                    return RedirectToAction("AdminDashboard", "Admin");
                var teacher = _db.Queryable<Models.Data.Teacher>().Where(it => it.TeacherId == id && it.Password == password).First();
                if (teacher != null)
                    return RedirectToAction("TeacherDashboard", "Teacher", new { id = teacher.TeacherId });
                else
                {
                    TempData["ErrorMessage"] = "账号或密码错误，请重试";
                    return RedirectToAction("Validate", "Login");
                }
            }
            else if (role == "student")
            {
                var student = _db.Queryable<Models.Data.Student>().Where(it => it.StudentId == id && it.Password == password).First();
                if (student != null)
                    return RedirectToAction("StudentDashboard", "Student", new { id = student.StudentId });
                else
                {
                    TempData["ErrorMessage"] = "账号或密码错误，请重试";
                    return RedirectToAction("Validate", "Login");
                }
            }
            return RedirectToAction("Validate", "Login");
        }

        public IActionResult Validate()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

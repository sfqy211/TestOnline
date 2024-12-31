using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using TestOnLine.Models;

namespace TestOnLine.Controllers
{
    public class StudentController : Controller
    {
        private readonly ISqlSugarClient _db;
        public StudentController(ISqlSugarClient db)
        {
            _db = db;
        }

        public IActionResult StudentDashboard(int id)
        {
            var student = _db.Queryable<Student>().Where(it => it.StudentId == id).First();
            if (student == null)
            {
                return NotFound();
            }

            ViewBag.StudentName = student.Name;
            ViewBag.StudentId = student.StudentId;
            return View("StudentDashboard");
        }

        public IActionResult LoadView(string viewName)
        {
            return PartialView($"_{viewName}");
        }
    }
}

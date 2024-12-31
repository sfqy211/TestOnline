using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using TestOnLine.Models;

namespace TestOnLine.Controllers.Teacher
{
    public class TeacherController : Controller
    {
        private readonly ISqlSugarClient _db;

        public TeacherController(ISqlSugarClient db)
        {
            _db = db;
        }

        public IActionResult TeacherDashboard(int id)
        {
            var teacher = _db.Queryable<Models.Data.Teacher>().Where(it => it.TeacherId == id).First();
            if (teacher == null)
            {
                return NotFound();
            }

            ViewBag.TeacherName = teacher.Name;
            ViewBag.TeacherId = teacher.TeacherId;
            return View("TeacherDashboard");
        }

        public IActionResult LoadView(string viewName, int teacherId)
        {
            ViewBag.TeacherId = teacherId;
            switch (viewName)
            {
                case "CourseManagement":
                    return PartialView("_CourseManagement");
                case "ExamManagement":
                    return PartialView("_ExamManagement");
                default:
                    return PartialView("_CourseManagement", new { teacherId });
            }
        }
    }
}

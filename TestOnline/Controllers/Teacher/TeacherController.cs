using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using TestOnLine.Models.Data;

namespace TestOnLine.Controllers.Teacher
{
    public class TeacherController : Controller
    {
        private readonly ISqlSugarClient _db;
        public TeacherController(ISqlSugarClient db)
        {
            _db = db;
        }

        public IActionResult TeacherDashboard(string id)
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

        public async Task<IActionResult> LoadView(string viewName, string teacherId)
        {
            ViewBag.TeacherId = teacherId;
            switch (viewName)
            {
                case "CourseManagement":
                    return await LoadCourseManagementView(teacherId);
                case "ExamManagement":
                    return await LoadExamManagementView(teacherId);
                default:
                    return NotFound();
            }
        }

        private async Task<IActionResult> LoadCourseManagementView(string teacherId)
        {
            var model = new Course
            {
                CurrentCourses = await _db.Queryable<Course, Models.Data.Teacher>((c, tcr) => new JoinQueryInfos(
                        JoinType.Inner, c.TeacherId == tcr.TeacherId
                    ))
                    .Where((c, tcr) => c.IsExam == false)
                    .ToListAsync(),

                CompletedCourses = await _db.Queryable<Course, Models.Data.Teacher>((c, tcr) => new JoinQueryInfos(
                        JoinType.Inner, c.TeacherId == tcr.TeacherId
                    ))
                    .Where((c, tcr) => c.IsExam == true)
                    .ToListAsync()
            };

            return PartialView("_CourseManagement", model);
        }

        private async Task<IActionResult> LoadExamManagementView(string teacherId)
        {
            var model = new Exam
            {
                OngoingExams = await _db.Queryable<Exam, Course, Models.Data.Teacher>((e, c, tcr) => new JoinQueryInfos(
                        JoinType.Inner, e.CourseId == c.CourseId,
                        JoinType.Inner, c.TeacherId == tcr.TeacherId
                    ))
                    .Select((e, c, tcr) => new OngoingExam
                    {
                        ExamId = e.ExamId,
                        ExamName = e.ExamName,
                        CourseId = e.CourseId,
                        TotalScore = e.TotalScore
                    })
                    .ToListAsync()
            };
            return PartialView("_ExamManagement", model);
        }
    }
}

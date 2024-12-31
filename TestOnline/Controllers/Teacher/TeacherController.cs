using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using TestOnLine.Models.Data;
using TestOnLine.Models.Management;

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

        public async Task<IActionResult> LoadView(string viewName, int teacherId)
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

        private async Task<IActionResult> LoadCourseManagementView(int teacherId)
        {
            var model = new CourseManagementModel
            {
                CurrentCourses = await _db.Queryable<Course, TeacherCourseRelation>((c, tcr) => new JoinQueryInfos(
                        JoinType.Inner, c.CourseId == tcr.CourseId
                    ))
                    .Where((c, tcr) => tcr.TeacherId == teacherId && c.IsExam == false)
                    .ToListAsync(),

                CompletedCourses = await _db.Queryable<Course, TeacherCourseRelation>((c, tcr) => new JoinQueryInfos(
                        JoinType.Inner, c.CourseId == tcr.CourseId
                    ))
                    .Where((c, tcr) => tcr.TeacherId == teacherId && c.IsExam == true)
                    .ToListAsync()
            };

            return PartialView("_CourseManagement", model);
        }

        private async Task<IActionResult> LoadExamManagementView(int teacherId)
        {
            var model = new ExamManagementModel
            {
                OngoingExams = await _db.Queryable<Exam, Course, TeacherCourseRelation>((e, c, tcr) => new JoinQueryInfos(
                        JoinType.Inner, e.CourseId == c.CourseId,
                        JoinType.Inner, c.CourseId == tcr.CourseId
                    ))
                    .Where((e, c, tcr) => tcr.TeacherId == teacherId && e.EndTime > DateTime.Now)
                    .Select((e, c, tcr) => new OngoingExam
                    {
                        ExamId = e.ExamId,
                        CourseName = c.Name,
                        ExamName = e.ExamName,
                        EndTime = e.EndTime,
                        CourseId = e.CourseId,
                        Data = e.Data,
                        Description = e.Description,
                        StartTime = e.StartTime,
                        Score = e.Score
                    })
                    .ToListAsync()
            };
            return PartialView("_ExamManagement", model);
        }
    }
}

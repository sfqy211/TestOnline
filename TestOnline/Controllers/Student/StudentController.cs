using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using TestOnLine.Models.Data;
using TestOnLine.Models.Management;

namespace TestOnLine.Controllers.Student
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
            var student = _db.Queryable<Models.Data.Student>().Where(it => it.StudentId == id).First();
            if (student == null)
            {
                return NotFound();
            }

            ViewBag.StudentName = student.Name;
            ViewBag.StudentId = student.StudentId;
            return View("StudentDashboard");
        }

        public async Task<IActionResult> LoadView(string viewName, int studentId)
        {
            ViewBag.StudentId = studentId;
            switch (viewName)
            {
                case "CourseManagement":
                    return await LoadCourseManagementView(studentId);
                case "ExamManagement":
                    return await LoadExamManagementView(studentId);
                case "GradeManagement":
                    return LoadGradeManagementView(studentId);
                default:
                    return NotFound();
            }
        }

        private async Task<IActionResult> LoadCourseManagementView(int studentId)
        {
            var model = new CourseManagementModel
            {
                CurrentCourses = await _db.Queryable<Course>()
                    .Where(c => SqlFunc.Subqueryable<ClassCourseRelation>()
                        .Where(ccr => ccr.CourseId == c.CourseId && SqlFunc.Subqueryable<Models.Data.Student>()
                            .Where(s => s.StudentId == studentId && s.ClassId == ccr.ClassId)
                            .Any())
                        .Any() && c.IsCompleted == false)
                    .ToListAsync(),

                CompletedCourses = await _db.Queryable<Course>()
                    .Where(c => SqlFunc.Subqueryable<ClassCourseRelation>()
                        .Where(ccr => ccr.CourseId == c.CourseId && SqlFunc.Subqueryable<Models.Data.Student>()
                            .Where(s => s.StudentId == studentId && s.ClassId == ccr.ClassId)
                            .Any())
                        .Any() && c.IsCompleted == true)
                    .ToListAsync()
            };

            return PartialView("_CourseManagement", model);
        }

        private async Task<IActionResult> LoadExamManagementView(int studentId)
        {
            var model = new ExamManagementModel
            {
                OngoingExams = await _db.Queryable<Exam, Course>((e, c) => new JoinQueryInfos(
                        JoinType.Inner, e.CourseId == c.CourseId
                    ))
                .Where((e, c) => e.EndTime > DateTime.Now && SqlFunc.Subqueryable<ClassCourseRelation>()
                    .Where(ccr => ccr.CourseId == c.CourseId && SqlFunc.Subqueryable<Models.Data.Student>()
                        .Where(s => s.StudentId == studentId && s.ClassId == ccr.ClassId)
                        .Any())
                    .Any())
                .Select((e, c) => new OngoingExam
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

        private IActionResult LoadGradeManagementView(int studentId)
        {
            var model = new GradeManagementModel
            {
                CourseGrades = new List<Course>(),
                ExamGrades = new List<Exam>()
            };
            return PartialView("_GradeManagement", model);
        }

        public async Task<IActionResult> CourseManagement(int studentId)
        {
            var model = new CourseManagementModel
            {
                CurrentCourses = await _db.Queryable<Course>()
                    .Where(c => SqlFunc.Subqueryable<ClassCourseRelation>()
                        .Where(ccr => ccr.CourseId == c.CourseId && SqlFunc.Subqueryable<Models.Data.Student>()
                            .Where(s => s.StudentId == studentId && s.ClassId == ccr.ClassId)
                            .Any())
                        .Any() && c.IsExam == false)
                    .ToListAsync(),

                CompletedCourses = await _db.Queryable<Course>()
                    .Where(c => SqlFunc.Subqueryable<ClassCourseRelation>()
                        .Where(ccr => ccr.CourseId == c.CourseId && SqlFunc.Subqueryable<Models.Data.Student>()
                            .Where(s => s.StudentId == studentId && s.ClassId == ccr.ClassId)
                            .Any())
                        .Any() && c.IsExam == true)
                    .ToListAsync()
            };

            return View(model);
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using TestOnLine.Models.Data;

namespace TestOnLine.Controllers.Student
{
    public class StudentController : Controller
    {
        private readonly ISqlSugarClient _db;
        public StudentController(ISqlSugarClient db)
        {
            _db = db;
        }

        public IActionResult StudentDashboard(string id)
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

        public async Task<IActionResult> LoadView(string viewName, string studentId)
        {
            ViewBag.StudentId = studentId;
            switch (viewName)
            {
                case "CourseManagement":
                    return await LoadCourseManagementView(studentId);
                case "ExamManagement":
                    return await LoadExamManagementView(studentId);
                case "GradeManagement":
                    return await LoadGradeManagementView(studentId);
                default:
                    return NotFound();
            }
        }

        private async Task<IActionResult> LoadCourseManagementView(string studentId)
        {
            var model = new Course
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

        private async Task<IActionResult> LoadExamManagementView(string studentId)
        {
            var model = new Exam
            {
                OngoingExams = await _db.Queryable<Exam, Course>((e, c) => new JoinQueryInfos(
                        JoinType.Inner, e.CourseId == c.CourseId
                    ))
                .Where((e, c) => SqlFunc.Subqueryable<ClassCourseRelation>()
                    .Where(ccr => ccr.CourseId == c.CourseId && SqlFunc.Subqueryable<Models.Data.Student>()
                        .Where(s => s.StudentId == studentId && s.ClassId == ccr.ClassId)
                        .Any())
                    .Any() && c.IsCompleted == false)
                .Select((e, c) => new OngoingExam
                {
                    ExamId = e.ExamId,
                    ExamName = e.ExamName,
                    CourseId = e.CourseId,
                    CourseName = c.Name
                })
                .ToListAsync()
            };
            return PartialView("_ExamManagement", model);
        }

        private async Task<IActionResult> LoadGradeManagementView(string studentId)
        {
            var examScores = await _db.Queryable<StudentExamRelation, Exam>((ser, e) => new JoinQueryInfos(
                    JoinType.Inner, ser.ExamId == e.ExamId
                ))
                .Where((ser, e) => ser.StudentId == studentId)
                .Select((ser, e) => new
                {
                    e.ExamName,
                    ser.StudentScore,
                    e.ExamId
                })
                .ToListAsync();

            var model = new Grade
            {
                ExamGrades = examScores.Select(es => new Exam
                {
                    ExamId = es.ExamId,
                    ExamName = es.ExamName,
                    StudentScore = es.StudentScore
                }).ToList()
            };

            return PartialView("_GradeManagement", model);
        }





        public async Task<IActionResult> CourseManagement(string studentId)
        {
            var model = new Course
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

            return View(model);
        }
    }
}


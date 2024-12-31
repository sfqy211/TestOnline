using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using TestOnLine.Models;

namespace TestOnLine.Controllers
{
    public class StudentController : Controller
    {
        private readonly ISqlSugarClient _db;
        public StudentController(ISqlSugarClient(ISqlSugarClient db)
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

        public async Task<IActionResult> LoadView(string viewName, int studentId)
        {
            ViewBag.StudentId = studentId;
            switch (viewName)
            {
                case "CourseManagement":
                    var courseManagementModel = new CourseManagementModel(_db);
                    await courseManagementModel.OnGetAsync(studentId);
                    return PartialView("_CourseManagement", courseManagementModel);
                case "ExamManagement":
                    var examManagementModel = new ExamManagementModel(_db);
                    await examManagementModel.OnGetAsync(studentId);
                    return PartialView("_ExamManagement", examManagementModel);
                case "GradeManagement":
                    var gradeManagementModel = new GradeManagementModel(_db);
                    await gradeManagementModel.OnGetAsync(studentId);
                    return PartialView("_GradeManagement", gradeManagementModel);
                case "PersonalInfo":
                    var student = await _db.Queryable<Student>().Where(it => it.StudentId == studentId).FirstAsync();
                    if (student == null)
                    {
                        return NotFound();
                    }
                    return PartialView("_PersonalInfo", student);
                default:
                    return PartialView("_CourseManagement", new { studentId });
            }
        }

        public async Task<IActionResult> CourseManagement(int studentId)
        {
            var model = new CourseManagementModel
            {
                CurrentCourses = await _db.Queryable<Course>()
                    .Where(c => SqlFunc.Subqueryable<ClassCourseRelation>()
                        .Where(ccr => ccr.CourseId == c.CourseId && SqlFunc.Subqueryable<Student>()
                            .Where(s => s.StudentId == studentId && s.ClassId == ccr.ClassId)
                            .Any())
                        .Any() && c.IsExam == false)
                    .ToListAsync(),

                CompletedCourses = await _db.Queryable<Course>()
                    .Where(c => SqlFunc.Subqueryable<ClassCourseRelation>()
                        .Where(ccr => ccr.CourseId == c.CourseId && SqlFunc.Subqueryable<Student>()
                            .Where(s => s.StudentId == studentId && s.ClassId == ccr.ClassId)
                            .Any())
                        .Any() && c.IsExam == true)
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var student = await _db.Queryable<Student>().Where(it => it.StudentId == request.StudentId).FirstAsync();
            if (student == null)
            {
                return Json(new { success = false, message = "学生不存在" });
            }
        
            student.Password = request.NewPassword;
            await _db.Updateable(student).ExecuteCommandAsync();
        
            return Json(new { success = true, message = "密码修改成功" });
        }
        
        public class ChangePasswordRequest
        {
            public int StudentId { get; set; }
            public string NewPassword { get; set; }
        }
    }
}

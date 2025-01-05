using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using SqlSugar;
using TestOnLine.Models.Data;

namespace TestOnLine.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly ISqlSugarClient _db;
        public AdminController(ISqlSugarClient db)
        {
            _db = db;
        }

        public IActionResult AdminDashboard()
        {
            return View("AdminDashboard");
        }

        public IActionResult LoadView(string viewName)
        {
            switch (viewName)
            {
                case "StudentManagement":
                    return LoadStudentManagementView();
                case "TeacherManagement":
                    return LoadTeacherManagementView();
                case "CourseManagement":
                    return LoadCourseManagementView();
                default:
                    return NotFound();
            }
        }

        private IActionResult LoadStudentManagementView()
        {
            // 实现学生管理视图加载逻辑
            return PartialView("_StudentManagement");
        }

        private IActionResult LoadTeacherManagementView()
        {
            // 实现教师管理视图加载逻辑
            return PartialView("_TeacherManagement");
        }

        private IActionResult LoadCourseManagementView()
        {
            // 实现课程管理视图加载逻辑
            return PartialView("_CourseManagement");
        }

        [HttpGet]
        public async Task<IActionResult> SearchStudents(string studentName)
        {
            var students = await _db.Queryable<Models.Data.Student>()
                .Where(s => s.Name.Contains(studentName))
                .ToListAsync();

            if (students.Any())
            {
                var classIds = students.Select(s => s.ClassId).Distinct().ToList();
                var classes = await _db.Queryable<Models.Data.Class>()
                    .In(c => c.ClassId, classIds)
                    .ToListAsync();

                foreach (var student in students)
                {
                    var studentClass = classes.FirstOrDefault(c => c.ClassId == student.ClassId);
                    if (studentClass != null)
                    {
                        student.FacultyId = studentClass.FacultyId;
                    }
                }
            }

            return PartialView("_StudentSearchResults", students);
        }




        [HttpGet]
        public async Task<IActionResult> SearchTeachers(string teacherName)
        {
            var teachers = await _db.Queryable<Models.Data.Teacher>()
                .Where(t => t.Name.Contains(teacherName))
                .ToListAsync();
            return PartialView("_TeacherSearchResults", teachers);
        }

        [HttpGet]
        public async Task<IActionResult> SearchCourses(string courseName)
        {
            var courses = await _db.Queryable<Models.Data.Course>()
                .Where(c => c.Name.Contains(courseName))
                .ToListAsync();

            if (courses.Any())
            {
                var teacherIds = courses.Select(c => c.TeacherId).Distinct().ToList();
                var teachers = await _db.Queryable<Models.Data.Teacher>()
                    .In(t => t.TeacherId, teacherIds)
                    .ToListAsync();

                foreach (var course in courses)
                {
                    var teacher = teachers.FirstOrDefault(t => t.TeacherId == course.TeacherId);
                    if (teacher != null)
                    {
                        course.FacultyId = teacher.FacultyId;
                    }
                }
            }

            return PartialView("_CourseSearchResults", courses);
        }

    }
}

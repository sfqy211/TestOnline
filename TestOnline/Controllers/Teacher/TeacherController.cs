using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using SqlSugar;
using TestOnLine.Controllers.Student;
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
                    .Where((c, tcr) => c.IsCompleted == false)
                    .Where((c, tcr) => c.TeacherId == teacherId)
                    .ToListAsync(),

                CompletedCourses = await _db.Queryable<Course, Models.Data.Teacher>((c, tcr) => new JoinQueryInfos(
                        JoinType.Inner, c.TeacherId == tcr.TeacherId
                    ))
                    .Where((c, tcr) => c.IsCompleted == true)
                    .Where((c, tcr) => c.TeacherId == teacherId)
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
                        CourseId = e.CourseId
                    })
                    .ToListAsync()
            };
            return PartialView("_ExamManagement", model);
        }

        public async Task<IActionResult> ToggleCourseStatus(string courseId)
        {
            var course = await _db.Queryable<Course>().Where(c => c.CourseId == courseId).FirstAsync();
            if (course == null)
            {
                return Content("课程未找到");
            }

            course.IsCompleted = !course.IsCompleted;
            await _db.Updateable(course).ExecuteCommandAsync();

            var teacherId = course.TeacherId;
            var model = new Course
            {
                CurrentCourses = await _db.Queryable<Course, Models.Data.Teacher>((c, tcr) => new JoinQueryInfos(
                        JoinType.Inner, c.TeacherId == tcr.TeacherId
                    ))
                    .Where((c, tcr) => c.IsCompleted == false)
                    .Where((c, tcr) => c.TeacherId == teacherId)
                    .ToListAsync(),

                CompletedCourses = await _db.Queryable<Course, Models.Data.Teacher>((c, tcr) => new JoinQueryInfos(
                        JoinType.Inner, c.TeacherId == tcr.TeacherId
                    ))
                    .Where((c, tcr) => c.IsCompleted == true)
                    .Where((c, tcr) => c.TeacherId == teacherId)
                    .ToListAsync()
            };

            return PartialView("_CourseManagement", model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateExam(string examId, string teacherId)
        {
            var exam = await _db.Queryable<Exam>().Where(e => e.ExamId == examId).FirstAsync();
            if (exam == null)
            {
                return NotFound();
            }

            var model = new Exam
            {
                ExamId = exam.ExamId,
                ExamName = exam.ExamName,
                Time = exam.Time,
                ChoiceQuestions = await _db.Queryable<ChoiceQuestion>().Where(q => q.ExamId == exam.ExamId).ToListAsync(),
                CompletionQuestions = await _db.Queryable<CompletionQuestion>().Where(q => q.ExamId == exam.ExamId).ToListAsync()
            };

            ViewBag.TeacherId = teacherId;
            return View("TeacherSetQuestions", model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateExam(Exam model)
        {
            // 更新或插入考试信息
            var exam = await _db.Queryable<Exam>().Where(e => e.ExamId == model.ExamId).FirstAsync();
            if (exam == null)
            {
                exam = new Exam
                {
                    ExamId = model.ExamId,
                    ExamName = model.ExamName,
                    Time = model.Time,
                    TeacherId = model.TeacherId
                };
                await _db.Insertable(exam).ExecuteCommandAsync();
            }
            else
            {
                exam.ExamName = model.ExamName;
                exam.Time = model.Time;
                await _db.Updateable(exam).ExecuteCommandAsync();
            }

            // 删除旧的选择题和填空题
            await _db.Deleteable<ChoiceQuestion>().Where(q => q.ExamId == model.ExamId).ExecuteCommandAsync();
            await _db.Deleteable<CompletionQuestion>().Where(q => q.ExamId == model.ExamId).ExecuteCommandAsync();

            // 插入新的选择题
            foreach (var choiceQuestion in model.ChoiceQuestions)
            {
                choiceQuestion.QuestionId = Guid.NewGuid().ToString();
                choiceQuestion.ExamId = model.ExamId;
                await _db.Insertable(choiceQuestion).ExecuteCommandAsync();
            }

            // 插入新的填空题
            foreach (var completionQuestion in model.CompletionQuestions)
            {
                completionQuestion.QuestionId = Guid.NewGuid().ToString();
                completionQuestion.ExamId = model.ExamId;
                await _db.Insertable(completionQuestion).ExecuteCommandAsync();
            }

            return RedirectToAction("TeacherDashboard", new { id = model.TeacherId });
        }

    }

}

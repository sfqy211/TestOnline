using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using TestOnLine.Models.Data;

namespace TestOnLine.Controllers.Student
{
    public class ExamController : Controller
    {
        private readonly ISqlSugarClient _db;

        public ExamController(ISqlSugarClient db)
        {
            _db = db;
        }

        public async Task<IActionResult> TakeExam(string examId, string studentId, string courseId)
        {
            var exam = await _db.Queryable<Models.Data.Exam>()
                .Where(e => e.ExamId == examId)
                .FirstAsync();

            if (exam == null)
            {
                return NotFound();
            }

            var course = await _db.Queryable<Models.Data.Course>()
                .Where(c => c.CourseId == courseId)
                .FirstAsync();

            if (course == null)
            {
                return NotFound();
            }

            var student = await _db.Queryable<Models.Data.Student>()
                .Where(s => s.StudentId == studentId)
                .FirstAsync();

            if (student == null)
            {
                return NotFound();
            }

            // 获取选择题并过滤掉选项内容为空的选项
            var choiceQuestions = await _db.Queryable<Models.Data.ChoiceQuestion>()
                .Where(q => q.ExamId == examId)
                .ToListAsync();

            // 过滤掉选项内容为空的选项
            foreach (var question in choiceQuestions)
            {
                question.OptionA = string.IsNullOrEmpty(question.OptionA) ? null : question.OptionA;
                question.OptionB = string.IsNullOrEmpty(question.OptionB) ? null : question.OptionB;
                question.OptionC = string.IsNullOrEmpty(question.OptionC) ? null : question.OptionC;
                question.OptionD = string.IsNullOrEmpty(question.OptionD) ? null : question.OptionD;
            }

            // 获取填空题
            var completionQuestions = await _db.Queryable<Models.Data.CompletionQuestion>()
                .Where(q => q.ExamId == examId)
                .ToListAsync();

            // 计算剩余时间
            var examDuration = TimeSpan.FromMinutes(exam.Time);
            var remainingTime = (int)(examDuration).TotalSeconds;

            if (remainingTime < 0)
            {
                remainingTime = 0;
            }

            var model = new ExamViewModel
            {
                ExamId = exam.ExamId,
                CourseName = course.Name,
                ExamName = exam.ExamName,
                StudentName = student.Name,
                StudentId = student.StudentId,
                RemainingTime = remainingTime,
                ChoiceQuestions = choiceQuestions,
                CompletionQuestions = completionQuestions
            };

            return View("StudentIndex", model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitExam(string ExamId, string StudentId, Dictionary<string, string> ChoiceAnswers, Dictionary<string, string> CompletionAnswers)
        {
            var exam = await _db.Queryable<Models.Data.Exam>()
                .Where(e => e.ExamId == ExamId)
                .FirstAsync();

            if (exam == null)
            {
                return NotFound();
            }

            var student = await _db.Queryable<Models.Data.Student>()
                .Where(s => s.StudentId == StudentId)
                .FirstAsync();

            if (student == null)
            {
                return NotFound();
            }

            // 获取选择题和填空题
            var choiceQuestions = await _db.Queryable<Models.Data.ChoiceQuestion>()
                .Where(q => q.ExamId == ExamId)
                .ToListAsync();

            var completionQuestions = await _db.Queryable<Models.Data.CompletionQuestion>()
                .Where(q => q.ExamId == ExamId)
                .ToListAsync();

            float totalScore = 0;

            // 处理选择题答案
            foreach (var answer in ChoiceAnswers)
            {
                var question = choiceQuestions.FirstOrDefault(q => q.QuestionId == answer.Key);
                if (question != null && question.Correct == answer.Value)
                {
                    totalScore += question.Score;
                }
            }

            // 处理填空题答案
            foreach (var answer in CompletionAnswers)
            {
                var question = completionQuestions.FirstOrDefault(q => q.QuestionId == answer.Key);
                if (question != null && question.Correct == answer.Value)
                {
                    totalScore += question.Score;
                }
            }

            // 保存学生考试关系
            var studentExamRelation = new StudentExamRelation
            {
                StudentId = StudentId,
                ExamId = ExamId,
                StudentScore = totalScore
            };

            await _db.Insertable(studentExamRelation).ExecuteCommandAsync();

            return RedirectToAction("StudentDashboard", "Student", new { id = StudentId });
        }
    }

    public class ExamViewModel
    {
        public string ExamId { get; set; }
        public string CourseName { get; set; }
        public string ExamName { get; set; }
        public string StudentName { get; set; }
        public string StudentId { get; set; }
        public List<ChoiceQuestion> ChoiceQuestions { get; set; }
        public List<CompletionQuestion> CompletionQuestions { get; set; }
        public int RemainingTime { get; set; }
    }

    public class ChoiceAnswer
    {
        public string QuestionId { get; set; }
        public string SelectedOption { get; set; }
    }

    public class CompletionAnswer
    {
        public string QuestionId { get; set; }
        public string AnswerText { get; set; }
    }

    public class ExamSubmission
    {
        public string ExamId { get; set; }
        public string StudentId { get; set; }
        public List<ChoiceAnswer> ChoiceAnswers { get; set; }
        public List<CompletionAnswer> CompletionAnswers { get; set; }
    }
}




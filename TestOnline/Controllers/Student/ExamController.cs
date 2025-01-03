using Microsoft.AspNetCore.Mvc;
using SqlSugar;

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

            string sampleQuestionsJson = @"
            [
                {""QuestionId"": 1, ""Text"": ""What is the capital of France?""},
                {""QuestionId"": 2, ""Text"": ""What is 2 + 2?""},
                {""QuestionId"": 3, ""Text"": ""Who wrote 'To Kill a Mockingbird'?""},
                {""QuestionId"": 4, ""Text"": ""What is the largest planet in our solar system?""},
                {""QuestionId"": 5, ""Text"": ""What is the chemical symbol for water?""}
            ]";

            // 假设考试数据是一个 JSON 字符串，包含题目信息
            var questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Question>>(sampleQuestionsJson);
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
                Questions = questions,
                RemainingTime = remainingTime
            };

            return View("StudentIndex", model);
        }
    }

    public class Question
    {
        public string Text { get; set; }
    }

    public class ExamViewModel
    {
        public string ExamId { get; set; }
        public string CourseName { get; set; }
        public string ExamName { get; set; }
        public string StudentName { get; set; }
        public string StudentId { get; set; }
        public List<Question> Questions { get; set; }
        public int RemainingTime { get; set; }
    }
}
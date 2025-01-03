using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class Exam
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string ExamId { get; set; }
        public string CourseId { get; set; }
        public string ExamName { get; set; }
        public float TotalScore { get; set; }
        public Int16 Time { get; set; }
        public string RightQuestionInfo { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<OngoingExam> OngoingExams { get; set; }
    }

    public class OngoingExam : Exam
    {
        public string CourseName { get; set; }
    }
}
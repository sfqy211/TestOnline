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
        public int Time { get; set; }
        public float Proportion { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<OngoingExam> OngoingExams { get; set; }
    }

    public class OngoingExam : Exam
    {
        public string CourseName { get; set; }
    }
}
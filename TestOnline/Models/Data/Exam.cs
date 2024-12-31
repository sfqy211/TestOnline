using SqlSugar;

namespace TestOnLine.Models
{
    public class Exam
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int ExamId { get; set; }

        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }

        public string RightAnswer { get; set; }

        public string StudentAnswer { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public float Score { get; set; }
    }
}
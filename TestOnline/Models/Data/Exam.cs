using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class Exam
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long ExamId { get; set; }

        public long CourseId { get; set; }

        public required string ExamName { get; set; }

        public required string Data { get; set; }

        public required string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public float Score { get; set; }
    }
}
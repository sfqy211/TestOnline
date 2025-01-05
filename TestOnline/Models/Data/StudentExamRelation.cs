using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class StudentExamRelation
    {
        [SugarColumn(IsNullable = true)]
        public string StudentId { get; set; }
        [SugarColumn(IsNullable = true)]
        public string ExamId { get; set; }
        public float StudentScore { get; set; }
    }
}

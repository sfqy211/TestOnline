using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class StudentExamRelation
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string QuestionId { get; set; }
        public string StudentId { get; set; }
        public string ExamId { get; set; }
        public float StudentScore { get; set; }
        public string StudentQuestionInfo { get; set; }
    }
}

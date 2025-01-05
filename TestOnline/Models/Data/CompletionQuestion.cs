using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class CompletionQuestion
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string QuestionId { get; set; }
        [SugarColumn(IsNullable = true)]
        public string ExamId { get; set; }
        public string Text { get; set; }
        public string Correct { get; set; }
        public float Score { get; set; }
    }
}

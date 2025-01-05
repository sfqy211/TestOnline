using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class ChoiceQuestion
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string QuestionId { get; set; }
        [SugarColumn(IsNullable = true)]
        public string ExamId { get; set; }
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Correct { get; set; }
        public float Score { get; set; }
    }
}

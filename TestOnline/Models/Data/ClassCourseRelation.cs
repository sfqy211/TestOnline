using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class ClassCourseRelation
    {
        [SugarColumn(IsNullable = true)]
        public string? ClassId { get; set; }

        [SugarColumn(IsNullable = true)]
        public string? CourseId { get; set; }
    }
}
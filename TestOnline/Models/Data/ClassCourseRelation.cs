using SqlSugar;

namespace TestOnline.Models
{
    public class ClassCourseRelation
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int ClassId { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public int CourseId { get; set; }
    }
}
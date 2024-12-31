using SqlSugar;

namespace TestOnline.Models
{
    public class TeacherCourseRelation
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int TeacherId { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public int CourseId { get; set; }
    }
}
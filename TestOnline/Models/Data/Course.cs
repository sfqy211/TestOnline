using SqlSugar;

namespace TestOnLine.Models
{
    public class Course
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int CourseId { get; set; }

        public int FacultyId { get; set; }

        public bool IsExam { get; set; }

        public string Name { get; set; }

        public float Credit { get; set; }
    }
}
using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class Course
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long CourseId { get; set; }

        public int FacultyId { get; set; }

        public bool IsExam { get; set; }

        public bool IsCompleted { get; set; }

        public required string Name { get; set; }

        public float Credit { get; set; }
    }
}
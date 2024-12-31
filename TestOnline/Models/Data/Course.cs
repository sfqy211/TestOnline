using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class Course
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int CourseId { get; set; }

        public int FacultyId { get; set; }

        public bool IsExam { get; set; }

        public required string Name { get; set; }

        public float Credit { get; set; }

        public float Score { get; set; }
    }
}
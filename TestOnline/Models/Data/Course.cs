using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class Course
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string CourseId { get; set; }
        public string TeacherId { get; set; }
        public string Name { get; set; }
        public float Credit { get; set; }
        public int CreditHours { get; set; }
        public bool IsExam { get; set; }
        public bool IsCompleted { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string FacultyId { get; set; }
        [SugarColumn(IsIgnore = true)]
        public List<Course> CurrentCourses { get; set; }
        [SugarColumn(IsIgnore = true)]
        public List<Course> CompletedCourses { get; set; }
    }
}
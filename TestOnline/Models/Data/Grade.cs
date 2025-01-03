namespace TestOnLine.Models.Data
{
    public class Grade
    {
        public required List<Course> CourseGrades { get; set; }
        public required List<Exam> ExamGrades { get; set; }
    }
}

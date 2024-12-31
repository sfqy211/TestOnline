using TestOnLine.Models.Data;

namespace TestOnLine.Models.Management
{
    public class CourseManagementModel
    {
        public required List<Course> CurrentCourses { get; set; }
        public required List<Course> CompletedCourses { get; set; }
    }
}
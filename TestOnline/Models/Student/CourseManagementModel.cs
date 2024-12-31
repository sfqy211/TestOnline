// Models/CourseManagementModel.cs
using System.Collections.Generic;

namespace TestOnLine.Models
{
    public class CourseManagementModel
    {
        public List<Course> CurrentCourses { get; set; }
        public List<Course> CompletedCourses { get; set; }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public bool IsExam { get; set; }
    }
}
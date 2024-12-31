using System;
using System.Collections.Generic;
using TestOnLine.Models.Data;

namespace TestOnLine.Models.Management
{
    public class GradeManagementModel
    {
        public required List<Course> CourseGrades { get; set; }
        public required List<Exam> ExamGrades { get; set; }
    }
}

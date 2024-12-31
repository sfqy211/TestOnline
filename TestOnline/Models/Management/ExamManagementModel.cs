using TestOnLine.Models.Data;

namespace TestOnLine.Models.Management
{
    public class ExamManagementModel
    {
        public required List<OngoingExam> OngoingExams { get; set; }
    }

    public class OngoingExam : Exam
    {
        public string CourseName { get; set; } = string.Empty;
    }
}
using SqlSugar;

namespace TestOnLine.Models
{
    public class Teacher
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int TeacherId { get; set; }

        public int FacultyId { get; set; }

        public string? Name { get; set; }

        public string? Password { get; set; }
    }
}

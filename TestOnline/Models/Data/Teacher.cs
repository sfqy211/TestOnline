using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class Teacher
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int TeacherId { get; set; }

        public int FacultyId { get; set; }

        public required string Name { get; set; }

        public required string Password { get; set; }
    }
}

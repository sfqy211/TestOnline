using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class Teacher
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string TeacherId { get; set; }

        public string FacultyId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}

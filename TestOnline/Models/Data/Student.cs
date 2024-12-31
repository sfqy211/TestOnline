using SqlSugar;
namespace TestOnLine.Models.Data
{
    public class Student
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public string? Name { get; set; }

        public string? Password { get; set; }
    }
}
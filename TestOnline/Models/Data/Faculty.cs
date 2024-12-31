using SqlSugar;

namespace TestOnLine.Models
{
    public class Faculty
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int FacultyId { get; set; }

        public string Name { get; set; }
    }
}
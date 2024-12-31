using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class Faculty
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int FacultyId { get; set; }

        public required string Name { get; set; }
    }
}
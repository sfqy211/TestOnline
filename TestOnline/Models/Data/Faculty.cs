using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class Faculty
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string FacultyId { get; set; }

        public string Name { get; set; }
    }
}
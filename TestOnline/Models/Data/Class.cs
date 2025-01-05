using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class Class
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string ClassId { get; set; }
        [SugarColumn(IsNullable = true)]
        public string FacultyId { get; set; }
    }
}
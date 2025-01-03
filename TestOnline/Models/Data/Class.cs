using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class Class
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string ClassId { get; set; }

        public string FacultyId { get; set; }
    }
}
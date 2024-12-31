using SqlSugar;

namespace TestOnLine.Models
{
    public class Class
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int ClassId { get; set; }

        public int FacultyId { get; set; }
    }
}
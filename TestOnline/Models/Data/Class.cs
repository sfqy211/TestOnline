using SqlSugar;

namespace TestOnLine.Models.Data
{
    public class Class
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int ClassId { get; set; }

        public int FacultyId { get; set; }
    }
}
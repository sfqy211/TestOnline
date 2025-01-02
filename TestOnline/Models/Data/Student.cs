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

        public int FacultyId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string? Department
        {
            get
            {
                if (FacultyId == 6)
                    return "计算机学院";
                return department;
            }
            set
            {
                department = value;
            }
        }

        private string? department;
    }
}
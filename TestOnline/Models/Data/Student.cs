﻿using SqlSugar;
namespace TestOnLine.Models.Data
{
    public class Student
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string StudentId { get; set; }
        [SugarColumn(IsNullable = true)]
        public string ClassId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string FacultyId { get; set; }
    }
}
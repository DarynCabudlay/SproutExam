using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.DataAccess.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string TIN { get; set; }
        public int EmployeeTypeId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

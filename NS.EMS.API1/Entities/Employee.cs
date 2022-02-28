using System;
using System.Collections.Generic;

namespace NS.EMS.API1.Entities
{
    public partial class Employee
    {
        public int Eid { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? DeptId { get; set; }
        public string Gender { get; set; }
    }
}

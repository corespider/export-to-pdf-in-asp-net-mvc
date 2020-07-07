using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExportPDFMVC.Models
{
    public class Employee
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string Mobile { get; set; }
        public string PresentAddress { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Qualification { get; set; }
        public string Email { get; set; }
    }
}
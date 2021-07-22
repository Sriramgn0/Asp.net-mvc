using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace University_Website.Models
{
    public class DepartmentType
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Departmenttype { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_Website.Models
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }

       
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public  virtual DepartmentType departmentType { get; set; }

        public string Description { get; set; }


        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual UserRegistration userRegistration { get; set; }

    }
}
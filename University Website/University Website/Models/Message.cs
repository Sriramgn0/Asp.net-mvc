using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace University_Website.Models
{
    public class Message
    {

        [Key]
        public int id { get; set; }

        public int FromUserID { get; set; }

       

        public int Totypeid { get; set; }
        [ForeignKey("Totypeid")]
        public DepartmentType dept { get; set; }

        public string to { get; set; }

        public string EventLink { get; set; }

        public string message { get; set; }
    }
}
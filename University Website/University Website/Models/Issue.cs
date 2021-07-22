using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace University_Website.Models
{
    public class Issue
    {
        [Key]
        public int IssueId { get; set; }

        public string IssueName { get; set; }

        public string Description { get; set; }

        public DateTime IssueDate { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual UserRegistration userRegistration { get; set; }


        public string AdminReview { get; set; }

        public string Contact { get; set; }
    }
}
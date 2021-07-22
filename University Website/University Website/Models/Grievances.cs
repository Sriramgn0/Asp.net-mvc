using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace University_Website.Models
{
    public class Grievances
    {


        [Key]
        public int id { get; set; }

        public string Topic { get; set; }

        public string SubTopic { get; set; }

        public string Details { get; set; }

        public string Suggestion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please Provide Event Deadline")]
        public DateTime DeadLine { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual UserRegistration userRegistration { get; set; }

        public string AdminReview { get; set; }

        public grief Status { get; set; }

        public enum grief
        {
            New,Progress,Resolved,Completed
        }
        


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace University_Website.Models
{
    public class IdeaReviewers
    {

        [Key]
        public int id { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual UserRegistration userRegistration { get; set; }

       
        public int IdeaId { get; set; }

        [ForeignKey("IdeaId")]
        public virtual Idea idea { get; set; }

        public int Vote { get; set; }

        public int DeVote { get; set; }

        public string Comment { get; set; }
    }
}
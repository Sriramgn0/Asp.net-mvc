using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
namespace University_Website.Models
{
    public class Idea
    {
        [Key]
        public int IdeaId  { get; set; }

       
        public int ClubID { get; set; }

       

        
        public int CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        public virtual  Category category { get; set; }

        public string IdeaTitle { get; set; }

        public string Description  { get; set; }

        public DateTime SubmissionDate { get; set; }

        public int UserId { get; set; }


        public string AdminReview { get; set; }

        public stat Status { get; set; }

        public enum stat {New,Accepted}

        public int NoOfVotes { get; set; }

        public int NoOFDevote { get; set; }
    }
}

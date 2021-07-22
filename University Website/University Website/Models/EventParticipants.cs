using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace University_Website.Models
{
    public class EventParticipants
    {
        [Key]
        public int id { get; set; }
        
        public int EventID { get; set; }

        [ForeignKey("EventID")]
        public virtual Events events { get; set; }

        
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual UserRegistration userRegistration { get; set; }

        public att Attendence { get; set; }
        public enum att { Select,Markattendence}

        public review Review { get; set; }
        public enum review
        {Select,Intrested,NotIntrested}

        public string UserComment { get; set; }

        public likeordislike userchoice { get; set; }
        public enum likeordislike { select,Like,Dislike}
    }
}
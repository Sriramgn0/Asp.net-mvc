using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace University_Website.Models
{
    public class SocialServiceParticipants
    {
        [Key]
        public int id { get; set; }

        
        public int SocialServiceId { get; set; }

        [ForeignKey("SocialServiceId")]
        public virtual SocialService socialService { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual UserRegistration userRegistration { get; set; }

        public int VolunterOrNot { get; set; }
    }
}
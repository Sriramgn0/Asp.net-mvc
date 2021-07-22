using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace University_Website.Models
{
    public class ClubMember
    {
        public int UserId { get; set; }
        [Key]
        public int ClubMemberId { get; set; }
        public int ClubId { get; set; }
        public string Designation { get; set; }
        [ForeignKey("UserId")]
        public virtual UserRegistration UserRegistration { get; set; }

        [ForeignKey("ClubId")]
        public virtual Club Club { get; set; }
    }
}
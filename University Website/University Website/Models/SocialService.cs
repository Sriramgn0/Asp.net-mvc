using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace University_Website.Models
{
    public class SocialService
    {
        [Key]
        public int SocialServiceId { get; set; }

        public DateTime EventDate { get; set; }

        public string  SocialServiceName { get; set; }

        public string Description { get; set; }

        public int NoOfVolunteer { get; set; }
    }
}
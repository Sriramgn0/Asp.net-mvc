using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace University_Website.Models
{
    public class Club
    {
        [Key]
        public int ClubId { get; set; }


        public string ClubName { get; set; }

        public string Details { get; set; }

        public string EligibleAge { get; set; }


    }
}
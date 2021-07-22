using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace University_Website.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public  int ClubId { get; set; }

        [ForeignKey("ClubId")]
        public virtual Club Clubs { get; set; }
    }
}
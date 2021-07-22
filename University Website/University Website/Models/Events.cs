using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace University_Website.Models
{
    public class Events
    {
        [Key]
        public int EventId { get; set; }

        public string EventName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please Provide Event Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please Provide Event End Date")]
        public DateTime EndDate { get; set; }

       
        public string status { get; set; }

        public string Description { get; set; }

        
        
    }
}
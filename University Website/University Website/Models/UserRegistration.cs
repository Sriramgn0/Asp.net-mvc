using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
namespace University_Website.Models
{
    public class UserRegistration
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please Provide First Name")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Please Provide Last Name")]
        public string Last_Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please Provide Date of Birth")]
        public DateTime DateOfBirth { get; set; }


        public gender Gender { get; set; }
        public enum gender
        {
            Male, Female, Other
        }

        [EmailAddress]
        [Required(ErrorMessage = "Please Provide Proper Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Provide Phone Number")]
        public string Contact_Number { get; set; }


        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{8,}", ErrorMessage = "Your password must be at least 8 characters long and contain at least 1 letter and 1 number")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Both Password and Confirm Password Must be Same")]
        public string ConfirmPassword { get; set; }

        // [Required(ErrorMessage = "Please select 1st security question")]
        public Questions Security_Question1 { get; set; }

      //  [Required(ErrorMessage = "Please Answer 1st Security Question")]
        public string Security_Answer1 { get; set; }


       // [Required(ErrorMessage = "Please select 2nd security question")]
        public Questions Security_Question2 { get; set; }

      //  [Required(ErrorMessage = "Please Answer 2nd Security Question")]
        public string Security_Answer2 { get; set; }



     //   [Required(ErrorMessage = "Please select 3rd security question")]
        public Questions Security_Question3 { get; set; }

     //   [Required(ErrorMessage = "Please Answer 3rd Security Question")]
        public string Security_Answer3 { get; set; }


        public enum Questions
        {
         

            [Display(Name = " What Is your favorite book?")]
           FavBook,

            [Display(Name = "What is the name of the road you grew up on?")]
           Road,

            [Display(Name = "What is your mother’s maiden name?")]
           MotherMaidenName,

            [Display(Name = "What was the name of your first/current/favorite pet?")]
           FavPet,

            [Display(Name = "What was the first company that you worked for?")]
           FirstCompany,

            [Display(Name = "Where did you meet your spouse?")]
           SpouseMeet,

            [Display(Name = "Where did you go to high school/college?")]
           HighSchoolLoc,

            [Display(Name = "What is your favorite food?,")]
           FavFood,

            [Display(Name = "What city were you born in?,")]
           PlaceOfBirth,

            [Display(Name = "Where is your favorite place to vacation?")]
           VacationPlace,           
        }

        public string UserType { get; set; }
    }

}
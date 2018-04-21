using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Models
{
    public class RecruiterModel
    {
        public int ID { get; set; }

        public string AccountType { get; set; }

        [Required(ErrorMessage = " First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = " Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = " Email is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Entered Email Address is not valid")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = " Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(10, ErrorMessage = "Entered Mobile Number is not Valid")]
        [Required(ErrorMessage = "Mobile Number is Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered Mobile Number format is not valid")]
        public string MobileNumber { get; set; }
        public string CurrentLocation { get; set; }
        public string CompanyName { get; set; }
        public string CurrentDesignation { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyState { get; set; }
        public string CompanyCountry { get; set; }
        public string ExperienceInHiring { get; set; }
        public string Industry { get; set; }
        public string Achivements { get; set; }
        public string CompanywebsiteLink { get; set; }
        public string NumberOfEmployees { get; set; }
        public string JobsPosted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Models
{
    public class JobSeekerModel
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

        public string CurrentLocation { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(10, ErrorMessage = "Entered Mobile Number is not Valid")]
        [Required(ErrorMessage = "Mobile Number is Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered Mobile Number format is not valid")]
        public string MobileNumber { get; set; }
        public string ShortBio { get; set; }
        public string TotalExperience { get; set; }
        public string KeySkills { get; set; }
        public string Industry { get; set; }
        public string JobRole { get; set; }
        public string JobLocation { get; set; }
        public string HighestQualification { get; set; }
        public string HighestQualificationSubject { get; set; }
        public string HighestQualificationCollege { get; set; }
        public DateTime HighestQualificationStartingTime { get; set; }
        public DateTime HighestQualificationCompletionTime { get; set; }
        public string Achivements { get; set; }
        public string LinkedInLink { get; set; }
        public string WebsiteLink { get; set; }
        public string ResumeLink { get; set; }
        public string AppliedJobs { get; set; }
    }
}

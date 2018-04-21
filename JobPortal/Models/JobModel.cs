using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Models
{
    public class JobModel
    {
        public int ID { get; set; }
        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string JobDescription { get; set; }

        [Required]
        public string PrimaryRole { get; set; }

        [Required]
        public string WorkExperience { get; set; }

        [Required]
        public string Skills { get; set; }

        [Required]
        public string JobLocation { get; set; }

        [Required]
        public double MinimumAnnualSalaryInRupees { get; set; }

        [Required]
        public double MaxuimumAnnualSalaryInRupees { get; set; }

        public string CompanyName { get; set; }

        public int RecruiterID { get; set; }
        public string JobSeekersID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Pages
{
    public class JobSeekerEditProfileModel : PageModel
    {
        [BindProperty]
        public JobSeekerModel CurrentJobSeeker { set; get; }

        private readonly JobSeekerDbContext _jobseekerContext;

        public JobSeekerEditProfileModel(JobSeekerDbContext context)
        {
            _jobseekerContext = context;
        }

        public async Task OnGetAsync()
        {
            if(User.Identity.IsAuthenticated)
            {
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                CurrentJobSeeker = await _jobseekerContext.JobSeekers.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                //industry field is being used for storing profile pic
                if (CurrentJobSeeker.Industry==null)
                {
                    CurrentJobSeeker.Industry = "images/profile-pic.png";
                }
                _jobseekerContext.JobSeekers.Update(CurrentJobSeeker);
                await _jobseekerContext.SaveChangesAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                var tempUser = await _jobseekerContext.JobSeekers.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                tempUser.Achivements = CurrentJobSeeker.Achivements;
                tempUser.CurrentLocation = CurrentJobSeeker.CurrentLocation;
                tempUser.HighestQualification = CurrentJobSeeker.HighestQualification;
                tempUser.HighestQualificationCollege = CurrentJobSeeker.HighestQualificationCollege;
                tempUser.HighestQualificationCompletionTime = CurrentJobSeeker.HighestQualificationCompletionTime;
                tempUser.HighestQualificationStartingTime = CurrentJobSeeker.HighestQualificationStartingTime;
                tempUser.HighestQualificationSubject = CurrentJobSeeker.HighestQualificationSubject;
                tempUser.JobLocation = CurrentJobSeeker.JobLocation;
                tempUser.JobRole = CurrentJobSeeker.JobRole;
                tempUser.KeySkills = CurrentJobSeeker.KeySkills;
                tempUser.LinkedInLink = CurrentJobSeeker.LinkedInLink;
                tempUser.ShortBio = CurrentJobSeeker.ShortBio;
                tempUser.TotalExperience = CurrentJobSeeker.TotalExperience;
                tempUser.WebsiteLink = CurrentJobSeeker.WebsiteLink;
                _jobseekerContext.JobSeekers.Update(tempUser);
                await _jobseekerContext.SaveChangesAsync();
                return RedirectToPage("/JobSeekerHomePage");
            }
            return Page();
        }

        
    }
}
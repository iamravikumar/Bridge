using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Pages
{
    public class JobProviderEditProfileModel : PageModel
    {
        [BindProperty]
        public RecruiterModel CurrentJobSeeker { set; get; }

        private readonly RecruiterDbContext _jobseekerContext;

        public JobProviderEditProfileModel(RecruiterDbContext context)
        {
            _jobseekerContext = context;
        }

        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                CurrentJobSeeker = await _jobseekerContext.Recruiters.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                var tempUser = await _jobseekerContext.Recruiters.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                tempUser.Achivements = CurrentJobSeeker.Achivements;
                tempUser.CurrentLocation = CurrentJobSeeker.CurrentLocation;
                tempUser.CompanyAddress = CurrentJobSeeker.CompanyAddress;
                tempUser.CompanyCountry = CurrentJobSeeker.CompanyCountry;
                tempUser.CompanyName = CurrentJobSeeker.CompanyName;
                tempUser.CompanyState = CurrentJobSeeker.CompanyState;
                tempUser.CompanywebsiteLink = CurrentJobSeeker.CompanywebsiteLink;
                tempUser.Industry = CurrentJobSeeker.Industry;
                tempUser.CurrentDesignation = CurrentJobSeeker.CurrentDesignation;
                tempUser.ExperienceInHiring = CurrentJobSeeker.ExperienceInHiring;
                tempUser.NumberOfEmployees = CurrentJobSeeker.NumberOfEmployees;
                
                _jobseekerContext.Recruiters.Update(tempUser);
                await _jobseekerContext.SaveChangesAsync();
                return RedirectToPage("/JobProviderHomePage");
            }
            return Page();
        }
    }
}
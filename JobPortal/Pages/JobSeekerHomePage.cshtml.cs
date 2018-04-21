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
    public class JobSeekerHomePageModel : PageModel
    {

        public JobSeekerModel CurrentJobSeeker { set; get; }
        public List<JobModel> AllJobs { set; get; }
        public List<JobModel> AppliedJobs { set; get; }

        private readonly JobSeekerDbContext _jobseekerContext;
        private readonly JobDbContext _jobContext;

        public JobSeekerHomePageModel(JobSeekerDbContext context1, JobDbContext context2)
        {
            _jobseekerContext = context1;
            _jobContext = context2;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                string AccountType = User.FindFirst(ClaimTypes.GivenName).Value;
                if (AccountType == "JobSeeker")
                {
                    string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                    CurrentJobSeeker = await _jobseekerContext.JobSeekers.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                    if (CurrentJobSeeker.AppliedJobs == null)
                    {
                        AppliedJobs = new List<JobModel>();
                    }
                    else
                    {
                        AppliedJobs = new List<JobModel>();
                        string[] jobIDs = CurrentJobSeeker.AppliedJobs.Split(',');
                        foreach (var item in jobIDs)
                        {
                            if (item == "")
                            {
                                continue;
                            }

                            var temp=await _jobContext.Jobs.SingleOrDefaultAsync(m => (m.ID == int.Parse(item)));
                            if(temp!=null)
                            {
                                AppliedJobs.Add(temp);
                            }
                        }
                    }
                    AllJobs = new List<JobModel>();
                    AllJobs = await _jobContext.Jobs.ToListAsync();
                    return Page();
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }
            return RedirectToPage("/Index");
        }
    }
}
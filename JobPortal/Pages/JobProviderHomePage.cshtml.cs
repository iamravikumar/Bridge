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
    public class JobProviderHomePageModel : PageModel
    {
        public RecruiterModel CurrentJobProvider { set; get; }
        public List<JobModel> JobsPostedByMe { set; get; }

        [BindProperty]
        public JobModel NewJob { set; get; }

        private readonly RecruiterDbContext _jobProviderContext;
        private readonly JobDbContext _jobContext;

        public JobProviderHomePageModel(RecruiterDbContext context1, JobDbContext context2)
        {
            _jobProviderContext = context1;
            _jobContext = context2;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                string AccountType = User.FindFirst(ClaimTypes.GivenName).Value;
                if (AccountType == "JobSeeker")
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                    CurrentJobProvider = await _jobProviderContext.Recruiters.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                    if (CurrentJobProvider.JobsPosted == null)
                    {
                        JobsPostedByMe = new List<JobModel>();
                    }
                    else
                    {
                        JobsPostedByMe = new List<JobModel>();
                        string[] jobIDs = CurrentJobProvider.JobsPosted.Split(',');
                        foreach (var item in jobIDs)
                        {
                            if(item=="")
                            {
                                continue;
                            }

                            var temp = await _jobContext.Jobs.SingleOrDefaultAsync(m => (m.ID == int.Parse(item)));
                            if (temp != null)
                            {
                                JobsPostedByMe.Add(temp);
                            }
                        }
                    }
                    return Page();
                }
            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    string AccountType = User.FindFirst(ClaimTypes.GivenName).Value;
                    if (AccountType == "JobSeeker")
                    {
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                        CurrentJobProvider = await _jobProviderContext.Recruiters.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                        NewJob.RecruiterID = CurrentJobProvider.ID;
                        await _jobContext.Jobs.AddAsync(NewJob);
                        await _jobContext.SaveChangesAsync();
                        if (CurrentJobProvider.JobsPosted == null)
                        {
                            CurrentJobProvider.JobsPosted = "," + NewJob.ID.ToString();
                        }
                        else
                        {
                            CurrentJobProvider.JobsPosted = CurrentJobProvider.JobsPosted + "," + NewJob.ID.ToString();
                        }
                        _jobProviderContext.Recruiters.Update(CurrentJobProvider);
                        await _jobProviderContext.SaveChangesAsync();
                        return RedirectToPage("/Index");
                    }
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }
            return Page();
        }
    }
}
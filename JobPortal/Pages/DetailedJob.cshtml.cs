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
    public class DetailedJobModel : PageModel
    {
        public bool HasApplied { set; get; }
        public bool ShowApply { set; get; }
        private readonly JobDbContext _jobContext;
        private readonly JobSeekerDbContext _jobSeekerContext;
        private readonly RecruiterDbContext _jobProviderContext;

        public JobModel CurrentJob { set; get; }
        public List<JobSeekerModel> Candidates { set; get; }

        [TempData]
        public int IDCurrent { set; get; }

        public DetailedJobModel(JobDbContext c1, JobSeekerDbContext c2, RecruiterDbContext c3)
        {
            _jobContext = c1;
            _jobSeekerContext = c2;
            _jobProviderContext = c3;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(User.Identity.IsAuthenticated)
            {
                IDCurrent = (int)id;
                Candidates = new List<JobSeekerModel>();
                string AccountType = User.FindFirst(ClaimTypes.GivenName).Value;
                if (AccountType == "JobSeeker")
                {
                    string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                    var temp = await _jobSeekerContext.JobSeekers.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                    var job = await _jobContext.Jobs.SingleOrDefaultAsync(m => (m.ID == id));
                    if(job==null)
                    {
                        return RedirectToPage("/Error");
                    }
                    else
                    {
                        CurrentJob = job;
                        ShowApply = true;
                        if(temp.AppliedJobs!=null)
                        {
                            string[] jobIDs = temp.AppliedJobs.Split(',');
                            foreach (var item in jobIDs)
                            {
                                if (item == "")
                                {
                                    continue;
                                }
                                if (int.Parse(item) == job.ID)
                                {
                                    HasApplied = true;
                                }
                            }
                        }
                        return Page();
                    }
                }
                else
                {
                    var job = await _jobContext.Jobs.SingleOrDefaultAsync(m => (m.ID == id));
                    if (job == null)
                    {
                        return RedirectToPage("/Error");
                    }
                    ShowApply = false;
                    CurrentJob = job;
                    if (job.JobSeekersID != null)
                    {
                        string[] jobIDs = job.JobSeekersID.Split(',');
                        foreach (var item in jobIDs)
                        {
                            if (item == "")
                            {
                                continue;
                            }
                            var temp2 =await _jobSeekerContext.JobSeekers.SingleOrDefaultAsync(m => (m.ID == int.Parse(item)));
                            if(temp2!=null)
                            {
                                Candidates.Add(temp2);
                            }
                        }
                    }
                    return Page();

                }
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //try
            {
                if(User.Identity.IsAuthenticated)
                {
                    string AccountType = User.FindFirst(ClaimTypes.GivenName).Value;
                    if (AccountType == "JobSeeker")
                    {
                        string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                        var temp = await _jobSeekerContext.JobSeekers.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                        var CurrentJob = await _jobContext.Jobs.SingleOrDefaultAsync(m => (m.ID == IDCurrent));
                        if (temp.AppliedJobs==null)
                        {
                            temp.AppliedJobs = "," + CurrentJob.ID.ToString();
                        }
                        else
                        {
                            temp.AppliedJobs = temp.AppliedJobs + "," + CurrentJob.ID.ToString();
                        }
                        if(CurrentJob.JobSeekersID==null)
                        {
                            CurrentJob.JobSeekersID = "," + temp.ID.ToString();
                        }
                        else
                        {
                            CurrentJob.JobSeekersID = CurrentJob.JobSeekersID + "," + temp.ID.ToString();
                        }
                        _jobContext.Jobs.Update(CurrentJob);
                        _jobSeekerContext.JobSeekers.Update(temp);
                        await _jobContext.SaveChangesAsync();
                        await _jobSeekerContext.SaveChangesAsync();
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        return Page();
                    }
                }
                else
                {
                    return RedirectToPage("Index");
                }
            }
            //catch(Exception)
            //{
            //    return RedirectToPage("/Error");
            //}
        }
    }
}
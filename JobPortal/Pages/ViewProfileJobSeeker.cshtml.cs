using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Pages
{
    public class ViewProfileJobSeekerModel : PageModel
    {

        private readonly JobSeekerDbContext _jobSeekerContext;
        public JobSeekerModel CurrentJobSeeker { set; get; }

        public ViewProfileJobSeekerModel(JobSeekerDbContext cqwe)
        {
            _jobSeekerContext = cqwe;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            CurrentJobSeeker = await _jobSeekerContext.JobSeekers.SingleOrDefaultAsync(m => (m.ID == id));
            if(CurrentJobSeeker ==null)
            {
                return RedirectToPage("Error");
            }
            else
            {
                return Page();
            }
        }
    }
}
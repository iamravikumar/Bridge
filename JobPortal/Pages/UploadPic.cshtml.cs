using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JobPortal.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Pages
{
    public class UploadPicModel : PageModel
    {
            private readonly JobSeekerDbContext _jobseekerContext;

            public UploadPicModel(JobSeekerDbContext cd)
            {
                _jobseekerContext = cd;
            }

            public async Task<IActionResult> OnPostAsync(IFormFile file)
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");

                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                var tempUser = await _jobseekerContext.JobSeekers.SingleOrDefaultAsync(m => (m.EmailID == emaiID));
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", tempUser.ID.ToString() + file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                tempUser.Industry = "images/" + tempUser.ID.ToString() + file.FileName;
                _jobseekerContext.JobSeekers.Update(tempUser);
                await _jobseekerContext.SaveChangesAsync();
                return RedirectToPage("/JobSeekerEditProfile");
            }
    }
}
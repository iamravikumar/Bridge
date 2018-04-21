using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Pages
{
    public class JobseekerSignupModel : PageModel
    {
        [BindProperty]
        public JobSeekerModel CurrentJobSeeker { set; get; }

        private readonly JobSeekerDbContext _jobseekerContext;

        public JobseekerSignupModel(JobSeekerDbContext context)
        {
            _jobseekerContext = context;
        }

        public async Task OnGetAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                CurrentJobSeeker.EmailID = CurrentJobSeeker.EmailID.ToLower();
                var JobSeekers = await _jobseekerContext.JobSeekers.ToListAsync();
                foreach (var item in JobSeekers)
                {
                    if (item.EmailID == CurrentJobSeeker.EmailID)
                    {
                        ModelState.AddModelError(string.Empty, "Email Id Already Registered");
                        return Page();
                    }
                }
                CurrentJobSeeker.AccountType = "JobSeeker";
                await _jobseekerContext.JobSeekers.AddAsync(CurrentJobSeeker);
                await _jobseekerContext.SaveChangesAsync();
                var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,CurrentJobSeeker.FirstName+ " "+CurrentJobSeeker.LastName),
                                new Claim(ClaimTypes.GivenName,"JobSeeker"),
                                new Claim(ClaimTypes.Email,CurrentJobSeeker.EmailID),
                            };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(1)
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToPage("/JobSeekerEditProfile");
            }
            else
            {
                return Page();
            }
        }
    }
}
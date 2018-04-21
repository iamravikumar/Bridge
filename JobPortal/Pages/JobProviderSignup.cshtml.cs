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
    public class JobProviderSignupModel : PageModel
    {

        [BindProperty]
        public RecruiterModel CurrentJobProvider { set; get; }

        private readonly RecruiterDbContext _jobProviderContext;

        public JobProviderSignupModel(RecruiterDbContext context)
        {
            _jobProviderContext = context;
        }

        public async Task OnGetAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                CurrentJobProvider.EmailID = CurrentJobProvider.EmailID.ToLower();
                var JobProviders = await _jobProviderContext.Recruiters.ToListAsync();
                foreach (var item in JobProviders)
                {
                    if (item.EmailID == CurrentJobProvider.EmailID)
                    {
                        ModelState.AddModelError(string.Empty, "Email Id Already Registered");
                        return Page();
                    }
                }
                CurrentJobProvider.AccountType = "JobProvider";
                await _jobProviderContext.Recruiters.AddAsync(CurrentJobProvider);
                await _jobProviderContext.SaveChangesAsync();
                var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,CurrentJobProvider.FirstName+ " "+CurrentJobProvider.LastName),
                                new Claim(ClaimTypes.GivenName,"JobProvider"),
                                new Claim(ClaimTypes.Email,CurrentJobProvider.EmailID),
                            };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(1)
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToPage("/JobProviderEditProfile");
            }
            else
            {
                return Page();
            }
        }
    }
}
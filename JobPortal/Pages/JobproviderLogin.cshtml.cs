using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JobPortal.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Pages
{
    public class JobproviderLoginModel : PageModel
    {
        public class InputModel
        {
            [Required]
            public string EmailId { get; set; }

            [Required]
            public string Password { get; set; }
        }

        [BindProperty]
        public InputModel LoginInput { set; get; }

        private readonly RecruiterDbContext _jobseekerContext;

        public JobproviderLoginModel(RecruiterDbContext context)
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
                LoginInput.EmailId = LoginInput.EmailId.ToLower();
                var JobSeekers = await _jobseekerContext.Recruiters.ToListAsync();
                foreach (var item in JobSeekers)
                {
                    if (item.EmailID == LoginInput.EmailId)
                    {
                        if (item.Password == LoginInput.Password)
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,item.FirstName + " "+item.LastName),
                                new Claim(ClaimTypes.GivenName,"JobProvider"),
                                new Claim(ClaimTypes.Email,item.EmailID),
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var authProperties = new AuthenticationProperties
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(1)
                            };
                            await HttpContext.SignInAsync(
                                    CookieAuthenticationDefaults.AuthenticationScheme,
                                    new ClaimsPrincipal(claimsIdentity),
                                    authProperties);
                            return RedirectToPage("/JobProviderHomePage");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid Login Attempt!");
                            return Page();
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt!");
                return Page();
            }
            else
            {
                return Page();
            }
        }
    }
}
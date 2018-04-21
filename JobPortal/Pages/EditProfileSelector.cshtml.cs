using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobPortal.Pages
{
    public class EditProfileSelectorModel : PageModel
    {
        public IActionResult OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                string AccountType = User.FindFirst(ClaimTypes.GivenName).Value;
                if(AccountType== "JobSeeker")
                {
                    return RedirectToPage("/JobSeekerEditProfile");
                }
                else
                {
                    return RedirectToPage("/JobProviderEditProfile");
                }
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}
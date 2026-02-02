using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthTest.Pages.Account
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public Credential Credential{get; set;} = new();
        public IActionResult OnGet()
        {
            if(User?.Identity?.IsAuthenticated == true) return RedirectToPage("/Index");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) return Page();
            if(Credential.Username == "admin" && Credential.Password == "admin")
            {
                var claims = new List<Claim> { 
                    new Claim(ClaimTypes.Name, "admin"), 
                    new Claim(ClaimTypes.Email, "admin@admin.com"),
                    new Claim("Role", "Administrator")
                    };
                var identity = new ClaimsIdentity(claims, "AuthCookie");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("AuthCookie", claimsPrincipal);
                return RedirectToPage("/Index");
            } else if(Credential.Username == "user" && Credential.Password == "user")
            {
                var claims = new List<Claim> { 
                    new Claim(ClaimTypes.Name, "user"), 
                    new Claim(ClaimTypes.Email, "user@user.com"),
                    new Claim("Role", "User")
                    };
                var identity = new ClaimsIdentity(claims, "AuthCookie");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("AuthCookie", claimsPrincipal);
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }


    public class Credential
    {
        [Required]
        public string Username {get; set;} = default!;
        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;} = default!;
    }
}
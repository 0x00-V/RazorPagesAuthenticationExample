using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthTest.Pages;

[Authorize(Policy = "AdminAccount")]
public class AdminPanel : PageModel
{
    public void OnGet()
    {

    }
}

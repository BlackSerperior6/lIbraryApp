using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages
{
    [Authorize]
    public class ControlPanelModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Authentication");

            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> EditReader()
        {

        }
    }
}

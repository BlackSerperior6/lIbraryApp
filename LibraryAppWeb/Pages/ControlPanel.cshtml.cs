using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages
{
    [Authorize]
    public class ControlPanelModel : PageModel
    {
        [BindProperty]
        public long RedactReaderId { get; set; }

        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet(string successMessage = null)
        {
            SuccessMessage = successMessage;
            return Page();
        } 

        public IActionResult OnPostRedactReader() => RedirectToPage("/Redact/Reader", new { id = RedactReaderId });
    }
}

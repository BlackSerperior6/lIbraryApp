using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelModel : PageModel
    {
        [BindProperty]
        public long RedactUserId { get; set; } = 1;

        [BindProperty]
        public long ViewUserId { get; set; } = 1;

        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet(string successMessage = null, string errorMessage = null)
        {
            SuccessMessage = successMessage;
            ErrorMessage = errorMessage;
            return Page();
        }

        public IActionResult OnPostRedactUser() => RedirectToPage("/Redact/Reader", new { id = RedactReaderId });
    }

}
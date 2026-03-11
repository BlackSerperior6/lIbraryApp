using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages
{
    [Authorize]
    public class ControlPanelModel : PageModel
    {
        [BindProperty]
        public long RedactReaderId { get; set; } = 1;

        [BindProperty]
        public long ViewReaderId { get; set; } = 1;

        [BindProperty]
        public long RedactBookId { get; set; } = 1;

        [BindProperty]
        public long ViewBookId { get; set; } = 1;

        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet(string successMessage = null, string errorMessage = null)
        {
            SuccessMessage = successMessage;
            ErrorMessage = errorMessage;
            return Page();
        } 

        public IActionResult OnPostRedactReader() => RedirectToPage("/Redact/Reader", new { id = RedactReaderId });

        public IActionResult OnPostRedactBook() => RedirectToPage("/Redact/Book", new { id = RedactBookId });

        public IActionResult OnPostViewReader() => RedirectToPage("/View/Reader", new { id = ViewReaderId });

        public IActionResult OnPostViewBook() => RedirectToPage("/View/Book", new { id = ViewBookId });
    }
}

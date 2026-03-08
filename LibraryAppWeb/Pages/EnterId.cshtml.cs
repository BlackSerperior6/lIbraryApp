using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages
{
    [Authorize]
    public class EnterIdModel : PageModel
    {
        [BindProperty]
        public string Id { get; set; }

        public string NextPage { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrWhiteSpace(NextPage))
                return RedirectToPage("/ControlPanel");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ulong.TryParse(Id, out var id))
            {
                ErrorMessage = $"{Id} не является числом!";
                return Page();
            }

            return RedirectToPage(NextPage, new { Id = id });
        }
    }
}

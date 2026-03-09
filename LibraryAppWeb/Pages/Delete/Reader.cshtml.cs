using LibraryAppWeb.Features;
using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.Delete
{
    [Authorize]
    public class ReaderModel : PageModel
    {
        [BindProperty]
        public long Id { get; set; }

        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;

            var result = await ReaderBaseHandler.RemoveReaderAsync(Id);

            if (!result.success)
            {
                ErrorMessage = $"Ошибка при выполнение запроса: {result.exception}";
                return Page();
            }

            if (result.affectedRows == 0)
            {
                ErrorMessage = $"Не существует читателя с id {Id}";
                return Page();
            }

            SuccessMessage = "Читатель был успешно удален!";
            return Page();
        }
    }
}

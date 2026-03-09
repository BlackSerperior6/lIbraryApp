using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.Delete
{
    public class BookModel : PageModel
    {
        [BindProperty]
        public ulong Id { get; set; }

        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;

            var result = await BookCatalogHandler.RemoveBookAsync(Id);

            if (!result.success)
            {
                ErrorMessage = $"Ошибка при выполнение запроса: {result.exception}";
                return Page();
            }

            if (result.affectedRow == 0)
            {
                ErrorMessage = $"Не существует книги с id {Id}";
                return Page();
            }

            SuccessMessage = "Книга была успешно удален!";
            return Page();
        }
    }
}

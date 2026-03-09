using LibraryAppWeb.Features;
using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.Create
{
    [Authorize]
    public class BookModel : PageModel
    {
        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Author { get; set; }

        [BindProperty]
        public DateTime ArrivalDate { get; set; } = DateTime.Now;

        [BindProperty]
        public DateTime ReleasedDate { get; set; } = DateTime.Now;

        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Author))
            {
                ErrorMessage = "Ни одно из полей не должно быть пустым!";
                return Page();
            }

            var book = new Book(Title, Author, ArrivalDate, ReleasedDate);

            var result = await BookCatalogHandler.AddBookAsync(book);

            if (!result.success)
            {
                ErrorMessage = $"Ошибка при выполнение запроса: {result.exception}";
                return Page();
            }

            SuccessMessage = "Книга была успешна добавлена!";
            return Page();
        }
    }
}

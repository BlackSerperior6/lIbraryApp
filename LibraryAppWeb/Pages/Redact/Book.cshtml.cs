using LibraryAppWeb.Features;
using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.Redact
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

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            var selectResult = await BookCatalogHandler.GetInfoAboutBookAsync(id);

            if (!selectResult.success)
                return RedirectToPage("/ControlPanel", new { errorMessage = $"Произошла ошибка во время выполнения запроса:\n{selectResult.exception}" });

            var dbReader = selectResult.reader;

            if (!await dbReader.ReadAsync())
            {
                await dbReader.CloseAsync();
                return RedirectToPage("/ControlPanel", new { errorMessage = "Книга с указанным id не найдена!" });
            }

            try
            {
                Title = dbReader.GetString(1);
                Author = dbReader.GetString(2);
                ReleasedDate = dbReader.GetDateTime(3);
                ArrivalDate = dbReader.GetDateTime(4);

                HttpContext.Session.SetString("BookId", id.ToString());
                HttpContext.Session.SetString("EntryVersion", dbReader.GetInt64(5).ToString());
            }
            catch (Exception ex)
            {
                return RedirectToPage("/ControlPanel", new { errorMessage = "Книга с указанным id не найдена!" });
            }

            await dbReader.CloseAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Author))
            {
                ErrorMessage = "Ни одно из полей не должно быть пустым!";
                return Page();
            }

            var bookIdString = HttpContext.Session.GetString("BookId");
            var entryVersionString = HttpContext.Session.GetString("EntryVersion");

            if (!long.TryParse(bookIdString, out var bookId) || !long.TryParse(entryVersionString, out var entryVersion))
            {
                ErrorMessage = "Не удалось получить данные из HTTP контекста!";
                return Page();
            }

            var currentBook = new Book(Title, Author, ArrivalDate, ReleasedDate);

            var updateResult = await BookCatalogHandler.UpdateBookAsync(bookId, entryVersion, currentBook);

            if (!updateResult.success)
            {
                ErrorMessage = $"Ошибка при выполнение запроса: {updateResult.exception}";
                return Page();
            }

            return RedirectToPage("/ControlPanel", new { successMessage = "Книга была успешно отредактированна!" });
        }
    }
}

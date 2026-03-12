using LibraryAppWeb.Handlers;
using LibraryAppWeb.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.ViewAll
{
    [Authorize]
    public class BookModel : PageModel
    {
        public Dictionary<long, Book> Books = 
        new Dictionary<long, Book>();

        public async Task<IActionResult> OnGetAsync()
        {
            var selectResult = await BookCatalogHandler.GetAllBooks();

            if (!selectResult.success)
                return RedirectToPage("/ControlPanel", new { errorMessage = $"Ошибка при выполнении запроса:\n{selectResult.exception}" });

            var dbReader = selectResult.reader;

            try
            {
                while (await dbReader.ReadAsync())
                {
                    var bookId = dbReader.GetInt64(0);
                    var title = dbReader.GetString(1);
                    var author = dbReader.GetString(2);
                    var releaseDate = dbReader.GetDateTime(3);
                    var arrivalDate = dbReader.GetDateTime(4);

                    var book = new Book(title, author, 
                    releaseDate, arrivalDate);

                    Books.Add(bookId, book);
                }

                await dbReader.CloseAsync();
                return Page();

            }
            catch (Exception ex)
            {
                await dbReader.CloseAsync();
                return RedirectToPage("/ControlPanel", new { errorMessage = $"Ошибка во время чтения дата базы:\n{ex}"});
            }
            
        }
    }
}
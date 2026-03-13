using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.View
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
                return RedirectToPage("/ControlPanel", new { errorMessage = $"Ошибка при выполнении запроса:\n{selectResult.exception}" });

            var dbReader = selectResult.reader;

            if (!await dbReader.ReadAsync())
            {
                await dbReader.CloseAsync();
                return RedirectToPage("/ControlPanel", new { errorMessage = "Не существует книги с таким id!" });
            }

            try
            {
                Title = dbReader.GetString(1);
                Author = dbReader.GetString(2);
                ReleasedDate = dbReader.GetDateTime(3);
                ArrivalDate = dbReader.GetDateTime(4);
            }
            catch (Exception ex)
            {
                await dbReader.CloseAsync();
                return RedirectToPage("/ControlPanel", new { errorMessage = $"������ ��� ������ �������:\n{ex}!" });
            }

            await dbReader.CloseAsync();

            return Page();
        }
    }
}

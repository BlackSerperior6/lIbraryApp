using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.View
{
    [Authorize]
    public class ReaderModel : PageModel
    {
        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string Patronymic { get; set; }

        [BindProperty]
        public DateTime IssuedDate { get; set; }

        [BindProperty]
        public DateTime Birthday { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var selectResult = await ReaderBaseHandler.GetInfoAboutReaderAsync(id);

            if (!selectResult.success)
                return RedirectToPage("/ControlPanel", new { errorMessage = $"Ошибка при выполнении запроса:\n{selectResult.exception}" });

            var dbReader = selectResult.reader;

            if (!await dbReader.ReadAsync())
            {
                await dbReader.CloseAsync();
                return RedirectToPage("/ControlPanel", new { errorMessage = "Читатель с таким id не найден!" });
            }

            try
            {
                LastName = dbReader.GetString(1);
                FirstName = dbReader.GetString(2);
                Patronymic = dbReader.GetString(3);
                IssuedDate = dbReader.GetDateTime(4);
                Birthday = dbReader.GetDateTime(5);
            }
            catch (Exception ex)
            {
                await dbReader.CloseAsync();
                return RedirectToPage("/ControlPanel", new { errorMessage = $"Ошибка при чтении датбазы:\n{ex}!" });
            }

            await dbReader.CloseAsync();

            return Page();
        }
    }
}

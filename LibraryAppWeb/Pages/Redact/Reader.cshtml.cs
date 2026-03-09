using LibraryAppWeb.Features;
using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.Redact
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

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var selectResult = await ReaderBaseHandler.GetInfoAboutReaderAsync(id);

            if (!selectResult.success)
            {
                ErrorMessage = $"Произошла ошибка во время выполнения запроса:\n{selectResult.exception}";
                return Page();
            }

            var dbReader = selectResult.reader;

            if (!await dbReader.ReadAsync())
            {
                ErrorMessage = "Читатель с указанным id не найден!";
                await dbReader.CloseAsync();
                return Page();
            }

            try
            {
                LastName = dbReader.GetString(1);
                FirstName = dbReader.GetString(2);
                Patronymic = dbReader.GetString(3);
                IssuedDate = dbReader.GetDateTime(4);
                Birthday = dbReader.GetDateTime(5);

                HttpContext.Session.SetString("ReaderId", id.ToString());
                HttpContext.Session.SetString("EntryVersion", dbReader.GetInt64(6).ToString());
            }
            catch (Exception ex) 
            {
                ErrorMessage = "Произошла ошибка при переходе на страницу" + ex.ToString();
            }

            await dbReader.CloseAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(Patronymic))
            {
                ErrorMessage = "Ни одно из полей не должно быть пустым!";
                return Page();
            }

            var readerIdString = HttpContext.Session.GetString("ReaderId");
            var entryVersionString = HttpContext.Session.GetString("EntryVersion");

            if (!long.TryParse(readerIdString, out var readerId) || !long.TryParse(entryVersionString, out var entryVersion))
            {
                ErrorMessage = "Не удалось получить данные из HTTP контекста!";
                return Page();
            }

            var currentReader = new Reader(LastName, FirstName, Patronymic, IssuedDate, Birthday);

            var updateResult = await ReaderBaseHandler.UpdateReaderAsync(readerId, entryVersion, currentReader);

            if (!updateResult.success)
            {
                ErrorMessage = $"Ошибка при выполнение запроса: {updateResult.exception}";
                return Page();
            }

            return RedirectToPage("/ControlPanel", new { successMessage = "Читатель был успешно отредактирован!" });
        }
    }
}

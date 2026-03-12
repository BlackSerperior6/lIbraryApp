using LibraryAppWeb.Features;
using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.Redact
{
    [Authorize(Roles = "Admin")]
    public class UserModel : PageModel
    {
        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; } = "";

        [BindProperty]
        public string Role { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            var selectResult = await DatabaseUsersHandler.GetInfoAboutUserAsync(id);

            if (!selectResult.success)
                return RedirectToPage("/AdminPanel", new { errorMessage = $"Произошла ошибка во время выполнения запроса:\n{selectResult.exception}" });

            var dbReader = selectResult.reader;

            if (!await dbReader.ReadAsync())
            {
                await dbReader.CloseAsync();
                return RedirectToPage("/AdminPanel", new { errorMessage = "Пользователь с указанным id не найден!!" });
            }

            try
            {
                Login = dbReader.GetString(1);
                Role = dbReader.GetString(3);

                HttpContext.Session.SetString("UserId", id.ToString());
                HttpContext.Session.SetString("EntryVersion", dbReader.GetInt64(4).ToString());
            }
            catch (Exception ex)
            {
                return RedirectToPage("/AdminPanel", new { errorMessage = $"Произошла ошибца при чтении дата базы:\n{ex}" });
            }

            await dbReader.CloseAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Role))
            {
                ErrorMessage = "Ни одно из полей не должно быть пустым!";
                return Page();
            }

            var userIdString = HttpContext.Session.GetString("UserId");
            var entryVersionString = HttpContext.Session.GetString("EntryVersion");

            if (!long.TryParse(userIdString, out var userId) || !long.TryParse(entryVersionString, out var entryVersion))
            {
                ErrorMessage = "Не удалось получить данные из HTTP контекста!";
                return Page();
            }

            var redactedUser = new DatabaseUser(Login, Password, Role);

            var updateResult = await DatabaseUsersHandler.UpdateUserAsync(userId, entryVersion, redactedUser);

            if (!updateResult.success)
            {
                ErrorMessage = $"Ошибка при выполнение запроса: {updateResult.exception}";
                return Page();
            }

            return RedirectToPage("/AdminPanel", new { successMessage = "Пользователь был успешно отредактирован!" });
        }
    }
}

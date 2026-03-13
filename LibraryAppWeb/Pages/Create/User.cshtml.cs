using LibraryAppWeb.Features;
using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace LibraryAppWeb.Pages.Create
{
    [Authorize(Roles = "Admin")]
    public class UserModel : PageModel
    {
        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Role { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Role))
            {
                ErrorMessage = "Ни одно из полей не должно быть пустым!";
                return Page();
            }

            var user = new DatabaseUser(Login, Password, Role);

            var result = await DatabaseUsersHandler.AddUserAsync(user);

            if (!result.success)
            {
                if (result.exception.SqlState == PostgresErrorCodes.UniqueViolation)
                    ErrorMessage = $"Ошибка! Пользователь с таким логином уже существует!";
                else
                    ErrorMessage = $"Ошибка при выполнение запроса: {result.exception}";

                return Page();
            }

            return RedirectToPage("/AdminPanel", new { successMessage = "Пользователь был успешно добавлен!" });
        }
    }
}
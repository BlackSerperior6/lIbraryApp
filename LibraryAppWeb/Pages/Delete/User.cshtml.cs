using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.Delete
{
    [Authorize(Roles = "Admin")]
    public class UserModel : PageModel
    {
        [BindProperty]
        public long Id { get; set; } = 1;

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await DatabaseUsersHandler.RemoveUserAsync(Id);

            if (!result.success)
            {
                ErrorMessage = $"Ошибка при выполнение запроса: {result.exception}";
                return Page();
            }

            if (result.affectedRows == 0)
            {
                ErrorMessage = $"Не существует пользователя с id {Id}";
                return Page();
            }

            return RedirectToPage("/AdminPanel", new { successMessage = "Пользователь был успешно удален!" });
        }
    }
}

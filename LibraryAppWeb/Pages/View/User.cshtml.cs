using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.View
{
    [Authorize(Roles = "Admin")]
    public class UserModel : PageModel
    {
        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Role {  get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var selectResult = await DatabaseUsersHandler.GetInfoAboutUserAsync(id);

            if (!selectResult.success)
                return RedirectToPage("/AdminPanel", new { errorMessage = $"Ошибка при выполнении запроса:\n{selectResult.exception}" });

            var dbReader = selectResult.reader;

            if (!await dbReader.ReadAsync())
            {
                await dbReader.CloseAsync();
                return RedirectToPage("/AdminPanel", new { errorMessage = "Не существует пользователя с таким id!" });
            }

            try
            {
                Login = dbReader.GetString(1);
                Role = dbReader.GetString(3);
            }
            catch (Exception ex)
            {
                await dbReader.CloseAsync();
                return RedirectToPage("/AdminPanel", new { errorMessage = $"Ошибка при чтении дата базы:\n{ex}" });
            }

            await dbReader.CloseAsync();

            return Page();
        }
    }
}

using LibraryAppWeb.Features;
using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.ViewAll
{
    [Authorize(Roles = "Admin")]
    public class UserModel : PageModel
    {
        public Dictionary<long, DatabaseUser> Users =
        new Dictionary<long, DatabaseUser>();

        public async Task<IActionResult> OnGetAsync()
        {
            var selectResult = await DatabaseUsersHandler.GetAllUsers();

            if (!selectResult.success)
                return RedirectToPage("/AdminPanel", new { errorMessage = $"Ошибка при выполнении запроса:\n{selectResult.exception}" });

            var dbReader = selectResult.reader;

            try
            {
                while (await dbReader.ReadAsync())
                {
                    var userId = dbReader.GetInt64(0);
                    var login = dbReader.GetString(1);
                    var role = dbReader.GetString(3);

                    var user = new DatabaseUser(login, "", role);

                    Users.Add(userId, user);
                }

                await dbReader.CloseAsync();
                return Page();

            }
            catch (Exception ex)
            {
                await dbReader.CloseAsync();
                return RedirectToPage("/AdminPanel", new { errorMessage = $"Ошибка во время чтения дата базы:\n{ex}" });
            }

        }
    }
}

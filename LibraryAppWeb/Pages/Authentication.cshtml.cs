using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using System.Threading.Tasks;

namespace LibraryAppWeb.Pages
{
    public class AuthenticationModel : PageModel
    {
        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var connection = new NpgsqlConnection("aaa");

            string connectionString = $"Server=localhost;Port=5432;Database=Library;" +
                $"User Id={Login};Password={Password};";

            if (await DataBaseConnectionFactory.ConnectAsync(connectionString))
            {
                // Store login info in session
                HttpContext.Session.SetString("Username", Login);
                return RedirectToPage("/Index");
            }

            ErrorMessage = "Неверный логин или пароль";
            return Page();
        }
    }
}

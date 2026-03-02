using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using System.Threading.Tasks;
using BCrypt.Net;
using NpgsqlTypes;
using Dapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

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
            string query = $"SELECT \"PasswordHash\", \"PasswordSalt\", \"Role\" from \"DBUsers\" WHERE 'Login' = @login;";

            try
            {
                await using var connection = await DataBaseConnectionFactory.CreateConnection();

                var user = await connection.QueryFirstOrDefaultAsync<(string passwordHash, string passwordSalt, string role)>(query, new { login = Login });

                if (!string.IsNullOrWhiteSpace(user.passwordSalt))
                {
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password + user.passwordSalt);

                    bool isValid = BCrypt.Net.BCrypt.Verify(Password, hashedPassword);

                    if (isValid)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, Login),
                            new Claim(ClaimTypes.Role, user.role)
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies")));

                        return RedirectToPage("OneDay, lil bro:)");
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage = $"Произошла ошибка:\n{e}. Пожалуйста, обратитесь к администратору!";
            }

            ErrorMessage = "Неверный логин или пароль!";
            return Page();
        }
    }
}

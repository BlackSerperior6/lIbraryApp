using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Dapper;

namespace LibraryAppWeb.Pages
{
    public class AuthenticationModel : PageModel
    {
        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            string query = $"SELECT \"PasswordHash\", \"Role\" from \"DatabaseUsers\" WHERE \"Login\" = @login;";

            try
            {
                await using var connection = DataBaseConnectionFactory.CreateConnection();

                var user = await connection.QueryFirstOrDefaultAsync
                    <(string passwordHash, string role)>(query, new { login = Login });

                if (!string.IsNullOrWhiteSpace(user.passwordHash))
                {
                    bool isValid = BCrypt.Net.BCrypt.Verify(Password, user.passwordHash);

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

                        var returnUrl = Request.Query["returnUrl"].ToString();

                        if (!string.IsNullOrWhiteSpace(returnUrl))
                            return Redirect(returnUrl);
                        else
                            return RedirectToPage("/ControlPanel");
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage = $"Произошла ошибка:\n{e}.\nПожалуйста, обратитесь к администратору!";
            }

            ErrorMessage = "Неверный логин или пароль!";
            return Page();
        }
    }
}

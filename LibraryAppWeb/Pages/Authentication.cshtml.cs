using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public async Task<IActionResult> OnPostAsync()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Login),
                new Claim(ClaimTypes.Role, "testAdmin")
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies")));

            var returnUrl = Request.Query["returnUrl"].ToString();

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToPage("/ControlPanel");

            /*string query = $"SELECT \"PasswordHash\", \"PasswordSalt\", \"Role\" from \"DBUsers\" WHERE 'Login' = @login;";

            try
            {
                bool adminOvverideFlag = true;

                await using var connection = await DataBaseConnectionFactory.CreateConnection();

                var user = await connection.QueryFirstOrDefaultAsync
                    <(string passwordHash, string passwordSalt, string role)>(query, new { login = Login });

                if (!string.IsNullOrWhiteSpace(user.passwordSalt) || adminOvverideFlag)
                {
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password + user.passwordSalt);

                    bool isValid = BCrypt.Net.BCrypt.Verify(Password, hashedPassword);

                    if (isValid || adminOvverideFlag)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, Login),
                            new Claim(ClaimTypes.Role, user.role)
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies")));

                        if (ReturnUrl != null)
                            return Redirect(ReturnUrl);
                        else
                            return RedirectToPage("/ControlPanel");
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage = $"Произошла ошибка:\n{e}. Пожалуйста, обратитесь к администратору!";
            }

            ErrorMessage = "Неверный логин или пароль!";
            return Page();*/
        }
    }
}

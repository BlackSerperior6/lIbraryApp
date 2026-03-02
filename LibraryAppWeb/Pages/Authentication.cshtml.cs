using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using System.Threading.Tasks;
using BCrypt.Net;

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
            string saltQuery = $"SELECT \"saltHash\" from \"DBUsers\" WHERE 'Login' = @login;";

            try
            {
                await using var connection = await IDbConnectionFactory.CreateConnection();
                await using var command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("@login", NpgsqlDbType.Text, Login);

                var reader = await command.ExecuteReaderAsync();

                await reader.ReadAsync();

                if (reader.GEtTe(0) != 0)
                {
                    await reader.CloseAsync();
                    return (false, null);
                }

                await reader.CloseAsync();

            }
            catch (NpgsqlException e)
            {
                ErrorMessage = e.Text;
            }

            try
            {
                await using var connection = await IDbConnectionFactory.CreateConnection();
                await using var command = new NpgsqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@lastName", NpgsqlDbType.Text, reader.LastName);
                command.Parameters.AddWithValue("@firstName", NpgsqlDbType.Text, reader.FirstName);
                command.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Text, reader.Patronymic);
                command.Parameters.AddWithValue("@issuedDate", NpgsqlDbType.Date, reader.IssuedDate);
                command.Parameters.AddWithValue("@birthDate", NpgsqlDbType.Date, reader.BirthDate);
                
                await command.ExecuteNonQueryAsync();
                return (true, null);
            }
            catch (NpgsqlException e)
            {
                return (false, e);
            }

            ErrorMessage = "�������� ����� ��� ������";
            return Page();
        }
    }
}

using LibraryAppWeb.Features;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace LibraryAppWeb.Handlers;

public static class DatabaseUsersHandler
{
    public static async Task<(bool success, NpgsqlException exception)> AddUserAsync(DatabaseUser user)
    {
        string query = $"INSERT INTO \"DatabaseUsers\" (\"UserId\", \"Login\", \"PasswordHash\", " +
                       $"\"Role\", \"version\") " +
                       $"VALUES (DEFAULT, @login, @passwordHash, " +
                       $"@role, DEFAULT);";
        
        try
        {
            await using var connection = DataBaseConnectionFactory.CreateConnection();
            await connection.OpenAsync();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@login", NpgsqlDbType.Text, user.Login);
            command.Parameters.AddWithValue("@passwordHash", NpgsqlDbType.Text, hashedPassword);
            command.Parameters.AddWithValue("@role", NpgsqlDbType.Text, user.Role);

            await command.ExecuteNonQueryAsync();
            return (true, null);

        }
        catch (NpgsqlException e)
        {
            return (false, e);
        }
    }

    public static async Task<(bool success, NpgsqlException exception, int affectedRows)> RemoveUserAsync(long id)
    {
        string query = $"DELETE FROM \"DatabaseUsers\" WHERE \"UserId\" = @id;";
        
        try
        {
            await using var connection = DataBaseConnectionFactory.CreateConnection();
            await connection.OpenAsync();

            await using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Bigint, (long) id);
            
            var affectedRows = await command.ExecuteNonQueryAsync();
            return (true, null, affectedRows);
        }
        catch (NpgsqlException e)
        {
            return (false, e, 0);
        }
    }

    public static async Task<(bool success, NpgsqlException exception, NpgsqlDataReader reader)> GetInfoAboutUserAsync(long id)
    {
        string query = $"SELECT * FROM \"DatabaseUsers\" WHERE \"UserId\" = @id;";

        try
        {
            var connection = DataBaseConnectionFactory.CreateConnection();
            await connection.OpenAsync();

            await using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Bigint, id);

            var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            return (true, null, reader);
        }
        catch (NpgsqlException e) 
        {
            return (false, e, null);
        }
    }

    public static async Task<(bool success, NpgsqlException exception)> UpdateUserAsync(long id, long currentVersion, DatabaseUser updatedUser)
    {
        string query = $"UPDATE \"DatabaseUsers\" Set \"Login\" = @login, " +
                       $"\"PasswordHash\" = @passwordHash" +
                       $", \"Role\" = @role, " +
                       $"\"version\" = '{currentVersion + 1}'" + 
                       $"WHERE \"UserId\" = @id AND \"version\" = '{currentVersion}';";

        try
        {
            await using var connection = DataBaseConnectionFactory.CreateConnection();
            await connection.OpenAsync();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);

            await using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@login", NpgsqlDbType.Text, updatedUser.Login);
            command.Parameters.AddWithValue("@passwordHash", NpgsqlDbType.Text, hashedPassword);
            command.Parameters.AddWithValue("@role", NpgsqlDbType.Text, updatedUser.Role);
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Bigint, id);

            var updated = await command.ExecuteNonQueryAsync();

            if (updated == 0)
                throw new NpgsqlException("Запись данного пользователя была изменена или обновлена за время вашей работы. Пожалуйста, начните процесс сначала!");

            return (true, null);
        }
        catch (NpgsqlException e)
        {
            return (false, e);
        }
    }

    public static async Task<(bool success, NpgsqlException exception, NpgsqlDataReader reader)> GetAllUsers()
    {
        string query = "SELECT * FROM \"DatabaseUsers\" ORDER BY \"UserId\" ASC";

        try
        {
            var connection = DataBaseConnectionFactory.CreateConnection();
            await connection.OpenAsync();

            await using var command = new NpgsqlCommand(query, connection);

            var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            return (true, null, reader);
        }
        catch (NpgsqlException e)
        {
            return (false, e, null);
        }

    }

}
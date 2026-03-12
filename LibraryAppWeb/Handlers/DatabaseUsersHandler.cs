using LibraryAppWeb.Features;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace LibraryAppWeb.Handlers

public static class DatabaseUsersHandler
{
    public static async Task<(bool success, NpgsqlException exception)> 
    AddUserAsync(DatabaseUser user)
    {
        string query = $"INSERT INTO \"DatabaseUsers\" (\"UserId\", \"Login\", \"PasswordHash\", " +
                       $"\"PasswordSalt\", \"Role\", \"version\") " +
                       $"VALUES (DEFAULT, @login, @passwordHash, @passwordSalt, " +
                       $"@role, DEFAULT);";
        
        try
        {
            await using var connection = DataBaseConnectionFactory.CreateConnection();
            await connection.OpenAsync();

            var passwordSalt = bcrypt.genSalt(5);

            string hashedPassword = bcrypt.Hash(user.Password, passwordSalt);

            await using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@login", NpgsqlDbType.Text, user.Login);
            command.Parameters.AddWithValue("@passwordHash", NpgsqlDbType.Text, hashedPassword);
            command.Parameters.AddWithValue("@passwordSalt", NpgsqlDbType.Text, passwordSalt);
            command.Parameters.AddWithValue("@role", NpgsqlDbType.Date, user.Role);

            await command.ExecuteNonQueryAsync();
            return (true, null);

        }
        catch (NpgsqlException e)
        {
            return (false, e);
        }


    }

    public static async Task<(bool success, NpgsqlException exception, int affectedRows)> 
    RemoveUserAsync(long id)
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

    public static async Task<(bool success, NpgsqlException exception, NpgsqlDataReader reader)> 
    GetInfoAboutUsserAsync(long id)
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

    public static async Task<(bool success, NpgsqlException exception)> UpdateUSerAsync(long id, 
    long currentVersion, DatabaseUser updatedUser)
    {
        string query = $"UPDATE \"DatabaseUsers\" Set \"Login\" = @login, " +
                       $"\"PasswordHash\" = @passwordHash, \"PasswordSalt\" = @passwordSalt" +
                       $", \"Role\" = @role, " +
                       $"\"version\" = '{currentVersion + 1}'" + 
                       $"WHERE \"UserId\" = @id AND \"version\" = '{currentVersion}';";

        try
        {
            await using var connection = DataBaseConnectionFactory.CreateConnection();
            await connection.OpenAsync();

            var passwordSalt = bcrypt.genSalt(5);

            string hashedPassword = bcrypt.Hash(updatedUser.Password, passwordSalt);

            await using var command = new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@login", NpgsqlDbType.Text, user.Login);
            command.Parameters.AddWithValue("@passwordHash", NpgsqlDbType.Text, hashedPassword);
            command.Parameters.AddWithValue("@passwordSalt", NpgsqlDbType.Text, passwordSalt);
            command.Parameters.AddWithValue("@role", NpgsqlDbType.Date, user.Role);

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

}
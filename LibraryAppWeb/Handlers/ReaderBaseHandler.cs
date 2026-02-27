using LibraryAppWeb.Features;
using Npgsql;
using NpgsqlTypes;

namespace LibraryAppWeb.Handlers;

public static class ReaderBaseHandler
{
    public static async Task<(bool success, NpgsqlException exception)> AddReaderAsync(Reader reader)
    {
        string query = $"INSERT INTO \"ReaderBase\" (\"ReaderID\", \"Last Name\", \"First Name\", " +
                       $"\"Patronymic\", \"Issued Date\", \"Birth Date\") " +
                       $"VALUES (DEFAULT, @lastName, @firstName, @patronymic, " +
                       $"@issuedDate, @birthDate, DEFAULT);";

        try
        {
            await using var command = new NpgsqlCommand(query, DataBaseClient.CurrentConnection);
            
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
    }

    public static async Task<(bool success, NpgsqlException exceptionm, int affectedRows)> RemoveReaderAsync(ulong id)
    {
        string query = $"DELETE FROM \"ReaderBase\" WHERE \"ReaderID\" = @id;";
        
        try
        {
            await using var command = new NpgsqlCommand(query, DataBaseClient.CurrentConnection);
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Bigint, id);
            
            var affectedRows = await command.ExecuteNonQueryAsync();
            return (true, null, affectedRows);
        }
        catch (NpgsqlException e)
        {
            return (false, e, 0);
        }
    }

    public static async Task<(bool success, NpgsqlException exception, NpgsqlDataReader reader)> GetInfoAboutReaderAsync(ulong id)
    {
        string query = $"SELECT * FROM \"ReaderBase\" WHERE \"ReaderID\" = @id;";

        try
        {
            await using var command = new NpgsqlCommand(query, DataBaseClient.CurrentConnection);
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Bigint, id);

            var reader = await command.ExecuteReaderAsync();
            return (true, null, reader);
        }
        catch (NpgsqlException e) 
        {
            return (false, e, null);
        }
    }

    public static async Task<(bool success, NpgsqlException exception)> UpdateReaderAsync(ulong id, int expectedVersion, Reader updatedReader)
    {
        string query = $"UPDATE \"ReaderBase\" Set \"Last Name\" = @lastName, " +
                       $"\"First Name\" = @firstName, \"Patronymic\" = @patronymic" +
                       $", \"Issued Date\" = @issuedDate, \"Birth Date\" = @birthDate " +
                       $"\"version\" = '{expectedVersion + 1}'" + 
                       $"WHERE \"ReaderID\" = @id AND \"version\" = '{expectedVersion}';";

        try
        {
            await using var command = new NpgsqlCommand(query, DataBaseClient.CurrentConnection);

            command.Parameters.AddWithValue("@lastName", NpgsqlDbType.Text, updatedReader.LastName);
            command.Parameters.AddWithValue("@firstName", NpgsqlDbType.Text, updatedReader.FirstName);
            command.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Text, updatedReader.Patronymic);
            command.Parameters.AddWithValue("@issuedDate", NpgsqlDbType.Date, updatedReader.IssuedDate);
            command.Parameters.AddWithValue("@birthDate", NpgsqlDbType.Date, updatedReader.BirthDate);
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Bigint, id);

            var updated = await command.ExecuteNonQueryAsync();

            if (updated == 0)
                throw new NpgsqlException("Данная запись была удалена, либо обновлена во время процесса. Пожалуйста, попробуйте еще раз!");

            return (true, null);
        }
        catch (NpgsqlException e)
        {
            return (false, e);
        }
    }
}
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
                       $"VALUES (DEFAULT, '@lastName', '@firstName', '@patronymic', " +
                       $"'@issuedDate', '@birthDate') FOR UPDATE;";

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
        string query = $"DELETE FROM \"ReaderBase\" WHERE \"ReaderID\" = '{id}';";
        
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
        string query = $"SELECT * FROM \"ReaderBase\" WHERE \"ReaderID\" = '{id}'";

        var result = await DataBaseClient.ExecuteSelectAsync(query);
        return (result.Success, result.Exception, result.reader);
    }

    public static async Task<(bool success, NpgsqlException exception)> UpdateReaderAsync(ulong id, Reader updatedReader)
    {
        string query = $"UPDATE \"ReaderBase\" Set \"Last Name\" = '{updatedReader.LastName}', " +
                       $"\"First Name\" = '{updatedReader.FirstName}', \"Patronymic\" = '{updatedReader.Patronymic}'" +
                       $", \"Issued Date\" = '{updatedReader.IssuedDate}', \"Birth Date\" = '{updatedReader.BirthDate}' " +
                       $"WHERE \"ReaderID\" = '{id}'";
        
        var result = await DataBaseClient.ExecuteInsertUpdateDeleteAsync(query);

        return (result.Success, result.Exception);
    }
}
using LibraryAppWeb.Features;
using Npgsql;

namespace LibraryAppWeb.Handlers;

public static class ReaderBaseHandler
{
    public static async Task<(bool success, NpgsqlException exception)> AddReaderAsync(Reader reader)
    {
        string query = $"INSERT INTO \"ReaderBase\" (\"ReaderID\", \"Last Name\", \"First Name\", " +
                       $"\"Patronymic\", \"Issued Date\", \"Birth Date\") " +
                       $"VALUES (DEFAULT, '{reader.LastName}', '{reader.FirstName}', '{reader.Patronymic}', " +
                       $"'{reader.IssuedDate}', '{reader.BirthDate}');";

        var result = await DataBaseClient.ExecuteInsertUpdateDeleteAsync(query);
        return (result.Success, result.Exception);
    }

    public static async Task<(bool success, NpgsqlException exceptionm, int affectedRows)> RemoveReaderAsync(ulong id)
    {
        string query = $"DELETE FROM \"ReaderBase\" WHERE \"ReaderID\" = '{id}'";
         
        var result = await DataBaseClient.ExecuteInsertUpdateDeleteAsync(query);
        return (result.Success, result.Exception, result.AffectedRows);
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
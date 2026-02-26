using Npgsql;

namespace LibraryAppWeb;

public class DataBaseClient
{
    private static NpgsqlConnection CurrentConnection;

    public static async Task<bool> ConnectAsync(string connectionString)
    {
        try
        {
            CurrentConnection = new NpgsqlConnection(connectionString);
            await CurrentConnection.OpenAsync();
            return true;
        }
        catch (NpgsqlException ex)
        {
            return false;
        }

    }

    public static async Task<(bool Success, NpgsqlException Exception, int AffectedRows)> ExecuteInsertUpdateDeleteAsync(string query)
    {
        try
        {
            await using var command = new NpgsqlCommand(query, CurrentConnection);
            int affectedRows = await command.ExecuteNonQueryAsync();
            return (true, null, affectedRows);
        }
        catch (NpgsqlException ex)
        {
            return (false, ex, 0);
        }
    }

    public static async Task<(bool Success, NpgsqlException Exception, NpgsqlDataReader reader)> ExecuteSelectAsync(string query)
    {
        try
        {
            await using var command = new NpgsqlCommand(query, CurrentConnection);
            var reader = await command.ExecuteReaderAsync();
            return (true, null, reader);
        }
        catch (NpgsqlException ex)
        {
            return (false, ex, null);
        }
    }
}
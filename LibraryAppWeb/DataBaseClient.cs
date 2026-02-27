using Npgsql;

namespace LibraryAppWeb;

public class DataBaseClient
{
    public static NpgsqlConnection CurrentConnection;

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
}
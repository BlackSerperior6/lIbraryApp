using Npgsql;

namespace LibraryAppWeb;

public static class DataBaseConnectionFactory
{
    private static string _connectionString;

    public static void Init(IConfiguration configuration)
    {
        var baseConnectionString = configuration.GetConnectionString("DefaultConnection");

        var dbPassword = Environment.GetEnvironmentVariable("DP_LIB_PASSWORD");

        if (string.IsNullOrEmpty(dbPassword))
        {
            throw new InvalidOperationException(
                "DP_LIB_PASSWORD environment variable is not set. " +
                "Please set it before running the application."
            );
        }

        _connectionString = $"{baseConnectionString};Password={dbPassword};";
    }

    public static async Task<NpgsqlConnection> CreateConnection() 
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
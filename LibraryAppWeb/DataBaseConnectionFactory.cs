using LibraryAppWeb.Interfaces;
using Npgsql;

namespace LibraryAppWeb;

public class DataBaseConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DataBaseConnectionFactory(IConfiguration configuration)
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

    public NpgsqlConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}
using Npgsql;

namespace LibraryAppWeb.Interfaces
{
    public interface IDbConnectionFactory
    {
        public NpgsqlConnection CreateConnection();
    }
}

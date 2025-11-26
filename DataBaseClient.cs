using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication
{
    public static class DataBaseClient
    {
        private static NpgsqlConnection CurrentConnection;

        public static bool Connect(string connetionString)
        {
            try
            {
                CurrentConnection = new NpgsqlConnection(connetionString);
                CurrentConnection.Open();
                return true;
            }
            catch (NpgsqlException ex)
            {
                return false;
            }

        }

        public static bool ExecuteInsertOrUpdate(string query, out NpgsqlException exception)
        {
            exception = null;

            try
            {
                var command = new NpgsqlCommand(query, CurrentConnection);
                command.ExecuteScalar();
                return true;
            }
            catch (NpgsqlException ex) 
            { 
                exception = ex;
                return false;
            }
        }

        public static bool ExecuteDelete(string query, out NpgsqlException exception, out int affectedRows)
        {
            exception = null;
            affectedRows = 0;

            try
            {
                var command = new NpgsqlCommand(query, CurrentConnection);
                affectedRows = command.ExecuteNonQuery();
                return true;
            }
            catch (NpgsqlException ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool ExecuteSelect(string query, out NpgsqlException exception, out NpgsqlDataReader reader)
        {
            exception = null;
            reader = null;

            try
            {
                var command = new NpgsqlCommand(query, CurrentConnection);
                reader = command.ExecuteReader();
                return true;
            }
            catch (NpgsqlException ex)
            {
                exception = ex;
                return false;
            }
        }
    }
}

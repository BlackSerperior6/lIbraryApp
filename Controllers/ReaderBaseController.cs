using LibraryApplication.Structs;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.Controllers
{
    public static class ReaderBaseController
    {
        public static bool AddReader(Reader reader, out NpgsqlException exception)
        {
            string query = $"INSERT INTO \"ReaderBase\" (\"ReaderID\", \"Last Name\", \"First Name\", " +
                            $"\"Patronymic\", \"Issued Date\", \"Birth Date\") " +
            $"VALUES (DEFAULT, '{reader.LastName}', '{reader.FirstName}', '{reader.Patronymic}', " +
                            $"'{reader.IssuedDate}', '{reader.BirthDate}');";

            return DataBaseClient.ExecuteInsertOrUpdate(query, out exception, out _);
        }

        public static bool RemoveReader(ulong id, out NpgsqlException exception, out int affectedRows)
        {
            string query = $"DELETE FROM \"ReaderBase\" WHERE \"ReaderID\" = '{id}'";
            return DataBaseClient.ExecuteDelete(query, out exception, out affectedRows);
        }

        public static bool SeeInfoAboutReader(ulong id, out NpgsqlException exception, out NpgsqlDataReader reader)
        {
            string query = $"SELECT * FROM \"ReaderBase\" WHERE \"ReaderID\" = '{id}'";
            return DataBaseClient.ExecuteSelect(query, out exception, out reader);
        }

        public static bool UpdateReader(ulong id, Reader updatedReader, out NpgsqlException exception)
        {
            string query = $"UPDATE \"ReaderBase\" Set \"Last Name\" = '{updatedReader.LastName}', " +
                            $"\"First Name\" = '{updatedReader.FirstName}', \"Patronymic\" = '{updatedReader.Patronymic}'" +
                            $", \"Issued Date\" = '{updatedReader.IssuedDate}', \"Birth Date\" = '{updatedReader.BirthDate}' " +
                            $"WHERE \"ReaderID\" = '{id}'";

            return DataBaseClient.ExecuteInsertOrUpdate(query, out exception, out _);
        }
    }
}

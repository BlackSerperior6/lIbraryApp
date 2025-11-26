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
        public static bool AddReader(string LastName, string FirstName, string Patronymic, 
            DateTime IssuedDate, DateTime BirthDate, out NpgsqlException exception)
        {
            string query = $"INSERT INTO \"Readers\" (\"ReaderID\", \"Last Name\", \"First Name\", " +
                            $"\"Patronymic\", \"Issued Date\", \"Birth Date\") " +
            $"VALUES (DEFAULT, '{LastName}', '{FirstName}', '{Patronymic}', " +
                            $"'{IssuedDate}', '{BirthDate}');";

            return DataBaseClient.ExecuteInsertOrUpdate(query, out exception);
        }

        public static bool RemoveReader(ulong id, out NpgsqlException exception, out int affectedRows)
        {
            string query = $"DELETE FROM \"Readers\" WHERE \"ReaderID\" = '{id}'";

            return DataBaseClient.ExecuteDelete(query, out exception, out affectedRows);
        }

        public static bool SeeInfoAboutReader(ulong id, out NpgsqlException exception, out NpgsqlDataReader reader)
        {
            string query = $"SELECT * FROM \"Readers\" WHERE \"ReaderID\" = '{id}'";
            return DataBaseClient.ExecuteSelect(query, out exception, out reader);
        }

        public static bool UpdateReader(ulong id, Reader updatedReader, out NpgsqlException exception)
        {
            string query = $"UPDATE \"Readers\" Set \"Last Name\" = '{updatedReader.LastName}', " +
                            $"\"First Name\" = '{updatedReader.FisrtName}', \"Patronymic\" = '{updatedReader.Patronymic}'" +
                            $", \"Issued Date\" = '{updatedReader.IssuedDate}', \"Birth Date\" = '{updatedReader.BirthDate}' " +
                            $"WHERE \"ReaderID\" = '{id}'";

            return DataBaseClient.ExecuteInsertOrUpdate(query, out exception);
        }
    }
}

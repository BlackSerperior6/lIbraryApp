using LibraryApplication.Structs;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.Controllers
{
    public static class BookCatalogController
    {
        public static bool AddBook(Book book,
            out NpgsqlException exception)
        {
            string query = $"INSERT INTO \"BookCatalog\" (\"BookId\", \"Title\", \"Author\", \"Release Date\", \"Arrival Date\") " +
            $"VALUES (DEFAULT, '{book.Title}', '{book.Author}', '{book.ReleasedDate}', '{book.ArrivalDate}');";

            return DataBaseClient.ExecuteInsertOrUpdate(query, out exception, out _);
        }

        public static bool RemoveBook(ulong id, out NpgsqlException exception, out int affectedRows)
        {
            string query = $"DELETE FROM \"BookCatalog\" WHERE \"BookId\" = '{id}'";

            return DataBaseClient.ExecuteDelete(query, out exception, out affectedRows);
        }

        public static bool GetInfoAboutBook(ulong id, out NpgsqlException exception, out NpgsqlDataReader reader)
        {
            string query = $"SELECT * FROM \"BookCatalog\" WHERE \"BookId\" = '{id}'";
            
            return DataBaseClient.ExecuteSelect(query, out exception, out reader);
        }

        public static bool UpdateBook(ulong id, Book updatedBook, out NpgsqlException exception)
        {
            string query = $"UPDATE \"BookCatalog\" Set \"Title\" = '{updatedBook.Title}', " +
                            $"\"Author\" = '{updatedBook.Author}', \"Release Date\" = '{updatedBook.ReleasedDate}', " +
                            $"\"Arrival Date\" = '{updatedBook.ArrivalDate}' " +
                            $"WHERE \"BookID\" = '{id}'";

            return DataBaseClient.ExecuteInsertOrUpdate(query, out exception, out _);
        }
    }
}

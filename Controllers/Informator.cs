using LibraryApplication.Structs;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.Controllers
{
    public static class Informator
    {
        public static bool IssuedBooksForAReader(ulong readerId, out NpgsqlException exception, 
            out Dictionary<Book, (DateTime, DateTime, ulong)> 
            books)
        {
            books = new Dictionary<Book, (DateTime, DateTime, ulong)>();

            string query = $"SELECT " +
                $"b.\"BookId\"," +
                $"b.\"Title\"," +
                $"b.\"Author\"," +
                $"b.\"Release Date\"," +
                $"b.\"Arrival Date\"," +
                $"br.\"Borrow Date\"," +
                $"br.\"Return Date Planed\"," +
                $"br.\"BorrowID\" " +
                $"FROM \"IssuedBooks\" br " +
                $"INNER JOIN \"BookCatalog\" b ON br.\"BookID\" = b.\"BookId\" " +
                $"WHERE br.\"ReaderID\" = '{readerId}' AND br.\"Return Date\" IS NULL " +
                $"ORDER BY br.\"BorrowID\" ASC;";

            if (!DataBaseClient.ExecuteSelect(query, out exception, out var reader))
                return false;

            while (reader.Read())
            {
                books.Add(new Book(reader.GetString(1), reader.GetString(2),
                            reader.GetDateTime(3), reader.GetDateTime(4)), (reader.GetDateTime(5), reader.GetDateTime(6),
                            (ulong) reader.GetInt64(7)));
            }

            reader.Close();
            return true;
        }

        public static bool IssuedBooksForAPeriod(DateTime start, DateTime end, out NpgsqlException exception,
            out Dictionary<Book, (DateTime, DateTime, ulong)>
            books)
        {
            books = new Dictionary<Book, (DateTime, DateTime, ulong)>();

            string query = $"SELECT " +
                $"b.\"BookId\"," +
                $"b.\"Title\"," +
                $"b.\"Author\"," +
                $"b.\"Release Date\"," +
                $"b.\"Arrival Date\"," +
                $"br.\"Borrow Date\"," +
                $"br.\"Return Date Planed\"," +
                $"br.\"BorrowID\" " +
                $"FROM \"IssuedBooks\" br " +
                $"INNER JOIN \"BookCatalog\" b ON br.\"BookID\" = b.\"BookId\" " +
                $"WHERE br.\"Borrow Date\" BETWEEN '{start}' AND '{end}' " +
                $"ORDER BY br.\"BorrowID\" ASC;";

            if (!DataBaseClient.ExecuteSelect(query, out exception, out var reader))
                return false;

            while (reader.Read())
            {
                books.Add(new Book(reader.GetString(1), reader.GetString(2),
                            reader.GetDateTime(3), reader.GetDateTime(4)), (reader.GetDateTime(5), reader.GetDateTime(6), 
                            (ulong)reader.GetInt64(7)));
            }

            reader.Close();
            return true;
        }

        public static bool ReturnedBooksPeriod(DateTime start, DateTime end, out NpgsqlException exception,
            out Dictionary<Book, (DateTime, DateTime, ulong)>
            books)
        {
            books = new Dictionary<Book, (DateTime, DateTime, ulong)>();

            string query = $"SELECT " +
                $"b.\"BookId\"," +
                $"b.\"Title\"," +
                $"b.\"Author\"," +
                $"b.\"Release Date\"," +
                $"b.\"Arrival Date\"," +
                $"br.\"Borrow Date\"," +
                $"br.\"Return Date\"," +
                $"br.\"BorrowID\" " +
                $"FROM \"IssuedBooks\" br " +
                $"INNER JOIN \"BookCatalog\" b ON br.\"BookID\" = b.\"BookId\" " +
                $"WHERE br.\"Return Date\" BETWEEN '{start}' AND '{end}' " +
                $"ORDER BY br.\"BorrowID\" ASC; ";

            if (!DataBaseClient.ExecuteSelect(query, out exception, out var reader))
                return false;

            while (reader.Read())
            {
                books.Add(new Book(reader.GetString(1), reader.GetString(2),
                            reader.GetDateTime(3), reader.GetDateTime(4)), (reader.GetDateTime(5), reader.GetDateTime(6), 
                            (ulong) reader.GetInt64(7)));
            }

            reader.Close();
            return true;
        }
    }
}

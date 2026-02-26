using LibraryAppWeb.Features;
using Npgsql;

namespace LibraryAppWeb.Handlers;

public static class InformatorHandler
{
    public static async Task<(bool success, NpgsqlException exception, Dictionary<Book, (DateTime, DateTime, ulong)> 
        books)> IssuedBooksForAReader(ulong readerId)
        {
            var books = new Dictionary<Book, (DateTime, DateTime, ulong)>();

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

            var result = await DataBaseClient.ExecuteSelectAsync(query);
            
            if (!result.Success)
                return (false, result.Exception, books);

            var reader = result.reader;
            
            while (await reader.ReadAsync())
            {
                books.Add(new Book(reader.GetString(1), reader.GetString(2),
                            reader.GetDateTime(3), reader.GetDateTime(4)), (reader.GetDateTime(5), reader.GetDateTime(6),
                            (ulong) reader.GetInt64(7)));
            }

            await reader.CloseAsync();
            return (true, null, books);
        }

    public static async Task<(bool success, NpgsqlException exception, Dictionary<Book, (DateTime, DateTime, ulong)>
        books)> IssuedBooksForAPeriod(DateTime start, DateTime end)
    {
        var books = new Dictionary<Book, (DateTime, DateTime, ulong)>();

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

        var select = await DataBaseClient.ExecuteSelectAsync(query);

        if (!select.Success)
            return (false, select.Exception, books);

        var reader = select.reader;

        while (await reader.ReadAsync())
        {
            books.Add(new Book(reader.GetString(1), reader.GetString(2),
                reader.GetDateTime(3), reader.GetDateTime(4)), (reader.GetDateTime(5), reader.GetDateTime(6),
                (ulong)reader.GetInt64(7)));
        }

        await reader.CloseAsync();
        return (true, null, books);
    }
}
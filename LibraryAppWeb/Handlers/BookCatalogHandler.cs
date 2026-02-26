using LibraryAppWeb.Features;
using Npgsql;

namespace LibraryAppWeb.Handlers;

public static class BookCatalogHandler
{
    public static async Task<(bool success, NpgsqlException exception)> AddBook(Book book)
    {
        string query = $"INSERT INTO \"BookCatalog\" (\"BookId\", \"Title\", \"Author\", \"Release Date\", \"Arrival Date\") " +
                       $"VALUES (DEFAULT, '{book.Title}', '{book.Author}', '{book.ReleasedDate}', '{book.ArrivalDate}');";

        var result = await DataBaseClient.ExecuteInsertUpdateDeleteAsync(query);

        return (result.Success, result.Exception);
    }

    public static async Task<(bool success, NpgsqlException exception, int affectedRow)> RemoveBook(ulong id)
    {
        string query = $"DELETE FROM \"BookCatalog\" WHERE \"BookId\" = '{id}'";

        var result = await DataBaseClient.ExecuteInsertUpdateDeleteAsync(query);
        
        return (result.Success, result.Exception, result.AffectedRows);
    }

    public static async Task<(bool success, NpgsqlException exception, NpgsqlDataReader reader)> GetInfoAboutBook(ulong id)
    {
        string query = $"SELECT * FROM \"BookCatalog\" WHERE \"BookId\" = '{id}'";
            
        var result = await DataBaseClient.ExecuteSelectAsync(query);

        return (result.Success, result.Exception, result.reader);
    }

    public static async Task<(bool success, NpgsqlException exception)> UpdateBook(ulong id, Book updatedBook)
    {
        string query = $"UPDATE \"BookCatalog\" Set \"Title\" = '{updatedBook.Title}', " +
                       $"\"Author\" = '{updatedBook.Author}', \"Release Date\" = '{updatedBook.ReleasedDate}', " +
                       $"\"Arrival Date\" = '{updatedBook.ArrivalDate}' " +
                       $"WHERE \"BookId\" = '{id}'";

        var result = await DataBaseClient.ExecuteInsertUpdateDeleteAsync(query);
        
        return (result.Success, result.Exception);
    }
}
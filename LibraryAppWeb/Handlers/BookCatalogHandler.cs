using LibraryAppWeb.Features;
using LibraryAppWeb.Interfaces;
using Npgsql;
using NpgsqlTypes;

namespace LibraryAppWeb.Handlers;

public static class BookCatalogHandler
{
    private static readonly IDbConnectionFactory _connectionFactory;

    public static async Task<(bool success, NpgsqlException exception)> AddBook(Book book)
    {
        string query = $"INSERT INTO \"BookCatalog\" (\"BookId\", \"Title\", \"Author\", \"Release Date\", \"Arrival Date\") " +
                       $"VALUES (DEFAULT, @title, @author, @releaseDate, @arrivalDate, DEFAULT);";
        
        try
        {
            await using var command = new NpgsqlCommand(query, DataBaseConnectionFactory.CurrentConnection);
        
            command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, book.Title);
            command.Parameters.AddWithValue("@author", NpgsqlDbType.Text, book.Author);
            command.Parameters.AddWithValue("@releaseDate", NpgsqlDbType.Date, book.ReleasedDate);
            command.Parameters.AddWithValue("@arrivalDate", NpgsqlDbType.Date, book.ArrivalDate);
            
            await command.ExecuteNonQueryAsync();
            return (true, null);
        }
        catch (NpgsqlException e)
        {
            return (false, e);
        }
    }

    public static async Task<(bool success, NpgsqlException exception, int affectedRow)> RemoveBook(ulong id)
    {
        string query = $"DELETE FROM \"BookCatalog\" WHERE \"BookId\" = @id;";
        
        try
        {
            await using var command = new NpgsqlCommand(query, DataBaseConnectionFactory.CurrentConnection);
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Bigint, id);
            
            var affectedRows = await command.ExecuteNonQueryAsync();
            return (true, null, affectedRows);
        }
        catch (NpgsqlException e)
        {
            return (false, e, 0);
        }
    }

    public static async Task<(bool success, NpgsqlException exception, NpgsqlDataReader reader)> GetInfoAboutBook(ulong id)
    {
        string query = $"SELECT * FROM \"BookCatalog\" WHERE \"BookId\" = @id;";
        
        try
        {
            await using var command = new NpgsqlCommand(query, DataBaseConnectionFactory.CurrentConnection);
        
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Bigint, id);
            
            var reader = await command.ExecuteReaderAsync();
            return (true, null, reader);

        }
        catch (NpgsqlException e)
        {
            return (false, e, null);
        }
    }

    public static async Task<(bool success, NpgsqlException exception)> UpdateBook(ulong id, int expectedVerion, Book updatedBook)
    {
        string query = $"UPDATE \"BookCatalog\" Set \"Title\" = @title, " +
                       $"\"Author\" = @author, \"Release Date\" = @releaseDate, " +
                       $"\"Arrival Date\" = @arrivalDate " +
                       $"\"version\" = {expectedVerion + 1}" +
                       $"WHERE \"BookId\" = @id and \"version\" = {expectedVerion};";
        
        try
        {
            await using var command = new NpgsqlCommand(query, DataBaseConnectionFactory.CurrentConnection);
        
            command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, updatedBook.Title);
            command.Parameters.AddWithValue("@author", NpgsqlDbType.Text, updatedBook.Author);
            command.Parameters.AddWithValue("@releaseDate", NpgsqlDbType.Date, updatedBook.ReleasedDate);
            command.Parameters.AddWithValue("@arrivalDate", NpgsqlDbType.Date, updatedBook.ArrivalDate);
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Bigint, id);

            int updated =  await command.ExecuteNonQueryAsync();

            if (updated == 0)
                throw new NpgsqlException("Данная запись была удалена, либо обновлена во время процесса. Пожалуйста, попробуйте еще раз!");

            return (true, null);

        }
        catch (NpgsqlException e)
        {
            return (false, e);
        }
    }
}
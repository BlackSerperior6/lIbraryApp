using LibraryAppWeb.Features;
using Npgsql;
using NpgsqlTypes;

namespace LibraryAppWeb.Handlers;

public static class BookCatalogHandler
{

    public static async Task<(bool success, NpgsqlException exception)> AddBook(Book book, NpgsqlConnection preExistingConnection = null)
    {
        string query = $"INSERT INTO \"BookCatalog\" (\"BookId\", \"Title\", \"Author\", \"Release Date\", \"Arrival Date\") " +
                       $"VALUES (DEFAULT, @title, @author, @releaseDate, @arrivalDate, DEFAULT);";
        
        try
        {
            await using var connection = preExistingConnection ?? await DataBaseConnectionFactory.CreateConnection();
            await using var command = new NpgsqlCommand(query, connection);
        
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

    public static async Task<(bool success, NpgsqlException exception, int affectedRow)> RemoveBook(ulong id, NpgsqlConnection preExistingConnection = null)
    {
        string query = $"DELETE FROM \"BookCatalog\" WHERE \"BookId\" = @id;";
        
        try
        {
            await using var connection = preExistingConnection ?? await DataBaseConnectionFactory.CreateConnection();
            await using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Bigint, id);
            
            var affectedRows = await command.ExecuteNonQueryAsync();
            return (true, null, affectedRows);
        }
        catch (NpgsqlException e)
        {
            return (false, e, 0);
        }
    }

    public static async Task<(bool success, NpgsqlException exception, NpgsqlDataReader reader)> GetInfoAboutBook(ulong id, 
        NpgsqlConnection preExistingConnection = null)
    {
        string query = $"SELECT * FROM \"BookCatalog\" WHERE \"BookId\" = @id;";
        
        try
        {
            await using var connection = preExistingConnection ?? await DataBaseConnectionFactory.CreateConnection();
            await using var command = new NpgsqlCommand(query, connection);
        
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Bigint, id);
            
            var reader = await command.ExecuteReaderAsync();
            return (true, null, reader);

        }
        catch (NpgsqlException e)
        {
            return (false, e, null);
        }
    }

    public static async Task<(bool success, NpgsqlException exception)> UpdateBook(ulong id, int expectedVerion, Book updatedBook, 
        NpgsqlConnection preExistingConnection = null)
    {
        string query = $"UPDATE \"BookCatalog\" Set \"Title\" = @title, " +
                       $"\"Author\" = @author, \"Release Date\" = @releaseDate, " +
                       $"\"Arrival Date\" = @arrivalDate " +
                       $"\"version\" = {expectedVerion + 1}" +
                       $"WHERE \"BookId\" = @id and \"version\" = {expectedVerion};";
        
        try
        {
            await using var connection = preExistingConnection ?? await DataBaseConnectionFactory.CreateConnection();
            await using var command = new NpgsqlCommand(query, connection);
        
            command.Parameters.AddWithValue("@title", NpgsqlDbType.Text, updatedBook.Title);
            command.Parameters.AddWithValue("@author", NpgsqlDbType.Text, updatedBook.Author);
            command.Parameters.AddWithValue("@releaseDate", NpgsqlDbType.Date, updatedBook.ReleasedDate);
            command.Parameters.AddWithValue("@arrivalDate", NpgsqlDbType.Date, updatedBook.ArrivalDate);
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Bigint, id);

            int updated =  await command.ExecuteNonQueryAsync();

            if (updated == 0)
                throw new NpgsqlException("������ ������ ���� �������, ���� ��������� �� ����� ��������. ����������, ���������� ��� ���!");

            return (true, null);

        }
        catch (NpgsqlException e)
        {
            return (false, e);
        }
    }
}
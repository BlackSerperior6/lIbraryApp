using Npgsql;

namespace LibraryAppWeb.Handlers;

public class BookIssuerHandler
{
    //exception - null и false, то книга занята, иначе действительно ошибка
    public static async Task<(bool, NpgsqlException exception)> IssuedBook(ulong readerId, ulong bookId, DateTime issueDate, DateTime plannedReturnDate)
    {
        string query = $"SELECT COUNT(*) FROM \"IssuedBooks\" WHERE \"BookID\" = @bookId AND \"Return Date\" IS NULL";

        try
        {
            await using (var command = new NpgsqlCommand(query, DataBaseConnectionFactory.CurrentConnection))
            {
                command.Parameters.AddWithValue("@readerId", NpgsqlTypes.NpgsqlDbType.Bigint, readerId);

                var reader = await command.ExecuteReaderAsync();

                await reader.ReadAsync();

                if (reader.GetInt32(0) != 0)
                {
                    await reader.CloseAsync();
                    return (false, null);
                }

                await reader.CloseAsync();
            }

            string secondQueue = $"INSERT INTO \"IssuedBooks\" (\"ReaderID\", \"BookID\", \"Borrow Date\", " +
                                 $"\"Return Date Planed\", \"BorrowID\", \"Return Date\") VALUES (@readerId, '@bookId', '@issueDate', '@plannedReturnDate', " +
                                 $"DEFAULT, NULL, DEFAULT)";

            await using (var command = new NpgsqlCommand(secondQueue, DataBaseConnectionFactory.CurrentConnection))
            {
                command.Parameters.AddWithValue("@readerId", NpgsqlTypes.NpgsqlDbType.Bigint, readerId);
                command.Parameters.AddWithValue("@bookId", NpgsqlTypes.NpgsqlDbType.Bigint, bookId);
                command.Parameters.AddWithValue("@issueDate", NpgsqlTypes.NpgsqlDbType.Date, issueDate);
                command.Parameters.AddWithValue("@@plannedReturnDate", NpgsqlTypes.NpgsqlDbType.Date, plannedReturnDate);

                await command.ExecuteNonQueryAsync();
                return (true, null);
            }
        }
        catch (NpgsqlException e)
        {
            return (false, e);
        }
    }

    public static async Task<(bool, NpgsqlException exception)> ReturnBook(ulong borrowId, DateTime returnDate, int expectedVersion)
    {
        string query = $"UPDATE \"IssuedBooks\" Set \"Return Date\" = @returnDate, \"version\" = '{expectedVersion + 1}' " +
            $"WHERE \"BorrowID\" = @borrowId AND \"Return Date\" IS NULL AND " +
            $"\"version\" = '{expectedVersion}'";

        try
        {
            await using var command = new NpgsqlCommand(query, DataBaseConnectionFactory.CurrentConnection);

            command.Parameters.AddWithValue("@returnDate", NpgsqlTypes.NpgsqlDbType.Date, returnDate);
            command.Parameters.AddWithValue("@borrowId", NpgsqlTypes.NpgsqlDbType.Date, borrowId);

            var updated = await command.ExecuteNonQueryAsync();

            if (updated == 0)
                throw new NpgsqlException("Запись была удалена или изменена во время процесса. Пожалуйста, попробуйте еще раз!");

            return (true, null);
        }
        catch (NpgsqlException e) 
        {
            return (false, e);
        }
    }
}
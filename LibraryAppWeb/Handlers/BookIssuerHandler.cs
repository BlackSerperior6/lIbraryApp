using Npgsql;

namespace LibraryAppWeb.Handlers;

public class BookIssuerHandler
{
    //exception - null и false, то книга занята, иначе действительно ошибка
    public static async Task<(bool, NpgsqlException exception)> IssuedBook(ulong readerId, ulong bookId, DateTime issueDate, DateTime plannedReturnDate)
    {
        string queue = $"SELECT COUNT(*) FROM \"IssuedBooks\" WHERE \"BookID\" = '{bookId}' AND \"Return Date\" IS NULL";
        
        var select = await DataBaseClient.ExecuteSelectAsync(queue);

        if (!select.Success)
            return (false, select.Exception);

        var reader = select.reader;
        await reader.ReadAsync();
            
        if (reader.GetInt32(0) != 0)
        { 
            await reader.CloseAsync();
            return (false, null); 
        }

        await reader.CloseAsync();

        string secondQueue = $"INSERT INTO \"IssuedBooks\" (\"ReaderID\", \"BookID\", \"Borrow Date\", " +
                             $"\"Return Date Planed\", \"BorrowID\", \"Return Date\") VALUES ('{readerId}', '{bookId}', '{issueDate}', '{plannedReturnDate}', " +
                             $"DEFAULT, NULL)";

        var result = await DataBaseClient.ExecuteInsertUpdateDeleteAsync(secondQueue);
        return (true, result.Exception);
    }
}
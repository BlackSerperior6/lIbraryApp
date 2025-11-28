using LibraryApplication.Structs;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.Controllers
{
    public static class BookIssuerController
    {
        //exception - null и false, то книга занята
        public static bool IssuedBook(ulong readerId, ulong bookId, DateTime issueDate, DateTime plannedReturnDate, 
            out NpgsqlException exception)
        {
            string queue = $"SELECT COUNT(*) FROM \"IssuedBooks\" WHERE \"BookID\" = '{readerId}' AND \"Return Date\" IS NULL";

            if (!DataBaseClient.ExecuteSelect(queue, out exception, out var reader))
                return false;

            reader.Read();
            
            if (reader.GetInt32(0) != 0)
                return false;

            string secondQueue = $"INSERT INTO \"IssuedBooks\" (\"ReaderID\", \"BookID\", \"Borrow Date\", " +
                $"\"Return Date Planed\", \"BorrowID\", \"Return Date\") VALUES ('{readerId}', '{bookId}', '{issueDate}', '{plannedReturnDate}', " +
                $"DEFAULT, NULL)";

            return DataBaseClient.ExecuteInsertOrUpdate(secondQueue, out exception, out _);
        }

        //exception - null и false, то такой записи нет
        public static bool ReturnBook(ulong readerId, ulong bookId, DateTime returnDate,
            out NpgsqlException exception)
        {
            string queue = $"UPDATE \"IssuedBooks\" Set \"Return Date\" = \"{returnDate}\"";

            if (!DataBaseClient.ExecuteInsertOrUpdate(queue, out exception, out var affectedRows))
                return false;

            return affectedRows > 0;
        }
    }
}

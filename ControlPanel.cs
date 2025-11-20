using LibraryApplication.BufferForms;
using LibraryApplication.InputForms;
using LibraryApplication.Structs;
using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace LibraryApplication
{
    public partial class ControlPanel : Form
    {
        private string connectionString;
        public ControlPanel(string conString)
        {
            connectionString = conString;
            InitializeComponent();
        }

        private void AddReader_Click(object sender, EventArgs e)
        {
            try
            {
                var editor = new RedactReader();

                if (editor.ShowDialog() == DialogResult.OK)
                {
                    Reader reader = editor.result;

                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = $"INSERT INTO \"Readers\" (\"ReaderID\", \"Last Name\", \"First Name\", " +
                            $"\"Patronymic\", \"Issued Date\") " +
                            $"VALUES (DEFAULT, '{reader.LastName}', '{reader.Name}', '{reader.Patronymic}', " +
                            $"'{reader.DateTime}');";

                        Console.WriteLine(query);

                        var command = new NpgsqlCommand(query, connection);
                        command.ExecuteScalar();

                        MessageBox.Show($"Читаель был добавлен в базу", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {ex.Message}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteReader_Click(object sender, EventArgs e)
        {
            try
            {
                var result = Interaction.InputBox(
                "Введите ID билета читателя:",
                "Ввод");

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"DELETE FROM \"Readers\" WHERE \"ReaderID\" = '{result}'";

                    var command = new NpgsqlCommand(query, connection);
                    int affected = command.ExecuteNonQuery();

                    if (affected == 0)
                        MessageBox.Show($"Не существует читателя с таким id билета!", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    else
                        MessageBox.Show($"Читатель был удален из базы!", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {ex.Message}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RedactReader_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Interaction.InputBox(
                "Введите ID билета читателя:",
                "Ввод");

                Reader? originalReader = null;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM \"Readers\" WHERE \"ReaderID\" = '{id}'";

                    var command = new NpgsqlCommand(query, connection);
                    var reader = command.ExecuteReader();

                    Console.WriteLine(query);

                    if (reader.Read())
                    {
                        originalReader = new Reader(reader.GetString(1), reader.GetString(2),
                            reader.GetString(3), reader.GetDateTime(4));
                    }

                }

                if (originalReader == null)
                {
                    MessageBox.Show($"Не существует читателя с таким id билета!", "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);

                    return;
                }

                Reader receivedReader = (Reader)originalReader;

                var editor = new RedactReader(receivedReader.LastName, receivedReader.Name, receivedReader.Patronymic,
                    receivedReader.DateTime);

                if (editor.ShowDialog() == DialogResult.OK)
                {
                    receivedReader = editor.result;

                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = $"UPDATE \"Readers\" Set \"Last Name\" = '{receivedReader.LastName}', " +
                            $"\"First Name\" = '{receivedReader.Name}', \"Patronymic\" = '{receivedReader.Patronymic}'" +
                            $", \"Issued Date\" = '{receivedReader.DateTime}' WHERE \"ReaderID\" = '{id}'";

                        var command = new NpgsqlCommand(query, connection);
                        command.ExecuteScalar();

                        MessageBox.Show($"Читатель был успешно отредактирован", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {ex.Message}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InfoReader_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Interaction.InputBox(
                "Введите ID билета читателя:",
                "Ввод");

                Reader? bufferReader = null;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM \"Readers\" WHERE \"ReaderID\" = '{id}'";

                    var command = new NpgsqlCommand(query, connection);
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                        bufferReader = new Reader(reader.GetString(1), reader.GetString(2),
                            reader.GetString(3), reader.GetDateTime(4));
                }

                if (bufferReader == null)
                {
                    MessageBox.Show($"Не существует читателя с таким id билета!", "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);

                    return;
                }

                Reader receivedReader = (Reader)bufferReader;

                var editor = new RedactReader(receivedReader.LastName, receivedReader.Name, receivedReader.Patronymic,
                    receivedReader.DateTime, false);

                editor.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {ex.Message}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddBook_Click(object sender, EventArgs e)
        {
            try
            {
                var editor = new RedactBook();

                if (editor.ShowDialog() == DialogResult.OK)
                {
                    Book book = editor.result;

                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = $"INSERT INTO \"Books\" (\"BookId\", \"Title\", \"Author\", \"Release Date\") " +
                            $"VALUES (DEFAULT, '{book.Title}', '{book.Author}', '{book.ReleasedDate}');";

                        var command = new NpgsqlCommand(query, connection);
                        command.ExecuteScalar();

                        MessageBox.Show($"Книга была добавлена в базу", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {ex.Message}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveBook_Click(object sender, EventArgs e)
        {
            try
            {
                var result = Interaction.InputBox(
                "Введите ID книги:",
                "Ввод");

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"DELETE FROM \"Books\" WHERE \"BookId\" = '{result}'";

                    var command = new NpgsqlCommand(query, connection);
                    int affected = command.ExecuteNonQuery();

                    if (affected == 0)
                        MessageBox.Show($"Не существует книги с таким id!", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    else
                        MessageBox.Show($"Книга была удалена из базы!", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {ex.Message}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RedactBook_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Interaction.InputBox(
                "Введите ID книги:",
                "Ввод");

                Book? originalBook = null;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM \"Books\" WHERE \"BooksId\" = '{id}'";

                    var command = new NpgsqlCommand(query, connection);
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        originalBook = new Book(reader.GetString(1), reader.GetString(2),
                            reader.GetDateTime(3));
                    }

                }

                if (originalBook == null)
                {
                    MessageBox.Show($"Не существует книги с таким id!", "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);

                    return;
                }

                Book receivedBook = (Book)originalBook;

                var editor = new RedactBook(receivedBook.Title, receivedBook.Author, receivedBook.ReleasedDate);

                if (editor.ShowDialog() == DialogResult.OK)
                {
                    receivedBook = editor.result;

                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = $"UPDATE \"Books\" Set \"Title\" = '{receivedBook.Title}', " +
                            $"\"Author\" = '{receivedBook.Author}', \"Release Date\" = '{receivedBook.ReleasedDate}'";

                        var command = new NpgsqlCommand(query, connection);
                        command.ExecuteScalar();

                        MessageBox.Show($"Книга была успешно отредактирована", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {ex.Message}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InfoBook_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Interaction.InputBox(
                "Введите ID книги:",
                "Ввод");

                Book? bufferBook = null;

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM \"Books\" WHERE \"BookId\" = '{id}'";

                    var command = new NpgsqlCommand(query, connection);
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                        bufferBook = new Book(reader.GetString(1), reader.GetString(2),
                            reader.GetDateTime(3));
                }

                if (bufferBook == null)
                {
                    MessageBox.Show($"Не существует читателя с таким id билета!", "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);

                    return;
                }

                Book receivedBook = (Book)bufferBook;

                var editor = new RedactBook(receivedBook.Title, receivedBook.Author, receivedBook.ReleasedDate, false);

                editor.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {ex.Message}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DatesReturn_Click(object sender, EventArgs e)
        {
            try
            {
                var editor = new DatesPickerForm();
                editor.Show();

                var begDate = editor.firstDate;
                var endDate = editor.lastDate;

                Dictionary<Book, DateTime> books = new Dictionary<Book, DateTime>();

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM getreturnedbookstimeperiod('{begDate}', '{endDate}')";

                    var command = new NpgsqlCommand(query, connection);
                    var reader = command.ExecuteReader();

                    int counter = 0;

                    while (reader.Read())
                    {
                        counter++;

                        books.Add(new Book(reader.GetString(1), reader.GetString(2),
                            reader.GetDateTime(3)), reader.GetDateTime(4));
                    }

                    if (counter == 0)
                        MessageBox.Show($"Не обнаружен возвращенных книг за этот период!", "Результат!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                var visuaLViewer = new BooksListView(books, "Дата возврата");
                visuaLViewer.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {ex.Message}.", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BorrowedDates_Click(object sender, EventArgs e)
        {
            try
            {
                var editor = new DatesPickerForm();
                editor.Show();

                var begDate = editor.firstDate;
                var endDate = editor.lastDate;

                Dictionary<Book, DateTime> books = new Dictionary<Book, DateTime>();

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM getborrowedbookstimeperiod('{begDate}', '{endDate}')";

                    var command = new NpgsqlCommand(query, connection);
                    var reader = command.ExecuteReader();

                    int counter = 0;

                    while (reader.Read())
                    {
                        counter++;

                        books.Add(new Book(reader.GetString(1), reader.GetString(2),
                            reader.GetDateTime(3)), reader.GetDateTime(4));
                    }

                    if (counter == 0)
                        MessageBox.Show($"Не обнаружен взятых книг за этот период!", "Результат!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                var visuaLViewer = new BooksListView(books, "Дата взятия");
                visuaLViewer.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {ex.Message}.", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

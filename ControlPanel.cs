using LibraryApplication.BufferForms;
using LibraryApplication.Controllers;
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
using static System.Reflection.Metadata.BlobBuilder;

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
            var editor = new RedactReader(null);

            if (editor.ShowDialog() == DialogResult.OK)
            {
                Reader reader = editor.result;

                if (!ReaderBaseController.AddReader(reader, out var exception))
                {
                    MessageBox.Show($"Ошибка при выполнение запроса: {exception}", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show($"Читатель был успешно добавлен", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteReader_Click(object sender, EventArgs e)
        {
            var result = Interaction.InputBox(
                "Введите ID билета читателя:",
                "Ввод");

            if (!ulong.TryParse(result, out var id))
            {
                MessageBox.Show($"{result} не является валидным id номером читателя", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!ReaderBaseController.RemoveReader(id, out var exception, out int affectedRows))
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {exception}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (affectedRows <= 0)
            {
                MessageBox.Show($"Читателя с таким id не обнаружено: {exception}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            MessageBox.Show($"Читатель был успешно удален", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RedactReader_Click(object sender, EventArgs e)
        {
            var result = Interaction.InputBox(
                "Введите ID билета читателя:",
                "Ввод");

            if (!ulong.TryParse(result, out var id))
            {
                MessageBox.Show($"{result} не является валидным id номером читателя", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!ReaderBaseController.GetInfoAboutReader(id, out var exception, out var dbReader))
            {
                MessageBox.Show($"Ошибка при выполнение первого запроса: {exception}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!dbReader.Read())
            {
                MessageBox.Show($"Читатель с указанным id не найден!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var currentReader = new Reader(dbReader.GetString(1), dbReader.GetString(2),
                            dbReader.GetString(3), dbReader.GetDateTime(4), dbReader.GetDateTime(5));

            var editor = new RedactReader(currentReader);

            if (editor.ShowDialog() == DialogResult.OK)
            {
                currentReader = editor.result;

                if (ReaderBaseController.UpdateReader(id, currentReader, out exception))
                {
                    MessageBox.Show($"Ошибка при выполнение второго запроса: {exception}", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show($"Читатель был успешно изменен", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void InfoReader_Click(object sender, EventArgs e)
        {
            var result = Interaction.InputBox(
                "Введите ID билета читателя:",
                "Ввод");

            if (!ulong.TryParse(result, out var id))
            {
                MessageBox.Show($"{result} не является валидным id номером читателя", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!ReaderBaseController.GetInfoAboutReader(id, out var exception, out var dbReader))
            {
                MessageBox.Show($"Ошибка при выполнение первого запроса: {exception}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!dbReader.Read())
            {
                MessageBox.Show($"Читатель с указанным id не найден!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var currentReader = new Reader(dbReader.GetString(1), dbReader.GetString(2),
                            dbReader.GetString(3), dbReader.GetDateTime(4), dbReader.GetDateTime(5));

            var editor = new RedactReader(currentReader, false);
        }

        private void AddBook_Click(object sender, EventArgs e)
        {
            var editor = new RedactBook(null);

            if (editor.ShowDialog() == DialogResult.OK)
            {
                Book book = editor.result;

                if (!BookCatalogController.AddBook(book, out var exception))
                {
                    MessageBox.Show($"Ошибка при выполнение запроса: {exception}", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show($"Книга была успешно добавлена", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RemoveBook_Click(object sender, EventArgs e)
        {
            var result = Interaction.InputBox(
                "Введите ID билета читателя:",
                "Ввод");

            if (!ulong.TryParse(result, out var id))
            {
                MessageBox.Show($"{result} не является валидным id номером читателя", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!BookCatalogController.RemoveBook(id, out var exception, out int affectedRows))
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {exception}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (affectedRows <= 0)
            {
                MessageBox.Show($"Книги с таким id не обнаружено: {exception}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            MessageBox.Show($"Книга была успешно удалена", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RedactBook_Click(object sender, EventArgs e)
        {
            var result = Interaction.InputBox(
                "Введите ID билета читателя:",
                "Ввод");

            if (!ulong.TryParse(result, out var id))
            {
                MessageBox.Show($"{result} не является валидным id номером читателя", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!BookCatalogController.GetInfoAboutBook(id, out var exception, out var dbReader))
            {
                MessageBox.Show($"Ошибка при выполнение первого запроса: {exception}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!dbReader.Read())
            {
                MessageBox.Show($"Книга с указанным id не найдена!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var currentBook = new Book(dbReader.GetString(1), dbReader.GetString(2),
                            dbReader.GetDateTime(3), dbReader.GetDateTime(4));

            var editor = new RedactBook(currentBook);

            if (editor.ShowDialog() == DialogResult.OK)
            {
                currentBook = editor.result;

                if (!BookCatalogController.UpdateBook(id, currentBook, out exception))
                {
                    MessageBox.Show($"Ошибка при выполнение второго запроса: {exception}", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show($"Книга была успешно изменена", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void InfoBook_Click(object sender, EventArgs e)
        {
            var result = Interaction.InputBox(
                "Введите ID билета читателя:",
                "Ввод");

            if (!ulong.TryParse(result, out var id))
            {
                MessageBox.Show($"{result} не является валидным id номером читателя", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!BookCatalogController.GetInfoAboutBook(id, out var exception, out var dbReader))
            {
                MessageBox.Show($"Ошибка при выполнение запроса: {exception}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!dbReader.Read())
            {
                MessageBox.Show($"Книга с указанным id не найдена!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var currentBook = new Book(dbReader.GetString(1), dbReader.GetString(2),
                            dbReader.GetDateTime(3), dbReader.GetDateTime(4));

            var editor = new RedactBook(currentBook, false);
        }

        private void DatesReturn_Click(object sender, EventArgs e)
        {
            var editor = new DatesPickerForm();

            if (editor.ShowDialog() == DialogResult.OK)
            {
                var begDate = editor.firstDate;
                var endDate = editor.lastDate;

                if (!Informator.ReturnedBooksPeriod(begDate, endDate, out var exception, out var books))
                {
                    MessageBox.Show($"Ошибка при выполнение запроса: {exception}", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                var visuaLViewer = new BooksListView(books);
                visuaLViewer.Show();
            }
        }

        private void BorrowedDates_Click(object sender, EventArgs e)
        {
            var editor = new DatesPickerForm();

            if (editor.ShowDialog() == DialogResult.OK)
            {
                var begDate = editor.firstDate;
                var endDate = editor.lastDate;

                if (!Informator.IssuedBooksForAPeriod(begDate, endDate, out var exception, out var books))
                {
                    MessageBox.Show($"Ошибка при выполнение запроса: {exception}", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                var visuaLViewer = new BooksListView(books);
                visuaLViewer.Show();
            }
        }

        private void ReadersBook_Click(object sender, EventArgs e)
        {
            var result = Interaction.InputBox(
                "Введите ID билета читателя:",
                "Ввод");

            if (!ulong.TryParse(result, out var id))
            {
                MessageBox.Show($"{result} не является валидным id номером читателя", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!Informator.IssuedBooksForAReader(id, out var exception, out var books, out var foundReader))
            {
                if (foundReader)
                {
                    MessageBox.Show($"Читателя с таким id не существует!", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Ошибка при выполнение запроса: {exception}", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }

            var visuaLViewer = new BooksListView(books);
            visuaLViewer.Show();
        }

        private void BorrowBook_Click(object sender, EventArgs e)
        {
            var editor = new IssueBookForm();

            if (editor.ShowDialog() == DialogResult.OK)
            {

                if (!BookIssuerController.IssuedBook(editor.readerId, editor.bookId, editor.issueDate, editor.returnDate, 
                    out var exception))
                {
                    if (exception == null)
                    {
                        MessageBox.Show($"Ошибка при выполнение запроса: {exception}", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Книга находтися в распоряжении другого читателя", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return;
                }

                MessageBox.Show($"Книга была успешно выдана читателю", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ReturnBook_Click(object sender, EventArgs e)
        {
            var editor = new ReturnBookForm();

            if (editor.ShowDialog() == DialogResult.OK)
            {

                if (!BookIssuerController.ReturnBook(editor.borrowId, editor.returnDate,
                    out var exception))
                {
                    if (exception == null)
                    {
                        MessageBox.Show($"Ошибка при выполнение запроса: {exception}", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Выдачи с таким id не происходило", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return;
                }

                MessageBox.Show($"Книга была успешно выдана читателю", "Подтверждение!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

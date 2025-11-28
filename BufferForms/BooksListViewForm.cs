using LibraryApplication.Structs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApplication.BufferForms
{
    public partial class BooksListView : Form
    {
        public BooksListView(Dictionary<Book, (DateTime, DateTime)> books)
        {
            InitializeComponent();

            foreach (var pair in books)
                MainGrid.Rows.Add(pair.Key.Title, pair.Key.Author, pair.Key.ReleasedDate, pair.Key.ArrivalDate, pair.Value.Item1,
                    pair.Value.Item2);

            foreach (var book in books.Keys)
                MainGrid.Rows.Add(book.Title, book.Author, book.ReleasedDate);
        }
    }
}

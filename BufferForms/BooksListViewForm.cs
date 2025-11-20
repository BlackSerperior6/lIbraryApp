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
        public BooksListView(Dictionary<Book, DateTime> books, string additionalColumnHeader, 
            bool lastColumnExists = true)
        {
            InitializeComponent();

            if (lastColumnExists)
            {
                foreach (var book in books)
                    MainGrid.Rows.Add(book.Key.Title, book.Key.Author, book.Key.ReleasedDate, book.Value);

                MainGrid.Columns[^1].HeaderText = additionalColumnHeader;
            }
            else 
            {
                MainGrid.Columns.Remove(MainGrid.Columns[^1]);

                foreach (var book in books.Keys)
                    MainGrid.Rows.Add(book.Title, book.Author, book.ReleasedDate);
            }
            
        }
    }
}

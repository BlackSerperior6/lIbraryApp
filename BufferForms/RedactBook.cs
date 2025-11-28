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

namespace LibraryApplication.InputForms
{
    public partial class RedactBook : Form
    {
        public Book result;
        private bool _editable;

        public RedactBook(Book? book, bool editable = true)
        {
            InitializeComponent();

            if (book == null)
            {
                Title.Text = "";
                Author.Text = "";
                ReleaseDate.Value = DateTime.Now;
                ArrivalDate.Value = DateTime.Now;
            }
            else 
            {
                Title.Text = book.Title;
                Author.Text = book.Author;
                ReleaseDate.Value = book.ReleasedDate;
                ArrivalDate.Value = book.ArrivalDate;
            }

            _editable = editable;

            if (!_editable)
            {
                Title.ReadOnly = true;
                Author.ReadOnly = true;
                ReleaseDate.Enabled = false;
                ArrivalDate.Enabled = false;

                ReleaseDate.BackColor = Color.White;
                ArrivalDate.BackColor = Color.White;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Title.Text) || string.IsNullOrWhiteSpace(Author.Text))
            {
                MessageBox.Show($"Ни одно из полей не должно быть пустым!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            result = new Book(Title.Text, Author.Text, ReleaseDate.Value, ArrivalDate.Value);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

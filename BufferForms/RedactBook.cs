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

        public RedactBook(string title = "", string author = "", DateTime? releaseDate = null, bool editable = true)
        {
            InitializeComponent();
            Title.Text = title;
            Author.Text = author;

            if (releaseDate != null)
                ReleaseDate.Value = (DateTime) releaseDate;
            else
                ReleaseDate.Value = DateTime.Now;

            _editable = editable;

            if (!_editable)
            {
                Title.ReadOnly = true;
                Author.ReadOnly = true;
                ReleaseDate.Enabled = false;

                ReleaseDate.BackColor = Color.White;
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

            result = new Book(Title.Text, Author.Text, ReleaseDate.Value);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

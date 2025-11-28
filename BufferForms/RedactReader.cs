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
    public partial class RedactReader : Form
    {
        public Reader result;
        private readonly bool _editable;

        public RedactReader(Reader? reader, bool editable = true)
        {
            InitializeComponent();

            if (reader == null)
            {
                LastName.Text = "";
                FirstName.Text = "";
                Patronymic.Text = "";
                IssuedDate.Value = DateTime.Now;
                BirthdayPicker.Value = DateTime.Now;
            }
            else 
            {
                LastName.Text = reader?.LastName;
                FirstName.Text = reader?.FirstName;
                Patronymic.Text = reader?.Patronymic;
                IssuedDate.Value = reader.IssuedDate;
                BirthdayPicker.Value = reader.BirthDate;
            }

            _editable = editable;

            if (!_editable)
            {
                LastName.ReadOnly = true;
                FirstName.ReadOnly = true;
                Patronymic.ReadOnly = true;
                IssuedDate.Enabled = false;
                BirthdayPicker.Enabled = false;

                IssuedDate.BackColor = Color.White;
                BirthdayPicker.BackColor = Color.White;
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (_editable)
            {
                if (string.IsNullOrWhiteSpace(LastName.Text) || string.IsNullOrWhiteSpace(FirstName.Text) ||
                string.IsNullOrWhiteSpace(Patronymic.Text))
                {
                    MessageBox.Show($"Ни одно из полей не должно быть пустым!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                result = new Reader(LastName.Text, FirstName.Text, Patronymic.Text, IssuedDate.Value, BirthdayPicker.Value);
                DialogResult = DialogResult.OK;
            }

            Close();
        }
    }
}

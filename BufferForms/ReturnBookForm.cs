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
    public partial class ReturnBookForm : Form
    {
        public ulong borrowId;
        public DateTime returnDate;

        public ReturnBookForm()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (!ulong.TryParse(ReaderId.Text, out borrowId))
            {
                MessageBox.Show($"Неверный формат id взятия книги", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult = DialogResult.Cancel;
                return;
            }

            returnDate = RetDate.Value;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

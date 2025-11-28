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
    public partial class IssueBookForm : Form
    {
        public ulong readerId;
        public ulong bookId;
        public DateTime issueDate;
        public DateTime returnDate;

        public IssueBookForm()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (!ulong.TryParse(ReaderId.Text, out readerId) || !ulong.TryParse(BookId.Text, out bookId))
            {
                MessageBox.Show($"Неверный формат id книги или читателя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult = DialogResult.Cancel;
                return;
            }

            issueDate = IssueDate.Value;
            returnDate = RetDate.Value;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

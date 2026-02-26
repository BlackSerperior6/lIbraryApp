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
    public partial class DatesPickerForm : Form
    {
        public DateTime firstDate;
        public DateTime lastDate;

        public DatesPickerForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (FirstDate.Value.Date > SecondDate.Value.Date)
            {
                MessageBox.Show($"Дата начала периода должна быть меньше или равна дате конца периода!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult = DialogResult.Cancel;
                return;
            }

            firstDate = FirstDate.Value;
            lastDate = SecondDate.Value;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

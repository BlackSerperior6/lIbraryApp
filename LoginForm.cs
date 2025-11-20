using Npgsql;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LibraryApplication
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void AuthButton_Click(object sender, EventArgs e)
        {
            string username = Login.Text;
            string password = Password.Text;
            string server = "localhost";
            string port = "5432";
            string database = "Library";

            string connectionString = $"Server={server};Port={port};Database={database};" +
                $"User Id={username};Password={password};";

            Connect(connectionString);
        }

        private void Connect(string connectionString)
        {
            try
            {
                using var conn = new NpgsqlConnection(connectionString);

                conn.Open();

                Hide();
                var controlPanel = new ControlPanel(connectionString);
                controlPanel.FormClosed += (s, args) => Close();

                controlPanel.Show();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка авторизации: {ex.Message}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

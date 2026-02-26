namespace LibraryApplication
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Login = new TextBox();
            Password = new TextBox();
            label1 = new Label();
            label2 = new Label();
            AuthButton = new Button();
            SuspendLayout();
            // 
            // Login
            // 
            Login.Location = new Point(107, 72);
            Login.Name = "Login";
            Login.Size = new Size(251, 23);
            Login.TabIndex = 0;
            // 
            // Password
            // 
            Password.Location = new Point(107, 115);
            Password.Name = "Password";
            Password.Size = new Size(251, 23);
            Password.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 72);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 2;
            label1.Text = "Логин";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 123);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 3;
            label2.Text = "Пароль";
            // 
            // AuthButton
            // 
            AuthButton.Location = new Point(174, 154);
            AuthButton.Name = "AuthButton";
            AuthButton.Size = new Size(100, 33);
            AuthButton.TabIndex = 4;
            AuthButton.Text = "Авторизация";
            AuthButton.UseVisualStyleBackColor = true;
            AuthButton.Click += AuthButton_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(476, 202);
            Controls.Add(AuthButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Password);
            Controls.Add(Login);
            Name = "LoginForm";
            Text = "Авторизация";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Login;
        private TextBox Password;
        private Label label1;
        private Label label2;
        private Button AuthButton;
    }
}

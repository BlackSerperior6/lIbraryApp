using System.Windows.Forms;

namespace LibraryApplication.InputForms
{
    partial class RedactReader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            IssuedDate = new DateTimePicker();
            LastName = new TextBox();
            FirstName = new TextBox();
            Patronymic = new TextBox();
            LastLabel = new Label();
            NameLabel = new Label();
            PatronymicLabel = new Label();
            label4 = new Label();
            Ok = new Button();
            label1 = new Label();
            BirthdayPicker = new DateTimePicker();
            SuspendLayout();
            // 
            // IssuedDate
            // 
            IssuedDate.Location = new Point(95, 99);
            IssuedDate.Name = "IssuedDate";
            IssuedDate.Size = new Size(119, 23);
            IssuedDate.TabIndex = 0;
            // 
            // LastName
            // 
            LastName.Location = new Point(95, 12);
            LastName.Name = "LastName";
            LastName.Size = new Size(119, 23);
            LastName.TabIndex = 1;
            // 
            // FirstName
            // 
            FirstName.Location = new Point(95, 41);
            FirstName.Name = "FirstName";
            FirstName.Size = new Size(119, 23);
            FirstName.TabIndex = 2;
            // 
            // Patronymic
            // 
            Patronymic.Location = new Point(95, 70);
            Patronymic.Name = "Patronymic";
            Patronymic.Size = new Size(119, 23);
            Patronymic.TabIndex = 3;
            // 
            // LastLabel
            // 
            LastLabel.AutoSize = true;
            LastLabel.Location = new Point(12, 12);
            LastLabel.Name = "LastLabel";
            LastLabel.Size = new Size(58, 15);
            LastLabel.TabIndex = 4;
            LastLabel.Text = "Фамилия";
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(12, 41);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(31, 15);
            NameLabel.TabIndex = 5;
            NameLabel.Text = "Имя";
            // 
            // PatronymicLabel
            // 
            PatronymicLabel.AutoSize = true;
            PatronymicLabel.Location = new Point(12, 70);
            PatronymicLabel.Name = "PatronymicLabel";
            PatronymicLabel.Size = new Size(58, 15);
            PatronymicLabel.TabIndex = 6;
            PatronymicLabel.Text = "Отчество";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 99);
            label4.Name = "label4";
            label4.Size = new Size(76, 15);
            label4.TabIndex = 7;
            label4.Text = "Дата выдачи";
            // 
            // Ok
            // 
            Ok.Location = new Point(69, 163);
            Ok.Name = "Ok";
            Ok.Size = new Size(75, 23);
            Ok.TabIndex = 8;
            Ok.Text = "Ок";
            Ok.UseVisualStyleBackColor = true;
            Ok.Click += Ok_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 128);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 10;
            label1.Text = "Дата рождения";
            // 
            // BirthdayPicker
            // 
            BirthdayPicker.Location = new Point(108, 128);
            BirthdayPicker.Name = "BirthdayPicker";
            BirthdayPicker.Size = new Size(119, 23);
            BirthdayPicker.TabIndex = 9;
            // 
            // RedactReader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(236, 201);
            Controls.Add(label1);
            Controls.Add(BirthdayPicker);
            Controls.Add(Ok);
            Controls.Add(label4);
            Controls.Add(PatronymicLabel);
            Controls.Add(NameLabel);
            Controls.Add(LastLabel);
            Controls.Add(Patronymic);
            Controls.Add(FirstName);
            Controls.Add(LastName);
            Controls.Add(IssuedDate);
            Name = "RedactReader";
            Text = "Читатель";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker IssuedDate;
        private TextBox LastName;
        private TextBox FirstName;
        private TextBox Patronymic;
        private Label LastLabel;
        private Label NameLabel;
        private Label PatronymicLabel;
        private Label label4;
        private Button Ok;
        private Label label1;
        private DateTimePicker BirthdayPicker;
    }
}
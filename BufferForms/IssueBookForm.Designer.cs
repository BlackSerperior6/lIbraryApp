namespace LibraryApplication.BufferForms
{
    partial class IssueBookForm
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
            ReaderId = new TextBox();
            BookId = new TextBox();
            IssueDate = new DateTimePicker();
            RetDate = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            Ok = new Button();
            SuspendLayout();
            // 
            // ReaderId
            // 
            ReaderId.Location = new Point(103, 23);
            ReaderId.Name = "ReaderId";
            ReaderId.Size = new Size(100, 23);
            ReaderId.TabIndex = 0;
            // 
            // BookId
            // 
            BookId.Location = new Point(103, 52);
            BookId.Name = "BookId";
            BookId.Size = new Size(100, 23);
            BookId.TabIndex = 1;
            // 
            // IssueDate
            // 
            IssueDate.Location = new Point(103, 81);
            IssueDate.Name = "IssueDate";
            IssueDate.Size = new Size(122, 23);
            IssueDate.TabIndex = 2;
            // 
            // RetDate
            // 
            RetDate.Location = new Point(130, 110);
            RetDate.Name = "RetDate";
            RetDate.Size = new Size(122, 23);
            RetDate.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(73, 15);
            label1.TabIndex = 4;
            label1.Text = "ID читателя:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 52);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 5;
            label2.Text = "ID книги:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 81);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 6;
            label3.Text = "Дата выдачи:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 110);
            label4.Name = "label4";
            label4.Size = new Size(112, 15);
            label4.TabIndex = 7;
            label4.Text = "Дата возвращения:";
            // 
            // Ok
            // 
            Ok.Location = new Point(130, 148);
            Ok.Name = "Ok";
            Ok.Size = new Size(75, 23);
            Ok.TabIndex = 8;
            Ok.Text = "Ок";
            Ok.UseVisualStyleBackColor = true;
            Ok.Click += Ok_Click;
            // 
            // InputForm1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(372, 183);
            Controls.Add(Ok);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(RetDate);
            Controls.Add(IssueDate);
            Controls.Add(BookId);
            Controls.Add(ReaderId);
            Name = "InputForm1";
            Text = "Ввод данных";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ReaderId;
        private TextBox BookId;
        private DateTimePicker IssueDate;
        private DateTimePicker RetDate;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button Ok;
    }
}
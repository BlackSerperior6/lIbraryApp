namespace LibraryApplication.BufferForms
{
    partial class ReturnBookForm
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
            label1 = new Label();
            Ok = new Button();
            RetDate = new DateTimePicker();
            label4 = new Label();
            SuspendLayout();
            // 
            // ReaderId
            // 
            ReaderId.Location = new Point(112, 20);
            ReaderId.Name = "ReaderId";
            ReaderId.Size = new Size(100, 23);
            ReaderId.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 4;
            label1.Text = "ID взятия книги:";
            // 
            // Ok
            // 
            Ok.Location = new Point(86, 81);
            Ok.Name = "Ok";
            Ok.Size = new Size(75, 23);
            Ok.TabIndex = 8;
            Ok.Text = "Ок";
            Ok.UseVisualStyleBackColor = true;
            Ok.Click += Ok_Click;
            // 
            // RetDate
            // 
            RetDate.Location = new Point(131, 52);
            RetDate.Name = "RetDate";
            RetDate.Size = new Size(122, 23);
            RetDate.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 52);
            label4.Name = "label4";
            label4.Size = new Size(112, 15);
            label4.TabIndex = 7;
            label4.Text = "Дата возвращения:";
            // 
            // ReturnBookForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(265, 114);
            Controls.Add(Ok);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(RetDate);
            Controls.Add(ReaderId);
            Name = "ReturnBookForm";
            Text = "Ввод данных";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ReaderId;
        private Label label1;
        private Button Ok;
        private DateTimePicker RetDate;
        private Label label4;
    }
}
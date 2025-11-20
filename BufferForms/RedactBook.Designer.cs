namespace LibraryApplication.InputForms
{
    partial class RedactBook
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
            Title = new TextBox();
            Author = new TextBox();
            ReleaseDate = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            OkButton = new Button();
            SuspendLayout();
            // 
            // Title
            // 
            Title.Location = new Point(92, 29);
            Title.Name = "Title";
            Title.Size = new Size(100, 23);
            Title.TabIndex = 0;
            // 
            // Author
            // 
            Author.Location = new Point(92, 58);
            Author.Name = "Author";
            Author.Size = new Size(100, 23);
            Author.TabIndex = 1;
            // 
            // ReleaseDate
            // 
            ReleaseDate.Location = new Point(92, 87);
            ReleaseDate.Name = "ReleaseDate";
            ReleaseDate.Size = new Size(113, 23);
            ReleaseDate.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 29);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 3;
            label1.Text = "Название";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 61);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 4;
            label2.Text = "Автор";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 87);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 5;
            label3.Text = "Дата выхода";
            // 
            // OkButton
            // 
            OkButton.Location = new Point(71, 116);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(75, 23);
            OkButton.TabIndex = 6;
            OkButton.Text = "Ок";
            OkButton.UseVisualStyleBackColor = true;
            OkButton.Click += OkButton_Click;
            // 
            // AddBookForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(215, 149);
            Controls.Add(OkButton);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(ReleaseDate);
            Controls.Add(Author);
            Controls.Add(Title);
            Name = "AddBookForm";
            Text = "Книга";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Title;
        private TextBox Author;
        private DateTimePicker ReleaseDate;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button OkButton;
    }
}
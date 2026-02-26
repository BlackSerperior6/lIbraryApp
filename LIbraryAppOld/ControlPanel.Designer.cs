namespace LibraryApplication
{
    partial class ControlPanel
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
            AddReader = new Button();
            DeleteReader = new Button();
            RedactReader = new Button();
            InfoReader = new Button();
            GroupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            AddBook = new Button();
            RemoveBook = new Button();
            RedactBook = new Button();
            InfoBook = new Button();
            groupBox3 = new GroupBox();
            DatesReturn = new Button();
            BorrowedDates = new Button();
            ReadersBook = new Button();
            groupBox4 = new GroupBox();
            BorrowBook = new Button();
            ReturnBook = new Button();
            GroupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // AddReader
            // 
            AddReader.Location = new Point(6, 22);
            AddReader.Name = "AddReader";
            AddReader.Size = new Size(114, 40);
            AddReader.TabIndex = 0;
            AddReader.Text = "Добавить читателя";
            AddReader.UseVisualStyleBackColor = true;
            AddReader.Click += AddReader_Click;
            // 
            // DeleteReader
            // 
            DeleteReader.Location = new Point(6, 89);
            DeleteReader.Name = "DeleteReader";
            DeleteReader.Size = new Size(114, 40);
            DeleteReader.TabIndex = 1;
            DeleteReader.Text = "Удалить читателя";
            DeleteReader.UseVisualStyleBackColor = true;
            DeleteReader.Click += DeleteReader_Click;
            // 
            // RedactReader
            // 
            RedactReader.Location = new Point(6, 149);
            RedactReader.Name = "RedactReader";
            RedactReader.Size = new Size(114, 40);
            RedactReader.TabIndex = 2;
            RedactReader.Text = "Редактировать читателя";
            RedactReader.UseVisualStyleBackColor = true;
            RedactReader.Click += RedactReader_Click;
            // 
            // InfoReader
            // 
            InfoReader.Location = new Point(6, 215);
            InfoReader.Name = "InfoReader";
            InfoReader.Size = new Size(114, 40);
            InfoReader.TabIndex = 3;
            InfoReader.Text = "Информация о читателе";
            InfoReader.UseVisualStyleBackColor = true;
            InfoReader.Click += InfoReader_Click;
            // 
            // GroupBox1
            // 
            GroupBox1.Controls.Add(AddReader);
            GroupBox1.Controls.Add(DeleteReader);
            GroupBox1.Controls.Add(RedactReader);
            GroupBox1.Controls.Add(InfoReader);
            GroupBox1.Location = new Point(8, 12);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(161, 278);
            GroupBox1.TabIndex = 11;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "Управление читателями";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(AddBook);
            groupBox2.Controls.Add(RemoveBook);
            groupBox2.Controls.Add(RedactBook);
            groupBox2.Controls.Add(InfoBook);
            groupBox2.Location = new Point(218, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(150, 278);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Управление каталогом";
            // 
            // AddBook
            // 
            AddBook.Location = new Point(6, 22);
            AddBook.Name = "AddBook";
            AddBook.Size = new Size(114, 40);
            AddBook.TabIndex = 0;
            AddBook.Text = "Добавить книгу";
            AddBook.UseVisualStyleBackColor = true;
            AddBook.Click += AddBook_Click;
            // 
            // RemoveBook
            // 
            RemoveBook.Location = new Point(6, 89);
            RemoveBook.Name = "RemoveBook";
            RemoveBook.Size = new Size(114, 40);
            RemoveBook.TabIndex = 1;
            RemoveBook.Text = "Удалить книгу";
            RemoveBook.UseVisualStyleBackColor = true;
            RemoveBook.Click += RemoveBook_Click;
            // 
            // RedactBook
            // 
            RedactBook.Location = new Point(6, 149);
            RedactBook.Name = "RedactBook";
            RedactBook.Size = new Size(114, 40);
            RedactBook.TabIndex = 2;
            RedactBook.Text = "Редактирование книги";
            RedactBook.UseVisualStyleBackColor = true;
            RedactBook.Click += RedactBook_Click;
            // 
            // InfoBook
            // 
            InfoBook.Location = new Point(6, 215);
            InfoBook.Name = "InfoBook";
            InfoBook.Size = new Size(114, 40);
            InfoBook.TabIndex = 3;
            InfoBook.Text = "Информация о книге";
            InfoBook.UseVisualStyleBackColor = true;
            InfoBook.Click += InfoBook_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(DatesReturn);
            groupBox3.Controls.Add(BorrowedDates);
            groupBox3.Controls.Add(ReadersBook);
            groupBox3.Location = new Point(428, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(161, 237);
            groupBox3.TabIndex = 13;
            groupBox3.TabStop = false;
            groupBox3.Text = "Справочная информация";
            // 
            // DatesReturn
            // 
            DatesReturn.Location = new Point(28, 65);
            DatesReturn.Name = "DatesReturn";
            DatesReturn.Size = new Size(114, 61);
            DatesReturn.TabIndex = 0;
            DatesReturn.Text = "Список книг, возвращенных за период";
            DatesReturn.UseVisualStyleBackColor = true;
            DatesReturn.Click += DatesReturn_Click;
            // 
            // BorrowedDates
            // 
            BorrowedDates.Location = new Point(28, 132);
            BorrowedDates.Name = "BorrowedDates";
            BorrowedDates.Size = new Size(114, 47);
            BorrowedDates.TabIndex = 1;
            BorrowedDates.Text = "Список книг, взятых за период";
            BorrowedDates.UseVisualStyleBackColor = true;
            BorrowedDates.Click += BorrowedDates_Click;
            // 
            // ReadersBook
            // 
            ReadersBook.Location = new Point(28, 185);
            ReadersBook.Name = "ReadersBook";
            ReadersBook.Size = new Size(114, 40);
            ReadersBook.TabIndex = 2;
            ReadersBook.Text = "Список книг у читателя";
            ReadersBook.UseVisualStyleBackColor = true;
            ReadersBook.Click += ReadersBook_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(BorrowBook);
            groupBox4.Controls.Add(ReturnBook);
            groupBox4.Location = new Point(619, 22);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(161, 196);
            groupBox4.TabIndex = 14;
            groupBox4.TabStop = false;
            groupBox4.Text = "Управление выдачей/приемом";
            // 
            // BorrowBook
            // 
            BorrowBook.Location = new Point(31, 65);
            BorrowBook.Name = "BorrowBook";
            BorrowBook.Size = new Size(114, 38);
            BorrowBook.TabIndex = 3;
            BorrowBook.Text = "Выдать книгу читателю";
            BorrowBook.UseVisualStyleBackColor = true;
            BorrowBook.Click += BorrowBook_Click;
            // 
            // ReturnBook
            // 
            ReturnBook.Location = new Point(31, 129);
            ReturnBook.Name = "ReturnBook";
            ReturnBook.Size = new Size(114, 47);
            ReturnBook.TabIndex = 4;
            ReturnBook.Text = "Вернуть книгу от читателя";
            ReturnBook.UseVisualStyleBackColor = true;
            ReturnBook.Click += ReturnBook_Click;
            // 
            // ControlPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(792, 313);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(GroupBox1);
            Name = "ControlPanel";
            Text = "Панель управления";
            GroupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button AddReader;
        private Button DeleteReader;
        private Button RedactReader;
        private Button InfoReader;
        private Button button8;
        private Button button9;
        private Button button10;
        private Button button11;
        private GroupBox GroupBox1;
        private GroupBox groupBox2;
        private Button AddBook;
        private Button RemoveBook;
        private Button RedactBook;
        private Button InfoBook;
        private GroupBox groupBox3;
        private Button DatesReturn;
        private Button BorrowedDates;
        private Button ReadersBook;
        private GroupBox groupBox4;
        private Button BorrowBook;
        private Button ReturnBook;
    }
}
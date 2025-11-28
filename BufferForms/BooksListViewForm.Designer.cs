namespace LibraryApplication.BufferForms
{
    partial class BooksListView
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
            MainGrid = new DataGridView();
            Title = new DataGridViewTextBoxColumn();
            Author = new DataGridViewTextBoxColumn();
            ReleaseDate = new DataGridViewTextBoxColumn();
            ColumnArrival = new DataGridViewTextBoxColumn();
            ColumnBorrowDate = new DataGridViewTextBoxColumn();
            ColumnDateReturn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)MainGrid).BeginInit();
            SuspendLayout();
            // 
            // MainGrid
            // 
            MainGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MainGrid.Columns.AddRange(new DataGridViewColumn[] { Title, Author, ReleaseDate, ColumnArrival, ColumnBorrowDate, ColumnDateReturn });
            MainGrid.Location = new Point(0, -1);
            MainGrid.Name = "MainGrid";
            MainGrid.Size = new Size(642, 529);
            MainGrid.TabIndex = 0;
            // 
            // Title
            // 
            Title.HeaderText = "Название";
            Title.Name = "Title";
            Title.ReadOnly = true;
            // 
            // Author
            // 
            Author.HeaderText = "Автор";
            Author.Name = "Author";
            Author.ReadOnly = true;
            // 
            // ReleaseDate
            // 
            ReleaseDate.HeaderText = "Дата Выпуска";
            ReleaseDate.Name = "ReleaseDate";
            ReleaseDate.ReadOnly = true;
            // 
            // ColumnArrival
            // 
            ColumnArrival.HeaderText = "Дата поступления";
            ColumnArrival.Name = "ColumnArrival";
            ColumnArrival.ReadOnly = true;
            // 
            // ColumnBorrowDate
            // 
            ColumnBorrowDate.HeaderText = "Дата взятия";
            ColumnBorrowDate.Name = "ColumnBorrowDate";
            ColumnBorrowDate.ReadOnly = true;
            // 
            // ColumnDateReturn
            // 
            ColumnDateReturn.HeaderText = "Дата возвращения";
            ColumnDateReturn.Name = "ColumnDateReturn";
            ColumnDateReturn.ReadOnly = true;
            // 
            // BooksListView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(642, 524);
            Controls.Add(MainGrid);
            Name = "BooksListView";
            Text = "Показ данных";
            ((System.ComponentModel.ISupportInitialize)MainGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView MainGrid;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn Author;
        private DataGridViewTextBoxColumn ReleaseDate;
        private DataGridViewTextBoxColumn ColumnArrival;
        private DataGridViewTextBoxColumn ColumnBorrowDate;
        private DataGridViewTextBoxColumn ColumnDateReturn;
    }
}
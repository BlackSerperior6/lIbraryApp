namespace LibraryApplication.BufferForms
{
    partial class DatesPickerForm
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
            FirstDate = new DateTimePicker();
            SecondDate = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            OkButton = new Button();
            SuspendLayout();
            // 
            // FirstDate
            // 
            FirstDate.Location = new Point(111, 22);
            FirstDate.Name = "FirstDate";
            FirstDate.Size = new Size(200, 23);
            FirstDate.TabIndex = 0;
            // 
            // SecondDate
            // 
            SecondDate.Location = new Point(111, 51);
            SecondDate.Name = "SecondDate";
            SecondDate.Size = new Size(200, 23);
            SecondDate.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(74, 15);
            label1.TabIndex = 2;
            label1.Text = "Дата начала";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 51);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 3;
            label2.Text = "Дата конца";
            // 
            // OkButton
            // 
            OkButton.Location = new Point(120, 81);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(75, 23);
            OkButton.TabIndex = 4;
            OkButton.Text = "Ок";
            OkButton.UseVisualStyleBackColor = true;
            OkButton.Click += OkButton_Click;
            // 
            // DatesPickerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(331, 116);
            Controls.Add(OkButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(SecondDate);
            Controls.Add(FirstDate);
            Name = "DatesPickerForm";
            Text = "DatesPickerForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker FirstDate;
        private DateTimePicker SecondDate;
        private Label label1;
        private Label label2;
        private Button OkButton;
    }
}
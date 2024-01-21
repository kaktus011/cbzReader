namespace cbzReader.Forms
{
    partial class DeleteComic
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
            listBox = new ListBox();
            DeleteSelectedBtn = new Button();
            SuspendLayout();
            // 
            // listBox
            // 
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 15;
            listBox.Location = new Point(12, 9);
            listBox.Name = "listBox";
            listBox.Size = new Size(559, 379);
            listBox.TabIndex = 0;
            // 
            // DeleteSelectedBtn
            // 
            DeleteSelectedBtn.Font = new Font("Segoe UI", 13F);
            DeleteSelectedBtn.Location = new Point(435, 394);
            DeleteSelectedBtn.Name = "DeleteSelectedBtn";
            DeleteSelectedBtn.Size = new Size(136, 40);
            DeleteSelectedBtn.TabIndex = 1;
            DeleteSelectedBtn.Text = "Delete";
            DeleteSelectedBtn.UseVisualStyleBackColor = true;
            DeleteSelectedBtn.Click += DeleteSelectedBtn_Click;
            // 
            // DeleteComic
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(583, 440);
            Controls.Add(DeleteSelectedBtn);
            Controls.Add(listBox);
            Name = "DeleteComic";
            Text = "DeleteComic";
            Load += DeleteComic_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox;
        private Button DeleteSelectedBtn;
    }
}
namespace cbzReader.Forms
{
    partial class Library
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
            ImportBtn = new Button();
            SuspendLayout();
            // 
            // ImportBtn
            // 
            ImportBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ImportBtn.Location = new Point(12, 12);
            ImportBtn.Name = "ImportBtn";
            ImportBtn.Size = new Size(129, 44);
            ImportBtn.TabIndex = 0;
            ImportBtn.Text = "Import";
            ImportBtn.UseVisualStyleBackColor = true;
            ImportBtn.Click += ImportBtn_Click;
            // 
            // Library
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(589, 416);
            Controls.Add(ImportBtn);
            Name = "Library";
            Text = "Library";
            ResumeLayout(false);
        }

        #endregion

        private Button importBtn;
        private Button ImportBtn;
    }
}

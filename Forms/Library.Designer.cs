namespace cbzReader
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
            importBtn = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // importBtn
            // 
            importBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            importBtn.Location = new Point(12, 12);
            importBtn.Name = "importBtn";
            importBtn.Size = new Size(129, 44);
            importBtn.TabIndex = 0;
            importBtn.Text = "Import";
            importBtn.UseVisualStyleBackColor = true;
            importBtn.Click += importBtn_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(27, 76);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 133);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // Library
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(581, 401);
            Controls.Add(pictureBox1);
            Controls.Add(importBtn);
            MinimumSize = new Size(597, 440);
            Name = "Library";
            Text = "Library";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button importBtn;
        private PictureBox pictureBox1;
    }
}

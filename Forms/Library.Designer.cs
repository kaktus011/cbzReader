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
            importProgBar = new ProgressBar();
            importingLbl = new Label();
            RestoreBtn = new Button();
            SuspendLayout();
            // 
            // ImportBtn
            // 
            ImportBtn.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ImportBtn.Location = new Point(12, 12);
            ImportBtn.Name = "ImportBtn";
            ImportBtn.Size = new Size(129, 44);
            ImportBtn.TabIndex = 0;
            ImportBtn.Text = "Import";
            ImportBtn.UseVisualStyleBackColor = true;
            ImportBtn.Click += ImportBtn_Click;
            // 
            // importProgBar
            // 
            importProgBar.Location = new Point(243, 23);
            importProgBar.Name = "importProgBar";
            importProgBar.Size = new Size(145, 16);
            importProgBar.Step = 5;
            importProgBar.Style = ProgressBarStyle.Continuous;
            importProgBar.TabIndex = 1;
            importProgBar.Visible = false;
            // 
            // importingLbl
            // 
            importingLbl.AutoSize = true;
            importingLbl.Font = new Font("Segoe UI", 11F);
            importingLbl.Location = new Point(162, 19);
            importingLbl.Name = "importingLbl";
            importingLbl.Size = new Size(78, 20);
            importingLbl.TabIndex = 2;
            importingLbl.Text = "Importing:";
            importingLbl.Visible = false;
            // 
            // RestoreBtn
            // 
            RestoreBtn.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RestoreBtn.Location = new Point(448, 12);
            RestoreBtn.Name = "RestoreBtn";
            RestoreBtn.Size = new Size(129, 44);
            RestoreBtn.TabIndex = 3;
            RestoreBtn.Text = "Restore";
            RestoreBtn.UseVisualStyleBackColor = true;
            RestoreBtn.Click += RestoreBtn_Click;
            // 
            // Library
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(589, 416);
            Controls.Add(RestoreBtn);
            Controls.Add(importingLbl);
            Controls.Add(importProgBar);
            Controls.Add(ImportBtn);
            DoubleBuffered = true;
            Name = "Library";
            Text = "Library";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button importBtn;
        private Button ImportBtn;
        private ProgressBar importProgBar;
        private Label importingLbl;
        private Button RestoreBtn;
    }
}

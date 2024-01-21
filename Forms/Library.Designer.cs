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
            DeleteBtn = new Button();
            SuspendLayout();
            // 
            // ImportBtn
            // 
            ImportBtn.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ImportBtn.Location = new Point(12, 13);
            ImportBtn.Name = "ImportBtn";
            ImportBtn.Size = new Size(129, 46);
            ImportBtn.TabIndex = 0;
            ImportBtn.Text = "Import";
            ImportBtn.UseVisualStyleBackColor = true;
            ImportBtn.Click += ImportBtn_Click;
            // 
            // importProgBar
            // 
            importProgBar.Location = new Point(147, 32);
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
            importingLbl.Location = new Point(177, 9);
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
            RestoreBtn.Size = new Size(129, 47);
            RestoreBtn.TabIndex = 3;
            RestoreBtn.Text = "Restore";
            RestoreBtn.UseVisualStyleBackColor = true;
            RestoreBtn.Click += RestoreBtn_Click;
            // 
            // DeleteBtn
            // 
            DeleteBtn.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DeleteBtn.Location = new Point(313, 11);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(129, 48);
            DeleteBtn.TabIndex = 4;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = true;
            DeleteBtn.Click += DeleteBtn_Click;
            // 
            // Library
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(589, 416);
            Controls.Add(DeleteBtn);
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
        private Button DeleteBtn;
    }
}

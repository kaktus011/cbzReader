namespace cbzReader.Forms
{
    partial class Reader
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
            picBox = new PictureBox();
            NextBtn = new Button();
            PrevBtn = new Button();
            progressBar = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            SuspendLayout();
            // 
            // picBox
            // 
            picBox.Location = new Point(59, 12);
            picBox.Name = "picBox";
            picBox.Size = new Size(794, 1123);
            picBox.TabIndex = 0;
            picBox.TabStop = false;
            // 
            // NextBtn
            // 
            NextBtn.Location = new Point(859, 12);
            NextBtn.Name = "NextBtn";
            NextBtn.Size = new Size(45, 1123);
            NextBtn.TabIndex = 1;
            NextBtn.Text = "next";
            NextBtn.UseVisualStyleBackColor = true;
            NextBtn.Click += NextBtn_Click;
            // 
            // PrevBtn
            // 
            PrevBtn.Location = new Point(4, 12);
            PrevBtn.Name = "PrevBtn";
            PrevBtn.Size = new Size(49, 1123);
            PrevBtn.TabIndex = 2;
            PrevBtn.Text = "prev";
            PrevBtn.UseVisualStyleBackColor = true;
            PrevBtn.Click += PrevBtn_Click;
            // 
            // progressBar
            // 
            progressBar.ForeColor = Color.SpringGreen;
            progressBar.Location = new Point(331, 1139);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(238, 11);
            progressBar.Step = 1;
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 3;
            // 
            // Reader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(916, 1155);
            Controls.Add(progressBar);
            Controls.Add(PrevBtn);
            Controls.Add(NextBtn);
            Controls.Add(picBox);
            Name = "Reader";
            Text = "Reader";
            Load += Reader_Load;
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picBox;
        private Button NextBtn;
        private Button PrevBtn;
        private ProgressBar progressBar;
    }
}
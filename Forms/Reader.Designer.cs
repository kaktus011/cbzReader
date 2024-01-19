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
            nextBtn = new Button();
            prevBtn = new Button();
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
            // nextBtn
            // 
            nextBtn.Location = new Point(859, 12);
            nextBtn.Name = "nextBtn";
            nextBtn.Size = new Size(45, 1123);
            nextBtn.TabIndex = 1;
            nextBtn.Text = "next";
            nextBtn.UseVisualStyleBackColor = true;
            nextBtn.Click += nextBtn_Click;
            // 
            // prevBtn
            // 
            prevBtn.Location = new Point(4, 12);
            prevBtn.Name = "prevBtn";
            prevBtn.Size = new Size(49, 1123);
            prevBtn.TabIndex = 2;
            prevBtn.Text = "prev";
            prevBtn.UseVisualStyleBackColor = true;
            prevBtn.Click += prevBtn_Click;
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
            Controls.Add(prevBtn);
            Controls.Add(nextBtn);
            Controls.Add(picBox);
            Name = "Reader";
            Text = "Reader";
            Load += Reader_Load;
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picBox;
        private Button nextBtn;
        private Button prevBtn;
        private ProgressBar progressBar;
    }
}
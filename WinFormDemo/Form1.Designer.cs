namespace WinFormDemo
{
    partial class Form1
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
            this.btnBeginWF = new System.Windows.Forms.Button();
            this.textBookmarkName = new System.Windows.Forms.TextBox();
            this.btnBookmarkContinue = new System.Windows.Forms.Button();
            this.textBoxContinue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnBeginWF
            // 
            this.btnBeginWF.Location = new System.Drawing.Point(158, 67);
            this.btnBeginWF.Name = "btnBeginWF";
            this.btnBeginWF.Size = new System.Drawing.Size(119, 33);
            this.btnBeginWF.TabIndex = 0;
            this.btnBeginWF.Text = "Begin workflow";
            this.btnBeginWF.UseVisualStyleBackColor = true;
            this.btnBeginWF.Click += new System.EventHandler(this.btnBeginWF_Click);
            // 
            // textBookmarkName
            // 
            this.textBookmarkName.Location = new System.Drawing.Point(358, 79);
            this.textBookmarkName.Name = "textBookmarkName";
            this.textBookmarkName.Size = new System.Drawing.Size(100, 20);
            this.textBookmarkName.TabIndex = 1;
            this.textBookmarkName.Text = "bookmark";
            // 
            // btnBookmarkContinue
            // 
            this.btnBookmarkContinue.Location = new System.Drawing.Point(185, 140);
            this.btnBookmarkContinue.Name = "btnBookmarkContinue";
            this.btnBookmarkContinue.Size = new System.Drawing.Size(75, 23);
            this.btnBookmarkContinue.TabIndex = 2;
            this.btnBookmarkContinue.Text = "Continue bookmark";
            this.btnBookmarkContinue.UseVisualStyleBackColor = true;
            this.btnBookmarkContinue.Click += new System.EventHandler(this.btnBookmarkContinue_Click);
            // 
            // textBoxContinue
            // 
            this.textBoxContinue.Location = new System.Drawing.Point(358, 140);
            this.textBoxContinue.Name = "textBoxContinue";
            this.textBoxContinue.Size = new System.Drawing.Size(100, 20);
            this.textBoxContinue.TabIndex = 3;
            this.textBoxContinue.Text = "150";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 362);
            this.Controls.Add(this.textBoxContinue);
            this.Controls.Add(this.btnBookmarkContinue);
            this.Controls.Add(this.textBookmarkName);
            this.Controls.Add(this.btnBeginWF);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBeginWF;
        private System.Windows.Forms.TextBox textBookmarkName;
        private System.Windows.Forms.Button btnBookmarkContinue;
        private System.Windows.Forms.TextBox textBoxContinue;
    }
}


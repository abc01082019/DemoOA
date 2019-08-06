namespace WFWindowsFormsDemo
{
    partial class MainForm
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
            this.btnBegin = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.textBoxBookmark = new System.Windows.Forms.TextBox();
            this.textBoxContinue = new System.Windows.Forms.TextBox();
            this.btnStateMachine = new System.Windows.Forms.Button();
            this.textBoxGuid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBegin
            // 
            this.btnBegin.Location = new System.Drawing.Point(129, 56);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(75, 23);
            this.btnBegin.TabIndex = 0;
            this.btnBegin.Text = "Begin workflow";
            this.btnBegin.UseVisualStyleBackColor = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(129, 118);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 1;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // textBoxBookmark
            // 
            this.textBoxBookmark.Location = new System.Drawing.Point(345, 56);
            this.textBoxBookmark.Name = "textBoxBookmark";
            this.textBoxBookmark.Size = new System.Drawing.Size(100, 20);
            this.textBoxBookmark.TabIndex = 2;
            this.textBoxBookmark.Text = "bookmark";
            // 
            // textBoxContinue
            // 
            this.textBoxContinue.Location = new System.Drawing.Point(345, 118);
            this.textBoxContinue.Name = "textBoxContinue";
            this.textBoxContinue.Size = new System.Drawing.Size(100, 20);
            this.textBoxContinue.TabIndex = 3;
            this.textBoxContinue.Text = "100";
            // 
            // btnStateMachine
            // 
            this.btnStateMachine.Location = new System.Drawing.Point(129, 217);
            this.btnStateMachine.Name = "btnStateMachine";
            this.btnStateMachine.Size = new System.Drawing.Size(75, 23);
            this.btnStateMachine.TabIndex = 4;
            this.btnStateMachine.Text = "Activat StateMachine";
            this.btnStateMachine.UseVisualStyleBackColor = true;
            this.btnStateMachine.Click += new System.EventHandler(this.btnStateMachine_Click);
            // 
            // textBoxGuid
            // 
            this.textBoxGuid.Location = new System.Drawing.Point(345, 174);
            this.textBoxGuid.Name = "textBoxGuid";
            this.textBoxGuid.Size = new System.Drawing.Size(100, 20);
            this.textBoxGuid.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(169, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "GUID";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 370);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxGuid);
            this.Controls.Add(this.btnStateMachine);
            this.Controls.Add(this.textBoxContinue);
            this.Controls.Add(this.textBoxBookmark);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnBegin);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBegin;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.TextBox textBoxBookmark;
        private System.Windows.Forms.TextBox textBoxContinue;
        private System.Windows.Forms.Button btnStateMachine;
        private System.Windows.Forms.TextBox textBoxGuid;
        private System.Windows.Forms.Label label1;
    }
}
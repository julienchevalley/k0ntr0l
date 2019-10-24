namespace K0ntr0l
{
    partial class FormPreviewCommsWarning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreviewCommsWarning));
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxDoNotShowAgain = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 49);
            this.label1.TabIndex = 3;
            this.label1.Text = "Bulk sending of pattern data while the x0xb0x is playing can result in unexpected" +
    "  communication errors . You should first press the \"R/S\" button on the x0xb0x t" +
    "o stop Playback before proceeding...";
            // 
            // checkBoxDoNotShowAgain
            // 
            this.checkBoxDoNotShowAgain.AutoSize = true;
            this.checkBoxDoNotShowAgain.Location = new System.Drawing.Point(16, 66);
            this.checkBoxDoNotShowAgain.Name = "checkBoxDoNotShowAgain";
            this.checkBoxDoNotShowAgain.Size = new System.Drawing.Size(233, 17);
            this.checkBoxDoNotShowAgain.TabIndex = 1;
            this.checkBoxDoNotShowAgain.Text = "Understood, do not show this warning again";
            this.checkBoxDoNotShowAgain.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(155, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Proceed";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormPreviewCommsWarning
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 129);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxDoNotShowAgain);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPreviewCommsWarning";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pattern Preview - x0xb0x Comms Warning";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPreviewCommsWarning_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxDoNotShowAgain;
        private System.Windows.Forms.Button button1;
    }
}
namespace K0ntr0l
{
    partial class FormPianoRollConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPianoRollConfiguration));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.labelNoteColor = new System.Windows.Forms.Label();
            this.labelAccentedNoteColor = new System.Windows.Forms.Label();
            this.labelRestColor = new System.Windows.Forms.Label();
            this.labelAccentedRestColor = new System.Windows.Forms.Label();
            this.labelSlideColor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Note Color : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Accented Note Color : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Rest Color : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Accented Rest Color : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Slide Color : ";
            // 
            // labelNoteColor
            // 
            this.labelNoteColor.Location = new System.Drawing.Point(143, 13);
            this.labelNoteColor.Name = "labelNoteColor";
            this.labelNoteColor.Size = new System.Drawing.Size(100, 13);
            this.labelNoteColor.TabIndex = 2;
            this.labelNoteColor.Click += new System.EventHandler(this.labelNoteColor_Click);
            // 
            // labelAccentedNoteColor
            // 
            this.labelAccentedNoteColor.Location = new System.Drawing.Point(143, 39);
            this.labelAccentedNoteColor.Name = "labelAccentedNoteColor";
            this.labelAccentedNoteColor.Size = new System.Drawing.Size(100, 13);
            this.labelAccentedNoteColor.TabIndex = 2;
            this.labelAccentedNoteColor.Click += new System.EventHandler(this.labelAccentedNoteColor_Click);
            // 
            // labelRestColor
            // 
            this.labelRestColor.Location = new System.Drawing.Point(143, 66);
            this.labelRestColor.Name = "labelRestColor";
            this.labelRestColor.Size = new System.Drawing.Size(100, 13);
            this.labelRestColor.TabIndex = 2;
            this.labelRestColor.Click += new System.EventHandler(this.labelRestColor_Click);
            // 
            // labelAccentedRestColor
            // 
            this.labelAccentedRestColor.Location = new System.Drawing.Point(143, 90);
            this.labelAccentedRestColor.Name = "labelAccentedRestColor";
            this.labelAccentedRestColor.Size = new System.Drawing.Size(100, 13);
            this.labelAccentedRestColor.TabIndex = 2;
            this.labelAccentedRestColor.Click += new System.EventHandler(this.labelAccentedRestColor_Click);
            // 
            // labelSlideColor
            // 
            this.labelSlideColor.Location = new System.Drawing.Point(143, 118);
            this.labelSlideColor.Name = "labelSlideColor";
            this.labelSlideColor.Size = new System.Drawing.Size(100, 13);
            this.labelSlideColor.TabIndex = 2;
            this.labelSlideColor.Click += new System.EventHandler(this.labelSliceColor_Click);
            // 
            // FormPianoRollConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 149);
            this.Controls.Add(this.labelSlideColor);
            this.Controls.Add(this.labelAccentedRestColor);
            this.Controls.Add(this.labelRestColor);
            this.Controls.Add(this.labelAccentedNoteColor);
            this.Controls.Add(this.labelNoteColor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPianoRollConfiguration";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Piano Roll Configuration";
            this.Load += new System.EventHandler(this.FormPianoRollConfiguration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label labelNoteColor;
        private System.Windows.Forms.Label labelAccentedNoteColor;
        private System.Windows.Forms.Label labelRestColor;
        private System.Windows.Forms.Label labelAccentedRestColor;
        private System.Windows.Forms.Label labelSlideColor;
    }
}
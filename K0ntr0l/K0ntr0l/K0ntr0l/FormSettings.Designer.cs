namespace K0ntr0l
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownWaitForByteSleepTime = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownReplyReadTimeOut = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownIOTimeOut = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxAjustTempo = new System.Windows.Forms.CheckBox();
            this.checkBoxPreviewPatterns = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonPianoRollColors = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxVersionCheck = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBoxShowToolTips = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButtonOpenPatternLibraryBrowser = new System.Windows.Forms.RadioButton();
            this.radioButtonOpenFileBrowser = new System.Windows.Forms.RadioButton();
            this.buttonRestoreDefaultSettings = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitForByteSleepTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReplyReadTimeOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIOTimeOut)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDownWaitForByteSleepTime);
            this.groupBox1.Controls.Add(this.numericUpDownReplyReadTimeOut);
            this.groupBox1.Controls.Add(this.numericUpDownIOTimeOut);
            this.groupBox1.Location = new System.Drawing.Point(12, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(395, 122);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "x0xb0x Communications";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Wait for Byte Sleep Time:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(270, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Milliseconds";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Milliseconds";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Milliseconds";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Reply Read Timeout:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "IO Timeout:";
            // 
            // numericUpDownWaitForByteSleepTime
            // 
            this.numericUpDownWaitForByteSleepTime.Location = new System.Drawing.Point(144, 90);
            this.numericUpDownWaitForByteSleepTime.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownWaitForByteSleepTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWaitForByteSleepTime.Name = "numericUpDownWaitForByteSleepTime";
            this.numericUpDownWaitForByteSleepTime.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownWaitForByteSleepTime.TabIndex = 9;
            this.numericUpDownWaitForByteSleepTime.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numericUpDownReplyReadTimeOut
            // 
            this.numericUpDownReplyReadTimeOut.Location = new System.Drawing.Point(144, 55);
            this.numericUpDownReplyReadTimeOut.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownReplyReadTimeOut.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownReplyReadTimeOut.Name = "numericUpDownReplyReadTimeOut";
            this.numericUpDownReplyReadTimeOut.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownReplyReadTimeOut.TabIndex = 8;
            this.numericUpDownReplyReadTimeOut.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDownIOTimeOut
            // 
            this.numericUpDownIOTimeOut.Location = new System.Drawing.Point(144, 24);
            this.numericUpDownIOTimeOut.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownIOTimeOut.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownIOTimeOut.Name = "numericUpDownIOTimeOut";
            this.numericUpDownIOTimeOut.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownIOTimeOut.TabIndex = 7;
            this.numericUpDownIOTimeOut.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxAjustTempo);
            this.groupBox2.Controls.Add(this.checkBoxPreviewPatterns);
            this.groupBox2.Location = new System.Drawing.Point(12, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 80);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Pattern Audition";
            // 
            // checkBoxAjustTempo
            // 
            this.checkBoxAjustTempo.AutoSize = true;
            this.checkBoxAjustTempo.Location = new System.Drawing.Point(11, 52);
            this.checkBoxAjustTempo.Name = "checkBoxAjustTempo";
            this.checkBoxAjustTempo.Size = new System.Drawing.Size(137, 17);
            this.checkBoxAjustTempo.TabIndex = 5;
            this.checkBoxAjustTempo.Text = "Adjust Tempo on the fly";
            this.checkBoxAjustTempo.UseVisualStyleBackColor = true;
            // 
            // checkBoxPreviewPatterns
            // 
            this.checkBoxPreviewPatterns.AutoSize = true;
            this.checkBoxPreviewPatterns.Location = new System.Drawing.Point(11, 24);
            this.checkBoxPreviewPatterns.Name = "checkBoxPreviewPatterns";
            this.checkBoxPreviewPatterns.Size = new System.Drawing.Size(230, 17);
            this.checkBoxPreviewPatterns.TabIndex = 4;
            this.checkBoxPreviewPatterns.Text = "Preview Patterns using Bank 16 Location 8";
            this.checkBoxPreviewPatterns.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonPianoRollColors);
            this.groupBox3.Location = new System.Drawing.Point(11, 299);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(395, 47);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Piano Roll";
            // 
            // buttonPianoRollColors
            // 
            this.buttonPianoRollColors.Location = new System.Drawing.Point(11, 18);
            this.buttonPianoRollColors.Name = "buttonPianoRollColors";
            this.buttonPianoRollColors.Size = new System.Drawing.Size(75, 23);
            this.buttonPianoRollColors.TabIndex = 11;
            this.buttonPianoRollColors.Text = "Colours...";
            this.buttonPianoRollColors.UseVisualStyleBackColor = true;
            this.buttonPianoRollColors.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxVersionCheck);
            this.groupBox4.Location = new System.Drawing.Point(11, 353);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(396, 44);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Update Check";
            // 
            // checkBoxVersionCheck
            // 
            this.checkBoxVersionCheck.AutoSize = true;
            this.checkBoxVersionCheck.Location = new System.Drawing.Point(12, 19);
            this.checkBoxVersionCheck.Name = "checkBoxVersionCheck";
            this.checkBoxVersionCheck.Size = new System.Drawing.Size(163, 17);
            this.checkBoxVersionCheck.TabIndex = 13;
            this.checkBoxVersionCheck.Text = "Check for updates on startup";
            this.checkBoxVersionCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBoxShowToolTips);
            this.groupBox5.Location = new System.Drawing.Point(12, 404);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(394, 43);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tool Tips";
            // 
            // checkBoxShowToolTips
            // 
            this.checkBoxShowToolTips.AutoSize = true;
            this.checkBoxShowToolTips.Location = new System.Drawing.Point(10, 19);
            this.checkBoxShowToolTips.Name = "checkBoxShowToolTips";
            this.checkBoxShowToolTips.Size = new System.Drawing.Size(100, 17);
            this.checkBoxShowToolTips.TabIndex = 15;
            this.checkBoxShowToolTips.Text = "Show Tool Tips";
            this.checkBoxShowToolTips.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButtonOpenPatternLibraryBrowser);
            this.groupBox6.Controls.Add(this.radioButtonOpenFileBrowser);
            this.groupBox6.Location = new System.Drawing.Point(12, 13);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(394, 49);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "x0xb0x View Selected Pattern Double Click Action";
            // 
            // radioButtonOpenPatternLibraryBrowser
            // 
            this.radioButtonOpenPatternLibraryBrowser.AutoSize = true;
            this.radioButtonOpenPatternLibraryBrowser.Location = new System.Drawing.Point(127, 20);
            this.radioButtonOpenPatternLibraryBrowser.Name = "radioButtonOpenPatternLibraryBrowser";
            this.radioButtonOpenPatternLibraryBrowser.Size = new System.Drawing.Size(163, 17);
            this.radioButtonOpenPatternLibraryBrowser.TabIndex = 2;
            this.radioButtonOpenPatternLibraryBrowser.TabStop = true;
            this.radioButtonOpenPatternLibraryBrowser.Text = "Open Pattern Library Browser";
            this.radioButtonOpenPatternLibraryBrowser.UseVisualStyleBackColor = true;
            // 
            // radioButtonOpenFileBrowser
            // 
            this.radioButtonOpenFileBrowser.AutoSize = true;
            this.radioButtonOpenFileBrowser.Location = new System.Drawing.Point(10, 20);
            this.radioButtonOpenFileBrowser.Name = "radioButtonOpenFileBrowser";
            this.radioButtonOpenFileBrowser.Size = new System.Drawing.Size(111, 17);
            this.radioButtonOpenFileBrowser.TabIndex = 1;
            this.radioButtonOpenFileBrowser.TabStop = true;
            this.radioButtonOpenFileBrowser.Text = "Open File Browser";
            this.radioButtonOpenFileBrowser.UseVisualStyleBackColor = true;
            // 
            // buttonRestoreDefaultSettings
            // 
            this.buttonRestoreDefaultSettings.Location = new System.Drawing.Point(257, 453);
            this.buttonRestoreDefaultSettings.Name = "buttonRestoreDefaultSettings";
            this.buttonRestoreDefaultSettings.Size = new System.Drawing.Size(149, 23);
            this.buttonRestoreDefaultSettings.TabIndex = 15;
            this.buttonRestoreDefaultSettings.Text = "Restore Default Settings";
            this.buttonRestoreDefaultSettings.UseVisualStyleBackColor = true;
            this.buttonRestoreDefaultSettings.Click += new System.EventHandler(this.buttonRestoreDefaultSettings_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 479);
            this.Controls.Add(this.buttonRestoreDefaultSettings);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "K0ntr0l Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitForByteSleepTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReplyReadTimeOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIOTimeOut)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownIOTimeOut;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownReplyReadTimeOut;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxPreviewPatterns;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonPianoRollColors;
        private System.Windows.Forms.CheckBox checkBoxAjustTempo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxVersionCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownWaitForByteSleepTime;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBoxShowToolTips;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButtonOpenPatternLibraryBrowser;
        private System.Windows.Forms.RadioButton radioButtonOpenFileBrowser;
        private System.Windows.Forms.Button buttonRestoreDefaultSettings;
    }
}
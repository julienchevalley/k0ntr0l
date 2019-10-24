namespace K0ntr0l
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBoxActivityReport = new System.Windows.Forms.ListBox();
            this.buttonDumpEeprom = new System.Windows.Forms.Button();
            this.buttonRestoreEeprom = new System.Windows.Forms.Button();
            this._textBoxPattern = new System.Windows.Forms.TextBox();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.treeViewX0x = new System.Windows.Forms.TreeView();
            this.numericUpDownX0xTempo = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseFiles = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxPatternName = new System.Windows.Forms.TextBox();
            this.buttonBrowseLibrary = new System.Windows.Forms.Button();
            this._pianoRoll = new BangOnTekCommon.PianoRoll();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItemImportPatternsDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPatternsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPatternsDirecoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadEEPROMBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveEEPROMBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerStartUpInitialisation = new System.Windows.Forms.Timer(this.components);
            this.textBoxPatternDescription = new System.Windows.Forms.TextBox();
            this.buttonBrowseFolder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX0xTempo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxActivityReport
            // 
            this.listBoxActivityReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxActivityReport.FormattingEnabled = true;
            this.listBoxActivityReport.Location = new System.Drawing.Point(11, 431);
            this.listBoxActivityReport.Name = "listBoxActivityReport";
            this.listBoxActivityReport.Size = new System.Drawing.Size(387, 82);
            this.listBoxActivityReport.TabIndex = 4;
            // 
            // buttonDumpEeprom
            // 
            this.buttonDumpEeprom.Enabled = false;
            this.buttonDumpEeprom.Location = new System.Drawing.Point(14, 19);
            this.buttonDumpEeprom.Name = "buttonDumpEeprom";
            this.buttonDumpEeprom.Size = new System.Drawing.Size(76, 23);
            this.buttonDumpEeprom.TabIndex = 10;
            this.buttonDumpEeprom.Text = "&Backup...";
            this.buttonDumpEeprom.UseVisualStyleBackColor = true;
            this.buttonDumpEeprom.Click += new System.EventHandler(this.buttonDumpEeprom_Click);
            // 
            // buttonRestoreEeprom
            // 
            this.buttonRestoreEeprom.Enabled = false;
            this.buttonRestoreEeprom.Location = new System.Drawing.Point(96, 19);
            this.buttonRestoreEeprom.Name = "buttonRestoreEeprom";
            this.buttonRestoreEeprom.Size = new System.Drawing.Size(76, 23);
            this.buttonRestoreEeprom.TabIndex = 11;
            this.buttonRestoreEeprom.Text = "&Restore...";
            this.buttonRestoreEeprom.UseVisualStyleBackColor = true;
            this.buttonRestoreEeprom.Click += new System.EventHandler(this.buttonRestoreEeprom_Click);
            // 
            // _textBoxPattern
            // 
            this._textBoxPattern.AcceptsReturn = true;
            this._textBoxPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._textBoxPattern.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._textBoxPattern.Location = new System.Drawing.Point(194, 159);
            this._textBoxPattern.Multiline = true;
            this._textBoxPattern.Name = "_textBoxPattern";
            this._textBoxPattern.ReadOnly = true;
            this._textBoxPattern.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBoxPattern.Size = new System.Drawing.Size(204, 235);
            this._textBoxPattern.TabIndex = 12;
            this._textBoxPattern.WordWrap = false;
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(9, 19);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(88, 21);
            this.comboBoxPorts.TabIndex = 16;
            this.comboBoxPorts.DropDown += new System.EventHandler(this.comboBoxPorts_DropDown);
            this.comboBoxPorts.SelectedIndexChanged += new System.EventHandler(this.comboBoxPorts_SelectedIndexChanged);
            // 
            // treeViewX0x
            // 
            this.treeViewX0x.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewX0x.HideSelection = false;
            this.treeViewX0x.Location = new System.Drawing.Point(12, 87);
            this.treeViewX0x.Name = "treeViewX0x";
            this.treeViewX0x.Size = new System.Drawing.Size(176, 307);
            this.treeViewX0x.TabIndex = 17;
            this.treeViewX0x.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewX0x_AfterSelect);
            this.treeViewX0x.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewX0x_NodeMouseClick);
            this.treeViewX0x.DoubleClick += new System.EventHandler(this.treeViewX0x_DoubleClick);
            this.treeViewX0x.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeViewX0x_MouseClick);
            // 
            // numericUpDownX0xTempo
            // 
            this.numericUpDownX0xTempo.Location = new System.Drawing.Point(17, 22);
            this.numericUpDownX0xTempo.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownX0xTempo.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownX0xTempo.Name = "numericUpDownX0xTempo";
            this.numericUpDownX0xTempo.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownX0xTempo.TabIndex = 20;
            this.numericUpDownX0xTempo.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownX0xTempo.ValueChanged += new System.EventHandler(this.numericUpDownTempo_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRestoreEeprom);
            this.groupBox1.Controls.Add(this.buttonDumpEeprom);
            this.groupBox1.Location = new System.Drawing.Point(123, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 54);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EEPROM";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxPorts);
            this.groupBox2.Location = new System.Drawing.Point(12, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(105, 54);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Port";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDownX0xTempo);
            this.groupBox3.Location = new System.Drawing.Point(312, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(86, 54);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tempo";
            // 
            // buttonBrowseFiles
            // 
            this.buttonBrowseFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBrowseFiles.Enabled = false;
            this.buttonBrowseFiles.Location = new System.Drawing.Point(11, 400);
            this.buttonBrowseFiles.Name = "buttonBrowseFiles";
            this.buttonBrowseFiles.Size = new System.Drawing.Size(98, 23);
            this.buttonBrowseFiles.TabIndex = 26;
            this.buttonBrowseFiles.Text = "Browse Files ...";
            this.buttonBrowseFiles.UseVisualStyleBackColor = true;
            this.buttonBrowseFiles.Click += new System.EventHandler(this.buttonBrowseFiles_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Select the pattern files folder...";
            // 
            // textBoxPatternName
            // 
            this.textBoxPatternName.Location = new System.Drawing.Point(194, 87);
            this.textBoxPatternName.Name = "textBoxPatternName";
            this.textBoxPatternName.ReadOnly = true;
            this.textBoxPatternName.Size = new System.Drawing.Size(204, 20);
            this.textBoxPatternName.TabIndex = 28;
            this.textBoxPatternName.TextChanged += new System.EventHandler(this.textBoxPatternName_TextChanged);
            // 
            // buttonBrowseLibrary
            // 
            this.buttonBrowseLibrary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBrowseLibrary.Enabled = false;
            this.buttonBrowseLibrary.Location = new System.Drawing.Point(247, 400);
            this.buttonBrowseLibrary.Name = "buttonBrowseLibrary";
            this.buttonBrowseLibrary.Size = new System.Drawing.Size(98, 23);
            this.buttonBrowseLibrary.TabIndex = 26;
            this.buttonBrowseLibrary.Text = "Browse Library ...";
            this.buttonBrowseLibrary.UseVisualStyleBackColor = true;
            this.buttonBrowseLibrary.Click += new System.EventHandler(this.buttonBrowseLibrary_Click);
            // 
            // _pianoRoll
            // 
            this._pianoRoll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pianoRoll.BlackKeyLength = 30;
            this._pianoRoll.KeysCount = 53;
            this._pianoRoll.Location = new System.Drawing.Point(404, 22);
            this._pianoRoll.LowestKey = BangOnTekCommon.PianoRoll.KeyName.C;
            this._pianoRoll.LowestKeyNoteValue = ((byte)(11));
            this._pianoRoll.Name = "_pianoRoll";
            this._pianoRoll.RootKeyLabel = "C-2";
            this._pianoRoll.RootKeyNoteValue = ((byte)(23));
            this._pianoRoll.Sequence = new BangOnTekCommon.PianoRollStep[0];
            this._pianoRoll.Size = new System.Drawing.Size(378, 493);
            this._pianoRoll.TabIndex = 27;
            this._pianoRoll.WhiteKeyLength = 45;
            this._pianoRoll.Click += new System.EventHandler(this._pianoRoll_Click);
            this._pianoRoll.DoubleClick += new System.EventHandler(this._pianoRoll_DoubleClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 518);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(794, 22);
            this.statusStrip.TabIndex = 30;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(395, 16);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItemImportPatternsDirectory,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(794, 24);
            this.menuStrip1.TabIndex = 31;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItemImportPatternsDirectory
            // 
            this.fileToolStripMenuItemImportPatternsDirectory.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadPatternsToolStripMenuItem,
            this.importPatternsDirecoryToolStripMenuItem,
            this.loadEEPROMBackupToolStripMenuItem,
            this.saveEEPROMBackupToolStripMenuItem});
            this.fileToolStripMenuItemImportPatternsDirectory.Name = "fileToolStripMenuItemImportPatternsDirectory";
            this.fileToolStripMenuItemImportPatternsDirectory.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItemImportPatternsDirectory.Text = "&File";
            this.fileToolStripMenuItemImportPatternsDirectory.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
            // 
            // loadPatternsToolStripMenuItem
            // 
            this.loadPatternsToolStripMenuItem.Enabled = false;
            this.loadPatternsToolStripMenuItem.Name = "loadPatternsToolStripMenuItem";
            this.loadPatternsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.loadPatternsToolStripMenuItem.Text = "&Import Pattern Files...";
            this.loadPatternsToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // importPatternsDirecoryToolStripMenuItem
            // 
            this.importPatternsDirecoryToolStripMenuItem.Enabled = false;
            this.importPatternsDirecoryToolStripMenuItem.Name = "importPatternsDirecoryToolStripMenuItem";
            this.importPatternsDirecoryToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.importPatternsDirecoryToolStripMenuItem.Text = "Import Patterns &Directory...";
            this.importPatternsDirecoryToolStripMenuItem.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // loadEEPROMBackupToolStripMenuItem
            // 
            this.loadEEPROMBackupToolStripMenuItem.Enabled = false;
            this.loadEEPROMBackupToolStripMenuItem.Name = "loadEEPROMBackupToolStripMenuItem";
            this.loadEEPROMBackupToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.loadEEPROMBackupToolStripMenuItem.Text = "&Load EEPROM Backup...";
            this.loadEEPROMBackupToolStripMenuItem.Click += new System.EventHandler(this.buttonRestoreEeprom_Click);
            // 
            // saveEEPROMBackupToolStripMenuItem
            // 
            this.saveEEPROMBackupToolStripMenuItem.Enabled = false;
            this.saveEEPROMBackupToolStripMenuItem.Name = "saveEEPROMBackupToolStripMenuItem";
            this.saveEEPROMBackupToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.saveEEPROMBackupToolStripMenuItem.Text = "&Save EEPROM Backup...";
            this.saveEEPROMBackupToolStripMenuItem.Click += new System.EventHandler(this.buttonDumpEeprom_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.settingsToolStripMenuItem.Text = "&Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // timerStartUpInitialisation
            // 
            this.timerStartUpInitialisation.Interval = 1000;
            this.timerStartUpInitialisation.Tick += new System.EventHandler(this.timerStartUpInitialisation_Tick);
            // 
            // textBoxPatternDescription
            // 
            this.textBoxPatternDescription.Location = new System.Drawing.Point(194, 113);
            this.textBoxPatternDescription.Multiline = true;
            this.textBoxPatternDescription.Name = "textBoxPatternDescription";
            this.textBoxPatternDescription.ReadOnly = true;
            this.textBoxPatternDescription.Size = new System.Drawing.Size(204, 40);
            this.textBoxPatternDescription.TabIndex = 32;
            this.textBoxPatternDescription.TextChanged += new System.EventHandler(this.textBoxPatternDescription_TextChanged);
            // 
            // buttonBrowseFolder
            // 
            this.buttonBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBrowseFolder.Enabled = false;
            this.buttonBrowseFolder.Location = new System.Drawing.Point(115, 400);
            this.buttonBrowseFolder.Name = "buttonBrowseFolder";
            this.buttonBrowseFolder.Size = new System.Drawing.Size(126, 23);
            this.buttonBrowseFolder.TabIndex = 33;
            this.buttonBrowseFolder.Text = " Browse Folders...";
            this.buttonBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 540);
            this.Controls.Add(this.buttonBrowseFolder);
            this.Controls.Add(this.textBoxPatternDescription);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.textBoxPatternName);
            this.Controls.Add(this._pianoRoll);
            this.Controls.Add(this.buttonBrowseLibrary);
            this.Controls.Add(this.buttonBrowseFiles);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.treeViewX0x);
            this.Controls.Add(this._textBoxPattern);
            this.Controls.Add(this.listBoxActivityReport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(628, 450);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "K0ntr0l";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX0xTempo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxActivityReport;
        private System.Windows.Forms.Button buttonDumpEeprom;
        private System.Windows.Forms.Button buttonRestoreEeprom;
        private System.Windows.Forms.TextBox _textBoxPattern;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.TreeView treeViewX0x;
        private System.Windows.Forms.NumericUpDown numericUpDownX0xTempo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonBrowseFiles;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private BangOnTekCommon.PianoRoll _pianoRoll;
        private System.Windows.Forms.TextBox textBoxPatternName;
        private System.Windows.Forms.Button buttonBrowseLibrary;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItemImportPatternsDirectory;
        private System.Windows.Forms.ToolStripMenuItem loadPatternsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Timer timerStartUpInitialisation;
        private System.Windows.Forms.TextBox textBoxPatternDescription;
        private System.Windows.Forms.ToolStripMenuItem loadEEPROMBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveEEPROMBackupToolStripMenuItem;
        private System.Windows.Forms.Button buttonBrowseFolder;
        private System.Windows.Forms.ToolStripMenuItem importPatternsDirecoryToolStripMenuItem;
    }
}


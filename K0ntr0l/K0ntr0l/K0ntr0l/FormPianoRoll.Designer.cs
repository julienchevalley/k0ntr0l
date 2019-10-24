namespace K0ntr0l
{
    partial class FormPianoRoll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPianoRoll));
            this.pianoRoll1 = new BangOnTekCommon.PianoRoll();
            this.SuspendLayout();
            // 
            // pianoRoll1
            // 
            this.pianoRoll1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pianoRoll1.BlackKeyLength = 30;
            this.pianoRoll1.KeysCount = 53;
            this.pianoRoll1.Location = new System.Drawing.Point(13, 13);
            this.pianoRoll1.LowestKey = BangOnTekCommon.PianoRoll.KeyName.C;
            this.pianoRoll1.LowestKeyNoteValue = ((byte)(11));
            this.pianoRoll1.Name = "pianoRoll1";
            this.pianoRoll1.RootKeyLabel = "C-2";
            this.pianoRoll1.RootKeyNoteValue = ((byte)(23));
            this.pianoRoll1.Sequence = new BangOnTekCommon.PianoRollStep[0];
            this.pianoRoll1.Size = new System.Drawing.Size(618, 467);
            this.pianoRoll1.TabIndex = 0;
            this.pianoRoll1.Text = "pianoRoll1";
            this.pianoRoll1.WhiteKeyLength = 45;
            this.pianoRoll1.DoubleClick += new System.EventHandler(this._pianoRoll_DoubleClick);
            // 
            // FormPianoRoll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 492);
            this.Controls.Add(this.pianoRoll1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPianoRoll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pattern View";
            this.ResumeLayout(false);

        }

        #endregion

        private BangOnTekCommon.PianoRoll pianoRoll1;
    }
}
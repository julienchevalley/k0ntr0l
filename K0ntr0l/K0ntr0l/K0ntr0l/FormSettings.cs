using System;
using System.Windows.Forms;

namespace K0ntr0l
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();

            InitialiseControls();
        }

        private void InitialiseControls()
        {
            checkBoxPreviewPatterns.Checked = Properties.Settings.Default.PreviewPatterns;
            checkBoxAjustTempo.Checked = Properties.Settings.Default.AdjustTempo;
            checkBoxVersionCheck.Checked = Properties.Settings.Default.CheckForUpdates;
            numericUpDownIOTimeOut.Value = Properties.Settings.Default.SerialPortReadWriteTimeout;
            numericUpDownReplyReadTimeOut.Value = Properties.Settings.Default.WaitForBytesTimeOut;
            numericUpDownWaitForByteSleepTime.Value = Properties.Settings.Default.WaitForBytesSleepTime;
            checkBoxShowToolTips.Checked = Properties.Settings.Default.ShowToolTips;
            radioButtonOpenFileBrowser.Checked = Properties.Settings.Default.SelectedPatternDoubleClickAction == 0;
            radioButtonOpenPatternLibraryBrowser.Checked = Properties.Settings.Default.SelectedPatternDoubleClickAction == 1;
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.PreviewPatterns = checkBoxPreviewPatterns.Checked;
            Properties.Settings.Default.SerialPortReadWriteTimeout = (int)numericUpDownIOTimeOut.Value;
            Properties.Settings.Default.WaitForBytesTimeOut = (int)numericUpDownReplyReadTimeOut.Value;
            Properties.Settings.Default.WaitForBytesSleepTime = (int)numericUpDownWaitForByteSleepTime.Value;
            Properties.Settings.Default.AdjustTempo = checkBoxAjustTempo.Checked;
            Properties.Settings.Default.CheckForUpdates = checkBoxVersionCheck.Checked;
            Properties.Settings.Default.ShowToolTips = checkBoxShowToolTips.Checked;
            Properties.Settings.Default.SelectedPatternDoubleClickAction = radioButtonOpenFileBrowser.Checked ? 0 : 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new FormPianoRollConfiguration())
            {
                form.ShowDialog(this);
            }
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
           
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip
            {
                AutoPopDelay = 5000, InitialDelay = 100, ReshowDelay = 100, ShowAlways = true
            };

            // Set up the delays for the ToolTip.
            // Force the ToolTip text to be displayed whether or not the form is active.

            // Set up the ToolTip text for the form's controls
            toolTip1.SetToolTip(checkBoxPreviewPatterns,
                "Audition currently selected pattern on Bank 16 Location 8.\n For this feature to work, you need to set the x0xb0x to 'Pattern (Sync out)', select bank 16 pattern 8 and press Play (R/S)");
            toolTip1.SetToolTip(checkBoxAjustTempo, "Change the tempo of the x0xb0x to match the tempo of the currently selected pattern");
            toolTip1.SetToolTip(checkBoxVersionCheck, "Check for newer versions on startup");
            toolTip1.SetToolTip(groupBox2, "Allows you to activate Pattern Preview.");
            toolTip1.SetToolTip(radioButtonOpenFileBrowser, "Open file browser dialog when double clicking in the x0xb0x view");
            toolTip1.SetToolTip(radioButtonOpenPatternLibraryBrowser, "Open the pattern library when double clicking in the x0xb0x view");
            toolTip1.SetToolTip(groupBox1, "x0xb0x Communication Settings - Do not change unless told to do so :)");
            toolTip1.SetToolTip(buttonPianoRollColors, "Defines the appearance of the Piano Roll control");
            toolTip1.SetToolTip(checkBoxVersionCheck, "Checks for newer K0ntr0l versions on Startup. Requires an internet connection");
            toolTip1.SetToolTip(checkBoxShowToolTips, "Toggles the display of Help Tooltips - Requires a Restart");
            toolTip1.SetToolTip(groupBox6,
                "Select the action to perform when double clicking a pattern in the list on the main window");
        }

        private void buttonRestoreDefaultSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            InitialiseControls();
        }
    }
}

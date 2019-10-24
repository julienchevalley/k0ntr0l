using System;
using System.Windows.Forms;

namespace K0ntr0l
{
    public partial class FormPianoRollConfiguration : Form
    {
        public FormPianoRollConfiguration()
        {
            InitializeComponent();
        }

        private void labelNoteColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = labelNoteColor.BackColor;
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
                Properties.Settings.Default.NoteColor = labelNoteColor.BackColor = colorDialog.Color;
        }

        private void labelAccentedNoteColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = labelAccentedNoteColor.BackColor;
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
                Properties.Settings.Default.AccentedNoteColor = labelAccentedNoteColor.BackColor = colorDialog.Color;
        }

        private void labelRestColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = labelRestColor.BackColor;
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
                Properties.Settings.Default.RestColor = labelRestColor.BackColor = colorDialog.Color;
        }

        private void labelAccentedRestColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = labelAccentedRestColor.BackColor;
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
                Properties.Settings.Default.AccentedRestColor = labelAccentedRestColor.BackColor = colorDialog.Color;
        }

        private void labelSliceColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = labelSlideColor.BackColor;
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
                Properties.Settings.Default.SlideColor = labelSlideColor.BackColor = colorDialog.Color;

        }

        private void FormPianoRollConfiguration_Load(object sender, EventArgs e)
        {
            labelNoteColor.BackColor = Properties.Settings.Default.NoteColor;
            labelAccentedNoteColor.BackColor = Properties.Settings.Default.AccentedNoteColor;
            labelRestColor.BackColor = Properties.Settings.Default.RestColor;
            labelAccentedRestColor.BackColor = Properties.Settings.Default.AccentedRestColor;
            labelSlideColor.BackColor = Properties.Settings.Default.SlideColor;
        }
    }
}
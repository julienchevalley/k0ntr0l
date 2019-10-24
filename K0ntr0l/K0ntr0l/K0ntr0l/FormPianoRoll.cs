using System;
using System.Drawing;
using System.Windows.Forms;
using BangOnTekCommon;

namespace K0ntr0l
{
    public partial class FormPianoRoll : Form
    {
        public FormPianoRoll(string patternName, PianoRollStep[] steps)
        {
            InitializeComponent();
            pianoRoll1.Sequence = steps;
            Text += " - " + patternName;
        }

        private void _pianoRoll_DoubleClick(object sender, EventArgs e)
        {
            using (var form = new FormPianoRollConfiguration())
            {
                form.ShowDialog(this);

                RefreshPianoRoll();
            }
        }

        private void RefreshPianoRoll()
        {
            PianoRoll.NoteBrush = new SolidBrush(Properties.Settings.Default.NoteColor);
            PianoRoll.AccentedNoteBrush = new SolidBrush(Properties.Settings.Default.AccentedNoteColor);
            PianoRoll.RestBrush = new SolidBrush(Properties.Settings.Default.RestColor);
            PianoRoll.AccentedRestBrush = new SolidBrush(Properties.Settings.Default.AccentedRestColor);
            PianoRoll.SlidePen = new Pen(Properties.Settings.Default.SlideColor, PianoRoll.SlidePenThickness);
            pianoRoll1.Refresh();
        }
    }
}

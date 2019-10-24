using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K0ntr0l
{
    public partial class FormPreviewCommsWarning : Form
    {
        public FormPreviewCommsWarning()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormPreviewCommsWarning_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FormPreviewCommsWarning = ! checkBoxDoNotShowAgain.Checked;
        }
    }
}

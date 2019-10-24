using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace K0ntr0l
{
    public partial class FormNewerVersionAvailable : Form
    {
        public FormNewerVersionAvailable()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO move the url to a constant
            Process.Start("https://kontrol.bangontek.com/download.html");
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

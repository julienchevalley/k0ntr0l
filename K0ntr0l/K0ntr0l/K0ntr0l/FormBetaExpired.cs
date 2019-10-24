using System.Windows.Forms;

namespace K0ntr0l
{
    public partial class FormBetaExpired : Form
    {
        public FormBetaExpired()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://kontrol.bangontek.com");
        }
    }
}

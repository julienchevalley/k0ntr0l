using System.Windows.Forms;

namespace K0ntr0l
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, System.EventArgs e)
        {
            labelVersion.Text += Properties.Resources.BuildNumber;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text + "/donate.html");
            // Test
        }
    }
}

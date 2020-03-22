using System;
using System.Windows.Forms;

namespace FinalResearchAssistant
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://forms.gle/zuD5sw5zfa3pxoGx8");
            linkLabel1.LinkVisited = true;
        }
    }
}

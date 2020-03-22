using System;
using System.Windows.Forms;
using System.IO;

namespace FinalResearchAssistant
{
    public partial class HistoryForm : Form
    {
        string user;

        public HistoryForm()
        {
            InitializeComponent();
        }

        public void getUser(string username)
        {
            user = username;
        }

        private void HistoryForm_Shown(object sender, EventArgs e)
        {
            userLabel.Text = user;

            string[] lines = File.ReadAllLines(@"" + user + @"\Research_Results_History.txt");

            foreach(var line in lines)
            {
                richTextBox1.AppendText(line + "\n");
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            userLabel.Text = "";
            this.Close();
        }
    }
}

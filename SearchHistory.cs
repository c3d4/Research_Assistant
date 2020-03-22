using System;
using System.Windows.Forms;
using System.IO;

namespace FinalResearchAssistant
{
    public partial class SearchHistory : Form
    {
        string user;

        public SearchHistory()
        {
            InitializeComponent();
        }
        
        public void getUser(string username)
        {
            user = username;
        }

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            userLabel.Text = "";
            richTextBox1.Clear();
            this.Close();
        }

        private void SearchHistory_Shown(object sender, EventArgs e)
        {
            userLabel.Text = user;

            string[] lines = File.ReadAllLines(@"" + user + @"\Search_History.txt");

            foreach (var line in lines)
            {
                richTextBox1.AppendText(line + Environment.NewLine);
            }
        }
    }
}

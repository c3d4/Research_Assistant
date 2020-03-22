using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace FinalResearchAssistant
{
    public partial class SavedResources : Form
    {
        string user; 

        public SavedResources()
        {
            InitializeComponent();
        }

        public void getUser(string username)
        {
            user = username;
        }

        // Show the data
        private void SavedResources_Shown(object sender, EventArgs e)
        {
            userLabel.Text = user;

            string[] lines = File.ReadAllLines(@"" + user + @"\Research_Results_Saved.txt");

            foreach (var line in lines)
            {
                richTextBox1.AppendText(line + Environment.NewLine);
            }
           
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            this.Close();
        }
    }
}

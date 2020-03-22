using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FinalResearchAssistant
{
    public partial class UserForm : Form
    {
        string username;

        RetrieverForm retrieverForm = new RetrieverForm();
        LocalResults localResults = new LocalResults();
        HistoryForm historyForm = new HistoryForm();
        SearchHistory searcHistory = new SearchHistory();
        SavedResources savedResources = new SavedResources();

        // Initialize the form
        public UserForm()
        {
            InitializeComponent();
        }

        // Whenever the form is opened
        private void UserForm_Shown(object sender, EventArgs e)
        {
            if (username == "admin")
            {
                bunifuFlatButton6.Visible = true;
            }

            timer1.Start();
            label4.Text = "Welcome, " + username;
        }

        // Go to the retriever form 
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            retrieverForm.getUser(username);
            retrieverForm.ShowDialog();
        }

        // Go to the local search form
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"" + username + @"\Research_Results_History.txt"))
            {
                localResults.getUser(username);
                localResults.ShowDialog();
            }                
        }

        // Go to the history form
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"" + username + @"\Research_Results_History.txt"))
            {
                historyForm.getUser(username);
                historyForm.ShowDialog();
            } 
        }

        // Go to the search history
        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"" + username + @"\Search_History.txt"))
            {
                searcHistory.getUser(username);
                searcHistory.ShowDialog();
            }
        }

        // Go to saved resources
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"" + username + @"\Research_Results_Saved.txt"))
            {
                savedResources.getUser(username);
                savedResources.ShowDialog();
            }
        }

        // Get the username
        public void setUser(string user)
        {
            username = user;
        }

        // Logout of the user page
        private void logoutButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }

        // Make sure that the proper files are there
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (File.Exists(@"" + username + @"\Research_Results_History.txt"))
            {
                bunifuFlatButton3.Enabled = true;
                bunifuFlatButton4.Enabled = true;
            }
            else
            {
                bunifuFlatButton4.Enabled = false;
                bunifuFlatButton3.Enabled = false;
            }
            if (File.Exists(@"" + username + @"\Search_History.txt"))
            {
                bunifuFlatButton5.Enabled = true;
            }
            else
            {
                bunifuFlatButton5.Enabled = false;
            }
            
            if (File.Exists(@"" + username + @"\Research_Results_Saved.txt"))
            {
                bunifuFlatButton2.Enabled = true;
            }
            else
            {
                bunifuFlatButton2.Enabled = false;
            }

        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            if (bunifuFlatButton6.Visible == true)
            {
                System.Diagnostics.Process.Start(@"admin");
            }
        }
    }
}

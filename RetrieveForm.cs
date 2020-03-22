using System;
using System.Windows.Forms;
using System.IO;

namespace FinalResearchAssistant
{
    public partial class RetrieverForm : Form
    {
        // Reference the two web services
        ApiHelper arxiv = new ApiHelper();
        ApiHelper core = new ApiHelper();

        ResultsForm resultsForm = new ResultsForm();

        // Username and history
        string user;
        bool enableHistory;

        // When the form first loads
        public RetrieverForm()
        {
            InitializeComponent();
            timer1.Start();
        }

        // Make sure everything works properly
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Update the number of results
            valueLabel.Text = (bunifuSlider1.Value + 5).ToString();

            // Make sure that the user can't search if not filled
            if (bunifuMaterialTextbox1.Text == "")
            {
                searchButton.Enabled = false;
            }

            if (arxivTrue.Checked == false && coreTrue.Checked == false)
            {
                searchButton.Enabled = false;
                errorMessage.Text = "Must be filled!";
            }

            if (bunifuMaterialTextbox1.Text != "" && (arxivTrue.Checked == true || coreTrue.Checked == true))
            {
                searchButton.Enabled = true;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            arxivTrue.Checked = true;
            coreTrue.Checked = true;
            historyYes.Checked = true;
            historyNo.Checked = false; 

            bunifuSlider1.Value = 0;
            bunifuMaterialTextbox1.Text = ""; 
            errorMessage.Text = "";
            this.Close();
        }

        #region Ensure that the checks are correct

        private void RetrieverForm_Shown(object sender, EventArgs e)
        {
            historyYes.Checked = true;
            historyNo.Checked = false;
        }

        private void historyYes_OnChange(object sender, EventArgs e)
        {
            historyYes.Checked = true;
            historyNo.Checked = false;
        }

        private void historyNo_OnChange(object sender, EventArgs e)
        {
            historyYes.Checked = false;
            historyNo.Checked = true;
        }

        #endregion


        #region Conduct the search

        // Read all of the settings listed
        private void searchButton_Click(object sender, EventArgs e)
        {
            // Get the search query
            string searchQuery = bunifuMaterialTextbox1.Text;

            if (historyYes.Checked == true)
            {
                File.AppendAllText(@"" + user + @"\Search_History.txt", searchQuery + Environment.NewLine);
            }

            // Get the history
            if (historyYes.Checked == true)
            {
                enableHistory = true;
            }
            else
            {
                enableHistory = false;
            }

            // Give the api helper the user
            arxiv.getUser(user);
            core.getUser(user);

            // Make sure the proper databases are used
            if (arxivTrue.Checked == true)
            {
                arxiv.url = "http://export.arxiv.org/api/query?search_query=" + searchQuery + "&max_results=5";
                arxiv.GetRequest();
            }

            if (coreTrue.Checked == true)
            {
                core.url = "https://core.ac.uk:443/api-v2/journals/search/" + searchQuery + "? page=1&pageSize=10&apiKey=rnBoaFLv8jiJd6N3XzECwule9SR7MPmH";
                core.GetRequest();
            }

            // Start creating the necessary files
            documentTimer.Start();
            mergedTimer.Start();

            bunifuMaterialTextbox1.Text = "";
            resultsForm.getUser(user, searchQuery, bunifuSlider1.Value + 5);
            resultsForm.ShowDialog();  //Show the settings form  instead of the app
        }

        // Merge the files
        private void documentTimer_Tick(object sender, EventArgs e)
        {
            LocalGatherer.getUser(user, enableHistory);
            // Merge the files properly
            if (arxivTrue.Checked == true && coreTrue.Checked == true)
            {
                if (File.Exists(@"" + user + @"\Research_Results_XML.txt") && File.Exists(@"" + user + @"\Research_Results_JSON.txt"))
                {
                    LocalGatherer.mergeFiles();
                    documentTimer.Stop();   // Stop checking for the files to run the code once
                }
            }
            else if (arxivTrue.Checked == true && coreTrue.Checked == false)
            {
                if (File.Exists(@"" + user + @"\Research_Results_XML.txt"))
                {
                    LocalGatherer.mergeFiles();
                    documentTimer.Stop();   // Stop checking for the files to run the code once
                }
            }
            else if (arxivTrue.Checked == false && coreTrue.Checked == true)
            {
                if (File.Exists(@"" + user + @"\Research_Results_JSON.txt"))
                {
                    LocalGatherer.mergeFiles();
                    documentTimer.Stop();   // Stop checking for the files to run the code once
                }
            }
        }

        // Make sure that the merged file exists
        private void mergedTimer_Tick(object sender, EventArgs e)
        {
            if (File.Exists(@"" + user + @"\Research_Results_Merged.txt"))
            {
                mergedTimer.Stop();   // Stop checking for the files to run the code once
            }
        }

        #endregion

        // Get the username from main the user form
        public void getUser(string username)
        {
            user = username;
        }
    }
}

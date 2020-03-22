using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace FinalResearchAssistant
{
    public partial class ResultsForm : Form
    {
        string user;
        string query;

        List<Panel> totalPanels = new List<Panel>(); // Panels
        List<PictureBox> totalPictures = new List<PictureBox>(); // Pictures
        List<Label> totalLabels = new List<Label>();  // Labels
        List<string> titles = new List<string>();

        List<Bunifu.Framework.UI.BunifuFlatButton> totalButtons = new List<Bunifu.Framework.UI.BunifuFlatButton>();  // Buttons

        List<Bunifu.Framework.UI.BunifuFlatButton> resultButtons = new List<Bunifu.Framework.UI.BunifuFlatButton>();  // Buttons

        List<string> totalLinks = new List<string>();   // Links

        // Number of results set from the previous screen
        int numberOfResults;

        public ResultsForm()
        {
            InitializeComponent();

            #region Add Controls

            totalPanels.Add(resultsPanel1);
            totalPanels.Add(resultsPanel2);
            totalPanels.Add(resultsPanel3);
            totalPanels.Add(resultsPanel4);
            totalPanels.Add(resultsPanel5);
            totalPanels.Add(resultsPanel6);
            totalPanels.Add(resultsPanel7);
            totalPanels.Add(resultsPanel8);
            totalPanels.Add(resultsPanel9);
            totalPanels.Add(resultsPanel10);
            totalPanels.Add(resultsPanel11);
            totalPanels.Add(resultsPanel12);
            totalPanels.Add(resultsPanel13);
            totalPanels.Add(resultsPanel14);
            totalPanels.Add(resultsPanel15);

            totalLabels.Add(resultsLabel1);
            totalLabels.Add(resultsLabel2);
            totalLabels.Add(resultsLabel3);
            totalLabels.Add(resultsLabel4);
            totalLabels.Add(resultsLabel5);
            totalLabels.Add(resultsLabel6);
            totalLabels.Add(resultsLabel7);
            totalLabels.Add(resultsLabel8);
            totalLabels.Add(resultsLabel9);
            totalLabels.Add(resultsLabel10);
            totalLabels.Add(resultsLabel11);
            totalLabels.Add(resultsLabel12);
            totalLabels.Add(resultsLabel13);
            totalLabels.Add(resultsLabel14);
            totalLabels.Add(resultsLabel15);

            totalButtons.Add(resultsButton1);
            totalButtons.Add(resultsButton2);
            totalButtons.Add(resultsButton3);
            totalButtons.Add(resultsButton4);
            totalButtons.Add(resultsButton5);
            totalButtons.Add(resultsButton6);
            totalButtons.Add(resultsButton7);
            totalButtons.Add(resultsButton8);
            totalButtons.Add(resultsButton9);
            totalButtons.Add(resultsButton10);
            totalButtons.Add(resultsButton11);
            totalButtons.Add(resultsButton12);
            totalButtons.Add(resultsButton13);
            totalButtons.Add(resultsButton14);
            totalButtons.Add(resultsButton15);

            resultButtons.Add(saveResult1);
            resultButtons.Add(saveResult2);
            resultButtons.Add(saveResult3);
            resultButtons.Add(saveResult4);
            resultButtons.Add(saveResult5);
            resultButtons.Add(saveResult6);
            resultButtons.Add(saveResult7);
            resultButtons.Add(saveResult8);
            resultButtons.Add(saveResult9);
            resultButtons.Add(saveResult10);
            resultButtons.Add(saveResult11);
            resultButtons.Add(saveResult12);
            resultButtons.Add(saveResult13);
            resultButtons.Add(saveResult14);
            resultButtons.Add(saveResult15);


            totalPictures.Add(rPicBox1);
            totalPictures.Add(rPicBox2);
            totalPictures.Add(rPicBox3);
            totalPictures.Add(rPicBox4);
            totalPictures.Add(rPicBox5);
            totalPictures.Add(rPicBox6);
            totalPictures.Add(rPicBox7);
            totalPictures.Add(rPicBox8);
            totalPictures.Add(rPicBox9);
            totalPictures.Add(rPicBox10);
            totalPictures.Add(rPicBox11);
            totalPictures.Add(rPicBox12);
            totalPictures.Add(rPicBox13);
            totalPictures.Add(rPicBox14);
            totalPictures.Add(rPicBox15);

            #endregion
        }

        // Start the timer everytime the form is entered
        private void ResultsForm_Shown(object sender, EventArgs e)
        {
            queryLabel.Text = query;
            timer1.Start();
        }

        #region Results 

        // Responsible for showing results
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (File.Exists(@"" + user + @"\Research_Results_Merged.txt") == true)
            {
                // Add all lines to a list
                string[] lines = File.ReadAllLines(@"" + user + @"\Research_Results_Merged.txt");
                returned.Text = "Results Returned: " + lines.Count() / 2;
                
                // CHECK THE NUMBER OF RESULTS HERE COMPARE TO NUMBER SET
                if (numberOfResults <= (lines.Count() / 2))
                {
                    shown.Text = "Results Shown: " + numberOfResults.ToString();
                    // Display Results
                    for (int i = 0; i < numberOfResults; i++)
                    {
                        // Highlight Panels
                        totalPanels[i].BackColor = Color.FromArgb(40, 40, 40);

                        // Set the text
                        totalLabels[i].Text = lines[2 * i];
                        titles.Add(lines[2 * i]);

                        // Show the buttons
                        totalButtons[i].Visible = true;
                        resultButtons[i].Visible = true;
                    }

                    // Get links
                    for (int i = 0; i < (numberOfResults * 2); i++)
                    {
                        if (lines[i].Contains("http"))
                        {
                            totalLinks.Add(lines[i]);
                        }
                    }
                    
                    // Display the images
                    for (int i = 0; i < numberOfResults; i++)
                    {
                        if (totalLinks[i].Contains("arxiv"))
                        {
                            totalPictures[i].Image = Image.FromFile("download.jpg");
                        } else
                        {
                            totalPictures[i].Image = Image.FromFile("download.png");
                        }
                    }
                }
                else if (numberOfResults > lines.Count() / 2)
                {
                    // Number of results returned
                    numberOfResults = lines.Count() / 2;
                    returned.Text = "Results Returned: " + (lines.Count() / 2).ToString();
                    shown.Text = "Results Shown: " + numberOfResults; 

                    // Display Results
                    for (int i = 0; i < numberOfResults; i++)
                    {
                        // Highlight Panels
                        totalPanels[i].BackColor = Color.FromArgb(40, 40, 40);

                        // Set the text
                        totalLabels[i].Text = lines[2 * i];

                        // Show the buttons
                        totalButtons[i].Visible = true;
                        resultButtons[i].Visible = true;
                    }

                    // Get Links
                    for (int i = 0; i < (numberOfResults * 2); i++)
                    {
                        if (lines[i].Contains("http"))
                        {
                            totalLinks.Add(lines[i]);
                        }
                    }

                    // Display the images
                    for (int i = 0; i < numberOfResults; i++)
                    {
                        if (totalLinks[i].Contains("arxiv"))
                        {
                            totalPictures[i].Image = Image.FromFile("download.jpg");
                        }
                        else
                        {
                            totalPictures[i].Image = Image.FromFile("download.png");
                        }
                    }
                }

                // Stop the timer
                timer1.Stop();
            }
        }

        #endregion

        #region Close and Reset the Form

        // Close the form and delete the merged file
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            // Reset the controls
            for (int i=0; i < numberOfResults; i++)
            {
                totalPanels[i].BackColor = Color.Transparent;
                totalLabels[i].Text = "";
                totalPictures[i].Image = null;
                resultButtons[i].Visible = false;
                returned.Text = "";
                shown.Text = "";

                totalButtons[i].Visible = false;
            }

            // Clear the links
            totalLinks.Clear();

            // Delete files to reset in the future
            if (File.Exists(@"" + user + @"\Research_Results_Merged.txt"))
            {
                File.Delete(@"" + user + @"\Research_Results_Merged.txt");
            }

            if (File.Exists(@"" + user + @"\Research_Results_XML.txt"))
            {
                File.Delete(@"" + user + @"\Research_Results_XML.txt");
            }

            if (File.Exists(@"" + user + @"\Research_Results_JSON.txt"))
            {
                File.Delete(@"" + user + @"\Research_Results_JSON.txt");
            }
            if (File.Exists(@"" + user + @"\Research_Assistant_Result.xml"))
            {
                File.Delete(@"" + user + @"\Research_Assistant_Result.xml");
            }

            // Close the form
            this.Close();
        }

        #endregion

        #region Button Links

        private void resultsButton1_Click(object sender, EventArgs e)
        {
            if (resultsButton1.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[0]);
            }
        }

        private void resultsButton2_Click(object sender, EventArgs e)
        {
            if (resultsButton2.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[1]);
            }
        }

        private void resultsButton3_Click(object sender, EventArgs e)
        {
            if (resultsButton3.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[2]);
            }
        }

        private void resultsButton4_Click(object sender, EventArgs e)
        {
            if (resultsButton4.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[3]);
            }
        }

        private void resultsButton5_Click(object sender, EventArgs e)
        {
            if (resultsButton5.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[4]);
            }
        }

        private void resultsButton6_Click(object sender, EventArgs e)
        {
            if (resultsButton6.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[5]);
            }
        }

        private void resultsButton7_Click(object sender, EventArgs e)
        {
            if (resultsButton7.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[6]);
            }
        }

        private void resultsButton8_Click(object sender, EventArgs e)
        {
            if (resultsButton8.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[7]);
            }
        }

        private void resultsButton9_Click(object sender, EventArgs e)
        {
            if (resultsButton9.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[8]);
            }
        }

        private void resultsButton10_Click(object sender, EventArgs e)
        {
            if (resultsButton10.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[9]);
            }
        }

        private void resultsButton11_Click(object sender, EventArgs e)
        {
            if (resultsButton11.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[10]);
            }
        }

        private void resultsButton12_Click(object sender, EventArgs e)
        {
            if (resultsButton12.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[11]);
            }
        }

        private void resultsButton13_Click(object sender, EventArgs e)
        {
            if (resultsButton13.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[12]);
            }
        }

        private void resultsButton14_Click(object sender, EventArgs e)
        {
            if (resultsButton14.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[13]);
            }
        }

        private void resultsButton15_Click(object sender, EventArgs e)
        {
            if (resultsButton15.Visible == true)
            {
                System.Diagnostics.Process.Start(totalLinks[14]);
            }
        }

        #endregion

        public void getUser(string username, string nQuery, int results)
        {
            user = username;
            query = nQuery;
            numberOfResults = results;
        }

        #region Button Save

        private void saveResult1_Click(object sender, EventArgs e)
        {
            if (saveResult1.Visible == true)
            {
                if (resultsPanel1.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[0] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[0] + Environment.NewLine);
                }
                resultsPanel1.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult2_Click(object sender, EventArgs e)
        {
            if (saveResult2.Visible == true)
            {
                if (resultsPanel2.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[1] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[1] + Environment.NewLine);
                }
                resultsPanel2.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult3_Click(object sender, EventArgs e)
        {
            if (saveResult3.Visible == true)
            {
                if (resultsPanel3.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[2] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[2] + Environment.NewLine);
                }
                resultsPanel3.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult4_Click(object sender, EventArgs e)
        {
            if (saveResult4.Visible == true)
            {
                if (resultsPanel4.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[3] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[3] + Environment.NewLine);
                }
                resultsPanel4.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult5_Click(object sender, EventArgs e)
        {
            if (saveResult5.Visible == true)
            {
                if (resultsPanel5.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[4] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[4] + Environment.NewLine);
                }
                resultsPanel5.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult6_Click(object sender, EventArgs e)
        {
            if (saveResult6.Visible == true)
            {
                if (resultsPanel6.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[5] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[5] + Environment.NewLine);
                }
                resultsPanel6.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult7_Click(object sender, EventArgs e)
        {
            if (saveResult7.Visible == true)
            {
                if (resultsPanel7.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[6] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[6] + Environment.NewLine);
                }
                resultsPanel7.BackColor = Color.FromArgb(70, 70, 70);
            }
        }
        private void saveResult8_Click(object sender, EventArgs e)
        {
            if (saveResult8.Visible == true)
            {
                if (resultsPanel8.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[7] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[7] + Environment.NewLine);
                }
                resultsPanel8.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult9_Click(object sender, EventArgs e)
        {
            if (saveResult9.Visible == true)
            {
                if (resultsPanel9.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[8] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[8] + Environment.NewLine);
                }
                resultsPanel9.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult10_Click(object sender, EventArgs e)
        {
            if (saveResult10.Visible == true)
            {
                if (resultsPanel10.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[9] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[9] + Environment.NewLine);
                }
                resultsPanel10.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult11_Click(object sender, EventArgs e)
        {
            if (saveResult11.Visible == true)
            {
                if (resultsPanel11.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[10] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[10] + Environment.NewLine);
                }
                resultsPanel11.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult12_Click(object sender, EventArgs e)
        {
            if (saveResult12.Visible == true)
            {
                if (resultsPanel12.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[11] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[11] + Environment.NewLine);
                }
                resultsPanel12.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult13_Click(object sender, EventArgs e)
        {
            if (saveResult13.Visible == true)
            {
                if (resultsPanel13.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[12] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[12] + Environment.NewLine);
                }
                resultsPanel13.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult14_Click(object sender, EventArgs e)
        {
            if (saveResult14.Visible == true)
            {
                if (resultsPanel14.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[13] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[13] + Environment.NewLine);
                }
                resultsPanel14.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void saveResult15_Click(object sender, EventArgs e)
        {
            if (saveResult15.Visible == true)
            {
                if (resultsPanel15.BackColor != Color.FromArgb(70, 70, 70))
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", titles[14] + Environment.NewLine);
                    File.AppendAllText(@"" + user + @"\Research_Results_Saved.txt", totalLinks[14] + Environment.NewLine);
                }
                resultsPanel15.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        #endregion

    }
}

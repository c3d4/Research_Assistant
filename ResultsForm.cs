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
        List<Panel> totalPanels = new List<Panel>();
        List<Label> totalLabels = new List<Label>();
        List<Button> totalButtons = new List<Button>();
        List<string> totalLinks = new List<string>();

        int numberOfResults;

        public ResultsForm()
        {
            InitializeComponent();

            #region Add Controls

            totalPanels.Add(resultPanel1);
            totalPanels.Add(resultPanel2);
            totalPanels.Add(resultPanel3);
            totalPanels.Add(resultPanel4);
            totalPanels.Add(resultPanel5);
            totalPanels.Add(resultPanel6);
            totalPanels.Add(resultPanel7);
            totalPanels.Add(resultPanel8);
            totalPanels.Add(resultPanel9);
            totalPanels.Add(resultPanel10);
            totalPanels.Add(resultPanel11);
            totalPanels.Add(resultPanel12);
            totalPanels.Add(resultPanel13);
            totalPanels.Add(resultPanel14);
            totalPanels.Add(resultPanel15);

            totalLabels.Add(label2);
            totalLabels.Add(label3);
            totalLabels.Add(label4);
            totalLabels.Add(label5);
            totalLabels.Add(label6);
            totalLabels.Add(label7);
            totalLabels.Add(label8);
            totalLabels.Add(label9);
            totalLabels.Add(label10);
            totalLabels.Add(label11); 
            totalLabels.Add(label12);
            totalLabels.Add(label13);
            totalLabels.Add(label14);
            totalLabels.Add(label15);
            totalLabels.Add(label16);

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

            #endregion
        }

        // Start the timer everytime the form is entered
        private void ResultsForm_Shown(object sender, EventArgs e)
        {
            timer1.Start();
        }

        #region Results 

        // Responsible for showing results
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (File.Exists("Research_Results_Merged.txt"))
            {
                // Add all lines to a list
                string[] lines = File.ReadAllLines("Research_Results_Merged.txt"); 

                // Number of results returned
                numberOfResults = lines.Count() / 2;

                // Display Results
                for (int i = 0; i< numberOfResults; i++)
                {
                    // Highlight Panels
                    totalPanels[i].BackColor = Color.Silver;

                    // Set the text
                    totalLabels[i].Text = lines[2 * i];

                    // Show the buttons
                    totalButtons[i].Visible = true;

                    //Get the links
                    if (i != 0)
                    {
                        totalLinks.Add(lines[(2 * i) - 1]);
                    }
                }
                
                // Get the very last link, as the formula above exits when its equal to results
                totalLinks.Add(lines[lines.Length - 1]);

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
            for (int i=0; i<numberOfResults; i++)
            {
                totalPanels[i].BackColor = Color.Transparent;
                totalLabels[i].Text = "";
                totalButtons[i].Visible = false;
            }

            // Clear the links
            totalLinks.Clear();

            // Delete the file to reset in the future
            File.Delete("Research_Results_Merged.txt");

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
    }
}

using System;
using System.IO;
using System.Windows.Forms;

namespace FinalResearchAssistant
{
    public partial class LocalResults : Form
    {
        string query;

        public LocalResults()
        {
            InitializeComponent();
        }

        // Get the query 
        public void getQuery(string _query)
        {
            query = _query;
        }

        // Close the form 
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            richTextBox1.ResetText();
            this.Close();
        }

        private void LocalResults_Shown(object sender, EventArgs e)
        {
            // Linear Search Algorithm
            string[] lines = File.ReadAllLines("Research_Results_History.txt");
            int count = lines.Length;

            for (int i = 0; i < count; i++)
            {
                if ((lines[i].ToLower().Contains(query.ToLower()) == true) && (i % 2 == 0))
                {
                    richTextBox1.AppendText(lines[i] + "\n"); // title
                    richTextBox1.AppendText(lines[i + 1] + "\n"); // link
                }
            }
            
        }
    }
}

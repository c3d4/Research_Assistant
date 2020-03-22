using System;
using System.IO;
using System.Windows.Forms;

namespace FinalResearchAssistant
{
    public partial class LocalResults : Form
    {
        string query;
        string user; 

        public LocalResults()
        {
            InitializeComponent();
        }

        // Get the query 
        public void getUser(string username)
        {
            user = username;
        }

        // Close the form 
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            richTextBox1.ResetText();
            this.Close();
        }

        // Linear Search Algorithm
        private void searchButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
            bunifuFlatButton2.Location = new System.Drawing.Point(524, 646);
            bunifuFlatButton2.Size = new System.Drawing.Size(206, 56);

            // Linear Search Algorithm
            string[] lines = File.ReadAllLines(@"" + user + @"\Research_Results_History.txt");
            
            query = bunifuMaterialTextbox1.Text;
            
            int count = lines.Length;

            for (int i = 0; i < count; i++)
            {
                if ((lines[i].ToLower().Contains(query.ToLower()) == true) && (i % 2 == 0))
                {
                    richTextBox1.AppendText(lines[i] + "\n"); // title
                    richTextBox1.AppendText(lines[i + 1] + "\n"); // link
                }
            }
            
            if (richTextBox1.Text == "")
            {
                richTextBox1.AppendText("No Results Found!");
            }
        }

        // Close the form
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox1.Visible = false;
            bunifuMaterialTextbox1.Text = "";
            bunifuFlatButton2.Size = new System.Drawing.Size(129, 56);
            bunifuFlatButton2.Location = new System.Drawing.Point(1104, 646);

            this.Close();
        }


    }
}

using System;
using System.IO;
using System.Windows.Forms;

namespace FinalResearchAssistant
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        // When the form is opened
        private void Signup_Shown(object sender, EventArgs e)
        {
            // Reset the form and error message
            timer1.Start();
            bunifuMaterialTextbox1.Text = "";
            bunifuMaterialTextbox2.Text = "";
            errorMessage.Text = "";
            confirmLabel.Text = "";
            timer2.Stop();
        }

        // Make sure that both are filled
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bunifuMaterialTextbox1.Text == "" || bunifuMaterialTextbox2.Text == "")
            {
                confirmButton.Enabled = false;
            }
            else
            {
                confirmButton.Enabled = true;
            }
        }

        // Set users and close the form 
        private void confirmButton_Click(object sender, EventArgs e)
        {
            // Create a folder for the user 
            if (Directory.Exists(bunifuMaterialTextbox1.Text) == false)
            {
                Directory.CreateDirectory(bunifuMaterialTextbox1.Text);
                confirmLabel.Text = "Account Created!";

                // In the folder, create a file called password
                string fileName = @""+bunifuMaterialTextbox1.Text + @"\key.txt";
                File.WriteAllText(fileName, bunifuMaterialTextbox2.Text);

                timer2.Start();
            }
            else
            {
                errorMessage.Text = bunifuMaterialTextbox1.Text + " it taken!";
            }
        }

        // Quit the form
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Close();
            timer2.Stop();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

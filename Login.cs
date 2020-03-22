using System;
using System.Windows.Forms;
using System.IO;

namespace FinalResearchAssistant
{
    public partial class Login : Form
    {
        UserForm userForm = new UserForm();

        public Login()
        {
            InitializeComponent();
        }

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

        private void Login_Shown(object sender, EventArgs e)
        {
            // Reset the form and error message
            timer1.Start();

            bunifuMaterialTextbox1.Text = "";
            bunifuMaterialTextbox2.Text = "";
            confirmLabel.Text = "";
            errorMessage.Text = "";
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(bunifuMaterialTextbox1.Text) == true)
            {
                string fileName = @"" + bunifuMaterialTextbox1.Text + @"\key.txt";
                string key = File.ReadAllText(fileName);

                if (bunifuMaterialTextbox2.Text == key)
                {
                    errorMessage.Text = "";
                    confirmLabel.Text = "Logged in sucessfully!";
                    userForm.setUser(bunifuMaterialTextbox1.Text);
                    bunifuMaterialTextbox1.Text = "";
                    bunifuMaterialTextbox2.Text = "";
                    confirmLabel.Text = "";

                    userForm.ShowDialog();
                }
                else
                {
                    errorMessage.Text = "The key is incorrect!";
                }
            }
            else
            {
                errorMessage.Text = bunifuMaterialTextbox1.Text + " isn't a registered user!";
            }
        }
    }
}

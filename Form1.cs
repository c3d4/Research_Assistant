using System;
using System.Drawing;
using System.Windows.Forms;

namespace FinalResearchAssistant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region GUI

        // Variables to move GUI 
        private bool _dragging; //Bool to check if mouse is being dragged
        private Point _startPoint = new Point(0, 0);    // Point of the cursor
        private int colorOffset = 30;   // The change in color from hovering over a GUI object

        // Change the color of the close button
        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(132 + colorOffset, 18 + colorOffset, 13 + colorOffset);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(132, 18, 13);
        }

        // Change the color of the minimize button
        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.BackColor = Color.FromArgb(132 + colorOffset, 18 + colorOffset, 13 + colorOffset);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.BackColor = Color.FromArgb(132, 18, 13);
        }

        // Close the application when pressing the X button
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Minimize the application when pressing the - button
        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Moving the panel
        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            Point p = PointToScreen(e.Location);
            Location = new Point(p.X - _startPoint.X, p.Y - _startPoint.Y);
        }

        private void panel1_MouseUp_1(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startPoint = new Point(e.X, e.Y);
        }

        #endregion

        #region Login and Signup

        // Reference Signup Form
        Signup signupForm = new Signup();
        Login loginForm = new Login();

        // Signup Button
        private void signupButton_Click(object sender, EventArgs e)
        {
            // Show the Signup form 
            signupForm.ShowDialog();
        }

        // Login Button
        private void loginButton_Click(object sender, EventArgs e)
        {
            // Show the login form
            loginForm.ShowDialog();
        }

        #endregion

        #region Preferances 

        // About science search
        SettingsForm aboutForm = new SettingsForm();

        private void preferancesButton_Click(object sender, EventArgs e)
        {
            aboutForm.ShowDialog();
        }

        #endregion
    }
}

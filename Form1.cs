using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace FinalResearchAssistant
{
    public partial class Form1 : Form
    {
        // Variables to move GUI 
        private bool _dragging; //Bool to check if mouse is being dragged
        private Point _startPoint = new Point(0, 0);    // Point of the cursor
        private int colorOffset = 30;   // The change in color from hovering over a GUI object

        ResultsForm resultsForm =  new ResultsForm();

        #region GUI
        // Change the color of the close button
        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(72 + colorOffset, 73 + colorOffset, 114 + colorOffset);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(72, 73, 114);
        }

        // Change the color of the minimize button
        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.BackColor = Color.FromArgb(72 + colorOffset, 73 + colorOffset, 114 + colorOffset);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.BackColor = Color.FromArgb(72, 73, 114);
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

        public Form1()
        {
            InitializeComponent();

            // Create the merged file 
        }

        // Reference the two web services
        ApiHelper arxiv = new ApiHelper();
        ApiHelper core = new ApiHelper();

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            if (bunifuMaterialTextbox1.Text != "")
            {
                string searchQuery = bunifuMaterialTextbox1.Text;
                arxiv.url = "http://export.arxiv.org/api/query?search_query=" + searchQuery + "&max_results=5";
                arxiv.GetRequest();

                core.url = "https://core.ac.uk:443/api-v2/journals/search/" + searchQuery + "? page=1&pageSize=10&apiKey=rnBoaFLv8jiJd6N3XzECwule9SR7MPmH";
                core.GetRequest();

                // Start searching for events
                timer.Start();
                timer1.Start();
                
                resultsForm.ShowDialog();  //Show the settings form  instead of the app

            }
        }

        // Wait for the files to exist
        private void timer_Tick(object sender, EventArgs e)
        {
            if (File.Exists("Research_Results_XML.txt") && File.Exists("Research_Results_JSON.txt"))
            {
                LocalGatherer.mergeFiles();
                timer.Stop();   // Stop checking for the files to run the code once
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (File.Exists("Research_Results_Merged.txt"))
            {
                timer1.Stop();   // Stop checking for the files to run the code once
            }
        }
    }
}

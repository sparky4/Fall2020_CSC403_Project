using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;

namespace Fall2020_CSC403_Project
{
    public partial class Settings : Form
    {

        
        public Settings()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Code to be executed when the form is loaded
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            // Code to be executed when button1 is clicked
        }


        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Console.WriteLine("CloseMenu");
                this.Close();
            }
        }

        
    }
}

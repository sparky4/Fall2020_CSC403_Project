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
        private int WIDTH = 1155;
        private int HEIGHT = 687;


        public Settings()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Code to be executed when the form is loaded
        }
        
        // Completley Restarts Game when pressed
        private void RestartButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }


        private void FrmLevel_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        
    }
}

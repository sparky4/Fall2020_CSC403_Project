# Fall2020_CSC403_Project
12/20/23  Runaway button was added to the FrmBattle.cs file through the FrmBattle.cs[Design] option in the Windows Forms Designer.  The function was added to the frmBattle.cs file at line 161 (at time of this documentation)  :    
     private void runAwayButton_Click(object sender, EventArgs e)
     {

         instance = null;
             this.Close(); // Close the form (combat window)
    
     }
     Clicking on the button effectivly closes the combat window.  
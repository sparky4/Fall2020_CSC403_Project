# Fall2020_CSC403_Project

Documentation for EnemyDeleteFunction By Grant Gremillion:
    Parameters:
        Takes one argument -> enemy type:Enemy
    Functionality:
        Uses Enemy.Name attribute to determine which enemy needs to be deleted
        Also checks that the Windows Picture Box object corresponding with that enemy is in the component list
            If both statements above are true, the Windows Picture Box object for corresponding enemy is removed from the components list,
            removing the enemy from the screen.
        
    
=======
12/20/23  Runaway button was added to the FrmBattle.cs file through the FrmBattle.cs[Design] option in the Windows Forms Designer.  The function was added to the frmBattle.cs file at line 161 (at time of this documentation)  :    
     private void runAwayButton_Click(object sender, EventArgs e)
     {

         instance = null;
             this.Close(); // Close the form (combat window)
    
     }
     Clicking on the button effectivly closes the combat window.  



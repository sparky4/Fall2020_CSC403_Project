# Fall2020_CSC403_Project

!!KNOWN BUGS!!
	closing or running away from a battle will not free the nullified instance of that battle and 
	it resides in memory this will cause it to still be active and if you do a battle with the same
	enemy from before you will do double the damage. Memory cleanup is not good the game will
	eat ram like no other due to calling of new too many times, and no real way to check if it
	has been called for that enemy before.

=======

Documentation for controls by Victoria Crenshaw
	I basically took code i actually wrote myself and implemented it into this game
	the source is https://github.com/sparky4/16-0/blob/master/src/lib/16_in.c#L913
	I actually wrote that if else nightmare becuase i did not like the controls of this game and commander keen
	so i fixed it to do that.
	

=======

Documentation for the close battle window and run away calls by victoria Crenshaw
	okay so i fixed the bug where if you close the battle window the game will not crash anymore
	I did this with setting instance = null; when you close the windows or you run away.
	added a hook for when window is closing the ame calls a function and that function dose instance = null;

=======
Documentation for EnemyDeleteFunction By Grant Gremillion:
    Parameters:
        Takes one argument -> enemy type:Enemy
    Functionality:
        Uses Enemy.Name attribute to determine which enemy needs to be deleted
        Also checks that the Windows Picture Box object corresponding with that enemy is in the component list
            If both statements above are true, the Windows Picture Box object for corresponding enemy is removed from the components list,
            removing the enemy from the screen.

=======
Documentation for Settings Window By Grant Gremillion:
    Description :
        Corresponding settings class inherits from Form class. The window is displayed on top of the FrmLevel window when the escape key is pressed. 
    Buttons :
        Restart Button - When pressed, the FrmLevel Window is destroyed and re-created to start the game over
        Unpause Button - When pressed, the settings window instance is destroyed and the player can continue playing.

=======
12/20/23  Runaway button was added to the FrmBattle.cs file through the FrmBattle.cs[Design] option in the Windows Forms Designer.  The function was added to the frmBattle.cs file at line 161 (at time of this documentation)  :    
     private void runAwayButton_Click(object sender, EventArgs e)
     {

         instance = null;
             this.Close(); // Close the form (combat window)
    
     }
     Clicking on the button effectivly closes the combat window.  

========
Healing Button is added to the FrmBattle.cs with the help of FrmBattle[Design] otion from the designer. After that , the function was added .

private void button3_Click(object sender, EventArgs e)
        {
            const int HEAL_AMOUNT = 10; 
            player.Heal(HEAL_AMOUNT); // Calls the Heal method on the player.
            UpdateHealthBars();  /// Updates the UI to reflect the new health after healing.
        }

===============
Alert for Low Health 

Within the FrmBattle.cs file, there's a health-check mechanism in place. If a player's health drops below 10, they're prompted to use a health potion. Accepting this recovers 7 health points and the UI is updated to reflect the new health value



using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class FrmBattle : Form
    {
        public short lvl;
        public static FrmBattle instance = null;
        public bool in_use = true;
        private Enemy enemy;
        private Player player;

        private FrmBattle()
        {
            InitializeComponent();
            player = Game.player;
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            this.FormClosed += FrmBattle_FormClosed;
        }

        public void Setup()
        {
            // update for this enemy
            picEnemy.BackgroundImage = enemy.Img;
            picEnemy.Refresh();
            BackColor = enemy.Color;
            picBossBattle.Visible = false;

            // Observer pattern
            enemy.AttackEvent += PlayerDamage;
            player.AttackEvent += EnemyDamage;

            // show health
            UpdateHealthBars();
        }

        public void SetupForBossBattle()
        {
            picBossBattle.Location = Point.Empty;
            picBossBattle.Size = ClientSize;
            picBossBattle.Visible = true;

            SoundPlayer simpleSound = new SoundPlayer(Resources.final_battle);
            simpleSound.Play();

            tmrFinalBattle.Enabled = true;
        }

        public static FrmBattle GetInstance(Enemy enemy)
        {
            if (instance == null)
            {
                instance = new FrmBattle();
                instance.enemy = enemy;
                instance.Setup();
                instance.FormClosed += delegate { instance = null; };
            }
            return instance;
        }

        private static int battlesWon = 0;

        private void UpdateHealthBars()
        {
            float playerHealthPer = player.Health / (float)player.MaxHealth;
            float enemyHealthPer = enemy.Health / (float)enemy.MaxHealth;

            const int MAX_HEALTHBAR_WIDTH = 226;
            lblPlayerHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
            lblEnemyHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * enemyHealthPer);

            lblPlayerHealthFull.Text = player.Health.ToString();
            lblEnemyHealthFull.Text = enemy.Health.ToString();

            if (player.Health < 6)
            {
                this.BackColor = Color.Red; // Danger
            }
            else if (player.Health < 12)
            {
                this.BackColor = Color.Orange; // Warning
            }
            else
            {
                this.BackColor = Color.Green; // Safe
            }


        }

        private void btnAttack_Click(object sender, EventArgs e)
        {
            player.OnAttack(-4);
            if (enemy.Health > 0)
            {
                enemy.OnAttack(-2);
            }

            UpdateHealthBars();

            if (player.Health <= 0)
            {
                instance = null;
                Close();
            }

            if (enemy.Health <= 0)
            {
                DeleteEnemy(enemy);
                instance = null;
                Close();

                IncrementAndDisplayBattlesWon();
            }
        }
<<<<<<< HEAD

        private void IncrementAndDisplayBattlesWon()
        {
            battlesWon++;
            MessageBox.Show("Battles won: " + battlesWon, "Victory");
        }

=======
        
>>>>>>> d5bcfc1bac287e27a01c8530053a6e7128f22d54
        // Deletes enemy by removing them from the components list
        // .Parent gets the components list
        private void DeleteEnemy(Enemy enemy)
        {
            switch (lvl)
            {
                //level 1 enemies
                case 1:
                    if (enemy.Name == "Poison Packet" && Program.FrmLevelInstance.picEnemyPoisonPacket.Parent != null)
                    {
                        Program.FrmLevelInstance.picEnemyPoisonPacket.Parent.Controls.Remove(Program.FrmLevelInstance.picEnemyPoisonPacket);
                    }
                    else if (enemy.Name == "Cheeto" && Program.FrmLevelInstance.picEnemyCheeto.Parent != null)
                    {
                        Program.FrmLevelInstance.picEnemyCheeto.Parent.Controls.Remove(Program.FrmLevelInstance.picEnemyCheeto);
                    }
                    else if (enemy.Name == "Koolaid Man" && Program.FrmLevelInstance.picBossKoolAid.Parent != null)
                    {
                        Program.FrmLevelInstance.picBossKoolAid.Parent.Controls.Remove(Program.FrmLevelInstance.picBossKoolAid);
                    }
                    break;
                //level2 enemies
                case 2:
                    if (enemy.Name == "Poison Packet" && Program.FrmLevel2Instance.picEnemyPoisonPacket.Parent != null)
                    {
                        Program.FrmLevel2Instance.picEnemyPoisonPacket.Parent.Controls.Remove(Program.FrmLevel2Instance.picEnemyPoisonPacket);
                    }
                    else if (enemy.Name == "Cheeto" && Program.FrmLevel2Instance.picEnemyCheeto.Parent != null)
                    {
                        Program.FrmLevel2Instance.picEnemyCheeto.Parent.Controls.Remove(Program.FrmLevel2Instance.picEnemyCheeto);
                    }
                    else if (enemy.Name == "Koolaid Man" && Program.FrmLevel2Instance.picBossKoolAid.Parent != null)
                    {
                        Program.FrmLevel2Instance.picBossKoolAid.Parent.Controls.Remove(Program.FrmLevel2Instance.picBossKoolAid);
                    }
                break;
            }
        }


        
       

        
        private void PlayerDamage(int amount)
        {
            FlashScreen();
            player.AlterHealth(amount);
            CheckHealthAndPromptPotion();
            
        }

        private void FlashScreen()
        {
            var originalColor = this.BackColor;
            this.BackColor = Color.Red; // Color to flash
            Application.DoEvents();
            System.Threading.Thread.Sleep(100); // Duration of the flash in milliseconds
            this.BackColor = originalColor;
        }



        private void EnemyDamage(int amount)
        {
            enemy.AlterHealth(amount);
        }

       

        private void CheckHealthAndPromptPotion()
        {
            const int LOW_HEALTH_THRESHOLD = 10; // adjust this value based on your game's needs

            if (player.Health <= LOW_HEALTH_THRESHOLD)
            {
                DialogResult result = MessageBox.Show("Your health is low! Would you like to use a potion?",
                                                      "Low Health Warning",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    UsePotion();
                }
            }
        }

        private void UsePotion()
        {
            const int POTION_HEAL_AMOUNT = 7; // can adjust this value
            player.AlterHealth(POTION_HEAL_AMOUNT);
            UpdateHealthBars(); // Assuming you have this function to refresh health display
        }

        private void tmrFinalBattle_Tick(object sender, EventArgs e)
        {
            picBossBattle.Visible = false;
            tmrFinalBattle.Enabled = false;
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //in_use = false;
            instance = null;    //bug fix by sparky4
            GC.Collect();
        }

        private void FrmBattle_FormClosed(object sender, FormClosedEventArgs e)
        {
            enemy.AttackEvent -= PlayerDamage;
            player.AttackEvent -= EnemyDamage;
        }
        private void FrmBattle_Load(object sender, EventArgs e)
        {
        }

        private void blockButton_Click(object sender, EventArgs e)
        {
            //Code here to modify the effects of blocking
        }

        private void runAwayButton_Click(object sender, EventArgs e)
        {
            //in_use = false;
            instance = null;
            GC.Collect();
            this.Close(); // Close the form (combat window)
        }

        private void healButton_Click(object sender, EventArgs e)
        {
            const int HEAL_AMOUNT = 10; //logic
            player.Heal(HEAL_AMOUNT);
            UpdateHealthBars(); // To update the UI after Healing 
        }

        
        private void inventoryButton_Click(object sender, EventArgs e)
        {
            this.btnAttack.Visible = false;
            this.button1.Visible = false;
            this.button2.Visible = false;
            this.button3.Visible = false;
            this.button4.Visible = false;
            this.button8.Visible = true;

            foreach (var item in player.inventory.items)
            {
                if (item.Name == "gun")
                {
                    this.button5.Visible = true;
                }
                if (item.Name == "sword")
                {
                    this.button6.Visible = true;
                }
                if (item.Name == "sheild")
                {
                    this.button7.Visible = true;
                }
            }


        }

        private void useGun_Click(object sender, EventArgs e)
        {
            Console.WriteLine("gun");
        }
        private void useSword_Click(object sender, EventArgs e)
        {
            Console.WriteLine("sword");
        }
        private void useSheild_Click(object sender, EventArgs e)
        {
            Console.WriteLine("sheild");
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.btnAttack.Visible = true;
            this.button1.Visible = true;
            this.button2.Visible = true;
            this.button3.Visible = true;
            this.button4.Visible = true;
            this.button8.Visible = false;

            foreach (var item in player.inventory.items)
            {
                if (item.Name == "gun")
                {
                    this.button5.Visible = false;
                }
                if (item.Name == "sword")
                {
                    this.button6.Visible = false;
                }
                if (item.Name == "sheild")
                {
                    this.button7.Visible = false;
                }
            }
        }
    }
}

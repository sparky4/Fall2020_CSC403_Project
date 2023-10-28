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

        private void UpdateHealthBars()
        {
            float playerHealthPer = player.Health / (float)player.MaxHealth;
            float enemyHealthPer = enemy.Health / (float)enemy.MaxHealth;

            const int MAX_HEALTHBAR_WIDTH = 226;
            lblPlayerHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
            lblEnemyHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * enemyHealthPer);

            lblPlayerHealthFull.Text = player.Health.ToString();
            lblEnemyHealthFull.Text = enemy.Health.ToString();
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
            }
        }

        // Deletes enemy by removing them from the components list
        // .Parent gets the components list
        private void DeleteEnemy(Enemy enemy)
        {
            if (enemy.Name == "Poison Packet" & Program.FrmLevelInstance.picEnemyPoisonPacket.Parent != null)
            {
                Program.FrmLevelInstance.picEnemyPoisonPacket.Parent.Controls.Remove(Program.FrmLevelInstance.picEnemyPoisonPacket);
            }
            else if (enemy.Name == "Cheeto" & Program.FrmLevelInstance.picEnemyCheeto.Parent != null)
            {
                Program.FrmLevelInstance.picEnemyCheeto.Parent.Controls.Remove(Program.FrmLevelInstance.picEnemyCheeto);
            }
            else if (enemy.Name == "Koolaid Man" & Program.FrmLevelInstance.picBossKoolAid.Parent != null)
            {
                Program.FrmLevelInstance.picBossKoolAid.Parent.Controls.Remove(Program.FrmLevelInstance.picBossKoolAid);
            }
        }

        private void EnemyDamage(int amount)
        {
            enemy.AlterHealth(amount);
        }

        private void PlayerDamage(int amount)
        {
            player.AlterHealth(amount);
            CheckHealthAndPromptPotion();
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

        //disable close button
        /*private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
          get
          {
            CreateParams myCp = base.CreateParams;
            myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
            return myCp;
          }
        }*/

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

        private void button3_Click(object sender, EventArgs e)
        {
            const int HEAL_AMOUNT = 10; //logic
            player.Heal(HEAL_AMOUNT);
            UpdateHealthBars(); // To update the UI after Healing 
        }
    }
}

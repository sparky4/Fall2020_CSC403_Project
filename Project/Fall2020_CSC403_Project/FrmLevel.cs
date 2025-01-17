﻿using Fall2020_CSC403_Project.code;
using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using WMPLib;
using System.Windows.Input;


namespace Fall2020_CSC403_Project
{
    public partial class FrmLevel : Form
    {
        public short lvl = 1;
        private WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer();

        private Player player;

        public Enemy enemyPoisonPacket;
        public Enemy bossKoolaid;
        public Enemy enemyCheeto;
        private Character[] walls;


        public Inventory inventory;
        // Creating Items
        public Item gun;
        public Item sword;
        public Item sheild;

        //2 boundary's are needed to cover the top right of the screen,  
        private Collider portalToNextLevel1;
        private Collider portalToNextLevel2;

        //Condition to restrict more than one instance of next level from executing
        private bool levelCompleted = false;

        private DateTime timeBegin;
        private FrmBattle frmBattle;

        private int WindowWidth = 1155;
        private int WindowHeight = 687;
        private Settings settings;

        private void PlayBackgroundMusic()
        {
            //fix by sparky4
            mediaPlayer.URL = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\data\\song1.wav"; // your music file pat

            if (!File.Exists(mediaPlayer.URL))
            {
                mediaPlayer.settings.setMode("loop", true); // This will loop the music

                mediaPlayer.controls.play();
            }
        }

        public FrmLevel()
        {
            InitializeComponent();

            // Using mouseclick to get x and y coordinates
            this.MouseClick += FrmLevel_MouseClick;
        }
        // This goes within the FrmLevel class definition
   

        private void FrmLevel_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Get the X and Y coordinates of the mouse click
            int x = e.X;
            int y = e.Y;

      
            MessageBox.Show($"X: {x}, Y: {y}", "Mouse Click Coordinates");
        }

        private void CompleteLevel()
        {
            mediaPlayer.controls.stop();
            //FrmLevel2 frmLevel2 = new FrmLevel2();
            Program.FrmLevel2Instance = new FrmLevel2(player,player.inventory);
            this.Hide();
            //frmLevel2.ShowDialog(); // Or use Show() if you don't want it to be modal.
            Program.FrmLevel2Instance.ShowDialog();
            this.Close(); // Close FrmLevel if you're done with it.
        }


        private void FrmLevel_Load(object sender, EventArgs e)
        {
            PlayBackgroundMusic();
            const int PADDING = 7;
            const int NUM_WALLS = 13;

            // Initialize the first portal collider
            Rectangle portalRect1 = new Rectangle(1020, 0, 133, 5); // (x, y, width, height)
            portalToNextLevel1 = new Collider(portalRect1);

            // Initialize the second portal collider
            Rectangle portalRect2 = new Rectangle(1153, 5, 5, 272);  
            portalToNextLevel2 = new Collider(portalRect2);

            player = new Player(CreatePosition(picPlayer), CreateCollider(picPlayer, PADDING));
            bossKoolaid = new Enemy(CreatePosition(picBossKoolAid), CreateCollider(picBossKoolAid, PADDING), "Koolaid Man");
            enemyPoisonPacket = new Enemy(CreatePosition(picEnemyPoisonPacket), CreateCollider(picEnemyPoisonPacket, PADDING), "Poison Packet");
            enemyCheeto = new Enemy(CreatePosition(picEnemyCheeto), CreateCollider(picEnemyCheeto, PADDING), "Cheeto");

            // Giving values to all items
            
            gun = new Item(CreatePosition(picGun), CreateCollider(picGun, PADDING),"gun");
            gun.Img = picGun.Image;
            

            sword = new Item(CreatePosition(picSword), CreateCollider(picSword, PADDING),"sword");
            sword.Img = picSword.Image;


            sheild = new Item(CreatePosition(picSheild), CreateCollider(picSheild, PADDING),"sheild");
            sheild.Img = picSheild.Image;


            inventory = player.inventory;



            bossKoolaid.Img = picBossKoolAid.BackgroundImage;
            enemyPoisonPacket.Img = picEnemyPoisonPacket.BackgroundImage;
            enemyCheeto.Img = picEnemyCheeto.BackgroundImage;

            bossKoolaid.Color = Color.Red;
            enemyPoisonPacket.Color = Color.Green;
            enemyCheeto.Color = Color.FromArgb(255, 245, 161);

            walls = new Character[NUM_WALLS];
            for (int w = 0; w < NUM_WALLS; w++)
            {
                PictureBox pic = Controls.Find("picWall" + w.ToString(), true)[0] as PictureBox;
                walls[w] = new Character(CreatePosition(pic), CreateCollider(pic, PADDING));
            }

            Game.player = player;
            timeBegin = DateTime.Now;
        }

        private Vector2 CreatePosition(PictureBox pic)
        {
            return new Vector2(pic.Location.X, pic.Location.Y);
        }

        private Collider CreateCollider(PictureBox pic, int padding)
        {
            Rectangle rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
            return new Collider(rect);
        }

        private void FrmLevel_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            player.ResetMoveSpeed();
        }

        private void tmrUpdateInGameTime_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now - timeBegin;
            string time = span.ToString(@"hh\:mm\:ss");
            lblInGameTime.Text = "Time: " + time.ToString();
            lblHealth.Text = "Health: " + player.Health.ToString();
        }

        private void tmrPlayerMove_Tick(object sender, EventArgs e)
        {
            //death check
            if (player.Health <= 0)
            {
                player.Death();
                this.panel1.Show();
            }

            //player movement
            if (player.Health > 0)
            {
                player.Move();
            }

            //wall collision
            if (HitAWall(player))
            {
                player.MoveBack();
            }

            //enemy collision
            if (HitAChar(player, enemyPoisonPacket) && enemyPoisonPacket.Health > 0)
            {
                Fight(enemyPoisonPacket);
            }
            if (HitAChar(player, enemyCheeto) && enemyCheeto.Health > 0)
            {
                Fight(enemyCheeto);
            }
            if (HitAChar(player, bossKoolaid) && bossKoolaid.Health > 0)
            {
                Fight(bossKoolaid);
            }
            if (!levelCompleted && IsAtPortal())
            {
                levelCompleted = true;
                CompleteLevel();
            }

            if (HitAnItem(player, gun))
            {
                // We must check that the gun exists before attmpting to delete it from the map
                if (Program.FrmLevelInstance.picGun.Parent != null)
                {
                    inventory.AddItem(gun);
                    Program.FrmLevelInstance.picGun.Parent.Controls.Remove(Program.FrmLevelInstance.picGun);
                }
            }

            if (HitAnItem(player, sword))
            {

                if (Program.FrmLevelInstance.picSword.Parent != null)
                {
                    inventory.AddItem(sword);
                    Program.FrmLevelInstance.picSword.Parent.Controls.Remove(Program.FrmLevelInstance.picSword);
                }
            }

            if (HitAnItem(player, sheild))
            {

                if (Program.FrmLevelInstance.picSheild.Parent != null)
                {
                    inventory.AddItem(sheild);
                    Program.FrmLevelInstance.picSheild.Parent.Controls.Remove(Program.FrmLevelInstance.picSheild);
                }
            }

            picPlayer.Location = new Point((int)player.Position.x, (int)player.Position.y);
        }
        private bool IsAtPortal()
        {
            return player.Collider.Intersects(portalToNextLevel1) ||
                   player.Collider.Intersects(portalToNextLevel2);
        }

        private bool HitAnItem(Character you, Item item)
        {
            return you.Collider.Intersects(item.Collider);
        }


        private bool HitAWall(Character c)
        {
            bool hitAWall = false;
            for (int w = 0; w < walls.Length; w++)
            {
                if (c.Collider.Intersects(walls[w].Collider))
                {
                    hitAWall = true;
                    break;
                }
            }
            return hitAWall;
        }

        private bool HitAChar(Character you, Character other)
        {
            return you.Collider.Intersects(other.Collider);
        }

        private void Fight(Enemy enemy)
        {
            player.ResetMoveSpeed();
            player.MoveBack();
            frmBattle = FrmBattle.GetInstance(enemy);
            frmBattle.lvl = lvl;
            if (frmBattle != null)
            {
                frmBattle.Show();

                if (enemy == bossKoolaid)
                {
                    frmBattle.SetupForBossBattle();
                }
            }
        }

        private void FrmLevel_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // The player wants to access their inventory
            if (e.KeyCode == Keys.I)
            {
                inventory.DisplayInventory();
            }

            // The player wants to access the settings
            if (e.KeyCode == Keys.Escape)
            {
                settings = new Settings();
                settings.Text = "Settings";
                settings.BackColor = System.Drawing.Color.LightBlue;
                settings.Show();
            }


            //movement
            // by sparky4
            if (Keyboard.IsKeyDown(Key.Up) && Keyboard.IsKeyDown(Key.Left))
            { player.GoUpLeft(); }// player.GoLeft(); Console.WriteLine("up left"); }

            else if (Keyboard.IsKeyDown(Key.Up) && Keyboard.IsKeyDown(Key.Right))
            { player.GoUpRight(); }// player.GoRight(); Console.WriteLine("up right"); }

            else if (Keyboard.IsKeyDown(Key.Down) && Keyboard.IsKeyDown(Key.Left))
            { player.GoDownLeft(); }// player.GoLeft(); Console.WriteLine("down left"); }

            else if (Keyboard.IsKeyDown(Key.Down) && Keyboard.IsKeyDown(Key.Right))
            { player.GoDownRight(); }// player.GoRight(); Console.WriteLine("down right"); }
            else
            //  movement for 1 directions
            if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
            {
                if ((e.KeyCode == Keys.Up && e.KeyCode != Keys.Down))
                    player.GoUp();
                if ((e.KeyCode == Keys.Down && e.KeyCode != Keys.Up))
                    player.GoDown();
            }
            else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
            {
                if ((e.KeyCode == Keys.Left && e.KeyCode != Keys.Right))
                    player.GoLeft();
                if ((e.KeyCode == Keys.Right && e.KeyCode != Keys.Left))
                    player.GoRight();
            }
            
        }

        private void lblInGameTime_Click(object sender, EventArgs e)
        {

        }
    }
}

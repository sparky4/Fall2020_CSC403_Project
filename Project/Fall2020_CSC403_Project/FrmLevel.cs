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
        private WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer();

        private Player player;

        private Enemy enemyPoisonPacket;
        private Enemy bossKoolaid;
        private Enemy enemyCheeto;
        private Character[] walls;


        private Inventory inventory;
        // Creating Items
        private Item gun;
        private Item sword;
        private Item sheild;


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
        }

        private void FrmLevel_Load(object sender, EventArgs e)
        {
            PlayBackgroundMusic();
            const int PADDING = 7;
            const int NUM_WALLS = 13;

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


            inventory = new Inventory();



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

            if (HitAnItem(player, gun))
            {
                // We must check that the gun exists before attmpting to delete it
                if (Program.FrmLevelInstance.picGun.Parent != null)
                {
                    inventory.AddItem(gun);
                    inventory.DisplayInventory();
                    Program.FrmLevelInstance.picGun.Parent.Controls.Remove(Program.FrmLevelInstance.picGun);
                }
            }

            if (HitAnItem(player, sword))
            {

                if (Program.FrmLevelInstance.picSword.Parent != null)
                {
                    inventory.AddItem(sword);
                    inventory.DisplayInventory();
                    Program.FrmLevelInstance.picSword.Parent.Controls.Remove(Program.FrmLevelInstance.picSword);
                }
            }

            if (HitAnItem(player, sheild))
            {

                if (Program.FrmLevelInstance.picSheild.Parent != null)
                {
                    inventory.AddItem(sheild);
                    inventory.DisplayInventory();
                    Program.FrmLevelInstance.picSheild.Parent.Controls.Remove(Program.FrmLevelInstance.picSheild);
                }
            }

            picPlayer.Location = new Point((int)player.Position.x, (int)player.Position.y);
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
            if (e.KeyCode == Keys.Escape)
            {
                settings = new Settings();
                settings.Text = "Settings";
                settings.BackColor = System.Drawing.Color.LightBlue;
                settings.Show();
            }


            //diagnal movement not possible as the e.KeyCode is only able to read 1 key at a time
            //sorry --sparky4

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
            //else player.ResetMoveSpeed();
            /*
            switch (e.KeyCode)
            {
                case Keys.Left:
                    player.GoLeft();
                    break;

                case Keys.Right:
                    player.GoRight();
                    break;

                case Keys.Up:
                    player.GoUp();
                    break;

                case Keys.Down:
                    player.GoDown();
                    break;

                default:
                    player.ResetMoveSpeed();
                    break;
           
            }
                   */
        }

        private void lblInGameTime_Click(object sender, EventArgs e)
        {

        }
    }
}

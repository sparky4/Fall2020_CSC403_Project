using Fall2020_CSC403_Project.code;
using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using WMPLib;

namespace Fall2020_CSC403_Project {
  public partial class FrmLevel : Form {
        private WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer();

    private Player player;

    private Enemy enemyPoisonPacket;
    private Enemy bossKoolaid;
    private Enemy enemyCheeto;
    private Character[] walls;

    private DateTime timeBegin;
    private FrmBattle frmBattle;

    private int WindowWidth = 1155;
    private int WindowHeight = 687;
    private Settings settings;
    
    private void PlayBackgroundMusic()
        {
                mediaPlayer.URL = "song1.wav"; // your music file path
            if (!File.Exists(mediaPlayer.URL))
            {
                mediaPlayer.settings.setMode("loop", true); // This will loop the music
                mediaPlayer.controls.play();
            }
            Console.WriteLine(Directory.GetCurrentDirectory());
        }

    public FrmLevel() {
      InitializeComponent();
    }

    private void FrmLevel_Load(object sender, EventArgs e) {
            PlayBackgroundMusic();
      const int PADDING = 7;
      const int NUM_WALLS = 13;

      player = new Player(CreatePosition(picPlayer), CreateCollider(picPlayer, PADDING));
      bossKoolaid = new Enemy(CreatePosition(picBossKoolAid), CreateCollider(picBossKoolAid, PADDING), "Koolaid Man");
      enemyPoisonPacket = new Enemy(CreatePosition(picEnemyPoisonPacket), CreateCollider(picEnemyPoisonPacket, PADDING), "Poison Packet");
      enemyCheeto = new Enemy(CreatePosition(picEnemyCheeto), CreateCollider(picEnemyCheeto, PADDING), "Cheeto");

      bossKoolaid.Img = picBossKoolAid.BackgroundImage;
      enemyPoisonPacket.Img = picEnemyPoisonPacket.BackgroundImage;
      enemyCheeto.Img = picEnemyCheeto.BackgroundImage;

      bossKoolaid.Color = Color.Red;
      enemyPoisonPacket.Color = Color.Green;
      enemyCheeto.Color = Color.FromArgb(255, 245, 161);

      walls = new Character[NUM_WALLS];
      for (int w = 0; w < NUM_WALLS; w++) {
        PictureBox pic = Controls.Find("picWall" + w.ToString(), true)[0] as PictureBox;
        walls[w] = new Character(CreatePosition(pic), CreateCollider(pic, PADDING));
      }

      Game.player = player;
      timeBegin = DateTime.Now;
    }

    private Vector2 CreatePosition(PictureBox pic) {
      return new Vector2(pic.Location.X, pic.Location.Y);
    }

    private Collider CreateCollider(PictureBox pic, int padding) {
      Rectangle rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
      return new Collider(rect);
    }

    private void FrmLevel_KeyUp(object sender, KeyEventArgs e) {
      player.ResetMoveSpeed();
    }

    private void tmrUpdateInGameTime_Tick(object sender, EventArgs e) {
      TimeSpan span = DateTime.Now - timeBegin;
      string time = span.ToString(@"hh\:mm\:ss");
      lblInGameTime.Text = "Time: " + time.ToString();
      lblHealth.Text = "Health: " + player.Health.ToString();
    }

    private void tmrPlayerMove_Tick(object sender, EventArgs e) {
                    
                    //death check
                    if (player.Health <= 0)
                        {
                            player.Death();
                            this.panel1.Show();
                            Console.WriteLine("Dead!");
                        }

                    //player movement
                    if (player.Health > 0)
                    {
                        player.Move();
                        //Console.WriteLine("Still Alive!");
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

                    picPlayer.Location = new Point((int)player.Position.x, (int)player.Position.y);
    }

    private bool HitAWall(Character c) {
      bool hitAWall = false;
      for (int w = 0; w < walls.Length; w++) {
        if (c.Collider.Intersects(walls[w].Collider)) {
          hitAWall = true;
          break;
        }
      }
      return hitAWall;
    }

    private bool HitAChar(Character you, Character other) {
      return you.Collider.Intersects(other.Collider);
    }

    private void Fight(Enemy enemy) {
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

        private void FrmLevel_KeyDown(object sender, KeyEventArgs e) {

            if (e.KeyCode == Keys.Escape)
            {
                Console.WriteLine("OpenSettings");
                settings = new Settings();
                settings.Text = "Settings";
                settings.BackColor = System.Drawing.Color.LightBlue;
                settings.Show();
            }

            //diagnal movement not possible as the e.KeyCode is only able to read 1 key at a time
            //sorry --sparky4
/*            if (e.KeyCode == Keys.Up && e.KeyCode == Keys.Left)
            { player.GoUp(); player.GoLeft(); }

            else if (e.KeyCode == Keys.Up && e.KeyCode == Keys.Right)
            { player.GoUp(); player.GoRight(); }

            else if (e.KeyCode == Keys.Down && e.KeyCode == Keys.Left)
            { player.GoDown(); player.GoLeft(); }

            else if (e.KeyCode == Keys.Down && e.KeyCode == Keys.Right)
            { player.GoDown(); player.GoRight(); }
            else*/
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
            else player.ResetMoveSpeed();   // do not move
            //old code below
            /*                switch (e.KeyCode) {
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
                  }*/
        }

    private void lblInGameTime_Click(object sender, EventArgs e) {

    }
    }
}

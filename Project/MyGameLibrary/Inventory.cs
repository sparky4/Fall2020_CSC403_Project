using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Fall2020_CSC403_Project.code
{
    public class Inventory
    {
        public List<Item> items;
        public Form inventoryWindow;

        public Inventory()
        {
            items = new List<Item>();
        }


        public void AddItem(Item item)
        {
            items.Add(new Item(item.Position, item.Collider, item.Name));
        }


        public void DisplayInventory()
        {
            inventoryWindow = new Form();
            inventoryWindow.Width = 400;
            inventoryWindow.Height = 200;
            inventoryWindow.Text = "Inventory";
            inventoryWindow.BackColor = System.Drawing.Color.DarkRed;

            foreach (var item in items)
            {
                if (item.Name == "gun")
                {
                    PictureBox gun = new PictureBox();
                    // Set PictureBox properties
                    gun.Location = new Point(10, 50); // Set the location
                    gun.Size = new Size(100, 100);    // Set the size

                    // Load an image (replace "yourImageFileName.png" with the actual file name of your image)

                    gun.Image = Image.FromFile(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\data\\gun.fw.png");

                    // Set PictureBoxSizeMode (adjust as needed)
                    gun.SizeMode = PictureBoxSizeMode.StretchImage;

                    inventoryWindow.Controls.Add(gun);
                }
                if (item.Name == "sword")
                {
                    PictureBox sword = new PictureBox();
    
                    sword.Location = new Point(140, 50); 
                    sword.Size = new Size(100, 100);    


                    sword.Image = Image.FromFile(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\data\\sword.jpg");


                    sword.SizeMode = PictureBoxSizeMode.StretchImage;

                    inventoryWindow.Controls.Add(sword);
                }
                if (item.Name == "sheild")
                {
                    PictureBox sheild = new PictureBox();
       
                    sheild.Location = new Point(270, 50);
                    sheild.Size = new Size(100, 100);    


                    sheild.Image = Image.FromFile(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\data\\sheild.jpg");

                    sheild.SizeMode = PictureBoxSizeMode.StretchImage;

                    inventoryWindow.Controls.Add(sheild);
                }

            }

            
            inventoryWindow.Show();


        }

    }
}

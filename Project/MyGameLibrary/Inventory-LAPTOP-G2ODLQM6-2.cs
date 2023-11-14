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
            this.items = new List<Item>();
            this.inventoryWindow = new Form();
        }


        public void AddItem(Item item)
        {
            items.Add(new Item(item.Position, item.Collider, item.Name));
        }


        public void DisplayInventory()
        {
            
            inventoryWindow.Width = 400;
            inventoryWindow.Height = 200;
            inventoryWindow.Text = "Inventory";
            inventoryWindow.BackColor = System.Drawing.Color.DarkRed;

            foreach (var item in items)
            {

                if (item.Name == "gun")
                {
                    Button gun = new Button();
                    // Set Button properties
                    gun.Location = new Point(10, 50); // Set the location
                    gun.Size = new Size(100, 100);    // Set the size

                    // Load an image (replace "yourImageFileName.png" with the actual file name of your image)

                    gun.BackgroundImage = Image.FromFile(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\data\\gun.fw.png");
                    gun.BackgroundImageLayout = ImageLayout.Stretch;
                    inventoryWindow.Controls.Add(gun);
                }
                if (item.Name == "sword")
                {
                    Button sword = new Button();
    
                    sword.Location = new Point(140, 50); 
                    sword.Size = new Size(100, 100);    


                    sword.BackgroundImage = Image.FromFile(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\data\\sword.jpg");
                    sword.BackgroundImageLayout = ImageLayout.Stretch;

                    inventoryWindow.Controls.Add(sword);
                }
                if (item.Name == "sheild")
                {
                    Button sheild = new Button();
       
                    sheild.Location = new Point(270, 50);
                    sheild.Size = new Size(100, 100);    


                    sheild.BackgroundImage = Image.FromFile(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + "\\data\\sheild.jpg");

                    sheild.BackgroundImageLayout = ImageLayout.Stretch;

                    inventoryWindow.Controls.Add(sheild);
                }

            }

            
            inventoryWindow.Show();


        }

    }
}

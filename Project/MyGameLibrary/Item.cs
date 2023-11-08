using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Fall2020_CSC403_Project.code
{
    public class Item 
    {

        public string Name { get; set; }
        public Image Img { get; set; }

        public Vector2 Position { get; private set; }

        public Collider Collider { get; private set; }


        public Item(Vector2 Pos, Collider collider, String name)
        {
            Position = Pos;
            Collider = collider;
            Name = name;
        }






    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code
{
    public class Inventory
    {
        public List<Item> items;

        public Inventory()
        {
            items = new List<Item>();
        }


        public void AddItem(Item item)
        {
            items.Add(new Item(item.Position,item.Collider,item.Name));
            Console.WriteLine("The nodes of items are: " + string.Join(",", items));
        }


        public void DisplayInventory()
        {
            Console.WriteLine("Inventory:");

            foreach (var item in items)
            {
                String name = item.Name;
                Console.WriteLine(name);
            }

            Console.WriteLine();
        }

    }
}

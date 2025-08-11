using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Bags : Item
    {
        Inventory _inventory;

        public Bags(string[] idents, string name, string desc) : base(idents, name, desc)
        {
            _inventory = new Inventory();
        }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            else if (_inventory.HasItem(id))
            {
                return _inventory.Fetch(id);
            } return null;
        }

        public override string FullDescription
        {
            get { return $"In the {Name} you can see:\n{_inventory.ItemList()}" ; }
        }

        public Inventory Inventory
        {
            get { return _inventory; }
        }
    }
}

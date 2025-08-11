using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Player : GameObject, IHaveInventory
    {
        Inventory _inventory;

        public Player (string name, string desc) : base(new string[] {"me", "inventory"}, name, desc)
        {
            _inventory = new Inventory();
        }

        public GameObject Locate(string id) 
        {
            if (AreYou(id))
            {
                return this;
            }
            var itm = _inventory.Fetch(id);
            if (itm != null)
            {
                return itm;
            }
            if (Location != null)
            {
                return Location.Locate(id);
            }
            return null;
        }

        public override string FullDescription 
        {
            get
            {
                return $"{Name}, {base.ShortDescription}.You are carrying: {_inventory.ItemList()}";
            }
        }

        public Inventory Inventory { get { return _inventory; } }

        public Location Location { get; set; }
    }
}

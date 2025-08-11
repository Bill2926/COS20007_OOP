using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Location : GameObject, IHaveInventory
    {
        Inventory _inventory;

        public Location(string[] idents, string name, string desc) : base(idents, name, desc)
        {
            _inventory = new Inventory();
        }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            return _inventory.Fetch(id);
        }

        public override string FullDescription
        {
            get
            {
                return $"You are in: {Name}, {base.FullDescription}. Here you can see: {_inventory.ItemList()}";
            }
        }

        public Inventory Inventory
        {
            get { return _inventory; }
        }
    }
}

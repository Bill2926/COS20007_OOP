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
        List<Path>? _paths;

        public Location(string[] idents, string name, string desc) : base(idents, name, desc)
        {
            _inventory = new Inventory();
            _paths = null;
        }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }

            GameObject item = _inventory.Fetch(id);
            if (item != null)
            {
                return item;
            }

            if (_paths != null)
            {
                foreach (Path path in _paths)
                {
                    if (path.AreYou(id))
                    {
                        return path;
                    }
                }
            }
            return null;
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

        public void AddPath(Path path)
        {
            if (_paths == null)
            {
                _paths = new List<Path>();
            }
            _paths.Add(path);
        }

        public Path Path
        {
            get { return _paths?[0]; } // But only if list is guaranteed to have at least 1
        }
    }
}

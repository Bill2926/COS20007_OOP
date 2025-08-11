using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public abstract class GameObject : IdenObj
    {
        string _description;
        string _name;

        public GameObject(string[] idents, string name, string desc) : base(idents)
        {
            _name = name;
            _description = desc;
        }

        public string Name { get { return _name; } }

        public string ShortDescription { get { return $"{_name} ({FirstId})"; } }

        public virtual string FullDescription { get { return _description; } }
    }
}

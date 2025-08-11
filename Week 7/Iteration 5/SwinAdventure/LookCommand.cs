using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace SwinAdventure
{
    public class LookCommand : Command
    {
        IHaveInventory container;
        GameObject item;
        Player p;

        public LookCommand() : base(["look"]) { }

        public override string Execute(Player p, string[] text)
        {
            if (text.Length == 3 || text.Length == 5)
            {
                if (text[0] != "look")
                    return "Error in look input";
                if (text[1] != "at")
                    return "What do you want to look at?";
                if (text.Length == 5 && text[3] != "in")
                    return "What do you want to look in?";
                if (text.Length == 3)
                {
                    container = p;
                }
                else
                {
                    container = FetchContainer(p, text[4]);
                    if (container == null)
                        return $"I cannot find the {text[4]}";
                }

                return LookAtIn(text[2], container);
            }
            else
                return "I don't know how to look like that";
        }

        private IHaveInventory? FetchContainer(Player p, string containerId)
        {
            return p.Locate(containerId) as IHaveInventory;
        }

        private string LookAtIn(string thingId, IHaveInventory container)
        {
            if (container.Locate(thingId) != null)
            {
                return container.Locate(thingId).FullDescription;
            }
            else
                return $"I cannot find the {thingId}";
        }
    }
}

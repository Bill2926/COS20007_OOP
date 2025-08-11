namespace SwinAdventure
{
    public class MoveCommand : Command
    {
        Player p;

        public MoveCommand() : base(["move", "go", "head", "leave"])
        { }

        public override string Execute(Player p, string[] text)
        {
            if (text.Length < 2)
            {
                return "I don't know how to move like that";
            }

            string id = text[1].ToLower();
            GameObject obj = p.Location.Locate(id);

            if (obj == null)
            {
                return "There is no path in that direction.";
            }

            Path path = obj as Path;
            if (path != null)
            {
                p.Location = path.Destination;
                return $"You move {id} to {path.Destination.Name}";
            }
            else
                return "That doesn't seem like a valid path.";
        }
    }
}

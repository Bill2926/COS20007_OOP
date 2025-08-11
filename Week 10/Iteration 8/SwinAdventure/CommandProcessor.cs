namespace SwinAdventure
{
    public class CommandProcessor : Command
    {
        readonly List<Command> commands;

        public CommandProcessor() : base(["Command"])
        {
            commands =
            [
                new MoveCommand(),
                new LookCommand(),
            ];
        }

        public override string Execute(Player p, string[] text)
        {
            if (text.Length == 0 || string.IsNullOrWhiteSpace(text[0]))
                return "Please enter a command.";

            string userInput = text[0].ToLower();

            foreach (Command command in commands)
            {
                if (command.AreYou(userInput))
                {
                    return command.Execute(p, text);
                }
            }

            return "I can not find that command!";
        }

    }
}

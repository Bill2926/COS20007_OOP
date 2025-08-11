namespace SwinAdventure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter your player's name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter player's description: ");
            string des = Console.ReadLine();

            // Create a player and some items
            Player player = new(name, des);
            Item itm1 = new(["hdmi"], "HDMI cord", "can connect to large screen");
            Item itm2 = new(["usb"], "an USB", "can store up to 1TB of data");
            Bags bag = new(["bag"], "a bag", "this bag is made by leather");
            Item itm3 = new(["mouse"], "a mouse", "gaming mouse with 0 latency");
            Command lookCommand = new LookCommand();

            player.Inventory.Put(itm1);
            player.Inventory.Put(itm2);
            player.Inventory.Put(bag);
            bag.Inventory.Put(itm3);

            //loop command
            while (true)
            {
                Console.WriteLine("Enter what do you want to find:");
                string userInput = Console.ReadLine();
                if (userInput.ToLower() != "exit")
                {
                    string[] userCommand = userInput.Split(' ');
                    string result = lookCommand.Execute(player, userCommand);
                    Console.WriteLine(" ");
                    Console.WriteLine($"Description for {userCommand[2]}:");
                    Console.WriteLine($"{result}\n");
                }
                else break;
            }
            Console.WriteLine("Iteration 5 finished !");
        }
    }
}
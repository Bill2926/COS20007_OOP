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
            Location duytan = new(["duytan"], "duytan", "Innovation Center of Swinburne");
            Location duongkhue = new(["duongkhue"], "duongkhue", "Global Citizen Education");
            Path north = new(["north"], "north move", "Duy Tan street", duytan);
            Path south = new(["south"], "south move", "Cau Giay street", duongkhue);
            Command moveCommand = new MoveCommand();


            player.Inventory.Put(itm1);
            player.Inventory.Put(itm2);
            player.Inventory.Put(bag);
            bag.Inventory.Put(itm3);
            duytan.Inventory.Put(itm1);
            player.Location = duytan;
            duytan.AddPath(south);
            duongkhue.AddPath(north);

            string[] MoveCommandWord = { "move", "head", "go" };

            //loop command
            while (true)
            {
                Console.WriteLine("What do you want to find or move?");
                string userInput = Console.ReadLine();
                string[] userCommand = userInput.Split(' ');
                if (userInput.ToLower() != "exit")
                {
                    if (userInput == "look")
                    {
                        //string[] userCommand = [userInput];
                        string result = lookCommand.Execute(player, userCommand);
                        Console.WriteLine(" ");
                        Console.WriteLine(result);
                    }
                    if (MoveCommandWord.Contains(userCommand[0]))
                    {
                        //string[] userCommand = userInput.Split(' ');
                        string result = moveCommand.Execute(player, userCommand);
                        Console.WriteLine(" ");
                        Console.WriteLine(result);
                    }
                    else
                    {
                        //string[] userCommand = userInput.Split(' ');
                        string result = lookCommand.Execute(player, userCommand);
                        Console.WriteLine(" ");
                        Console.WriteLine($"{result}\n");
                    }
                }
                else break;
            }
            Console.WriteLine("Iteration 5 finished !");
        }
    }
}
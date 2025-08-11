namespace SwinAdventure;
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter your current major: ");
        string major = Console.ReadLine();

        //Create player and command processor
        Player player = new(name, major);
        CommandProcessor cp = new();
        
        //Create other thing
        Item itm1 = new(["hdmi"], "HDMI cord", "can connect to large screen");
        Item itm2 = new(["usb"], "an USB", "can store up to 1TB of data");
        Item itm3 = new(["mouse"], "a mouse", "gaming mouse with 0 latency");
        Bags bag = new(["bag"], "a bag", "this bag is made by leather");
        Location duytan = new(["duytan"], "duytan", "Innovation Center of Swinburne");
        Location duongkhue = new(["duongkhue"], "duongkhue", "Global Citizen Education");
        Path north = new(["north"], "north move", "Duy Tan street", duytan);
        Path south = new(["south"], "south move", "Cau Giay street", duongkhue);

        //Add them
        player.Inventory.Put(itm1);
        player.Inventory.Put(itm2);
        player.Inventory.Put(bag);
        bag.Inventory.Put(itm3);
        duytan.Inventory.Put(itm1);
        player.Location = duytan;
        duytan.AddPath(south);
        duongkhue.AddPath(north);

        //Start the program
        while (true)
        {
            Console.Write("What do you want to do? (type 'exit' to quit)\n> ");
            string input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            if (input.ToLower() == "exit")
                break;

            string[] commandWords = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string result = cp.Execute(player, commandWords);
            Console.WriteLine(result);
        }
    }
}
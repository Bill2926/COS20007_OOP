namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Message myMessage = new("Hello, World! Greetings from Message Object. My student ID is SWH02701/105547489");
            myMessage.Print();
            string? name;
            Message[] messages = {new ("Hi Minh! How are you today"),
                                  new ("Hi Tuan! What are you doing"),
                                  new ("Hello Hoang! Long time no see"),
                                  new ("Welcome back Manh!"),
                                  new ("Hi Mysterious man! How are you")};

            do
            {
                Console.WriteLine("Enter your name below:");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("No name entered");
                }
            } while (string.IsNullOrWhiteSpace(name));
            name = name.ToLower();
            if (name == "minh")
            {
                messages[0].Print();
            }
            else if (name == "tuan")
            {
                messages[1].Print();
            }
            else if (name == "hoang")
            {
                messages[2].Print();
            }
            else if (name == "manh")
            {
                messages[3].Print();
            }
            else
            {
                messages[4].Print();
            }
        }
    }
}




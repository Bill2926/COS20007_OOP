namespace AntiPatternsLab
{
    public class GodObject
    {
        public string username = "admin";
        public string password = "1234";

        public void Login()
        {
            Console.WriteLine("Login System");
            Console.Write("Enter username: ");
            var u = Console.ReadLine();
            Console.Write("Enter password: ");
            var p = Console.ReadLine();

            if (u == username && p == password)
                Console.WriteLine("Login success!");
            else
                Console.WriteLine("Access denied.");
        }

        public void AccessAdmin()
        {
            Console.WriteLine("Showing sensitive admin data...");
        }
    }
}

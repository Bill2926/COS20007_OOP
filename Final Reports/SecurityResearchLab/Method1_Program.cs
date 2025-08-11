namespace SecurityResearchLab
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-= Secure Login Test =-");
            Credential userInput = CredentialFactory.Create();
            IDataAccess data = new SecureProxy(userInput);

            data.AccessSensitiveData();
        }

    }

    //Without Secure Desgin
    //namespace LoginSystemInsecure
    //{
    //    class Program
    //    {
    //        static string storedUsername = "admin";
    //        static string storedPassword = "1234";

    //        static void Main(string[] args)
    //        {
    //            Console.WriteLine("=== Insecure Login System ===");

    //            Console.Write("Enter username: ");
    //            string user = Console.ReadLine();

    //            Console.Write("Enter password: ");
    //            string pass = Console.ReadLine();

    //            if (user == storedUsername && pass == storedPassword)
    //            {
    //                Console.WriteLine("Login successful!");
    //                Console.WriteLine("Accessing sensitive data...");
    //            }
    //            else
    //            {
    //                Console.WriteLine("Invalid credentials.");
    //            }
    //        }
    //    }
    //}

}

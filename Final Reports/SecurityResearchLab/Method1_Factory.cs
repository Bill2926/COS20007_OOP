namespace SecurityResearchLab
{
    public class Credential
    {
        public string Username { get; }
        public string Password { get; }

        public Credential(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public class CredentialFactory
    {
        public static Credential Create()
        {
            Console.Write("Enter username: ");
            string user = Console.ReadLine();

            Console.Write("Enter password: ");
            string pass = Console.ReadLine();

            return new Credential(user, pass);
        }
    }

}

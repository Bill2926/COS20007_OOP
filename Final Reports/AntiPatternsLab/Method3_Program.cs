namespace AntiPatternsLab
{
    public class Program
    {
        static void Main(string[] args)
        {
            GodObject god = new();
            god.Login();
            god.AccessAdmin();
        }
    }
}

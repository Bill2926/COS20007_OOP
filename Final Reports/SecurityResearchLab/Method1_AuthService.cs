namespace SecurityResearchLab
{
    public class AuthService
    {
        private static AuthService _instance;
        private static readonly object _lock = new();

        private string storedUsername = "admin";
        private string storedPassword = "1234";

        private AuthService() { }

        public static AuthService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new AuthService();
                    return _instance;
                }
            }
        }

        public bool Authenticate(string user, string pass)
        {
            return user == storedUsername && pass == storedPassword;
        }
    }

}

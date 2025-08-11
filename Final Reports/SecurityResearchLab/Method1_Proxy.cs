namespace SecurityResearchLab
{
    public interface IDataAccess
    {
        void AccessSensitiveData();
    }

    public class RealDataAccess : IDataAccess
    {
        public void AccessSensitiveData()
        {
            Console.WriteLine("Accessing sensitive data...");
        }
    }

    public class SecureProxy : IDataAccess
    {
        private readonly Credential _cred;
        private readonly IDataAccess _realAccess = new RealDataAccess();

        public SecureProxy(Credential cred)
        {
            _cred = cred;
        }

        public void AccessSensitiveData()
        {
            if (AuthService.Instance.Authenticate(_cred.Username, _cred.Password))
            {
                Console.WriteLine("Login successful!");
                _realAccess.AccessSensitiveData();
            }
            else
            {
                Console.WriteLine("Unauthorized access attempt.");
            }
        }
    }
}

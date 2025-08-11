namespace HurdleTest
{
    public abstract class Thing
    {
        readonly string _name;

        public Thing(string name)
        {
            _name = name;
        }

        public abstract int Size();

        public abstract void Print();

        public string Name => _name;
    }
}

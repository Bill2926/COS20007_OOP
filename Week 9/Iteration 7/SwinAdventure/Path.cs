namespace SwinAdventure
{
    public class Path : GameObject
    {
        Location _des;

        public Path(string[] idents, string name, string desc, Location des) : base(idents, name, desc)
        {
            _des = des;
        }

        public Location Destination
        {
            get
            { return _des; }
        }
    }
}

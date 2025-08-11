namespace HurdleTest
{
    public class FileSystem
    {
        readonly List<Thing> _contents = [];

        public void Add(Thing content)
        {
            _contents.Add(content);
        }

        public void PrintContents() 
        {
            Console.WriteLine("This File System contains:");
            foreach (var thing in _contents)
            {
                thing.Print();
            }
        }
    }
}

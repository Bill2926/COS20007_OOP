namespace HurdleTest
{
    public class Folder : Thing
    {
        private readonly List<Thing> _contents = [];

        public Folder(string name) : base(name)
        { }

        public void Add(Thing toAdd)
        {
            _contents.Add(toAdd);
        }

        public override int Size()
        {
            int folderSize = 0;

            for (int i = 0; i < _contents.Count; i++)
            {
                folderSize += _contents[i].Size();
            }
            return folderSize;
        }

        public override void Print()
        {
            int folderCount = 0;
            int fileCount = 0;

            foreach (Thing toAdd in _contents)
            {
                if (toAdd is Folder folder)
                {
                    folderCount++;
                }
                else if (toAdd is File file)
                {
                    fileCount++;
                }
            }

            string folderText = folderCount.ToString();
            string fileText = fileCount.ToString();

            Console.WriteLine($"The Folder: '{Name}' contains {folderText} folder(s) and {fileText} file(s) totalling {Size()} bytes:");
            if (folderCount == 0)
            {
                foreach (var thing in _contents)
                {
                    thing.Print();
                }
            }
            else if (folderCount != 0)
                foreach (var thing in _contents)
                {
                    Console.WriteLine($"The Folder: '{thing.Name}' Size: {thing.Size()} bytes");
                }
            Console.WriteLine("");
        }

    }
}

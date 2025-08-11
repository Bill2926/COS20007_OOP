namespace HurdleTest
{
    public class File : Thing
    {
        readonly string _extension;
        readonly int _size;

        public File(string name, string extension, int size) : base(name)
        {
            _extension = extension;
            _size = size;
        }

        public override int Size()
        {
            return _size;
        }

        public override void Print()
        {
            Console.WriteLine($"File '{Name}{_extension}' Size: {_size}bytes");
        }
    }
}

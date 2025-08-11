namespace HurdleTest
{
    internal class Program
    {
        //A = 2,3,5,7,11,13,17,19,23,29
        //B = "7489"
        //=> B = 19,11,23,29

        static void Main(string[] args)
        {
            Console.WriteLine("OOP Hurdle Test");
            FileSystem fileSystem = new();

            for (int i = 0; i <= 19; i++)
            {
                if (i < 10)
                {
                    File newFile = new($"105547489-0{i}", ".txt", 100);
                    fileSystem.Add(newFile);
                }
                else
                {
                    File newFile = new($"105547489-{i}", ".txt", 100);
                    fileSystem.Add(newFile);
                }
            }

            Folder folder1 = new("Folder 1");
            fileSystem.Add(folder1);
            for (int i = 0; i <= 11; i++)
            {
                if (i < 10)
                {
                    File newFile = new($"105547489-0{i}", ".txt", 100);
                    folder1.Add(newFile);
                }
                else
                {
                    File newFile = new($"105547489-{i}", ".txt", 100);
                    folder1.Add(newFile);
                }
            }

            Folder folder2 = new("Folder 2");
            Folder folder3 = new("Folder 3");
            folder2.Add(folder3);
            fileSystem.Add(folder2);
            fileSystem.Add(folder3);

            for (int i = 0 ; i <= 23 ; i++)
            {
                if (i < 10)
                {
                    File newFile = new($"105547489-0{i}", ".txt", 100);
                    folder3.Add(newFile);
                }
                else
                {
                    File newFile = new($"105547489-{i}", ".txt", 100);
                    folder3.Add(newFile);
                }
            }

            //Adding empty folder
            for (int i = 0; i <= 29; i++)
            {
                Folder emptyFolder = new($"Empty{i}");
                fileSystem.Add(emptyFolder);
            }

            fileSystem.PrintContents();
            Console.ReadLine();
        }
    }
}

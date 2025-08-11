using System.IO;

namespace Lmao;

public class Program
{
    public static void Main(string[] args)
    {
        //Storing
        StreamWriter writer = new StreamWriter(@"C:\\Users\\Bill\\Desktop\\test.txt");
        writer.WriteLine("Trung Dinh");
        writer.WriteLine("2006");
        writer.Close();

        //Reading 
        StreamReader reader = new StreamReader(@"C:\\Users\\Bill\\Desktop\\test.txt"); 
        string name = reader.ReadLine();
        string year = reader.ReadLine();
        reader.Close();

        Console.WriteLine("Name: " + name);
        Console.WriteLine("YOB: " + year);
    }
}
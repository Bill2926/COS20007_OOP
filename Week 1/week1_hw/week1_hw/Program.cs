//1.1 OOP HOMEWORK

double[] numbers = { 2.5, -1.4, -7.2, -11.7, -13.5, 
                   -13.5, -14.9, -15.2, -14.0, -9.7, -2.6, 2.1 };
float sum = 0;
for (int i = 0; i < numbers.Length; i ++)
{
    sum += (float)numbers[i];
}
float average = (float)sum / numbers.Length;
if (average >= 10)
{
    Console.WriteLine("Multiple digits");
}
else
{
    Console.WriteLine("Single digits");
}
if (average < 0)
{
    Console.WriteLine("Average value negative");
}
int studentID = 1; 
int lastDigit = (int)Math.Abs(average) % 10;
if (lastDigit > studentID)
{
    Console.WriteLine("Larger than my last digit");
}
else if (lastDigit == studentID)
{
    Console.WriteLine("Equal to my last digit");
}
else
{
    Console.WriteLine("Smaller than my last digit");
}
Console.WriteLine("Nguyen Duc Manh");
Console.WriteLine("SWH02701");
Console.WriteLine(average);
Console.ReadKey();


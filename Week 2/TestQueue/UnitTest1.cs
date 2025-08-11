using System.Security.Cryptography.X509Certificates;
using QueueApplication;
    
namespace TestQueue;

public class Test
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void TestEnqueue()
    {
        IntegerQueue myQueue = new IntegerQueue();

        myQueue.Enqueue(12345);
        int myCount = myQueue.Count;

        Assert.That(myCount, Is.EqualTo(1));

        Assert.That(myQueue._elements.Count, Is.EqualTo(1));
        Assert.That(myQueue._elements[0], Is.EqualTo(12345));
    }

    [Test]
    public void TestDequeue()
    {
        Assert.Fail();
    }
}
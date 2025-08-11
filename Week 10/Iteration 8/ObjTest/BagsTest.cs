namespace SwinAdventure;

public class BagsTest
{
    Item _item1;
    Item _item2;
    Bags _bag1;
    Bags _bag2;

    [SetUp]
    public void Setup()
    {
        _item1 = new Item(["Ram"], "a Ram", "an NVIDIA Ram");
        _item2 = new Item(["CPU"], "a CPU", "an Intel CPU");
        _bag1 = new Bags(["Bag1"], "bag test 1", "This bag is huge");
        _bag2 = new Bags(["Bag2"], "bag test 2", "This bag is small");
        _bag1.Inventory.Put(_item1);
        _bag1.Inventory.Put(_item2);
    }

    [Test]
    public void BagLocatesItemTest()
    {
        Assert.That(_bag1.Inventory.HasItem("Ram"));
        Assert.That(_bag1.Inventory.HasItem("CPU"));
        Assert.That(_bag1.Locate("Ram"), Is.EqualTo(_item1));
        Assert.That(_bag1.Locate("CPU"), Is.EqualTo(_item2));
    }

    [Test]
    public void BagLocatesItselfTest()
    {
        Assert.That(_bag1.Locate("Bag1"), Is.EqualTo(_bag1));
        Assert.That(_bag2.Locate("Bag2"), Is.EqualTo(_bag2));
    }

    [Test]
    public void BagLocatesNothingTest()
    {
        Assert.That(_bag1.Locate("abc"), Is.Null);
        Assert.That(_bag2.Locate("xyz"), Is.Null);
    }

    [Test]
    public void BagFullDescriptionTest()
    {
        Assert.That(_bag1.FullDescription, Is.EqualTo("In the bag test 1 you can see:\na Ram (ram)\na CPU (cpu)\n"));
    }

    [Test]
    public void BagInBagTest()
    {
        Item _item3 = new Item(["Mouse"], "a Mouse", "a wireless mouse");
        _bag1.Inventory.Put(_bag2);
        _bag2.Inventory.Put(_item3);

        Assert.That(_bag1.Locate("Bag2"), Is.EqualTo(_bag2)); //Can locate bag2 in bag1's inventoyr
        Assert.That(_bag1.Locate("Ram"), Is.EqualTo(_item1)); //bag1 still can locates other items
        Assert.That(_bag1.Locate("Mouse"), Is.Null); //bag1 can't search for bag2's item
    }
}

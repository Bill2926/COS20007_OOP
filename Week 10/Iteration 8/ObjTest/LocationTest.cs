namespace SwinAdventure;

public class LocationTest
{
    Location _location;
    Player _player;

    [SetUp]
    public void Setup()
    {
        _location = new(["duytan", "location"], "80 duy tan", "Innovation Space");
        _player = new("Manh", "A student at Swinburne");
    }

    [Test]
    public void LocationLocateItself()
    {
        Assert.IsTrue(_location.AreYou("duytan"));
    }

    [Test]
    public void LocationLocateItem()
    {
        Item item = new(["usb"], "an usb", "this is an usb");
        _location.Inventory.Put(item);
        Assert.IsTrue(_location.Locate("usb").AreYou("usb"));
    }

    [Test]
    public void PlayerInLocation()
    {
        _player.Location = _location;
        string expect = _location.FullDescription;
        Assert.That(_player.Location.FullDescription, Is.EqualTo(expect));
    }

    [Test]
    public void PlayerLocateLocation()
    {
        _player.Location = _location;
        Assert.IsTrue(_player.Locate("duytan").AreYou("duytan"));
    }

    [Test]
    public void PlayerLocateItem()
    {
        Item item = new(["usb"], "an usb", "magical usb can stores 1TB");
        _location.Inventory.Put(item);
        _player.Location = _location;
        Assert.IsTrue(_player.Locate("usb").AreYou("usb"));
    }
} 

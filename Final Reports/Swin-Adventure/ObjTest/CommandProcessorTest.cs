using SwinAdventure;
using Path = SwinAdventure.Path;

namespace ObjTest;

public class CommandProcessorTest
{
    Player player;
    CommandProcessor cp;
    Item itm;
    Bags bag;
    Location duytan;
    Location duongkhue;
    Path north;
    Path south;

    [SetUp]
    public void Setup()
    {
        player = new("Duy", "Software Engineering");
        itm = new(["hdmi"], "HDMI cord", "can connect to large screen");
        bag = new(["bag"], "a bag", "this bag is made by leather");
        duytan = new(["duytan"], "duytan", "Innovation Center of Swinburne");
        duongkhue = new(["duongkhue"], "duongkhue", "Global Citizen Education");
        north = new(["north"], "north move", "Duy Tan street", duytan);
        south = new(["south"], "south move", "Cau Giay street", duongkhue);

        cp = new CommandProcessor();
    }

    [Test]
    public void LookCommand()
    {
        bag.Inventory.Put(itm);
        player.Inventory.Put(bag);
        LookCommand lookCommand = new();

        string input1 = cp.Execute(player, ["look", "at", "me"]);
        string input2 = cp.Execute(player, ["look", "at", "hdmi", "in", "bag"]);
        string expect1 = lookCommand.Execute(player, ["look", "at", "me"]);
        string expect2 = lookCommand.Execute(player, ["look", "at", "hdmi", "in", "bag"]);

        Assert.That(input1, Is.EqualTo(expect1));
        Assert.That(input2, Is.EqualTo(expect2));
    }

    [Test]
    public void MoveCommand()
    {
        duytan.AddPath(south);
        duongkhue.AddPath(north);
        player.Location = duytan;
        MoveCommand moveCommand = new();
        string input1 = cp.Execute(player, ["move", "south"]);
        Assert.That(player.Location.AreYou("duongkhue"), Is.True);
        string input2 = cp.Execute(player, ["move", "north"]);
        Assert.That(player.Location.AreYou("duytan"), Is.True);
    }

    [Test]
    public void CommandNotFind()
    {
        string input = cp.Execute(player, ["not", "a", "command"]);
        Assert.That(input, Is.EqualTo("I can not find that command!"));
    }

    [Test]
    public void CommandWithNoInput()
    {
        string input = cp.Execute(player, []);
        Assert.That(input, Is.EqualTo("Please enter a command."));
    }
}

namespace SwinAdventure;

public class MoveCommandTest
{
    Location start;
    Location end;
    Path north;
    Player player;
    MoveCommand moveCommand;

    [SetUp]
    public void Setup()
    {
        start = new Location(["duytan"], "80 Duy Tan", "Innovation Space");
        end = new Location(["vovinam"], "3.1 VOV", "Martial Art");
        north = new Path(["north"], "north move", "go through the north forrest", end);
        player = new Player("TestPlayer", "A test player");
        player.Location = start;
        moveCommand = new MoveCommand();
    }

    [Test]
    public void TestValidMove()
    {
        start.AddPath(north);
        string result = moveCommand.Execute(player, ["move", "north"]);
        string expect = $"You move north to {end.Name}";
        Assert.That(result, Is.EqualTo(expect));
    }

    [Test]
    public void TestInvalidMove()
    {
        string result = moveCommand.Execute(player, ["move", "south"]);
        string expect = "There is no path in that direction.";
        Assert.That(result, Is.EqualTo(expect));
    }

    [Test]
    public void TestNonPath()
    {
        Item item = new Item(["north"], "a test item", "this is a test item");
        start.Inventory.Put(item);
        string result = moveCommand.Execute(player, ["move", "north"]);
        string expect = "That doesn't seem like a valid path.";
        Assert.That(result, Is.EqualTo(expect));
    }

    [Test]
    public void TestInvalidCommand()
    {
        string result = moveCommand.Execute(player, ["move"]);
        string expect = "I don't know how to move like that";
        Assert.That(result, Is.EqualTo(expect));
    }
}

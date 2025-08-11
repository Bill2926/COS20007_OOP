using System.Numerics;

namespace SwinAdventure;

public class LookCommandTest
{
    Player me;
    Item gem;
    Bags bag;
    Command look;

    [SetUp]
    public void Setup()
    {
        me = new Player("me", "the main character of the game");
        gem = new Item(["gem"], "a bright red stone", "it sparkles in the light");
        bag = new Bags(["bag"], "a small bag", "it has a zipper");
        look = new LookCommand();

        me.Inventory.Put(gem);
        me.Inventory.Put(bag);
        bag.Inventory.Put(gem);
    }

    [Test]
    public void TestLookAtMe()
    {
        string prompt = look.Execute(me, ["look", "at", "inventory"]);
        string expected = me.FullDescription;
        Assert.That(prompt, Is.EqualTo(expected));
    }

    [Test]
    public void TestLookAtGem()
    {
        string prompt = look.Execute(me, ["look", "at", "gem"]);
        string expected = gem.FullDescription;
        Assert.That(prompt, Is.EqualTo(expected));
    }

    [Test]
    public void TestLookAtUnk()
    {
        string prompt = look.Execute(me, ["look", "at", "unk"]);
        string expected = "I cannot find the unk";
        Assert.That(prompt, Is.EqualTo(expected));
    }

    [Test]
    public void TestLookAtGemInMe()
    {
        string prompt = look.Execute(me, ["look", "at", "gem", "in", "inventory"]);
        string expected = gem.FullDescription;
        Assert.That(prompt, Is.EqualTo(expected));
    }

    [Test]
    public void TestLookAtGemInBag()
    {
        Assert.That(me.Locate("bag"), Is.EqualTo(bag)); //test that bag is in player's inventory
        string prompt = look.Execute(me, ["look", "at", "gem", "in", "bag"]);
        string expected = gem.FullDescription;
        Assert.That(prompt, Is.EqualTo(expected));
    }

    [Test]
    public void TestLookAtGemInNoBag()
    {
        me.Inventory.Take("bag"); //remove bag from player's inventory
        string prompt = look.Execute(me, ["look", "at", "gem", "in", "bag"]);
        string expected = "I cannot find the bag";
        Assert.That(prompt, Is.EqualTo(expected));
    }

    [Test]
    public void TestLookAtNoGemInBag()
    {
        bag.Inventory.Take("gem"); //remove gem from player's inventory
        string prompt = look.Execute(me, ["look", "at", "gem", "in", "bag"]);
        string expected = "I cannot find the gem";
        Assert.That(prompt, Is.EqualTo(expected));
    }

    [Test]
    public void TestInvalidLook()
    {
        string prompt = look.Execute(me, ["look", "at", "gem", "not in", "bag"]);
        string expected = "What do you want to look in?";
        Assert.That(prompt, Is.EqualTo(expected));
    }

    [Test]
    public void TestInvalidLook2()
    {
        string prompt = look.Execute(me, ["kool", "at", "gem", "in", "bag"]);
        string expected = "Error in look input";
        Assert.That(prompt, Is.EqualTo(expected));
    }
}

using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace SwinAdventure
{
    public class ItemTest
    {
        Item laptop;

        [SetUp]
        public void Setup() 
        {
            laptop = new Item(new string[] { "laptop" }, "a laptop", "This is a Swinburne laptop");
        }

        [Test]
        public void TestItemIdentifiable()
        { 
            var areyou2 = laptop.AreYou("laptop");
            Assert.IsTrue(areyou2);
        }

        [Test]
        public void TestShortDescription()
        {
            Assert.That(laptop.ShortDescription, Is.EqualTo("a laptop (laptop)")); 
        }

        [Test]
        public void TestFullDescription()
        {
            Assert.That(laptop.FullDescription, Is.EqualTo("This is a Swinburne laptop"));
        }

        [Test]
        public void PrivilegeEscalationTest()
        {
            var firstID = new string[] { "sword", "blade" };
            var item = new Item(firstID, "Sword", "A sharp blade");
            item.PrivilegeEscalation("7489");

            Assert.That(item.FirstId, Is.EqualTo("7489"));
        }

    }
}
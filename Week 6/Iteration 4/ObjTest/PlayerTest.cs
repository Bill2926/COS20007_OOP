namespace SwinAdventure
{
    public class PlayerTest
    {
        Item _item;
        Inventory _inventory;
        Player _swinburneStudent;

        [SetUp]
        public void Setup()
        {
            _item = new(new String[] { "sword" }, "diamond sword", "can destroy enemies");
            _inventory = new Inventory();
            _swinburneStudent = new Player("Duc Manh", "OOP Student");
        }

        [Test]
        public void PlayerIsIdentifiableTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_swinburneStudent.AreYou("me"), Is.True);
                Assert.That(_item.AreYou("sword"), Is.True);
            });
        }

        [Test]
        public void PlayerLocatesItemsTest()
        {
            _swinburneStudent.Inventory.Put(_item);
            Assert.That(_swinburneStudent.Locate(_item.FirstId), Is.EqualTo(_item));
        }

        [Test]
        public void PlayerLocatesItselfTesr()
        {
            Assert.That(_swinburneStudent, Is.EqualTo(_swinburneStudent.Locate("me")));
            Assert.That(_swinburneStudent, Is.EqualTo(_swinburneStudent.Locate("inventory")));
        }

        [Test]
        public void PlayerLocatesNothingTest()
        {
            Assert.That(_swinburneStudent.Locate("shield"), Is.EqualTo(null));
        }

        [Test]
        public void PlayerFullDescriptionTest()
        {
            string expectedOutput = "Duc Manh, Duc Manh (me).You are carrying: diamond sword (sword)\n";
            _swinburneStudent.Inventory.Put(_item);
            Assert.That(expectedOutput, Is.EqualTo(_swinburneStudent.FullDescription));
        }
    }
}

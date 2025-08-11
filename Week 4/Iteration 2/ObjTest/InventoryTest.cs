namespace SwinAdventure
{
    public class InventoryTest
    {
        Item _item;
        Inventory _inventory;

        [SetUp]
        public void Setup()
        {
            _item = new(new String[] { "HDMI" }, "HDMI cord", "can connect to large screen");
            _inventory = new Inventory();
        }

        [Test]
        public void FindItemTest()
        {
            _inventory.Put(_item);
            Assert.That(_inventory.HasItem(_item.FirstId), Is.True);
        }

        [Test]
        public void NoItemFindTest()
        {
            Assert.That(_inventory.HasItem("Mouse"), Is.False);
        }

        [Test]
        public void FetchItemTest()
        {
            _inventory.Put(_item);
            Assert.That(_inventory.Fetch(_item.FirstId), Is.EqualTo(_item));
        }

        [Test]
        public void TakeItemTest()
        {
            _inventory.Put(_item);
            _inventory.Take(_item.FirstId);
            Assert.That(_inventory.HasItem(_item.FirstId), Is.False);
        }

        [Test]
        public void TestItemList()
        {
            _inventory.Put(_item);
            Assert.That(_inventory.ItemList, Is.EqualTo("HDMI cord (hdmi)\n"));
        }
    }
}

namespace SwinAdventure
{
    public class ObjTest
    {
        private IdenObj _testObject;
        private string _testString;
        private string[] _testArray;
        private IdenObj _testObject_emp;
        private string _testString_emp;
        private string[] _testArray_emp;

        [SetUp]
        public void Setup()
        {
            _testString = "Duc";
            _testArray = new string[] {"0209", "Duc", "Manh" };
            _testObject = new IdenObj(_testArray);
            _testObject.AddIdentifier(_testString);
            _testString_emp = "";
            _testArray_emp = new string[] { };
            _testObject_emp = new IdenObj(_testArray_emp);
            _testObject_emp.AddIdentifier(_testString_emp);
        }

        [Test]
        public void TestAreYou()
        {
            Assert.IsTrue(_testObject.AreYou(_testString));
        }

        [Test]
        public void TestNotAreYou()
        {
            Assert.IsFalse(_testObject.AreYou("Trung"));
        }

        [Test]
        public void Insensitive()
        {
            Assert.IsTrue(_testObject.AreYou("DUC"));
        }

        [Test]
        public void TestFirstId()
        {
            Assert.That(_testObject.FirstId, Is.EqualTo("0209"));
            Assert.That(_testObject.FirstId, Is.Not.EqualTo("Duc"));
        }

        [Test]
        public void TestFirstIdWithNoIds()
        {
            Assert.That(_testObject_emp.FirstId, Is.EqualTo(""));
        }

        [Test]
        public void TestAddID()
        {
            _testObject.AddIdentifier("Trung");
            _testObject.AddIdentifier("Ninh");
            Assert.IsTrue(_testObject.AreYou("Trung"));
            Assert.IsTrue(_testObject.AreYou("Ninh"));

        }

        [Test]
        public void TestPrivilegeEscalation()
        {
            _testObject.PrivilegeEscalation("7489");
            Assert.That(_testObject.FirstId, Is.EqualTo("7489"));
        }
    }
}
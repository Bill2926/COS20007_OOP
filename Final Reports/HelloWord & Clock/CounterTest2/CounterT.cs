namespace CounterTask
{
    public class CounterT
    {
        Counter _testCounter;
        private int i;

        [SetUp]
        public void Setup()
        {
            _testCounter = new Counter("test", 0);
        }

        [Test]
        public void CounterInitial0()
        {
            Assert.That(_testCounter.Ticks, Is.EqualTo(0));
        }

        [Test]
        public void IncrementTest()
        {
            _testCounter.Increment();
            Assert.That(_testCounter.Ticks, Is.EqualTo(1));
        }

        [Test]
        public void MultipleIncrementTest()
        {
            for (i = 0; i < 10; i ++)
            {
                _testCounter.Increment();
            }
            Assert.That(_testCounter.Ticks, Is.EqualTo(10));
        }

        [Test]
        public void ResettingTest()
        {
            for (i = 0; i < 10; i++)
            {
                _testCounter.Increment();
            }
            _testCounter.Reset();
            Assert.That(_testCounter.Ticks, Is.EqualTo(0));
        }
    }
}
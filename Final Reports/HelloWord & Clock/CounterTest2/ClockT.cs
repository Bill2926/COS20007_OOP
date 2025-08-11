namespace CounterTask
{
    public class ClockT
    {
        Clock _testClock;
        string _startTime;

        [SetUp]
        public void Setup()
        {
            _testClock = new Clock();
            _startTime = "12:00:00 AM";
        }

        [Test]
        public void InitialTime()
        {
            Assert.That(_startTime, Is.EqualTo(_testClock.ClockDisplay()));
        }

        [Test]
        public void TickOnce()
        {
            _testClock.TimeIncrease();
            Assert.That(_testClock.ClockDisplay(), Is.EqualTo("12:00:01 AM"));
        }

        [Test]
        public void TimeReset()
        {
            for (int i = 0; i < 86400; i ++)
            {
                _testClock.TimeIncrease();
            }
            Assert.That(_testClock.ClockDisplay(), Is.EqualTo("12:00:00 AM"));
        }

        [TestCase (3600, "01:00:00 AM")] //1 hour
        [TestCase (43200, "12:00:00 PM")] //12 hours
        [TestCase(86400, "12:00:00 AM")] //24 hours
        [TestCase(86400*2, "12:00:00 AM")] //48 hours
        public void ClockRun(int second, string expected)
        {
            for (int i = 0; i < second; i++)
            {
                _testClock.TimeIncrease();
            }
            Assert.That(_testClock.ClockDisplay(), Is.EqualTo(expected));
        }
    }
}

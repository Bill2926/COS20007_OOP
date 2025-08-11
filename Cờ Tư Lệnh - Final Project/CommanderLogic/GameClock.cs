namespace CommanderLogic
{
    public class GameClock
    {
        private int totalSeconds;
        private Timer timer;
        private readonly Action<string> onTick;
        public event Action<Player> TimeOut;
        private bool hasStarted = false;

        public GameClock(Player side, int max, Action<string> onTick)
        {
            Side = side;
            totalSeconds = max * 60;
            this.onTick = onTick;
        }

        public void Start()
        {
            hasStarted = true;
            timer = new Timer(TimerCallback, null, 0, 1000);
        }

        private void TimerCallback(object state)
        {
            if (hasStarted && totalSeconds > 0)
            {
                totalSeconds--;
                string time = GetTime();
                onTick?.Invoke(time);
            }
            else
            {
                timer?.Change(Timeout.Infinite, Timeout.Infinite); // Stop timer
                System.Diagnostics.Debug.WriteLine($"Out of Time: {Side}");
                TimeOut?.Invoke(Side); 
            }
        }

        public void Stop()
        {
            hasStarted = false;
            timer?.Dispose();
        }

        public string GetTime()
        {
            int mins = totalSeconds / 60;
            int secs = totalSeconds % 60;
            return $"{mins:D2}:{secs:D2}";
        }

        public Player Side { get; set; }
    }
}

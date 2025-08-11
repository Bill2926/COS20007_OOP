namespace CommanderLogic
{
    public class GameResult
    {
        public GameResult(Player winner, EndReason reason)
        {
            Winner = winner;
            Reason = reason;
        }

        public static GameResult Win(Player winner, EndReason reason)
        {
            return new GameResult(winner, reason);
        }

        public Player Winner { get; }

        public EndReason Reason { get; }
    }

    public enum EndReason
    {
        LostCommander,
        OutOfTime
    }
}

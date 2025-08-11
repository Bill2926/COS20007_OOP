namespace CommanderLogic
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }

        public abstract Position From { get; }

        public abstract Position To { get; }

        public abstract void MoveExecute(Board board);

        public abstract string Log(Board board);
    }
    public enum MoveType
    {
        Normal,
        HideHQ,
        UnHideHQ,
        //other special moves later on
    }
}

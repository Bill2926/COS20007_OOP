namespace CommanderLogic.Moves
{
    public class HideHQ : Move
    {
        public HideHQ(Position from, Position to)
        {
            From = from;
            To = to;
        }

        public override void MoveExecute(Board board)
        {
            Commander commander = board[From] as Commander;
            HQ hq = board[To] as HQ;
            hq.HideCommander(commander, From);
            board[From] = null;
        }

        public override MoveType Type => MoveType.HideHQ;

        public override Position From { get; }

        public override Position To { get; }

        public override string Log(Board board)
        {
            GamePieces piece = board[From];
            string side = piece.Side switch
            {
                Player.Red => "R",
                Player.Blue => "B",
                _ => "?"
            };
            return $"{piece.Name} ({side}): {From.PosToString()} hide {To.PosToString()}";
        }
    }
}

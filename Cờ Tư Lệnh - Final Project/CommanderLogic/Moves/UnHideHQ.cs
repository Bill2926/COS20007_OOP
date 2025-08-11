namespace CommanderLogic.Moves
{
    public class UnHideHQ : Move
    {
        public UnHideHQ(Position from, Position to)
        {
            From = from;
            To = to;
        }

        public override void MoveExecute(Board board)
        {
            HQ hq = board[From] as HQ;
            int row = hq.CommanderRow;
            int col = hq.CommanderCol;
            Commander commander = hq.commanderSlot[0];
            board[row, col] = commander;
            hq.UnHideCommander();
        }

        public override MoveType Type => MoveType.UnHideHQ;

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
            return $"{piece.Name} ({side}): {From.PosToString()} unhide {To.PosToString()}";
        }
    }
}

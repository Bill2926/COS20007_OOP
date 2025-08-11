namespace CommanderLogic.Moves
{
    public class NormalMove : Move
    {
        public NormalMove(Position from, Position to)
        {
            From = from;
            To = to;
        }

        public override void MoveExecute(Board board)
        {
            GamePieces piece = board[From];
            CapturedPiece = board[To];
            board[To] = piece;
            board[From] = null; // Clear the original position
            piece.HasMovedYet = true; // Mark the piece as moved
        }

        public override MoveType Type => MoveType.Normal;

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
            return $"{piece.Name} ({side}): {From.PosToString()} to {To.PosToString()}";
        }

        public GamePieces CapturedPiece { get; private set; }
    }
}

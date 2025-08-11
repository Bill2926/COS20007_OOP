using CommanderLogic.Moves;

namespace CommanderLogic
{
    //Move and capture hori and verti 1 pos
    //Like other heavy pieces

    public class AAG : GamePieces
    {
        public AAG(Player side)
        {
            Side = side;
        }

        public override PieceName Name => PieceName.AAG;

        public override Player Side { get; }

        public override int Point => 10;

        public override GamePieces CopyPiece()
        {
            AAG copy = new AAG(Side);
            copy.HasMovedYet = this.HasMovedYet;
            return copy;
        }

        private static readonly Direction[] dirs =
        [
            Direction.Up,
            Direction.Down,
            Direction.Left,
            Direction.Right,
        ];

        private IEnumerable<Move> AAGMove(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;
                Move To = new NormalMove(from, to);

                if (!Board.InsideBoard(to) || Position.CanCrossRiver(To))
                    continue;

                GamePieces target = board[to];
                if (board.EmptyPosition(to))
                {
                    yield return new NormalMove(from, to);
                }
                else if (target.Side != Side)
                {
                    if (CanCapture(to, board))
                        yield return new NormalMove(from, to);
                }
                else
                    continue;
            }
        }

        private bool CanCapture(Position to, Board board)
        {
            if (!Board.InsideBoard(to) || board.EmptyPosition(to))
            {
                return false;
            }
            GamePieces target = board[to];
            return target.Side != Side; // Can capture if the piece is not on the same side
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return AAGMove(from, board);
        }
    }
}

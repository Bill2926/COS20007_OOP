using CommanderLogic.Moves;

namespace CommanderLogic
{
    //move 1 pos in 4 directions
    //capture diagonally 1 pos

    public class Militia : GamePieces
    {
        public Militia(Player side)
        {
            Side = side;
        }

        public override PieceName Name => PieceName.Militia;

        public override Player Side { get; }

        public override int Point => 10;

        public override GamePieces CopyPiece()
        {
            Militia copy = new Militia(Side);
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

        private static readonly Direction[] captures =
        [
            Direction.UpLeft,
            Direction.DownLeft,
            Direction.DownRight,
            Direction.UpRight,
        ];

        private bool CanCapture(Position to, Board board)
        {
            if (!Board.InsideBoard(to) || board.EmptyPosition(to))
            {
                return false;
            }
            GamePieces target = board[to];
            return target.Side != Side; // Can capture if the piece is not on the same side
        }

        private IEnumerable<Move> MilitiaMove(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;
                if (!Board.InsideBoard(to) || Position.OceanPosition(to))
                {
                    break;
                }

                if (CanCapture(to, board) || board.EmptyPosition(to))
                {
                    yield return new NormalMove(from, to);
                }
            }

            foreach (Direction capture in captures)
            {
                Position diagonal = from + capture;
                if (CanCapture(diagonal, board))
                {
                    yield return new NormalMove(from, diagonal);
                }
                else
                    continue;
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MilitiaMove(from, board);
        }
    }
}

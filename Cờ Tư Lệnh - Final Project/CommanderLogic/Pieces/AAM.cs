using CommanderLogic.Moves;

namespace CommanderLogic
{
    public class AAM : GamePieces
    {
        //Move hori and verti 2 pos max
        //Capture diagonal 1 pos max
        //Can't pass the ocean (only bridge) and other pieces

        public AAM(Player side)
        {
            Side = side;
        }

        public override PieceName Name => PieceName.AAM;

        public override Player Side { get; }

        public override int Point => 20;

        public override GamePieces CopyPiece()
        {
            AAM copy = new AAM(Side);
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
            Direction.UpRight,
            Direction.DownRight,
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

        private IEnumerable<Move> AAMMove(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                for (int i = 1; i <= 2; i++)
                {
                    Position to = from + i * dir;
                    Move To = new NormalMove(from, to);
                    if (!Board.InsideBoard(to) || Position.CanCrossRiver(To))
                        break;

                    GamePieces target = board[to];
                    if (board.EmptyPosition(to))
                    {
                        yield return To;
                    }
                    else if (target.Side != Side)
                    {
                        for (int x = i; x <= 2; x++)
                        {
                            Position to2 = from + x * dir;
                            if (CanCapture(to2, board))
                            {
                                yield return new NormalMove(from, to2);
                            }
                            continue;
                        }
                        break;
                    }
                    else break;
                }
            }
        }

        private IEnumerable<Move> AAMCaptureDiagonally(Position from, Board board)
        {
            foreach (Direction capture in captures)
            {
                Position to = from + capture;
                Move To = new NormalMove(from, to);
                if (!Board.InsideBoard(to) || Position.CanCrossRiver(To))
                    break;
                GamePieces target = board[to];
                if (target != null)
                {
                    if (target.Side != Side && CanCapture(to, board))
                    {
                        yield return To;
                    }
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return AAMMove(from, board)
                .Concat(AAMCaptureDiagonally(from, board));
        }
    }
}

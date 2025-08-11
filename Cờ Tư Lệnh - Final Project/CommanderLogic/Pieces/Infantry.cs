using CommanderLogic.Moves;

namespace CommanderLogic.Pieces
{
    //Infantry can captures pieces that on their available move
    //verti and hori 1 move max
    //Can pass the river

    public class Infantry : GamePieces
    {

        public Infantry(Player side)
        {
            Side = side;

            if (side == Player.Red)
            {
                forward = Direction.Up;
                backward = Direction.Down;
            }
            else if (side == Player.Blue)
            {
                forward = Direction.Down;
                backward = Direction.Up;
            }
        }

        public override PieceName Name => PieceName.Infantry;

        public override Player Side { get; }

        public override int Point => 10;

        public override GamePieces CopyPiece()
        {
            Infantry copy = new(Side)
            {
                HasMovedYet = HasMovedYet
            };
            return copy;
        }

        private static readonly Direction[] dirs =
        [
            Direction.Left,
            Direction.Right,
        ];

        private readonly Direction forward;

        private readonly Direction backward;

        private IEnumerable<Move> MoveForward(Position from, Board board)
        {
            Position to = from + forward;
            if (Board.InsideBoard(to) && board.EmptyPosition(to) && !Position.OceanPosition(to) || CanCapture(to, board))
            {
                Move To = new NormalMove(from, to);
                //to.CrossRiver(To);
                yield return To;
            }
        }

        private IEnumerable<Move> MoveBackward(Position from, Board board)
        {
            Position to = from + backward;
            if (Board.InsideBoard(to) && board.EmptyPosition(to) && !Position.OceanPosition(to) || CanCapture(to, board))
            {
                yield return new NormalMove(from, to);
            }
        }

        private IEnumerable<Move> MoveHorizontally(Position from, Board board)
        {
            foreach (var dir in dirs)
            {
                Position to = from + dir;
                if (Board.InsideBoard(to) && board.EmptyPosition(to) && !Position.OceanPosition(to) || CanCapture(to, board))
                {
                    yield return new NormalMove(from, to);
                }
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
            return MoveHorizontally(from, board)
                .Concat(MoveForward(from, board))
                .Concat(MoveBackward(from, board));
        }
    }
}

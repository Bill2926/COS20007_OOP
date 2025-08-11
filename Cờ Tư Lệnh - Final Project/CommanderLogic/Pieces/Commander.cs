using CommanderLogic.Moves;

namespace CommanderLogic
{
    //Can move hori, vert 10 squares max (can move backward)
    //Only capture hori, vert 1 square
    //Can't get over other pieces
    //Can't facing opponent's Commander

    public class Commander : GamePieces
    {

        public Commander(Player side)
        {
            Side = side;
        }

        public override PieceName Name => PieceName.Commander;

        public override Player Side { get; }

        public override int Point => 100;

        public override GamePieces CopyPiece()
        {
            Commander copy = new(Side)
            {
                HasMovedYet = this.HasMovedYet
            };
            return copy;
        }

        private static readonly Direction[] dirs =
        [
            Direction.Up,
            Direction.Down,
            Direction.Left,
            Direction.Right,
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

        private IEnumerable<Move> CommanderMove(Position from, Board board)
        {
            foreach (var dir in dirs)
            {
                for (int i = 1; i <= 10; i++)
                {
                    Position to = from + i * dir;
                    Move To = new NormalMove(from, to);
                    if (!Board.InsideBoard(to) || Position.OceanPosition(to))
                        break;
                    if (board.EmptyPosition(to))
                        yield return To;
                    else
                        break;
                }

                Position cap = from + dir;
                if (CanCapture(cap, board))
                {
                    yield return new NormalMove(from, cap);
                }
            }
        }

        private IEnumerable<Move> HideHQ(Position from, Board board)
        {
           foreach (var dir in dirs)
            {
                for (int i = 1; i <= 10; i++)
                {
                    Position to = from + i * dir;
                    if (!Board.InsideBoard(to) || Position.OceanPosition(to) || !IsHQ(to, board))
                        break;
                    yield return new HideHQ(from, to);
                }
            }
        }

        private bool IsHQ(Position to, Board board)
        {
            GamePieces hq = board[to];
            if (hq is null)
                return false;
            if (hq.Name != PieceName.HQ)
            {
                return false;
            }
            else 
                return true;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return CommanderMove(from, board)
                .Concat(HideHQ(from, board));
        }
    }
}

using System.Text.RegularExpressions;
using CommanderLogic.Moves;

namespace CommanderLogic
{
    //move and capture All direction (max 4, go over other pieces)
    //capture and replace or go back (bombing)

    public class AF : GamePieces
    {
        public AF(Player side)
        {
            Side = side;
        }

        public override PieceName Name => PieceName.AF;

        public override Player Side { get; }

        public override int Point => 40;

        public override GamePieces CopyPiece()
        {
            AF copy = new AF(Side);
            copy.HasMovedYet = this.HasMovedYet;
            return copy;
        }

        private static readonly Direction[] dirs =
        [
            Direction.Up,
            Direction.Down,
            Direction.Left,
            Direction.Right,
            Direction.UpLeft,
            Direction.DownLeft,
            Direction.UpRight,
            Direction.DownRight,
        ];

        private IEnumerable<Move> AFMove(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                for (int i = 1; i <= 4; i++)
                {
                    Position to = from + i * dir;

                    if (!Board.InsideBoard(to))
                        break;

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
                    else // same-side piece: fly over
                    {
                        continue;
                    }
                }
            }
        }

        private bool CanCapture(Position to, Board board)
        {
            if (Board.InsideBoard(to) && board.EmptyPosition(to))
            {
                return false;
            }
            GamePieces target = board[to];
            return target.Side != Side;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return AFMove(from, board);
        }
    }
}

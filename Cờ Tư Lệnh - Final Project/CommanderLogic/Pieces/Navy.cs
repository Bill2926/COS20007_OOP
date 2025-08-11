using System.Text.RegularExpressions;
using CommanderLogic.Moves;

namespace CommanderLogic
{
    public class Navy : GamePieces
    {
        //Must stay on water, can move 4 pos in any direction
        //Capture on water must go there
        //Capture on land can stay

        public Navy(Player side)
        {
            Side = side;
        }

        public override PieceName Name => PieceName.Navy;

        public override Player Side { get; }

        public override int Point => 80;

        public override GamePieces CopyPiece()
        {
            Navy copy = new Navy(Side);
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

        public static bool NavyRange(Position to)
        {
            int[] NavyCol = { 0, 1, 2 };
            int[] RiverCol = { 3, 4 };
            return (NavyCol.Contains(to.Column)) ||
                   (RiverCol.Contains(to.Column) && to.Row == 5) ||
                   (RiverCol.Contains(to.Column) && to.Row == 6);

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

        private IEnumerable<Move> NavyMove(Position from, Board board)
        {
            foreach (var dir in dirs)
            {
                for (int i = 1; i <= 4; i++)
                {
                    Position to = from + i * dir;
                    Move To = new NormalMove(from, to);
                    if (!Board.InsideBoard(to) || !NavyRange(to)) 
                        break;

                    GamePieces target = board[to];
                    if (board.EmptyPosition(to))
                    {
                        yield return To;
                    }
                    else if (target.Side != Side)
                    {
                        for (int x = i; x <= 4; x++)
                        {
                            Position to2 = from + x * dir;
                            if (CanCapture(to2, board))
                            {
                                yield return new NormalMove(from, to2);
                            }
                        }
                    }
                    else
                        break;
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return NavyMove(from, board);
        }
    }
}

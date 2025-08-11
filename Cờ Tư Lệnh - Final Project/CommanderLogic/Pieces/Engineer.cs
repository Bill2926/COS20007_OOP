using CommanderLogic.Moves;
using System.Text.RegularExpressions;

namespace CommanderLogic
{
    //moves in 4 directions
    //can carry heavy pieces (tank, artillery, aam)
    //can capture horizontally and vertically 1 square

    public class Engineer : GamePieces
    {
        GamePieces heavyPiece = null;

        public Engineer(Player side)
        {
            Side = side;
        }

        public override PieceName Name => PieceName.Engineer;

        public override Player Side { get; }

        public override int Point => 10;

        public override GamePieces CopyPiece()
        {
            Engineer copy = new Engineer(Side);
            copy.HasMovedYet = this.HasMovedYet;
            return copy;
        }

        private static readonly PieceName[] heavyPieces =
        [
            PieceName.Tank,
            PieceName.Artillery,
            PieceName.AAM
        ];

        private static readonly Direction[] dirs =
        [
            Direction.Up,
            Direction.Down,
            Direction.Left,
            Direction.Right,
        ];

        private bool CanCapture(Position to, Board board)
        {
            if (Board.InsideBoard(to) && board.EmptyPosition(to))
            {
                return false;
            }
            GamePieces target = board[to];
            return target.Side != Side;
        }

        private IEnumerable<Move> EngineerMove(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;
                Move To = new NormalMove(from, to);

                if (!Board.InsideBoard(to) || Position.OceanPosition(to))
                    break;

                if (board.EmptyPosition(to))
                {
                    yield return To;
                }

                if (CanCapture(to, board))
                {
                    yield return To;
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return EngineerMove(from, board);
        }
    }
}

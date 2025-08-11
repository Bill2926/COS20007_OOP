using System.Text.RegularExpressions;
using CommanderLogic.Moves;

namespace CommanderLogic
{
    //move 2 pos in 4 directions
    // can capture horizontally and vertically 2 square

    public class Tank : GamePieces
    {
        public Tank(Player side)
        {
            Side = side;
        }

        public override PieceName Name => PieceName.Tank;

        public override Player Side { get; }

        public override int Point => 20;

        public override GamePieces CopyPiece()
        {
            Tank copy = new Tank(Side);
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

        private bool CanCapture(Position to, Board board)
        {
            if (!Board.InsideBoard(to) || board.EmptyPosition(to))
            {
                return false;
            }
            GamePieces target = board[to];
            return target.Side != Side; // Can capture if the piece is not on the same side
        }

        private IEnumerable<Move> TankMove(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                for (int i = 1; i <= 2; i++)
                {
                    Position to = from + i * dir;
                    Move To = new NormalMove(from, to);

                    if (!Board.InsideBoard(to) || Position.OceanPosition(to) || (Position.CanCrossRiver(To) && !Position.CrossShallowRiver(To)))
                        break;
                    if (board.EmptyPosition(to) || Position.CrossShallowRiver(To))
                        yield return To;
                    if (CanCapture(to, board))
                    {
                        yield return To;
                    }
                    else
                        break; 
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return TankMove(from, board);
        }
    }
}

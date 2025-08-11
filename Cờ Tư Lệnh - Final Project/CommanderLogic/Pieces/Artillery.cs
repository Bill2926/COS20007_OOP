using CommanderLogic.Moves;

namespace CommanderLogic
{
    public class Artillery : GamePieces
    {
        //Move hori, verti, and diagonal 3 pos max
        //Cant pass the ocean or other pieces (only bridge)
        //Capture like the move
        //Can capture over other pieces

        public Artillery(Player side)
        {
            Side = side;
        }

        public override PieceName Name => PieceName.Artillery;

        public override Player Side { get; }

        public override int Point => 30;

        public override GamePieces CopyPiece()
        {
            Artillery copy = new Artillery(Side);
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

        private IEnumerable<Move> ArtilleryMove(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                for (int i = 1; i <= 3; i++)
                {
                    Position to = from + i * dir;
                    Move To = new NormalMove(from, to);

                    if (!Board.InsideBoard(to) || Position.CanCrossRiver(To) || Position.OceanPosition(to))
                        break;

                    GamePieces target = board[to];

                    if (board.EmptyPosition(to))
                    {
                        yield return To;
                    }
                    else if (target.Side != Side)
                    {
                        for (int x = i; x <= 3; x++)
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
            return ArtilleryMove(from, board);
        }
    }
}

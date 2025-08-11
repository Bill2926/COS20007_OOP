namespace CommanderLogic;

public enum PieceName
{
    Commander,
    Infantry,
    Tank,
    Militia,
    Engineer,
    Artillery,
    AAG,
    AAM,
    AF,
    Navy,
    HQ
}

public abstract class GamePieces
{
    public abstract PieceName Name { get; }

    public abstract Player Side { get; }

    public abstract int Point { get; }

    public bool HasMovedYet { get; set; } = false; // Track if the piece has moved

    public abstract GamePieces CopyPiece();

    public abstract IEnumerable<Move> GetMoves(Position from, Board board);

    public GamePieces Clone()
    {
        return CopyPiece();
    }

    protected IEnumerable<Position> MoveInDirection(Position from, Board board, Direction dir)
    {
        for (Position pos = from + dir; Board.InsideBoard(pos); pos += dir)
        {
            if (board.EmptyPosition(pos))
            {
                yield return pos;
                continue;
            }
            break;
        }
    }

    protected IEnumerable<Position> MoveInDirectionsLimited(Position from, Board board, int maxSteps, params Direction[] dirs)
    {
        foreach (var direction in dirs)
        {
            int steps = 0;
            foreach (var pos in MoveInDirection(from, board, direction))
            {
                if (steps >= maxSteps)
                {
                    break;
                }
                yield return pos;
                steps++;
            }
        }
    }
}
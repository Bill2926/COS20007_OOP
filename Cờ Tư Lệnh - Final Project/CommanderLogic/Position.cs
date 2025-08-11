namespace CommanderLogic
{
    public class Position
    {
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static bool OceanPosition(Position to)
        {
            int[] OceanCol = { 0, 1 };

            if (OceanCol.Contains(to.Column))
            {
                return true;
            }
            return false;
        }

        public static bool CanCrossRiver(Move move)
        {
            return (move.From.Row <= 5 && move.To.Row == 6) || (move.From.Row >= 6 && move.To.Row == 5);
        }

        public static bool CrossShallowRiver(Move move)
        {
            int[] ShallowRiverCol = [ 5, 7 ];

            if ((move.From.Row == 5 && move.To.Row == 6) || (move.From.Row == 6 && move.To.Row == 5))
            {
                if (ShallowRiverCol.Contains(move.From.Column) && ShallowRiverCol.Contains(move.To.Column))
                {
                    return true;
                }
            }
            return false;
        }

        public string PosToString()
        {
            return $"({Column},{Row})";
        }

        public override bool Equals(object obj) => obj is Position position && Row == position.Row && Column == position.Column;

        public override int GetHashCode() => HashCode.Combine(Row, Column);

        public int Row { get; }

        public int Column { get; }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public static Position operator +(Position p, Direction d)
        {
            return new Position(p.Row + d.RowChange, p.Column + d.ColumnChange);
        }
    }

}

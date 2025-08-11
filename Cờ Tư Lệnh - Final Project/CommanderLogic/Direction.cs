namespace CommanderLogic
{
    public class Direction
    {
        //preset directions
        //readonly so that it cannot be reassigned after initialization
        public readonly static Direction Up = new Direction(1, 0);
        public readonly static Direction Down = new Direction(-1, 0);
        public readonly static Direction Left = new Direction(0, -1);
        public readonly static Direction Right = new Direction(0, 1);
        public readonly static Direction UpLeft = new Direction(1, -1);
        public readonly static Direction UpRight = new Direction(-1, 1);
        public readonly static Direction DownLeft = new Direction(-1, -1);
        public readonly static Direction DownRight = new Direction(1, 1);

        public Direction(int rowChange, int columnChange)
        {
            RowChange = rowChange;
            ColumnChange = columnChange;
        }

        public int RowChange { get; }

        public int ColumnChange { get; }

        public static Direction operator *(int k, Direction d)
        {
            return new Direction(k * d.RowChange, k * d.ColumnChange);
        }
    }
}

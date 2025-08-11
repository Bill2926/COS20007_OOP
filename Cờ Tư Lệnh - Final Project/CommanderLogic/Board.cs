using CommanderLogic.Pieces;

namespace CommanderLogic
{
    public class Board
    {
        private readonly GamePieces[,] pieces = new GamePieces[12, 11]; //2D Array, Col and Row + 1 because of zero indexed

        public static Board Initialize() //initial the board
        {
            Board board = new();
            board.InitializePieces();
            return board;
        }

        public GamePieces this[int row, int column] //set a piece with both row and col
        {
            get { return pieces[row, column]; }
            set { pieces[row, column] = value;}
        }

        public GamePieces this[Position pos] //set a piece's with Position object
        {
            //get { return pieces[pos.Row, pos.Column]; }
            get
            {
                if (!InsideBoard(pos))
                    throw new ArgumentOutOfRangeException(nameof(pos), "Position is outside the board.");
                return pieces[pos.Row, pos.Column];
            }
            set { pieces[pos.Row, pos.Column] = value; }
        }

        public static bool InsideBoard(Position pos) //prevent pieces outside the board
        {
            return pos.Row >= 0 && pos.Row < 12 && pos.Column >= 0 && pos.Column < 11;
        }

        public bool EmptyPosition(Position pos) //that position is empty or not
        {
            return this[pos] == null;
        }

        private void InitializePieces()
        {
            // The coordinate also zero indexed
            //11,6 means 12th row and 7th column
            //(doesn't matter the coordinate system, just count by hand)

            // Blue
            this[11, 6] = new Commander(Player.Blue);
            this[7, 2] = new Infantry(Player.Blue);
            this[7, 10] = new Infantry(Player.Blue);
            this[8, 5] = new Tank(Player.Blue);
            this[8, 7] = new Tank(Player.Blue);
            this[7, 6] = new Militia(Player.Blue);
            this[7, 3] = new Engineer(Player.Blue);
            this[7, 9] = new Engineer(Player.Blue);
            this[9, 3] = new Artillery(Player.Blue);
            this[9, 9] = new Artillery(Player.Blue);
            this[8, 4] = new AAG(Player.Blue);
            this[8, 8] = new AAG(Player.Blue);
            this[9, 6] = new AAM(Player.Blue);
            this[10, 4] = new AF(Player.Blue);
            this[10, 8] = new AF(Player.Blue);
            this[10, 1] = new Navy(Player.Blue);
            this[8, 2] = new Navy(Player.Blue);
            this[10, 5] = new HQ(Player.Blue);
            this[10, 7] = new HQ(Player.Blue);

            // Red
            this[0, 6] = new Commander(Player.Red);
            this[4, 2] = new Infantry(Player.Red);
            this[4, 10] = new Infantry(Player.Red);
            this[3, 5] = new Tank(Player.Red);
            this[3, 7] = new Tank(Player.Red);
            this[4, 6] = new Militia(Player.Red);
            this[4, 3] = new Engineer(Player.Red);
            this[4, 9] = new Engineer(Player.Red);
            this[2, 3] = new Artillery(Player.Red);
            this[2, 9] = new Artillery(Player.Red);
            this[3, 4] = new AAG(Player.Red);
            this[3, 8] = new AAG(Player.Red);
            this[2, 6] = new AAM(Player.Red);
            this[1, 4] = new AF(Player.Red);  
            this[1, 8] = new AF(Player.Red);  
            this[1, 1] = new Navy(Player.Red);
            this[3, 2] = new Navy(Player.Red);
            this[1, 5] = new HQ(Player.Red);
            this[1, 7] = new HQ(Player.Red);
        }

        public Board Clone()
        {
            Board cloneBoard = new();
            for (int row = 0; row < 12; row++)
            {
                for (int col = 0; col < 11; col++)
                {
                    cloneBoard[row, col] = pieces[row, col]?.Clone(); // Clone each piece if it exists
                }
            }
            return cloneBoard;
        }
    }
}

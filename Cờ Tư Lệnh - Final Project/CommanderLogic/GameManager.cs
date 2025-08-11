namespace CommanderLogic
{
    public class GameManager
    {
        int bluePoint = 0;
        int redPoint = 0;
        int moveCount = 1;
        readonly Stack<(int moveCount, Board board, Move move, Player turn)> moveHistory = new();
        readonly Stack<(GamePieces piece, int row, int col)> capturedHistory = new();

        public GameManager(Player player, Board board)
        {
            Turn = player;
            Board = board;
        }

        public IEnumerable<Move> LegalMove(Position position)
        { 
            if (Board.EmptyPosition(position) || Board[position].Side != Turn)
            {
                return []; // No legal moves if the position is empty or not the player's turn
            }
           
            GamePieces piece = Board[position];
            return piece.GetMoves(position, Board);
        }

        public void MakeMove(Move move)
        {
            move.MoveExecute(Board);
            System.Diagnostics.Debug.WriteLine(IsGameOver());
            Turn = Turn.Opponent(); // Switch turn to the opponent after a move
        }

        public (GamePieces piece, Move move, Board board) Undo()
        {
            if (moveHistory.Count == 0)
                throw new InvalidOperationException("No moves to undo.");

            var lastMove = moveHistory.Pop();
            Turn = lastMove.turn;
            Board = lastMove.board;

            GamePieces piece = Board[lastMove.move.From];
            return (piece, lastMove.move, Board);
        }

        public void PushMoveHistory(int moveCount, Board board, Move move, Player turn)
        {
            moveHistory.Push((moveCount, board, move, turn));
        }

        public (int moveCount, Board board, Move move, Player turn)? PopMoveHistory()
        {
            return moveHistory.Count > 0 ? moveHistory.Pop() : null;
        }

        public void PushCapturedHistory(GamePieces piece, int row, int col)
        {
            capturedHistory.Push((piece, row, col));
        }

        public (GamePieces piece, int row, int col)? PopCapturedHistory()
        {
            return capturedHistory.Count > 0 ? capturedHistory.Pop() : null;
        }

        public IEnumerable<(int moveCount, Board board, Move move, Player turn)> GetMoveHistory()
        {
            return moveHistory.Reverse(); // So recent moves appear last
        }

        public void CheckGameResult(GamePieces p)
        {
            if (p.Name == PieceName.Commander)
            {
                if (p.Side == Turn)
                {
                    Result = GameResult.Win(Turn.Opponent(), EndReason.LostCommander);
                }
            }
        }

        public bool IsGameOver()
        {
            return Result != null;
        }

        public void OutOfTime(Player side)
        {
            Result = GameResult.Win(side.Opponent(), EndReason.OutOfTime);
        }

        //Properties
        public Board Board { get; set; }

        public Player Turn { get; set; }

        public int RedScore
        {
            get { return redPoint; }
            set { redPoint = value; }
        }

        public int BlueScore
        {
            get { return bluePoint; }
            set { bluePoint = value; }
        }

        public int MoveCount
        {
            get { return moveCount; }
            set { moveCount = value; }
        }

        public GameResult Result { get; private set; } = null;
    }
}

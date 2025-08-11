using CommanderLogic.Moves;

namespace CommanderLogic
{
    public class HQ : GamePieces
    {
        //HQ doesn't move, Commander can hide inside
        bool emptyHQ = true;
        public List<Commander> commanderSlot = new(1);

        public HQ(Player side)
        {
            Side = side;
        }

        public override PieceName Name => PieceName.HQ;

        public override Player Side { get; }

        public override int Point => 10;

        public override GamePieces CopyPiece()
        {
            HQ copy = new(Side)
            {
                HasMovedYet = this.HasMovedYet
            };
            return copy;
        }

        private IEnumerable<Move> UnhideCommander(Position from, Board board)
        {
            Position to = new(CommanderRow, CommanderCol);
            if (!emptyHQ && board.EmptyPosition(to))
            {
                yield return new UnHideHQ(from, to);
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return UnhideCommander(from, board); 
        }

        public bool IsEmpty()
        {
            return emptyHQ;
        }

        public void HideCommander(Commander commander, Position commanderFrom)
        {
            if (commander.Side == Side && commanderSlot.Count == 0 && IsEmpty())
            {
                commanderSlot.Add(commander);
                CommanderRow = commanderFrom.Row;
                CommanderCol = commanderFrom.Column;
                emptyHQ = false;
            }
        }

        public void UnHideCommander()
        {
            commanderSlot.Clear();
            emptyHQ = true;
            CommanderRow = CommanderCol = -1; 

        }

        public int CommanderRow { get; set; }

        public int CommanderCol { get; set; }
    }
}

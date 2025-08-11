namespace CommanderLogic
{
    public enum Player
    {
        None,
        Red,
        Blue
    }

    public static class PlayerExtensions
    {
        public static Player Opponent(this Player player) //return player's opponent
        {
            return player switch
            {
                Player.Red => Player.Blue,
                Player.Blue => Player.Red,
                _ => Player.None
            };
        }
    }
}

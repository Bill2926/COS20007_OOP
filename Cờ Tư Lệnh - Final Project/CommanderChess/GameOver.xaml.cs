using CommanderLogic;
using System.Windows.Controls;

namespace CommanderChess
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : UserControl
    {
        public event Action<PlayerChoices> Selected;
        
        public GameOver(GameManager manager)
        {
            InitializeComponent();
            GameResult result = manager.Result;
            Winner.Text = GetWinner(result.Winner);
            Reason.Text = GetReason(result.Reason, manager.Turn.Opponent());
        }

        private void Restart(object sender, System.Windows.RoutedEventArgs e)
        {
            Selected?.Invoke(PlayerChoices.Restart);
        }

        private void Exit(object sender, System.Windows.RoutedEventArgs e)
        {
            Selected?.Invoke(PlayerChoices.Exit);
        }

        private static string GetWinner(Player winner)
        {
            return winner switch
            {
                Player.Red => "RED WINS",
                Player.Blue => "BLUE WINS",
                _ => "DRAW !"
            };
        }

        private static string OpponentString (Player player)
        {
            return player switch
            {
                Player.Red => "BLUE",
                Player.Blue => "RED"
            };
        }

        private static string GetReason(EndReason reason, Player player)
        {
            return reason switch
            {
                EndReason.LostCommander => $"{OpponentString(player)} LOSE THEIR COMMANDER",
                EndReason.OutOfTime => $"{OpponentString(player)} RAN OUT OF TIME",
                _ => ""
            };
        }
    }

    public enum PlayerChoices
    {
        Restart,
        Exit,
        Continue
    }
}

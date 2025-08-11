using CommanderLogic;
using System.Windows;
using System.Windows.Controls;

namespace CommanderChess
{
    public partial class StartMenu : UserControl
    {
        bool _isMuted = false;

        public event Action<StartMenuChoice> Selected;
        public event Action<int> TimeSelected;

        public StartMenu(GameManager manager)
        {
            InitializeComponent();
            //
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            Selected?.Invoke(StartMenuChoice.Start);
            StartMenuGrid.Visibility = Visibility.Collapsed;
            TimeMenuGrid.Visibility = Visibility.Visible;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Selected?.Invoke(StartMenuChoice.Exit);
        }

        private void Mute(object sender, RoutedEventArgs e)
        {
            _isMuted = !_isMuted;
            if (sender is Button btn)
            {
                btn.Content = _isMuted ? "🔇" : "🔊";
            }
            Selected?.Invoke(StartMenuChoice.ToggleMute);
        }

        private void ConfirmTime(object sender, RoutedEventArgs e)
        {
            if (TimeSelection.SelectedItem is ComboBoxItem item)
            {
                int minutes = int.Parse(item.Content.ToString());
                TimeSelected?.Invoke(minutes);
            }
        }
        public bool IsMuted => _isMuted;
    }

    public enum StartMenuChoice
    {
        Start,
        Exit,
        ToggleMute
    }
}

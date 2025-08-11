using CommanderChess.View;
using CommanderLogic;
using CommanderLogic.Moves;
using System.Windows;
using System.Windows.Input;

namespace CommanderChess
{
    public partial class MainWindow : Window
    {
        bool isSelectingMove = false;
        string RedTime = "00:00:00";
        string BlueTime = "00:00:00";
        int _clockMin;
        readonly Dictionary<Position, Move> MoveCache = [];
        GameManager manager = new(Player.Red, Board.Initialize());
        readonly SoundControl _soundControl = new();
        LogView LogView;
        BoardView BoardView;
        GameClock redClock;
        GameClock blueClock;
        GraveyardView GraveyardView;
        Position SelectedPosition;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize views
            BoardView = new BoardView(Placing, manager);
            LogView = new LogView(Log, manager);
            GraveyardView = new GraveyardView(Graveyard, manager);

            var startMenu = new StartMenu(manager);
            Start.Content = startMenu;

            _soundControl.PlayMusic("Assets/SoundTrack/start.wav");

            StartMenuChoice choice;
            do
            {
                var tcs = new TaskCompletionSource<StartMenuChoice>();

                startMenu.Selected += option => tcs.TrySetResult(option);

                choice = await tcs.Task;

                if (choice == StartMenuChoice.ToggleMute)
                {
                    _soundControl.Volume = startMenu.IsMuted ? 0f : 0.7f;
                    if (startMenu.IsMuted)
                    {
                        _soundControl.StopMusic();
                    }
                    else _soundControl.PlayMusic("Assets/SoundTrack/start.wav");
                }

            } while (choice == StartMenuChoice.ToggleMute); 

            if (choice == StartMenuChoice.Start)
            {
                startMenu.TimeSelected += minutes =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        Start.Content = null;
                        _soundControl.StopMusic();
                        BoardView.PieceClicked += BoardView_PieceClicked;
                        BoardView.PositionClicked += Grid_MouseLeftButtonDown;
                        BoardView.Draw();
                        BoardView.Start(Dot);
                        BoardView.LoadImages();
                        LogView.Draw();
                        GraveyardView.Draw();
                        ClockInitiate(minutes);
                        ClockMin(minutes);
                    });
                };
            }
            else if (choice == StartMenuChoice.Exit)
            {
                Application.Current.Shutdown();
            }
        }


        private void ClockInitiate(int minutes)
        {
            redClock = new GameClock(Player.Red, minutes, time =>
            {
                Dispatcher.Invoke(() =>
                {
                    RedTime = time;
                    GraveyardView.RedTimeUpdate($"Red: {RedTime}");
                });
            });

            blueClock = new GameClock(Player.Blue, minutes, time =>
            {
                Dispatcher.Invoke(() =>
                {
                    BlueTime = time; 
                    GraveyardView.BlueTimeUpdate($"Blue: {BlueTime}");
                });
            });

            redClock.TimeOut += OutOfTime;
            blueClock.TimeOut += OutOfTime;
            redClock.Start();
            UpdateTime();
        }

        public void ClockMin(int minutes)
        {
            _clockMin = minutes;
        }

        private void SwitchClock()
        {
            if (manager.Turn == Player.Red)
            {
                redClock.Start();
                blueClock.Stop();
            }
            else if (manager.Turn == Player.Blue)
            {
                redClock.Stop();
                blueClock.Start();
            }
        }

        private void UpdateTime()
        {
            RedTime = redClock.GetTime();
            BlueTime = blueClock.GetTime();
            GraveyardView.ClockUpdate($"Red: {RedTime}", $"Blue: {BlueTime}");
        }

        private void OutOfTime(Player side)
        {
            Dispatcher.Invoke(() =>
            {
                manager.OutOfTime(side);

                redClock.Stop();
                blueClock.Stop();
                isSelectingMove = false;
                GOMenu();
            });
        }

        private void BoardView_PieceClicked(object sender, Position e)
        {
            if (MenuOnScreen())
            {
                return;
            }

            // Handle the piece selection logic
            if (SelectedPosition == null)
            {
                OnFromPositionSelected(e);
            }
            else
            {
                Grid_MouseLeftButtonDown(sender, e);
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, Position pos)
        {
            if (!isSelectingMove) return;

            // Handle the click on the piece image
            OnToPositionSelected(pos);
        }

        private void OnFromPositionSelected(Position position)
        {
            //point is clicked && no selected piece
            IEnumerable<Move> moves = manager.LegalMove(position);
            if (moves.Any())
            {
                SelectedPosition = position;
                CacheMoves(moves); // Cache the moves for the selected piece
                BoardView.ShowDot(MoveCache); // Show the dots for the available moves
                isSelectingMove = true;
            }
        }

        private void OnToPositionSelected(Position position)
        {
            SelectedPosition = null;
            BoardView.HideDot(MoveCache);
            isSelectingMove = false;

            if (MoveCache.TryGetValue(position, out Move move))
            {
                HandleMove(move);
            }
        }

        private void HandleMove(Move move)
        {
            GamePieces p = manager.Board[move.From];
            Board snapshot = manager.Board.Clone();
            manager.PushMoveHistory(manager.MoveCount++, snapshot, move, manager.Turn); 
            manager.MakeMove(move);

            if (p.Name == PieceName.Navy) //Play the ocean move for navy
                _soundControl.PlayMusic("Assets/SoundTrack/ocean.wav");

            if (move is NormalMove normalMove && normalMove.CapturedPiece is not null)
            {
                _soundControl.PlayMusic("Assets/SoundTrack/capture.mp3");
                GraveyardView.Add(normalMove.CapturedPiece);
            }

            BoardView.LoadImages();
            LogView.Draw();
            BoardView.CircleHQ(move);
            SwitchClock();

            //check if game is ended
            if (manager.IsGameOver())
            {
                GOMenu();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.R)
            {
                Undo();
            }
        }

        private void Undo()
        {
            try
            {
                var (piece, move, board) = manager.Undo();

                if (move is NormalMove normalMove && normalMove.CapturedPiece is not null)
                {
                    GraveyardView.Remove(piece);
                }
                if (move is HideHQ hidehq)
                {
                    BoardView.RedoHQ(hidehq);
                }

                BoardView.LoadImages();
                LogView.Draw();
                SwitchClock();

                if (manager.MoveCount != 0)
                    manager.MoveCount--;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Cannot undo: " + ex.Message, "Undo Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            MoveCache.Clear();
            foreach (Move move in moves)
            {
                MoveCache[move.To] = move;
            }
        }

        private bool MenuOnScreen()
        {
            return Menu.Content != null;
        }

        private void GOMenu()
        {
            _soundControl.PlayMusic("Assets/SoundTrack/end.wav", loop: false);
            GameOver gameOverMenu = new(manager);
            redClock.Stop();
            blueClock.Stop();
            Menu.Content = gameOverMenu;

            gameOverMenu.Selected += option =>
            {
                if (option == PlayerChoices.Restart)
                {
                    Menu.Content = null;
                    _soundControl.StopMusic();
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }

        private void RestartGame()
        {
            //Clear UI and other data
            Placing.Children.Clear();
            Dot.Children.Clear();
            Graveyard.Children.Clear();
            Log.Children.Clear();
            MoveCache.Clear();
            BoardView.HideDot(MoveCache);

            //Initiate again
            //Recreate View Classes
            manager = new(Player.Red, Board.Initialize());
            BoardView = new BoardView(Placing, manager);
            LogView = new LogView(Log, manager);
            GraveyardView = new GraveyardView(Graveyard, manager);

            //Start to draw the UI
            BoardView.Draw();
            BoardView.Start(Dot);
            BoardView.LoadImages();
            BoardView.PieceClicked += BoardView_PieceClicked;
            BoardView.PositionClicked += Grid_MouseLeftButtonDown;
            GraveyardView.Draw();
            LogView.Draw();
            ClockInitiate(_clockMin);
            System.Diagnostics.Debug.WriteLine(_clockMin);
        }
    }
}

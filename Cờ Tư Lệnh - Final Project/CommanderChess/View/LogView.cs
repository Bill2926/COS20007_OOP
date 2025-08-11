using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommanderLogic;

namespace CommanderChess.View
{
    public class LogView
    {
        readonly Canvas canvas;
        readonly GameManager manager;

        public LogView(Canvas canvas, GameManager manager)
        {
            this.canvas = canvas;
            this.manager = manager;
        }

        public void Draw()
        {
            canvas.Children.Clear();
            TextBlock log = new()
            {
                Text = "Moves Log",
                FontSize = 10,
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Bold,
            };

            Canvas.SetLeft(log, 0);
            Canvas.SetTop(log, -15);
            canvas.Children.Add(log);

            int row = 0;
            var history = manager.GetMoveHistory().TakeLast(10);
            foreach (var (moveCount, board, move, turn) in history)
            {
                TextBlock logRow = new()
                {
                    Text = $"{moveCount}. {move.Log(board)}",
                    FontSize = 7,
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.Normal,
                };

                Canvas.SetLeft(logRow, 0);
                Canvas.SetTop(logRow, row * 15);
                canvas.Children.Add(logRow);
                row++;
            }

            canvas.Width = 100;
            canvas.Height = 10 * 15;
        }
    }
}

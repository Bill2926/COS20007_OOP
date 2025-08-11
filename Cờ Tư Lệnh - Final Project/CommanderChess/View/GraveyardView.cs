using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommanderLogic;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace CommanderChess.View
{
    public class GraveyardView
    {
        readonly int graveyard = 20;
        int redCount = 0;
        int blueCount = 0;
        string RedTime = "00:05:00";
        string BlueTime = "00:05:00";
        TextBlock PointRedText;
        TextBlock PointBlueText;
        TextBlock RedTimer;
        TextBlock BlueTimer;
        readonly Image[,] pieceGrave = new Image[10, 6];

        readonly Canvas canvas;
        readonly GameManager manager;

        public GraveyardView(Canvas canvas, GameManager manager)
        {
            this.canvas = canvas;
            this.manager = manager;
        }

        public void Draw()
        {
            int graveRows = 9;
            int graveCols = 5;

            for (int r = 0; r < graveRows; r++)
            {
                for (int c = 0; c < graveCols; c++)
                {
                    Rectangle cell = new()
                    {
                        Width = graveyard,
                        Height = graveyard,
                        Stroke = (r == 4) ? Brushes.Transparent : Brushes.Black,
                        Fill = (r == 4) ? Brushes.Transparent : Brushes.White
                    };
                    Canvas.SetLeft(cell, c * graveyard);
                    Canvas.SetTop(cell, r * graveyard); // Flip Y-axis
                    canvas.Children.Add(cell);

                    Image img = new()
                    {
                        Width = graveyard,
                        Height = graveyard
                    };
                    pieceGrave[r, c] = img;
                    Canvas.SetLeft(img, c * graveyard);
                    Canvas.SetTop(img, r * graveyard);
                    canvas.Children.Add(img);
                }
            }

            canvas.Width = graveCols * graveyard;
            canvas.Height = graveRows * graveyard;

            //Display points
            PointRedText = new TextBlock
            {
                Text = $"Point for Red: {manager.RedScore}",
                FontSize = 10,
                Foreground = Brushes.Red,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(5)
            };
            canvas.Children.Add(PointRedText);
            Canvas.SetLeft(PointRedText, 0);
            Canvas.SetTop(PointRedText, 8 * graveyard + 15);

            PointBlueText = new TextBlock
            {
                Text = $"Point for Blue: {manager.BlueScore}",
                FontSize = 10,
                Foreground = Brushes.Blue,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(5)
            };
            canvas.Children.Add(PointBlueText);
            Canvas.SetLeft(PointBlueText, 0);
            Canvas.SetTop(PointBlueText, -20);

            RedTimer = new TextBlock
            {
                Text = $"Red: {RedTime}",
                FontSize = 10,
                Foreground = Brushes.Red,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(5)
            };
            canvas.Children.Add(RedTimer);
            Canvas.SetLeft(RedTimer, 0);
            Canvas.SetTop(RedTimer, 8 * graveyard + 30);

            BlueTimer = new TextBlock
            {
                Text = $"Blue: {BlueTime}",
                FontSize = 10,
                Foreground = Brushes.Blue,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(5)
            };
            canvas.Children.Add(BlueTimer);
            Canvas.SetLeft(BlueTimer, 0);
            Canvas.SetTop(BlueTimer, -35);
        }

        public void Add(GamePieces piece)
        {
            int index;
            int piecePoint = piece.Point;
            if (piece.Side == Player.Red)
            {
                index = redCount++;
                manager.BlueScore += piecePoint;
            }
            else
            {
                index = blueCount++;
                manager.RedScore += piecePoint;
            }

            int col = index % 5;
            int row = (piece.Side == Player.Red) ? index / 5 : (index / 5) + 5;

            if (row < 10 && col < 6) // safe bounds for pieceGrave
            {
                Image img = pieceGrave[row, col];
                img.Source = PieceImage.GetImage(piece); // your existing loader
            }

            PointRedText.Text = $"Point for Red: {manager.RedScore}";
            PointBlueText.Text = $"Point for Blue: {manager.BlueScore}";

            manager.PushCapturedHistory(piece, row, col);
            manager.CheckGameResult(piece);
            System.Diagnostics.Debug.WriteLine(manager.IsGameOver());
        }

        public void Remove(GamePieces piece)
        {
            var history = manager.PopCapturedHistory();
            if (history is null)
                return;

            var (capturedPiece, row, col) = history.Value;
            Image img = pieceGrave[row, col];
            img.Source = null;

            int piecePoint = capturedPiece.Point;
            if (capturedPiece.Side == Player.Red)
            {
                redCount--;
                manager.BlueScore -= piecePoint;
            }
            else
            {
                blueCount--;
                manager.RedScore -= piecePoint;
            }

            PointRedText.Text = $"Point for Red: {manager.RedScore}";
            PointBlueText.Text = $"Point for Blue: {manager.BlueScore}";
        }

        public void ClockUpdate(string changeR, string changeB)
        {
            RedTimer.Text = changeR;
            BlueTimer.Text = changeB;
            RedTimer.InvalidateVisual();
            BlueTimer.InvalidateVisual();
        }

        public void RedTimeUpdate(string changeR)
        {
            RedTimer.Text = changeR;
        }

        public void BlueTimeUpdate(string changeB)
        {
            BlueTimer.Text = changeB;
        }
    }
}

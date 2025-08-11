using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using CommanderLogic;

namespace CommanderChess.View
{
    public class BoardView
    {
        readonly int rows = 11;
        readonly int cols = 10;
        readonly int cellSize = 25;
        double imageSize = 20;
        readonly Image[,] pieceImgs = new Image[12, 11]; //2D array to hold images of Placing
        readonly Ellipse[,] DotCircles = new Ellipse[12, 11]; //2D array to hold dots of Placing 
        readonly Ellipse[,] HQHighlight = new Ellipse[12, 11];
        readonly Position[] hqPos =
        [
            new Position (5, 1),
            new Position (7,1),
            new Position (5,10),
            new Position (7, 10)
        ];
        readonly Canvas canvas;
        readonly GameManager manager;

        public BoardView(Canvas canvas, GameManager manager)
        {
            this.canvas = canvas;
            this.manager = manager;
        }

        public event EventHandler<Position> PositionClicked;

        public event EventHandler<Position> PieceClicked;

        private void PieceImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image image && image.Tag is Position pos)
            {
                PieceClicked?.Invoke(this, pos);
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Ellipse dotCircle && dotCircle.Tag is Position pos)
            {
                PositionClicked?.Invoke(this, pos);
            }
        }

        public void Draw()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Rectangle cell = new()
                    {
                        Width = cellSize,
                        Height = cellSize,
                        Stroke = Brushes.Black,
                        Fill = (r == 5 || c == 0 || c == 1) ? Brushes.RoyalBlue : Brushes.White,
                    };

                    Line bridge1 = new()
                    {
                        X1 = 0,
                        Y1 = 0,
                        X2 = 0,
                        Y2 = cellSize,
                        Stroke = Brushes.OrangeRed
                    };

                    Line bridge2 = new()
                    {
                        X1 = 0,
                        Y1 = 0,
                        X2 = 0,
                        Y2 = cellSize,
                        Stroke = Brushes.OrangeRed
                    };

                    Canvas.SetLeft(bridge1, 5 * cellSize);
                    Canvas.SetTop(bridge1, 5 * cellSize);
                    Canvas.SetLeft(bridge2, 7 * cellSize);
                    Canvas.SetTop(bridge2, 5 * cellSize);
                    Canvas.SetLeft(cell, c * cellSize);
                    Canvas.SetTop(cell, (rows - 1 - r) * cellSize); // Flip Y-axis

                    canvas.Children.Add(cell);
                    canvas.Children.Add(bridge1);
                    canvas.Children.Add(bridge2);
                }
            }

            foreach (var pos in hqPos)
            {
                Ellipse hqCircle = new()
                {
                    Width = 25,
                    Height = 25,
                    Fill = Brushes.Transparent
                };
                HQHighlight[pos.Column, pos.Row] = hqCircle;
                double x = pos.Row * cellSize - 25 / 2 - 0.5;
                double y = (11 - pos.Column) * cellSize - 25 / 2 - 0.5; // HQ is at (11, 6)
                Canvas.SetLeft(hqCircle, x);
                Canvas.SetTop(hqCircle, y);
                canvas.Children.Add(hqCircle);
            }

            canvas.Width = cols * cellSize;
            canvas.Height = rows * cellSize;
        }

        public void Start(Canvas dot)
        {
            for (int r = 0; r < 12; r++)
            {
                for (int c = 0; c < 11; c++)
                {
                    Image image = new()
                    {
                        Width = 20,
                        Height = 20
                    };
                    pieceImgs[r, c] = image;
                    double x = c * cellSize - imageSize / 2;
                    double y = (11 - r) * cellSize - imageSize / 2;
                    Canvas.SetLeft(image, x);
                    Canvas.SetTop(image, y);
                    canvas.Children.Add(image);

                    image.Tag = new Position(r, c);
                    image.MouseLeftButtonDown += PieceImage_MouseLeftButtonDown;

                    // Create a dot for each cell
                    Ellipse DotCircle = new()
                    {
                        Width = 6,
                        Height = 6,
                        Fill = Brushes.Transparent
                    };
                    DotCircles[r, c] = DotCircle;
                    double dotX = c * cellSize - 7 / 2;
                    double dotY = (11 - r) * cellSize - 7 / 2;
                    Canvas.SetLeft(DotCircle, dotX);
                    Canvas.SetTop(DotCircle, dotY);
                    dot.Children.Add(DotCircle);

                    DotCircle.Tag = new Position(r, c);
                    DotCircle.MouseLeftButtonDown += Grid_MouseLeftButtonDown;
                }
            }
        }

        public void LoadImages()
        {
            for (int r = 0; r < 12; ++r)
            {
                for (int c = 0; c < 11; ++c)
                {
                    GamePieces p = manager.Board[r, c];
                    pieceImgs[r, c].Source = PieceImage.GetImage(p); //Get the image from PieceImage class
                }
            }
        }

        public void CircleHQ(Move move)
        {
            Position pos = move.To;
            Position pos1 = move.From;
            if (manager.Board[pos.Row, pos.Column] is HQ hq && !hq.IsEmpty())
            {
                HQHighlight[pos.Row, pos.Column].Fill = Brushes.Aqua;
            }
            else if (manager.Board[pos1.Row, pos1.Column] is HQ hq1 && hq1.IsEmpty())
            {
                HQHighlight[pos1.Row, pos1.Column].Fill = Brushes.Transparent;
            }
        }

        public void RedoHQ(Move move)
        {
            Position pos = move.To;
            HQHighlight[pos.Row, pos.Column].Fill = Brushes.Transparent;
        }

        public void ShowDot(Dictionary<Position, Move> MoveCache)
        {
            foreach (Position to in MoveCache.Keys)
            {
                DotCircles[to.Row, to.Column].Fill = manager.Turn switch
                {
                    Player.Blue => Brushes.Blue,
                    Player.Red => Brushes.Red,
                    _ => Brushes.Gray
                };
            }
        }

        public void HideDot(Dictionary<Position, Move> MoveCache)
        {
            foreach (Position to in MoveCache.Keys)
            {
                DotCircles[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }
    }
}

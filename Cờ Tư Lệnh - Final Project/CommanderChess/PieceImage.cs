using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommanderLogic;

namespace CommanderChess
{
    public static class PieceImage
    {
        //Dictionaries for 2 sides
        private static readonly Dictionary<PieceName, ImageSource> RedSources = new()
        {
            { PieceName.Commander, LoadImage("Assets/PieceImages/Commander_Red.png") },
            { PieceName.HQ, LoadImage("Assets/PieceImages/HQ_Red.png") },
            { PieceName.Infantry, LoadImage("Assets/PieceImages/Infantry_Red.png") },
            { PieceName.Tank, LoadImage("Assets/PieceImages/Tank_Red.png") },
            { PieceName.Militia, LoadImage("Assets/PieceImages/Militia_Red.png") },
            { PieceName.Engineer, LoadImage("Assets/PieceImages/Engineer_Red.png") },
            { PieceName.Artillery, LoadImage("Assets/PieceImages/Artillery_Red.png") },
            { PieceName.AAG, LoadImage("Assets/PieceImages/AAG_Red.png") },
            { PieceName.AAM, LoadImage("Assets/PieceImages/AAM_Red.png") },
            { PieceName.AF, LoadImage("Assets/PieceImages/AF_Red.png") },
            { PieceName.Navy, LoadImage("Assets/PieceImages/Navy_Red.png") }
        };

        private static readonly Dictionary<PieceName, ImageSource> BlueSources = new()
        {
            { PieceName.Commander, LoadImage("Assets/PieceImages/Commander_Blue.png") },
            { PieceName.HQ, LoadImage("Assets/PieceImages/HQ_Blue.png") },
            { PieceName.Infantry, LoadImage("Assets/PieceImages/Infantry_Blue.png") },
            { PieceName.Tank, LoadImage("Assets/PieceImages/Tank_Blue.png") },
            { PieceName.Militia, LoadImage("Assets/PieceImages/Militia_Blue.png") },
            { PieceName.Engineer, LoadImage("Assets/PieceImages/Engineer_Blue.png") },
            { PieceName.Artillery, LoadImage("Assets/PieceImages/Artillery_Blue.png") },
            { PieceName.AAG, LoadImage("Assets/PieceImages/AAG_Blue.png") },
            { PieceName.AAM, LoadImage("Assets/PieceImages/AAM_Blue.png") },
            { PieceName.AF, LoadImage("Assets/PieceImages/AF_Blue.png") },
            { PieceName.Navy, LoadImage("Assets/PieceImages/Navy_Blue.png") }
        };

        private static ImageSource LoadImage(string imgPath)
        {
            return new BitmapImage(new Uri(imgPath, UriKind.Relative));
        }

        private static ImageSource GetImage(Player side, PieceName name)
        {
            return side switch
            {
                Player.Red => RedSources[name],
                Player.Blue => BlueSources[name],
                _ => throw new NotImplementedException(),
            };
        }

        public static ImageSource GetImage(GamePieces p)
        {
            if (p == null)
            {
                return null;
            }

            return GetImage(p.Side, p.Name);
        }
    }
}

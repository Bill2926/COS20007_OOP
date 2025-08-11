using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class MyLine : Shape
    {
        float _endX, _endY;
        
        public MyLine()
        {
            FillColor = Color.Red;
        }

        public MyLine(Color color, float startX, float startY, float endX, float endY)
        {
            FillColor = color;
            X = startX;
            Y = startY;
            _endX = endX;
            _endY = endY;
        }

        public float EndX
        {
            get { return _endX; }
            set { _endX = value; }
        }

        public float EndY
        {
            get { return _endY; }
            set { _endY = value; }
        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutLine();
            }
            SplashKit.DrawLine(FillColor, X, Y, _endX, _endY);
        }

        public override void DrawOutLine()
        {
            SplashKit.FillCircle(Color.Black, X, Y, 5);
            SplashKit.FillCircle(Color.Black, _endX, _endY, 5);
        }

        public override bool IsAt(Point2D pt)
        {
            double distance1 = SplashKit.PointPointDistance(pt, new Point2D() { X = X, Y = Y });
            double distance2 = SplashKit.PointPointDistance(pt, new Point2D() { X = _endX, Y = _endY });
            double result = distance1 + distance2;
            if ((int)result == 100)
            {
                return true;
            } return false;
        }
    }
}

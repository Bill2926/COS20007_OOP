using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class MyCircle : Shape //Shape is the base class
    {
        int _radius = 50;

        //constructor
        public MyCircle(): this(Color.Blue, 50 + 1) //SWH02701
        {
            //other steps
        }

        public MyCircle(Color color, int radius) : base(color)
        {
            _radius = radius;
        }

        //method
        public override void Draw()
        {
            if (Selected)
            {
                DrawOutLine();
            }
            SplashKit.FillCircle(FillColor, X, Y, _radius);
        }

        public override void DrawOutLine()
        {
            SplashKit.DrawCircle(Color.Black, X, Y, _radius + 5);
        }

        public override bool IsAt(Point2D pt)
        {
            double distance = SplashKit.PointPointDistance(pt, new Point2D() { X = this.X, Y = this.Y });
            if (distance <= _radius)
            {
                return true;
            }
            return false;
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Circle");
            base.SaveTo(writer);
            writer.WriteLine(X);
            writer.WriteLine(Y);
            writer.WriteLine(_radius);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            X = reader.ReadInteger();
            Y = reader.ReadInteger();
            _radius = reader.ReadInteger();
        }
    }
}

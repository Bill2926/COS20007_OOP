using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class MyRectangle : Shape //Shape is the base class
    {
        int _width, _height;

        //constructor

        public MyRectangle(): this(Color.Green, 0.0f, 0.0f, 101, 101) //SWH02701
        {
            //other steps
        }

        public MyRectangle(Color color, float x, float y, int width, int height) : base(color)
        {
            Width = width;
            Height = height;
            X = x; //X Y belongs to the Shape class
            Y = y;
        }

        //method
        public override void Draw()
        {
            if (Selected)
            {
                DrawOutLine();
            }
            SplashKit.FillRectangle(FillColor, X, Y, Width, Height);
        }

        public override void DrawOutLine()
        {
            SplashKit.DrawRectangle(Color.Black, X - 7, Y - 7, _width + 14, _height + 14); //105547489
        }

        public override bool IsAt(Point2D pt)
        {
            return SplashKit.PointInRectangle(pt, SplashKit.RectangleFrom(X, Y, _width, _height));
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Rectangle");
            base.SaveTo(writer); //tell the base class to not be overriden
            writer.WriteLine(_width);
            writer.WriteLine(_height);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);  
            Width = reader.ReadInteger();
            Height = reader.ReadInteger();
        }

        //properties
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
    }
}

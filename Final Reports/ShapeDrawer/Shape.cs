using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ShapeDrawer
{
    public abstract class Shape
    {
        //private fields
        Color _color;
        float _x, _y;
        bool _selected; //bool field is "false" by default

        public Shape(): this(Color.Yellow) //Constructor
        {
            //other steps
        }

        public Shape(Color color) //Overloaded constructor
        {
            _color = color;
            _x = 10;
            _y = 10;
        }
        //Properties
        public Color FillColor
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        //methods
        public abstract void Draw();

        public abstract bool IsAt(Point2D pt);

        public abstract void DrawOutLine();
    }
}
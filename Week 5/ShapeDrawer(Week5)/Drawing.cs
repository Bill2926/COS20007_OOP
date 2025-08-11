using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace ShapeDrawer
{
    public class Drawing
    {
        readonly List<Shape> _shapes;
        Color _background;

        public Drawing(Color background)
        {
            _shapes = new List<Shape>();
            _background = background;
        }

        public Drawing() : this(Color.White) //default constructor - initializes objs with predefined values
        {
            //other steps
        }

        //methods
        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        public void RemoveShape(Shape shape)
        {
            _ = _shapes.Remove(shape); //to discard the value it returns
        }

        public void Draw()
        {
            SplashKit.ClearScreen(_background);
            foreach (Shape shape in _shapes)
            {
                shape.Draw();
            }
        }

        public void SelectShapeAt(Point2D pt)
        {
            foreach (Shape shape in _shapes)
            {
                shape.Selected = shape.IsAt(pt);
            }
        }

        public void Save(string filename)
        {
            StreamWriter writer = new StreamWriter(filename);

            try
            {
                writer.WriteColor(_background);
                writer.WriteLine(_shapes.Count);

                foreach (Shape s in _shapes)
                {
                    s.SaveTo(writer);
                }
            }
            finally
            {
                writer.Close();
            }
        }

        public void Load(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            int count;
            string kind;
            Shape s;

            try
            {
                Background = reader.ReadColor();
                count = reader.ReadInteger();

                _shapes.Clear(); //clear the list before loading new shapes

                for (int i = 0; i < count; i++)
                {
                    kind = reader.ReadLine();
                    if (kind == "Rectangle")
                    {
                        s = new MyRectangle();
                    }
                    else if (kind == "Circle")
                    {
                        s = new MyCircle();
                    }
                    else if (kind == "Line")
                    {
                        s = new MyLine();
                    }
                    else
                    {
                        throw new Exception("Unknown shape type: " + kind);
                    }
                    s.LoadFrom(reader);
                    _shapes.Add(s);
                }
            }
            finally
            {
                reader.Close();
            }
        }

        //properties
        public Color Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
            }
        }

        public int ShapeCount => _shapes.Count;

        public List<Shape> SelectedShapes
        {
            get
            {
                List<Shape> result = new List<Shape>();
                foreach (Shape shape in _shapes)
                {
                    if (shape.Selected)
                    {
                        result.Add(shape);
                    }
                }
                return result;
            }
        }

        public List<Shape> AllShapes
        {
            get
            {
                return _shapes;
            }
        }
    }
}

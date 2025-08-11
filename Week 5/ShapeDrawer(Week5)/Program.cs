using System;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class Program
    {
        enum ShapeKind
        {
            Rectangle,
            Circle,
            Line
        }   

        public static void Main()
        {
            ////the first Shape is the type of object, which is the class i've made earlier
            //Shape myShape = new Shape(); //Shape() to call the constructor
            Window window = new Window("Shape Drawer", 800, 600);
            Drawing myDrawing = new Drawing();
            ShapeKind kindToAdd = ShapeKind.Circle;
            do
            {
                //8.4
                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    kindToAdd = ShapeKind.Rectangle;
                }
                else if (SplashKit.KeyTyped(KeyCode.CKey))
                {
                    kindToAdd = ShapeKind.Circle;
                }
                else if (SplashKit.KeyTyped(KeyCode.LKey))
                {
                    kindToAdd = ShapeKind.Line;
                }
                
                //earlier code
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Shape newShape = null;
                    var lines = myDrawing.AllShapes.OfType<MyLine>().ToList();
                    int linesCount = lines.Count;
                    switch (kindToAdd)
                    {
                        case ShapeKind.Circle:
                            newShape = new MyCircle();
                            break;
                        case ShapeKind.Line:
                            if (linesCount < 9)
                            {
                                float X = SplashKit.MouseX();
                                float Y = SplashKit.MouseY();
                                newShape = new MyLine(Color.Red, X, Y, X + 100, Y);
                            }
                            break;
                        default:
                            newShape = new MyRectangle();
                            break;

                    }
                    if (newShape != null)
                    {
                        newShape.X = SplashKit.MouseX();
                        newShape.Y = SplashKit.MouseY();
                        myDrawing.AddShape(newShape);
                    }
                }
                if (SplashKit.KeyDown(KeyCode.SpaceKey))
                {
                    myDrawing.Background = Color.RandomRGB(255);
                }
                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    myDrawing.SelectShapeAt(SplashKit.MousePosition());
                }
                if (SplashKit.KeyDown(KeyCode.DeleteKey) || SplashKit.KeyDown(KeyCode.BackspaceKey))
                {
                    foreach (Shape newShape in myDrawing.SelectedShapes)
                    {
                        myDrawing.RemoveShape(newShape);
                    }
                }
                if (SplashKit.KeyTyped(KeyCode.SKey))
                {
                    myDrawing.Save(@"C:\Users\Bill\Desktop\COS20007\Week 5\ShapeDrawer(Week5)\TextDrawing.txt");
                }
                if (SplashKit.KeyTyped(KeyCode.OKey))
                {
                    try
                    {
                        myDrawing.Load(@"C:\Users\Bill\Desktop\COS20007\Week 5\ShapeDrawer(Week5)\TextDrawing.txt");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error loading file: {0}" + e.Message);
                    }
                }
                myDrawing.Draw();
                SplashKit.RefreshScreen();
            } while (!window.CloseRequested);
        }
    }
}
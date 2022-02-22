using project_true.Figures;
using System;
using project_true.Primitives;

namespace project_true
{
    class Program
    {
        
        
        
        static void Main(string[] args)
        {
            int distance = 5;

            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraDir = new MyVector() { X = 1, Y = 0, Z = 0 };

            Camera camera = new Camera(cameraCenter, cameraDir);

            MyPoint planeCenter = new MyPoint() { X = distance, Y = 0, Z = 0 };

            MyPlane plane = new MyPlane(planeCenter, cameraDir);

            Console.WriteLine();
        }
    }
}
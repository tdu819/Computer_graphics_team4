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
            int n = 20, m = 20;
            var r = 2;

            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraDir = new MyVector() { X = 1, Y = 0, Z = 0 };

            Camera camera = new Camera(cameraCenter, cameraDir);

            MyPoint planeCenter = new MyPoint() { X = distance, Y = 0, Z = 0 };

            MyPlane plane = new MyPlane(planeCenter, cameraDir);

            MyPoint TopLeft = plane.GetTopLeftPoint(0, 10, -10);

            MyPoint sphereCenter = new MyPoint() { X = 10, Y = 0, Z = 0 };

            Sphere sphere = new Sphere() { Center = sphereCenter, Radius = r };

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    MyPoint point = new MyPoint() { X = TopLeft.X + 0, Y = TopLeft.Y - j, Z = TopLeft.Z + i };

                    var o = cameraCenter;
                    var c = sphereCenter;
                    var k = o - c;
                    var d = new MyVector(cameraCenter, point);

                    var d2 = MyVector.Dot(d, d);
                    var r2 = r * r;


                    point.Value = "#";
                    Console.Write(point.Value);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
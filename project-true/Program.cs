using project_true.Figures;
using System;
using project_true.Primitives;
using project_true.Tools;

namespace project_true
{
    public static class Program
    {
        static void Main(string[] args)
        {
            int distance = 5;
            int n = 20, m = 20;
            double r = 9;

            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraDir = new MyVector() { X = 1, Y = 0, Z = 0 };

            MyCamera myCamera = new MyCamera(cameraCenter, cameraDir);

            MyPoint planeCenter = new MyPoint() { X = distance, Y = 0, Z = 0 };

            MyPlane plane = new MyPlane(planeCenter, cameraDir);

            MyPoint TopLeft = plane.GetTopLeftPoint(0, 9.5, -9.5);

            MyPoint sphereCenter = new MyPoint() { X = 10, Y = 1, Z = 2 };

            MySphere mySphere = new MySphere() { Center = sphereCenter, Radius = r };

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    MyPoint point = new MyPoint() { X = TopLeft.X + 0, Y = TopLeft.Y - i, Z = TopLeft.Z + j };

                    if (RayTracer.RayIntersectsSphere(cameraCenter, mySphere, point))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }


                }

                Console.WriteLine("|");
            }

            Console.WriteLine();
        }
    }
}
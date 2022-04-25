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
            //SphereTracing();
            TriangleTracing();
            Console.ReadLine();
        }

        static private void TriangleTracing()
        {
            // Distance from Camera to Plain
            int distance = 5;


            // Our Canvas size
            int n = 20, m = 20;

            // Sphere radius
            double r = 9;

            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraDir = new MyVector() { X = 1, Y = 0, Z = 0 };

            // Camera. Coordinates (0; 0; 0).
            MyCamera myCamera = new MyCamera(cameraCenter, cameraDir);

            MyPoint planeCenter = new MyPoint() { X = distance, Y = cameraCenter.Y, Z = cameraCenter.Z };

            // Plain. Плоскость.
            MyPlane plane = new MyPlane(planeCenter, cameraDir);

            MyPoint topLeft = plane.GetTopLeftPoint(0, 9.5, -9.5);

            // Triangle
            MyTriangle myTriangle = new MyTriangle(new MyPoint(10, 0, -10), new MyPoint(10, 0, 10), new MyPoint(10, 10, 0));

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    MyPoint point = new MyPoint() { X = topLeft.X + 0, Y = topLeft.Y - i, Z = topLeft.Z + j };

                    // ref IntersectionPoint
                    MyPoint IntersectionPoint = new MyPoint();

                    // DO NOT TOUCH!!!!!
                    if (RayTracer.RayIntersectsTriangle(cameraCenter, new MyVector(point, cameraCenter), myTriangle, ref IntersectionPoint))
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

        static private void SphereTracing()
        {
            // Distance from Camera to Plain
            int distance = 5;


            // Our Canvas size
            int n = 20, m = 20;

            // Sphere radius
            double r = 9;

            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraDir = new MyVector() { X = 1, Y = 0, Z = 0 };

            // Camera. Coordinates (0; 0; 0).
            MyCamera myCamera = new MyCamera(cameraCenter, cameraDir);

            MyPoint planeCenter = new MyPoint() { X = distance, Y = cameraCenter.Y, Z = cameraCenter.Z };

            // Plain. Плоскость.
            MyPlane plane = new MyPlane(planeCenter, cameraDir);

            MyPoint topLeft = plane.GetTopLeftPoint(0, 9.5, -9.5);

            MyPoint sphereCenter = new MyPoint() { X = 10, Y = 1, Z = 2 };

            // Our Sphere
            MySphere mySphere = new MySphere() { Center = sphereCenter, Radius = r };

            // Triangle
            MyTriangle myTriangle = new MyTriangle(new MyPoint(10, 0, 0), new MyPoint(15, 0, 0), new MyPoint(10, 5, 0));

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    MyPoint point = new MyPoint() { X = topLeft.X + 0, Y = topLeft.Y - i, Z = topLeft.Z + j };

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
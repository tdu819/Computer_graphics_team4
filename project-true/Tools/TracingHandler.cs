using project_true.Figures;
using project_true.Primitives;
using project_true.MyScene;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_true.Tools
{
    class TracingHandler
    {
        public static void TriangleTracing()
        {
            // Distance from Camera to Plain
            int distance = 5;


            // Our Canvas size
            int n = 20, m = 20;

            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraDir = new MyVector() { X = 1, Y = 0, Z = 0 };

            // Camera. Coordinates (0; 0; 0).
            MyCamera myCamera = new MyCamera(cameraCenter, cameraDir, distance);

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

                    // Point and Camera most likely not working correctly
                    if (myTriangle.RayIntersect(cameraCenter, point, ref IntersectionPoint))
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

        public static void SphereTracing()
        {
            // Distance from Camera to Plain
            int distance = 5;


            // Our Canvas size
            int n = 20, m = 20;

            // Sphere radius
            double r = 3;

            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraDir = new MyVector() { X = 1, Y = 0, Z = 0 };

            // Camera. Coordinates (0; 0; 0).
            MyCamera myCamera = new MyCamera(cameraCenter, cameraDir, distance);

            MyPoint planeCenter = new MyPoint() { X = distance, Y = cameraCenter.Y, Z = cameraCenter.Z };

            // Plain. Плоскость.
            MyPlane plane = new MyPlane(planeCenter, cameraDir);

            MyPoint topLeft = plane.GetTopLeftPoint(0, 9.5, -9.5);

            MyPoint sphereCenter = new MyPoint() { X = -1, Y = 0, Z = 0 };

            // Our Sphere
            MySphere mySphere = new MySphere() { Center = sphereCenter, Radius = r };

            // Light Vector
            MyVector L = new MyVector(0, 1, 0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    MyPoint point = new MyPoint() { X = topLeft.X + 0, Y = topLeft.Y - i, Z = topLeft.Z + j };
                    MyPoint IntersectionPoint = new MyPoint();

                    if (mySphere.RayIntersect(cameraCenter, point, ref IntersectionPoint))
                    {
                        Lighting(IntersectionPoint, sphereCenter, L);
                        //Console.Write("#");
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

        public static void Lighting(MyPoint intersection, MyPoint center, MyVector L)
        {
            MyVector normal = new MyVector(intersection, center).Normalization();
            double dot = MyVector.Dot(normal, L);
            if (dot < 0)
            {
                Console.Write(" ");
            }
            else if (dot >= 0 && dot < 0.2)
            {
                Console.Write(".");
            }
            else if (dot >= 0.2 && dot < 0.5)
            {
                Console.Write("*");
            }
            else if (dot >= 0.5 && dot < 0.8)
            {
                Console.Write("O");
            }
            else if (dot >= 0.8)
            {
                Console.Write("#");
            }
        }
        public void NearestSphereTracing()
        {
            // Camera
            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraVector = new MyVector() { X = 1, Y = 0, Z = 0 };
            int distance = 5;

            MyCamera camera = new MyCamera(cameraCenter, cameraVector, distance);

            // Scene
            Scene scene = new Scene(camera);

            //Sphere1
            double r1 = 9;
            MyPoint sphereCenter1 = new MyPoint() { X = 10, Y = 0, Z = -5 };

            Figure mySphere1 = new MySphere() { Center = sphereCenter1, Radius = r1 };

            //Sphere2
            double r2 = 8;
            MyPoint sphereCenter2 = new MyPoint() { X = 10, Y = 0, Z = 5 };

            Figure mySphere2 = new MySphere() { Center = sphereCenter2, Radius = r2 };

            // Add Sphere
            scene.AddFigure(mySphere1);
            scene.AddFigure(mySphere2);

            // Our Canvas size
            int n = 20, m = 20;
            MyPoint topLeft = camera.Plane.GetTopLeftPoint(0, 9.5, -9.5);

            // Light Vector
            MyVector L = new MyVector(0, 1, 0);
            Figure nearest = FindNearestFigure(scene, n, m, topLeft);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    MyPoint rayPointer = new MyPoint() { X = topLeft.X + 0, Y = topLeft.Y - i, Z = topLeft.Z + j };

                    // ref IntersectionPoint
                    MyPoint IntersectionPoint = new MyPoint();

                    // Point and Camera most likely not working correctly
                    if (nearest.RayIntersect(scene.Camera.Center, rayPointer, ref IntersectionPoint))
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

        public Figure FindNearestFigure(Scene scene, int height, int width, MyPoint topLeft)
        {
            double minDistance = Double.MaxValue;
            Figure nearestFigure = null;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    MyPoint rayPointer = new MyPoint() { X = topLeft.X + 0, Y = topLeft.Y - i, Z = topLeft.Z + j };

                    foreach (Figure f in scene.Figures)
                    {
                        MyPoint IntersectionPoint = new MyPoint();
                        if (f.RayIntersect(scene.Camera.Center, rayPointer, ref IntersectionPoint))
                        {
                            double dist = MyVector.Length(new MyVector(scene.Camera.Center, IntersectionPoint));
                            if (dist < minDistance)
                            {
                                minDistance = dist;
                                nearestFigure = f;
                            }
                        }
                    }
                }
            }
            return nearestFigure;
        }
    }
}

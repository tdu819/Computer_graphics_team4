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
        // TODO Intersection point is not necessary?
        /// <summary>
        /// Part 4.5 Implementation
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="intersection"></param>
        /// <param name="L"></param>
        public static void Lighting(Figure figure, MyPoint intersection, MyVector L)
        {
            MyVector normal = figure.GetNormal(intersection);
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

        /// <summary>
        /// Part 4 Implementation
        /// </summary>
        public void FigureTracing()
        {
            // Camera
            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraVector = new MyVector() { X = 1, Y = 0, Z = 0 };
            int distance = 5;

            MyCamera camera = new MyCamera(cameraCenter, cameraVector, distance);

            // Scene
            Scene scene = new Scene(camera);

            //Sphere
            double r = 9;
            MyPoint sphereCenter = new MyPoint() { X = 10, Y = 0, Z = 0 };

            //Figure myFigure = new MySphere() { Center = sphereCenter, Radius = r };

            //Triangle
            MyPoint a = new MyPoint(7, 0, -5);
            MyPoint b = new MyPoint(5, 5, -2);
            MyPoint c = new MyPoint(6, 0, 8);

            Figure myFigure = new MyTriangle(a, b, c);

            // Add Sphere
            scene.AddFigure(myFigure);

            // Our Canvas size
            int height = 20, width = 20;
            MyPoint topLeft = camera.Plane.GetTopLeftPoint(0, 9.5, -9.5);

            // Light Vector
            //MyVector L = new MyVector(0, 1, 0);
            MyVector L = null;

            DrawFigure(myFigure, scene, height, width, topLeft, L);

            Console.WriteLine();
        }
        public void NearestFigureTracing()
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
            int height = 20, width = 20;
            MyPoint topLeft = camera.Plane.GetTopLeftPoint(0, 9.5, -9.5);

            // Light Vector
            Figure nearest = FindNearestFigure(scene, height, width, topLeft);

            DrawFigure(nearest, scene, height, width, topLeft, null);

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

        public void DrawFigure(Figure figure, Scene scene, int height, int width, MyPoint topLeft, MyVector L)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    MyPoint rayPointer = new MyPoint() { X = topLeft.X + 0, Y = topLeft.Y - i, Z = topLeft.Z + j };

                    // ref IntersectionPoint
                    MyPoint IntersectionPoint = new MyPoint();

                    // Point and Camera most likely not working correctly
                    if (figure.RayIntersect(scene.Camera.Center, rayPointer, ref IntersectionPoint))
                    {
                        if (L != null)
                        {
                            Lighting(figure, IntersectionPoint, L);
                        }
                        else
                        {
                            Console.Write("#");
                        }
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine("|");
            }
        }
    }
}

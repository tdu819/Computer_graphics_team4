using project_true.Figures;
using project_true.Primitives;
using project_true.MyScene;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using project_true.Camera;
using System.IO;
using System.Numerics;
using Microsoft.VisualBasic.CompilerServices;

namespace project_true.Tracing
{
    public class TracingHandler
    {
        public Scene CreateTestingScene()
        {
            // Camera
            MyPoint cameraCenter = new MyPoint() { X = -10, Y = 0, Z = 0 };
            MyVector cameraVector = new MyVector() { X = 1, Y = 0, Z = 0 };
            int distance = 50;

            MyCamera camera = new MyCamera(cameraCenter, cameraVector, distance);

            // Scene
            Scene scene = new Scene(camera);


            return scene;
        }

        public (MyPoint, Figure) FindNearestIntersectionPoint(Scene scene, MyPoint rayPointer)
        {
            double minDistance = Double.MaxValue;
            Figure nearestFigure = null;
            MyPoint IntersectionPoint = null;

            foreach (Figure f in scene.Figures)
            {
                MyPoint Intersection = new MyPoint();
                if (f.RayIntersect(scene.Camera.Center, rayPointer, ref Intersection))
                {
                    double dist = MyVector.Length(new MyVector(scene.Camera.Center, Intersection));
                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        nearestFigure = f;
                        IntersectionPoint = new MyPoint(Intersection);
                    }
                }
            }

            return (IntersectionPoint, nearestFigure);
        }


        private static double Lighting(Figure figure, MyPoint intersection, MyVector L)
        {
            string result = "";
            MyVector normal = figure.GetNormal(intersection);
            double dot = MyVector.Dot(normal, L);
            return dot;
        }

        private void DrawLighting(double lighting)
        {
            if (lighting < 0)
            {
                Console.Write(" ");
            }
            else if (lighting >= 0 && lighting < 0.2)
            {
                Console.Write(".");
            }
            else if (lighting >= 0.2 && lighting < 0.5)
            {
                Console.Write("*");
            }
            else if (lighting >= 0.5 && lighting < 0.8)
            {
                Console.Write("O");
            }
            else if (lighting >= 0.8)
            {
                Console.Write("#");
            }
        }


        /// <summary>
        ///  lab 2 part 1
        /// </summary>
        public void FigureTracing()
        {
            var scene = CreateTestingScene();

            // Sphere
            double r = 9;
            MyPoint sphereCenter = new MyPoint() { X = 20, Y = 0, Z = 0 };

            Figure myFigureSphere = new MySphere() { Center = sphereCenter, Radius = r };

            //Triangle
            MyPoint a = new MyPoint(15, 25, -50);
            MyPoint b = new MyPoint(15, 25, 20);
            MyPoint c = new MyPoint(15, -30, 20);

            Figure myFigureTriangle = new MyTriangle(a, b, c);


            scene.AddFigure(myFigureSphere);
            scene.AddFigure(myFigureTriangle);


            // Our Canvas size
            int height = 20, width = 20;


            // Light Vector
            MyVector L = new MyVector(1, 0, 0);
            // MyVector L = null;

            // draw to console
            DrawScene(scene, height, width, L);
            WriteToPPM(scene, height, width, L, 255);

            Console.WriteLine();
        }

        public void NearestFigureTracing()
        {
            Scene scene = CreateTestingScene();

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

            // Light Vector
            Figure nearest = FindNearestFigure(scene, height, width);

            DrawFigure(nearest, scene, height, width, null);

            Console.WriteLine();
        }

        public Figure FindNearestFigure(Scene scene, int height, int width)
        {
            MyPoint topLeft = scene.Camera.Plane.GetTopLeftPoint(height, width);
            double minDistance = Double.MaxValue;
            Figure nearestFigure = null;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // todo strange raypointer. x + 0.
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

        private void DrawFigure(Figure figure, Scene scene, int height, int width, MyVector L)
        {
            MyPoint topLeft = scene.Camera.Plane.GetTopLeftPoint(height, width);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    MyPoint rayPointer = new MyPoint() { X = topLeft.X + 0, Y = topLeft.Y - i, Z = topLeft.Z + j };

                    // ref IntersectionPoint
                    MyPoint IntersectionPoint = new MyPoint();

                    if (!figure.RayIntersect(scene.Camera.Center, rayPointer, ref IntersectionPoint))
                    {
                        Console.Write(" ");
                        continue;
                    }

                    if (L != null)
                    {
                        Lighting(figure, IntersectionPoint, L);
                    }
                    else
                    {
                        Console.Write("#");
                    }
                }

                Console.WriteLine("|");
            }
        }

        public void DrawScene(Scene scene, int height, int width, MyVector L)
        {
            MyPoint topLeft = scene.Camera.Plane.GetTopLeftPoint(height, width);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    MyPoint rayPointer = new MyPoint() { X = topLeft.X + 0, Y = topLeft.Y - i, Z = topLeft.Z + j };

                    (MyPoint IntersectionPoint, Figure nearestFigure) = FindNearestIntersectionPoint(scene, rayPointer);

                    if (nearestFigure == null)
                    {
                        Console.Write(" ");
                        continue;
                    }

                    if (L == null)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        double lighting = Lighting(nearestFigure, IntersectionPoint, L);
                        DrawLighting(lighting);
                    }
                }

                Console.WriteLine("|");
            }
        }

        // lab2 part1
        private void WriteToPPM(Scene scene, int height, int width, MyVector L, int maxColor)
        {
            MyPoint topLeft = scene.Camera.Plane.GetTopLeftPoint(height, width);

            using StreamWriter file = new StreamWriter("car.ppm");
            
            //  "P3" means this is a RGB color image in ASCII
            file.WriteLine("P3");
            
            file.WriteLine($"{width} {height}");
            // TODO Create variable for max value of each color
            file.WriteLine(maxColor);

            Vector3 rgb = new Vector3(maxColor, maxColor, maxColor);

            Vector3 defaultColor = new Vector3(0, 0, 0);


            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    MyPoint rayPointer = new MyPoint() { X = topLeft.X + 0, Y = topLeft.Y - i, Z = topLeft.Z + j };

                    (MyPoint IntersectionPoint, Figure nearestFigure) = FindNearestIntersectionPoint(scene, rayPointer);

                    if (nearestFigure == null)
                    {
                        file.WriteLine($"{defaultColor.X} {defaultColor.Y} {defaultColor.Z}");
                        continue;
                    }

                    Vector3 pixel;
                    if (L != null)
                    {
                        double lighting = Lighting(nearestFigure, IntersectionPoint, L);
                        pixel = Vector3.Multiply((float)lighting, rgb);
                    }
                    else
                    {
                        pixel = Vector3.Multiply(1f, rgb);
                    }

                    file.WriteLine($"{pixel.X} {pixel.Y} {pixel.Z}");
                }
            }
        }

        // lab2 part5
        public void Shadows()
        {
            Scene scene = CreateTestingScene();

            //Sphere
            double r = 9;
            MyPoint sphereCenter = new MyPoint() { X = 20, Y = 0, Z = 0 };

            Figure myFigure = new MySphere() { Center = sphereCenter, Radius = r };

            //Triangle
            MyPoint a = new MyPoint(29, -10, 0);
            MyPoint b = new MyPoint(11, -10, 0);
            MyPoint c = new MyPoint(29, -10, 30);

            Figure myFigure1 = new MyTriangle(a, b, c);

            // Add Sphere
            scene.AddFigure(myFigure);
            scene.AddFigure(myFigure1);

            // Our Canvas size
            int height = 20, width = 20;
            MyPoint topLeft = scene.Camera.Plane.GetTopLeftPoint(height, width);

            // Light Vector
            MyVector L = new MyVector(0, 1, 0);
            // MyVector L = null;

            DrawSceneWithShadows(scene, height, width, L);

            Console.WriteLine();
        }


        public void DrawSceneWithShadows(Scene scene, int height, int width, MyVector L)
        {
            MyPoint topLeft = scene.Camera.Plane.GetTopLeftPoint(height, width);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    MyPoint rayPointer = new MyPoint() { X = topLeft.X + 0, Y = topLeft.Y - i, Z = topLeft.Z + j };

                    (var IntersectionPoint, var nearestFigure) = FindNearestIntersectionPoint(scene, rayPointer);

                    if (nearestFigure == null)
                    {
                        Console.Write(" ");
                        continue;
                    }

                    if (L == null)
                    {
                        Console.Write("#");
                        continue;
                    }

                    bool flag = false;

                    MyVector vector = L * (-1);

                    foreach (Figure f in scene.Figures)
                    {
                        MyPoint Intersection = new MyPoint();
                        if (f.RayIntersect(IntersectionPoint, IntersectionPoint + vector, ref Intersection))
                        {
                            flag = true;
                            Console.Write("B");

                            break;
                        }
                    }

                    if (!flag)
                    {
                        double lighting = Lighting(nearestFigure, IntersectionPoint, L);
                        DrawLighting(lighting);
                    }
                }

                Console.WriteLine("|");
            }
        }
    }
}
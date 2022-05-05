using project_true.Figures;
using project_true.Primitives;
using project_true.MyScene;
using System;
using System.Collections.Generic;
using System.Text;
using project_true.Camera;
using System.IO;
using System.Numerics;

namespace project_true.Tracing
{
    public class TracingHandler
    {
        // TODO Intersection point is not necessary?
        /// <summary>
        /// Part 4.5 Implementation
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="intersection"></param>
        /// <param name="L"></param>
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
        /// Lab 1 Part 4 Implementation
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
            MyPoint sphereCenter = new MyPoint() { X = 10, Y = 0, Z = 7 };

            Figure myFigure = new MySphere() { Center = sphereCenter, Radius = r };

            //Triangle
            MyPoint a = new MyPoint(7, 0, -7);
            MyPoint b = new MyPoint(5, 5, -4);
            MyPoint c = new MyPoint(6, 0, 6);

            Figure myFigure1 = new MyTriangle(a, b, c);

            // Add Sphere
            scene.AddFigure(myFigure);
            scene.AddFigure(myFigure1);

            // Our Canvas size
            int height = 20, width = 20;
            MyPoint topLeft = camera.Plane.GetTopLeftPoint(height, width);

            // Light Vector
            MyVector L = new MyVector(1, 0, 0);
            //MyVector L = null;

            //DrawFigure(myFigure, scene, height, width, topLeft, L);

            //DrawScene(scene, height, width, topLeft, L);
            WriteToPPM(scene, height, width, topLeft, L);

            Console.WriteLine();
        }
        
        // lab1,  part5. 
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
            MyPoint topLeft = camera.Plane.GetTopLeftPoint(height, width);

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
        private void DrawFigure(Figure figure, Scene scene, int height, int width, MyPoint topLeft, MyVector L)
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
        private void DrawScene(Scene scene, int height, int width, MyPoint topLeft, MyVector L)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    MyPoint rayPointer = new MyPoint() { X = topLeft.X + 0, Y = topLeft.Y - i, Z = topLeft.Z + j };

                    double minDistance = Double.MaxValue;
                    Figure nearestFigure = null;
                    MyPoint IntersectionPoint = new MyPoint();

                    foreach (Figure f in scene.Figures)
                    {
                        MyPoint Intersection = new MyPoint();
                        if (f.RayIntersect(scene.Camera.Center, rayPointer, ref Intersection))
                            {
                            double dist = MyVector.Length(new MyVector(scene.Camera.Center, IntersectionPoint));
                            if (dist < minDistance)
                            {
                                minDistance = dist;
                                nearestFigure = f;
                                IntersectionPoint = new MyPoint(Intersection);
                            }
                        }
                    }

                    // Point and Camera most likely not working correctly
                    if (nearestFigure != null)
                    {
                        if (L != null)
                        {
                            double lighting = Lighting(nearestFigure, IntersectionPoint, L);
                            DrawLighting(lighting);
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
        private void WriteToPPM(Scene scene, int height, int width, MyPoint topLeft, MyVector L)
        {
            using StreamWriter file = new StreamWriter("car.ppm");
            file.WriteLine("P3");
            file.WriteLine($"{width} {height}");
            // TODO Create variable for max value of each color
            file.WriteLine("255");

            Vector3 rgb = new Vector3(255, 255, 255);

            Vector3 defaultColor = new Vector3(0, 0, 0);


            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    MyPoint rayPointer = new MyPoint() { X = topLeft.X + 0, Y = topLeft.Y - i, Z = topLeft.Z + j };

                    double minDistance = Double.MaxValue;
                    Figure nearestFigure = null;
                    MyPoint IntersectionPoint = new MyPoint();

                    foreach (Figure f in scene.Figures)
                    {
                        MyPoint Intersection = new MyPoint();
                        if (f.RayIntersect(scene.Camera.Center, rayPointer, ref Intersection))
                        {
                            double dist = MyVector.Length(new MyVector(scene.Camera.Center, IntersectionPoint));
                            if (dist < minDistance)
                            {
                                minDistance = dist;
                                nearestFigure = f;
                                IntersectionPoint = new MyPoint(Intersection);
                            }
                        }
                    }

                    // Point and Camera most likely not working correctly
                    if (nearestFigure != null)
                    {
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
                    else
                    {
                        file.WriteLine($"{defaultColor.X} {defaultColor.Y} {defaultColor.Z}");
                    }
                }
            }
        }

    }
}

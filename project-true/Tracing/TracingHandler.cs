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

        public (MyPoint, Figure) FindNearestIntersectionPoint(Scene scene, 
                                                              MyPoint origin, 
                                                              MyPoint rayPointer)
        {
            double minDistance = Double.MaxValue;
            Figure nearestFigure = null;
            MyPoint IntersectionPoint = null;

            foreach (Figure f in scene.Figures)
            {
                MyPoint Intersection = new MyPoint();
                if (f.RayIntersect(origin, rayPointer, ref Intersection))
                {
                    double dist = MyVector.Length(new MyVector(origin, Intersection));
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
            MyVector normal = figure.GetNormal(intersection);
            double dot = MyVector.Dot(normal, L);
            return dot;
        }

        private void DrawLighting(double lighting)
        {
            if (lighting <= 0)
            {
                Console.Write(" ");
            }
            else if (lighting > 0 && lighting < 0.2)
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

        // lab2 part1
        public void WriteToPPM(ScreenState screenState, int maxColor, string outPutFile)
        {

            using StreamWriter file = new StreamWriter(outPutFile);
            
            //  "P3" means this is a RGB color image in ASCII
            file.WriteLine("P3");
            
            file.WriteLine($"{screenState.Width} {screenState.Height}");
            file.WriteLine(maxColor);

            Vector3 rgb = new Vector3(maxColor, maxColor, maxColor);

            for (int i = 0; i < screenState.Height; i++)
            {
                for (int j = 0; j < screenState.Width; j++)
                {                    
                    Vector3 pixel;
                    pixel = Vector3.Multiply((float)screenState[i, j], rgb);

                    file.WriteLine($"{pixel.X} {pixel.Y} {pixel.Z}");
                }
                Console.SetCursorPosition(0, Math.Max(Console.CursorTop - 1, 0));
                ClearCurrentConsoleLine();
                Console.WriteLine($"{i + 1}/{screenState.Height} lines");
            }
        }

        public void DrawScene(ScreenState screenState)
        {
            for (int i = 0; i < screenState.Height; i++)
            {
                for (int j = 0; j < screenState.Width; j++)
                {
                    DrawLighting(screenState[i, j]);
                }
                Console.WriteLine("|");
            }
        }

        public ScreenState Tracing(Scene scene, int height, int width, MyVector L = null)
        {
            ScreenState screenState = new ScreenState(height, width);
            MyPoint topLeft = scene.Camera.Plane.GetTopLeftPoint(height, width);

            MyPoint rayPointer = new MyPoint();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    rayPointer.X = topLeft.X + 0;
                    rayPointer.Y = topLeft.Y - i;
                    rayPointer.Z = topLeft.Z + j;

                    (var IntersectionPoint, var nearestFigure) =
                        FindNearestIntersectionPoint(scene, scene.Camera.Center, rayPointer);

                    if (nearestFigure == null)
                    {
                        screenState[i, j] = 0;
                        continue;
                    }

                    if (L == null)
                    {
                        screenState[i, j] = 1;
                        continue;
                    }

                    double lighting = Lighting(nearestFigure, IntersectionPoint, L);

                    foreach (Figure f in scene.Figures)
                    {
                        MyPoint Intersection = new MyPoint();
                        if (f.RayIntersect(IntersectionPoint, IntersectionPoint + L, ref Intersection))
                        {
                            lighting = 0;
                            break;
                        }
                    }

                    screenState[i, j] = lighting;
                }
                Console.SetCursorPosition(0, Math.Max(Console.CursorTop - 1, 0));
                ClearCurrentConsoleLine();
                Console.WriteLine($"{i + 1}/{height} lines");
            }
            return screenState;
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
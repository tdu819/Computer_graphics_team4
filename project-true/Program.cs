using project_true.Figures;
using System;
using project_true.Primitives;

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

                    var o = cameraCenter;
                    var c = sphereCenter;
                    var k1 = o - c;
                    MyVector k = new MyVector(k1.X, k1.Y, k1.Z);

                    var d = new MyVector(cameraCenter, point);

                    var d2 = MyVector.Dot(d, d);
                    var r2 = r * r;
                    var k2 = MyVector.Dot(k, k);

                    var a = d2;
                    var b = 2 * MyVector.Dot(d, k);
                    var cc = k2 - r2;

                    var D = b * b - 4 * a * cc;
                    if (D < 0 || (a == 0 && b == 0 && cc != 0)) // if cc == 0 will be strange situation.
                    {
                        point.Value = " ";
                    }
                    else
                    {
                        double t1 = 0;
                        double t2 = 0;
                        if (a == 0 && b != 0)
                        {
                            t1 = -(cc / b);
                            t1 = t2;
                        }
                        else if (a != 0)
                        {
                            t1 = (-b + Math.Sqrt(D)) / (2 * a);
                            t2 = (-b - Math.Sqrt(D)) / (2 * a);
                        }

                        if (t1 < 0 && t2 < 0)
                        {
                            point.Value = " ";
                        }
                        else
                        {
                            point.Value = "#";
                        }
                    }

                    Console.Write(point.Value);
                }

                Console.WriteLine("|");
            }

            Console.WriteLine();
        }
    }
}
using project_true.Figures;
using System;
using System.Numerics;
using project_true.Primitives;
using project_true.Tracing;

namespace project_true
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var someMatrix = new Matrix4x4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

            MyPoint a = new MyPoint(15, 25, -50);
            MyPoint b = new MyPoint(15, 25, 20);
            MyPoint c = new MyPoint(15, -30, 20);
            MyTriangle triangle = new MyTriangle(a, b, c);

            float x = 5, y = -5, z = 80;
            MyTriangle newTriangle = triangle.Move(x, y, z);

            // obj handler lab 2 part 3.
            // string path = "koenigsegg.obj";
            //
            // ObjHandler objHandler = new ObjHandler();
            // var result = objHandler.ReadObjFile(path);

            //
            Console.WriteLine("hi");
            // TracingHandler handler = new TracingHandler();
            // handler.FigureTracing();


            // Console.ReadLine();
            // Console.Clear();

            //handler.NearestFigureTracing();

            // Console.ReadLine();
        }
    }
}
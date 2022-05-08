using project_true.Figures;
using System;
using System.Numerics;
using project_true.Primitives;
using project_true.Tracing;
using project_true.MyScene;
using project_true.Camera;
using project_true.Matrixes;

namespace project_true
{
    public static class Program
    {
        static void Main(string[] args)
        {
            TracingHandler handler = new TracingHandler();
            Matrix4x4 I = Matrix4x4.Identity;

            Matrix4x4 rotation = new Matrix4x4().CreateRotateMatrix(0, 180, 0);
            Matrix4x4 translate = new Matrix4x4().CreateTranslationMatrix(30, 0, 0);
            Matrix4x4 RT = Matrix4x4.Multiply(translate, rotation);

            MyCamera camera = new MyCamera(new MyPoint(0, 0, 0), new MyVector(1, 0, 0), 10);
            Scene scene = new Scene(camera);


            MyPoint a = new MyPoint(15, 0, -50);
            MyPoint b = new MyPoint(15, 25, 20);
            MyPoint c = new MyPoint(15, 0, 20);
            MyTriangle triangle = new MyTriangle(a, b, c).ScaleRotateMove(RT);

            scene.AddFigure(triangle);

            MyPoint topLeft = camera.Plane.GetTopLeftPoint(40, 40);
            handler.DrawScene(scene, 40, 40, topLeft, null);

            // obj handler lab 2 part 3.
            // string path = "koenigsegg.obj";
            //
            // ObjHandler objHandler = new ObjHandler();
            // var result = objHandler.ReadObjFile(path);

            //
            //Console.WriteLine("hi");
            // TracingHandler handler = new TracingHandler();
            // handler.FigureTracing();


            // Console.ReadLine();
            // Console.Clear();

            //handler.NearestFigureTracing();

            Console.ReadLine();
        }
    }
}
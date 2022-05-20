using project_true.Figures;
using System;
using System.IO;
using System.Numerics;
using project_true.Primitives;
using project_true.Tracing;
using project_true.MyScene;
using project_true.Camera;
using project_true.Matrixes;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace project_true
{
    public static class Program
    {
        [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: project_true.Primitives.MyVector")]
        static void Main(string[] args)
        {
            // lab2 part 0
            string sourceFile = null;
            string outputFile = null;

            foreach (string a in args)
            {
                if(a.StartsWith("--source=") && sourceFile == null)
                {
                    sourceFile = a.Substring(9);
                }
                if(a.StartsWith("--output=") && outputFile == null)
                {
                    outputFile = a.Substring(9);
                }
            }

            if (!File.Exists(sourceFile))
            {
                Console.WriteLine("There is no such file");
                Console.ReadLine();
                return;
            }

            Matrix4x4 rotation = new Matrix4x4().CreateRotateMatrix(0, 90, 0);
            Matrix4x4 translate = new Matrix4x4().CreateTranslationMatrix(0, 0, 0);
            Matrix4x4 scale = new Matrix4x4().CreateScaleMatrix(1, 1, 2f);

            Matrix4x4 RT = Matrix4x4.Multiply(translate, rotation);


            // Matrix4x4 SRT = Matrix4x4.Multiply(rotation, scale);
            Matrix4x4 SRT = Matrix4x4.Multiply(scale, rotation);
            SRT = Matrix4x4.Multiply(translate, SRT ); 
            

            ObjHandler objHandler = new ObjHandler();
            TracingHandler tracingHandler = new TracingHandler();
            List<MyObject> objects = objHandler.ReadObjFile(sourceFile);
            
            Scene scene = tracingHandler.CreateTestingScene();
            foreach (MyObject obj in objects)
            {
                scene.Figures.AddRange(obj.Triangles.Select(t => t.ScaleRotateMove(SRT)));
            }
                        

            tracingHandler.DrawSceneWithShadows(scene, 45, 100, new MyVector(0, 1, 0));
            
            tracingHandler.WriteToPPM(scene, 45, 100, new MyVector(0, 1, 0), 255, outputFile);
             

            //  hack lab2 part5
            // TracingHandler tracingHandler = new TracingHandler();
            // tracingHandler.Shadows();

            // lab2 part4
            // TracingHandler handler = new TracingHandler();
            // Matrix4x4 I = Matrix4x4.Identity;
            //
            // Matrix4x4 rotation = new Matrix4x4().CreateRotateMatrix(0, 180, 0);
            // Matrix4x4 translate = new Matrix4x4().CreateTranslationMatrix(30, 0, 0);
            // Matrix4x4 scale = new Matrix4x4().CreateScaleMatrix(0.5f, 0.5f, 0.5f);
            //
            // Matrix4x4 RT = Matrix4x4.Multiply(translate, rotation); 
            //
            //
            // Matrix4x4 SRT = Matrix4x4.Multiply(rotation, scale);
            // SRT = Matrix4x4.Multiply(translate, SRT);
            //
            // MyCamera camera = new MyCamera(new MyPoint(0, 0, 0), new MyVector(1, 0, 0), 10);
            // Scene scene = new Scene(camera);
            //
            //
            // MyPoint a = new MyPoint(15, 0, -50);
            // MyPoint b = new MyPoint(15, 25, 20);
            // MyPoint c = new MyPoint(15, 0, 20);
            // MyTriangle triangle = new MyTriangle(a, b, c).ScaleRotateMove(SRT);
            //
            // scene.AddFigure(triangle);
            //
            // MyPoint topLeft = camera.Plane.GetTopLeftPoint(40, 40);
            // handler.DrawScene(scene, 40, 40, topLeft, null);


            // 

            // lab 2 part 3. (obj handler)
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

            // Console.ReadLine();
        }
    }
}
using project_true.Figures;
using System;
using project_true.Primitives;
using project_true.Tracing;

namespace project_true
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string path = "koenigsegg.obj";
            
            ObjHandler objHandler = new ObjHandler();
            var result = objHandler.ReadObjFile(path);


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
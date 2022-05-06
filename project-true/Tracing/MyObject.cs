using System.Collections.Generic;
using project_true.Figures;
using project_true.Primitives;

namespace project_true.Tracing
{
    public class MyObject
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public List<MyPoint> Points { get; set; } = new List<MyPoint>();
        public List<MyVector> Normals { get; set; } = new List<MyVector>();
        public List<MyTriangle> Triangles { get; set; } = new List<MyTriangle>();

        public MyObject(string name)
        {
            Name = name;
        }
    }
}
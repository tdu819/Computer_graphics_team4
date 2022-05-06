using System.Collections.Generic;
using System.IO;
using project_true.Figures;
using project_true.Primitives;

namespace project_true.Tracing
{
    public class ObjHandler
    {
        public List<MyObject> ReadObjFile(string path)
        {
            MyObject myObject = null;
            List<MyObject> myObjects = new List<MyObject>();

            using StreamReader file = new StreamReader(path);
            string line = string.Empty;

            var array = new string[4];

            int counter = 0;

            while ((line = file.ReadLine()) != null)
            {
                counter++;
                if (line[0] == '#') continue;

                array = line.Split(' ');

                switch (array[0])
                {
                    case "o":
                    {
                        myObject = new MyObject(array[1]);
                        myObjects.Add(myObject);

                        break;
                    }
                    case "v":
                    {
                        double x = double.Parse(array[1]);
                        double y = double.Parse(array[2]);
                        double z = double.Parse(array[3]);
                        myObject?.Points.Add(new MyPoint(x, y, z));

                        break;
                    }

                    case "vn":
                    {
                        double x = double.Parse(array[1]);
                        double y = double.Parse(array[2]);
                        double z = double.Parse(array[3]);
                        myObject?.Normals.Add(new MyVector(x, y, z).Normalization());

                        break;
                    }

                    case "f":
                    {
                        string[] a = array[1].Split('/');
                        string[] b = array[2].Split('/');
                        string[] c = array[3].Split('/');


                        MyPoint pointA = myObject?.Points[int.Parse(a[0]) + 1];
                        pointA.Normal = myObject?.Normals[int.Parse(a[2]) + 1];

                        MyPoint pointB = myObject?.Points[int.Parse(b[0]) + 1];
                        pointB.Normal = myObject?.Normals[int.Parse(b[2]) + 1];

                        MyPoint pointC = myObject?.Points[int.Parse(c[0]) + 1];
                        pointC.Normal = myObject?.Normals[int.Parse(c[2]) + 1];

                        var triangle = new MyTriangle(pointA, pointB, pointC);
                        myObject?.Triangles.Add(triangle);

                        break;
                    }
                }
            }

            return myObjects;
        }
    }
}
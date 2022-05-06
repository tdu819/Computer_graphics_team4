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
            MyObject myObject = new MyObject("Name");
            List<MyObject> myObjects = new List<MyObject>();

            using StreamReader file = new StreamReader(path);
            string line;

            var array = new string[5];

            int counter = 0;

            while ((line = file.ReadLine()) != null)
            {
                counter++;
                if (line[0] == '#') continue;

                array = line.Split(' ');

                switch (array[0])
                {
                    // case "o":
                    // {
                    //     myObject = new MyObject(array[1]);
                    //     myObjects.Add(myObject);
                    //
                    //     break;
                    // }
                    case "v":
                    {
                        double x = double.Parse(array[1], System.Globalization.CultureInfo.InvariantCulture);
                        double y = double.Parse(array[2], System.Globalization.CultureInfo.InvariantCulture);
                        double z = double.Parse(array[3], System.Globalization.CultureInfo.InvariantCulture);
                        myObject?.Points.Add(new MyPoint(x, y, z));

                        break;
                    }

                    case "vn":
                    {
                        double x = double.Parse(array[1], System.Globalization.CultureInfo.InvariantCulture);
                        double y = double.Parse(array[2], System.Globalization.CultureInfo.InvariantCulture);
                        double z = double.Parse(array[3], System.Globalization.CultureInfo.InvariantCulture);
                        myObject?.Normals.Add(new MyVector(x, y, z).Normalization());

                        break;
                    }
                }
            }

            file.Close();

            using StreamReader file2 = new StreamReader(path);


            var counter2 = 0;
            
            while ((line = file2.ReadLine()) != null)
            {
                counter2++;
                array = line.Split(' ');


                if (array[0] == "f")
                {
                    
                    if (array.Length > 4)
                    {
                        continue;
                    }
                    
                    string[] a = array[1].Split('/');
                    string[] b = array[2].Split('/');
                    string[] c = array[3].Split('/');


                    MyPoint pointA =
                        myObject?.Points[int.Parse(a[0], System.Globalization.CultureInfo.InvariantCulture)];
                    pointA.Normal = myObject?.Normals[int.Parse(a[2])];

                    MyPoint pointB =
                        myObject?.Points[int.Parse(b[0], System.Globalization.CultureInfo.InvariantCulture)];
                    pointB.Normal =
                        myObject?.Normals[int.Parse(b[2], System.Globalization.CultureInfo.InvariantCulture)];

                    MyPoint pointC =
                        myObject?.Points[int.Parse(c[0], System.Globalization.CultureInfo.InvariantCulture)];
                    pointC.Normal =
                        myObject?.Normals[int.Parse(c[2], System.Globalization.CultureInfo.InvariantCulture)];

                    var triangle = new MyTriangle(pointA, pointB, pointC);
                    myObject?.Triangles.Add(triangle);
                }
            }

            myObjects.Add(myObject);
            return myObjects;
        }
    }
}
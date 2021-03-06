using System.Collections.Generic;
using System.IO;
using System.Linq;
using project_true.Figures;
using project_true.Primitives;

namespace project_true.Tracing
{
    public class ObjHandler
    {
        /// <summary>
        /// Parse Obj file and returns list of objects with its list of triangles
        /// </summary>
        /// <param name="path"> Path to file for parsing </param>
        public List<MyObject> ReadObjFile(string path)
        {
            // Object initialization
            MyObject myObject = null;
            List<MyObject> myObjects = new List<MyObject>();
            // Number of f lines per object
            List<int> fLineCounter = new List<int>();

            using StreamReader file = new StreamReader(path);
            string line;

            string[] splittedLine;

            // Lists of points and normals, first element is null
            List<MyPoint> points = new List<MyPoint>() { null };
            List<MyVector> normals = new List<MyVector>() { null };

            // List of splitted strings with indexes of points and normals
            List<List<string>> figures = new List<List<string>>();

            // Counter for f string per object
            int figuresCount = 0;

            while ((line = file.ReadLine()) != null)
            {
                // For comment lines
                if (line[0] == '#') continue;

                splittedLine = line.Split(' ');

                switch (splittedLine[0])
                {
                    case "o":
                        {
                            if (myObject != null)
                            {
                                fLineCounter.Add(figuresCount);
                            }

                            myObject = new MyObject(splittedLine[1]);
                            myObjects.Add(myObject);

                            figuresCount = 0;
                            break;
                        }
                    case "v":
                        {
                            double x = double.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture);
                            double y = double.Parse(splittedLine[2], System.Globalization.CultureInfo.InvariantCulture);
                            double z = double.Parse(splittedLine[3], System.Globalization.CultureInfo.InvariantCulture);
                            
                            if (myObject != null)
                            {
                                points.Add(new MyPoint(x, y, z));
                            }

                            break;
                        }

                    case "vn":
                        {
                            double x = double.Parse(splittedLine[1], System.Globalization.CultureInfo.InvariantCulture);
                            double y = double.Parse(splittedLine[2], System.Globalization.CultureInfo.InvariantCulture);
                            double z = double.Parse(splittedLine[3], System.Globalization.CultureInfo.InvariantCulture);
                            if (myObject != null)
                            {
                                normals.Add(new MyVector(x, y, z).Normalization());
                            }
                            break;
                        }
                    case "f":
                        {
                            if (myObject != null)
                            {
                                List<string> fString = splittedLine.Skip(1).ToList();
                                figures.Add(fString);
                                figuresCount++;
                            }
                            break;
                        }
                }
            }
            if (myObject != null)
            {
                fLineCounter.Add(figuresCount);
            }

            file.Close();

            for (int i = 0; i < myObjects.Count; i++)
            {
                MyObject obj = myObjects[i];
                figuresCount = fLineCounter[i];

                int position = 0;

                for (int j = position; j < figuresCount; j++, position++)
                {
                    List<string> fLine = figures[j];

                    // goes through all vertexes in line with "f" start symbol
                    for (int p1 = 0; p1 < fLine.Count - 2; p1++)
                    {
                        for (int p2 = p1 + 1; p2 < fLine.Count - 1; p2++)
                        {
                            for (int p3 = p2 + 1; p3 < fLine.Count; p3++)
                            {
                                // indexes of points and normals for chosen vertexes
                                string[] v1 = fLine[p1].Split('/');
                                string[] v2 = fLine[p2].Split('/');
                                string[] v3 = fLine[p3].Split('/');

                                MyPoint a = points[int.Parse(v1[0])];
                                if (v1.Length >= 3)
                                {
                                    a.Normal = normals[int.Parse(v1[2])];
                                }

                                MyPoint b = points[int.Parse(v2[0])];
                                if (v2.Length >= 3)
                                {
                                    b.Normal = normals[int.Parse(v2[2])];
                                }
                                
                                MyPoint c = points[int.Parse(v3[0])];
                                if (v3.Length >= 3)
                                {
                                    c.Normal = normals[int.Parse(v3[2])];
                                }
                                

                                MyTriangle triangle = new MyTriangle(a, b, c);
                                obj.Triangles.Add(triangle);
                            }
                        }
                    }
                }
            }
            return myObjects;
        }
    }
}
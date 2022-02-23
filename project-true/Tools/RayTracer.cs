using project_true.Figures;
using project_true.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_true.Tools
{
    public static class RayTracer
    {
        
        public static bool RayIntersectsTriangle(MyPoint rayOrigin, 
                                                 MyVector rayVector, 
                                                 MyTriangle inTriangle, 
                                                 ref MyPoint outIntersectionPoint)
        {
            const double EPSILON = 0.0000001;
            MyPoint vertex0 = inTriangle.A;
            MyPoint vertex1 = inTriangle.B;
            MyPoint vertex2 = inTriangle.C;

            MyVector edge1 = new MyVector();
            MyVector edge2 = new MyVector();
            MyVector h = new MyVector();
            MyVector s = new MyVector();
            MyVector q = new MyVector();

            double a, f, u, v;

            edge1 = new MyVector(vertex1, vertex0);
            edge2 = new MyVector(vertex2, vertex0);
            h = MyVector.Cross(rayVector, edge2);
            a = MyVector.Dot(edge1, h);
            if (a > -EPSILON && a < EPSILON)
            {
                return false;    // This ray is parallel to this triangle.
            }
            f = 1.0 / a;
            s = new MyVector(rayOrigin, vertex0);
            u = f * (MyVector.Dot(s, h));
            if (u < 0.0 || u > 1.0)
            {
                return false;
            }
            q = MyVector.Cross(s, edge1);
            v = f * MyVector.Dot(rayVector, q);
            if (v < 0.0 || u + v > 1.0)
            {
                return false;
            }
            // At this stage we can compute t to find out where the intersection point is on the line.
            double t = f * MyVector.Dot(edge2, q);
            if (t > EPSILON) // ray intersection
            {
                outIntersectionPoint = new MyPoint(0.0, 0.0, 0.0);
                
                outIntersectionPoint = rayOrigin + rayVector * t;
                return true;
            }
            else // This means that there is a line intersection but not a ray intersection.
            {
                return false;
            }
        }
    }
}

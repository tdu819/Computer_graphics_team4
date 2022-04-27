/*using project_true.Figures;
using project_true.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_true.Tools
{
    public static class RayTracer
    {
        public static bool RayIntersectsSphere(MyPoint rayOrigin,
            MySphere sphere,
            MyPoint point,
            ref MyPoint intersectPoint)
        {
            var o = rayOrigin;
            var c = sphere.Center;
            var r = sphere.Radius;
            var k1 = o - c;
            MyVector k = new MyVector(k1.X, k1.Y, k1.Z);

            var d = new MyVector(rayOrigin, point);

            var d2 = MyVector.Dot(d, d);
            var r2 = r * r;
            var k2 = MyVector.Dot(k, k);

            var a = d2;
            var b = 2 * MyVector.Dot(d, k);
            var cc = k2 - r2;

            double t1, t2;

            if (a == 0)
            {
                if (b == 0)
                {
                    return false;
                }
                else
                {
                    t1 = -cc / b;
                    t2 = t1;
                }
            }
            else
            {
                var D = b * b - 4 * a * cc;
                if (D < 0)
                {
                    return false;
                }
                else
                {
                    t1 = (-b + Math.Sqrt(D)) / (2 * a);
                    t2 = (-b - Math.Sqrt(D)) / (2 * a);
                }
            }

            if (t1 < 0 && t2 < 0)
            {
                return false;
            }
            else if (t2 < 0)
            {
                intersectPoint = new MyPoint(d * t1);
                return true;
            }
            else
            {
                intersectPoint = new MyPoint(d * Math.Min(t1, t2));
                return true;
            }
        }

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
                return false; // This ray is parallel to this triangle.
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
}*/
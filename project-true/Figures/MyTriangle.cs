using project_true.Primitives;
using System.Drawing;

namespace project_true.Figures
{
    public class MyTriangle : Figure
    {
        public MyPoint A { get; set; }
        public MyPoint B { get; set; }
        public MyPoint C { get; set; }

        public MyTriangle()
        {
            
        }
        
        public MyTriangle(MyPoint a, MyPoint b, MyPoint c)
        {
            A = a;
            B = b;
            C = c;
        }

        // lab1 part3
        public override bool RayIntersect(MyPoint rayOrigin, 
                                          MyPoint rayPointer, 
                                          ref MyPoint IntersectionPoint)
        {
            const double EPSILON = 0.0000001;
            MyPoint vertex0 = this.A;
            MyPoint vertex1 = this.B;
            MyPoint vertex2 = this.C;

            MyVector rayVector = new MyVector(rayOrigin, rayPointer);

            MyVector edge1 = new MyVector();
            MyVector edge2 = new MyVector();
            MyVector h = new MyVector();
            MyVector s = new MyVector();
            MyVector q = new MyVector();

            double a, f, u, v;

            edge1 = new MyVector(vertex1 - vertex0);
            edge2 = new MyVector(vertex2 - vertex0);
            h = MyVector.Cross(rayVector, edge2);
            a = MyVector.Dot(edge1, h);
            if (a > -EPSILON && a < EPSILON)
            {
                return false; // This ray is parallel to this triangle.
            }

            f = 1.0 / a;
            s = new MyVector(rayOrigin - vertex0);
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
                IntersectionPoint = new MyPoint(0.0, 0.0, 0.0);

                IntersectionPoint = rayOrigin + (rayVector * t);
                return true;
            }
            else // This means that there is a line intersection but not a ray intersection.
            {
                return false;
            }
        }

        public override MyVector GetNormal(MyPoint intersectionPoint)
        {
            MyVector sideA = new MyVector(B - A);
            MyVector sideB = new MyVector(C - A);
            return MyVector.Cross(sideA, sideB).Normalization();
        }
        
        
        public override bool Equals(object? obj)
        {
            return this.A == ((MyTriangle)obj).A &&
                   this.B == ((MyTriangle)obj).B &&
                   this.C == ((MyTriangle)obj).C;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
using project_true.Primitives;
using System;

namespace project_true.Figures
{
    public class MySphere : Figure
    {
        public MyPoint Center { get; set; }
        
        public double Radius { get; set; }

        public override MyVector GetNormal(MyPoint intersectionPoint)
        {
            MyVector normal = new MyVector(Center, intersectionPoint).Normalization();
            return normal;
        }

        public override bool RayIntersect(MyPoint rayOrigin, 
                                          MyPoint rayPointer, 
                                          ref MyPoint IntersectionPoint)
        {
            var o = rayOrigin;
            var c = this.Center;
            var r = this.Radius;
            var k1 = o - c;
            MyVector k = new MyVector(k1.X, k1.Y, k1.Z);

            var d = new MyVector(rayOrigin, rayPointer);

            var d2 = MyVector.Dot(d, d);
            var r2 = r * r;
            var k2 = MyVector.Dot(k, k);

            var a = d2;
            var b = 2 * MyVector.Dot(d, k);
            var cc = Math.Round(k2 - r2, 6);

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

            if (t1 <= 0 && t2 <= 0)
            {
                return false;
            }
            // TODO Question: t == 0? + one t == 0 and other t > 0?
            else if (t2 <= 0)
            {
                IntersectionPoint = new MyPoint(d * t1);
                return true;
            }
            else
            {
                IntersectionPoint = new MyPoint(d * Math.Min(t1, t2));
                return true;
            }
        }
        
        public override bool Equals(object? obj)
        {
            return this.Center == ((MySphere)obj).Center &&
                   this.Radius == ((MySphere)obj).Radius;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
    }
}
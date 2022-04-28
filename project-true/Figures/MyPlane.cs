using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using project_true.Primitives;

namespace project_true.Figures
{
    // A(x−a)+B(y−b)+C(z−c) = 0
    public class MyPlane : Figure
    {
        MyPoint _center;
        MyVector _normal;

        public MyPlane(MyPoint center, MyVector vector)
        {
            _center = center;
            _normal = vector;
        }


        // todo check it. vector normal of the plate. expereimental feature.
        public MyPoint GetTopLeftPoint(double distanceX, double distanceY, double distanceZ)
        {
            return new MyPoint()
            {
                X = _center.X + (distanceX * _normal.X), Y = _center.Y + (distanceY * _normal.Y),
                Z = _center.Z + (distanceZ * _normal.Z)
            };
        }

        public override bool RayIntersect(MyPoint rayOrigin, MyPoint rayPointer, ref MyPoint IntersectionPoint)
        {
            throw new NotImplementedException();
            
            // float denom = normal.dot(ray.direction);
            // if (abs(denom) > 0.0001f) // your favorite epsilon
            // {
            //     float t = (center - ray.origin).dot(normal) / denom;
            //     if (t >= 0) return true; // you might want to allow an epsilon here too
            // }
            // return false;
        }

        public override MyVector GetNormal(MyPoint intersectionPoint)
        {
            return _normal;
        }
    }
}
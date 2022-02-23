using System;
using System.Collections.Generic;
using System.Text;
using project_true.Primitives;

namespace project_true.Figures
{
    // A(x−a)+B(y−b)+C(z−c) = 0
    public class MyPlane
    {
        MyPoint _center;
        MyVector _normal;

        public MyPlane(MyPoint center, MyVector vector)
        {
            _center = center;
            _normal = vector;
        }


        public MyPoint GetTopLeftPoint(double distanceX, double distanceY, double distanceZ)
        {
            return new MyPoint() { X = _center.X + distanceX, Y = _center.Y + distanceY, Z = _center.Z + distanceZ };
        }
    }
}
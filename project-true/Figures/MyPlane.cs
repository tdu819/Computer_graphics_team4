using project_true.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}

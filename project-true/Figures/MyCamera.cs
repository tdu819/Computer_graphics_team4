using System;
using System.Collections.Generic;
using System.Text;
using project_true.Primitives;

namespace project_true.Figures
{
    public class MyCamera
    {
        MyPoint _center;
        MyVector _direction;

        public MyCamera(MyPoint center, MyVector vector)
        {
            _center = center;
            _direction = vector;
        }
    }
}
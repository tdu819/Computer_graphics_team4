using System;
using System.Collections.Generic;
using System.Text;
using project_true.Primitives;

namespace project_true.Figures
{
    public class MyCamera
    {
        MyPoint _center { get; }
        MyVector _direction { get; }
        double _distance { get; }
        MyPlane _plane { get; }

        public MyCamera(MyPoint center, MyVector vector, double distance)
        {
            _center = center;
            _direction = vector;
            _distance = distance;
            _plane = CreateCameraPlane();
        }

        private MyPlane CreateCameraPlane()
        {
            MyPoint planeCenter = new MyPoint() 
            { 
                X = _distance, 
                Y = _center.Y, 
                Z = _center.Z 
            };
            MyPlane plane = new MyPlane(planeCenter, _direction.Normalization());
            return plane;
        }
    }
}
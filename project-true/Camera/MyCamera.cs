using System;
using System.Collections.Generic;
using System.Text;
using project_true.Figures;
using project_true.Primitives;

namespace project_true.Camera
{
    public class MyCamera
    {
        public MyPoint Center { get; set; }
        public MyVector Direction { get; set; }
        private double _distance;
        public double Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                CreateCameraPlane();
            }
        }
        private MyPlane _plane;
        public MyPlane Plane { get { return _plane; } }

        public MyCamera(MyPoint center, MyVector vector, double distance)
        {
            Center = center;
            Direction = vector;
            Distance = distance;
            CreateCameraPlane();
        }

        private void CreateCameraPlane()
        {
            MyPoint planeCenter = new MyPoint() 
            { 
                X = _distance * Direction.X, 
                Y = _distance * Direction.Y, 
                Z = _distance * Direction.Z 
            };
            MyPlane plane = new MyPlane(planeCenter, Direction.Normalization());
            _plane = plane;
        }
    }
}
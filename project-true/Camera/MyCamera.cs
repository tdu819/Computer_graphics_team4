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
            MyVector xAxis = new MyVector(1, 0, 0);
            MyVector yAxis = new MyVector(0, 1, 0);
            var xzAngle = Math.Acos(MyVector.Dot(xAxis, Direction) / (MyVector.Length(Direction)));
            var xyAngle = Math.Acos(MyVector.Dot(yAxis, Direction) / (MyVector.Length(Direction)));

            var xzCos = Math.Round(Math.Cos(xzAngle), 5);
            var xzSin = Math.Round(Math.Sin(xzAngle), 5);
            var xySin = Math.Round(Math.Sin(xyAngle), 5);
            var xyCos = Math.Round(Math.Cos(xyAngle), 5);

            MyPoint planeCenter = new MyPoint() 
            { 
                X = Center.X + (_distance * xzCos * xySin), 
                Y = Center.Y + (_distance * xyCos),
                Z = Center.Z + (_distance * xzSin * xySin)
                /*X = _distance,
                Y = Center.Y,
                Z = Center.Z*/
            };
            MyPlane plane = new MyPlane(planeCenter, Direction.Normalization());
            _plane = plane;
        }
    }
}
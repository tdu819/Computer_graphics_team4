using System;
using System.Numerics;

namespace project_true.Primitives
{
    public class MyVector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public MyVector()
        {
        }

        public MyVector(MyPoint first, MyPoint second)
        {
            X = second.X - first.X;
            Y = second.Y - first.Y;
            Z = second.Z - first.Z;
        }

        public MyVector(double v1, double v2, double v3)
        {
            this.X = v1;
            this.Y = v2;
            this.Z = v3;
        }

        public static double Dot(MyVector vector1, MyVector vector2)
        {
            return vector1.X * vector2.X +
                   vector1.Y * vector2.Y +
                   vector1.Z * vector2.Z;
        }

        public static MyVector Abs(MyVector value)
        {
            return new MyVector(Math.Abs(value.X), Math.Abs(value.Y), Math.Abs(value.Z));
        }

        public static MyVector operator +(MyVector left, MyVector right)
        {
            return new MyVector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static MyVector operator -(MyVector left, MyVector right)
        {
            return new MyVector(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public bool Equals(MyVector other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Z == other.Z;
        }

        public static double Length(MyVector vector)
        {
            return Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2) + Math.Pow(vector.Z, 2));
        }
    }
}
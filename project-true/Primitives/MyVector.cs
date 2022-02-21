using System;
using System.Numerics;

namespace project_true.Primitives
{
    public class MyVector
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public static float Dot(MyVector vector1, MyVector vector2)
        {
            return vector1.X * vector2.X +
                   vector1.Y * vector2.Y +
                   vector1.Z * vector2.Z;
        }

        public static Vector3 Abs(Vector3 value)
        {
            return new Vector3(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z));
        }

        public static Vector3 operator +(MyVector left, MyVector right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector3 operator -(MyVector left, MyVector right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
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
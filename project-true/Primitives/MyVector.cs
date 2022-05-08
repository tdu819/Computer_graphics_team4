using System;
using System.Numerics;
using project_true.Matrixes;

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

        public MyVector(MyPoint point)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;
        }

        public MyVector(double v1, double v2, double v3)
        {
            this.X = v1;
            this.Y = v2;
            this.Z = v3;
        }

        public MyVector Move(float x, float y, float z)
        {
            return (MyVector)this.MemberwiseClone();
        }

        public MyVector Scale(float x, float y, float z)
        {
            Matrix4x4 scaleMatrix = new Matrix4x4().CreateScaleMatrix(x, y, z);

            Vector4 vector = new Vector4((float)this.X, (float)this.Y, (float)this.Z, 1f);
            Vector4 res = scaleMatrix.MultiplyMatrix4x4ByVector(vector);

            MyVector result = new MyVector(res.X / res.W, res.Y / res.W, res.Z / res.W);

            return result;
        }

        public MyVector Rotate(Matrix4x4 matrix)
        {
            Vector4 vector = new Vector4((float)this.X, (float)this.Y, (float)this.Z, 1f);
            Vector4 res = matrix.MultiplyMatrix4x4ByVector(vector);

            MyVector result = new MyVector(res.X / res.W, res.Y / res.W, res.Z / res.W);

            return result;
        }
        
        public MyVector ScaleRotateMove(Matrix4x4 matrix)
        {
            Vector4 vector = new Vector4((float)this.X, (float)this.Y, (float)this.Z, 1f);
            Vector4 res = matrix.MultiplyMatrix4x4ByVector(vector);

            MyVector result = new MyVector(res.X / res.W, res.Y / res.W, res.Z / res.W);

            return result;
        }

        public static double Dot(MyVector vector1, MyVector vector2)
        {
            return vector1.X * vector2.X +
                   vector1.Y * vector2.Y +
                   vector1.Z * vector2.Z;
        }

        public static MyVector Cross(MyVector v1, MyVector v2)
        {
            double X = v1.Y * v2.Z - v1.Z * v2.Y;
            double Y = v1.Z * v2.X - v1.X * v2.Z;
            double Z = v1.X * v2.Y - v1.Y * v2.X;
            return new MyVector(X, Y, Z);
        }


        public static MyVector Abs(MyVector value)
        {
            return new MyVector(Math.Abs(value.X), Math.Abs(value.Y), Math.Abs(value.Z));
        }

        public static MyVector operator +(MyVector left, MyVector right)
        {
            return new MyVector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static MyPoint operator +(MyPoint point, MyVector vector)
        {
            return new MyPoint(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
        }

        public static MyPoint operator +(MyVector vector, MyPoint point)
        {
            return new MyPoint(vector.X + point.X, vector.Y + point.Y, vector.Z + point.Z);
        }

        public static MyVector operator -(MyVector left, MyVector right)
        {
            return new MyVector(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static MyVector operator *(MyVector vector, double number)
        {
            return new MyVector(vector.X * number, vector.Y * number, vector.Z * number);
        }

        public static MyVector operator *(double number, MyVector vector)
        {
            return new MyVector(vector.X * number, vector.Y * number, vector.Z * number);
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

        public MyVector Normalization()
        {
            double someLength = MyVector.Length(this);

            double X = this.X / someLength;
            double Y = this.Y / someLength;
            double Z = this.Z / someLength;
            return new MyVector(X, Y, Z);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return this.X == ((MyVector)obj).X &&
                   this.Y == ((MyVector)obj).Y &&
                   this.Z == ((MyVector)obj).Z;
        }
    }
}
using System.Numerics;
using project_true.Matrixes;

namespace project_true.Primitives
{
    public class MyPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public MyVector Normal { get; set; }

        public MyPoint()
        {
        }

        public MyPoint(double v1, double v2, double v3)
        {
            this.X = v1;
            this.Y = v2;
            this.Z = v3;
        }

        public MyPoint(MyVector vector)
        {
            this.X = vector.X;
            this.Y = vector.Y;
            this.Z = vector.Z;
        }

        public MyPoint(MyPoint point)
        {
            this.X = point.X;
            this.Y = point.Y;
            this.Z = point.Z;
        }

        public static MyPoint operator -(MyPoint left, MyPoint right)
        {
            return new MyPoint(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public MyPoint Move(float x, float y, float z)
        {
            MyVector normal = null;
            if (Normal != null)
            {
                normal = Normal.Move(x, y, z);
            }
            MyPoint newPoint = new MyPoint(X + x, Y + y, Z + z);
            newPoint.Normal = normal;
            return newPoint;
        }

        public MyPoint Scale(float x, float y, float z)
        {
            MyVector normal = null;
            if (Normal != null)
            {
                normal = Normal.Scale(x, y, z).Normalization();
            }

            MyPoint newPoint = new MyPoint(X * x, Y * y, Z * z);
            newPoint.Normal = normal;
            return newPoint;
        }

        public MyPoint Rotate(Matrix4x4 matrix)
        {
            Vector4 vector = new Vector4((float)this.X, (float)this.Y, (float)this.Z, 1f);
            Vector4 res = matrix.MultiplyMatrix4x4ByVector(vector);
            MyVector normal = null;
            if (Normal != null)
            {
                normal = Normal.Rotate(matrix).Normalization();
            }

            MyPoint newPoint = new MyPoint(res.X / res.W, res.Y / res.W, res.Z / res.W);
            newPoint.Normal = normal;
            return newPoint;
        }
        
        public MyPoint ScaleRotateMove(Matrix4x4 matrix)
        {
            Vector4 vector = new Vector4((float)this.X, (float)this.Y, (float)this.Z, 1f);
            Vector4 res = matrix.MultiplyMatrix4x4ByVector(vector);
            MyVector normal = null;
            if (Normal != null)
            {
                normal = Normal.ScaleRotateMove(matrix).Normalization();
            }

            MyPoint newPoint = new MyPoint(res.X / res.W, res.Y / res.W, res.Z / res.W);
            newPoint.Normal = normal;
            return newPoint;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return this.X == ((MyPoint)obj).X &&
                   this.Y == ((MyPoint)obj).Y &&
                   this.Z == ((MyPoint)obj).Z;
        }
    }
}
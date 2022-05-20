using System;
using System.Numerics;
using project_true.Primitives;

namespace project_true.Matrixes
{
    public static class StaticMatrixClass
    {
        // Функції застосування матриць до
        //
        // точок,
        // векторів,
        // нормалей,
        // трикутників.

        // lab2 part4
        public static Vector4 MultiplyMatrix4x4ByVector(this Matrix4x4 matrix, Vector4 vector)
        {
            Vector4 result = new Vector4();

            result.X = matrix.M11 * vector.X + matrix.M12 * vector.Y + matrix.M13 * vector.Z + matrix.M14 * vector.W;
            result.Y = matrix.M21 * vector.X + matrix.M22 * vector.Y + matrix.M23 * vector.Z + matrix.M24 * vector.W;
            result.Z = matrix.M31 * vector.X + matrix.M32 * vector.Y + matrix.M33 * vector.Z + matrix.M34 * vector.W;
            result.W = matrix.M41 * vector.X + matrix.M42 * vector.Y + matrix.M43 * vector.Z + matrix.M44 * vector.W;

            return result;
        }

        public static Matrix4x4 CreateTranslationMatrix(this Matrix4x4 matrix, float x, float y, float z)
        {
            Matrix4x4 result = new Matrix4x4(
                1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1);

            return result;
        }

        public static Matrix4x4 CreateScaleMatrix(this Matrix4x4 matrix, float x, float y, float z)
        {
            Matrix4x4 result = new Matrix4x4(
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1);

            return result;
        }

        public static Matrix4x4 CreateRotateMatrix(this Matrix4x4 matrix, double xa, double ya, double za)
        {
            double k = Math.PI / 180;
            return CreateRotateMatrixRadians(matrix, xa * k, ya * k, za * k);
        }

        public static Matrix4x4 CreateRotateMatrixRadians(this Matrix4x4 matrix, double radx, double rady, double radz)
        {
            Matrix4x4 xMatrix = new Matrix4x4();
            Matrix4x4 yMatrix = new Matrix4x4();
            Matrix4x4 zMatrix = new Matrix4x4();

            xMatrix = xMatrix.RotateAroundX(radx);
            yMatrix = yMatrix.RotateAroundY(rady);
            zMatrix = zMatrix.RotateAroundZ(radz);

            return Matrix4x4.Multiply(Matrix4x4.Multiply(xMatrix, yMatrix), zMatrix);
        }

        public static Matrix4x4 RotateAroundX(this Matrix4x4 matrix, double radians)
        {
            matrix = Matrix4x4.Identity;
            matrix.M22 = (float)Math.Cos(radians);
            matrix.M23 = (float)Math.Sin(radians);
            matrix.M32 = (float)-(Math.Sin(radians));
            matrix.M33 = (float)Math.Cos(radians);
            return matrix;
        }
        public static Matrix4x4 RotateAroundY(this Matrix4x4 matrix, double radians)
        {
            matrix = Matrix4x4.Identity;
            matrix.M11 = (float)Math.Cos(radians);
            matrix.M13 = (float)-(Math.Sin(radians));
            matrix.M31 = (float)Math.Sin(radians);
            matrix.M33 = (float)Math.Cos(radians);
            return matrix;
        }
        public static Matrix4x4 RotateAroundZ(this Matrix4x4 matrix, double radians)
        {
            matrix = Matrix4x4.Identity;
            matrix.M11 = (float)Math.Cos(radians);
            matrix.M12 = (float)Math.Sin(radians);
            matrix.M21 = (float)-(Math.Sin(radians));
            matrix.M22 = (float)Math.Cos(radians);
            return matrix;
        }
    }
}
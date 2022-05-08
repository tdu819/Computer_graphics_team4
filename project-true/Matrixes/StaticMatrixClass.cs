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
    }
}
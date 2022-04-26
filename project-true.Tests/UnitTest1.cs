using System;
using NUnit.Framework;
using project_true.Figures;
using project_true.Primitives;
using project_true.Tools;

namespace project_true.Tests
{
    [TestFixture]
    public class MyVectorTests
    {
        [SetUp]
        public void Setup()
        {
            var vector1 = new MyVector(1, 2, 3);
            var vector2 = new MyVector(1, 2, 3);
        }

        [Test]
        public void Dot_vector1_1_1_And_vector1_2_3_Result_6()
        {
            // arrange
            double expected = 6.0;

            var vector1 = new MyVector(1, 1, 1);
            var vector2 = new MyVector(1, 2, 3);

            // act

            var actual = MyVector.Dot(vector1, vector2);

            // assert

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Dot_vector1_2_3_And_vector1_2_3_Result_14()
        {
            // arrange
            double expected = 14.0;

            var vector1 = new MyVector(1, 2, 3);
            var vector2 = new MyVector(1, 2, 3);

            // act

            var actual = MyVector.Dot(vector1, vector2);

            // assert

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Normalization_LengthEquals_1()
        {
            // arrange
            double expected = 1;

            var vector1 = new MyVector(11, 22, 33);

            // act

            var actual = MyVector.Length(vector1.Normalization());

            // assert

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Normalization_Vector_4_0_0_AreEquals_1_0_0()
        {
            // arrange
            MyVector expected = new MyVector(1, 0, 0);

            MyVector vector1 = new MyVector(4, 0, 0);

            // act

            MyVector actual = (vector1.Normalization());

            // assert

            Assert.AreEqual(expected, actual);
        }
    }

    // [TestFixture]
    // public class MyPointTests
    // {
    //     [SetUp]
    //     public void Setup()
    //     {
    //     }
    //
    //     [Test]
    //     public void Test1()
    //     {
    //         Assert.Fail();
    //     }
    // }
    //
    //
    // [TestFixture]
    // public class ProgramTests
    // {
    //     [SetUp]
    //     public void Setup()
    //     {
    //     }
    //
    //     [Test]
    //     public void Test1()
    //     {
    //         Assert.Fail();
    //     }
    // }
    //
    //
    
    [TestFixture]
    public class RayTracerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Ray from camera to sphere tangent point.
        /// </summary>
        [Test]
        public void RayIntersectsSphere_Vector_15_0_sqrt75_Intersect()
        {
            // arrange

            MyPoint origin = new MyPoint(0, 0, 0);
            MyPoint sphereCenter = new MyPoint(20, 0, 0);
            double radius = 10;
            MySphere sphere = new MySphere() { Center = sphereCenter, Radius = radius};
            MyPoint point = new MyPoint(15, 0, Math.Sqrt(75));
            MyPoint intersect = new MyPoint();
            // act

            bool actual = RayTracer.RayIntersectsSphere(origin, sphere, point, ref intersect);

            // assert

            Assert.IsTrue(actual);
        }

        [Test]
        public void RayIntersectsSphere_Vector_15_0_sqrt76_NotIntersect()
        {
            // arrange

            MyPoint origin = new MyPoint(0, 0, 0);
            MyPoint sphereCenter = new MyPoint(20, 0, 0);
            double radius = 10;
            MySphere sphere = new MySphere() { Center = sphereCenter, Radius = radius };
            MyPoint point = new MyPoint(15, 0, Math.Sqrt(76));
            MyPoint intersect = new MyPoint();
            // act

            bool actual = RayTracer.RayIntersectsSphere(origin, sphere, point, ref intersect);

            // assert

            Assert.IsFalse(actual);
        }

        /// <summary>
        /// Result point is tangent point.
        /// </summary>
        [Test]
        public void RayIntersectsSphere_Vector_15_0_sqrt75_IntersectPoint()
        {
            // arrange

            MyPoint origin = new MyPoint(0, 0, 0);
            MyPoint sphereCenter = new MyPoint(20, 0, 0);
            double radius = 10;
            MySphere sphere = new MySphere() { Center = sphereCenter, Radius = radius };
            MyPoint intersection = new MyPoint(15, 0, Math.Sqrt(75));
            MyPoint expected = intersection;
            MyPoint actual = new MyPoint();
            // act

            RayTracer.RayIntersectsSphere(origin, sphere, intersection, ref actual);

            // assert

            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Result is nearest of two possible points.
        /// </summary>
        [Test]
        public void RayIntersectsSphere_Vector_10_0_0_NearestIntersectPoint()
        {
            // arrange

            MyPoint origin = new MyPoint(0, 0, 0);
            MyPoint sphereCenter = new MyPoint(20, 0, 0);
            double radius = 10;
            MySphere sphere = new MySphere() { Center = sphereCenter, Radius = radius };
            MyPoint intersection = new MyPoint(10, 0, 0);
            MyPoint expected = intersection;
            MyPoint actual = new MyPoint();
            // act

            RayTracer.RayIntersectsSphere(origin, sphere, intersection, ref actual);

            // assert

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RayIntersectsSphere_SphereBehind_NotIntersect()
        {
            // arrange

            MyPoint origin = new MyPoint(0, 0, 0);
            MyPoint sphereCenter = new MyPoint(-20, 0, 0);
            double radius = 10;
            MySphere sphere = new MySphere() { Center = sphereCenter, Radius = radius };
            MyPoint point = new MyPoint(15, 0, Math.Sqrt(76));
            MyPoint intersect = new MyPoint();
            // act

            bool actual = RayTracer.RayIntersectsSphere(origin, sphere, point, ref intersect);

            // assert

            Assert.IsFalse(actual);
        }
    }
}
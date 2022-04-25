using System;
using NUnit.Framework;
using project_true.Primitives;

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
        public void Normalization_Vectors_AreEquals()
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
    // [TestFixture]
    // public class RayTracerTests
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
}
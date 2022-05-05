using System;
using NUnit.Framework;
using project_true.Camera;
using project_true.Figures;
using project_true.MyScene;
using project_true.Primitives;
using project_true.Tracing;

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
    public class MySphereTests
    {
        [SetUp]
        public void Setup()
        {
            var vector1 = new MyVector(1, 2, 3);
            var vector2 = new MyVector(1, 2, 3);
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
            MySphere sphere = new MySphere() { Center = sphereCenter, Radius = radius };
            MyPoint point = new MyPoint(15, 0, Math.Sqrt(75));
            MyPoint intersect = new MyPoint();
            // act

            bool actual = sphere.RayIntersect(origin, point, ref intersect);

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

            bool actual = sphere.RayIntersect(origin, point, ref intersect);

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

            sphere.RayIntersect(origin, intersection, ref actual);

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

            sphere.RayIntersect(origin, intersection, ref actual);

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
            MyPoint point = new MyPoint(10, 0, 0);
            MyPoint intersect = new MyPoint();
            // act

            bool actual = sphere.RayIntersect(origin, point, ref intersect);

            // assert

            Assert.IsFalse(actual);
        }

        [Test]
        public void RayIntersectsSphere_CameraInsideSphere_NotIntersect()
        {
            // arrange

            MyPoint origin = new MyPoint(0, 0, 0);
            MyPoint sphereCenter = new MyPoint(0, 0, 0);
            double radius = 10;
            MySphere sphere = new MySphere() { Center = sphereCenter, Radius = radius };
            MyPoint point = new MyPoint(10, 0, 0);
            MyPoint expected = new MyPoint(10, 0, 0);
            MyPoint actual = new MyPoint();
            // act

            sphere.RayIntersect(origin, point, ref actual);

            // assert

            Assert.AreEqual(expected, actual);
        }
    }


    [TestFixture]
    public class MyTriangleTests
    {
        [SetUp]
        public void Setup()
        {
            var vector1 = new MyVector(1, 2, 3);
            var vector2 = new MyVector(1, 2, 3);
        }

        [Test]
        public void RayIntersectsTriangle_cameraStrikesInsideTriangle()
        {
            // arrange

            MyPoint camera = new MyPoint(0, 0, 0);
            MyPoint a = new MyPoint(10, -1, 0);
            MyPoint b = new MyPoint(15, 1, -5);
            MyPoint c = new MyPoint(15, 1, 5);
            MyTriangle triangle = new MyTriangle(a, b, c);
            MyPoint rayPointer = new MyPoint(20, 0, 0);


            MyPoint outIntersectionPoint = new MyPoint();
            // act

            bool actual = triangle.RayIntersect(camera, rayPointer, ref outIntersectionPoint);

            // assert

            Assert.IsTrue(actual);
        }

        [Test]
        public void RayIntersectsTriangle_cameraStrikesInTriangleVertex()
        {
            // arrange

            MyPoint camera = new MyPoint(0, 0, 0);
            MyPoint a = new MyPoint(10, 0, 0);
            MyPoint b = new MyPoint(15, 1, -5);
            MyPoint c = new MyPoint(15, 1, 5);
            MyTriangle triangle = new MyTriangle(a, b, c);
            MyPoint rayPointer = new MyPoint(20, 0, 0);


            MyPoint outIntersectionPoint = new MyPoint();
            // act

            bool actual = triangle.RayIntersect(camera, rayPointer, ref outIntersectionPoint);

            // assert

            Assert.IsTrue(actual);
        }

        [Test]
        public void RayIntersectsTriangle_cameraAndTriangleParallell()
        {
            // arrange

            MyPoint camera = new MyPoint(0, 0, 0);
            MyPoint a = new MyPoint(10, 1, 0);
            MyPoint b = new MyPoint(15, 1, -5);
            MyPoint c = new MyPoint(15, 1, 5);
            MyTriangle triangle = new MyTriangle(a, b, c);
            MyPoint rayPointer = new MyPoint(20, 0, 0);

            MyPoint outIntersectionPoint = new MyPoint();
            // act

            bool actual = triangle.RayIntersect(camera, rayPointer, ref outIntersectionPoint);

            // assert

            Assert.IsFalse(actual);
        }

        [Test]
        public void RayIntersectsTriangle_cameraAndTriangle_CameraMissesTriangle()
        {
            // arrange

            MyPoint camera = new MyPoint(0, 0, 0);
            MyPoint a = new MyPoint(10, 1, 0);
            MyPoint b = new MyPoint(15, 2, -5);
            MyPoint c = new MyPoint(15, 3, 5);
            MyTriangle triangle = new MyTriangle(a, b, c);
            MyPoint rayPointer = new MyPoint(20, 0, 0);

            MyPoint outIntersectionPoint = new MyPoint();
            // act

            bool actual = triangle.RayIntersect(camera, rayPointer, ref outIntersectionPoint);

            // assert

            Assert.IsFalse(actual);
        }

        [Test]
        public void RayIntersectsTriangle_TriangleBehindTheCamera()
        {
            // arrange

            MyPoint camera = new MyPoint(0, 0, 0);
            MyPoint a = new MyPoint(-10, 0, 0);
            MyPoint b = new MyPoint(-15, 1, -5);
            MyPoint c = new MyPoint(-15, 2, 5);
            MyTriangle triangle = new MyTriangle(a, b, c);
            MyPoint rayPointer = new MyPoint(20, 0, 0);

            MyPoint outIntersectionPoint = new MyPoint();
            // act

            bool actual = triangle.RayIntersect(camera, rayPointer, ref outIntersectionPoint);

            // assert

            Assert.IsFalse(actual);
        }
    }


    [TestFixture]
    public class TracingHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FindNearestFigure_TwoSpheres_ReturnNearestMySphere1()
        {
            TracingHandler handler = new TracingHandler();

            // arrange
            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraVector = new MyVector() { X = 1, Y = 0, Z = 0 };
            int distance = 5;

            MyCamera camera = new MyCamera(cameraCenter, cameraVector, distance);

            // Scene
            Scene scene = new Scene(camera);

            //Sphere1
            double r1 = 9;
            MyPoint sphereCenter1 = new MyPoint() { X = 10, Y = 0, Z = -5 };

            Figure mySphere1 = new MySphere() { Center = sphereCenter1, Radius = r1 };

            //Sphere2
            double r2 = 8;
            MyPoint sphereCenter2 = new MyPoint() { X = 10, Y = 0, Z = 5 };

            Figure mySphere2 = new MySphere() { Center = sphereCenter2, Radius = r2 };

            // Add Sphere
            scene.AddFigure(mySphere1);
            scene.AddFigure(mySphere2);

            // Our Canvas size
            int height = 20, width = 20;
            MyPoint topLeft = camera.Plane.GetTopLeftPoint(height, width);

            // expected
            var expected = new MySphere() { Center = sphereCenter1, Radius = r1 };

            // act

            Figure actual = handler.FindNearestFigure(scene, height, width, topLeft);

            // assert

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindNearestFigure_TwoSpheres_ReturnNearestMySphere2()
        {
            TracingHandler handler = new TracingHandler();

            // arrange
            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraVector = new MyVector() { X = 1, Y = 0, Z = 0 };
            int distance = 5;

            MyCamera camera = new MyCamera(cameraCenter, cameraVector, distance);

            // Scene
            Scene scene = new Scene(camera);

            //Sphere1
            double r1 = 5;
            MyPoint sphereCenter1 = new MyPoint() { X = 10, Y = 0, Z = -5 };

            Figure mySphere1 = new MySphere() { Center = sphereCenter1, Radius = r1 };

            //Sphere2
            double r2 = 5;
            MyPoint sphereCenter2 = new MyPoint() { X = 7, Y = 0, Z = 5 };

            Figure mySphere2 = new MySphere() { Center = sphereCenter2, Radius = r2 };

            // Add Sphere
            scene.AddFigure(mySphere1);
            scene.AddFigure(mySphere2);

            // Our Canvas size
            int height = 20, width = 20;
            MyPoint topLeft = camera.Plane.GetTopLeftPoint(height, width);

            // expected
            var expected = new MySphere() { Center = sphereCenter2, Radius = r2 };

            // act

            Figure actual = handler.FindNearestFigure(scene, height, width, topLeft);

            // assert

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindNearestFigure_SphereAndTriangle_ReturnNearestFigureTriangle()
        {
            TracingHandler handler = new TracingHandler();

            // arrange
            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraVector = new MyVector() { X = 1, Y = 0, Z = 0 };
            int distance = 5;

            MyCamera camera = new MyCamera(cameraCenter, cameraVector, distance);

            // Scene
            Scene scene = new Scene(camera);

            //Sphere1
            double r1 = 1;
            MyPoint sphereCenter1 = new MyPoint() { X = 10, Y = 0, Z = -5 };

            Figure mySphere1 = new MySphere() { Center = sphereCenter1, Radius = r1 };

            //triangle1
            MyPoint a = new MyPoint(1, 0, 0);
            MyPoint b = new MyPoint(5, 4, -1);
            MyPoint c = new MyPoint(1, -4, -1);


            Figure triangle1 = new MyTriangle(a, b, c);

            // Add Sphere
            scene.AddFigure(mySphere1);
            scene.AddFigure(triangle1);

            // Our Canvas size
            int height = 20, width = 20;
            MyPoint topLeft = camera.Plane.GetTopLeftPoint(height, width);

            // expected
            var expected = new MyTriangle(a, b, c);

            // act

            Figure actual = handler.FindNearestFigure(scene, height, width, topLeft);

            // assert

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindNearestFigure_SphereAroundCameraAndTriangle_ReturnNearestFigureSphere()
        {
            TracingHandler handler = new TracingHandler();

            // arrange
            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraVector = new MyVector() { X = 1, Y = 0, Z = 0 };
            int distance = 5;

            MyCamera camera = new MyCamera(cameraCenter, cameraVector, distance);

            // Scene
            Scene scene = new Scene(camera);

            //Sphere1
            double r1 = 6;
            MyPoint sphereCenter1 = new MyPoint() { X = 10, Y = 0, Z = 0 };

            Figure mySphere1 = new MySphere() { Center = sphereCenter1, Radius = r1 };

            //triangle1
            MyPoint a = new MyPoint(-3, 0, -5);
            MyPoint b = new MyPoint(-3, 4, -1);
            MyPoint c = new MyPoint(-3, 0, 10);


            Figure triangle1 = new MyTriangle(a, b, c);

            // Add Sphere
            scene.AddFigure(mySphere1);
            scene.AddFigure(triangle1);

            // Our Canvas size
            int height = 20, width = 20;
            MyPoint topLeft = camera.Plane.GetTopLeftPoint(height, width);

            // expected
            var expected = new MySphere() { Center = sphereCenter1, Radius = r1 };

            // act

            Figure actual = handler.FindNearestFigure(scene, height, width, topLeft);

            // assert

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindNearestFigure_SphereAroundCameraAndTriangle_ReturnNull()
        {
            TracingHandler handler = new TracingHandler();

            // arrange
            MyPoint cameraCenter = new MyPoint() { X = 0, Y = 0, Z = 0 };
            MyVector cameraVector = new MyVector() { X = 1, Y = 0, Z = 0 };
            int distance = 5;

            MyCamera camera = new MyCamera(cameraCenter, cameraVector, distance);

            // Scene
            Scene scene = new Scene(camera);

            //Sphere1
            double r1 = 1;
            MyPoint sphereCenter1 = new MyPoint() { X = -10, Y = 0, Z = 0 };

            Figure mySphere1 = new MySphere() { Center = sphereCenter1, Radius = r1 };

            //triangle1
            MyPoint a = new MyPoint(-3, 1, 0);
            MyPoint b = new MyPoint(-3, 0, 0);
            MyPoint c = new MyPoint(-3, -1, 0);


            Figure triangle1 = new MyTriangle(a, b, c);

            // Add Sphere
            scene.AddFigure(mySphere1);
            scene.AddFigure(triangle1);

            // Our Canvas size
            int height = 20, width = 20;
            MyPoint topLeft = camera.Plane.GetTopLeftPoint(height, width);

            // expected
            // var expected = new MySphere() { Center = sphereCenter1, Radius = r1 };

            // act

            Figure actual = handler.FindNearestFigure(scene, height, width, topLeft);

            // assert

            Assert.IsNull(actual);
        }
    }

    [TestFixture]
    public class MyPlaneTests
    {
        [TestCase(-10, 0, 0, ExpectedResult=false)]
        [TestCase(10, 0, 0, ExpectedResult=true)]
        public bool RayIntersect_VerticalPlane(int px, int py, int pz)
        {
            // arrange

            MyPoint camera = new MyPoint(0, 0, 0);
            MyPoint a = new MyPoint(px, py, pz);
            MyVector normal = new MyVector(1, 0, 0);
            MyPlane plane = new MyPlane(a, normal);
            MyPoint rayPointer = new MyPoint(20, 0, 0);


            MyPoint outIntersectionPoint = new MyPoint();
            // act

            bool actual = plane.RayIntersect(camera, rayPointer, ref outIntersectionPoint);

            // assert

            return actual;
        }
        
        // infinite number of intersections?
        [TestCase(-10, 0, 0, ExpectedResult=false)]
        [TestCase(10, 0, 0, ExpectedResult=false)]
        public bool RayIntersect_InvisiblePlane(int px, int py, int pz)
        {
            // arrange

            MyPoint camera = new MyPoint(0, 0, 0);
            MyPoint a = new MyPoint(px, py, pz);
            MyVector normal = new MyVector(0, 0, 1);
            MyPlane plane = new MyPlane(a, normal);
            MyPoint rayPointer = new MyPoint(20, 0, 0);


            MyPoint outIntersectionPoint = new MyPoint();
            // act

            bool actual = plane.RayIntersect(camera, rayPointer, ref outIntersectionPoint);

            // assert

            return actual;
        }

        [TestCase(-10, 0, 0, ExpectedResult = false)]
        [TestCase(10, 0, 0, ExpectedResult = true)]
        public bool RayIntersect_AngledPlane(int px, int py, int pz)
        {
            // arrange

            MyPoint camera = new MyPoint(0, 0, 0);
            MyPoint a = new MyPoint(px, py, pz);
            MyVector normal = new MyVector(1, 0, 0);
            MyPlane plane = new MyPlane(a, normal);
            MyPoint rayPointer = new MyPoint(1, 1, 0);


            MyPoint outIntersectionPoint = new MyPoint();
            // act

            bool actual = plane.RayIntersect(camera, rayPointer, ref outIntersectionPoint);

            // assert

            return actual;
        }

        /// <summary>
        /// Plane is parallel to our ray.
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="pz"></param>
        /// <returns></returns>
        [TestCase(-10, 0, -1, ExpectedResult = false)]
        [TestCase(10, 0, -1, ExpectedResult = false)]
        public bool RayIntersect_ParallelPlane(int px, int py, int pz)
        {
            // arrange

            MyPoint camera = new MyPoint(0, 0, 0);
            MyPoint a = new MyPoint(px, py, pz);
            MyVector normal = new MyVector(0, 0, 1);
            MyPlane plane = new MyPlane(a, normal);
            MyPoint rayPointer = new MyPoint(1, 0, 0);


            MyPoint outIntersectionPoint = new MyPoint();
            // act

            bool actual = plane.RayIntersect(camera, rayPointer, ref outIntersectionPoint);

            // assert

            return actual;
        }
        
        [Test]
        public void RayIntersect_IntersectionPoint()
        {
            // arrange

            MyPoint camera = new MyPoint(0, 0, 0);
            
            MyPoint a = new MyPoint(10, 0, 0);
            MyVector normal = new MyVector(1, 0, 0);
            MyPlane plane = new MyPlane(a, normal);
            MyPoint rayPointer = new MyPoint(1, 0, 0);


            var expected = new MyPoint(10, 0, 0);


            MyPoint outIntersectionPoint = new MyPoint();
            // act

            plane.RayIntersect(camera, rayPointer, ref outIntersectionPoint);

            var actual = outIntersectionPoint;

            // assert

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void RayIntersect_PlaneThroughPointX10()
        {
            // arrange

            MyPoint camera = new MyPoint(0, 0, 0);
            
            MyPoint a = new MyPoint(10, 0, 0);
            MyVector normal = new MyVector(1, 0, 0);
            MyPlane plane = new MyPlane(a, normal);
            MyPoint rayPointer = new MyPoint(10, 17, 8);


            var expected = new MyPoint(10, 17, 8);


            MyPoint outIntersectionPoint = new MyPoint();
            // act

            plane.RayIntersect(camera, rayPointer, ref outIntersectionPoint);

            var actual = outIntersectionPoint;

            // assert

            Assert.AreEqual(expected, actual);
        }
    }
}
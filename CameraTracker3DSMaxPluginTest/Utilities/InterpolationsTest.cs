using Microsoft.VisualStudio.TestTools.UnitTesting;
using CameraTracker3DSMaxPlugin.Utilities;
using CameraTracker3DSMaxPlugin.Model;

namespace CameraTracker3DSMaxPluginTest.Utilities {
    [TestClass]
    public class InterpolationsTest {

        [TestMethod]
        public void TestNearestNeighborFloat() {
            float a = 0.0f;
            float b = 1.0f;

            Assert.AreEqual(a, Interpolations.NearestNeighbor(a, b, 0.0f));
            Assert.AreEqual(a, Interpolations.NearestNeighbor(a, b, 0.25f));
            Assert.AreEqual(b, Interpolations.NearestNeighbor(a, b, 0.5f));
            Assert.AreEqual(b, Interpolations.NearestNeighbor(a, b, 0.75f));
            Assert.AreEqual(b, Interpolations.NearestNeighbor(a, b, 1.0f));
        }

        [TestMethod]
        public void TestNearestNeighborPoint3() {
            Point3 a = new Point3(0.0f, 0.0f, 0.0f);
            Point3 b = new Point3(1.0f, 1.0f, 1.0f);

            Assert.AreEqual(a, Interpolations.NearestNeighbor(a, b, 0.0f));
            Assert.AreEqual(a, Interpolations.NearestNeighbor(a, b, 0.25f));
            Assert.AreEqual(b, Interpolations.NearestNeighbor(a, b, 0.5f));
            Assert.AreEqual(b, Interpolations.NearestNeighbor(a, b, 0.75f));
            Assert.AreEqual(b, Interpolations.NearestNeighbor(a, b, 1.0f));
        }

        [TestMethod]
        public void TestLinearFloat1() {
            float a = 0.0f;
            float b = 1.0f;

            Assert.AreEqual(a, Interpolations.Linear(a, b, 0.0f));
            Assert.AreEqual(0.25f, Interpolations.Linear(a, b, 0.25f));
            Assert.AreEqual(0.5f, Interpolations.Linear(a, b, 0.5f));
            Assert.AreEqual(0.75f, Interpolations.Linear(a, b, 0.75f));
            Assert.AreEqual(b, Interpolations.Linear(a, b, 1.0f));
        }

        [TestMethod]
        public void TestLinearFloat2() {
            float a = 0.0f;
            float b = -2.0f;

            Assert.AreEqual(a, Interpolations.Linear(a, b, 0.0f));
            Assert.AreEqual(-0.5f, Interpolations.Linear(a, b, 0.25f));
            Assert.AreEqual(-1.0f, Interpolations.Linear(a, b, 0.5f));
            Assert.AreEqual(-1.5f, Interpolations.Linear(a, b, 0.75f));
            Assert.AreEqual(b, Interpolations.Linear(a, b, 1.0f));
        }

        [TestMethod]
        public void TestLinearFloat3() {
            float a = 1.0f;
            float b = 1.0f;

            Assert.AreEqual(1.0f, Interpolations.Linear(a, b, 0.0f));
            Assert.AreEqual(1.0f, Interpolations.Linear(a, b, 0.25f));
            Assert.AreEqual(1.0f, Interpolations.Linear(a, b, 0.5f));
            Assert.AreEqual(1.0f, Interpolations.Linear(a, b, 0.75f));
            Assert.AreEqual(1.0f, Interpolations.Linear(a, b, 1.0f));
        }

        [TestMethod]
        public void TestLinearPoint3() {
            Point3 a = new Point3(0.0f, 0.0f, 1.0f);
            Point3 b = new Point3(1.0f, -2.0f, 1.0f);

            Assert.AreEqual(a, Interpolations.Linear(a, b, 0.0f));
            Assert.AreEqual(new Point3(0.25f, -0.5f, 1.0f), Interpolations.Linear(a, b, 0.25f));
            Assert.AreEqual(new Point3(0.5f, -1.0f, 1.0f), Interpolations.Linear(a, b, 0.5f));
            Assert.AreEqual(new Point3(0.75f, -1.5f, 1.0f), Interpolations.Linear(a, b, 0.75f));
            Assert.AreEqual(b, Interpolations.Linear(a, b, 1.0f));
        }

        [TestMethod]
        public void TestCubicFloat1() {
            float a = 0.0f;
            float b = 1.0f;
            float c = 1.0f;
            float d = 0.0f;

            Assert.AreEqual(b, Interpolations.Cubic(a, b, c, d, 0.0f));
            Assert.AreEqual(1.09375f, Interpolations.Cubic(a, b, c, d, 0.25f));
            Assert.AreEqual(1.125f, Interpolations.Cubic(a, b, c, d, 0.5f));
            Assert.AreEqual(1.09375f, Interpolations.Cubic(a, b, c, d, 0.75f));
            Assert.AreEqual(c, Interpolations.Cubic(a, b, c, d, 1.0f));
        }

        [TestMethod]
        public void TestCubicFloat2() {
            float a = 0.0f;
            float b = 1.0f;
            float c = 1.0f;
            float d = 2.0f;

            Assert.AreEqual(b, Interpolations.Cubic(a, b, c, d, 0.0f));
            Assert.AreEqual(1.046875f, Interpolations.Cubic(a, b, c, d, 0.25f));
            Assert.AreEqual(1.0f, Interpolations.Cubic(a, b, c, d, 0.5f));
            Assert.AreEqual(0.953125f, Interpolations.Cubic(a, b, c, d, 0.75f));
            Assert.AreEqual(c, Interpolations.Cubic(a, b, c, d, 1.0f));
        }

        [TestMethod]
        public void TestCubicFloat3() {
            float a = 0.0f;
            float b = 1.0f;
            float c = 2.0f;
            float d = 3.0f;

            Assert.AreEqual(b, Interpolations.Cubic(a, b, c, d, 0.0f));
            Assert.AreEqual(1.25f, Interpolations.Cubic(a, b, c, d, 0.25f));
            Assert.AreEqual(1.5f, Interpolations.Cubic(a, b, c, d, 0.5f));
            Assert.AreEqual(1.75f, Interpolations.Cubic(a, b, c, d, 0.75f));
            Assert.AreEqual(c, Interpolations.Cubic(a, b, c, d, 1.0f));
        }

        [TestMethod]
        public void TestCubicPoint3() {
            Point3 a = new Point3(0.0f, 0.0f, 0.0f);
            Point3 b = new Point3(1.0f, 1.0f, 1.0f);
            Point3 c = new Point3(1.0f, 1.0f, 2.0f);
            Point3 d = new Point3(0.0f, 2.0f, 3.0f);

            Assert.AreEqual(b, Interpolations.Cubic(a, b, c, d, 0.0f));
            Assert.AreEqual(new Point3(1.09375f, 1.046875f, 1.25f), Interpolations.Cubic(a, b, c, d, 0.25f));
            Assert.AreEqual(new Point3(1.125f, 1.0f, 1.5f), Interpolations.Cubic(a, b, c, d, 0.5f));
            Assert.AreEqual(new Point3(1.09375f, 0.953125f, 1.75f), Interpolations.Cubic(a, b, c, d, 0.75f));
            Assert.AreEqual(c, Interpolations.Cubic(a, b, c, d, 1.0f));
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CameraTracker3DSMaxPlugin.Model;

namespace CameraTracker3DSMaxPluginTest.Model {
    [TestClass]
    public class Point3Test {
        [TestMethod]
        public void TestEquality() {
            Point3 p1 = new Point3(0.0f, 0.0f, 0.0f);
            Point3 p2 = new Point3(0.0f, 0.0f, 0.0f);
            Point3 p3 = new Point3(1.0f, 0.0f, 2.0f);
            Assert.IsTrue(p1 == p2);
            Assert.IsFalse(p1 != p2);
            Assert.IsFalse(p1 == p3);
            Assert.IsTrue(p1 != p3);
            Assert.IsTrue(p1.Equals(p2));
            Assert.IsFalse(p1.Equals(p3));
        }

        [TestMethod]
        public void TestAddition() {
            Point3 p1 = new Point3(2.0f, 1.0f, 0.0f);
            Point3 p2 = new Point3(3.0f, 2.0f, 1.0f);
            Assert.AreEqual<Point3>(new Point3(5.0f, 3.0f, 1.0f), p1 + p2);
        }

        [TestMethod]
        public void TestSubtraction() {
            Point3 p1 = new Point3(2.0f, 1.0f, 0.0f);
            Point3 p2 = new Point3(3.0f, 2.0f, 1.0f);
            Assert.AreEqual<Point3>(new Point3(-1.0f, -1.0f, -1.0f), p1 - p2);
            Assert.AreEqual<Point3>(new Point3(1.0f, 1.0f, 1.0f), p2 - p1);
        }

        [TestMethod]
        public void TestNegate() {
            Point3 p = new Point3(1.0f, -2.0f, 3.0f);
            Assert.AreEqual<Point3>(new Point3(-1.0f, 2.0f, -3.0f), -p);
        }

        [TestMethod]
        public void TestScale() {
            Point3 p = new Point3(2.0f, 1.0f, 0.0f);
            float s = 3.0f;
            Assert.AreEqual<Point3>(new Point3(6.0f, 3.0f, 0.0f), p * s);
            Assert.AreEqual<Point3>(new Point3(6.0f, 3.0f, 0.0f), s * p);
        }
    }
}

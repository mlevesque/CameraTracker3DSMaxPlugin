using Microsoft.VisualStudio.TestTools.UnitTesting;
using CameraTracker3DSMaxPlugin.Modifiers;
using CameraTracker3DSMaxPlugin.Model;

namespace CameraTracker3DSMaxPluginTest.Modifiers {
    [TestClass]
    public class ModifiersTest {
        [TestMethod]
        public void TestTimeScale() {
            IDataModifier mod = new TimeScaleModifier(2.0);
            Assert.AreEqual(4.0, mod.ModifyTimeStamp(2.0));
            Assert.AreEqual<Point3>(new Point3(1.0f, 2.0f, 3.0f), mod.ModifyPosition(new Point3(1.0f, 2.0f, 3.0f)));
            Assert.AreEqual<Point3>(new Point3(1.0f, 2.0f, 3.0f), mod.ModifyRotation(new Point3(1.0f, 2.0f, 3.0f)));
        }

        [TestMethod]
        public void TestTimeOffset() {
            IDataModifier mod = new TimeOffsetModifier(3.0);
            Assert.AreEqual(4.0, mod.ModifyTimeStamp(1.0));
            Assert.AreEqual<Point3>(new Point3(1.0f, 2.0f, 3.0f), mod.ModifyPosition(new Point3(1.0f, 2.0f, 3.0f)));
            Assert.AreEqual<Point3>(new Point3(1.0f, 2.0f, 3.0f), mod.ModifyRotation(new Point3(1.0f, 2.0f, 3.0f)));
        }

        [TestMethod]
        public void TestPositionScale() {
            IDataModifier mod = new PositionScaleModifier(3.0f);
            Assert.AreEqual(1.0, mod.ModifyTimeStamp(1.0));
            Assert.AreEqual<Point3>(new Point3(3.0f, 6.0f, 9.0f), mod.ModifyPosition(new Point3(1.0f, 2.0f, 3.0f)));
            Assert.AreEqual<Point3>(new Point3(1.0f, 2.0f, 3.0f), mod.ModifyRotation(new Point3(1.0f, 2.0f, 3.0f)));
        }

        [TestMethod]
        public void TestPositionOffset() {
            IDataModifier mod = new PositionOffsetModifier(new Point3(1.0f, 2.0f, 3.0f));
            Assert.AreEqual(1.0, mod.ModifyTimeStamp(1.0));
            Assert.AreEqual<Point3>(new Point3(2.0f, 4.0f, 6.0f), mod.ModifyPosition(new Point3(1.0f, 2.0f, 3.0f)));
            Assert.AreEqual<Point3>(new Point3(1.0f, 2.0f, 3.0f), mod.ModifyRotation(new Point3(1.0f, 2.0f, 3.0f)));
        }

        [TestMethod]
        public void TestRotationScale() {
            IDataModifier mod = new RotationScaleModifier(3.0f);
            Assert.AreEqual(1.0, mod.ModifyTimeStamp(1.0));
            Assert.AreEqual<Point3>(new Point3(1.0f, 2.0f, 3.0f), mod.ModifyPosition(new Point3(1.0f, 2.0f, 3.0f)));
            Assert.AreEqual<Point3>(new Point3(3.0f, 6.0f, 9.0f), mod.ModifyRotation(new Point3(1.0f, 2.0f, 3.0f)));
        }

        [TestMethod]
        public void TestRotationOffset() {
            IDataModifier mod = new RotationOffsetModifier(new Point3(1.0f, 2.0f, 3.0f));
            Assert.AreEqual(1.0, mod.ModifyTimeStamp(1.0));
            Assert.AreEqual<Point3>(new Point3(1.0f, 2.0f, 3.0f), mod.ModifyPosition(new Point3(1.0f, 2.0f, 3.0f)));
            Assert.AreEqual<Point3>(new Point3(2.0f, 4.0f, 6.0f), mod.ModifyRotation(new Point3(1.0f, 2.0f, 3.0f)));
        }
    }
}

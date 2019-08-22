using Microsoft.VisualStudio.TestTools.UnitTesting;
using CameraTracker3DSMaxPlugin.Modifiers;
using CameraTracker3DSMaxPlugin.Model;

namespace CameraTracker3DSMaxPluginTest.Modifiers {
    [TestClass]
    public class ModifierContainerTest {
        [TestMethod]
        public void TestSingularComponents() {
            IDataModifier mod1 = new TimeScaleModifier(2.0);
            IDataModifier mod2 = new PositionOffsetModifier(new Point3(1.0f, 2.0f, 3.0f));
            IDataModifier mod3 = new RotationScaleModifier(3.0f);
            IDataModifierContainer con = new DataModifierContainer();
            con.Add(mod1);
            con.Add(mod2);
            con.Add(mod3);
            Assert.AreEqual(4.0, con.ModifyTimeStamp(2.0));
            Assert.AreEqual<Point3>(new Point3(2.0f, 4.0f, 6.0f), con.ModifyPosition(new Point3(1.0f, 2.0f, 3.0f)));
            Assert.AreEqual<Point3>(new Point3(3.0f, 6.0f, 9.0f), con.ModifyRotation(new Point3(1.0f, 2.0f, 3.0f)));
        }

        [TestMethod]
        public void TestMultiComponents() {
            IDataModifier mod1 = new TimeScaleModifier(2.0);
            IDataModifier mod2 = new PositionOffsetModifier(new Point3(1.0f, 2.0f, 3.0f));
            IDataModifier mod3 = new PositionScaleModifier(3.0f);
            IDataModifierContainer con = new DataModifierContainer();
            con.Add(mod1);
            con.Add(mod2);
            con.Add(mod3);
            Assert.AreEqual(4.0, con.ModifyTimeStamp(2.0));
            Assert.AreEqual<Point3>(new Point3(6.0f, 12.0f, 18.0f), con.ModifyPosition(new Point3(1.0f, 2.0f, 3.0f)));
            Assert.AreEqual<Point3>(new Point3(1.0f, 2.0f, 3.0f), con.ModifyRotation(new Point3(1.0f, 2.0f, 3.0f)));
        }

        [TestMethod]
        public void TestExecuionOrder() {
            IDataModifier mod1 = new TimeScaleModifier(2.0);
            IDataModifier mod2 = new PositionOffsetModifier(new Point3(1.0f, 2.0f, 3.0f));
            IDataModifier mod3 = new PositionScaleModifier(3.0f);
            IDataModifierContainer con = new DataModifierContainer();
            con.Add(mod1);
            con.Add(mod2);
            con.Add(mod3);
            Assert.AreEqual(4.0, con.ModifyTimeStamp(2.0));
            Assert.AreEqual<Point3>(new Point3(6.0f, 12.0f, 18.0f), con.ModifyPosition(new Point3(1.0f, 2.0f, 3.0f)));
            Assert.AreEqual<Point3>(new Point3(1.0f, 2.0f, 3.0f), con.ModifyRotation(new Point3(1.0f, 2.0f, 3.0f)));
            con.Clear();
            con.Add(mod3);
            con.Add(mod2);
            con.Add(mod1);
            Assert.AreEqual(4.0, con.ModifyTimeStamp(2.0));
            Assert.AreEqual<Point3>(new Point3(4.0f, 8.0f, 12.0f), con.ModifyPosition(new Point3(1.0f, 2.0f, 3.0f)));
            Assert.AreEqual<Point3>(new Point3(1.0f, 2.0f, 3.0f), con.ModifyRotation(new Point3(1.0f, 2.0f, 3.0f)));
        }

        [TestMethod]
        public void TestRemoveAt() {
            IDataModifier mod1 = new TimeScaleModifier(2.0);
            IDataModifier mod2 = new PositionOffsetModifier(new Point3(1.0f, 2.0f, 3.0f));
            IDataModifier mod3 = new PositionScaleModifier(3.0f);
            IDataModifierContainer con = new DataModifierContainer();
            con.Add(mod1);
            con.Add(mod2);
            con.Add(mod3);
            con.RemoveByIndex(1);
            Assert.AreEqual(4.0, con.ModifyTimeStamp(2.0));
            Assert.AreEqual<Point3>(new Point3(3.0f, 6.0f, 9.0f), con.ModifyPosition(new Point3(1.0f, 2.0f, 3.0f)));
            Assert.AreEqual<Point3>(new Point3(1.0f, 2.0f, 3.0f), con.ModifyRotation(new Point3(1.0f, 2.0f, 3.0f)));
        }
    }
}

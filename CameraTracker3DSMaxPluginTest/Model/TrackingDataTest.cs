using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CameraTracker3DSMaxPlugin.Model;

namespace CameraTracker3DSMaxPluginTest.Model {
    [TestClass]
    public class TrackingDataTest {
        void PerformEntryComparison(TrackingEntry expected, TrackingEntry actual) {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.TimeStamp, actual.TimeStamp, 0.000001);
            Assert.AreEqual<Point3>(expected.Position, actual.Position);
            Assert.AreEqual<Point3>(expected.Rotation, actual.Rotation);
        }

        [TestMethod]
        public void TestIsEmpty() {
            TrackingData emptyData = new TrackingData(0);
            TrackingData nonEmptyData = new TrackingData(1);
            nonEmptyData.PushEntry(1.0, new Point3(), new Point3());
            Assert.IsTrue(emptyData.IsEmpty);
            Assert.IsFalse(nonEmptyData.IsEmpty);
        }

        [TestMethod]
        public void TestDuration() {
            TrackingData emptyData = new TrackingData(0);
            TrackingData nonEmptyData = new TrackingData(4);
            double[] durations = { 1.0, 2.3, 0.4, 0.8 };
            double total = 0.0;
            foreach (double d in durations) {
                nonEmptyData.PushEntry(d, new Point3(), new Point3());
                total += d;
            }
            Assert.AreEqual(0.0, emptyData.Duration);
            Assert.AreEqual(total, nonEmptyData.Duration);
        }

        [TestMethod]
        public void TestEmptyDataInterpolation() {
            TrackingData emptyData = new TrackingData(0);
            Assert.IsNull(emptyData.GetDataAtTime(0.0));
        }

        [TestMethod]
        public void TestNearestNeighborInterpolationSingleEntry() {
            TrackingData data = new TrackingData(1);
            TrackingEntry expected = new TrackingEntry(0.0, new Point3(0.0f, 1.0f, 2.0f), new Point3(3.0f, 4.0f, 5.0f));
            data.PushEntry(1.0, expected.Position, expected.Rotation);
            // t = 0.0
            expected.TimeStamp = 0.0;
            PerformEntryComparison(expected, data.GetDataAtTime(0.0, InterpolationMethod.NearestNeighbor));
            // t = 0.25
            expected.TimeStamp = 0.25;
            PerformEntryComparison(expected, data.GetDataAtTime(0.25, InterpolationMethod.NearestNeighbor));
            // t = 0.5
            expected.TimeStamp = 0.5;
            PerformEntryComparison(expected, data.GetDataAtTime(0.5, InterpolationMethod.NearestNeighbor));
            // t = 0.75
            expected.TimeStamp = 0.75;
            PerformEntryComparison(expected, data.GetDataAtTime(0.75, InterpolationMethod.NearestNeighbor));
            // t = 1.0
            expected.TimeStamp = 1.0;
            PerformEntryComparison(expected, data.GetDataAtTime(1.0, InterpolationMethod.NearestNeighbor));
        }

        [TestMethod]
        public void TestNearestNeighborInterpolationDoubleEntry() {
            TrackingData data = new TrackingData(2);
            TrackingEntry expected1 = new TrackingEntry(0.0, new Point3(0.0f, 1.0f, 2.0f), new Point3(3.0f, 4.0f, 5.0f));
            TrackingEntry expected2 = new TrackingEntry(0.0, new Point3(6.0f, 7.0f, 8.0f), new Point3(4.0f, 3.0f, 2.0f));
            data.PushEntry(2.0, expected1.Position, expected1.Rotation);
            data.PushEntry(1.0, expected2.Position, expected2.Rotation);
            // t = 0.0
            expected1.TimeStamp = 0.0;
            PerformEntryComparison(expected1, data.GetDataAtTime(expected1.TimeStamp, InterpolationMethod.NearestNeighbor));
            // t = 0.5
            expected1.TimeStamp = 0.5;
            PerformEntryComparison(expected1, data.GetDataAtTime(expected1.TimeStamp, InterpolationMethod.NearestNeighbor));
            // t = 1.0
            expected2.TimeStamp = 1.0;
            PerformEntryComparison(expected2, data.GetDataAtTime(expected2.TimeStamp, InterpolationMethod.NearestNeighbor));
            // t = 1.5
            expected2.TimeStamp = 1.5;
            PerformEntryComparison(expected2, data.GetDataAtTime(expected2.TimeStamp, InterpolationMethod.NearestNeighbor));
            // t = 2.0
            expected2.TimeStamp = 2.0;
            PerformEntryComparison(expected2, data.GetDataAtTime(expected2.TimeStamp, InterpolationMethod.NearestNeighbor));
            // t = 3.0
            expected2.TimeStamp = 3.0;
            PerformEntryComparison(expected2, data.GetDataAtTime(expected2.TimeStamp, InterpolationMethod.NearestNeighbor));
        }

        [TestMethod]
        public void TestNearestNeighborInterpolationMultipleEntry() {
            TrackingData data = new TrackingData(5);
            TrackingEntry expected1 = new TrackingEntry(0.0, new Point3(0.0f, 1.0f, 2.0f), new Point3(3.0f, 4.0f, 5.0f));
            TrackingEntry expected2 = new TrackingEntry(0.0, new Point3(6.0f, 7.0f, 8.0f), new Point3(4.0f, 3.0f, 2.0f));
            TrackingEntry expected3 = new TrackingEntry(0.0, new Point3(1.0f, 1.0f, 6.0f), new Point3(1.0f, 2.0f, 8.0f));
            TrackingEntry expected4 = new TrackingEntry(0.0, new Point3(3.0f, 2.0f, 3.0f), new Point3(0.0f, 1.0f, 6.0f));
            TrackingEntry expected5 = new TrackingEntry(0.0, new Point3(9.0f, 5.0f, 1.0f), new Point3(6.0f, 0.0f, 1.0f));
            data.PushEntry(1.0, expected1.Position, expected1.Rotation);
            data.PushEntry(6.0, expected2.Position, expected2.Rotation);
            data.PushEntry(3.0, expected3.Position, expected3.Rotation);
            data.PushEntry(5.0, expected4.Position, expected4.Rotation);
            data.PushEntry(1.0, expected5.Position, expected5.Rotation);
            // t = 0.0
            expected1.TimeStamp = 0.0;
            PerformEntryComparison(expected1, data.GetDataAtTime(expected1.TimeStamp, InterpolationMethod.NearestNeighbor));
            // t = 2.0
            expected2.TimeStamp = 2.0;
            PerformEntryComparison(expected2, data.GetDataAtTime(expected2.TimeStamp, InterpolationMethod.NearestNeighbor));
            // t = 3.0
            expected2.TimeStamp = 3.0;
            PerformEntryComparison(expected2, data.GetDataAtTime(expected2.TimeStamp, InterpolationMethod.NearestNeighbor));
            // t = 8.0
            expected3.TimeStamp = 8.0;
            PerformEntryComparison(expected3, data.GetDataAtTime(expected3.TimeStamp, InterpolationMethod.NearestNeighbor));
            // t = 8.5
            expected4.TimeStamp = 8.5;
            PerformEntryComparison(expected4, data.GetDataAtTime(expected4.TimeStamp, InterpolationMethod.NearestNeighbor));
            // t = 12.0
            expected4.TimeStamp = 12.0;
            PerformEntryComparison(expected4, data.GetDataAtTime(expected4.TimeStamp, InterpolationMethod.NearestNeighbor));
            // t = 14.0
            expected5.TimeStamp = 14.0;
            PerformEntryComparison(expected5, data.GetDataAtTime(expected5.TimeStamp, InterpolationMethod.NearestNeighbor));
            // t = 17.0
            expected5.TimeStamp = 17.0;
            PerformEntryComparison(expected5, data.GetDataAtTime(expected5.TimeStamp, InterpolationMethod.NearestNeighbor));
        }

        [TestMethod]
        public void TestLinearInterpolationSingleEntry() {
            TrackingData data = new TrackingData(1);
            TrackingEntry expected = new TrackingEntry(0.0, new Point3(0.0f, 1.0f, 2.0f), new Point3(3.0f, 4.0f, 5.0f));
            data.PushEntry(1.0, expected.Position, expected.Rotation);
            // t = 0.0
            expected.TimeStamp = 0.0;
            PerformEntryComparison(expected, data.GetDataAtTime(0.0, InterpolationMethod.Linear));
            // t = 0.25
            expected.TimeStamp = 0.25;
            PerformEntryComparison(expected, data.GetDataAtTime(0.25, InterpolationMethod.Linear));
            // t = 0.5
            expected.TimeStamp = 0.5;
            PerformEntryComparison(expected, data.GetDataAtTime(0.5, InterpolationMethod.Linear));
            // t = 0.75
            expected.TimeStamp = 0.75;
            PerformEntryComparison(expected, data.GetDataAtTime(0.75, InterpolationMethod.Linear));
            // t = 1.0
            expected.TimeStamp = 1.0;
            PerformEntryComparison(expected, data.GetDataAtTime(1.0, InterpolationMethod.Linear));
        }

        [TestMethod]
        public void TestLinearInterpolationDoubleEntry() {
            TrackingData data = new TrackingData(2);
            TrackingEntry expected;
            data.PushEntry(2.0, new Point3(0.0f, 0.0f, 1.0f), new Point3(0.0f, 0.0f, 1.0f));
            data.PushEntry(1.0, new Point3(1.0f, -2.0f, 1.0f), new Point3(1.0f, -2.0f, 1.0f));
            // t = 0.0
            expected = new TrackingEntry(0.0, new Point3(0.0f, 0.0f, 1.0f), new Point3(0.0f, 0.0f, 1.0f));
            PerformEntryComparison(expected, data.GetDataAtTime(expected.TimeStamp, InterpolationMethod.Linear));
            // t = 0.5
            expected = new TrackingEntry(0.5, new Point3(0.25f, -0.5f, 1.0f), new Point3(0.25f, -0.5f, 1.0f));
            PerformEntryComparison(expected, data.GetDataAtTime(expected.TimeStamp, InterpolationMethod.Linear));
            // t = 1.0
            expected = new TrackingEntry(1.0, new Point3(0.5f, -1.0f, 1.0f), new Point3(0.5f, -1.0f, 1.0f));
            PerformEntryComparison(expected, data.GetDataAtTime(expected.TimeStamp, InterpolationMethod.Linear));
            // t = 1.5
            expected = new TrackingEntry(1.5, new Point3(0.75f, -1.5f, 1.0f), new Point3(0.75f, -1.5f, 1.0f));
            PerformEntryComparison(expected, data.GetDataAtTime(expected.TimeStamp, InterpolationMethod.Linear));
            // t = 2.0
            expected = new TrackingEntry(2.0, new Point3(1.0f, -2.0f, 1.0f), new Point3(1.0f, -2.0f, 1.0f));
            PerformEntryComparison(expected, data.GetDataAtTime(expected.TimeStamp, InterpolationMethod.Linear));
        }

        [TestMethod]
        public void TestCubicInterpolationSingleEntry() {
            TrackingData data = new TrackingData(1);
            TrackingEntry expected = new TrackingEntry(0.0, new Point3(0.0f, 1.0f, 2.0f), new Point3(3.0f, 4.0f, 5.0f));
            data.PushEntry(1.0, expected.Position, expected.Rotation);
            // t = 0.0
            expected.TimeStamp = 0.0;
            PerformEntryComparison(expected, data.GetDataAtTime(0.0, InterpolationMethod.Cubic));
            // t = 0.25
            expected.TimeStamp = 0.25;
            PerformEntryComparison(expected, data.GetDataAtTime(0.25, InterpolationMethod.Cubic));
            // t = 0.5
            expected.TimeStamp = 0.5;
            PerformEntryComparison(expected, data.GetDataAtTime(0.5, InterpolationMethod.Cubic));
            // t = 0.75
            expected.TimeStamp = 0.75;
            PerformEntryComparison(expected, data.GetDataAtTime(0.75, InterpolationMethod.Cubic));
            // t = 1.0
            expected.TimeStamp = 1.0;
            PerformEntryComparison(expected, data.GetDataAtTime(1.0, InterpolationMethod.Cubic));
        }
    }
}

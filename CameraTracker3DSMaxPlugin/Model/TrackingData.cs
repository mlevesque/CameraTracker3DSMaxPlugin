using System.Collections.Generic;
using CameraTracker3DSMaxPlugin.Utilities;
using System;

namespace CameraTracker3DSMaxPlugin.Model {

    public enum InterpolationMethod {
        NearestNeighbor,
        Linear,
        Cubic
    }

    public class TrackingEntry {
        public TrackingEntry(double timeStamp, Point3 position, Point3 rotation) {
            TimeStamp = timeStamp;
            Position = position;
            Rotation = rotation;
        }

        public double TimeStamp;
        public Point3 Position;
        public Point3 Rotation;
    }

    public class TrackingData {
        private IList<TrackingEntry> m_entries;
        private double m_duration;

        public bool IsEmpty { get { return m_entries.Count == 0; } }
        public double Duration { get { return m_duration; } }
        public int NumberOfEntries { get { return m_entries.Count; } }

        public TrackingData(int capacity = 100) {
            m_entries = new List<TrackingEntry>(capacity);
            m_duration = 0.0;
        }

        public void PushEntry(double deltaTime, Point3 position, Point3 rotation) {
            // add entry
            TrackingEntry entry = new TrackingEntry(
                m_duration,
                position,
                rotation
                );
            m_entries.Add(entry);

            // update duration
            m_duration += deltaTime;
        }

        public TrackingEntry GetDataAtIndex(int index) {
            TrackingEntry entry = m_entries[index];
            return new TrackingEntry(entry.TimeStamp, entry.Position, entry.Rotation);
        }

        public TrackingEntry GetDataAtTime(double time, InterpolationMethod interpolationMethod = InterpolationMethod.Cubic) {
            if (IsEmpty) {
                return null;
            }
            int index = FindIndexAtTimeRec(time, 0, NumberOfEntries);
            return GetDataAtTimeAndIndex(time, index, interpolationMethod);
        }

        public TrackingData CloneDataWithConstantInterval(double interval, InterpolationMethod interpolationMethod = InterpolationMethod.Cubic) {
            if (interval <= 0.0) {
                throw new ArgumentException("Interval must be a positive value.");
            }
            int capacity = (int)(Duration / interval);
            TrackingData newData = new TrackingData(capacity);
            double time = 0.0;
            int index = 0;
            while (time < Duration) {
                // update index to the correct entry we should work off of at the given time
                while (index < NumberOfEntries && time >= m_entries[index].TimeStamp) {
                    index++;
                }

                // build entry and add it to new data
                TrackingEntry entry = GetDataAtTimeAndIndex(time, index, interpolationMethod);
                newData.PushEntry(interval, entry.Position, entry.Rotation);

                // advance time to next interval
                time += interval;
            }
            return newData;
        }


        private TrackingEntry GetDataAtTimeAndIndex(double time, int index, InterpolationMethod interpolationMethod) {
            switch (interpolationMethod) {
                case InterpolationMethod.Linear:
                    return GetDataLinearAtIndex(index, time);
                case InterpolationMethod.Cubic:
                    return GetDataCubicAtTime(index, time);
                case InterpolationMethod.NearestNeighbor:
                default:
                    return GetDataNearestNeighborAtIndex(index, time);
            }
        }

        private int FindIndexAtTimeRec(double time, int start, int end) {
            // base case - we've narrowed down the range
            if (end - start <= 1) {
                return start;
            }

            // get middle index
            int middle = start + (end - start) / 2;
            TrackingEntry entry = m_entries[middle];

            // which way should we go?
            if (time == entry.TimeStamp) {
                return middle;
            }
            else if (time < entry.TimeStamp) {
                return FindIndexAtTimeRec(time, start, middle);
            }
            else {
                return FindIndexAtTimeRec(time, middle, end);
            }
        }

        private int[] GetIndexPairFromIndex(int index) {
            // special case for single entry
            if (NumberOfEntries == 1) {
                return new int[] { 0, 0 };
            }

            // if index is at the end, then shift backwards
            if (index == (NumberOfEntries - 1)) {
                return new int[] { index - 1, index };
            }
            else {
                return new int[] { index, index + 1 };
            }
        }

        private int[] GetIndexQuadFromIndex(int index) {
            // get middle pair
            int[] pair = GetIndexPairFromIndex(index);

            // get ending indices
            int p0 = pair[0] == 0 ? 0 : pair[0] - 1;
            int p3 = pair[1] == (NumberOfEntries - 1) ? pair[1] : pair[1] + 1;

            // return quad
            return new int[] { p0, pair[0], pair[1], p3 };
        }

        private float NormalizeTimeAtRange(double time, double start, double end) {
            double diff = end - start;
            if (diff == 0.0) {
                return 0.0f;
            }
            else {
                return (float)((time - start) / diff);
            }
        }

        private TrackingEntry GetDataNearestNeighborAtIndex(int index, double time) {
            int[] pair = GetIndexPairFromIndex(index);
            TrackingEntry entry1 = m_entries[pair[0]];
            TrackingEntry entry2 = m_entries[pair[1]];
            float t = NormalizeTimeAtRange(time, entry1.TimeStamp, entry2.TimeStamp);
            return new TrackingEntry(
                time,
                Interpolations.NearestNeighbor(entry1.Position, entry2.Position, t),
                Interpolations.NearestNeighbor(entry1.Rotation, entry2.Rotation, t)
                );
        }

        private TrackingEntry GetDataLinearAtIndex(int index, double time) {
            int[] pair = GetIndexPairFromIndex(index);
            TrackingEntry entry1 = m_entries[pair[0]];
            TrackingEntry entry2 = m_entries[pair[1]];
            float t = NormalizeTimeAtRange(time, entry1.TimeStamp, entry2.TimeStamp);
            return new TrackingEntry(
                time,
                Interpolations.Linear(entry1.Position, entry2.Position, t),
                Interpolations.Linear(entry1.Rotation, entry2.Rotation, t)
                );
        }

        private TrackingEntry GetDataCubicAtTime(int index, double time) {
            int[] quad = GetIndexQuadFromIndex(index);
            TrackingEntry entry1 = m_entries[quad[0]];
            TrackingEntry entry2 = m_entries[quad[1]];
            TrackingEntry entry3 = m_entries[quad[2]];
            TrackingEntry entry4 = m_entries[quad[3]];
            float t = NormalizeTimeAtRange(time, entry2.TimeStamp, entry3.TimeStamp);
            return new TrackingEntry(
                time,
                Interpolations.Cubic(entry1.Position, entry2.Position, entry3.Position, entry4.Position, t),
                Interpolations.Cubic(entry1.Rotation, entry2.Rotation, entry3.Rotation, entry4.Rotation, t)
                );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraTracker3DSMaxPlugin.Model {
    public struct Point3 : IEquatable<Point3> {
        private static float Epsilon = 0.0001f;

        public Point3(float x = 0.0f, float y = 0.0f, float z = 0.0f) {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public bool Equals(Point3 other) {
            return Math.Abs(this.X - other.X) < Epsilon
                && Math.Abs(this.Y - other.Y) < Epsilon
                && Math.Abs(this.Z - other.Z) < Epsilon;
        }
    }
}

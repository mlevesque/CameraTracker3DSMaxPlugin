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

        public static bool operator ==(Point3 lhs, Point3 rhs) {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Point3 lhs, Point3 rhs) {
            return !lhs.Equals(rhs);
        }

        public static Point3 operator +(Point3 lhs, Point3 rhs) {
            return new Point3(
                lhs.X + rhs.X,
                lhs.Y + rhs.Y,
                lhs.Z + rhs.Z
                );
        }

        public static Point3 operator -(Point3 lhs, Point3 rhs) {
            return new Point3(
                lhs.X - rhs.X,
                lhs.Y - rhs.Y,
                lhs.Z - rhs.Z
                );
        }

        public static Point3 operator -(Point3 other) {
            return new Point3(
                -other.X,
                -other.Y,
                -other.Z
                );
        }

        public static Point3 operator *(float s, Point3 p) {
            return new Point3(
                s * p.X,
                s * p.Y,
                s * p.Z
                );
        }

        public static Point3 operator *(Point3 p, float s) {
            return new Point3(
                p.X * s,
                p.Y * s,
                p.Z * s
                );
        }
    }
}

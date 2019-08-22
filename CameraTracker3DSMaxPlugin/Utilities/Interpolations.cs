using CameraTracker3DSMaxPlugin.Model;

namespace CameraTracker3DSMaxPlugin.Utilities {
    public class Interpolations {
        public static float NearestNeighbor(float a, float b, float t) {
            if (t < 0.5) return a;
            return b;
        }

        public static Point3 NearestNeighbor(Point3 a, Point3 b, float t) {
            return new Point3(
                NearestNeighbor(a.X, b.X, t),
                NearestNeighbor(a.Y, b.Y, t),
                NearestNeighbor(a.Z, b.Z, t)
                );
        }

        public static float Linear(float a, float b, float t) {
            return a + (b - a) * t;
        }

        public static Point3 Linear(Point3 a, Point3 b, float t) {
            return new Point3(
                Linear(a.X, b.X, t),
                Linear(a.Y, b.Y, t),
                Linear(a.Z, b.Z, t)
                );
        }

        public static float Cubic(float p0, float p1, float p2, float p3, float t) {
            return p1 + 0.5f * t * (p2 - p0 +
                t * (2.0f * p0 - 5.0f * p1 + 4.0f * p2 - p3 +
                    t * (3.0f * (p1 - p2) + p3 - p0)));
        }

        public static Point3 Cubic(Point3 p0, Point3 p1, Point3 p2, Point3 p3, float t) {
            return new Point3(
                Cubic(p0.X, p1.X, p2.X, p3.X, t),
                Cubic(p0.Y, p1.Y, p2.Y, p3.Y, t),
                Cubic(p0.Z, p1.Z, p2.Z, p3.Z, t)
                );
        }
    }
}

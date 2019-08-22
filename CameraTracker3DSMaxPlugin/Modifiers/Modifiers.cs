using System;
using CameraTracker3DSMaxPlugin.Model;

namespace CameraTracker3DSMaxPlugin.Modifiers {
    public interface IDataModifier {
        double ModifyTimeStamp(double value);
        Point3 ModifyPosition(Point3 value);
        Point3 ModifyRotation(Point3 value);
    }

    public abstract class BaseDataModifier : IDataModifier {
        virtual public double ModifyTimeStamp(double value) {
            return value;
        }
        virtual public Point3 ModifyPosition(Point3 value) {
            return value;
        }
        virtual public Point3 ModifyRotation(Point3 value) {
            return value;
        }
    }

    public class TimeScaleModifier : BaseDataModifier {
        private double m_scale;
        public TimeScaleModifier(double scale) {
            m_scale = scale;
        }
        override public double ModifyTimeStamp(double value) {
            return value * m_scale;
        }
    }

    public class TimeOffsetModifier : BaseDataModifier {
        private double m_offset;
        public TimeOffsetModifier(double offset) {
            m_offset = offset;
        }
        override public double ModifyTimeStamp(double value) {
            return value + m_offset;
        }
    }

    public class PositionScaleModifier : BaseDataModifier {
        private float m_scale;
        public PositionScaleModifier(float scale) {
            m_scale = scale;
        }
        public override Point3 ModifyPosition(Point3 value) {
            return value * m_scale;
        }
    }

    public class PositionOffsetModifier : BaseDataModifier {
        private Point3 m_offset;
        public PositionOffsetModifier(Point3 offset) {
            m_offset = offset;
        }
        public override Point3 ModifyPosition(Point3 value) {
            return value + m_offset;
        }
    }

    public class RotationScaleModifier : BaseDataModifier {
        private float m_scale;
        public RotationScaleModifier(float scale) {
            m_scale = scale;
        }
        public override Point3 ModifyRotation(Point3 value) {
            return value * m_scale;
        }
    }

    public class RotationOffsetModifier : BaseDataModifier {
        private Point3 m_offset;
        public RotationOffsetModifier(Point3 offset) {
            m_offset = offset;
        }
        public override Point3 ModifyRotation(Point3 value) {
            return value + m_offset;
        }
    }
}

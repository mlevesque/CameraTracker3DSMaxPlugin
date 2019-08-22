using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraTracker3DSMaxPlugin.Model;

namespace CameraTracker3DSMaxPlugin.Modifiers {
    public interface IDataModifierContainer : IDataModifier {
        void Add(IDataModifier modifier);
        void Clear();
        void RemoveByIndex(int index);
    }

    public class DataModifierContainer : IDataModifierContainer {
        private IList<IDataModifier> m_modList;

        public DataModifierContainer() {
            m_modList = new List<IDataModifier>();
        }

        public void Add(IDataModifier modifier) {
            m_modList.Add(modifier);
        }

        public void Clear() {
            m_modList.Clear();
        }

        public void RemoveByIndex(int index) {
            m_modList.RemoveAt(index);
        }

        public double ModifyTimeStamp(double value) {
            double result = value;
            foreach (IDataModifier mod in m_modList) {
                result = mod.ModifyTimeStamp(result);
            }
            return result;
        }

        public Point3 ModifyPosition(Point3 value) {
            Point3 result = value;
            foreach (IDataModifier mod in m_modList) {
                result = mod.ModifyPosition(result);
            }
            return result;
        }

        public Point3 ModifyRotation(Point3 value) {
            Point3 result = value;
            foreach (IDataModifier mod in m_modList) {
                result = mod.ModifyRotation(result);
            }
            return result;
        }
    }
}

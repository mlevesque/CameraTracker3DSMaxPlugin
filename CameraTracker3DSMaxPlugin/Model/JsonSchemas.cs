using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraTracker3DSMaxPlugin.Model {
    public class TrackerDataEntryJsonSchema {
        public double t;
        public float px, py, pz;
        public float rx, ry, rz;
    }
   public class TrackerDataFileJsonSchema {
        public TrackerDataEntryJsonSchema[] data;
    }
}

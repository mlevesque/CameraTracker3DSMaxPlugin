using System;
using System.Windows.Forms;
using Autodesk.Max;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;
using CameraTracker3DSMaxPlugin.Utilities;

namespace CameraTracker3DSMaxPlugin {
    public partial class CameraTrackerUI : Form {
        IGlobal m_global;
        Model.TrackerDataFileJsonSchema m_jsonData;

        public CameraTrackerUI(IGlobal global) {
            m_global = global;
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e) {
            /*
            IInterface16 ip = m_global.COREInterface16;
            IINode node = ip.GetSelNode(0);
            IControl c = node.TMController;

            m_global.SuspendAnimate();
            m_global.AnimateOn();

            IPoint3 p = m_global.Point3.Create(100.0, 100.0, 100.0);
            c.PositionController.SetValue(160, p, true, GetSetMethod.Absolute);

            p = m_global.Point3.Create(-100.0, -100.0, -100.0);
            c.PositionController.SetValue(1600, p, true, GetSetMethod.Absolute);


            m_global.AnimateOff();
            m_global.ResumeAnimate();
            
            float scale = float.Parse(txtScale.Text, CultureInfo.InvariantCulture.NumberFormat);
            ip.RescaleWorldUnits(scale, false);
            */

            //Interpolations.NearestNeighbor(0.0f, 1.0f, 0.5f);

            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK) {
                lblFile.Text = openFile.FileName;
                String data = File.ReadAllText(openFile.FileName, System.Text.Encoding.UTF8);
                m_jsonData = JsonConvert.DeserializeObject<Model.TrackerDataFileJsonSchema>(data);

                foreach (Model.TrackerDataEntryJsonSchema entry in m_jsonData.data) {
                    listBoxData.Items.Add(entry.t + ", (" 
                        + entry.px + ", " 
                        + entry.py + ", " 
                        + entry.pz + "), (" 
                        + entry.rx + ", " 
                        + entry.ry + ", " 
                        + entry.rz + ")");
                }
            }
        }
    }
}

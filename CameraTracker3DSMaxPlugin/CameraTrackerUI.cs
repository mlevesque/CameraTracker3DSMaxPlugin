using System;
using System.Windows.Forms;
using Autodesk.Max;
using System.IO;
using Newtonsoft.Json;

namespace CameraTracker3DSMaxPlugin
{
    public partial class CameraTrackerUI : Form
    {
        IGlobal m_global;
        Model.TrackerDataFileJsonSchema m_jsonData;

        public CameraTrackerUI(IGlobal global)
        {
            m_global = global;
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
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

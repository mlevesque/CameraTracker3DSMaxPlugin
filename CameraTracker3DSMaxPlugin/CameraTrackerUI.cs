using System;
using System.Windows.Forms;
using Autodesk.Max;

namespace CameraTracker3DSMaxPlugin
{
    public partial class CameraTrackerUI : Form
    {
        IGlobal m_global;

        public CameraTrackerUI(IGlobal global)
        {
            m_global = global;
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello, World!");
        }
    }
}

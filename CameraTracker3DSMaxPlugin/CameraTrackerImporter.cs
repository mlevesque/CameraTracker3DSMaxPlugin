using System.Diagnostics;

using UiViewModels.Actions;
using Autodesk.Max;

namespace CameraTracker3DSMaxPlugin {
    #region AbstractCustom_CuiActionCommandAdapter
    public abstract class AbstractCustom_CuiActionCommandAdapter : CuiActionCommandAdapter {
        public override string ActionText {
            get { return InternalActionText; }
        }

        public override string Category {
            get { return InternalCategory; }
        }

        public override string InternalActionText {
            get { return CustomActionText; }
        }

        public override string InternalCategory {
            get { return "3D Tracker Importer"; }
        }

        public override void Execute(object parameter) {
            try {
                CustomExecute(parameter);
            }
            catch (System.Exception ex) {
                Debug.Print("Exception occurred: " + ex.Message);
            }
        }

        public abstract string CustomActionText { get; }
        public abstract void CustomExecute(object parameter);
    }
    #endregion

    #region CameraTrackerImporter
    public class CameraTrackerImporter : AbstractCustom_CuiActionCommandAdapter {
        private CameraTrackerUI m_form;
        public override string CustomActionText {
            get { return "Camera Tracker Importer"; }
        }

        public override void CustomExecute(object parameter) {
            m_form = new CameraTrackerUI(GlobalInterface.Instance);
            m_form.Show();
        }
    }
    #endregion
}

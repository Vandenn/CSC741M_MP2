using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC741M_MP2.View
{
    public interface IMainView
    {
        void openMPGDialog();
        void openJPGDialog();
        void showInvalidInputError(string title, string text);
        void updateProgressBar(int progress);
        void setUIEnabled(bool enabled);
        void populateSettings(string defaultSearchPath, double constantAValue, int postTransitionFrameTolerance);
        void fillShotBoundaryPanel(List<string> shotBoundaries);
        void fillKeyframePanel(List<string> keyframes);
    }
}

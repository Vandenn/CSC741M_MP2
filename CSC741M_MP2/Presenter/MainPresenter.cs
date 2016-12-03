using CSC741M_MP2.Model;
using CSC741M_MP2.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSC741M_MP2.Presenter
{
    public class MainPresenter
    {
        private IMainView view;
        private Settings settings;

        private BackgroundWorker processWorker; 

        private List<string> shotBoundaries;
        private List<string> keyframes;

        public MainPresenter(IMainView view)
        {
            /// Property Initialization
            this.view = view;
            settings = Settings.getInstance();
            view.populateSettings(
                settings.defaultSearchPath,
                settings.constantAValue,
                settings.postTransitionFrameTolerance
            );
            shotBoundaries = new List<string>();
            keyframes = new List<string>();

            /// BackgroundWorker initialization
            processWorker = new BackgroundWorker();
            processWorker.DoWork += new DoWorkEventHandler(process_DoWork);
            processWorker.ProgressChanged += new ProgressChangedEventHandler
                    (process_ProgressChanged);
            processWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                    (process_RunWorkerCompleted);
            processWorker.WorkerReportsProgress = true; 
        }

        #region BackgroundWorker Functions
        private void process_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];
            string inputPath = parameters[0] as string;

            JPGHandler jpgHandler = new JPGHandler(inputPath);
            jpgHandler.ProgressUpdate += process_ProgressListener;
            shotBoundaries = jpgHandler.getShotBoundaries();
            processWorker.ReportProgress(0);
            keyframes = jpgHandler.getKeyframes(shotBoundaries);
        }

        private void process_ProgressListener(int progress)
        {
            processWorker.ReportProgress(progress);
        }

        private void process_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            view.updateProgressBar(e.ProgressPercentage);
        }

        private void process_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            view.fillShotBoundaryPanel(shotBoundaries);
            view.fillKeyframePanel(keyframes);
            view.setUIEnabled(true);
        }
        #endregion

        #region View Event Handlers
        public void inputBrowseButtonClickHandler()
        {
            view.openJPGDialog();
        }

        public void runButtonClickHandler(string path)
        {
            if (checkIfInputPathIsValid(path))
            {
                view.setUIEnabled(false);
                object[] parameters = new object[] { path };
                processWorker.RunWorkerAsync(parameters);
            }
            else
            {
                view.showInvalidInputError("Invalid Input", "Invalid Input Path or File!");
            }
        }

        public void saveSettingsButtonClickHandler(string defaultSearchPath, string constantAValue, string postTransitionFrameTolerance)
        {
            double dConstantAValue;
            int nPostTransitionFrameTolerance;

            if (Directory.Exists(defaultSearchPath) && 
                Double.TryParse(constantAValue, out dConstantAValue) && dConstantAValue > 0 && 
                int.TryParse(postTransitionFrameTolerance, out nPostTransitionFrameTolerance) && nPostTransitionFrameTolerance > 0)
            {
                settings.defaultSearchPath = defaultSearchPath;
                settings.constantAValue = dConstantAValue;
                settings.postTransitionFrameTolerance = nPostTransitionFrameTolerance;
                settings.saveSettings();
            }
            else
            {
                view.showInvalidInputError("Invalid Input", "One or More Invalid Inputs!");
            }
        }
        #endregion

        #region Helper Functions
        public string getDefaultSearchPath()
        {
            return Directory.Exists(settings.defaultSearchPath) ? settings.defaultSearchPath : @"C:\";
        }
        #endregion

        private bool checkIfInputPathIsValid(string path)
        {
            return Directory.Exists(path);
        }
    }
}

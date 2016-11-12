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
        private BackgroundWorker shotBoundaryWorker;
        private BackgroundWorker keyframeWorker;

        private List<string> shotBoundaries;
        private List<string> keyframes;

        public MainPresenter(IMainView view)
        {
            /// Property Initialization
            this.view = view;
            settings = Settings.getInstance();
            view.populateSettings(
                settings.DefaultSearchPath
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
            shotBoundaryWorker = new BackgroundWorker();
            shotBoundaryWorker.DoWork += new DoWorkEventHandler(shotBoundary_DoWork);
            shotBoundaryWorker.ProgressChanged += new ProgressChangedEventHandler
                    (shotBoundary_ProgressChanged);
            shotBoundaryWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                    (shotBoundary_RunWorkerCompleted);
            shotBoundaryWorker.WorkerReportsProgress = true;
            keyframeWorker = new BackgroundWorker();
            keyframeWorker.DoWork += new DoWorkEventHandler(keyframe_DoWork);
            keyframeWorker.ProgressChanged += new ProgressChangedEventHandler
                    (keyframe_ProgressChanged);
            keyframeWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                    (keyframe_RunWorkerCompleted);
            keyframeWorker.WorkerReportsProgress = true;
        }

        #region BackgroundWorker Functions
        private void process_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];
            int selectedIndex = (int)parameters[0];
            string inputPath = parameters[1] as string;

            switch (selectedIndex)
            {
                case 0:
                    processWorker.ReportProgress(0);
                    Thread.Sleep(200);
                    processWorker.ReportProgress(50);
                    Thread.Sleep(200);
                    processWorker.ReportProgress(100);
                    break;
                case 1:
                    JPGHandler jpgHandler = new JPGHandler(inputPath);
                    jpgHandler.ProgressUpdate += process_ProgressListener;
                    shotBoundaries = jpgHandler.getShotBoundaries();
                    processWorker.ReportProgress(0);
                    keyframes = jpgHandler.getKeyframes();
                    break;
                default: break;
            }
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
            shotBoundaryWorker.RunWorkerAsync(shotBoundaries);
            keyframeWorker.RunWorkerAsync(keyframes);
            view.setUIEnabled(true);
        }

        private void shotBoundary_DoWork(object sender, DoWorkEventArgs e)
        {
            List<String> results = (List<String>)e.Argument;
            int x = 5;
            int y = 5;
            foreach (string path in results)
            {
                PictureBox picture = new PictureBox();
                picture.Image = Image.FromFile(path);
                picture.Location = new Point(x, y);
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                x += picture.Width + 5;
                shotBoundaryWorker.ReportProgress(0, picture);
            }
        }

        private void shotBoundary_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            view.addShotBoundaryResult((PictureBox)e.UserState);
        }

        private void shotBoundary_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void keyframe_DoWork(object sender, DoWorkEventArgs e)
        {
            List<String> results = (List<String>)e.Argument;
            int x = 5;
            int y = 5;
            foreach (string path in results)
            {
                PictureBox picture = new PictureBox();
                picture.Image = Image.FromFile(path);
                picture.Location = new Point(x, y);
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                x += picture.Width + 5;
                keyframeWorker.ReportProgress(0, picture);
            }
        }

        private void keyframe_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            view.addKeyframeResult((PictureBox)e.UserState);
        }

        private void keyframe_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        #endregion

        #region View Event Handlers
        public void inputBrowseButtonClickHandler(int selectedIndex)
        {
            if (selectedIndex == 0) view.openMPGDialog();
            else if (selectedIndex == 1) view.openJPGDialog();
        }

        public void runButtonClickHandler(int selectedIndex, string path)
        {
            if (checkIfInputPathIsValid(selectedIndex, path))
            {
                view.setUIEnabled(false);
                object[] parameters = new object[] { selectedIndex, path };
                processWorker.RunWorkerAsync(parameters);
            }
            else
            {
                view.showInvalidInputError("Invalid Input", "Invalid Input Path or File!");
            }
        }

        public void saveSettingsButtonClickHandler(string defaultSearchPath)
        {
            if (Directory.Exists(defaultSearchPath))
            {
                settings.DefaultSearchPath = defaultSearchPath;
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
            return Directory.Exists(settings.DefaultSearchPath) ? settings.DefaultSearchPath : @"C:\";
        }
        #endregion

        private bool checkIfInputPathIsValid(int selectedIndex, string path)
        {
            return selectedIndex == 0 && File.Exists(path) || selectedIndex == 1 && Directory.Exists(path);
        }
    }
}

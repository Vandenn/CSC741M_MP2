using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.IO;
using CSC741M_MP2.Presenter;
using MetroFramework.Controls;

namespace CSC741M_MP2.View
{
    public partial class MainView : MetroForm, IMainView
    {
        private readonly string noFilePathChosen = "Click Browse to look for file/folder";

        private MainPresenter mainPresenter;
        private BackgroundWorker shotBoundaryWorker;
        private BackgroundWorker keyframeWorker;
        private string currentPath;

        private List<PictureBox> shotBoundaryPictureBoxes;
        private List<PictureBox> keyframePictureBoxes;
        private List<MetroLabel> shotBoundaryLabels;
        private List<MetroLabel> keyframeLabels;

        public MainView()
        {
            InitializeComponent();

            /// Presenter initialization
            mainPresenter = new MainPresenter(this);

            /// Property Initialization
            currentPath = "";
            shotBoundaryPictureBoxes = new List<PictureBox>();
            keyframePictureBoxes = new List<PictureBox>();
            shotBoundaryLabels = new List<MetroLabel>();
            keyframeLabels = new List<MetroLabel>();

            /// Background Worker Initialization
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

            /// UI Initialization
            filePathLabel.Text = noFilePathChosen;
            metroTabs.SelectedIndex = 0;
        }

        #region Inherited Functions
        public void openMPGDialog()
        {
            OpenFileDialog browser = new OpenFileDialog();
            browser.Title = "Open Query Video";
            browser.Filter = "Query Video Files|*.mpg";
            browser.InitialDirectory = mainPresenter.getDefaultSearchPath();
            browser.CheckFileExists = true;

            if (browser.ShowDialog() == DialogResult.OK)
            {
                currentPath = browser.FileName.ToString();
                filePathLabel.Text = currentPath;
            }
        }

        public void openJPGDialog()
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.Description = "Set Motion JPEG Path";
            browser.SelectedPath = mainPresenter.getDefaultSearchPath();
            browser.ShowNewFolderButton = false;

            if (browser.ShowDialog() == DialogResult.OK)
            {
                currentPath = browser.SelectedPath.ToString();
                if (!currentPath.EndsWith("\\")) currentPath = currentPath + "\\";
                filePathLabel.Text = currentPath;
            }
        }

        public void showInvalidInputError(string title, string text)
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void updateProgressBar(int progress)
        {
            processProgressBar.Value = progress;
        }

        public void setUIEnabled(bool enabled)
        {
            metroTabs.Enabled = enabled;
            inputBrowseButton.Enabled = enabled;
            runButton.Enabled = enabled;
        }

        public void populateSettings(string defaultSearchPath, double constantAValue, int postTransitionFrameTolerance)
        {
            defaultSearchPathTextBox.Text = defaultSearchPath;
            constantATextBox.Text = constantAValue.ToString();
            postTransitionTextBox.Text = postTransitionFrameTolerance.ToString();
        }

        public void fillShotBoundaryPanel(List<string> shotBoundaries)
        {
            clearShotBoundaryPanel();
            shotBoundaryWorker.RunWorkerAsync(shotBoundaries);
        }

        public void fillKeyframePanel(List<string> keyframes)
        {
            clearKeyframePanel();
            keyframeWorker.RunWorkerAsync(keyframes);
        }
        #endregion

        #region Background Worker Functions
        private void shotBoundary_DoWork(object sender, DoWorkEventArgs e)
        {
            List<String> results = (List<String>)e.Argument;
            int x = 5;
            int y = 5;
            foreach (string path in results)
            {
                PictureBox picture = new PictureBox();
                picture.Image = Image.FromFile(path);
                Console.WriteLine(path);
                picture.Location = new Point(x, y);
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                MetroLabel label = new MetroLabel();
                label.Text = Path.GetFileName(path);
                label.Location = new Point(x, y * 2 + picture.Height);
                x += picture.Width + 10;
                shotBoundaryWorker.ReportProgress(0, new object[]{ picture, label });
            }
        }

        private void shotBoundary_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            object[] param = (object[])e.UserState;
            shotBoundaryPictureBoxes.Add((PictureBox)param[0]);
            shotBoundaryLabels.Add((MetroLabel)param[1]);
            shotBoundaryPanel.Controls.Add((PictureBox)param[0]);
            shotBoundaryPanel.Controls.Add((MetroLabel)param[1]);
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
                MetroLabel label = new MetroLabel();
                label.Text = Path.GetFileName(path);
                label.Location = new Point(x, y * 2 + picture.Height);
                x += picture.Width + 10;
                keyframeWorker.ReportProgress(0, new object[] { picture, label });
            }
        }

        private void keyframe_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            object[] param = (object[])e.UserState;
            keyframePictureBoxes.Add((PictureBox)param[0]);
            keyframeLabels.Add((MetroLabel)param[1]);
            keyframePanel.Controls.Add((PictureBox)param[0]);
            keyframePanel.Controls.Add((MetroLabel)param[1]);
        }

        private void keyframe_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        #endregion

        #region Home Tab Functions
        private void inputBrowseButton_Click(object sender, EventArgs e)
        {
            mainPresenter.inputBrowseButtonClickHandler();
        }

        private void fileTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filePathLabel.Text = noFilePathChosen;
            currentPath = "";
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            mainPresenter.runButtonClickHandler(currentPath);
        }

        private void clearShotBoundaryPanel()
        {
            for (int i = shotBoundaryPictureBoxes.Count - 1; i >= 0; i--)
            {
                shotBoundaryPictureBoxes[i].Dispose();
                shotBoundaryPictureBoxes.RemoveAt(i);
                shotBoundaryLabels[i].Dispose();
                shotBoundaryLabels.RemoveAt(i);
            }
        }

        private void clearKeyframePanel()
        {
            for (int i = keyframePictureBoxes.Count - 1; i >= 0; i--)
            {
                keyframePictureBoxes[i].Dispose();
                keyframePictureBoxes.RemoveAt(i);
                keyframeLabels[i].Dispose();
                keyframeLabels.RemoveAt(i);
            }
        }
        #endregion

        #region Settings Tab Functions
        private void defaultSearchPathBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.Description = "Set Default Search Path";
            browser.SelectedPath = @"C:\";
            browser.ShowNewFolderButton = false;

            if (browser.ShowDialog() == DialogResult.OK)
            {
                currentPath = browser.SelectedPath.ToString();
                if (!currentPath.EndsWith("\\")) currentPath = currentPath + "\\";
                defaultSearchPathTextBox.Text = currentPath;
            }
        }

        private void saveSettingsButton_Click(object sender, EventArgs e)
        {
            mainPresenter.saveSettingsButtonClickHandler(defaultSearchPathTextBox.Text, constantATextBox.Text, postTransitionTextBox.Text);
        }
        #endregion
    }
}

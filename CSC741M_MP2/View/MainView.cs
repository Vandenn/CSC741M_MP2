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

namespace CSC741M_MP2.View
{
    public partial class MainView : MetroForm, IMainView
    {
        private readonly string noFilePathChosen = "Click Browse to look for file/folder";

        private MainPresenter mainPresenter;
        private string currentPath;

        public MainView()
        {
            InitializeComponent();

            /// Presenter initialization
            mainPresenter = new MainPresenter(this);

            /// Property Initialization
            currentPath = "";

            /// UI Initialization
            filePathLabel.Text = noFilePathChosen;
            fileTypeComboBox.SelectedIndex = 1;
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
            fileTypeComboBox.Enabled = enabled;
            metroTabs.Enabled = enabled;
            inputBrowseButton.Enabled = enabled;
            runButton.Enabled = enabled;
        }

        public void populateSettings(string defaultSearchPath)
        {
            defaultSearchPathTextBox.Text = defaultSearchPath;
        }

        public void addShotBoundaryResult(PictureBox image)
        {
            shotBoundaryPanel.Controls.Add(image);
        }

        public void addKeyframeResult(PictureBox image)
        {
            keyframePanel.Controls.Add(image);
        }

        public void clearShotBoundaryResults()
        {

        }

        public void clearKeyframeResults()
        {

        }
        #endregion

        #region Home Tab Functions
        private void inputBrowseButton_Click(object sender, EventArgs e)
        {
            mainPresenter.inputBrowseButtonClickHandler(fileTypeComboBox.SelectedIndex);
        }

        private void fileTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            filePathLabel.Text = noFilePathChosen;
            currentPath = "";
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            mainPresenter.runButtonClickHandler(fileTypeComboBox.SelectedIndex, currentPath);
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
            mainPresenter.saveSettingsButtonClickHandler(defaultSearchPathTextBox.Text);
        }
        #endregion
    }
}

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

namespace CSC741M_MP2
{
    public partial class Main : MetroForm
    {
        private string currentPath;

        public Main()
        {
            InitializeComponent();

            //Property Initialization
            currentPath = @"C:\";

            //UI Initialization
            fileTypeComboBox.SelectedIndex = 0;
            metroTabs.SelectedIndex = 0;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (fileTypeComboBox.SelectedIndex == 0)
            {
                OpenFileDialog browser = new OpenFileDialog();
                browser.Title = "Open Query Video";
                browser.Filter = "Query Video Files|*.mpg";
                browser.InitialDirectory = @"C:\";
                browser.CheckFileExists = true;

                if (browser.ShowDialog() == DialogResult.OK)
                {
                    currentPath = browser.FileName.ToString();
                    filePathLabel.Text = currentPath;
                }
            }
            else if (fileTypeComboBox.SelectedIndex == 1)
            {
                FolderBrowserDialog browser = new FolderBrowserDialog();
                browser.Description = "Set Motion JPEG Path";
                browser.SelectedPath = @"C:\";
                browser.ShowNewFolderButton = false;

                if (browser.ShowDialog() == DialogResult.OK)
                {
                    currentPath = browser.SelectedPath.ToString();
                    if (!currentPath.EndsWith("\\")) currentPath = currentPath + "\\";
                    filePathLabel.Text = currentPath;
                }
            }
        }
    }
}

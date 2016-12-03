namespace CSC741M_MP2.View
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.filePathLabel = new MetroFramework.Controls.MetroLabel();
            this.inputBrowseButton = new MetroFramework.Controls.MetroButton();
            this.shotBoundaryPanel = new MetroFramework.Controls.MetroPanel();
            this.metroTabs = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.processProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.keyframePanel = new MetroFramework.Controls.MetroPanel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.shotBoundariesLabel = new MetroFramework.Controls.MetroLabel();
            this.runButton = new MetroFramework.Controls.MetroButton();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.postTransitionTextBox = new MetroFramework.Controls.MetroTextBox();
            this.constantATextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.constantALabel = new MetroFramework.Controls.MetroLabel();
            this.defaultSearchPathBrowseButton = new MetroFramework.Controls.MetroButton();
            this.saveSettingsButton = new MetroFramework.Controls.MetroButton();
            this.defaultSearchPathTextBox = new MetroFramework.Controls.MetroTextBox();
            this.defaultSearchPathLabel = new MetroFramework.Controls.MetroLabel();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.metroTabs.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // filePathLabel
            // 
            this.filePathLabel.Location = new System.Drawing.Point(95, 7);
            this.filePathLabel.Name = "filePathLabel";
            this.filePathLabel.Size = new System.Drawing.Size(484, 29);
            this.filePathLabel.TabIndex = 0;
            this.filePathLabel.Text = "Click Browse to look for file/folder";
            this.filePathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // inputBrowseButton
            // 
            this.inputBrowseButton.Location = new System.Drawing.Point(3, 7);
            this.inputBrowseButton.Name = "inputBrowseButton";
            this.inputBrowseButton.Size = new System.Drawing.Size(86, 29);
            this.inputBrowseButton.TabIndex = 1;
            this.inputBrowseButton.Text = "Browse";
            this.inputBrowseButton.Click += new System.EventHandler(this.inputBrowseButton_Click);
            // 
            // shotBoundaryPanel
            // 
            this.shotBoundaryPanel.AutoScroll = true;
            this.shotBoundaryPanel.HorizontalScrollbar = true;
            this.shotBoundaryPanel.HorizontalScrollbarBarColor = true;
            this.shotBoundaryPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.shotBoundaryPanel.HorizontalScrollbarSize = 10;
            this.shotBoundaryPanel.Location = new System.Drawing.Point(3, 65);
            this.shotBoundaryPanel.Name = "shotBoundaryPanel";
            this.shotBoundaryPanel.Size = new System.Drawing.Size(665, 148);
            this.shotBoundaryPanel.TabIndex = 2;
            this.shotBoundaryPanel.VerticalScrollbar = true;
            this.shotBoundaryPanel.VerticalScrollbarBarColor = true;
            this.shotBoundaryPanel.VerticalScrollbarHighlightOnWheel = false;
            this.shotBoundaryPanel.VerticalScrollbarSize = 10;
            // 
            // metroTabs
            // 
            this.metroTabs.Controls.Add(this.metroTabPage1);
            this.metroTabs.Controls.Add(this.metroTabPage2);
            this.metroTabs.Controls.Add(this.metroTabPage3);
            this.metroTabs.Location = new System.Drawing.Point(23, 63);
            this.metroTabs.Name = "metroTabs";
            this.metroTabs.SelectedIndex = 0;
            this.metroTabs.Size = new System.Drawing.Size(679, 482);
            this.metroTabs.TabIndex = 3;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.processProgressBar);
            this.metroTabPage1.Controls.Add(this.keyframePanel);
            this.metroTabPage1.Controls.Add(this.metroLabel2);
            this.metroTabPage1.Controls.Add(this.shotBoundariesLabel);
            this.metroTabPage1.Controls.Add(this.runButton);
            this.metroTabPage1.Controls.Add(this.shotBoundaryPanel);
            this.metroTabPage1.Controls.Add(this.filePathLabel);
            this.metroTabPage1.Controls.Add(this.inputBrowseButton);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(671, 443);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Home";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            // 
            // processProgressBar
            // 
            this.processProgressBar.Location = new System.Drawing.Point(3, 417);
            this.processProgressBar.Name = "processProgressBar";
            this.processProgressBar.Size = new System.Drawing.Size(665, 23);
            this.processProgressBar.TabIndex = 7;
            // 
            // keyframePanel
            // 
            this.keyframePanel.AutoScroll = true;
            this.keyframePanel.HorizontalScrollbar = true;
            this.keyframePanel.HorizontalScrollbarBarColor = true;
            this.keyframePanel.HorizontalScrollbarHighlightOnWheel = false;
            this.keyframePanel.HorizontalScrollbarSize = 10;
            this.keyframePanel.Location = new System.Drawing.Point(3, 249);
            this.keyframePanel.Name = "keyframePanel";
            this.keyframePanel.Size = new System.Drawing.Size(665, 148);
            this.keyframePanel.TabIndex = 3;
            this.keyframePanel.VerticalScrollbar = true;
            this.keyframePanel.VerticalScrollbarBarColor = true;
            this.keyframePanel.VerticalScrollbarHighlightOnWheel = false;
            this.keyframePanel.VerticalScrollbarSize = 10;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(3, 227);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(69, 19);
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "Keyframes";
            // 
            // shotBoundariesLabel
            // 
            this.shotBoundariesLabel.AutoSize = true;
            this.shotBoundariesLabel.Location = new System.Drawing.Point(3, 43);
            this.shotBoundariesLabel.Name = "shotBoundariesLabel";
            this.shotBoundariesLabel.Size = new System.Drawing.Size(104, 19);
            this.shotBoundariesLabel.TabIndex = 5;
            this.shotBoundariesLabel.Text = "Shot Boundaries";
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(585, 7);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(86, 29);
            this.runButton.TabIndex = 4;
            this.runButton.Text = "Run";
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.postTransitionTextBox);
            this.metroTabPage2.Controls.Add(this.constantATextBox);
            this.metroTabPage2.Controls.Add(this.metroLabel3);
            this.metroTabPage2.Controls.Add(this.constantALabel);
            this.metroTabPage2.Controls.Add(this.defaultSearchPathBrowseButton);
            this.metroTabPage2.Controls.Add(this.saveSettingsButton);
            this.metroTabPage2.Controls.Add(this.defaultSearchPathTextBox);
            this.metroTabPage2.Controls.Add(this.defaultSearchPathLabel);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(671, 443);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Settings";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            // 
            // postTransitionTextBox
            // 
            this.postTransitionTextBox.Location = new System.Drawing.Point(217, 90);
            this.postTransitionTextBox.Name = "postTransitionTextBox";
            this.postTransitionTextBox.Size = new System.Drawing.Size(291, 23);
            this.postTransitionTextBox.TabIndex = 9;
            // 
            // constantATextBox
            // 
            this.constantATextBox.Location = new System.Drawing.Point(217, 51);
            this.constantATextBox.Name = "constantATextBox";
            this.constantATextBox.Size = new System.Drawing.Size(291, 23);
            this.constantATextBox.TabIndex = 8;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(4, 94);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(192, 19);
            this.metroLabel3.TabIndex = 7;
            this.metroLabel3.Text = "Post Transition Frame Tolerance";
            // 
            // constantALabel
            // 
            this.constantALabel.AutoSize = true;
            this.constantALabel.Location = new System.Drawing.Point(4, 55);
            this.constantALabel.Name = "constantALabel";
            this.constantALabel.Size = new System.Drawing.Size(113, 19);
            this.constantALabel.TabIndex = 6;
            this.constantALabel.Text = "Constant Value (a)";
            // 
            // defaultSearchPathBrowseButton
            // 
            this.defaultSearchPathBrowseButton.Location = new System.Drawing.Point(514, 9);
            this.defaultSearchPathBrowseButton.Name = "defaultSearchPathBrowseButton";
            this.defaultSearchPathBrowseButton.Size = new System.Drawing.Size(86, 29);
            this.defaultSearchPathBrowseButton.TabIndex = 5;
            this.defaultSearchPathBrowseButton.Text = "Browse";
            this.defaultSearchPathBrowseButton.Click += new System.EventHandler(this.defaultSearchPathBrowseButton_Click);
            // 
            // saveSettingsButton
            // 
            this.saveSettingsButton.Location = new System.Drawing.Point(3, 164);
            this.saveSettingsButton.Name = "saveSettingsButton";
            this.saveSettingsButton.Size = new System.Drawing.Size(86, 29);
            this.saveSettingsButton.TabIndex = 4;
            this.saveSettingsButton.Text = "Save";
            this.saveSettingsButton.Click += new System.EventHandler(this.saveSettingsButton_Click);
            // 
            // defaultSearchPathTextBox
            // 
            this.defaultSearchPathTextBox.Location = new System.Drawing.Point(217, 12);
            this.defaultSearchPathTextBox.Name = "defaultSearchPathTextBox";
            this.defaultSearchPathTextBox.Size = new System.Drawing.Size(291, 23);
            this.defaultSearchPathTextBox.TabIndex = 3;
            // 
            // defaultSearchPathLabel
            // 
            this.defaultSearchPathLabel.AutoSize = true;
            this.defaultSearchPathLabel.Location = new System.Drawing.Point(4, 16);
            this.defaultSearchPathLabel.Name = "defaultSearchPathLabel";
            this.defaultSearchPathLabel.Size = new System.Drawing.Size(122, 19);
            this.defaultSearchPathLabel.TabIndex = 2;
            this.defaultSearchPathLabel.Text = "Default Search Path";
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.metroLabel1);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(671, 443);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "About";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(3, 29);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(510, 152);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "CSC741M MP2\r\n\r\nDeveloped and Implemented by: LIVELO, Evan Dennison S. and VER, An" +
    "drea Nicole O.\r\nSubmitted to: Dr. Conrado Ruiz Jr.\r\n\r\nT1 AY 2016-2017\r\n\r\n";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 568);
            this.Controls.Add(this.metroTabs);
            this.Name = "MainView";
            this.Text = "Video Segmentor";
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.metroTabs.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroButton inputBrowseButton;
        private MetroFramework.Controls.MetroLabel filePathLabel;
        private MetroFramework.Controls.MetroPanel shotBoundaryPanel;
        private MetroFramework.Controls.MetroTabControl metroTabs;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroProgressBar processProgressBar;
        private MetroFramework.Controls.MetroPanel keyframePanel;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel shotBoundariesLabel;
        private MetroFramework.Controls.MetroButton runButton;
        private MetroFramework.Controls.MetroButton saveSettingsButton;
        private MetroFramework.Controls.MetroTextBox defaultSearchPathTextBox;
        private MetroFramework.Controls.MetroLabel defaultSearchPathLabel;
        private MetroFramework.Controls.MetroButton defaultSearchPathBrowseButton;
        private MetroFramework.Controls.MetroLabel constantALabel;
        private MetroFramework.Controls.MetroTextBox postTransitionTextBox;
        private MetroFramework.Controls.MetroTextBox constantATextBox;
        private MetroFramework.Controls.MetroLabel metroLabel3;
    }
}
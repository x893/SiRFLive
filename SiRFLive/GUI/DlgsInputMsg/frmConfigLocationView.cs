﻿namespace SiRFLive.GUI.DlgsInputMsg
{
    using SiRFLive.General;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class frmConfigLocationView : Form
    {
        private string _IMUfilePath = string.Empty;
        private bool _isAutoCenter = true;
        private bool _isIMUAvailable;
        private double _lat;
        private double _lon;
        private uint _radius = 5;
        private Button cancelBtn;
        private GroupBox centerPointGrpBox;
        private IContainer components;
        private RadioButton frmCommCenterFixedLocationRadioButton;
        private Button frmCommIMUFileBrowseBtn;
        private Label frmCommIMUFilePathLabel;
        private TextBox frmCommIMUFilePathTxtBox;
        private RadioButton frmCommLastLocationAutoCenterRadioButton;
        private TextBox frmCommLatitudeTextBox;
        private TextBox frmCommLongitudeTextBox;
        private Label frmCommRadiusLabel;
        private TextBox frmCommRadiusTxtBox;
        private CheckBox frmCommTruePositionAvailableChkBox;
        private GroupBox imuGrpBox;
        private Label label1;
        private Label label2;
        private Button setAll;
        private Button setCenterPointBtn;
        private Button setIMUInfoBtn;
        private Button setRadiusBtn;

        public event UpdateWindowEventHandler UpdateParentParams;

        public frmConfigLocationView()
        {
            this.InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmCommCenterFixedLocationRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.frmCommCenterFixedLocationRadioButton.Checked)
            {
                this.frmCommLatitudeTextBox.Enabled = true;
                this.frmCommLongitudeTextBox.Enabled = true;
            }
            else
            {
                this.frmCommLatitudeTextBox.Enabled = false;
                this.frmCommLongitudeTextBox.Enabled = false;
            }
        }

        private void frmCommIMUFileBrowseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "IMU File Locator";
            if (this.frmCommIMUFilePathTxtBox.Text.Length != 0)
            {
                if (File.Exists(this.frmCommIMUFilePathTxtBox.Text))
                {
                    dialog.InitialDirectory = this.frmCommIMUFilePathTxtBox.Text.ToString();
                }
                else
                {
                    dialog.InitialDirectory = @".\";
                }
            }
            else
            {
                dialog.InitialDirectory = @".\";
            }
            dialog.Filter = "IMU files (*.gps)|*.gps|All files (*.*)|*.*";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.frmCommIMUFilePathTxtBox.Text = dialog.FileName;
            }
        }

        private void frmCommLastLocationAutoCenterRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.frmCommLastLocationAutoCenterRadioButton.Checked)
            {
                this.frmCommLatitudeTextBox.Enabled = false;
                this.frmCommLongitudeTextBox.Enabled = false;
            }
            else
            {
                this.frmCommLatitudeTextBox.Enabled = true;
                this.frmCommLongitudeTextBox.Enabled = true;
            }
        }

        private void frmConfigLocationView_Load(object sender, EventArgs e)
        {
            if (clsGlobal.IsMarketingUser())
            {
                this.imuGrpBox.Enabled = false;
                this.setIMUInfoBtn.Enabled = false;
            }
            else
            {
                this.imuGrpBox.Enabled = true;
                this.setIMUInfoBtn.Enabled = true;
            }
            this.frmCommRadiusTxtBox.Text = this._radius.ToString();
            if (this._isAutoCenter)
            {
                this.frmCommLastLocationAutoCenterRadioButton.Checked = true;
                this.frmCommLatitudeTextBox.Enabled = false;
                this.frmCommLongitudeTextBox.Enabled = false;
            }
            else
            {
                this.frmCommCenterFixedLocationRadioButton.Checked = true;
                this.frmCommLatitudeTextBox.Enabled = true;
                this.frmCommLongitudeTextBox.Enabled = true;
            }
            this.frmCommLatitudeTextBox.Text = this._lat.ToString();
            this.frmCommLongitudeTextBox.Text = this._lon.ToString();
            this.frmCommTruePositionAvailableChkBox.Checked = this._isIMUAvailable;
            this.frmCommIMUFilePathTxtBox.Text = this._IMUfilePath;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmConfigLocationView));
            this.frmCommLongitudeTextBox = new TextBox();
            this.frmCommLatitudeTextBox = new TextBox();
            this.label2 = new Label();
            this.label1 = new Label();
            this.frmCommCenterFixedLocationRadioButton = new RadioButton();
            this.frmCommLastLocationAutoCenterRadioButton = new RadioButton();
            this.frmCommRadiusLabel = new Label();
            this.frmCommRadiusTxtBox = new TextBox();
            this.imuGrpBox = new GroupBox();
            this.frmCommIMUFilePathTxtBox = new TextBox();
            this.frmCommIMUFileBrowseBtn = new Button();
            this.frmCommTruePositionAvailableChkBox = new CheckBox();
            this.frmCommIMUFilePathLabel = new Label();
            this.setRadiusBtn = new Button();
            this.setCenterPointBtn = new Button();
            this.setIMUInfoBtn = new Button();
            this.cancelBtn = new Button();
            this.centerPointGrpBox = new GroupBox();
            this.setAll = new Button();
            this.imuGrpBox.SuspendLayout();
            this.centerPointGrpBox.SuspendLayout();
            base.SuspendLayout();
            this.frmCommLongitudeTextBox.Location = new Point(0x48, 0x59);
            this.frmCommLongitudeTextBox.Name = "frmCommLongitudeTextBox";
            this.frmCommLongitudeTextBox.Size = new Size(80, 20);
            this.frmCommLongitudeTextBox.TabIndex = 0x12;
            this.frmCommLatitudeTextBox.Location = new Point(0x48, 0x41);
            this.frmCommLatitudeTextBox.Name = "frmCommLatitudeTextBox";
            this.frmCommLatitudeTextBox.Size = new Size(80, 20);
            this.frmCommLatitudeTextBox.TabIndex = 0x11;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(15, 0x5d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x39, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Longitude:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(15, 0x45);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x30, 13);
            this.label1.TabIndex = 0x13;
            this.label1.Text = "Latitude:";
            this.frmCommCenterFixedLocationRadioButton.AutoSize = true;
            this.frmCommCenterFixedLocationRadioButton.Location = new Point(15, 0x2a);
            this.frmCommCenterFixedLocationRadioButton.Name = "frmCommCenterFixedLocationRadioButton";
            this.frmCommCenterFixedLocationRadioButton.Size = new Size(0x8f, 0x11);
            this.frmCommCenterFixedLocationRadioButton.TabIndex = 0x10;
            this.frmCommCenterFixedLocationRadioButton.Text = "Center on &Fixed Location";
            this.frmCommCenterFixedLocationRadioButton.UseVisualStyleBackColor = true;
            this.frmCommCenterFixedLocationRadioButton.CheckedChanged += new EventHandler(this.frmCommCenterFixedLocationRadioButton_CheckedChanged);
            this.frmCommLastLocationAutoCenterRadioButton.AutoSize = true;
            this.frmCommLastLocationAutoCenterRadioButton.Checked = true;
            this.frmCommLastLocationAutoCenterRadioButton.Location = new Point(15, 0x13);
            this.frmCommLastLocationAutoCenterRadioButton.Name = "frmCommLastLocationAutoCenterRadioButton";
            this.frmCommLastLocationAutoCenterRadioButton.Size = new Size(0xa3, 0x11);
            this.frmCommLastLocationAutoCenterRadioButton.TabIndex = 15;
            this.frmCommLastLocationAutoCenterRadioButton.TabStop = true;
            this.frmCommLastLocationAutoCenterRadioButton.Text = "&Auto Center on &Last Location";
            this.frmCommLastLocationAutoCenterRadioButton.UseVisualStyleBackColor = true;
            this.frmCommLastLocationAutoCenterRadioButton.CheckedChanged += new EventHandler(this.frmCommLastLocationAutoCenterRadioButton_CheckedChanged);
            this.frmCommRadiusLabel.AutoSize = true;
            this.frmCommRadiusLabel.Location = new Point(0x1b, 0x13);
            this.frmCommRadiusLabel.Name = "frmCommRadiusLabel";
            this.frmCommRadiusLabel.Size = new Size(0x39, 13);
            this.frmCommRadiusLabel.TabIndex = 14;
            this.frmCommRadiusLabel.Text = "Radius (m)";
            this.frmCommRadiusTxtBox.Location = new Point(0x5e, 15);
            this.frmCommRadiusTxtBox.Name = "frmCommRadiusTxtBox";
            this.frmCommRadiusTxtBox.Size = new Size(80, 20);
            this.frmCommRadiusTxtBox.TabIndex = 13;
            this.imuGrpBox.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.imuGrpBox.Controls.Add(this.frmCommIMUFilePathTxtBox);
            this.imuGrpBox.Controls.Add(this.frmCommIMUFileBrowseBtn);
            this.imuGrpBox.Controls.Add(this.frmCommTruePositionAvailableChkBox);
            this.imuGrpBox.Controls.Add(this.frmCommIMUFilePathLabel);
            this.imuGrpBox.Location = new Point(30, 180);
            this.imuGrpBox.Name = "imuGrpBox";
            this.imuGrpBox.Size = new Size(0x1e2, 0x5c);
            this.imuGrpBox.TabIndex = 0x15;
            this.imuGrpBox.TabStop = false;
            this.imuGrpBox.Text = "IMU";
            this.frmCommIMUFilePathTxtBox.Location = new Point(0x1a, 60);
            this.frmCommIMUFilePathTxtBox.Name = "frmCommIMUFilePathTxtBox";
            this.frmCommIMUFilePathTxtBox.Size = new Size(0x195, 20);
            this.frmCommIMUFilePathTxtBox.TabIndex = 1;
            this.frmCommIMUFileBrowseBtn.Location = new Point(0x1b5, 0x3a);
            this.frmCommIMUFileBrowseBtn.Name = "frmCommIMUFileBrowseBtn";
            this.frmCommIMUFileBrowseBtn.Size = new Size(30, 0x17);
            this.frmCommIMUFileBrowseBtn.TabIndex = 2;
            this.frmCommIMUFileBrowseBtn.Text = "&...";
            this.frmCommIMUFileBrowseBtn.UseVisualStyleBackColor = true;
            this.frmCommIMUFileBrowseBtn.Click += new EventHandler(this.frmCommIMUFileBrowseBtn_Click);
            this.frmCommTruePositionAvailableChkBox.AutoSize = true;
            this.frmCommTruePositionAvailableChkBox.Location = new Point(0x1a, 0x13);
            this.frmCommTruePositionAvailableChkBox.Name = "frmCommTruePositionAvailableChkBox";
            this.frmCommTruePositionAvailableChkBox.Size = new Size(0x5f, 0x11);
            this.frmCommTruePositionAvailableChkBox.TabIndex = 0;
            this.frmCommTruePositionAvailableChkBox.Text = "&Data Available";
            this.frmCommTruePositionAvailableChkBox.UseVisualStyleBackColor = true;
            this.frmCommIMUFilePathLabel.AutoSize = true;
            this.frmCommIMUFilePathLabel.Location = new Point(0x17, 0x2c);
            this.frmCommIMUFilePathLabel.Name = "frmCommIMUFilePathLabel";
            this.frmCommIMUFilePathLabel.Size = new Size(0x47, 13);
            this.frmCommIMUFilePathLabel.TabIndex = 4;
            this.frmCommIMUFilePathLabel.Text = "IMU File Path";
            this.setRadiusBtn.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.setRadiusBtn.Location = new Point(0x192, 20);
            this.setRadiusBtn.Name = "setRadiusBtn";
            this.setRadiusBtn.Size = new Size(100, 0x17);
            this.setRadiusBtn.TabIndex = 0x16;
            this.setRadiusBtn.Text = "Set &Radius";
            this.setRadiusBtn.UseVisualStyleBackColor = true;
            this.setRadiusBtn.Click += new EventHandler(this.setRadiusBtn_Click);
            this.setCenterPointBtn.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.setCenterPointBtn.Location = new Point(0x192, 50);
            this.setCenterPointBtn.Name = "setCenterPointBtn";
            this.setCenterPointBtn.Size = new Size(100, 0x17);
            this.setCenterPointBtn.TabIndex = 0x17;
            this.setCenterPointBtn.Text = "Set Center &Point";
            this.setCenterPointBtn.UseVisualStyleBackColor = true;
            this.setCenterPointBtn.Click += new EventHandler(this.setCenterPoint_Click);
            this.setIMUInfoBtn.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.setIMUInfoBtn.Location = new Point(0x192, 80);
            this.setIMUInfoBtn.Name = "setIMUInfoBtn";
            this.setIMUInfoBtn.Size = new Size(100, 0x17);
            this.setIMUInfoBtn.TabIndex = 0x18;
            this.setIMUInfoBtn.Text = "Set &IMU Info";
            this.setIMUInfoBtn.UseVisualStyleBackColor = true;
            this.setIMUInfoBtn.Click += new EventHandler(this.setIMUInfo_Click);
            this.cancelBtn.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new Point(0x192, 140);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new Size(100, 0x17);
            this.cancelBtn.TabIndex = 0x19;
            this.cancelBtn.Text = "&Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new EventHandler(this.cancelBtn_Click);
            this.centerPointGrpBox.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.centerPointGrpBox.Controls.Add(this.frmCommLongitudeTextBox);
            this.centerPointGrpBox.Controls.Add(this.frmCommLastLocationAutoCenterRadioButton);
            this.centerPointGrpBox.Controls.Add(this.frmCommCenterFixedLocationRadioButton);
            this.centerPointGrpBox.Controls.Add(this.label1);
            this.centerPointGrpBox.Controls.Add(this.label2);
            this.centerPointGrpBox.Controls.Add(this.frmCommLatitudeTextBox);
            this.centerPointGrpBox.Location = new Point(30, 0x29);
            this.centerPointGrpBox.Name = "centerPointGrpBox";
            this.centerPointGrpBox.Size = new Size(0xc1, 0x7a);
            this.centerPointGrpBox.TabIndex = 0x1a;
            this.centerPointGrpBox.TabStop = false;
            this.centerPointGrpBox.Text = "Center Point";
            this.setAll.Location = new Point(0x192, 110);
            this.setAll.Name = "setAll";
            this.setAll.Size = new Size(100, 0x17);
            this.setAll.TabIndex = 0x1b;
            this.setAll.Text = "&Set All";
            this.setAll.UseVisualStyleBackColor = true;
            this.setAll.Click += new EventHandler(this.setAll_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.CancelButton = this.cancelBtn;
            base.ClientSize = new Size(0x210, 0x119);
            base.Controls.Add(this.setAll);
            base.Controls.Add(this.centerPointGrpBox);
            base.Controls.Add(this.cancelBtn);
            base.Controls.Add(this.setIMUInfoBtn);
            base.Controls.Add(this.setCenterPointBtn);
            base.Controls.Add(this.setRadiusBtn);
            base.Controls.Add(this.imuGrpBox);
            base.Controls.Add(this.frmCommRadiusLabel);
            base.Controls.Add(this.frmCommRadiusTxtBox);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmConfigLocationView";
            base.ShowInTaskbar = false;
            base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Location View Configuration";
            base.Load += new EventHandler(this.frmConfigLocationView_Load);
            this.imuGrpBox.ResumeLayout(false);
            this.imuGrpBox.PerformLayout();
            this.centerPointGrpBox.ResumeLayout(false);
            this.centerPointGrpBox.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void setAll_Click(object sender, EventArgs e)
        {
            if ((this.setRadius() && this.setCenterPoint()) && ((clsGlobal.IsMarketingUser() || !this.frmCommTruePositionAvailableChkBox.Checked) || this.setIMUInfo()))
            {
                base.Close();
            }
        }

        private bool setCenterPoint()
        {
            double lat = 0.0;
            double lon = 0.0;
            bool isAutoCenter = true;
            bool flag2 = false;
            try
            {
                lat = double.Parse(this.frmCommLatitudeTextBox.Text);
                lon = double.Parse(this.frmCommLongitudeTextBox.Text);
                if (this.frmCommLastLocationAutoCenterRadioButton.Checked)
                {
                    isAutoCenter = true;
                }
                else
                {
                    isAutoCenter = false;
                }
                if (this.UpdateParentParams != null)
                {
                    this.UpdateParentParams(2, 0, lat, lon, isAutoCenter, false, string.Empty);
                    flag2 = true;
                }
            }
            catch
            {
                MessageBox.Show("Either Latitude or Longitude data is invalid", "Error Location View Configuration", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return flag2;
        }

        private void setCenterPoint_Click(object sender, EventArgs e)
        {
            this.setCenterPoint();
        }

        private bool setIMUInfo()
        {
            if ((this.frmCommIMUFilePathTxtBox.Text == string.Empty) || !File.Exists(this.frmCommIMUFilePathTxtBox.Text))
            {
                MessageBox.Show("IMU filepath is invalid", "Error Location View Configuration", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if (this.UpdateParentParams != null)
            {
                this.UpdateParentParams(3, 0, 0.0, 0.0, false, this.frmCommTruePositionAvailableChkBox.Checked, this.frmCommIMUFilePathTxtBox.Text);
            }
            return false;
        }

        private void setIMUInfo_Click(object sender, EventArgs e)
        {
            this.setIMUInfo();
        }

        private bool setRadius()
        {
            bool flag = false;
            uint result = 5;
            if (uint.TryParse(this.frmCommRadiusTxtBox.Text, out result))
            {
                if (this.UpdateParentParams != null)
                {
                    this.UpdateParentParams(1, result, 0.0, 0.0, false, false, string.Empty);
                    flag = true;
                }
                return flag;
            }
            MessageBox.Show("Radius data is invalid", "Error Location View Configuration", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return flag;
        }

        private void setRadiusBtn_Click(object sender, EventArgs e)
        {
            this.setRadius();
        }

        public string IMUFilepath
        {
            get
            {
                return this._IMUfilePath;
            }
            set
            {
                this._IMUfilePath = value;
            }
        }

        public bool IsAutoCenter
        {
            get
            {
                return this._isAutoCenter;
            }
            set
            {
                this._isAutoCenter = value;
            }
        }

        public bool IsIMUAvailable
        {
            get
            {
                return this._isIMUAvailable;
            }
            set
            {
                this._isIMUAvailable = value;
            }
        }

        public double Lat
        {
            get
            {
                return this._lat;
            }
            set
            {
                this._lat = value;
            }
        }

        public double Lon
        {
            get
            {
                return this._lon;
            }
            set
            {
                this._lon = value;
            }
        }

        public uint Radius
        {
            get
            {
                return this._radius;
            }
            set
            {
                this._radius = value;
            }
        }

        public delegate void UpdateWindowEventHandler(uint type, uint radius, double lat, double lon, bool isAutoCenter, bool isIMUAvailable, string imuPath);
    }
}


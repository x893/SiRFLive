﻿namespace SiRFLive.GUI.Automation
{
    using SiRFLive.Communication;
    using SiRFLive.Configuration;
    using SiRFLive.General;
    using SiRFLive.GUI.DlgsInputMsg;
    using SiRFLive.TestAutomation;
    using SiRFLive.Utilities;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class frmConfiguration : Form
    {
        private IContainer components;
        private Button configCheckBtn;
        private DataGridView configDataGridView;
        private Button configExitBtn;
        private Button configFileBrowserBtn;
        private Label configFilePathLabel;
        private TextBox configFilePathVal;
        internal ObjectInterface configGuiCtrl;
        private Button configLoadBtn;
        private Button configSaveBtn;
        private string currentDir;
        private int currentSelectCellIdx;
        private int currentSelectRowIdx;
        private Button frmEditConfigurationAutoReplyBtn;
        private static frmConfiguration m_SChildform;
        private DataGridViewTextBoxColumn Parameters;
        private DataGridViewTextBoxColumn Values;

        public frmConfiguration()
        {
            this.configGuiCtrl = new ObjectInterface();
            this.currentDir = ConfigurationManager.AppSettings["InstalledDirectory"] + @"\scripts";
            this.InitializeComponent();
        }

        public frmConfiguration(string scriptNamePath)
        {
            this.configGuiCtrl = new ObjectInterface();
            this.currentDir = ConfigurationManager.AppSettings["InstalledDirectory"] + @"\scripts";
            this.InitializeComponent();
            this.configFilePathVal.Text = scriptNamePath.Replace(".py", ".cfg");
        }

        private void configCheckBtn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            string fPath = @".\tmpConfigFile.cfg";
            this.writeNewConfig(fPath);
            IniHelper helper = new IniHelper(fPath);
            if (File.Exists(fPath))
            {
                if (helper.IniSiRFLiveRxSetupErrorCheck(fPath) == 0)
                {
                    MessageBox.Show("No Error Found", "Success");
                }
                File.Delete(fPath);
            }
            else
            {
                MessageBox.Show("File does not exist!", "ERROR!");
            }
            this.Cursor = Cursors.Default;
        }

        private void configDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            e_configParams @params;
            int rowIndex = e.RowIndex;
            string source = this.configDataGridView.Rows[rowIndex].Cells[0].Value.ToString().Trim();
            string inStr = this.configDataGridView.Rows[rowIndex].Cells[1].Value.ToString().Trim();
            switch (source)
            {
                case "PLAYBACK_FILES":
                case "HOST_APP":
                case "PATCH_FILE":
                    @params = e_configParams.E_CONFIG_UNKNOWN;
                    if (!(source == "HOST_APP"))
                    {
                        if (source == "PATCH_FILE")
                        {
                            @params = e_configParams.E_PATCH_NAME;
                        }
                        else
                        {
                            @params = e_configParams.E_PLAYBACK_FILES;
                        }
                        break;
                    }
                    @params = e_configParams.E_HOST_APP_NAME;
                    break;

                case "BASE_TEST_LOG":
                case "HOST_APP_DIR":
                {
                    e_configParams type = e_configParams.E_CONFIG_UNKNOWN;
                    if (!(source == "HOST_APP_DIR"))
                    {
                        type = e_configParams.E_LOG_DIR;
                    }
                    else
                    {
                        type = e_configParams.E_HOST_DIR;
                    }
                    frmUpdateTestLogDir dir = frmUpdateTestLogDir.GetChildInstance(type);
                    if (dir.IsDisposed)
                    {
                        dir = new frmUpdateTestLogDir(type);
                    }
                    this.currentSelectCellIdx = 1;
                    this.currentSelectRowIdx = rowIndex;
                    dir.updateParent += new frmUpdateTestLogDir.updateParentEventHandler(this.updateConfigList);
                    dir.MdiParent = base.MdiParent;
                    dir.ShowDialog();
                    return;
                }
                case "PLAY_TIME_LISTS":
                {
                    frmUpdatePlaybackTime time = frmUpdatePlaybackTime.GetChildInstance();
                    if (time.IsDisposed)
                    {
                        time = new frmUpdatePlaybackTime();
                    }
                    this.currentSelectCellIdx = 1;
                    this.currentSelectRowIdx = rowIndex;
                    time.updateParent += new frmUpdatePlaybackTime.updateParentEventHandler(this.updateConfigList);
                    time.MdiParent = base.MdiParent;
                    time.ShowDialog();
                    return;
                }
                case "SIGNAL_SOURCE":
                case "ATTEN_SOURCE":
                case "POWER_SOURCE":
                {
                    frmComboBoxSelection selection = frmComboBoxSelection.GetChildInstance(source);
                    if (selection.IsDisposed)
                    {
                        selection = new frmComboBoxSelection(source);
                    }
                    this.currentSelectCellIdx = 1;
                    this.currentSelectRowIdx = rowIndex;
                    selection.updateParent += new frmComboBoxSelection.updateParentEventHandler(this.updateConfigList);
                    selection.MdiParent = base.MdiParent;
                    selection.ShowDialog();
                    return;
                }
                default:
                    if (this.configDataGridView.Rows[rowIndex].HeaderCell.Value.ToString() == "RX_SETUP")
                    {
                        frmReceiverConfig config = frmReceiverConfig.GetChildInstance();
                        if (config.IsDisposed)
                        {
                            config = new frmReceiverConfig();
                        }
                        config.MdiParent = base.MdiParent;
                        config.updateParent += new frmReceiverConfig.updateParentEventHandler(this.updateConfigList);
                        config.ShowDialog();
                    }
                    return;
            }
            frmUpdatePbFiles childInstance = frmUpdatePbFiles.GetChildInstance(@params, inStr);
            if (childInstance.IsDisposed)
            {
                childInstance = new frmUpdatePbFiles(@params, inStr);
            }
            this.currentSelectCellIdx = 1;
            this.currentSelectRowIdx = rowIndex;
            childInstance.updateParent += new frmUpdatePbFiles.updateParentEventHandler(this.updateConfigList);
            childInstance.MdiParent = base.MdiParent;
            childInstance.ShowDialog();
        }

        private void configExitBtn_Click(object sender, EventArgs e)
        {
            clsGlobal.AutomationParamsHash.Clear();
            base.Close();
            m_SChildform = null;
        }

        private void configFileBrowser_Click(object sender, EventArgs e)
        {
            this.configGuiCtrl.FileBrowser(this.configFilePathVal, this.currentDir);
            this.currentDir = this.configFilePathVal.Text;
        }

        private void configLoadBtn_Click(object sender, EventArgs e)
        {
            this.loadConfigFile();
        }

        private void configSaveBtn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            int num = 0;
            IniHelper helper = new IniHelper(this.configFilePathVal.Text);
            if (!File.Exists(this.configFilePathVal.Text))
            {
                this.writeNewConfig(this.configFilePathVal.Text);
            }
            else
            {
                if (MessageBox.Show("File exists! -- Overwrite?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
                if ((File.GetAttributes(this.configFilePathVal.Text) & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
                {
                    while (num < (this.configDataGridView.Rows.Count - 1))
                    {
                        string str = this.configDataGridView.Rows[num].Cells[0].Value.ToString().Trim();
                        if (((str == "EXTRA_HOST_APP_ARGVS") || (str == "SIM_FILE")) || str.StartsWith("TEST_"))
                        {
                            helper.IniWriteValue(this.configDataGridView.Rows[num].HeaderCell.Value.ToString().Trim().Replace(" ", ""), this.configDataGridView.Rows[num].Cells[0].Value.ToString().Trim().Replace(" ", ""), this.configDataGridView.Rows[num].Cells[1].Value.ToString().Trim());
                        }
                        else
                        {
                            helper.IniWriteValue(this.configDataGridView.Rows[num].HeaderCell.Value.ToString().Trim().Replace(" ", ""), this.configDataGridView.Rows[num].Cells[0].Value.ToString().Trim().Replace(" ", ""), this.configDataGridView.Rows[num].Cells[1].Value.ToString().Trim().Replace(" ", ""));
                        }
                        num++;
                    }
                }
                else
                {
                    MessageBox.Show("File is read only!\nPlease change property and retry", "Error");
                    this.Cursor = Cursors.Default;
                    return;
                }
            }
            helper.IniSiRFLiveRxSetupErrorCheck(this.configFilePathVal.Text);
            string text1 = ConfigurationManager.AppSettings["InstalledDirectory"] + @"\Protocols\Protocols_AI3_Request.xml";
            helper.GetIniFileString("TEST_SETUP", "REQUIRED_AIDING", "");
            this.Cursor = Cursors.Default;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmConfiguration_Load(object sender, EventArgs e)
        {
            this.loadConfigFile();
        }

        private void frmEditConfigurationAutoReplyBtn_Click(object sender, EventArgs e)
        {
            string filepath = string.Empty;
            if ((this.configFilePathVal.Text.Length == 0) || !File.Exists(this.configFilePathVal.Text))
            {
                filepath = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutomationSetupAutoReply.cfg";
            }
            else
            {
                filepath = this.configFilePathVal.Text;
            }
            frmAutoReply reply = new frmAutoReply();
            reply.AutoReplyConfigFilePath = filepath;
            reply.CommWindow = new CommunicationManager();
            reply.CommWindow.ReadAutoReplyData(filepath);
            reply.ShowDialog();
            this.loadConfigFile();
            reply.CommWindow.Dispose();
            reply.CommWindow = null;
            reply.Dispose();
            reply = null;
        }

        public static frmConfiguration GetChildInstance()
        {
            if (m_SChildform == null)
            {
                m_SChildform = new frmConfiguration();
            }
            return m_SChildform;
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmConfiguration));
            this.configFileBrowserBtn = new Button();
            this.configFilePathLabel = new Label();
            this.configFilePathVal = new TextBox();
            this.configLoadBtn = new Button();
            this.configSaveBtn = new Button();
            this.configExitBtn = new Button();
            this.configDataGridView = new DataGridView();
            this.Parameters = new DataGridViewTextBoxColumn();
            this.Values = new DataGridViewTextBoxColumn();
            this.configCheckBtn = new Button();
            this.frmEditConfigurationAutoReplyBtn = new Button();
            ((ISupportInitialize) this.configDataGridView).BeginInit();
            base.SuspendLayout();
            this.configFileBrowserBtn.Location = new Point(580, 0x3f);
            this.configFileBrowserBtn.Name = "configFileBrowserBtn";
            this.configFileBrowserBtn.Size = new Size(0x1a, 0x17);
            this.configFileBrowserBtn.TabIndex = 1;
            this.configFileBrowserBtn.Text = "&...";
            this.configFileBrowserBtn.UseVisualStyleBackColor = true;
            this.configFileBrowserBtn.Click += new EventHandler(this.configFileBrowser_Click);
            this.configFilePathLabel.AutoSize = true;
            this.configFilePathLabel.Location = new Point(0x1f, 0x2b);
            this.configFilePathLabel.Name = "configFilePathLabel";
            this.configFilePathLabel.Size = new Size(0x71, 13);
            this.configFilePathLabel.TabIndex = 4;
            this.configFilePathLabel.Text = "Configuration File Path";
            this.configFilePathVal.Location = new Point(0x1d, 0x40);
            this.configFilePathVal.Name = "configFilePathVal";
            this.configFilePathVal.Size = new Size(0x221, 20);
            this.configFilePathVal.TabIndex = 0;
            this.configLoadBtn.Location = new Point(0x8b, 0x1d2);
            this.configLoadBtn.Name = "configLoadBtn";
            this.configLoadBtn.Size = new Size(0x4b, 0x17);
            this.configLoadBtn.TabIndex = 4;
            this.configLoadBtn.Text = "&Load";
            this.configLoadBtn.UseVisualStyleBackColor = true;
            this.configLoadBtn.Click += new EventHandler(this.configLoadBtn_Click);
            this.configSaveBtn.Location = new Point(0x147, 0x1d2);
            this.configSaveBtn.Name = "configSaveBtn";
            this.configSaveBtn.Size = new Size(0x4b, 0x17);
            this.configSaveBtn.TabIndex = 6;
            this.configSaveBtn.Text = "&Save";
            this.configSaveBtn.Click += new EventHandler(this.configSaveBtn_Click);
            this.configExitBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.configExitBtn.Location = new Point(0x1a5, 0x1d2);
            this.configExitBtn.Name = "configExitBtn";
            this.configExitBtn.Size = new Size(0x4b, 0x17);
            this.configExitBtn.TabIndex = 7;
            this.configExitBtn.Text = "E&xit";
            this.configExitBtn.UseVisualStyleBackColor = true;
            this.configExitBtn.Click += new EventHandler(this.configExitBtn_Click);
            this.configDataGridView.AllowDrop = true;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.Control;
            style.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.configDataGridView.ColumnHeadersDefaultCellStyle = style;
            this.configDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.configDataGridView.Columns.AddRange(new DataGridViewColumn[] { this.Parameters, this.Values });
            this.configDataGridView.Location = new Point(0x1d, 0x6b);
            this.configDataGridView.Name = "configDataGridView";
            style2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style2.BackColor = SystemColors.HighlightText;
            style2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            style2.ForeColor = SystemColors.WindowText;
            style2.SelectionBackColor = SystemColors.Highlight;
            style2.SelectionForeColor = SystemColors.HighlightText;
            style2.WrapMode = DataGridViewTriState.True;
            this.configDataGridView.RowHeadersDefaultCellStyle = style2;
            this.configDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            style3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.configDataGridView.RowsDefaultCellStyle = style3;
            this.configDataGridView.Size = new Size(0x221, 0x153);
            this.configDataGridView.TabIndex = 3;
            this.configDataGridView.CellContentDoubleClick += new DataGridViewCellEventHandler(this.configDataGridView_CellDoubleClick);
            this.Parameters.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Parameters.HeaderText = "Parameters";
            this.Parameters.Name = "Parameters";
            this.Parameters.ReadOnly = true;
            this.Parameters.Width = 0x5f;
            this.Values.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.Values.HeaderText = "Values";
            this.Values.Name = "Values";
            this.configCheckBtn.Location = new Point(0xe9, 0x1d2);
            this.configCheckBtn.Name = "configCheckBtn";
            this.configCheckBtn.Size = new Size(0x4b, 0x17);
            this.configCheckBtn.TabIndex = 5;
            this.configCheckBtn.Text = "&Check";
            this.configCheckBtn.UseVisualStyleBackColor = true;
            this.configCheckBtn.Click += new EventHandler(this.configCheckBtn_Click);
            this.frmEditConfigurationAutoReplyBtn.Location = new Point(0x1d, 10);
            this.frmEditConfigurationAutoReplyBtn.Name = "frmEditConfigurationAutoReplyBtn";
            this.frmEditConfigurationAutoReplyBtn.Size = new Size(0x73, 0x17);
            this.frmEditConfigurationAutoReplyBtn.TabIndex = 8;
            this.frmEditConfigurationAutoReplyBtn.Text = "Config &AutoReply";
            this.frmEditConfigurationAutoReplyBtn.UseVisualStyleBackColor = true;
            this.frmEditConfigurationAutoReplyBtn.Click += new EventHandler(this.frmEditConfigurationAutoReplyBtn_Click);
            base.AcceptButton = this.configLoadBtn;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            base.CancelButton = this.configExitBtn;
            base.ClientSize = new Size(0x266, 0x1f3);
            base.Controls.Add(this.frmEditConfigurationAutoReplyBtn);
            base.Controls.Add(this.configDataGridView);
            base.Controls.Add(this.configExitBtn);
            base.Controls.Add(this.configSaveBtn);
            base.Controls.Add(this.configCheckBtn);
            base.Controls.Add(this.configLoadBtn);
            base.Controls.Add(this.configFileBrowserBtn);
            base.Controls.Add(this.configFilePathLabel);
            base.Controls.Add(this.configFilePathVal);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "frmConfiguration";
            base.ShowInTaskbar = false;
            base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Configuration View";
            base.Load += new EventHandler(this.frmConfiguration_Load);
            ((ISupportInitialize) this.configDataGridView).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void loadConfigFile()
        {
            this.Cursor = Cursors.WaitCursor;
            if (!File.Exists(this.configFilePathVal.Text))
            {
                MessageBox.Show("Configuration File does not exist", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.Cursor = Cursors.Default;
            }
            else
            {
                IniHelper helper = new IniHelper(this.configFilePathVal.Text);
                List<string> sections = helper.GetSections();
                List<string> list2 = new List<string>();
                string str = string.Empty;
                char[] trimChars = new char[] { '\n', '\r', '\t', '\0' };
                this.configDataGridView.Rows.Clear();
                foreach (string str2 in sections)
                {
                    if (((str2 != "RF_PLAYBACK") && (str2 != "SIM")) && ((str2 != "LIVE") && (str2 != "AIDING_PARAMS")))
                    {
                        foreach (string str3 in helper.GetKeys(str2))
                        {
                            if (!str3.Contains("#"))
                            {
                                int num = this.configDataGridView.Rows.Add();
                                str = helper.GetIniFileString(str2, str3, "");
                                this.configDataGridView.Rows[num].HeaderCell.Value = str2;
                                this.configDataGridView.Rows[num].Cells[0].Value = str3;
                                this.configDataGridView.Rows[num].Cells[1].Value = str.TrimEnd(trimChars);
                                if (!clsGlobal.AutomationParamsHash.ContainsKey(str3))
                                {
                                    if ((str3 != "BASE_TEST_LOG") && (str3 != "SIM_FILE"))
                                    {
                                        clsGlobal.AutomationParamsHash.Add(str3, this.configDataGridView.Rows[num].Cells[1].Value.ToString().Replace(" ", ""));
                                    }
                                    else
                                    {
                                        clsGlobal.AutomationParamsHash.Add(str3, this.configDataGridView.Rows[num].Cells[1].Value);
                                    }
                                }
                            }
                        }
                    }
                }
                string category = helper.GetIniFileString("TEST_SETUP", "SIGNAL_SOURCE", "");
                foreach (string str5 in helper.GetKeys(category))
                {
                    if (!str5.Contains("#"))
                    {
                        int num2 = this.configDataGridView.Rows.Add();
                        str = helper.GetIniFileString(category, str5, "");
                        this.configDataGridView.Rows[num2].HeaderCell.Value = category;
                        this.configDataGridView.Rows[num2].Cells[0].Value = str5;
                        this.configDataGridView.Rows[num2].Cells[1].Value = str.TrimEnd(trimChars);
                        if (!clsGlobal.AutomationParamsHash.ContainsKey(str5))
                        {
                            clsGlobal.AutomationParamsHash.Add(str5, this.configDataGridView.Rows[num2].Cells[1].Value);
                        }
                    }
                }
                string str6 = helper.GetIniFileString("TEST_SETUP", "REQUIRED_AIDING", "");
                foreach (string str7 in helper.GetKeys("AIDING_PARAMS"))
                {
                    if (!str7.Contains("#"))
                    {
                        if (str6 == "1")
                        {
                            int num3 = this.configDataGridView.Rows.Add();
                            str = helper.GetIniFileString("AIDING_PARAMS", str7, "");
                            this.configDataGridView.Rows[num3].HeaderCell.Value = "AIDING_PARAMS";
                            this.configDataGridView.Rows[num3].Cells[0].Value = str7;
                            this.configDataGridView.Rows[num3].Cells[1].Value = str.TrimEnd(trimChars);
                        }
                        if (!clsGlobal.AutomationParamsHash.ContainsKey(str7))
                        {
                            clsGlobal.AutomationParamsHash.Add(str7, str.TrimEnd(trimChars));
                        }
                    }
                }
                this.Cursor = Cursors.Default;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            m_SChildform = null;
        }

        internal void updateConfigList(Hashtable updatedHash)
        {
            string key = string.Empty;
            for (int i = 0; i < (this.configDataGridView.Rows.Count - 1); i++)
            {
                if (this.configDataGridView.Rows[i].HeaderCell.Value.ToString().Trim() == "RX_SETUP")
                {
                    key = this.configDataGridView.Rows[i].Cells[0].Value.ToString();
                    this.configDataGridView.Rows[i].Cells[1].Value.ToString();
                    if (updatedHash.ContainsKey(key))
                    {
                        this.configDataGridView.Rows[i].Cells[1].Value = (string) updatedHash[key];
                    }
                }
            }
        }

        internal void updateConfigList(string updatedData)
        {
            this.configDataGridView.Rows[this.currentSelectRowIdx].Cells[this.currentSelectCellIdx].Value = updatedData;
            int num = 0;
            string str3 = updatedData;
            if (str3 != null)
            {
                if (!(str3 == "RF_PLAYBACK"))
                {
                    if (str3 == "SIM")
                    {
                        for (int i = 0; i < (this.configDataGridView.Rows.Count - 1); i++)
                        {
                            if (this.configDataGridView.Rows[i].HeaderCell.Value.ToString() == "RF_PLAYBACK")
                            {
                                this.configDataGridView.Rows.RemoveAt(i);
                                i--;
                            }
                            if (this.configDataGridView.Rows[i].HeaderCell.Value.ToString() == "SIM")
                            {
                                num++;
                            }
                        }
                    }
                    else if (str3 == "LIVE")
                    {
                        for (int j = 0; j < (this.configDataGridView.Rows.Count - 1); j++)
                        {
                            if ((this.configDataGridView.Rows[j].HeaderCell.Value.ToString() == "RF_PLAYBACK") || (this.configDataGridView.Rows[j].HeaderCell.Value.ToString() == "SIM"))
                            {
                                this.configDataGridView.Rows.RemoveAt(j);
                                j--;
                            }
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < (this.configDataGridView.Rows.Count - 1); k++)
                    {
                        if (this.configDataGridView.Rows[k].HeaderCell.Value.ToString() == "SIM")
                        {
                            this.configDataGridView.Rows.RemoveAt(k);
                            k--;
                        }
                        if (this.configDataGridView.Rows[k].HeaderCell.Value.ToString() == "RF_PLAYBACK")
                        {
                            num++;
                        }
                    }
                }
            }
            if (num == 0)
            {
                IniHelper helper = new IniHelper(this.configFilePathVal.Text);
                helper.GetSections();
                List<string> list = new List<string>();
                string str = string.Empty;
                char[] trimChars = new char[] { '\n', '\r', '\t', '\0' };
                foreach (string str2 in helper.GetKeys(updatedData))
                {
                    if (!str2.Contains("#"))
                    {
                        int num5 = this.configDataGridView.Rows.Add();
                        str = helper.GetIniFileString(updatedData, str2, "");
                        this.configDataGridView.Rows[num5].HeaderCell.Value = updatedData;
                        this.configDataGridView.Rows[num5].Cells[0].Value = str2;
                        this.configDataGridView.Rows[num5].Cells[1].Value = str.TrimEnd(trimChars);
                    }
                }
            }
        }

        private void writeNewConfig(string fPath)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            int num = 0;
            StreamWriter writer = File.CreateText(fPath);
            while (num < (this.configDataGridView.Rows.Count - 1))
            {
                if (str != this.configDataGridView.Rows[num].HeaderCell.Value.ToString().Replace(" ", ""))
                {
                    writer.WriteLine("[" + this.configDataGridView.Rows[num].HeaderCell.Value.ToString() + "]");
                }
                str2 = this.configDataGridView.Rows[num].Cells[0].Value.ToString().Replace(" ", "");
                if (str2 != "EXTRA_HOST_APP_ARGVS")
                {
                    str3 = this.configDataGridView.Rows[num].Cells[1].Value.ToString().Replace(" ", "");
                }
                else
                {
                    str3 = this.configDataGridView.Rows[num].Cells[1].Value.ToString();
                }
                writer.WriteLine(str2 + " = " + str3);
                num++;
            }
            writer.Close();
        }

        internal class configLine
        {
            private string Parameters = string.Empty;
            private string Values = string.Empty;

            internal string getKey()
            {
                return this.Parameters;
            }

            internal string getValue()
            {
                return this.Values;
            }

            internal void setKey(string keyStr)
            {
                this.Parameters = keyStr;
            }

            internal void setValue(string valStr)
            {
                this.Values = valStr;
            }
        }
    }
}


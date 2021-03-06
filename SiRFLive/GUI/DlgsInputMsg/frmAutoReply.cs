﻿namespace SiRFLive.GUI.DlgsInputMsg
{
    using CommonClassLibrary;
    using SiRFLive.Communication;
    using SiRFLive.Configuration;
    using SiRFLive.General;
    using SiRFLive.Reporting;
    using SiRFLive.Utilities;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class frmAutoReply : Form
    {
        private string _autoReplyConfigFilePath = (clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutomationSetupAutoReply.cfg");
        private int _defaultWidth;
        private bool _fileSaved;
        private frmAutoReplySummary _formautoReplySum;
        private int _maxItemWidth;
        private CheckBox auxNavChkBox;
        private Button button_autoReplySummary;
        private Button button_Browse_AcqAssistSrc_File;
        private Button button_Browse_EphSrc_ExtEphFile;
        private Button button_Browse_EphSrc_File;
        private Button button_cancel;
        private Button button_done;
        private Button button_GPSOrigin;
        private Button button_load;
        private Button button_OriginSkew;
        private Button button_Send_AcqAssist;
        private Button button_Send_EphClkCorr;
        private Button button_Send_Freq;
        private Button button_Send_Pos_Request;
        private Button button_sendNow_Alm;
        private Button button_sendNow_NavBit;
        private Button button_SET_ApproxPos;
        private Button button_SET_Freq;
        private Button button_Set_HWCfg;
        private Button button_Set_PosRequest;
        private Button button_SET_TimeTrans;
        private Button button_SetAcqAssist;
        private Button button_SetAlmanacSrc;
        private Button button_SetEphClkCorr;
        private Button button_SetNavBitSrc;
        private Button button1_CancelTTBTimeAidingCfg;
        private CheckBox checkBox_AutoReply_ApproxPos;
        private CheckBox checkBox_AutoReply_Freq;
        private CheckBox checkBox_AutoReply_HWCfg;
        private CheckBox checkBox_AutoReply_TimeTrans;
        private CheckBox checkBox_AutoSend_AcqAssist;
        private CheckBox checkBox_AutoSend_Almanac;
        private CheckBox checkBox_AutoSend_Eph;
        private CheckBox checkBox_AutoSend_NavBit;
        private CheckBox checkBox_AutoSend_PosRequest;
        private CheckBox checkBox_ForwardRequestToCP_Freq;
        private CheckBox checkBox_ForwardRequestToCP_HWCfg;
        private CheckBox checkBox_FwdReqToCP_ApproxPos;
        private CheckBox checkBox_FwdToCP_TimeTrans;
        private CheckBox checkBox_ignoreXO;
        private CheckBox checkBox_Reject_ApproxPos;
        private CheckBox checkBox_ScreenCPResp_ApproxPos;
        private CheckBox checkBox_ScreenCPResp_TimeTrans;
        private CheckBox checkBox_ScreenCPResponse_Freq;
        private CheckBox checkBox_ScreenCPResponse_HWCfg;
        private CheckBox checkBox_useTTBFreq;
        private ComboBox combo_box_profile;
        private ComboBox comboBox_ClkSrc_B1;
        private ComboBox comboBox_CoarseTimeTransAvail;
        private ComboBox comboBox_FreqTransAvail;
        private ComboBox comboBox_FreqTransMethod;
        private ComboBox comboBox_IncNormFreq_B4;
        private ComboBox comboBox_NormFreq_Freq;
        private ComboBox comboBox_NorminalFreq;
        private ComboBox comboBox_PositionMethod;
        private ComboBox comboBox_PreciseTimeAvail;
        private ComboBox comboBox_PreciseTimeTransDirection;
        private ComboBox comboBox_Priority;
        private ComboBox comboBox_RefClkOnOff_B2;
        private ComboBox comboBox_RefClkReq_B3;
        private ComboBox comboBox_RefClkStatus;
        private ComboBox comboBox_RFDefaultFreq;
        private ComboBox comboBox_RTC_Resource;
        private ComboBox comboBox_RTCAvail;
        private ComboBox comboBox_SkewPPM;
        private ComboBox comboBox_TimeTagValid;
        private CommunicationManager comm;
        private IContainer components;
        private Label ephCommentLabel;
        private ComboBox frmAutoReplyRefLocationComboBox;
        private Button frmAutoReplySetAsDefaultBtn;
        private GroupBox groupBox1;
        private GroupBox groupBox14;
        private GroupBox groupBox15;
        private GroupBox groupBox16;
        private GroupBox groupBox17;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private GroupBox groupBox8;
        private frmCommonSimpleInput inputForm = new frmCommonSimpleInput("Enter Location Name:");
        private Label label_53;
        private Label label_profileFilePath;
        private Label label1;
        private Label label10;
        private Label label13;
        private Label label15;
        private Label label17;
        private Label label18;
        private Label label2;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label label27;
        private Label label28;
        private Label label29;
        private Label label3;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label33;
        private Label label36;
        private Label label37;
        private Label label38;
        private Label label39;
        private Label label4;
        private Label label40;
        private Label label41;
        private Label label42;
        private Label label43;
        private Label label44;
        private Label label45;
        private Label label46;
        private Label label47;
        private Label label48;
        private Label label49;
        private Label label5;
        private Label label50;
        private Label label51;
        private Label label52;
        private Label label53;
        private Label label57;
        private Label label58;
        private Label label6;
        private Label label60;
        private Label label61;
        private Label label62;
        private Label label7;
        private Label label8;
        private Label label9;
        private NumericUpDown numericUpDown_EstHoriErr;
        private NumericUpDown numericUpDown_EstVertErr;
        private NumericUpDown numericUpDown_HorrErrMax;
        private NumericUpDown numericUpDown_NumOfFixes;
        private NumericUpDown numericUpDown_ResponseTimeMax;
        private NumericUpDown numericUpDown_TimeBetweenFixes;
        private NumericUpDown numericUpDown_VertErrMax;
        private RadioButton radioButton_1Hz;
        private RadioButton radioButton_2Hz;
        private RadioButton radioButton_4Hz;
        private RadioButton radioButton_AcqAssistSrc_File;
        private RadioButton radioButton_AcqAssistSrc_None;
        private RadioButton radioButton_AcqAssistSrc_TTB;
        private RadioButton radioButton_AlmanacSrc_None;
        private RadioButton radioButton_AlmanacSrc_TTB;
        private RadioButton radioButton_EphSrc_ExtEphFile;
        private RadioButton radioButton_EphSrc_File;
        private RadioButton radioButton_EphSrc_None;
        private RadioButton radioButton_EphSrc_TTB;
        private RadioButton radioButton_NavBitSrc_None;
        private RadioButton radioButton_NavBitSrc_TTB;
        private RadioButton radioButton_Reject_Freq;
        private RadioButton radioButton_Reject_TimeTrans;
        private RadioButton radioButton_SpecifyFreqParam;
        private RadioButton radioButton_useDOSTimeForCoarseTimeAiding;
        private RadioButton radioButton_UseRxRptFreq;
        private RadioButton radioButton_useTTBTimeAiding;
        private GroupBox scaleFreqOffsetGrpBox;
        private CheckBox subframe123ChkBox;
        private CheckBox subframe45ChkBox;
        private TabControl tabControl_autoReply;
        private TabPage tabPage_Aiding;
        private TabPage tabPage_ApproxPos;
        private TabPage tabPage_FreqTrans;
        private TabPage tabPage_HWCfg;
        private TabPage tabPage_Pos_Request;
        private TabPage tabPage_TimeTrans;
        private TextBox textBox_Accuracy;
        private TextBox textBox_Accuracy_TimeTrans;
        private TextBox textBox_AccuracyAuto;
        private TextBox textBox_AcqAssistSrc_File;
        private TextBox textBox_Altitude;
        private TextBox textBox_distance_skew;
        private TextBox textBox_EphSrc_ExtEphFile;
        private TextBox textBox_EphSrc_FilePath;
        private TextBox textBox_Freq;
        private TextBox textBox_FreqAuto;
        private TextBox textBox_heading_skew;
        private TextBox textBox_Latitude;
        private TextBox textBox_Longitude;
        private TextBox textBox_NWEnhanceType;
        private TextBox textBox_profileFilePath;
        private TextBox textBox_Skew_TimeTrans;
        private TextBox textBox_UtcOffset;
        private Label TTBTimeAidingAccLabel;
        private TextBox TTBTimeAidingAccTxtBox;
        private Label TTBTimeAidingAccUnitLabel;
        private Label TTBTimeAidingEnableChkboxLabel;
        private ComboBox TTBTimeAidingEnableComboBox;
        private Label TTBTimeAidingModeLabel;
        private Button TTBTimeAidingSetBtn;
        private Label TTBTimeAidingSkewLabel;
        private TextBox TTBTimeAidingSkewTxtBox;
        private Label TTBTimeAidingSkewUnitLabel;
        private ComboBox TTBTimeAidingTypeComboBox;
        private Label utcOffsetLabel;

        public frmAutoReply()
        {
            this.InitializeComponent();
            this.inputForm.updateParent += new frmCommonSimpleInput.updateParentEventHandler(this.updateConfigList);
            this.inputForm.MdiParent = base.MdiParent;
            this._defaultWidth = this.frmAutoReplyRefLocationComboBox.Width;
            this._fileSaved = false;
        }

        private void AcqAssistGUIUpdate()
        {
            if (!this.checkBox_AutoSend_AcqAssist.Checked || this.radioButton_AcqAssistSrc_None.Checked)
            {
                this.button_SetAcqAssist.ForeColor = Color.Black;
                this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_AcqData_fromTTB = false;
                this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_AcqData_fromFile = false;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.AcqAssistSource = 0;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.AcqAssistReply = 0;
            }
            else if (this.checkBox_AutoSend_AcqAssist.Checked)
            {
                this.button_SetAcqAssist.ForeColor = Color.Brown;
                if (this.radioButton_AcqAssistSrc_TTB.Checked)
                {
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_AcqData_fromTTB = true;
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_AcqData_fromFile = false;
                    this.comm.AutoReplyCtrl.PositionRequestCtrl.AcqAssistSource = 1;
                    this.comm.AutoReplyCtrl.PositionRequestCtrl.AcqAssistReply = 1;
                }
                else
                {
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_AcqData_fromFile = true;
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_AcqData_fromTTB = false;
                    this.comm.AutoReplyCtrl.PositionRequestCtrl.AcqAssistSource = 2;
                    this.comm.AutoReplyCtrl.PositionRequestCtrl.AcqAssistReply = 1;
                    this.comm.AutoReplyCtrl.AcqDataFilePath = this.textBox_AcqAssistSrc_File.Text;
                }
            }
        }

        private void AlmanacSrcGUIUpdate()
        {
            if (!this.checkBox_AutoSend_Almanac.Checked || this.radioButton_AlmanacSrc_None.Checked)
            {
                this.button_SetAlmanacSrc.ForeColor = Color.Black;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.AlmSource = 0;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.AlmReply = 0;
            }
            else if (this.checkBox_AutoSend_Almanac.Checked)
            {
                this.button_SetAlmanacSrc.ForeColor = Color.Brown;
                if (this.radioButton_AlmanacSrc_TTB.Checked)
                {
                    this.comm.AutoReplyCtrl.AlmMsgFromTTB = this.comm.AutoReplyCtrl.SetupAlmfromTTB(this.comm.GetAlmanacDataFromTTB());
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Alm = true;
                    this.comm.AutoReplyCtrl.PositionRequestCtrl.AlmSource = 1;
                    this.comm.AutoReplyCtrl.PositionRequestCtrl.AlmReply = 1;
                }
            }
        }

        private void approxPosGUIUpdate()
        {
            this.comm.AutoReplyCtrl.ApproxPositionCtrl.Reply = this.checkBox_AutoReply_ApproxPos.Checked;
            this.comm.AutoReplyCtrl.ApproxPositionCtrl.Reject = this.checkBox_Reject_ApproxPos.Checked;
            this.comm.m_NavData.RefLocationName = this.frmAutoReplyRefLocationComboBox.Text;
            try
            {
                this.comm.AutoReplyCtrl.ApproxPositionCtrl.EstHorrErr = (double) this.numericUpDown_EstHoriErr.Value;
                this.comm.AutoReplyCtrl.ApproxPositionCtrl.EstVertiErr = (double) this.numericUpDown_EstVertErr.Value;
                this.comm.AutoReplyCtrl.ApproxPositionCtrl.Lat = Convert.ToDouble(this.textBox_Latitude.Text);
                this.comm.AutoReplyCtrl.ApproxPositionCtrl.DistanceSkew = Convert.ToDouble(this.textBox_distance_skew.Text);
                this.comm.AutoReplyCtrl.ApproxPositionCtrl.Lon = Convert.ToDouble(this.textBox_Longitude.Text);
                this.comm.AutoReplyCtrl.ApproxPositionCtrl.HeadingSkew = Convert.ToDouble(this.textBox_heading_skew.Text);
                this.comm.AutoReplyCtrl.ApproxPositionCtrl.Alt = Convert.ToDouble(this.textBox_Altitude.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Position: " + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            this.comm.AutoReplyCtrl.AutoReplyApproxPositionResp();
            if (this.checkBox_AutoReply_ApproxPos.Checked)
            {
                this.button_SET_ApproxPos.ForeColor = Color.Brown;
            }
            else
            {
                this.button_SET_ApproxPos.ForeColor = Color.Black;
            }
            this.comm.AutoReplyCtrl.AutoReplyParams.AutoReplyApproxPos = this.comm.AutoReplyCtrl.ApproxPositionCtrl.Reply;
            this.comm.AutoReplyCtrl.UpdateAutoReplyStatus();
        }

        private void auxNavChkBox_CheckedChanged(object sender, EventArgs e)
        {
            byte networkEnhanceType = this.comm.AutoReplyCtrl.HWCfgCtrl.NetworkEnhanceType;
            if (this.auxNavChkBox.Checked)
            {
                networkEnhanceType = (byte) (networkEnhanceType | 4);
            }
            else
            {
                networkEnhanceType = (byte) (networkEnhanceType & 0xfb);
            }
            this.textBox_NWEnhanceType.Text = networkEnhanceType.ToString();
        }

        private void button_autoReplySummary_Click(object sender, EventArgs e)
        {
            this.createAutoReplySummaryWindow();
        }

        private void button_Browse_AcqAssistSrc_File_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = clsGlobal.InstalledDirectory;
            dialog.Filter = "Acquisition files (*.acq)|*.acq|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_AcqAssistSrc_File.Text = dialog.FileName;
            }
        }

        private void button_Browse_EphSrc_ExtEphFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = clsGlobal.InstalledDirectory;
            dialog.Filter = "Ephemeris files (*.eph)|*.eph|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_EphSrc_ExtEphFile.Text = dialog.FileName;
            }
        }

        private void button_Browse_EphSrc_File_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = clsGlobal.InstalledDirectory;
            dialog.Filter = "Ephemeris files (*.eph)|*.eph|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_EphSrc_FilePath.Text = dialog.FileName;
            }
        }

        private void button_done_Click(object sender, EventArgs e)
        {
            this.hwConfigGUIUpdate();
            this.freqGUIUpdate();
            this.approxPosGUIUpdate();
            this.TimeTransGUIUpdate();
            this.PosRequestGUIUpdate();
            this.EphAidingGUIUpdate();
            this.AlmanacSrcGUIUpdate();
            this.AcqAssistGUIUpdate();
            this.NavBitSrcGUIUpdate();
            this._fileSaved = true;
            this.saveAutoReplyData(this._autoReplyConfigFilePath);
            string path = string.Empty;
            switch (this.combo_box_profile.SelectedIndex)
            {
                case 1:
                    path = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyAutonomous.cfg";
                    break;

                case 2:
                    path = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyMSA1Coarse.cfg";
                    break;

                case 3:
                    path = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyMSA1Precise.cfg";
                    break;

                case 4:
                    path = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyMSA2Coarse.cfg";
                    break;

                case 5:
                    path = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyMSA2Precise.cfg";
                    break;

                case 6:
                    path = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyMSBCoarse.cfg";
                    break;

                case 7:
                    path = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyMSBPrecise.cfg";
                    break;

                case 8:
                    path = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyTestMode6.cfg";
                    break;

                case 9:
                    path = this.textBox_profileFilePath.Text;
                    break;
            }
            if ((this.combo_box_profile.SelectedIndex == 9) && (path != this._autoReplyConfigFilePath))
            {
                if (File.Exists(path))
                {
                    switch (MessageBox.Show(string.Format("File exists -- Overwrite?\n{0}", path), "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
                    {
                        case DialogResult.Yes:
                            if ((File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                            {
                                MessageBox.Show("File is read only!\nPlease change property and retry", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                return;
                            }
                            File.Delete(path);
                            File.Copy(this._autoReplyConfigFilePath, path);
                            break;

                        case DialogResult.Cancel:
                            return;
                    }
                }
                else
                {
                    File.Copy(this._autoReplyConfigFilePath, path);
                }
            }
            this.comm.AutoReplyCtrl.UpdateAutoReplyStatus();
            bool autoReply = this.comm.AutoReplyCtrl.AutoReplyParams.AutoReply;
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void button_GPSOrigin_Click(object sender, EventArgs e)
        {
        }

        private void button_load_Click(object sender, EventArgs e)
        {
            string filepath = this._autoReplyConfigFilePath;
            switch (this.combo_box_profile.SelectedIndex)
            {
                case 0:
                    filepath = this._autoReplyConfigFilePath;
                    break;

                case 1:
                    filepath = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyAutonomous.cfg";
                    break;

                case 2:
                    filepath = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyMSA1Coarse.cfg";
                    break;

                case 3:
                    filepath = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyMSA1Precise.cfg";
                    break;

                case 4:
                    filepath = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyMSA2Coarse.cfg";
                    break;

                case 5:
                    filepath = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyMSA2Precise.cfg";
                    break;

                case 6:
                    filepath = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyMSBCoarse.cfg";
                    break;

                case 7:
                    filepath = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyMSBPrecise.cfg";
                    break;

                case 8:
                    filepath = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutoReplyTestMode6.cfg";
                    break;

                case 9:
                    filepath = this.textBox_profileFilePath.Text;
                    break;
            }
            this.comm.ReadAutoReplyData(filepath);
            this.frmAutoReplyLoad();
            this.textBox_profileFilePath.Text = filepath;
            this.Text = this.comm.PortName + " Auto Reply " + this.combo_box_profile.Text;
        }

        private void button_OriginSkew_Click(object sender, EventArgs e)
        {
        }

        private void button_Send_AcqAssist_Click(object sender, EventArgs e)
        {
            string acqAssistDataFromTTB = this.comm.GetAcqAssistDataFromTTB();
            this.comm.WriteData(acqAssistDataFromTTB);
        }

        private void button_Send_EphClkCorr_Click(object sender, EventArgs e)
        {
            string ephDataFromTTB = this.comm.GetEphDataFromTTB();
            this.comm.WriteData(ephDataFromTTB);
        }

        private void button_Send_Freq_Click(object sender, EventArgs e)
        {
            string msg = this.setupFreqRespMsgs();
            this.comm.WriteData(msg);
        }

        private void button_Send_Pos_Request_Click(object sender, EventArgs e)
        {
            string msg = this.setupPosRequesMsg();
            this.comm.WriteData(msg);
        }

        private void button_sendNow_Alm_Click(object sender, EventArgs e)
        {
            string msg = this.comm.AutoReplyCtrl.SetupAlmfromTTB(this.comm.GetAlmanacDataFromTTB());
            this.comm.WriteData(msg);
        }

        private void button_sendNow_NavBit_Click(object sender, EventArgs e)
        {
            string msg = this.comm.AutoReplyCtrl.SetupNavSF123fromTTB(this.comm.GetNavBitSF123FromTTB());
            string str2 = this.comm.AutoReplyCtrl.SetupNavSF45FromTTB(this.comm.GetNavBitSF45DataSet0FromTTB());
            this.comm.WriteData(msg);
            this.comm.WriteData(str2);
        }

        private void button_SET_ApproxPos_Click(object sender, EventArgs e)
        {
            this.approxPosGUIUpdate();
        }

        private void button_SET_Freq_Click(object sender, EventArgs e)
        {
            this.freqGUIUpdate();
        }

        private void button_Set_PosRequest_Click(object sender, EventArgs e)
        {
            this.PosRequestGUIUpdate();
        }

        private void button_SET_TimeTrans_Click(object sender, EventArgs e)
        {
            this.TimeTransGUIUpdate();
        }

        private void button_SetAcqAssist_Click(object sender, EventArgs e)
        {
            this.AcqAssistGUIUpdate();
        }

        private void button_SetAlmanacSrc_Click(object sender, EventArgs e)
        {
            this.AlmanacSrcGUIUpdate();
        }

        private void button_SetEphClkCorr_Click(object sender, EventArgs e)
        {
            this.EphAidingGUIUpdate();
        }

        private void button_SetNavBitSrc_Click(object sender, EventArgs e)
        {
            this.NavBitSrcGUIUpdate();
        }

        private void button1_CancelTTBTimeAidingCfg_Click(object sender, EventArgs e)
        {
            this.setTTBTimeAiding();
            this.TTBTimeAidingSetBtn.ForeColor = Color.Black;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this._fileSaved = true;
            base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            base.Close();
        }

        private void checkBox_ignoreXO_CheckedChanged(object sender, EventArgs e)
        {
            this.comm.AutoReplyCtrl.AutoReplyParams.FreqAidingIgnoreXO = this.checkBox_ignoreXO.Checked;
        }

        private void checkBox_useTTBFreq_CheckedChanged(object sender, EventArgs e)
        {
            this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForFreqAid = this.checkBox_useTTBFreq.Checked;
        }

        private void combo_box_profile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.combo_box_profile.SelectedIndex == 9)
            {
                MessageBox.Show("Please enter the absolute file path in the \"Config File Path\" text box");
            }
        }

        private void comboBox_ClkSrc_B1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updateCtrlsWhenClkSrcChange();
            this.comboBox_FreqTransMethod.SelectedIndex = this.comboBox_ClkSrc_B1.SelectedIndex;
            this.updateRefClkNomFreqWhenFreqTransMethodChange();
        }

        private void comboBox_FreqTransAvail_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updatewhenFreqTransAvailchange();
        }

        private void comboBox_FreqTransMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updateRefClkNomFreqWhenFreqTransMethodChange();
            this.comboBox_ClkSrc_B1.SelectedIndex = this.comboBox_FreqTransMethod.SelectedIndex;
            this.updateCtrlsWhenClkSrcChange();
        }

        private void comboBox_IncNormFreq_B4_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updateCtrlsWhenIncNormFreqChange();
        }

        private void comboBox_NormFreq_Freq_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox_NorminalFreq.SelectedIndex = this.comboBox_NormFreq_Freq.SelectedIndex;
        }

        private void comboBox_NorminalFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox_NormFreq_Freq.SelectedIndex = this.comboBox_NorminalFreq.SelectedIndex;
        }

        private void comboBox_RefClkOnOff_B2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox_RefClkStatus.SelectedIndex = this.comboBox_RefClkOnOff_B2.SelectedIndex;
        }

        private void comboBox_RefClkStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox_RefClkOnOff_B2.SelectedIndex = this.comboBox_RefClkStatus.SelectedIndex;
        }

        private void createAutoReplySummaryWindow()
        {
            if (!base.IsDisposed)
            {
                string str = this.comm.sourceDeviceName + ": Auto Reply Summary";
                if ((this._formautoReplySum == null) || this._formautoReplySum.IsDisposed)
                {
                    this._formautoReplySum = new frmAutoReplySummary();
                }
                this._formautoReplySum.CommWindow = this.comm;
                this._formautoReplySum.Text = str;
                this._formautoReplySum.ShowDialog();
            }
            else
            {
                MessageBox.Show("COM window not initialized!", "Information");
            }
        }

        private void disableEnableFreqTransAutoReplyPage(bool status)
        {
            this.comboBox_ClkSrc_B1.Enabled = status;
            this.comboBox_RefClkReq_B3.Enabled = status;
            this.comboBox_RefClkOnOff_B2.Enabled = status;
            this.comboBox_IncNormFreq_B4.Enabled = status;
            this.comboBox_RFDefaultFreq.Enabled = status;
            this.comboBox_NormFreq_Freq.Enabled = status;
            this.comboBox_SkewPPM.Enabled = status;
            this.radioButton_SpecifyFreqParam.Enabled = status;
            this.radioButton_UseRxRptFreq.Enabled = status;
            this.textBox_Freq.Enabled = status;
            this.textBox_FreqAuto.Enabled = status;
            this.textBox_Accuracy.Enabled = status;
            this.textBox_AccuracyAuto.Enabled = status;
            this.radioButton_1Hz.Enabled = status;
            this.radioButton_2Hz.Enabled = status;
            this.radioButton_4Hz.Enabled = status;
            this.comboBox_TimeTagValid.Enabled = status;
            this.checkBox_AutoReply_Freq.Checked = status;
            this.checkBox_AutoReply_Freq.Enabled = status;
            this.checkBox_useTTBFreq.Enabled = status;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EphAidingGUIUpdate()
        {
            if (!this.checkBox_AutoSend_Eph.Checked || this.radioButton_EphSrc_None.Checked)
            {
                this.button_SetEphClkCorr.ForeColor = Color.Black;
                this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromTTB = false;
                this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromFile = false;
                this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_ExtEph_fromFile = false;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.EphSource = 0;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.EphReply = 0;
            }
            else if (this.checkBox_AutoSend_Eph.Checked)
            {
                this.button_SetEphClkCorr.ForeColor = Color.Brown;
                if (this.radioButton_EphSrc_TTB.Checked)
                {
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromTTB = true;
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromFile = false;
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_ExtEph_fromFile = false;
                    this.comm.AutoReplyCtrl.PositionRequestCtrl.EphSource = 2;
                    this.comm.AutoReplyCtrl.PositionRequestCtrl.EphReply = 1;
                }
                else if (this.radioButton_EphSrc_File.Checked)
                {
                    if ((this.textBox_EphSrc_FilePath.Text.Length == 0) || !File.Exists(this.textBox_EphSrc_FilePath.Text))
                    {
                        MessageBox.Show("File does not exist", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromFile = true;
                        this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromTTB = false;
                        this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_ExtEph_fromFile = false;
                        this.comm.AutoReplyCtrl.EphFilePath = this.textBox_EphSrc_FilePath.Text;
                        this.comm.AutoReplyCtrl.PositionRequestCtrl.EphSource = 1;
                        this.comm.AutoReplyCtrl.PositionRequestCtrl.EphReply = 1;
                    }
                }
                else if ((this.textBox_EphSrc_ExtEphFile.Text.Length == 0) || !File.Exists(this.textBox_EphSrc_ExtEphFile.Text))
                {
                    MessageBox.Show("File does not exist", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromFile = false;
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromTTB = false;
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_ExtEph_fromFile = true;
                    this.comm.AutoReplyCtrl.EphFilePath = this.textBox_EphSrc_ExtEphFile.Text;
                    this.comm.AutoReplyCtrl.PositionRequestCtrl.EphSource = 3;
                    this.comm.AutoReplyCtrl.PositionRequestCtrl.EphReply = 1;
                }
            }
        }

        private void freqGUIUpdate()
        {
            this.comm.AutoReplyCtrl.FreqTransferCtrl.Reply = this.checkBox_AutoReply_Freq.Checked;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.Reject = this.radioButton_Reject_Freq.Checked;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqAidingMethod = this.comboBox_ClkSrc_B1.SelectedIndex;
            this.comm.AutoReplyCtrl.AutoReplyParams.AutoReply = true;
            this.comm.AutoReplyCtrl.AutoReplyParams.AutoReplyFreqTrans = true;
            this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForFreqAid = this.checkBox_useTTBFreq.Checked;
            this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForHwCfg = this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForFreqAid || this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForTimeAid;
            this.setupFreqRespMsgs();
            if (!this.checkBox_AutoReply_Freq.Checked)
            {
                this.button_SET_Freq.ForeColor = Color.Black;
            }
            else
            {
                this.button_SET_Freq.ForeColor = Color.Brown;
            }
            this.comm.AutoReplyCtrl.AutoReplyParams.AutoReplyFreqTrans = this.comm.AutoReplyCtrl.FreqTransferCtrl.Reply;
            this.comm.AutoReplyCtrl.UpdateAutoReplyStatus();
        }

        private void frmAutoReplyLoad()
        {
            this.updateDefautlReferenceLocationComboBox();
            this.setLastTimeValues();
            this.setTimeAidingGUIStatus();
            this.updatewhenFreqTransAvailchange();
            this.updateRefClkNomFreqWhenFreqTransMethodChange();
            this.comboBox_ClkSrc_B1.SelectedIndex = this.comboBox_FreqTransMethod.SelectedIndex;
            this.updateCtrlsWhenClkSrcChange();
            byte networkEnhanceType = this.comm.AutoReplyCtrl.HWCfgCtrl.NetworkEnhanceType;
            if ((networkEnhanceType & 4) == 4)
            {
                this.auxNavChkBox.Checked = true;
            }
            else
            {
                this.auxNavChkBox.Checked = false;
            }
            if ((networkEnhanceType & 8) == 8)
            {
                this.subframe123ChkBox.Checked = true;
            }
            else
            {
                this.subframe123ChkBox.Checked = false;
            }
            if ((networkEnhanceType & 0x10) == 0x10)
            {
                this.subframe45ChkBox.Checked = true;
            }
            else
            {
                this.subframe45ChkBox.Checked = false;
            }
            this.updateFreqCtrlsWhenRadioChange();
            this.updateCtrlsWhenClkSrcChange();
            this.comboBox_FreqTransMethod.SelectedIndex = this.comboBox_ClkSrc_B1.SelectedIndex;
            this.updateRefClkNomFreqWhenFreqTransMethodChange();
            this.comboBox_RefClkStatus.SelectedIndex = this.comboBox_RefClkOnOff_B2.SelectedIndex;
            this.updateCtrlsWhenIncNormFreqChange();
            this.updateAidingCtrlsOnAcqAssistSrcRadioChange();
            this.updateAidingCtrlsOnEphSrcRadioChange();
            this.updateAidingCtrlsOnNavBitSrcRadioChange();
        }

        private void frmAutoReplyRefLocationComboBox_DropDown(object sender, EventArgs e)
        {
            this.frmAutoReplyRefLocationComboBox.Width = this._maxItemWidth;
        }

        private void frmAutoReplyRefLocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.frmAutoReplyRefLocationComboBox.SelectedItem.ToString() != "USER_DEFINED")
            {
                this.comm.m_NavData.RefLocationName = this.frmAutoReplyRefLocationComboBox.SelectedItem.ToString();
                this.updateReferenceLocationComboBox();
            }
            else
            {
                this.inputForm.UpdateType = "UPDATE_REF_NAME";
                this.inputForm.ShowDialog();
            }
        }

        private void frmAutoReplySetAsDefaultBtn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.comm.m_NavData.SetDefaultReferencePosition(this.frmAutoReplyRefLocationComboBox.SelectedItem.ToString(), this.textBox_Latitude.Text, this.textBox_Longitude.Text, this.textBox_Altitude.Text, true);
            this.Cursor = Cursors.Default;
        }

        private void frmAutpReplyConfig_Load(object sender, EventArgs e)
        {
            this.setDefault();
            this.frmAutoReplyLoad();
            this.button_OriginSkew.Visible = false;
            this.button_GPSOrigin.Visible = false;
            this.groupBox15.Visible = false;
            this.button_SetAlmanacSrc.Visible = false;
            this.button_sendNow_Alm.Visible = false;
            this.scaleFreqOffsetGrpBox.Visible = false;
        }

        private void hwConfigGUIUpdate()
        {
            this.comm.AutoReplyCtrl.HWCfgCtrl.Reply = this.checkBox_AutoReply_HWCfg.Checked;
            this.comm.AutoReplyCtrl.HWCfgCtrl.PreciseTimeEnabled = Convert.ToByte(this.comboBox_PreciseTimeAvail.SelectedIndex);
            this.comm.AutoReplyCtrl.HWCfgCtrl.PreciseTimeDirection = Convert.ToByte(this.comboBox_PreciseTimeTransDirection.SelectedIndex);
            this.comm.AutoReplyCtrl.HWCfgCtrl.FreqAidEnabled = Convert.ToByte(this.comboBox_FreqTransAvail.SelectedIndex);
            this.comm.AutoReplyCtrl.HWCfgCtrl.FreqAidMethod = Convert.ToByte(this.comboBox_FreqTransMethod.SelectedIndex);
            this.comm.AutoReplyCtrl.HWCfgCtrl.RTCAvailabe = Convert.ToByte(this.comboBox_RTCAvail.SelectedIndex);
            this.comm.AutoReplyCtrl.HWCfgCtrl.RTCSource = Convert.ToByte(this.comboBox_RTC_Resource.SelectedIndex);
            this.comm.AutoReplyCtrl.HWCfgCtrl.CoarseTimeEnabled = Convert.ToByte(this.comboBox_CoarseTimeTransAvail.SelectedIndex);
            this.comm.AutoReplyCtrl.HWCfgCtrl.RefClkEnabled = Convert.ToByte(this.comboBox_RefClkStatus.SelectedIndex);
            this.comm.AutoReplyCtrl.HWCfgCtrl.NorminalFreqHz = Convert.ToInt64(this.comboBox_NorminalFreq.Text);
            try
            {
                this.comm.AutoReplyCtrl.HWCfgCtrl.NetworkEnhanceType = Convert.ToByte(this.textBox_NWEnhanceType.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Enhanced Network: " + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            if (this.checkBox_AutoReply_HWCfg.Checked)
            {
                this.comm.AutoReplyCtrl.AutoReplyHWCfgResp();
                this.button_Set_HWCfg.ForeColor = Color.Brown;
            }
            else
            {
                this.comm.AutoReplyCtrl.UpdateAutoReplyStatus();
                this.button_Set_HWCfg.ForeColor = Color.Black;
            }
            this.comm.AutoReplyCtrl.AutoReplyParams.AutoReplyHWCfg = this.comm.AutoReplyCtrl.HWCfgCtrl.Reply;
            this.comm.AutoReplyCtrl.UpdateAutoReplyStatus();
        }

        private void hwConfigSetBtn_Click(object sender, EventArgs e)
        {
            this.hwConfigGUIUpdate();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmAutoReply));
            this.comboBox_PreciseTimeAvail = new ComboBox();
            this.comboBox_PreciseTimeTransDirection = new ComboBox();
            this.comboBox_FreqTransAvail = new ComboBox();
            this.comboBox_FreqTransMethod = new ComboBox();
            this.comboBox_RTCAvail = new ComboBox();
            this.comboBox_RTC_Resource = new ComboBox();
            this.comboBox_CoarseTimeTransAvail = new ComboBox();
            this.comboBox_RefClkStatus = new ComboBox();
            this.comboBox_NorminalFreq = new ComboBox();
            this.groupBox1 = new GroupBox();
            this.checkBox_ScreenCPResponse_HWCfg = new CheckBox();
            this.checkBox_ForwardRequestToCP_HWCfg = new CheckBox();
            this.checkBox_AutoReply_HWCfg = new CheckBox();
            this.button_Set_HWCfg = new Button();
            this.button_cancel = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.label9 = new Label();
            this.label13 = new Label();
            this.label15 = new Label();
            this.textBox_NWEnhanceType = new TextBox();
            this.label10 = new Label();
            this.tabControl_autoReply = new TabControl();
            this.tabPage_HWCfg = new TabPage();
            this.subframe45ChkBox = new CheckBox();
            this.subframe123ChkBox = new CheckBox();
            this.auxNavChkBox = new CheckBox();
            this.tabPage_FreqTrans = new TabPage();
            this.radioButton_Reject_Freq = new RadioButton();
            this.button_Send_Freq = new Button();
            this.button_SET_Freq = new Button();
            this.scaleFreqOffsetGrpBox = new GroupBox();
            this.radioButton_4Hz = new RadioButton();
            this.radioButton_2Hz = new RadioButton();
            this.radioButton_1Hz = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.checkBox_ScreenCPResponse_Freq = new CheckBox();
            this.checkBox_ForwardRequestToCP_Freq = new CheckBox();
            this.checkBox_AutoReply_Freq = new CheckBox();
            this.groupBox4 = new GroupBox();
            this.checkBox_ignoreXO = new CheckBox();
            this.comboBox_TimeTagValid = new ComboBox();
            this.checkBox_useTTBFreq = new CheckBox();
            this.label17 = new Label();
            this.label18 = new Label();
            this.comboBox_SkewPPM = new ComboBox();
            this.comboBox_NormFreq_Freq = new ComboBox();
            this.comboBox_IncNormFreq_B4 = new ComboBox();
            this.comboBox_RefClkReq_B3 = new ComboBox();
            this.comboBox_RefClkOnOff_B2 = new ComboBox();
            this.comboBox_ClkSrc_B1 = new ComboBox();
            this.label20 = new Label();
            this.label21 = new Label();
            this.label22 = new Label();
            this.label23 = new Label();
            this.label24 = new Label();
            this.label25 = new Label();
            this.label26 = new Label();
            this.radioButton_UseRxRptFreq = new RadioButton();
            this.radioButton_SpecifyFreqParam = new RadioButton();
            this.textBox_AccuracyAuto = new TextBox();
            this.textBox_Accuracy = new TextBox();
            this.textBox_FreqAuto = new TextBox();
            this.textBox_Freq = new TextBox();
            this.comboBox_RFDefaultFreq = new ComboBox();
            this.label27 = new Label();
            this.label28 = new Label();
            this.label29 = new Label();
            this.label30 = new Label();
            this.label31 = new Label();
            this.label32 = new Label();
            this.label33 = new Label();
            this.tabPage_ApproxPos = new TabPage();
            this.label62 = new Label();
            this.frmAutoReplySetAsDefaultBtn = new Button();
            this.frmAutoReplyRefLocationComboBox = new ComboBox();
            this.button_GPSOrigin = new Button();
            this.button_OriginSkew = new Button();
            this.button_SET_ApproxPos = new Button();
            this.groupBox5 = new GroupBox();
            this.checkBox_ScreenCPResp_ApproxPos = new CheckBox();
            this.checkBox_FwdReqToCP_ApproxPos = new CheckBox();
            this.checkBox_AutoReply_ApproxPos = new CheckBox();
            this.checkBox_Reject_ApproxPos = new CheckBox();
            this.numericUpDown_EstVertErr = new NumericUpDown();
            this.numericUpDown_EstHoriErr = new NumericUpDown();
            this.textBox_heading_skew = new TextBox();
            this.textBox_Longitude = new TextBox();
            this.textBox_distance_skew = new TextBox();
            this.textBox_Latitude = new TextBox();
            this.textBox_Altitude = new TextBox();
            this.label36 = new Label();
            this.label37 = new Label();
            this.label38 = new Label();
            this.label39 = new Label();
            this.label40 = new Label();
            this.label41 = new Label();
            this.label42 = new Label();
            this.label43 = new Label();
            this.label44 = new Label();
            this.tabPage_TimeTrans = new TabPage();
            this.groupBox8 = new GroupBox();
            this.button1_CancelTTBTimeAidingCfg = new Button();
            this.TTBTimeAidingSkewUnitLabel = new Label();
            this.TTBTimeAidingTypeComboBox = new ComboBox();
            this.TTBTimeAidingEnableChkboxLabel = new Label();
            this.TTBTimeAidingSkewTxtBox = new TextBox();
            this.TTBTimeAidingAccUnitLabel = new Label();
            this.TTBTimeAidingEnableComboBox = new ComboBox();
            this.TTBTimeAidingAccTxtBox = new TextBox();
            this.TTBTimeAidingModeLabel = new Label();
            this.TTBTimeAidingAccLabel = new Label();
            this.TTBTimeAidingSkewLabel = new Label();
            this.TTBTimeAidingSetBtn = new Button();
            this.utcOffsetLabel = new Label();
            this.radioButton_useTTBTimeAiding = new RadioButton();
            this.button_SET_TimeTrans = new Button();
            this.radioButton_Reject_TimeTrans = new RadioButton();
            this.label45 = new Label();
            this.radioButton_useDOSTimeForCoarseTimeAiding = new RadioButton();
            this.groupBox6 = new GroupBox();
            this.checkBox_ScreenCPResp_TimeTrans = new CheckBox();
            this.checkBox_FwdToCP_TimeTrans = new CheckBox();
            this.checkBox_AutoReply_TimeTrans = new CheckBox();
            this.textBox_Accuracy_TimeTrans = new TextBox();
            this.label46 = new Label();
            this.textBox_Skew_TimeTrans = new TextBox();
            this.textBox_UtcOffset = new TextBox();
            this.tabPage_Pos_Request = new TabPage();
            this.label61 = new Label();
            this.label60 = new Label();
            this.label57 = new Label();
            this.label58 = new Label();
            this.button_Send_Pos_Request = new Button();
            this.checkBox_AutoSend_PosRequest = new CheckBox();
            this.label47 = new Label();
            this.numericUpDown_ResponseTimeMax = new NumericUpDown();
            this.label48 = new Label();
            this.button_Set_PosRequest = new Button();
            this.label49 = new Label();
            this.numericUpDown_VertErrMax = new NumericUpDown();
            this.label51 = new Label();
            this.label50 = new Label();
            this.numericUpDown_HorrErrMax = new NumericUpDown();
            this.label52 = new Label();
            this.comboBox_PositionMethod = new ComboBox();
            this.label_53 = new Label();
            this.comboBox_Priority = new ComboBox();
            this.numericUpDown_TimeBetweenFixes = new NumericUpDown();
            this.numericUpDown_NumOfFixes = new NumericUpDown();
            this.tabPage_Aiding = new TabPage();
            this.button_sendNow_NavBit = new Button();
            this.button_sendNow_Alm = new Button();
            this.button_SetNavBitSrc = new Button();
            this.button_SetAlmanacSrc = new Button();
            this.groupBox16 = new GroupBox();
            this.textBox_AcqAssistSrc_File = new TextBox();
            this.checkBox_AutoSend_AcqAssist = new CheckBox();
            this.radioButton_AcqAssistSrc_None = new RadioButton();
            this.button_Browse_AcqAssistSrc_File = new Button();
            this.radioButton_AcqAssistSrc_TTB = new RadioButton();
            this.radioButton_AcqAssistSrc_File = new RadioButton();
            this.groupBox15 = new GroupBox();
            this.checkBox_AutoSend_Almanac = new CheckBox();
            this.radioButton_AlmanacSrc_None = new RadioButton();
            this.radioButton_AlmanacSrc_TTB = new RadioButton();
            this.groupBox14 = new GroupBox();
            this.checkBox_AutoSend_NavBit = new CheckBox();
            this.radioButton_NavBitSrc_None = new RadioButton();
            this.radioButton_NavBitSrc_TTB = new RadioButton();
            this.button_Send_AcqAssist = new Button();
            this.button_Send_EphClkCorr = new Button();
            this.groupBox17 = new GroupBox();
            this.textBox_EphSrc_FilePath = new TextBox();
            this.checkBox_AutoSend_Eph = new CheckBox();
            this.textBox_EphSrc_ExtEphFile = new TextBox();
            this.button_Browse_EphSrc_File = new Button();
            this.button_Browse_EphSrc_ExtEphFile = new Button();
            this.radioButton_EphSrc_File = new RadioButton();
            this.radioButton_EphSrc_ExtEphFile = new RadioButton();
            this.radioButton_EphSrc_TTB = new RadioButton();
            this.radioButton_EphSrc_None = new RadioButton();
            this.button_SetAcqAssist = new Button();
            this.button_SetEphClkCorr = new Button();
            this.button_done = new Button();
            this.combo_box_profile = new ComboBox();
            this.button_load = new Button();
            this.textBox_profileFilePath = new TextBox();
            this.label_profileFilePath = new Label();
            this.button_autoReplySummary = new Button();
            this.groupBox7 = new GroupBox();
            this.label53 = new Label();
            this.ephCommentLabel = new Label();
            this.groupBox1.SuspendLayout();
            this.tabControl_autoReply.SuspendLayout();
            this.tabPage_HWCfg.SuspendLayout();
            this.tabPage_FreqTrans.SuspendLayout();
            this.scaleFreqOffsetGrpBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage_ApproxPos.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.numericUpDown_EstVertErr.BeginInit();
            this.numericUpDown_EstHoriErr.BeginInit();
            this.tabPage_TimeTrans.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPage_Pos_Request.SuspendLayout();
            this.numericUpDown_ResponseTimeMax.BeginInit();
            this.numericUpDown_VertErrMax.BeginInit();
            this.numericUpDown_HorrErrMax.BeginInit();
            this.numericUpDown_TimeBetweenFixes.BeginInit();
            this.numericUpDown_NumOfFixes.BeginInit();
            this.tabPage_Aiding.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox7.SuspendLayout();
            base.SuspendLayout();
            this.comboBox_PreciseTimeAvail.FormattingEnabled = true;
            this.comboBox_PreciseTimeAvail.Items.AddRange(new object[] { "NO", "YES" });
            this.comboBox_PreciseTimeAvail.Location = new Point(0x101, 30);
            this.comboBox_PreciseTimeAvail.Name = "comboBox_PreciseTimeAvail";
            this.comboBox_PreciseTimeAvail.Size = new Size(0x79, 0x15);
            this.comboBox_PreciseTimeAvail.TabIndex = 0;
            this.comboBox_PreciseTimeTransDirection.FormattingEnabled = true;
            this.comboBox_PreciseTimeTransDirection.Items.AddRange(new object[] { "CP --> SLC", "CP <-- SLC" });
            this.comboBox_PreciseTimeTransDirection.Location = new Point(0x101, 0x33);
            this.comboBox_PreciseTimeTransDirection.Name = "comboBox_PreciseTimeTransDirection";
            this.comboBox_PreciseTimeTransDirection.Size = new Size(0x79, 0x15);
            this.comboBox_PreciseTimeTransDirection.TabIndex = 1;
            this.comboBox_FreqTransAvail.FormattingEnabled = true;
            this.comboBox_FreqTransAvail.Items.AddRange(new object[] { "NO", "YES" });
            this.comboBox_FreqTransAvail.Location = new Point(0x101, 0x48);
            this.comboBox_FreqTransAvail.Name = "comboBox_FreqTransAvail";
            this.comboBox_FreqTransAvail.Size = new Size(0x79, 0x15);
            this.comboBox_FreqTransAvail.TabIndex = 2;
            this.comboBox_FreqTransAvail.SelectedIndexChanged += new EventHandler(this.comboBox_FreqTransAvail_SelectedIndexChanged);
            this.comboBox_FreqTransMethod.FormattingEnabled = true;
            this.comboBox_FreqTransMethod.Items.AddRange(new object[] { "Counter (Ref. Clk)", "Non-Counter (SLC Clk)" });
            this.comboBox_FreqTransMethod.Location = new Point(0x101, 0x5d);
            this.comboBox_FreqTransMethod.Name = "comboBox_FreqTransMethod";
            this.comboBox_FreqTransMethod.Size = new Size(0x79, 0x15);
            this.comboBox_FreqTransMethod.TabIndex = 3;
            this.comboBox_FreqTransMethod.SelectedIndexChanged += new EventHandler(this.comboBox_FreqTransMethod_SelectedIndexChanged);
            this.comboBox_RTCAvail.FormattingEnabled = true;
            this.comboBox_RTCAvail.Items.AddRange(new object[] { "NO", "YES" });
            this.comboBox_RTCAvail.Location = new Point(0x101, 0x9c);
            this.comboBox_RTCAvail.Name = "comboBox_RTCAvail";
            this.comboBox_RTCAvail.Size = new Size(0x79, 0x15);
            this.comboBox_RTCAvail.TabIndex = 4;
            this.comboBox_RTC_Resource.FormattingEnabled = true;
            this.comboBox_RTC_Resource.Items.AddRange(new object[] { "External to GPS", "Internal to GPS" });
            this.comboBox_RTC_Resource.Location = new Point(0x101, 0xb1);
            this.comboBox_RTC_Resource.Name = "comboBox_RTC_Resource";
            this.comboBox_RTC_Resource.Size = new Size(0x79, 0x15);
            this.comboBox_RTC_Resource.TabIndex = 5;
            this.comboBox_CoarseTimeTransAvail.FormattingEnabled = true;
            this.comboBox_CoarseTimeTransAvail.Items.AddRange(new object[] { "NO", "YES" });
            this.comboBox_CoarseTimeTransAvail.Location = new Point(0x101, 0xc6);
            this.comboBox_CoarseTimeTransAvail.Name = "comboBox_CoarseTimeTransAvail";
            this.comboBox_CoarseTimeTransAvail.Size = new Size(0x79, 0x15);
            this.comboBox_CoarseTimeTransAvail.TabIndex = 6;
            this.comboBox_RefClkStatus.FormattingEnabled = true;
            this.comboBox_RefClkStatus.Items.AddRange(new object[] { "ON", "OFF" });
            this.comboBox_RefClkStatus.Location = new Point(0x101, 0xdb);
            this.comboBox_RefClkStatus.Name = "comboBox_RefClkStatus";
            this.comboBox_RefClkStatus.Size = new Size(0x79, 0x15);
            this.comboBox_RefClkStatus.TabIndex = 7;
            this.comboBox_RefClkStatus.SelectedIndexChanged += new EventHandler(this.comboBox_RefClkStatus_SelectedIndexChanged);
            this.comboBox_NorminalFreq.FormattingEnabled = true;
            this.comboBox_NorminalFreq.Items.AddRange(new object[] { "13000000", "16800000", "19200000", "19800000", "26000000", "33600000", "38400000", "40000000" });
            this.comboBox_NorminalFreq.Location = new Point(0x101, 240);
            this.comboBox_NorminalFreq.Name = "comboBox_NorminalFreq";
            this.comboBox_NorminalFreq.Size = new Size(0x79, 0x15);
            this.comboBox_NorminalFreq.TabIndex = 8;
            this.comboBox_NorminalFreq.SelectedIndexChanged += new EventHandler(this.comboBox_NorminalFreq_SelectedIndexChanged);
            this.groupBox1.Controls.Add(this.checkBox_ScreenCPResponse_HWCfg);
            this.groupBox1.Controls.Add(this.checkBox_ForwardRequestToCP_HWCfg);
            this.groupBox1.Controls.Add(this.checkBox_AutoReply_HWCfg);
            this.groupBox1.Location = new Point(0x1a6, 0x43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x9f, 100);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control";
            this.checkBox_ScreenCPResponse_HWCfg.AutoSize = true;
            this.checkBox_ScreenCPResponse_HWCfg.Location = new Point(12, 0x41);
            this.checkBox_ScreenCPResponse_HWCfg.Name = "checkBox_ScreenCPResponse_HWCfg";
            this.checkBox_ScreenCPResponse_HWCfg.Size = new Size(0x80, 0x11);
            this.checkBox_ScreenCPResponse_HWCfg.TabIndex = 2;
            this.checkBox_ScreenCPResponse_HWCfg.Text = "Screen CP Response";
            this.checkBox_ScreenCPResponse_HWCfg.UseVisualStyleBackColor = true;
            this.checkBox_ForwardRequestToCP_HWCfg.AutoSize = true;
            this.checkBox_ForwardRequestToCP_HWCfg.Location = new Point(12, 0x2b);
            this.checkBox_ForwardRequestToCP_HWCfg.Name = "checkBox_ForwardRequestToCP_HWCfg";
            this.checkBox_ForwardRequestToCP_HWCfg.Size = new Size(0x88, 0x11);
            this.checkBox_ForwardRequestToCP_HWCfg.TabIndex = 1;
            this.checkBox_ForwardRequestToCP_HWCfg.Text = "Forward Request to CP";
            this.checkBox_ForwardRequestToCP_HWCfg.UseVisualStyleBackColor = true;
            this.checkBox_AutoReply_HWCfg.AutoSize = true;
            this.checkBox_AutoReply_HWCfg.Location = new Point(12, 20);
            this.checkBox_AutoReply_HWCfg.Name = "checkBox_AutoReply_HWCfg";
            this.checkBox_AutoReply_HWCfg.Size = new Size(0x4b, 0x11);
            this.checkBox_AutoReply_HWCfg.TabIndex = 0;
            this.checkBox_AutoReply_HWCfg.Text = "AutoReply";
            this.checkBox_AutoReply_HWCfg.UseVisualStyleBackColor = true;
            this.button_Set_HWCfg.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button_Set_HWCfg.Location = new Point(0x1fa, 0x1f);
            this.button_Set_HWCfg.Name = "button_Set_HWCfg";
            this.button_Set_HWCfg.Size = new Size(0x4b, 0x17);
            this.button_Set_HWCfg.TabIndex = 0x10;
            this.button_Set_HWCfg.Text = "&SET";
            this.button_Set_HWCfg.UseVisualStyleBackColor = true;
            this.button_Set_HWCfg.Click += new EventHandler(this.hwConfigSetBtn_Click);
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button_cancel.Location = new Point(0x242, 0x261);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new Size(0x4b, 0x17);
            this.button_cancel.TabIndex = 15;
            this.button_cancel.Text = "&Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new EventHandler(this.cancelBtn_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x41, 0x22);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x9c, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Precise Time Transfer Available";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x41, 0x37);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x9b, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Precise Time Transfer Direction";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x41, 0x4c);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x91, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Frequency Transfer Available";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x41, 0x61);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x8a, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Frequency Transfer Method";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x41, 160);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x4b, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "RTC Available";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x41, 0xb5);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x4b, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "RTC  For GPS";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x41, 0xca);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x9a, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Coarse Time Transfer Available";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x41, 0xdf);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x75, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Reference clock status";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x41, 0xf4);
            this.label9.Name = "label9";
            this.label9.Size = new Size(150, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "External Clock Frequency (Hz)";
            this.label13.AutoSize = true;
            this.label13.ForeColor = SystemColors.AppWorkspace;
            this.label13.Location = new Point(0x41, 270);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0xd5, 0x1a);
            this.label13.TabIndex = 4;
            this.label13.Text = "Frequency should match the Ext Clock Freq\r\nin AutoReply Frequency Transfer Request.";
            this.label13.TextAlign = ContentAlignment.TopCenter;
            this.label15.AutoSize = true;
            this.label15.ForeColor = SystemColors.AppWorkspace;
            this.label15.Location = new Point(0x41, 0x7b);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0xf6, 0x1a);
            this.label15.TabIndex = 5;
            this.label15.Text = "Transfer method should match the Clock Source in\r\nAutoReply Frequency Transfer Request.";
            this.textBox_NWEnhanceType.Location = new Point(0x101, 0x138);
            this.textBox_NWEnhanceType.Name = "textBox_NWEnhanceType";
            this.textBox_NWEnhanceType.Size = new Size(0x79, 20);
            this.textBox_NWEnhanceType.TabIndex = 9;
            this.textBox_NWEnhanceType.Text = "0";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x41, 0x13c);
            this.label10.Name = "label10";
            this.label10.Size = new Size(120, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Network Enhance Type";
            this.tabControl_autoReply.Controls.Add(this.tabPage_HWCfg);
            this.tabControl_autoReply.Controls.Add(this.tabPage_FreqTrans);
            this.tabControl_autoReply.Controls.Add(this.tabPage_ApproxPos);
            this.tabControl_autoReply.Controls.Add(this.tabPage_TimeTrans);
            this.tabControl_autoReply.Controls.Add(this.tabPage_Pos_Request);
            this.tabControl_autoReply.Controls.Add(this.tabPage_Aiding);
            this.tabControl_autoReply.Location = new Point(0x15, 12);
            this.tabControl_autoReply.Name = "tabControl_autoReply";
            this.tabControl_autoReply.SelectedIndex = 0;
            this.tabControl_autoReply.Size = new Size(0x283, 0x20f);
            this.tabControl_autoReply.TabIndex = 8;
            this.tabPage_HWCfg.Controls.Add(this.subframe45ChkBox);
            this.tabPage_HWCfg.Controls.Add(this.subframe123ChkBox);
            this.tabPage_HWCfg.Controls.Add(this.auxNavChkBox);
            this.tabPage_HWCfg.Controls.Add(this.comboBox_PreciseTimeAvail);
            this.tabPage_HWCfg.Controls.Add(this.label10);
            this.tabPage_HWCfg.Controls.Add(this.comboBox_PreciseTimeTransDirection);
            this.tabPage_HWCfg.Controls.Add(this.textBox_NWEnhanceType);
            this.tabPage_HWCfg.Controls.Add(this.comboBox_FreqTransAvail);
            this.tabPage_HWCfg.Controls.Add(this.label15);
            this.tabPage_HWCfg.Controls.Add(this.comboBox_FreqTransMethod);
            this.tabPage_HWCfg.Controls.Add(this.label13);
            this.tabPage_HWCfg.Controls.Add(this.comboBox_RTCAvail);
            this.tabPage_HWCfg.Controls.Add(this.label9);
            this.tabPage_HWCfg.Controls.Add(this.comboBox_RTC_Resource);
            this.tabPage_HWCfg.Controls.Add(this.label8);
            this.tabPage_HWCfg.Controls.Add(this.comboBox_CoarseTimeTransAvail);
            this.tabPage_HWCfg.Controls.Add(this.label7);
            this.tabPage_HWCfg.Controls.Add(this.comboBox_RefClkStatus);
            this.tabPage_HWCfg.Controls.Add(this.comboBox_NorminalFreq);
            this.tabPage_HWCfg.Controls.Add(this.groupBox1);
            this.tabPage_HWCfg.Controls.Add(this.button_Set_HWCfg);
            this.tabPage_HWCfg.Controls.Add(this.label6);
            this.tabPage_HWCfg.Controls.Add(this.label5);
            this.tabPage_HWCfg.Controls.Add(this.label1);
            this.tabPage_HWCfg.Controls.Add(this.label4);
            this.tabPage_HWCfg.Controls.Add(this.label2);
            this.tabPage_HWCfg.Controls.Add(this.label3);
            this.tabPage_HWCfg.Location = new Point(4, 0x16);
            this.tabPage_HWCfg.Name = "tabPage_HWCfg";
            this.tabPage_HWCfg.Padding = new Padding(3);
            this.tabPage_HWCfg.Size = new Size(0x27b, 0x1f5);
            this.tabPage_HWCfg.TabIndex = 0;
            this.tabPage_HWCfg.Text = "HW Config";
            this.tabPage_HWCfg.UseVisualStyleBackColor = true;
            this.subframe45ChkBox.AutoSize = true;
            this.subframe45ChkBox.Location = new Point(70, 390);
            this.subframe45ChkBox.Name = "subframe45ChkBox";
            this.subframe45ChkBox.Size = new Size(0x7a, 0x11);
            this.subframe45ChkBox.TabIndex = 12;
            this.subframe45ChkBox.Text = "NavBit subframe 4,5";
            this.subframe45ChkBox.UseVisualStyleBackColor = true;
            this.subframe45ChkBox.CheckedChanged += new EventHandler(this.subframe45ChkBox_CheckedChanged);
            this.subframe123ChkBox.AutoSize = true;
            this.subframe123ChkBox.Location = new Point(70, 370);
            this.subframe123ChkBox.Name = "subframe123ChkBox";
            this.subframe123ChkBox.Size = new Size(0x83, 0x11);
            this.subframe123ChkBox.TabIndex = 11;
            this.subframe123ChkBox.Text = "NavBit subframe 1,2,3";
            this.subframe123ChkBox.UseVisualStyleBackColor = true;
            this.subframe123ChkBox.CheckedChanged += new EventHandler(this.subframe123ChkBox_CheckedChanged);
            this.auxNavChkBox.AutoSize = true;
            this.auxNavChkBox.Location = new Point(70, 350);
            this.auxNavChkBox.Name = "auxNavChkBox";
            this.auxNavChkBox.Size = new Size(0x7d, 0x11);
            this.auxNavChkBox.TabIndex = 10;
            this.auxNavChkBox.Text = "Aux Nav Supported?";
            this.auxNavChkBox.UseVisualStyleBackColor = true;
            this.auxNavChkBox.CheckedChanged += new EventHandler(this.auxNavChkBox_CheckedChanged);
            this.tabPage_FreqTrans.Controls.Add(this.radioButton_Reject_Freq);
            this.tabPage_FreqTrans.Controls.Add(this.button_Send_Freq);
            this.tabPage_FreqTrans.Controls.Add(this.button_SET_Freq);
            this.tabPage_FreqTrans.Controls.Add(this.scaleFreqOffsetGrpBox);
            this.tabPage_FreqTrans.Controls.Add(this.groupBox2);
            this.tabPage_FreqTrans.Controls.Add(this.groupBox4);
            this.tabPage_FreqTrans.Controls.Add(this.radioButton_UseRxRptFreq);
            this.tabPage_FreqTrans.Controls.Add(this.radioButton_SpecifyFreqParam);
            this.tabPage_FreqTrans.Controls.Add(this.textBox_AccuracyAuto);
            this.tabPage_FreqTrans.Controls.Add(this.textBox_Accuracy);
            this.tabPage_FreqTrans.Controls.Add(this.textBox_FreqAuto);
            this.tabPage_FreqTrans.Controls.Add(this.textBox_Freq);
            this.tabPage_FreqTrans.Controls.Add(this.comboBox_RFDefaultFreq);
            this.tabPage_FreqTrans.Controls.Add(this.label27);
            this.tabPage_FreqTrans.Controls.Add(this.label28);
            this.tabPage_FreqTrans.Controls.Add(this.label29);
            this.tabPage_FreqTrans.Controls.Add(this.label30);
            this.tabPage_FreqTrans.Controls.Add(this.label31);
            this.tabPage_FreqTrans.Controls.Add(this.label32);
            this.tabPage_FreqTrans.Controls.Add(this.label33);
            this.tabPage_FreqTrans.Location = new Point(4, 0x16);
            this.tabPage_FreqTrans.Name = "tabPage_FreqTrans";
            this.tabPage_FreqTrans.Padding = new Padding(3);
            this.tabPage_FreqTrans.Size = new Size(0x27b, 0x1f5);
            this.tabPage_FreqTrans.TabIndex = 1;
            this.tabPage_FreqTrans.Text = "Freq Trans";
            this.tabPage_FreqTrans.UseVisualStyleBackColor = true;
            this.radioButton_Reject_Freq.AutoSize = true;
            this.radioButton_Reject_Freq.Location = new Point(0x10, 210);
            this.radioButton_Reject_Freq.Name = "radioButton_Reject_Freq";
            this.radioButton_Reject_Freq.Size = new Size(0x97, 0x11);
            this.radioButton_Reject_Freq.TabIndex = 7;
            this.radioButton_Reject_Freq.TabStop = true;
            this.radioButton_Reject_Freq.Text = "Reject. Data Not Available";
            this.radioButton_Reject_Freq.UseVisualStyleBackColor = true;
            this.radioButton_Reject_Freq.CheckedChanged += new EventHandler(this.radioButton_Reject_Freq_CheckedChanged);
            this.button_Send_Freq.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button_Send_Freq.Location = new Point(0x219, 0x49);
            this.button_Send_Freq.Name = "button_Send_Freq";
            this.button_Send_Freq.Size = new Size(0x4b, 0x17);
            this.button_Send_Freq.TabIndex = 12;
            this.button_Send_Freq.Text = "Send Now";
            this.button_Send_Freq.UseVisualStyleBackColor = true;
            this.button_Send_Freq.Click += new EventHandler(this.button_Send_Freq_Click);
            this.button_SET_Freq.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button_SET_Freq.Location = new Point(0x219, 0x26);
            this.button_SET_Freq.Name = "button_SET_Freq";
            this.button_SET_Freq.Size = new Size(0x4b, 0x17);
            this.button_SET_Freq.TabIndex = 11;
            this.button_SET_Freq.Text = "SET";
            this.button_SET_Freq.UseVisualStyleBackColor = true;
            this.button_SET_Freq.Click += new EventHandler(this.button_SET_Freq_Click);
            this.scaleFreqOffsetGrpBox.Controls.Add(this.radioButton_4Hz);
            this.scaleFreqOffsetGrpBox.Controls.Add(this.radioButton_2Hz);
            this.scaleFreqOffsetGrpBox.Controls.Add(this.radioButton_1Hz);
            this.scaleFreqOffsetGrpBox.Location = new Point(0x195, 0x1f);
            this.scaleFreqOffsetGrpBox.Name = "scaleFreqOffsetGrpBox";
            this.scaleFreqOffsetGrpBox.Size = new Size(120, 0x5b);
            this.scaleFreqOffsetGrpBox.TabIndex = 8;
            this.scaleFreqOffsetGrpBox.TabStop = false;
            this.scaleFreqOffsetGrpBox.Text = "Frequency Offset Scale";
            this.radioButton_4Hz.AutoSize = true;
            this.radioButton_4Hz.Location = new Point(0x18, 0x3e);
            this.radioButton_4Hz.Name = "radioButton_4Hz";
            this.radioButton_4Hz.Size = new Size(0x2f, 0x11);
            this.radioButton_4Hz.TabIndex = 2;
            this.radioButton_4Hz.TabStop = true;
            this.radioButton_4Hz.Text = "4 Hz";
            this.radioButton_4Hz.UseVisualStyleBackColor = true;
            this.radioButton_2Hz.AutoSize = true;
            this.radioButton_2Hz.Location = new Point(0x18, 0x2d);
            this.radioButton_2Hz.Name = "radioButton_2Hz";
            this.radioButton_2Hz.Size = new Size(0x2f, 0x11);
            this.radioButton_2Hz.TabIndex = 1;
            this.radioButton_2Hz.TabStop = true;
            this.radioButton_2Hz.Text = "2 Hz";
            this.radioButton_2Hz.UseVisualStyleBackColor = true;
            this.radioButton_1Hz.AutoSize = true;
            this.radioButton_1Hz.Location = new Point(0x18, 0x1c);
            this.radioButton_1Hz.Name = "radioButton_1Hz";
            this.radioButton_1Hz.Size = new Size(0x2f, 0x11);
            this.radioButton_1Hz.TabIndex = 0;
            this.radioButton_1Hz.TabStop = true;
            this.radioButton_1Hz.Text = "1 Hz";
            this.radioButton_1Hz.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add(this.checkBox_ScreenCPResponse_Freq);
            this.groupBox2.Controls.Add(this.checkBox_ForwardRequestToCP_Freq);
            this.groupBox2.Controls.Add(this.checkBox_AutoReply_Freq);
            this.groupBox2.Location = new Point(0x195, 0x83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xd5, 0x4f);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control";
            this.checkBox_ScreenCPResponse_Freq.AutoSize = true;
            this.checkBox_ScreenCPResponse_Freq.Location = new Point(0x18, 0x36);
            this.checkBox_ScreenCPResponse_Freq.Name = "checkBox_ScreenCPResponse_Freq";
            this.checkBox_ScreenCPResponse_Freq.Size = new Size(0x80, 0x11);
            this.checkBox_ScreenCPResponse_Freq.TabIndex = 2;
            this.checkBox_ScreenCPResponse_Freq.Text = "Screen CP Response";
            this.checkBox_ScreenCPResponse_Freq.UseVisualStyleBackColor = true;
            this.checkBox_ForwardRequestToCP_Freq.AutoSize = true;
            this.checkBox_ForwardRequestToCP_Freq.Location = new Point(0x18, 0x25);
            this.checkBox_ForwardRequestToCP_Freq.Name = "checkBox_ForwardRequestToCP_Freq";
            this.checkBox_ForwardRequestToCP_Freq.Size = new Size(0x88, 0x11);
            this.checkBox_ForwardRequestToCP_Freq.TabIndex = 1;
            this.checkBox_ForwardRequestToCP_Freq.Text = "Forward Request to CP";
            this.checkBox_ForwardRequestToCP_Freq.UseVisualStyleBackColor = true;
            this.checkBox_AutoReply_Freq.AutoSize = true;
            this.checkBox_AutoReply_Freq.Location = new Point(0x18, 0x13);
            this.checkBox_AutoReply_Freq.Name = "checkBox_AutoReply_Freq";
            this.checkBox_AutoReply_Freq.Size = new Size(0x4b, 0x11);
            this.checkBox_AutoReply_Freq.TabIndex = 0;
            this.checkBox_AutoReply_Freq.Text = "AutoReply";
            this.checkBox_AutoReply_Freq.UseVisualStyleBackColor = true;
            this.groupBox4.Controls.Add(this.checkBox_ignoreXO);
            this.groupBox4.Controls.Add(this.comboBox_TimeTagValid);
            this.groupBox4.Controls.Add(this.checkBox_useTTBFreq);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.comboBox_SkewPPM);
            this.groupBox4.Controls.Add(this.comboBox_NormFreq_Freq);
            this.groupBox4.Controls.Add(this.comboBox_IncNormFreq_B4);
            this.groupBox4.Controls.Add(this.comboBox_RefClkReq_B3);
            this.groupBox4.Controls.Add(this.comboBox_RefClkOnOff_B2);
            this.groupBox4.Controls.Add(this.comboBox_ClkSrc_B1);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Location = new Point(12, 0xec);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x25e, 0xfe);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Tag = "";
            this.groupBox4.Text = "Reference Clock Info";
            this.checkBox_ignoreXO.AutoSize = true;
            this.checkBox_ignoreXO.Location = new Point(0xc5, 0x12);
            this.checkBox_ignoreXO.Name = "checkBox_ignoreXO";
            this.checkBox_ignoreXO.Size = new Size(0xe0, 0x11);
            this.checkBox_ignoreXO.TabIndex = 12;
            this.checkBox_ignoreXO.Text = "Force Freq Transfer Data Use (Ignore XO)";
            this.checkBox_ignoreXO.UseVisualStyleBackColor = true;
            this.checkBox_ignoreXO.CheckedChanged += new EventHandler(this.checkBox_ignoreXO_CheckedChanged);
            this.comboBox_TimeTagValid.FormattingEnabled = true;
            this.comboBox_TimeTagValid.Items.AddRange(new object[] { "valid fwd", "invalid" });
            this.comboBox_TimeTagValid.Location = new Point(0xc5, 0xdb);
            this.comboBox_TimeTagValid.Name = "comboBox_TimeTagValid";
            this.comboBox_TimeTagValid.Size = new Size(0x79, 0x15);
            this.comboBox_TimeTagValid.TabIndex = 7;
            this.checkBox_useTTBFreq.AutoSize = true;
            this.checkBox_useTTBFreq.Location = new Point(20, 0x12);
            this.checkBox_useTTBFreq.Name = "checkBox_useTTBFreq";
            this.checkBox_useTTBFreq.Size = new Size(0x83, 0x11);
            this.checkBox_useTTBFreq.TabIndex = 0;
            this.checkBox_useTTBFreq.Text = "Use TTB Freq Aiding?";
            this.checkBox_useTTBFreq.UseVisualStyleBackColor = true;
            this.checkBox_useTTBFreq.CheckedChanged += new EventHandler(this.checkBox_useTTBFreq_CheckedChanged);
            this.label17.AutoSize = true;
            this.label17.Location = new Point(20, 0xdf);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x34, 13);
            this.label17.TabIndex = 11;
            this.label17.Text = "Time Tag";
            this.label18.AutoSize = true;
            this.label18.ForeColor = SystemColors.ControlDarkDark;
            this.label18.Location = new Point(20, 0xc4);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x1aa, 13);
            this.label18.TabIndex = 9;
            this.label18.Text = "Frequency should match the External Clock Frequency in AutoReply HW Config Request";
            this.comboBox_SkewPPM.FormattingEnabled = true;
            this.comboBox_SkewPPM.Items.AddRange(new object[] { "0", "0.5", "-0.5", "1.0", "-1.0", "1.5", "-1.5", "2.0", "-2.0", "2.5", "-2.5", "3.0", "-3.0", "5.0", "-5.0" });
            this.comboBox_SkewPPM.Location = new Point(0x152, 0xa8);
            this.comboBox_SkewPPM.Name = "comboBox_SkewPPM";
            this.comboBox_SkewPPM.Size = new Size(0x4d, 0x15);
            this.comboBox_SkewPPM.TabIndex = 6;
            this.comboBox_NormFreq_Freq.FormattingEnabled = true;
            this.comboBox_NormFreq_Freq.Items.AddRange(new object[] { "13000000", "16800000", "19200000", "19800000", "26000000", "33600000", "38400000", "40000000" });
            this.comboBox_NormFreq_Freq.Location = new Point(0xc5, 0xa8);
            this.comboBox_NormFreq_Freq.Name = "comboBox_NormFreq_Freq";
            this.comboBox_NormFreq_Freq.Size = new Size(0x79, 0x15);
            this.comboBox_NormFreq_Freq.TabIndex = 5;
            this.comboBox_NormFreq_Freq.SelectedIndexChanged += new EventHandler(this.comboBox_NormFreq_Freq_SelectedIndexChanged);
            this.comboBox_IncNormFreq_B4.FormattingEnabled = true;
            this.comboBox_IncNormFreq_B4.Items.AddRange(new object[] { "No", "Yes" });
            this.comboBox_IncNormFreq_B4.Location = new Point(0xc5, 0x90);
            this.comboBox_IncNormFreq_B4.Name = "comboBox_IncNormFreq_B4";
            this.comboBox_IncNormFreq_B4.Size = new Size(0x79, 0x15);
            this.comboBox_IncNormFreq_B4.TabIndex = 4;
            this.comboBox_IncNormFreq_B4.SelectedIndexChanged += new EventHandler(this.comboBox_IncNormFreq_B4_SelectedIndexChanged);
            this.comboBox_RefClkReq_B3.FormattingEnabled = true;
            this.comboBox_RefClkReq_B3.Items.AddRange(new object[] { "None", "Turn Off" });
            this.comboBox_RefClkReq_B3.Location = new Point(0xc5, 120);
            this.comboBox_RefClkReq_B3.Name = "comboBox_RefClkReq_B3";
            this.comboBox_RefClkReq_B3.Size = new Size(0x79, 0x15);
            this.comboBox_RefClkReq_B3.TabIndex = 3;
            this.comboBox_RefClkOnOff_B2.FormattingEnabled = true;
            this.comboBox_RefClkOnOff_B2.Items.AddRange(new object[] { "On", "Off" });
            this.comboBox_RefClkOnOff_B2.Location = new Point(0xc5, 0x60);
            this.comboBox_RefClkOnOff_B2.Name = "comboBox_RefClkOnOff_B2";
            this.comboBox_RefClkOnOff_B2.Size = new Size(0x79, 0x15);
            this.comboBox_RefClkOnOff_B2.TabIndex = 2;
            this.comboBox_RefClkOnOff_B2.SelectedIndexChanged += new EventHandler(this.comboBox_RefClkOnOff_B2_SelectedIndexChanged);
            this.comboBox_ClkSrc_B1.FormattingEnabled = true;
            this.comboBox_ClkSrc_B1.Items.AddRange(new object[] { "Ref. Clk (Counter method)", "SLC Clk (Non-Counter method)" });
            this.comboBox_ClkSrc_B1.Location = new Point(0xc5, 40);
            this.comboBox_ClkSrc_B1.Name = "comboBox_ClkSrc_B1";
            this.comboBox_ClkSrc_B1.Size = new Size(180, 0x15);
            this.comboBox_ClkSrc_B1.TabIndex = 1;
            this.comboBox_ClkSrc_B1.SelectedIndexChanged += new EventHandler(this.comboBox_ClkSrc_B1_SelectedIndexChanged);
            this.label20.AutoSize = true;
            this.label20.Location = new Point(20, 0x2c);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x47, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "Clock Source";
            this.label21.AutoSize = true;
            this.label21.Location = new Point(0x153, 0x98);
            this.label21.Name = "label21";
            this.label21.Size = new Size(60, 13);
            this.label21.TabIndex = 0;
            this.label21.Text = "Skew(ppm)";
            this.label22.AutoSize = true;
            this.label22.Location = new Point(20, 0xac);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x79, 13);
            this.label22.TabIndex = 0;
            this.label22.Text = "External Clock Freq (Hz)";
            this.label23.AutoSize = true;
            this.label23.Location = new Point(20, 0x94);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x6b, 13);
            this.label23.TabIndex = 0;
            this.label23.Text = "Include Nominal Freq";
            this.label24.AutoSize = true;
            this.label24.Location = new Point(20, 0x7c);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x61, 13);
            this.label24.TabIndex = 0;
            this.label24.Text = "Ref Clock Request";
            this.label25.AutoSize = true;
            this.label25.Location = new Point(20, 100);
            this.label25.Name = "label25";
            this.label25.Size = new Size(90, 13);
            this.label25.TabIndex = 0;
            this.label25.Text = "Reference Clock ";
            this.label26.AutoSize = true;
            this.label26.ForeColor = SystemColors.ControlDarkDark;
            this.label26.Location = new Point(20, 0x44);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x19e, 13);
            this.label26.TabIndex = 0;
            this.label26.Text = "Clock Source should match the Freq Transfer method in AutoRely HW Config Request";
            this.radioButton_UseRxRptFreq.AutoSize = true;
            this.radioButton_UseRxRptFreq.Location = new Point(0x10, 0x85);
            this.radioButton_UseRxRptFreq.Name = "radioButton_UseRxRptFreq";
            this.radioButton_UseRxRptFreq.Size = new Size(0xdd, 0x11);
            this.radioButton_UseRxRptFreq.TabIndex = 4;
            this.radioButton_UseRxRptFreq.TabStop = true;
            this.radioButton_UseRxRptFreq.Text = "Use Receiver Reported Frequency Offset";
            this.radioButton_UseRxRptFreq.UseVisualStyleBackColor = true;
            this.radioButton_UseRxRptFreq.CheckedChanged += new EventHandler(this.radioButton_UseRxRptFreq_CheckedChanged);
            this.radioButton_SpecifyFreqParam.AutoSize = true;
            this.radioButton_SpecifyFreqParam.Location = new Point(0x10, 0x3b);
            this.radioButton_SpecifyFreqParam.Name = "radioButton_SpecifyFreqParam";
            this.radioButton_SpecifyFreqParam.Size = new Size(0xa9, 0x11);
            this.radioButton_SpecifyFreqParam.TabIndex = 1;
            this.radioButton_SpecifyFreqParam.TabStop = true;
            this.radioButton_SpecifyFreqParam.Text = "Specify Frequency Parameters";
            this.radioButton_SpecifyFreqParam.UseVisualStyleBackColor = true;
            this.radioButton_SpecifyFreqParam.CheckedChanged += new EventHandler(this.radioButton_SpecifyFreqParam_CheckedChanged);
            this.textBox_AccuracyAuto.Location = new Point(50, 180);
            this.textBox_AccuracyAuto.Name = "textBox_AccuracyAuto";
            this.textBox_AccuracyAuto.Size = new Size(0x59, 20);
            this.textBox_AccuracyAuto.TabIndex = 6;
            this.textBox_AccuracyAuto.Text = "0.5";
            this.textBox_Accuracy.Location = new Point(50, 0x69);
            this.textBox_Accuracy.Name = "textBox_Accuracy";
            this.textBox_Accuracy.Size = new Size(0x59, 20);
            this.textBox_Accuracy.TabIndex = 3;
            this.textBox_Accuracy.Text = "0.5";
            this.textBox_FreqAuto.Location = new Point(50, 0x9e);
            this.textBox_FreqAuto.Name = "textBox_FreqAuto";
            this.textBox_FreqAuto.Size = new Size(0x59, 20);
            this.textBox_FreqAuto.TabIndex = 5;
            this.textBox_FreqAuto.Text = "0";
            this.textBox_Freq.Location = new Point(50, 0x52);
            this.textBox_Freq.Name = "textBox_Freq";
            this.textBox_Freq.Size = new Size(0x59, 20);
            this.textBox_Freq.TabIndex = 2;
            this.textBox_Freq.Text = "0";
            this.comboBox_RFDefaultFreq.FormattingEnabled = true;
            this.comboBox_RFDefaultFreq.Items.AddRange(new object[] { "91236", "95248", "96250", "98254", "118460", "228150" });
            this.comboBox_RFDefaultFreq.Location = new Point(0xba, 0x1f);
            this.comboBox_RFDefaultFreq.Name = "comboBox_RFDefaultFreq";
            this.comboBox_RFDefaultFreq.Size = new Size(0x79, 0x15);
            this.comboBox_RFDefaultFreq.TabIndex = 0;
            this.label27.AutoSize = true;
            this.label27.ForeColor = SystemColors.ControlDarkDark;
            this.label27.Location = new Point(0xc4, 0xa2);
            this.label27.Name = "label27";
            this.label27.Size = new Size(0xcc, 13);
            this.label27.TabIndex = 0x17;
            this.label27.Text = "when no receiver info available, at 20ppm";
            this.label28.AutoSize = true;
            this.label28.ForeColor = SystemColors.ControlText;
            this.label28.Location = new Point(0x8d, 0xa2);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x36, 13);
            this.label28.TabIndex = 0x16;
            this.label28.Text = "Offset(Hz)";
            this.label29.AutoSize = true;
            this.label29.ForeColor = SystemColors.ControlText;
            this.label29.Location = new Point(0x8d, 0xb8);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x4e, 13);
            this.label29.TabIndex = 0x15;
            this.label29.Text = "Accuracy(ppm)";
            this.label30.AutoSize = true;
            this.label30.ForeColor = SystemColors.ControlText;
            this.label30.Location = new Point(0x8d, 0x6d);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0x4e, 13);
            this.label30.TabIndex = 0x18;
            this.label30.Text = "Accuracy(ppm)";
            this.label31.AutoSize = true;
            this.label31.ForeColor = SystemColors.ControlDarkDark;
            this.label31.Location = new Point(0xe1, 80);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0xa8, 0x1a);
            this.label31.TabIndex = 0x1b;
            this.label31.Text = "In Counter method, if TTB is used,\r\nit will substitute this value.";
            this.label32.AutoSize = true;
            this.label32.ForeColor = SystemColors.ControlText;
            this.label32.Location = new Point(0x8e, 0x55);
            this.label32.Name = "label32";
            this.label32.Size = new Size(0x4e, 13);
            this.label32.TabIndex = 0x1a;
            this.label32.Text = "Offset Freq(Hz)";
            this.label33.AutoSize = true;
            this.label33.Location = new Point(6, 0x23);
            this.label33.Name = "label33";
            this.label33.Size = new Size(0xa4, 13);
            this.label33.TabIndex = 0x19;
            this.label33.Text = "RF Default Frequency Offset (Hz)";
            this.tabPage_ApproxPos.Controls.Add(this.label62);
            this.tabPage_ApproxPos.Controls.Add(this.frmAutoReplySetAsDefaultBtn);
            this.tabPage_ApproxPos.Controls.Add(this.frmAutoReplyRefLocationComboBox);
            this.tabPage_ApproxPos.Controls.Add(this.button_GPSOrigin);
            this.tabPage_ApproxPos.Controls.Add(this.button_OriginSkew);
            this.tabPage_ApproxPos.Controls.Add(this.button_SET_ApproxPos);
            this.tabPage_ApproxPos.Controls.Add(this.groupBox5);
            this.tabPage_ApproxPos.Controls.Add(this.numericUpDown_EstVertErr);
            this.tabPage_ApproxPos.Controls.Add(this.numericUpDown_EstHoriErr);
            this.tabPage_ApproxPos.Controls.Add(this.textBox_heading_skew);
            this.tabPage_ApproxPos.Controls.Add(this.textBox_Longitude);
            this.tabPage_ApproxPos.Controls.Add(this.textBox_distance_skew);
            this.tabPage_ApproxPos.Controls.Add(this.textBox_Latitude);
            this.tabPage_ApproxPos.Controls.Add(this.textBox_Altitude);
            this.tabPage_ApproxPos.Controls.Add(this.label36);
            this.tabPage_ApproxPos.Controls.Add(this.label37);
            this.tabPage_ApproxPos.Controls.Add(this.label38);
            this.tabPage_ApproxPos.Controls.Add(this.label39);
            this.tabPage_ApproxPos.Controls.Add(this.label40);
            this.tabPage_ApproxPos.Controls.Add(this.label41);
            this.tabPage_ApproxPos.Controls.Add(this.label42);
            this.tabPage_ApproxPos.Controls.Add(this.label43);
            this.tabPage_ApproxPos.Controls.Add(this.label44);
            this.tabPage_ApproxPos.Location = new Point(4, 0x16);
            this.tabPage_ApproxPos.Name = "tabPage_ApproxPos";
            this.tabPage_ApproxPos.Padding = new Padding(3);
            this.tabPage_ApproxPos.Size = new Size(0x27b, 0x1f5);
            this.tabPage_ApproxPos.TabIndex = 2;
            this.tabPage_ApproxPos.Text = "Approx Pos";
            this.tabPage_ApproxPos.UseVisualStyleBackColor = true;
            this.label62.AutoSize = true;
            this.label62.Location = new Point(30, 0x34);
            this.label62.Name = "label62";
            this.label62.Size = new Size(0x65, 13);
            this.label62.TabIndex = 0x27;
            this.label62.Text = "Reference Location";
            this.frmAutoReplySetAsDefaultBtn.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.frmAutoReplySetAsDefaultBtn.Location = new Point(0x1e3, 0x54);
            this.frmAutoReplySetAsDefaultBtn.Name = "frmAutoReplySetAsDefaultBtn";
            this.frmAutoReplySetAsDefaultBtn.Size = new Size(0x70, 0x17);
            this.frmAutoReplySetAsDefaultBtn.TabIndex = 12;
            this.frmAutoReplySetAsDefaultBtn.Text = "Set Position Default";
            this.frmAutoReplySetAsDefaultBtn.UseVisualStyleBackColor = true;
            this.frmAutoReplySetAsDefaultBtn.Click += new EventHandler(this.frmAutoReplySetAsDefaultBtn_Click);
            this.frmAutoReplyRefLocationComboBox.FormattingEnabled = true;
            this.frmAutoReplyRefLocationComboBox.Location = new Point(0x97, 0x30);
            this.frmAutoReplyRefLocationComboBox.Name = "frmAutoReplyRefLocationComboBox";
            this.frmAutoReplyRefLocationComboBox.Size = new Size(0x79, 0x15);
            this.frmAutoReplyRefLocationComboBox.TabIndex = 0;
            this.frmAutoReplyRefLocationComboBox.SelectedIndexChanged += new EventHandler(this.frmAutoReplyRefLocationComboBox_SelectedIndexChanged);
            this.frmAutoReplyRefLocationComboBox.DropDown += new EventHandler(this.frmAutoReplyRefLocationComboBox_DropDown);
            this.button_GPSOrigin.Enabled = false;
            this.button_GPSOrigin.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button_GPSOrigin.Location = new Point(0x1e3, 0x92);
            this.button_GPSOrigin.Name = "button_GPSOrigin";
            this.button_GPSOrigin.Size = new Size(0x70, 0x17);
            this.button_GPSOrigin.TabIndex = 11;
            this.button_GPSOrigin.Text = "GPS Origin";
            this.button_GPSOrigin.UseVisualStyleBackColor = true;
            this.button_GPSOrigin.Click += new EventHandler(this.button_GPSOrigin_Click);
            this.button_OriginSkew.Enabled = false;
            this.button_OriginSkew.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button_OriginSkew.Location = new Point(0x1e3, 0x73);
            this.button_OriginSkew.Name = "button_OriginSkew";
            this.button_OriginSkew.Size = new Size(0x70, 0x17);
            this.button_OriginSkew.TabIndex = 10;
            this.button_OriginSkew.Text = "Origin Skew";
            this.button_OriginSkew.UseVisualStyleBackColor = true;
            this.button_OriginSkew.Click += new EventHandler(this.button_OriginSkew_Click);
            this.button_SET_ApproxPos.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button_SET_ApproxPos.Location = new Point(0x1e3, 0x35);
            this.button_SET_ApproxPos.Name = "button_SET_ApproxPos";
            this.button_SET_ApproxPos.Size = new Size(0x70, 0x17);
            this.button_SET_ApproxPos.TabIndex = 9;
            this.button_SET_ApproxPos.Text = "SET";
            this.button_SET_ApproxPos.UseVisualStyleBackColor = true;
            this.button_SET_ApproxPos.Click += new EventHandler(this.button_SET_ApproxPos_Click);
            this.groupBox5.Controls.Add(this.checkBox_ScreenCPResp_ApproxPos);
            this.groupBox5.Controls.Add(this.checkBox_FwdReqToCP_ApproxPos);
            this.groupBox5.Controls.Add(this.checkBox_AutoReply_ApproxPos);
            this.groupBox5.Controls.Add(this.checkBox_Reject_ApproxPos);
            this.groupBox5.Location = new Point(0x21, 0xd7);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0xca, 0x61);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Control";
            this.checkBox_ScreenCPResp_ApproxPos.AutoSize = true;
            this.checkBox_ScreenCPResp_ApproxPos.Location = new Point(12, 0x34);
            this.checkBox_ScreenCPResp_ApproxPos.Name = "checkBox_ScreenCPResp_ApproxPos";
            this.checkBox_ScreenCPResp_ApproxPos.Size = new Size(0x80, 0x11);
            this.checkBox_ScreenCPResp_ApproxPos.TabIndex = 2;
            this.checkBox_ScreenCPResp_ApproxPos.Text = "Screen CP Response";
            this.checkBox_ScreenCPResp_ApproxPos.UseVisualStyleBackColor = true;
            this.checkBox_FwdReqToCP_ApproxPos.AutoSize = true;
            this.checkBox_FwdReqToCP_ApproxPos.Location = new Point(12, 0x23);
            this.checkBox_FwdReqToCP_ApproxPos.Name = "checkBox_FwdReqToCP_ApproxPos";
            this.checkBox_FwdReqToCP_ApproxPos.Size = new Size(0x88, 0x11);
            this.checkBox_FwdReqToCP_ApproxPos.TabIndex = 1;
            this.checkBox_FwdReqToCP_ApproxPos.Text = "Forward Request to CP";
            this.checkBox_FwdReqToCP_ApproxPos.UseVisualStyleBackColor = true;
            this.checkBox_AutoReply_ApproxPos.AutoSize = true;
            this.checkBox_AutoReply_ApproxPos.Location = new Point(12, 0x12);
            this.checkBox_AutoReply_ApproxPos.Name = "checkBox_AutoReply_ApproxPos";
            this.checkBox_AutoReply_ApproxPos.Size = new Size(0x4b, 0x11);
            this.checkBox_AutoReply_ApproxPos.TabIndex = 0;
            this.checkBox_AutoReply_ApproxPos.Text = "AutoReply";
            this.checkBox_AutoReply_ApproxPos.UseVisualStyleBackColor = true;
            this.checkBox_Reject_ApproxPos.AutoSize = true;
            this.checkBox_Reject_ApproxPos.Location = new Point(12, 0x45);
            this.checkBox_Reject_ApproxPos.Name = "checkBox_Reject_ApproxPos";
            this.checkBox_Reject_ApproxPos.Size = new Size(0x9b, 0x11);
            this.checkBox_Reject_ApproxPos.TabIndex = 3;
            this.checkBox_Reject_ApproxPos.Text = "Reject.  Data Not Available";
            this.checkBox_Reject_ApproxPos.UseVisualStyleBackColor = true;
            this.numericUpDown_EstVertErr.Location = new Point(0xbb, 0xb8);
            int[] bits = new int[4];
            bits[0] = 0xffff;
            bits[3] = 0x10000;
            this.numericUpDown_EstVertErr.Maximum = new decimal(bits);
            this.numericUpDown_EstVertErr.Name = "numericUpDown_EstVertErr";
            this.numericUpDown_EstVertErr.Size = new Size(100, 20);
            this.numericUpDown_EstVertErr.TabIndex = 7;
            int[] numArray2 = new int[4];
            numArray2[0] = 500;
            this.numericUpDown_EstVertErr.Value = new decimal(numArray2);
            this.numericUpDown_EstHoriErr.Location = new Point(0xbb, 0x9d);
            int[] numArray3 = new int[4];
            numArray3[0] = 0x168000;
            this.numericUpDown_EstHoriErr.Maximum = new decimal(numArray3);
            this.numericUpDown_EstHoriErr.Name = "numericUpDown_EstHoriErr";
            this.numericUpDown_EstHoriErr.Size = new Size(100, 20);
            this.numericUpDown_EstHoriErr.TabIndex = 6;
            int[] numArray4 = new int[4];
            numArray4[0] = 0x7530;
            this.numericUpDown_EstHoriErr.Value = new decimal(numArray4);
            this.textBox_heading_skew.Location = new Point(0x125, 0x67);
            this.textBox_heading_skew.Name = "textBox_heading_skew";
            this.textBox_heading_skew.Size = new Size(0x45, 20);
            this.textBox_heading_skew.TabIndex = 4;
            this.textBox_heading_skew.Text = "0.00000";
            this.textBox_Longitude.Location = new Point(0xbb, 0x67);
            this.textBox_Longitude.Name = "textBox_Longitude";
            this.textBox_Longitude.Size = new Size(100, 20);
            this.textBox_Longitude.TabIndex = 3;
            this.textBox_Longitude.Text = "-121.914756";
            this.textBox_distance_skew.Location = new Point(0x125, 0x4c);
            this.textBox_distance_skew.Name = "textBox_distance_skew";
            this.textBox_distance_skew.Size = new Size(0x45, 20);
            this.textBox_distance_skew.TabIndex = 2;
            this.textBox_distance_skew.Text = "0.00000";
            this.textBox_Latitude.Location = new Point(0xbb, 0x4c);
            this.textBox_Latitude.Name = "textBox_Latitude";
            this.textBox_Latitude.Size = new Size(100, 20);
            this.textBox_Latitude.TabIndex = 1;
            this.textBox_Latitude.Text = "37.375286";
            this.textBox_Altitude.Location = new Point(0xbb, 130);
            this.textBox_Altitude.Name = "textBox_Altitude";
            this.textBox_Altitude.Size = new Size(100, 20);
            this.textBox_Altitude.TabIndex = 5;
            this.textBox_Altitude.Text = "-17.0";
            this.label36.AutoSize = true;
            this.label36.ForeColor = SystemColors.ControlDarkDark;
            this.label36.Location = new Point(0x125, 160);
            this.label36.Name = "label36";
            this.label36.Size = new Size(140, 0x27);
            this.label36.TabIndex = 0x16;
            this.label36.Text = "For F1.9 and higher, setting\r\nEst. Vr. Error = 0 will disable \r\naltitude aiding";
            this.label37.AutoSize = true;
            this.label37.Location = new Point(30, 0xbb);
            this.label37.Name = "label37";
            this.label37.Size = new Size(130, 13);
            this.label37.TabIndex = 0x15;
            this.label37.Text = "Estimated Vertical Error(m)";
            this.label38.AutoSize = true;
            this.label38.Location = new Point(30, 160);
            this.label38.Name = "label38";
            this.label38.Size = new Size(0x8e, 13);
            this.label38.TabIndex = 0x12;
            this.label38.Text = "Estimated Horizontal Error(m)";
            this.label39.AutoSize = true;
            this.label39.Location = new Point(30, 0x85);
            this.label39.Name = "label39";
            this.label39.Size = new Size(0x52, 13);
            this.label39.TabIndex = 0x11;
            this.label39.Text = "Altitude (meters)";
            this.label40.AutoSize = true;
            this.label40.Location = new Point(30, 0x6a);
            this.label40.Name = "label40";
            this.label40.Size = new Size(0x36, 13);
            this.label40.TabIndex = 0x10;
            this.label40.Text = "Longitude";
            this.label41.AutoSize = true;
            this.label41.Location = new Point(0x16d, 0x6b);
            this.label41.Name = "label41";
            this.label41.Size = new Size(0x57, 13);
            this.label41.TabIndex = 13;
            this.label41.Text = "heading (degree)";
            this.label42.AutoSize = true;
            this.label42.Location = new Point(0x134, 0x3b);
            this.label42.Name = "label42";
            this.label42.Size = new Size(0x22, 13);
            this.label42.TabIndex = 12;
            this.label42.Text = "Skew";
            this.label43.AutoSize = true;
            this.label43.Location = new Point(0x16d, 80);
            this.label43.Name = "label43";
            this.label43.Size = new Size(0x15, 13);
            this.label43.TabIndex = 15;
            this.label43.Text = "(m)";
            this.label44.AutoSize = true;
            this.label44.Location = new Point(30, 0x4f);
            this.label44.Name = "label44";
            this.label44.Size = new Size(0x2d, 13);
            this.label44.TabIndex = 14;
            this.label44.Text = "Latitude";
            this.tabPage_TimeTrans.Controls.Add(this.groupBox8);
            this.tabPage_TimeTrans.Controls.Add(this.utcOffsetLabel);
            this.tabPage_TimeTrans.Controls.Add(this.radioButton_useTTBTimeAiding);
            this.tabPage_TimeTrans.Controls.Add(this.button_SET_TimeTrans);
            this.tabPage_TimeTrans.Controls.Add(this.radioButton_Reject_TimeTrans);
            this.tabPage_TimeTrans.Controls.Add(this.label45);
            this.tabPage_TimeTrans.Controls.Add(this.radioButton_useDOSTimeForCoarseTimeAiding);
            this.tabPage_TimeTrans.Controls.Add(this.groupBox6);
            this.tabPage_TimeTrans.Controls.Add(this.textBox_Accuracy_TimeTrans);
            this.tabPage_TimeTrans.Controls.Add(this.label46);
            this.tabPage_TimeTrans.Controls.Add(this.textBox_Skew_TimeTrans);
            this.tabPage_TimeTrans.Controls.Add(this.textBox_UtcOffset);
            this.tabPage_TimeTrans.Location = new Point(4, 0x16);
            this.tabPage_TimeTrans.Name = "tabPage_TimeTrans";
            this.tabPage_TimeTrans.Padding = new Padding(3);
            this.tabPage_TimeTrans.Size = new Size(0x27b, 0x1f5);
            this.tabPage_TimeTrans.TabIndex = 3;
            this.tabPage_TimeTrans.Text = "Time Trans";
            this.tabPage_TimeTrans.UseVisualStyleBackColor = true;
            this.groupBox8.Controls.Add(this.button1_CancelTTBTimeAidingCfg);
            this.groupBox8.Controls.Add(this.TTBTimeAidingSkewUnitLabel);
            this.groupBox8.Controls.Add(this.TTBTimeAidingTypeComboBox);
            this.groupBox8.Controls.Add(this.TTBTimeAidingEnableChkboxLabel);
            this.groupBox8.Controls.Add(this.TTBTimeAidingSkewTxtBox);
            this.groupBox8.Controls.Add(this.TTBTimeAidingAccUnitLabel);
            this.groupBox8.Controls.Add(this.TTBTimeAidingEnableComboBox);
            this.groupBox8.Controls.Add(this.TTBTimeAidingAccTxtBox);
            this.groupBox8.Controls.Add(this.TTBTimeAidingModeLabel);
            this.groupBox8.Controls.Add(this.TTBTimeAidingAccLabel);
            this.groupBox8.Controls.Add(this.TTBTimeAidingSkewLabel);
            this.groupBox8.Controls.Add(this.TTBTimeAidingSetBtn);
            this.groupBox8.Location = new Point(50, 0xc6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new Size(0x131, 130);
            this.groupBox8.TabIndex = 6;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "TTB Config";
            this.button1_CancelTTBTimeAidingCfg.Location = new Point(210, 0x47);
            this.button1_CancelTTBTimeAidingCfg.Name = "button1_CancelTTBTimeAidingCfg";
            this.button1_CancelTTBTimeAidingCfg.Size = new Size(0x4b, 0x17);
            this.button1_CancelTTBTimeAidingCfg.TabIndex = 5;
            this.button1_CancelTTBTimeAidingCfg.Text = "Cancel";
            this.button1_CancelTTBTimeAidingCfg.UseVisualStyleBackColor = true;
            this.button1_CancelTTBTimeAidingCfg.Click += new EventHandler(this.button1_CancelTTBTimeAidingCfg_Click);
            this.TTBTimeAidingSkewUnitLabel.AutoSize = true;
            this.TTBTimeAidingSkewUnitLabel.Location = new Point(0xb8, 0x63);
            this.TTBTimeAidingSkewUnitLabel.Name = "TTBTimeAidingSkewUnitLabel";
            this.TTBTimeAidingSkewUnitLabel.Size = new Size(20, 13);
            this.TTBTimeAidingSkewUnitLabel.TabIndex = 0x16;
            this.TTBTimeAidingSkewUnitLabel.Text = "ms";
            this.TTBTimeAidingTypeComboBox.FormattingEnabled = true;
            this.TTBTimeAidingTypeComboBox.Items.AddRange(new object[] { "Precise", "Coarse" });
            this.TTBTimeAidingTypeComboBox.Location = new Point(0x53, 0x2f);
            this.TTBTimeAidingTypeComboBox.Name = "TTBTimeAidingTypeComboBox";
            this.TTBTimeAidingTypeComboBox.Size = new Size(100, 0x15);
            this.TTBTimeAidingTypeComboBox.TabIndex = 1;
            this.TTBTimeAidingTypeComboBox.Text = "Coarse";
            this.TTBTimeAidingTypeComboBox.SelectedIndexChanged += new EventHandler(this.TTBTimeAidingTypeComboBox_SelectedIndexChanged);
            this.TTBTimeAidingEnableChkboxLabel.AutoSize = true;
            this.TTBTimeAidingEnableChkboxLabel.Location = new Point(0x19, 0x1c);
            this.TTBTimeAidingEnableChkboxLabel.Name = "TTBTimeAidingEnableChkboxLabel";
            this.TTBTimeAidingEnableChkboxLabel.Size = new Size(40, 13);
            this.TTBTimeAidingEnableChkboxLabel.TabIndex = 14;
            this.TTBTimeAidingEnableChkboxLabel.Text = "Enable";
            this.TTBTimeAidingSkewTxtBox.Location = new Point(0x53, 0x5d);
            this.TTBTimeAidingSkewTxtBox.Name = "TTBTimeAidingSkewTxtBox";
            this.TTBTimeAidingSkewTxtBox.Size = new Size(100, 20);
            this.TTBTimeAidingSkewTxtBox.TabIndex = 3;
            this.TTBTimeAidingSkewTxtBox.Text = "0";
            this.TTBTimeAidingAccUnitLabel.AutoSize = true;
            this.TTBTimeAidingAccUnitLabel.Location = new Point(0xb8, 0x4d);
            this.TTBTimeAidingAccUnitLabel.Name = "TTBTimeAidingAccUnitLabel";
            this.TTBTimeAidingAccUnitLabel.Size = new Size(20, 13);
            this.TTBTimeAidingAccUnitLabel.TabIndex = 0x15;
            this.TTBTimeAidingAccUnitLabel.Text = "ms";
            this.TTBTimeAidingEnableComboBox.FormattingEnabled = true;
            this.TTBTimeAidingEnableComboBox.Items.AddRange(new object[] { "Disable", "Enable" });
            this.TTBTimeAidingEnableComboBox.Location = new Point(0x53, 0x18);
            this.TTBTimeAidingEnableComboBox.Name = "TTBTimeAidingEnableComboBox";
            this.TTBTimeAidingEnableComboBox.Size = new Size(100, 0x15);
            this.TTBTimeAidingEnableComboBox.TabIndex = 0;
            this.TTBTimeAidingEnableComboBox.Text = "Enable";
            this.TTBTimeAidingAccTxtBox.Location = new Point(0x53, 70);
            this.TTBTimeAidingAccTxtBox.Name = "TTBTimeAidingAccTxtBox";
            this.TTBTimeAidingAccTxtBox.Size = new Size(100, 20);
            this.TTBTimeAidingAccTxtBox.TabIndex = 2;
            this.TTBTimeAidingAccTxtBox.Text = "2";
            this.TTBTimeAidingModeLabel.AutoSize = true;
            this.TTBTimeAidingModeLabel.Location = new Point(0x1f, 0x33);
            this.TTBTimeAidingModeLabel.Name = "TTBTimeAidingModeLabel";
            this.TTBTimeAidingModeLabel.Size = new Size(0x22, 13);
            this.TTBTimeAidingModeLabel.TabIndex = 0x10;
            this.TTBTimeAidingModeLabel.Text = "Mode";
            this.TTBTimeAidingAccLabel.AutoSize = true;
            this.TTBTimeAidingAccLabel.Location = new Point(15, 0x4a);
            this.TTBTimeAidingAccLabel.Name = "TTBTimeAidingAccLabel";
            this.TTBTimeAidingAccLabel.Size = new Size(0x34, 13);
            this.TTBTimeAidingAccLabel.TabIndex = 0x13;
            this.TTBTimeAidingAccLabel.Text = "Accuracy";
            this.TTBTimeAidingSkewLabel.AutoSize = true;
            this.TTBTimeAidingSkewLabel.Location = new Point(0x1f, 0x61);
            this.TTBTimeAidingSkewLabel.Name = "TTBTimeAidingSkewLabel";
            this.TTBTimeAidingSkewLabel.Size = new Size(0x22, 13);
            this.TTBTimeAidingSkewLabel.TabIndex = 20;
            this.TTBTimeAidingSkewLabel.Text = "Skew";
            this.TTBTimeAidingSetBtn.Location = new Point(210, 0x22);
            this.TTBTimeAidingSetBtn.Name = "TTBTimeAidingSetBtn";
            this.TTBTimeAidingSetBtn.Size = new Size(0x4b, 0x17);
            this.TTBTimeAidingSetBtn.TabIndex = 4;
            this.TTBTimeAidingSetBtn.Text = "Set";
            this.TTBTimeAidingSetBtn.UseVisualStyleBackColor = true;
            this.TTBTimeAidingSetBtn.Click += new EventHandler(this.TTBTimeAidingSetBtn__Click);
            this.utcOffsetLabel.AutoSize = true;
            this.utcOffsetLabel.Location = new Point(0xa3, 0x6c);
            this.utcOffsetLabel.Name = "utcOffsetLabel";
            this.utcOffsetLabel.Size = new Size(60, 13);
            this.utcOffsetLabel.TabIndex = 0x15;
            this.utcOffsetLabel.Text = "UTC Offset";
            this.radioButton_useTTBTimeAiding.AutoSize = true;
            this.radioButton_useTTBTimeAiding.Location = new Point(50, 0xad);
            this.radioButton_useTTBTimeAiding.Name = "radioButton_useTTBTimeAiding";
            this.radioButton_useTTBTimeAiding.Size = new Size(0x7e, 0x11);
            this.radioButton_useTTBTimeAiding.TabIndex = 5;
            this.radioButton_useTTBTimeAiding.Text = "Use TTB Time Aiding";
            this.radioButton_useTTBTimeAiding.UseVisualStyleBackColor = true;
            this.radioButton_useTTBTimeAiding.CheckedChanged += new EventHandler(this.radioButton_useTTBTimeAiding_CheckedChanged);
            this.button_SET_TimeTrans.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button_SET_TimeTrans.Location = new Point(0x16f, 0x23);
            this.button_SET_TimeTrans.Name = "button_SET_TimeTrans";
            this.button_SET_TimeTrans.Size = new Size(0x4b, 0x17);
            this.button_SET_TimeTrans.TabIndex = 14;
            this.button_SET_TimeTrans.Text = "SET";
            this.button_SET_TimeTrans.UseVisualStyleBackColor = true;
            this.button_SET_TimeTrans.Click += new EventHandler(this.button_SET_TimeTrans_Click);
            this.radioButton_Reject_TimeTrans.AutoSize = true;
            this.radioButton_Reject_TimeTrans.Location = new Point(50, 0x8d);
            this.radioButton_Reject_TimeTrans.Name = "radioButton_Reject_TimeTrans";
            this.radioButton_Reject_TimeTrans.Size = new Size(0x97, 0x11);
            this.radioButton_Reject_TimeTrans.TabIndex = 4;
            this.radioButton_Reject_TimeTrans.Text = "Reject. Data Not Available";
            this.radioButton_Reject_TimeTrans.UseVisualStyleBackColor = true;
            this.radioButton_Reject_TimeTrans.CheckedChanged += new EventHandler(this.radioButton_Reject_TimeTrans_CheckedChanged);
            this.label45.AutoSize = true;
            this.label45.Location = new Point(0xa3, 0x51);
            this.label45.Name = "label45";
            this.label45.Size = new Size(0x30, 13);
            this.label45.TabIndex = 0x13;
            this.label45.Text = "Skew (s)";
            this.radioButton_useDOSTimeForCoarseTimeAiding.AutoSize = true;
            this.radioButton_useDOSTimeForCoarseTimeAiding.Checked = true;
            this.radioButton_useDOSTimeForCoarseTimeAiding.Location = new Point(50, 0x1b);
            this.radioButton_useDOSTimeForCoarseTimeAiding.Name = "radioButton_useDOSTimeForCoarseTimeAiding";
            this.radioButton_useDOSTimeForCoarseTimeAiding.Size = new Size(0xcd, 0x11);
            this.radioButton_useDOSTimeForCoarseTimeAiding.TabIndex = 0;
            this.radioButton_useDOSTimeForCoarseTimeAiding.TabStop = true;
            this.radioButton_useDOSTimeForCoarseTimeAiding.Text = "Use DOS Time for Coarse Time Aiding";
            this.radioButton_useDOSTimeForCoarseTimeAiding.UseVisualStyleBackColor = true;
            this.radioButton_useDOSTimeForCoarseTimeAiding.CheckedChanged += new EventHandler(this.radioButton_useDOSTimeForCoarseTimeAiding_CheckedChanged);
            this.groupBox6.Controls.Add(this.checkBox_ScreenCPResp_TimeTrans);
            this.groupBox6.Controls.Add(this.checkBox_FwdToCP_TimeTrans);
            this.groupBox6.Controls.Add(this.checkBox_AutoReply_TimeTrans);
            this.groupBox6.Location = new Point(0x16f, 0x4a);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0xbb, 0x55);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Control";
            this.checkBox_ScreenCPResp_TimeTrans.AutoSize = true;
            this.checkBox_ScreenCPResp_TimeTrans.Location = new Point(0x16, 0x3a);
            this.checkBox_ScreenCPResp_TimeTrans.Name = "checkBox_ScreenCPResp_TimeTrans";
            this.checkBox_ScreenCPResp_TimeTrans.Size = new Size(0x80, 0x11);
            this.checkBox_ScreenCPResp_TimeTrans.TabIndex = 2;
            this.checkBox_ScreenCPResp_TimeTrans.Text = "Screen CP Response";
            this.checkBox_ScreenCPResp_TimeTrans.UseVisualStyleBackColor = true;
            this.checkBox_FwdToCP_TimeTrans.AutoSize = true;
            this.checkBox_FwdToCP_TimeTrans.Location = new Point(0x16, 40);
            this.checkBox_FwdToCP_TimeTrans.Name = "checkBox_FwdToCP_TimeTrans";
            this.checkBox_FwdToCP_TimeTrans.Size = new Size(0x88, 0x11);
            this.checkBox_FwdToCP_TimeTrans.TabIndex = 1;
            this.checkBox_FwdToCP_TimeTrans.Text = "Forward Request to CP";
            this.checkBox_FwdToCP_TimeTrans.UseVisualStyleBackColor = true;
            this.checkBox_AutoReply_TimeTrans.AutoSize = true;
            this.checkBox_AutoReply_TimeTrans.Location = new Point(0x16, 0x15);
            this.checkBox_AutoReply_TimeTrans.Name = "checkBox_AutoReply_TimeTrans";
            this.checkBox_AutoReply_TimeTrans.Size = new Size(0x4b, 0x11);
            this.checkBox_AutoReply_TimeTrans.TabIndex = 0;
            this.checkBox_AutoReply_TimeTrans.Text = "AutoReply";
            this.checkBox_AutoReply_TimeTrans.UseVisualStyleBackColor = true;
            this.textBox_Accuracy_TimeTrans.Location = new Point(0x49, 50);
            this.textBox_Accuracy_TimeTrans.Name = "textBox_Accuracy_TimeTrans";
            this.textBox_Accuracy_TimeTrans.Size = new Size(0x57, 20);
            this.textBox_Accuracy_TimeTrans.TabIndex = 1;
            this.textBox_Accuracy_TimeTrans.Text = "0";
            this.label46.AutoSize = true;
            this.label46.Location = new Point(0xa3, 0x36);
            this.label46.Name = "label46";
            this.label46.Size = new Size(0x4a, 13);
            this.label46.TabIndex = 20;
            this.label46.Text = "Accuracy (ms)";
            this.textBox_Skew_TimeTrans.Location = new Point(0x49, 0x4d);
            this.textBox_Skew_TimeTrans.Name = "textBox_Skew_TimeTrans";
            this.textBox_Skew_TimeTrans.Size = new Size(0x57, 20);
            this.textBox_Skew_TimeTrans.TabIndex = 2;
            this.textBox_Skew_TimeTrans.Text = "0.0";
            this.textBox_UtcOffset.Location = new Point(0x49, 0x68);
            this.textBox_UtcOffset.Name = "textBox_UtcOffset";
            this.textBox_UtcOffset.Size = new Size(0x57, 20);
            this.textBox_UtcOffset.TabIndex = 3;
            this.textBox_UtcOffset.Text = "0.0";
            this.tabPage_Pos_Request.Controls.Add(this.label61);
            this.tabPage_Pos_Request.Controls.Add(this.label60);
            this.tabPage_Pos_Request.Controls.Add(this.label57);
            this.tabPage_Pos_Request.Controls.Add(this.label58);
            this.tabPage_Pos_Request.Controls.Add(this.button_Send_Pos_Request);
            this.tabPage_Pos_Request.Controls.Add(this.checkBox_AutoSend_PosRequest);
            this.tabPage_Pos_Request.Controls.Add(this.label47);
            this.tabPage_Pos_Request.Controls.Add(this.numericUpDown_ResponseTimeMax);
            this.tabPage_Pos_Request.Controls.Add(this.label48);
            this.tabPage_Pos_Request.Controls.Add(this.button_Set_PosRequest);
            this.tabPage_Pos_Request.Controls.Add(this.label49);
            this.tabPage_Pos_Request.Controls.Add(this.numericUpDown_VertErrMax);
            this.tabPage_Pos_Request.Controls.Add(this.label51);
            this.tabPage_Pos_Request.Controls.Add(this.label50);
            this.tabPage_Pos_Request.Controls.Add(this.numericUpDown_HorrErrMax);
            this.tabPage_Pos_Request.Controls.Add(this.label52);
            this.tabPage_Pos_Request.Controls.Add(this.comboBox_PositionMethod);
            this.tabPage_Pos_Request.Controls.Add(this.label_53);
            this.tabPage_Pos_Request.Controls.Add(this.comboBox_Priority);
            this.tabPage_Pos_Request.Controls.Add(this.numericUpDown_TimeBetweenFixes);
            this.tabPage_Pos_Request.Controls.Add(this.numericUpDown_NumOfFixes);
            this.tabPage_Pos_Request.Location = new Point(4, 0x16);
            this.tabPage_Pos_Request.Name = "tabPage_Pos_Request";
            this.tabPage_Pos_Request.Padding = new Padding(3);
            this.tabPage_Pos_Request.Size = new Size(0x27b, 0x1f5);
            this.tabPage_Pos_Request.TabIndex = 4;
            this.tabPage_Pos_Request.Text = "Position Request";
            this.tabPage_Pos_Request.UseVisualStyleBackColor = true;
            this.label61.AutoSize = true;
            this.label61.ForeColor = SystemColors.ControlDarkDark;
            this.label61.Location = new Point(0x143, 0x42);
            this.label61.Name = "label61";
            this.label61.Size = new Size(0x4e, 13);
            this.label61.TabIndex = 0x10;
            this.label61.Text = "0 = Continuous";
            this.label60.AutoSize = true;
            this.label60.ForeColor = SystemColors.ControlDarkDark;
            this.label60.Location = new Point(0x143, 0xc7);
            this.label60.Name = "label60";
            this.label60.Size = new Size(0x3f, 13);
            this.label60.TabIndex = 15;
            this.label60.Text = "0 = No Limit";
            this.label57.AutoSize = true;
            this.label57.ForeColor = SystemColors.ControlDarkDark;
            this.label57.Location = new Point(0x143, 0x7a);
            this.label57.Name = "label57";
            this.label57.Size = new Size(0x87, 13);
            this.label57.TabIndex = 14;
            this.label57.Text = "0 ~ 255    0 = No Maximum";
            this.label58.AutoSize = true;
            this.label58.ForeColor = SystemColors.ControlDarkDark;
            this.label58.Location = new Point(0x43, 170);
            this.label58.Name = "label58";
            this.label58.Size = new Size(0x18d, 13);
            this.label58.TabIndex = 13;
            this.label58.Text = "0: <1 m,  1: <5 m,  2: <10m,  3: <20m, 4: <40m, 5: <80m, 6: <160m, 7: No Maximum";
            this.button_Send_Pos_Request.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button_Send_Pos_Request.Location = new Point(0x1e8, 0x6c);
            this.button_Send_Pos_Request.Name = "button_Send_Pos_Request";
            this.button_Send_Pos_Request.Size = new Size(0x4f, 0x17);
            this.button_Send_Pos_Request.TabIndex = 9;
            this.button_Send_Pos_Request.Text = "Send Now";
            this.button_Send_Pos_Request.UseVisualStyleBackColor = true;
            this.button_Send_Pos_Request.Click += new EventHandler(this.button_Send_Pos_Request_Click);
            this.checkBox_AutoSend_PosRequest.AutoSize = true;
            this.checkBox_AutoSend_PosRequest.Location = new Point(0x1e8, 0x25);
            this.checkBox_AutoSend_PosRequest.Name = "checkBox_AutoSend_PosRequest";
            this.checkBox_AutoSend_PosRequest.Size = new Size(0x49, 0x11);
            this.checkBox_AutoSend_PosRequest.TabIndex = 7;
            this.checkBox_AutoSend_PosRequest.Text = "AutoSend";
            this.checkBox_AutoSend_PosRequest.UseVisualStyleBackColor = true;
            this.label47.AutoSize = true;
            this.label47.Location = new Point(0x43, 0xe0);
            this.label47.Name = "label47";
            this.label47.Size = new Size(0x26, 13);
            this.label47.TabIndex = 3;
            this.label47.Text = "Priority";
            this.numericUpDown_ResponseTimeMax.Location = new Point(0xc7, 0xc3);
            this.numericUpDown_ResponseTimeMax.Name = "numericUpDown_ResponseTimeMax";
            this.numericUpDown_ResponseTimeMax.Size = new Size(120, 20);
            this.numericUpDown_ResponseTimeMax.TabIndex = 5;
            this.label48.AutoSize = true;
            this.label48.Location = new Point(0x43, 0xc7);
            this.label48.Name = "label48";
            this.label48.Size = new Size(0x76, 13);
            this.label48.TabIndex = 3;
            this.label48.Text = "Response Time Max (s)";
            this.button_Set_PosRequest.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button_Set_PosRequest.Location = new Point(0x1e8, 0x4b);
            this.button_Set_PosRequest.Name = "button_Set_PosRequest";
            this.button_Set_PosRequest.Size = new Size(0x4d, 0x17);
            this.button_Set_PosRequest.TabIndex = 8;
            this.button_Set_PosRequest.Text = "SET";
            this.button_Set_PosRequest.UseVisualStyleBackColor = true;
            this.button_Set_PosRequest.Click += new EventHandler(this.button_Set_PosRequest_Click);
            this.label49.AutoSize = true;
            this.label49.Location = new Point(0x43, 150);
            this.label49.Name = "label49";
            this.label49.Size = new Size(90, 13);
            this.label49.TabIndex = 3;
            this.label49.Text = "Vertical Error Max";
            this.numericUpDown_VertErrMax.Location = new Point(0xc7, 0x92);
            this.numericUpDown_VertErrMax.Name = "numericUpDown_VertErrMax";
            this.numericUpDown_VertErrMax.Size = new Size(120, 20);
            this.numericUpDown_VertErrMax.TabIndex = 4;
            this.label51.AutoSize = true;
            this.label51.Location = new Point(0x43, 0x7a);
            this.label51.Name = "label51";
            this.label51.Size = new Size(0x77, 13);
            this.label51.TabIndex = 3;
            this.label51.Text = "Horizontal Error Max (m)";
            this.label50.AutoSize = true;
            this.label50.Location = new Point(0x43, 0x5e);
            this.label50.Name = "label50";
            this.label50.Size = new Size(0x74, 13);
            this.label50.TabIndex = 3;
            this.label50.Text = "Time Between Fixes (s)";
            this.numericUpDown_HorrErrMax.Location = new Point(0xc7, 0x76);
            int[] numArray5 = new int[4];
            numArray5[0] = 0xff;
            this.numericUpDown_HorrErrMax.Maximum = new decimal(numArray5);
            this.numericUpDown_HorrErrMax.Name = "numericUpDown_HorrErrMax";
            this.numericUpDown_HorrErrMax.Size = new Size(120, 20);
            this.numericUpDown_HorrErrMax.TabIndex = 3;
            this.label52.AutoSize = true;
            this.label52.Location = new Point(0x43, 0x42);
            this.label52.Name = "label52";
            this.label52.Size = new Size(0x44, 13);
            this.label52.TabIndex = 3;
            this.label52.Text = "Num of Fixes";
            this.comboBox_PositionMethod.FormattingEnabled = true;
            this.comboBox_PositionMethod.Items.AddRange(new object[] { "MS Assisted", "MS Based", "MS Based Preferred", "MS Assist Preferred", "Simul. MSB & MSA", "Coarse Location" });
            this.comboBox_PositionMethod.Location = new Point(0xc7, 0x21);
            this.comboBox_PositionMethod.Name = "comboBox_PositionMethod";
            this.comboBox_PositionMethod.Size = new Size(120, 0x15);
            this.comboBox_PositionMethod.TabIndex = 0;
            this.label_53.AutoSize = true;
            this.label_53.Location = new Point(0x43, 0x25);
            this.label_53.Name = "label_53";
            this.label_53.Size = new Size(0x57, 13);
            this.label_53.TabIndex = 2;
            this.label_53.Text = "Location Method";
            this.comboBox_Priority.FormattingEnabled = true;
            this.comboBox_Priority.Items.AddRange(new object[] { "No Priority", "Response Time", "Position Error", "Use Entire Response Time" });
            this.comboBox_Priority.Location = new Point(0xc7, 0xdd);
            this.comboBox_Priority.Name = "comboBox_Priority";
            this.comboBox_Priority.Size = new Size(0x9d, 0x15);
            this.comboBox_Priority.TabIndex = 6;
            this.numericUpDown_TimeBetweenFixes.Location = new Point(0xc7, 90);
            int[] numArray6 = new int[4];
            numArray6[0] = 0xff;
            this.numericUpDown_TimeBetweenFixes.Maximum = new decimal(numArray6);
            this.numericUpDown_TimeBetweenFixes.Name = "numericUpDown_TimeBetweenFixes";
            this.numericUpDown_TimeBetweenFixes.Size = new Size(120, 20);
            this.numericUpDown_TimeBetweenFixes.TabIndex = 2;
            this.numericUpDown_NumOfFixes.Location = new Point(0xc7, 0x3e);
            int[] numArray7 = new int[4];
            numArray7[0] = 0xff;
            this.numericUpDown_NumOfFixes.Maximum = new decimal(numArray7);
            this.numericUpDown_NumOfFixes.Name = "numericUpDown_NumOfFixes";
            this.numericUpDown_NumOfFixes.Size = new Size(120, 20);
            this.numericUpDown_NumOfFixes.TabIndex = 1;
            this.tabPage_Aiding.Controls.Add(this.button_sendNow_NavBit);
            this.tabPage_Aiding.Controls.Add(this.button_sendNow_Alm);
            this.tabPage_Aiding.Controls.Add(this.button_SetNavBitSrc);
            this.tabPage_Aiding.Controls.Add(this.button_SetAlmanacSrc);
            this.tabPage_Aiding.Controls.Add(this.groupBox16);
            this.tabPage_Aiding.Controls.Add(this.groupBox15);
            this.tabPage_Aiding.Controls.Add(this.groupBox14);
            this.tabPage_Aiding.Controls.Add(this.button_Send_AcqAssist);
            this.tabPage_Aiding.Controls.Add(this.button_Send_EphClkCorr);
            this.tabPage_Aiding.Controls.Add(this.groupBox17);
            this.tabPage_Aiding.Controls.Add(this.button_SetAcqAssist);
            this.tabPage_Aiding.Controls.Add(this.button_SetEphClkCorr);
            this.tabPage_Aiding.Location = new Point(4, 0x16);
            this.tabPage_Aiding.Name = "tabPage_Aiding";
            this.tabPage_Aiding.Padding = new Padding(3);
            this.tabPage_Aiding.Size = new Size(0x27b, 0x1f5);
            this.tabPage_Aiding.TabIndex = 5;
            this.tabPage_Aiding.Text = "Aiding...";
            this.tabPage_Aiding.UseVisualStyleBackColor = true;
            this.button_sendNow_NavBit.Location = new Point(0x1d8, 0x15d);
            this.button_sendNow_NavBit.Name = "button_sendNow_NavBit";
            this.button_sendNow_NavBit.Size = new Size(0x51, 0x17);
            this.button_sendNow_NavBit.TabIndex = 11;
            this.button_sendNow_NavBit.Text = "Send Now";
            this.button_sendNow_NavBit.UseVisualStyleBackColor = true;
            this.button_sendNow_NavBit.Click += new EventHandler(this.button_sendNow_NavBit_Click);
            this.button_sendNow_Alm.Location = new Point(0x1d8, 440);
            this.button_sendNow_Alm.Name = "button_sendNow_Alm";
            this.button_sendNow_Alm.Size = new Size(0x51, 0x17);
            this.button_sendNow_Alm.TabIndex = 9;
            this.button_sendNow_Alm.Text = "Send Now";
            this.button_sendNow_Alm.UseVisualStyleBackColor = true;
            this.button_sendNow_Alm.Click += new EventHandler(this.button_sendNow_Alm_Click);
            this.button_SetNavBitSrc.Location = new Point(0x1d8, 0x13e);
            this.button_SetNavBitSrc.Name = "button_SetNavBitSrc";
            this.button_SetNavBitSrc.Size = new Size(0x51, 0x17);
            this.button_SetNavBitSrc.TabIndex = 10;
            this.button_SetNavBitSrc.Text = "SET";
            this.button_SetNavBitSrc.UseVisualStyleBackColor = true;
            this.button_SetNavBitSrc.Click += new EventHandler(this.button_SetNavBitSrc_Click);
            this.button_SetAlmanacSrc.Location = new Point(0x1d8, 0x199);
            this.button_SetAlmanacSrc.Name = "button_SetAlmanacSrc";
            this.button_SetAlmanacSrc.Size = new Size(0x51, 0x17);
            this.button_SetAlmanacSrc.TabIndex = 8;
            this.button_SetAlmanacSrc.Text = "SET";
            this.button_SetAlmanacSrc.UseVisualStyleBackColor = true;
            this.button_SetAlmanacSrc.Click += new EventHandler(this.button_SetAlmanacSrc_Click);
            this.groupBox16.Controls.Add(this.textBox_AcqAssistSrc_File);
            this.groupBox16.Controls.Add(this.checkBox_AutoSend_AcqAssist);
            this.groupBox16.Controls.Add(this.radioButton_AcqAssistSrc_None);
            this.groupBox16.Controls.Add(this.button_Browse_AcqAssistSrc_File);
            this.groupBox16.Controls.Add(this.radioButton_AcqAssistSrc_TTB);
            this.groupBox16.Controls.Add(this.radioButton_AcqAssistSrc_File);
            this.groupBox16.Location = new Point(0x52, 0xc9);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new Size(0x161, 90);
            this.groupBox16.TabIndex = 1;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Acquisition Assistance Source";
            this.textBox_AcqAssistSrc_File.Location = new Point(0x8a, 0x3a);
            this.textBox_AcqAssistSrc_File.Name = "textBox_AcqAssistSrc_File";
            this.textBox_AcqAssistSrc_File.Size = new Size(0x8a, 20);
            this.textBox_AcqAssistSrc_File.TabIndex = 3;
            this.checkBox_AutoSend_AcqAssist.AutoSize = true;
            this.checkBox_AutoSend_AcqAssist.Location = new Point(0xfc, 0x16);
            this.checkBox_AutoSend_AcqAssist.Name = "checkBox_AutoSend_AcqAssist";
            this.checkBox_AutoSend_AcqAssist.Size = new Size(0x49, 0x11);
            this.checkBox_AutoSend_AcqAssist.TabIndex = 5;
            this.checkBox_AutoSend_AcqAssist.Text = "AutoSend";
            this.checkBox_AutoSend_AcqAssist.UseVisualStyleBackColor = true;
            this.radioButton_AcqAssistSrc_None.AutoSize = true;
            this.radioButton_AcqAssistSrc_None.Location = new Point(0x15, 0x16);
            this.radioButton_AcqAssistSrc_None.Name = "radioButton_AcqAssistSrc_None";
            this.radioButton_AcqAssistSrc_None.Size = new Size(0x33, 0x11);
            this.radioButton_AcqAssistSrc_None.TabIndex = 0;
            this.radioButton_AcqAssistSrc_None.TabStop = true;
            this.radioButton_AcqAssistSrc_None.Text = "None";
            this.radioButton_AcqAssistSrc_None.UseVisualStyleBackColor = true;
            this.radioButton_AcqAssistSrc_None.CheckedChanged += new EventHandler(this.radioButton_AcqAssistSrc_None_CheckedChanged);
            this.button_Browse_AcqAssistSrc_File.Location = new Point(0x120, 0x39);
            this.button_Browse_AcqAssistSrc_File.Name = "button_Browse_AcqAssistSrc_File";
            this.button_Browse_AcqAssistSrc_File.Size = new Size(0x25, 0x17);
            this.button_Browse_AcqAssistSrc_File.TabIndex = 4;
            this.button_Browse_AcqAssistSrc_File.Text = "   ...   ";
            this.button_Browse_AcqAssistSrc_File.UseVisualStyleBackColor = true;
            this.button_Browse_AcqAssistSrc_File.Click += new EventHandler(this.button_Browse_AcqAssistSrc_File_Click);
            this.radioButton_AcqAssistSrc_TTB.AutoSize = true;
            this.radioButton_AcqAssistSrc_TTB.Location = new Point(0x15, 0x29);
            this.radioButton_AcqAssistSrc_TTB.Name = "radioButton_AcqAssistSrc_TTB";
            this.radioButton_AcqAssistSrc_TTB.Size = new Size(0x2e, 0x11);
            this.radioButton_AcqAssistSrc_TTB.TabIndex = 1;
            this.radioButton_AcqAssistSrc_TTB.TabStop = true;
            this.radioButton_AcqAssistSrc_TTB.Text = "TTB";
            this.radioButton_AcqAssistSrc_TTB.UseVisualStyleBackColor = true;
            this.radioButton_AcqAssistSrc_TTB.CheckedChanged += new EventHandler(this.radioButton_AcqAssistSrc_TTB_CheckedChanged);
            this.radioButton_AcqAssistSrc_File.Location = new Point(0x15, 60);
            this.radioButton_AcqAssistSrc_File.Name = "radioButton_AcqAssistSrc_File";
            this.radioButton_AcqAssistSrc_File.Size = new Size(0x29, 0x11);
            this.radioButton_AcqAssistSrc_File.TabIndex = 2;
            this.radioButton_AcqAssistSrc_File.TabStop = true;
            this.radioButton_AcqAssistSrc_File.Text = "File";
            this.radioButton_AcqAssistSrc_File.UseVisualStyleBackColor = true;
            this.radioButton_AcqAssistSrc_File.CheckedChanged += new EventHandler(this.radioButton_AcqAssistSrc_File_CheckedChanged);
            this.groupBox15.Controls.Add(this.checkBox_AutoSend_Almanac);
            this.groupBox15.Controls.Add(this.radioButton_AlmanacSrc_None);
            this.groupBox15.Controls.Add(this.radioButton_AlmanacSrc_TTB);
            this.groupBox15.Location = new Point(0x52, 0x192);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new Size(0x162, 0x40);
            this.groupBox15.TabIndex = 2;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Almanac Source";
            this.checkBox_AutoSend_Almanac.AutoSize = true;
            this.checkBox_AutoSend_Almanac.Location = new Point(0xfc, 0x16);
            this.checkBox_AutoSend_Almanac.Name = "checkBox_AutoSend_Almanac";
            this.checkBox_AutoSend_Almanac.Size = new Size(0x49, 0x11);
            this.checkBox_AutoSend_Almanac.TabIndex = 2;
            this.checkBox_AutoSend_Almanac.Text = "AutoSend";
            this.checkBox_AutoSend_Almanac.UseVisualStyleBackColor = true;
            this.radioButton_AlmanacSrc_None.AutoSize = true;
            this.radioButton_AlmanacSrc_None.Location = new Point(0x15, 0x13);
            this.radioButton_AlmanacSrc_None.Name = "radioButton_AlmanacSrc_None";
            this.radioButton_AlmanacSrc_None.Size = new Size(0x33, 0x11);
            this.radioButton_AlmanacSrc_None.TabIndex = 0;
            this.radioButton_AlmanacSrc_None.TabStop = true;
            this.radioButton_AlmanacSrc_None.Text = "None";
            this.radioButton_AlmanacSrc_None.UseVisualStyleBackColor = true;
            this.radioButton_AlmanacSrc_TTB.AutoSize = true;
            this.radioButton_AlmanacSrc_TTB.Location = new Point(0x15, 0x29);
            this.radioButton_AlmanacSrc_TTB.Name = "radioButton_AlmanacSrc_TTB";
            this.radioButton_AlmanacSrc_TTB.Size = new Size(0x2e, 0x11);
            this.radioButton_AlmanacSrc_TTB.TabIndex = 1;
            this.radioButton_AlmanacSrc_TTB.TabStop = true;
            this.radioButton_AlmanacSrc_TTB.Text = "TTB";
            this.radioButton_AlmanacSrc_TTB.UseVisualStyleBackColor = true;
            this.groupBox14.Controls.Add(this.checkBox_AutoSend_NavBit);
            this.groupBox14.Controls.Add(this.radioButton_NavBitSrc_None);
            this.groupBox14.Controls.Add(this.radioButton_NavBitSrc_TTB);
            this.groupBox14.Location = new Point(0x53, 0x137);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new Size(0x161, 0x47);
            this.groupBox14.TabIndex = 3;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Nav Bit Source";
            this.checkBox_AutoSend_NavBit.AutoSize = true;
            this.checkBox_AutoSend_NavBit.Location = new Point(0xfc, 0x16);
            this.checkBox_AutoSend_NavBit.Name = "checkBox_AutoSend_NavBit";
            this.checkBox_AutoSend_NavBit.Size = new Size(0x49, 0x11);
            this.checkBox_AutoSend_NavBit.TabIndex = 3;
            this.checkBox_AutoSend_NavBit.Text = "AutoSend";
            this.checkBox_AutoSend_NavBit.UseVisualStyleBackColor = true;
            this.radioButton_NavBitSrc_None.AutoSize = true;
            this.radioButton_NavBitSrc_None.Location = new Point(0x15, 0x16);
            this.radioButton_NavBitSrc_None.Name = "radioButton_NavBitSrc_None";
            this.radioButton_NavBitSrc_None.Size = new Size(0x33, 0x11);
            this.radioButton_NavBitSrc_None.TabIndex = 0;
            this.radioButton_NavBitSrc_None.TabStop = true;
            this.radioButton_NavBitSrc_None.Text = "None";
            this.radioButton_NavBitSrc_None.UseVisualStyleBackColor = true;
            this.radioButton_NavBitSrc_None.CheckedChanged += new EventHandler(this.radioButton_NavBitSrc_None_CheckedChanged);
            this.radioButton_NavBitSrc_TTB.AutoSize = true;
            this.radioButton_NavBitSrc_TTB.Location = new Point(0x15, 0x2d);
            this.radioButton_NavBitSrc_TTB.Name = "radioButton_NavBitSrc_TTB";
            this.radioButton_NavBitSrc_TTB.Size = new Size(0x2e, 0x11);
            this.radioButton_NavBitSrc_TTB.TabIndex = 1;
            this.radioButton_NavBitSrc_TTB.TabStop = true;
            this.radioButton_NavBitSrc_TTB.Text = "TTB";
            this.radioButton_NavBitSrc_TTB.UseVisualStyleBackColor = true;
            this.radioButton_NavBitSrc_TTB.CheckedChanged += new EventHandler(this.radioButton_NavBitSrc_TTB_CheckedChanged);
            this.button_Send_AcqAssist.Location = new Point(0x1d8, 0xfb);
            this.button_Send_AcqAssist.Name = "button_Send_AcqAssist";
            this.button_Send_AcqAssist.Size = new Size(0x51, 0x17);
            this.button_Send_AcqAssist.TabIndex = 7;
            this.button_Send_AcqAssist.Text = "Send Now";
            this.button_Send_AcqAssist.UseVisualStyleBackColor = true;
            this.button_Send_AcqAssist.Click += new EventHandler(this.button_Send_AcqAssist_Click);
            this.button_Send_EphClkCorr.Location = new Point(0x1d8, 80);
            this.button_Send_EphClkCorr.Name = "button_Send_EphClkCorr";
            this.button_Send_EphClkCorr.Size = new Size(0x51, 0x17);
            this.button_Send_EphClkCorr.TabIndex = 5;
            this.button_Send_EphClkCorr.Text = "Send Now";
            this.button_Send_EphClkCorr.UseVisualStyleBackColor = true;
            this.button_Send_EphClkCorr.Click += new EventHandler(this.button_Send_EphClkCorr_Click);
            this.groupBox17.Controls.Add(this.ephCommentLabel);
            this.groupBox17.Controls.Add(this.textBox_EphSrc_FilePath);
            this.groupBox17.Controls.Add(this.checkBox_AutoSend_Eph);
            this.groupBox17.Controls.Add(this.textBox_EphSrc_ExtEphFile);
            this.groupBox17.Controls.Add(this.button_Browse_EphSrc_File);
            this.groupBox17.Controls.Add(this.button_Browse_EphSrc_ExtEphFile);
            this.groupBox17.Controls.Add(this.radioButton_EphSrc_File);
            this.groupBox17.Controls.Add(this.radioButton_EphSrc_ExtEphFile);
            this.groupBox17.Controls.Add(this.radioButton_EphSrc_TTB);
            this.groupBox17.Controls.Add(this.radioButton_EphSrc_None);
            this.groupBox17.Location = new Point(0x52, 0x18);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new Size(0x161, 0x9d);
            this.groupBox17.TabIndex = 0;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Ephemeris Source";
            this.textBox_EphSrc_FilePath.Location = new Point(0x8a, 0x26);
            this.textBox_EphSrc_FilePath.Name = "textBox_EphSrc_FilePath";
            this.textBox_EphSrc_FilePath.Size = new Size(0x8a, 20);
            this.textBox_EphSrc_FilePath.TabIndex = 2;
            this.checkBox_AutoSend_Eph.AutoSize = true;
            this.checkBox_AutoSend_Eph.Location = new Point(0xfc, 0x11);
            this.checkBox_AutoSend_Eph.Name = "checkBox_AutoSend_Eph";
            this.checkBox_AutoSend_Eph.Size = new Size(0x49, 0x11);
            this.checkBox_AutoSend_Eph.TabIndex = 8;
            this.checkBox_AutoSend_Eph.Text = "AutoSend";
            this.checkBox_AutoSend_Eph.UseVisualStyleBackColor = true;
            this.textBox_EphSrc_ExtEphFile.Location = new Point(0x8a, 0x4b);
            this.textBox_EphSrc_ExtEphFile.Name = "textBox_EphSrc_ExtEphFile";
            this.textBox_EphSrc_ExtEphFile.Size = new Size(0x8a, 20);
            this.textBox_EphSrc_ExtEphFile.TabIndex = 6;
            this.button_Browse_EphSrc_File.Location = new Point(0x120, 0x25);
            this.button_Browse_EphSrc_File.Name = "button_Browse_EphSrc_File";
            this.button_Browse_EphSrc_File.Size = new Size(0x25, 0x17);
            this.button_Browse_EphSrc_File.TabIndex = 3;
            this.button_Browse_EphSrc_File.Text = "   ...   ";
            this.button_Browse_EphSrc_File.UseVisualStyleBackColor = true;
            this.button_Browse_EphSrc_File.Click += new EventHandler(this.button_Browse_EphSrc_File_Click);
            this.button_Browse_EphSrc_ExtEphFile.Location = new Point(0x120, 0x4a);
            this.button_Browse_EphSrc_ExtEphFile.Name = "button_Browse_EphSrc_ExtEphFile";
            this.button_Browse_EphSrc_ExtEphFile.Size = new Size(0x25, 0x17);
            this.button_Browse_EphSrc_ExtEphFile.TabIndex = 7;
            this.button_Browse_EphSrc_ExtEphFile.Text = "   ...   ";
            this.button_Browse_EphSrc_ExtEphFile.UseVisualStyleBackColor = true;
            this.button_Browse_EphSrc_ExtEphFile.Click += new EventHandler(this.button_Browse_EphSrc_ExtEphFile_Click);
            this.radioButton_EphSrc_File.AutoSize = true;
            this.radioButton_EphSrc_File.Location = new Point(0x15, 40);
            this.radioButton_EphSrc_File.Name = "radioButton_EphSrc_File";
            this.radioButton_EphSrc_File.Size = new Size(0x29, 0x11);
            this.radioButton_EphSrc_File.TabIndex = 1;
            this.radioButton_EphSrc_File.TabStop = true;
            this.radioButton_EphSrc_File.Text = "File";
            this.radioButton_EphSrc_File.UseVisualStyleBackColor = true;
            this.radioButton_EphSrc_File.CheckedChanged += new EventHandler(this.radioButton_EphSrc_File_CheckedChanged);
            this.radioButton_EphSrc_ExtEphFile.AutoSize = true;
            this.radioButton_EphSrc_ExtEphFile.Location = new Point(0x15, 80);
            this.radioButton_EphSrc_ExtEphFile.Name = "radioButton_EphSrc_ExtEphFile";
            this.radioButton_EphSrc_ExtEphFile.Size = new Size(0x6f, 0x11);
            this.radioButton_EphSrc_ExtEphFile.TabIndex = 5;
            this.radioButton_EphSrc_ExtEphFile.TabStop = true;
            this.radioButton_EphSrc_ExtEphFile.Text = "Extended Eph File";
            this.radioButton_EphSrc_ExtEphFile.UseVisualStyleBackColor = true;
            this.radioButton_EphSrc_ExtEphFile.CheckedChanged += new EventHandler(this.radioButton_EphSrc_ExtEphFile_CheckedChanged);
            this.radioButton_EphSrc_TTB.AutoSize = true;
            this.radioButton_EphSrc_TTB.Location = new Point(0x15, 60);
            this.radioButton_EphSrc_TTB.Name = "radioButton_EphSrc_TTB";
            this.radioButton_EphSrc_TTB.Size = new Size(0x2e, 0x11);
            this.radioButton_EphSrc_TTB.TabIndex = 4;
            this.radioButton_EphSrc_TTB.TabStop = true;
            this.radioButton_EphSrc_TTB.Text = "TTB";
            this.radioButton_EphSrc_TTB.UseVisualStyleBackColor = true;
            this.radioButton_EphSrc_TTB.CheckedChanged += new EventHandler(this.radioButton_EphSrc_TTB_CheckedChanged);
            this.radioButton_EphSrc_None.AutoSize = true;
            this.radioButton_EphSrc_None.Location = new Point(0x15, 20);
            this.radioButton_EphSrc_None.Name = "radioButton_EphSrc_None";
            this.radioButton_EphSrc_None.Size = new Size(0x33, 0x11);
            this.radioButton_EphSrc_None.TabIndex = 0;
            this.radioButton_EphSrc_None.TabStop = true;
            this.radioButton_EphSrc_None.Text = "None";
            this.radioButton_EphSrc_None.UseVisualStyleBackColor = true;
            this.radioButton_EphSrc_None.CheckedChanged += new EventHandler(this.radioButton_EphSrc_None_CheckedChanged);
            this.button_SetAcqAssist.Location = new Point(0x1d8, 220);
            this.button_SetAcqAssist.Name = "button_SetAcqAssist";
            this.button_SetAcqAssist.Size = new Size(0x51, 0x17);
            this.button_SetAcqAssist.TabIndex = 6;
            this.button_SetAcqAssist.Text = "SET";
            this.button_SetAcqAssist.UseVisualStyleBackColor = true;
            this.button_SetAcqAssist.Click += new EventHandler(this.button_SetAcqAssist_Click);
            this.button_SetEphClkCorr.Location = new Point(0x1d8, 0x31);
            this.button_SetEphClkCorr.Name = "button_SetEphClkCorr";
            this.button_SetEphClkCorr.Size = new Size(0x51, 0x17);
            this.button_SetEphClkCorr.TabIndex = 4;
            this.button_SetEphClkCorr.Text = "SET";
            this.button_SetEphClkCorr.UseVisualStyleBackColor = true;
            this.button_SetEphClkCorr.Click += new EventHandler(this.button_SetEphClkCorr_Click);
            this.button_done.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button_done.Location = new Point(0x1e2, 0x261);
            this.button_done.Name = "button_done";
            this.button_done.Size = new Size(0x4b, 0x17);
            this.button_done.TabIndex = 14;
            this.button_done.Text = "&Done";
            this.button_done.UseVisualStyleBackColor = true;
            this.button_done.Click += new EventHandler(this.button_done_Click);
            this.combo_box_profile.FormattingEnabled = true;
            this.combo_box_profile.Items.AddRange(new object[] { "Default", "Autonomous", "MSA-1 Coarse", "MSA-1 Precise", "MSA-2 Coarse", "MSA-2 Precise", "MSB Coarse", "MSB Precise", "TESTMODE6", "User Defined" });
            this.combo_box_profile.Location = new Point(0x72, 0x15);
            this.combo_box_profile.Name = "combo_box_profile";
            this.combo_box_profile.Size = new Size(0x79, 0x15);
            this.combo_box_profile.TabIndex = 0;
            this.combo_box_profile.SelectedIndexChanged += new EventHandler(this.combo_box_profile_SelectedIndexChanged);
            this.button_load.Location = new Point(0x12b, 0x13);
            this.button_load.Name = "button_load";
            this.button_load.Size = new Size(0x4b, 0x17);
            this.button_load.TabIndex = 2;
            this.button_load.Text = "&Load";
            this.button_load.UseVisualStyleBackColor = true;
            this.button_load.Click += new EventHandler(this.button_load_Click);
            this.textBox_profileFilePath.Location = new Point(0x72, 0x31);
            this.textBox_profileFilePath.Name = "textBox_profileFilePath";
            this.textBox_profileFilePath.Size = new Size(0x117, 20);
            this.textBox_profileFilePath.TabIndex = 1;
            this.label_profileFilePath.AutoSize = true;
            this.label_profileFilePath.Location = new Point(0x15, 0x35);
            this.label_profileFilePath.Name = "label_profileFilePath";
            this.label_profileFilePath.Size = new Size(0x54, 13);
            this.label_profileFilePath.TabIndex = 13;
            this.label_profileFilePath.Text = "Config File Path:";
            this.button_autoReplySummary.Location = new Point(0x1cf, 0x22c);
            this.button_autoReplySummary.Name = "button_autoReplySummary";
            this.button_autoReplySummary.Size = new Size(0xc5, 0x17);
            this.button_autoReplySummary.TabIndex = 13;
            this.button_autoReplySummary.Text = "&View Current Auto Reply Config";
            this.button_autoReplySummary.UseVisualStyleBackColor = true;
            this.button_autoReplySummary.Click += new EventHandler(this.button_autoReplySummary_Click);
            this.groupBox7.Controls.Add(this.label53);
            this.groupBox7.Controls.Add(this.label_profileFilePath);
            this.groupBox7.Controls.Add(this.combo_box_profile);
            this.groupBox7.Controls.Add(this.textBox_profileFilePath);
            this.groupBox7.Controls.Add(this.button_load);
            this.groupBox7.Location = new Point(0x19, 0x225);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new Size(0x19b, 0x5b);
            this.groupBox7.TabIndex = 12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Predefined config file";
            this.label53.AutoSize = true;
            this.label53.Location = new Point(0x33, 0x19);
            this.label53.Name = "label53";
            this.label53.Size = new Size(0x36, 13);
            this.label53.TabIndex = 13;
            this.label53.Text = "Selection:";
            this.ephCommentLabel.AutoSize = true;
            this.ephCommentLabel.Location = new Point(0x18, 0x6b);
            this.ephCommentLabel.Name = "ephCommentLabel";
            this.ephCommentLabel.Size = new Size(0x11e, 0x27);
            this.ephCommentLabel.TabIndex = 9;
            this.ephCommentLabel.Text = "MSA mode: Eph is needed for the tool to post processing \r\nposition fix calculation only. Eph will not be sent to the DUT\r\nas part of aiding information.";
            base.AcceptButton = this.button_done;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            base.CancelButton = this.button_cancel;
            base.ClientSize = new Size(0x2ad, 0x287);
            base.Controls.Add(this.groupBox7);
            base.Controls.Add(this.button_autoReplySummary);
            base.Controls.Add(this.button_done);
            base.Controls.Add(this.tabControl_autoReply);
            base.Controls.Add(this.button_cancel);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "frmAutoReply";
            base.ShowInTaskbar = false;
            base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "AutoReply";
            base.Load += new EventHandler(this.frmAutpReplyConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl_autoReply.ResumeLayout(false);
            this.tabPage_HWCfg.ResumeLayout(false);
            this.tabPage_HWCfg.PerformLayout();
            this.tabPage_FreqTrans.ResumeLayout(false);
            this.tabPage_FreqTrans.PerformLayout();
            this.scaleFreqOffsetGrpBox.ResumeLayout(false);
            this.scaleFreqOffsetGrpBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage_ApproxPos.ResumeLayout(false);
            this.tabPage_ApproxPos.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.numericUpDown_EstVertErr.EndInit();
            this.numericUpDown_EstHoriErr.EndInit();
            this.tabPage_TimeTrans.ResumeLayout(false);
            this.tabPage_TimeTrans.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabPage_Pos_Request.ResumeLayout(false);
            this.tabPage_Pos_Request.PerformLayout();
            this.numericUpDown_ResponseTimeMax.EndInit();
            this.numericUpDown_VertErrMax.EndInit();
            this.numericUpDown_HorrErrMax.EndInit();
            this.numericUpDown_TimeBetweenFixes.EndInit();
            this.numericUpDown_NumOfFixes.EndInit();
            this.tabPage_Aiding.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            base.ResumeLayout(false);
        }

        private void NavBitSrcGUIUpdate()
        {
            if (!this.checkBox_AutoSend_NavBit.Checked || this.radioButton_NavBitSrc_None.Checked)
            {
                this.button_SetNavBitSrc.ForeColor = Color.Black;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.NavBitSource = 0;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.NavBitReply = 0;
                this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_NavBit = false;
            }
            else if (this.checkBox_AutoSend_NavBit.Checked)
            {
                this.button_SetNavBitSrc.ForeColor = Color.Brown;
                if (this.radioButton_NavBitSrc_TTB.Checked)
                {
                    if (this.comm.TTBPort.IsOpen)
                    {
                        this.comm.AutoReplyCtrl.SetupAuxNavMsgFromTTB(this.comm.GetAuxNavDataFromTTB());
                        this.comm.AutoReplyCtrl.SF45DataSet0MsgFromTTB = this.comm.AutoReplyCtrl.SetupNavSF45FromTTB(this.comm.GetNavBitSF45DataSet0FromTTB());
                        this.comm.AutoReplyCtrl.SF45DataSet1MsgFromTTB = this.comm.AutoReplyCtrl.SetupNavSF45FromTTB(this.comm.GetNavBitSF45DataSet1FromTTB());
                    }
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_NavBit = true;
                    this.comm.AutoReplyCtrl.PositionRequestCtrl.NavBitSource = 1;
                    this.comm.AutoReplyCtrl.PositionRequestCtrl.NavBitReply = 1;
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (!this._fileSaved)
            {
                this.saveAutoReplyData(this._autoReplyConfigFilePath);
            }
            base.OnClosed(e);
        }

        private void PosRequestGUIUpdate()
        {
            string listenerName = string.Empty;
            if (!this.checkBox_AutoSend_PosRequest.Checked)
            {
                this.button_Set_PosRequest.ForeColor = Color.Black;
                this.comm.AutoReplyCtrl.AutoReplyParams.AutoPosReq = false;
                if (this.comm.ListenersCtrl != null)
                {
                    listenerName = "GeodeticNavigationData_GUI";
                    this.comm.ListenersCtrl.Stop(listenerName, this.comm.PortName);
                    this.comm.ListenersCtrl.Start(listenerName, this.comm.PortName);
                    listenerName = "TTFF_GUI";
                    this.comm.ListenersCtrl.Stop(listenerName, this.comm.PortName);
                    this.comm.ListenersCtrl.Start(listenerName, this.comm.PortName);
                }
            }
            else
            {
                if (this.comm.ListenersCtrl != null)
                {
                    listenerName = "PostionResponse_GUI";
                    this.comm.ListenersCtrl.Start(listenerName, this.comm.PortName);
                    if (this.comm.MessageProtocol == "OSP")
                    {
                        listenerName = "TTFF_MSA_GUI";
                        this.comm.ListenersCtrl.Stop(listenerName, this.comm.PortName);
                        this.comm.ListenersCtrl.Start(listenerName, this.comm.PortName);
                    }
                    else
                    {
                        listenerName = "TTFF_GUI";
                        this.comm.ListenersCtrl.Stop(listenerName, this.comm.PortName);
                        this.comm.ListenersCtrl.Start(listenerName, this.comm.PortName);
                    }
                }
                this.button_Set_PosRequest.ForeColor = Color.Brown;
                this.comm.AutoReplyCtrl.AutoReplyParams.AutoPosReq = true;
                this.setupPosRequesMsg();
            }
        }

        private void radioButton_AcqAssistSrc_File_CheckedChanged(object sender, EventArgs e)
        {
            this.updateAidingCtrlsOnAcqAssistSrcRadioChange();
        }

        private void radioButton_AcqAssistSrc_None_CheckedChanged(object sender, EventArgs e)
        {
            this.updateAidingCtrlsOnAcqAssistSrcRadioChange();
        }

        private void radioButton_AcqAssistSrc_TTB_CheckedChanged(object sender, EventArgs e)
        {
            this.updateAidingCtrlsOnAcqAssistSrcRadioChange();
        }

        private void radioButton_EphSrc_ExtEphFile_CheckedChanged(object sender, EventArgs e)
        {
            this.updateAidingCtrlsOnEphSrcRadioChange();
        }

        private void radioButton_EphSrc_File_CheckedChanged(object sender, EventArgs e)
        {
            this.updateAidingCtrlsOnEphSrcRadioChange();
        }

        private void radioButton_EphSrc_None_CheckedChanged(object sender, EventArgs e)
        {
            this.updateAidingCtrlsOnEphSrcRadioChange();
        }

        private void radioButton_EphSrc_TTB_CheckedChanged(object sender, EventArgs e)
        {
            this.updateAidingCtrlsOnEphSrcRadioChange();
        }

        private void radioButton_NavBitSrc_None_CheckedChanged(object sender, EventArgs e)
        {
            this.updateAidingCtrlsOnNavBitSrcRadioChange();
        }

        private void radioButton_NavBitSrc_TTB_CheckedChanged(object sender, EventArgs e)
        {
            this.updateAidingCtrlsOnNavBitSrcRadioChange();
        }

        private void radioButton_Reject_Freq_CheckedChanged(object sender, EventArgs e)
        {
            this.updateFreqCtrlsWhenRadioChange();
        }

        private void radioButton_Reject_TimeTrans_CheckedChanged(object sender, EventArgs e)
        {
            this.setTimeAidingGUIStatus();
        }

        private void radioButton_SpecifyFreqParam_CheckedChanged(object sender, EventArgs e)
        {
            this.updateFreqCtrlsWhenRadioChange();
        }

        private void radioButton_useDOSTimeForCoarseTimeAiding_CheckedChanged(object sender, EventArgs e)
        {
            this.setTimeAidingGUIStatus();
        }

        private void radioButton_UseRxRptFreq_CheckedChanged(object sender, EventArgs e)
        {
            this.updateFreqCtrlsWhenRadioChange();
        }

        private void radioButton_useTTBTimeAiding_CheckedChanged(object sender, EventArgs e)
        {
            this.setTimeAidingGUIStatus();
        }

        private void saveAutoReplyData(string filepath)
        {
            if (File.Exists(filepath))
            {
                if ((File.GetAttributes(filepath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    MessageBox.Show(string.Format("Readonly file\n{0}\nPlease change file property and retry", filepath), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
            {
                filepath = clsGlobal.InstalledDirectory + @"\scripts\SiRFLiveAutomationSetupAutoReply.cfg";
            }
            this.writeAutoReplyData(filepath);
        }

        private void setDefault()
        {
            this.comboBox_PreciseTimeAvail.Text = "NO";
            this.comboBox_PreciseTimeTransDirection.Text = "CP --> SLC";
            this.comboBox_FreqTransAvail.Text = "YES";
            this.comboBox_FreqTransMethod.Text = "Non-Counter (SLC Clk)";
            this.comboBox_RTCAvail.Text = "NO";
            this.comboBox_RTC_Resource.Text = "External to GPS";
            this.comboBox_CoarseTimeTransAvail.Text = "YES";
            this.comboBox_RefClkStatus.Text = "ON";
            this.comboBox_NorminalFreq.Text = "19200000";
            this.textBox_NWEnhanceType.Text = "0";
            this.checkBox_AutoReply_HWCfg.Checked = false;
            this.checkBox_ForwardRequestToCP_HWCfg.Checked = false;
            this.checkBox_ScreenCPResponse_HWCfg.Checked = false;
            this.checkBox_ForwardRequestToCP_HWCfg.Visible = false;
            this.checkBox_ScreenCPResponse_HWCfg.Visible = false;
            this.comboBox_RFDefaultFreq.SelectedIndex = 2;
            this.comboBox_ClkSrc_B1.SelectedIndex = 1;
            this.comboBox_RefClkOnOff_B2.SelectedIndex = 0;
            this.comboBox_RefClkReq_B3.SelectedIndex = 0;
            this.comboBox_IncNormFreq_B4.SelectedIndex = 1;
            this.comboBox_NormFreq_Freq.SelectedIndex = 2;
            this.comboBox_SkewPPM.SelectedIndex = 0;
            this.comboBox_TimeTagValid.SelectedIndex = 0;
            this.radioButton_Reject_Freq.Checked = true;
            this.checkBox_AutoReply_Freq.Checked = false;
            this.radioButton_UseRxRptFreq.Checked = false;
            this.radioButton_1Hz.Checked = true;
            this.updateFreqCtrlsWhenRadioChange();
            this.updateCtrlsWhenIncNormFreqChange();
            this.updateCtrlsWhenClkSrcChange();
            this.checkBox_useTTBFreq.Checked = false;
            this.checkBox_ForwardRequestToCP_Freq.Visible = false;
            this.checkBox_ScreenCPResponse_Freq.Visible = false;
            this.textBox_Latitude.Text = "37.375286";
            this.textBox_Longitude.Text = "-121.914756";
            this.textBox_Altitude.Text = "-17.0";
            this.textBox_distance_skew.Text = "0.000000";
            this.textBox_heading_skew.Text = "0.000000";
            this.checkBox_AutoReply_ApproxPos.Checked = false;
            this.checkBox_FwdReqToCP_ApproxPos.Visible = false;
            this.checkBox_ScreenCPResp_ApproxPos.Visible = false;
            this.radioButton_useDOSTimeForCoarseTimeAiding.Checked = false;
            this.checkBox_AutoReply_TimeTrans.Checked = false;
            this.checkBox_ScreenCPResp_TimeTrans.Visible = false;
            this.checkBox_FwdToCP_TimeTrans.Visible = false;
            this.checkBox_AutoSend_PosRequest.Checked = false;
            this.numericUpDown_NumOfFixes.Value = 0M;
            this.numericUpDown_TimeBetweenFixes.Value = 1M;
            this.numericUpDown_HorrErrMax.Value = 0M;
            this.numericUpDown_VertErrMax.Value = 7M;
            this.numericUpDown_ResponseTimeMax.Value = 0M;
            this.comboBox_Priority.SelectedIndex = 0;
            this.comboBox_PositionMethod.SelectedIndex = 1;
            this.radioButton_EphSrc_None.Checked = true;
            this.checkBox_AutoSend_Eph.Checked = false;
            this.radioButton_AcqAssistSrc_None.Checked = true;
            this.checkBox_AutoSend_AcqAssist.Checked = false;
            this.radioButton_AlmanacSrc_None.Checked = true;
            this.radioButton_AlmanacSrc_None.Enabled = false;
            this.radioButton_AlmanacSrc_TTB.Checked = false;
            this.radioButton_AlmanacSrc_TTB.Enabled = false;
            this.checkBox_AutoSend_Almanac.Checked = false;
            this.checkBox_AutoSend_Almanac.Enabled = false;
            this.button_SetAlmanacSrc.Enabled = false;
            this.button_sendNow_Alm.Enabled = false;
            this.radioButton_NavBitSrc_None.Checked = true;
            this.checkBox_AutoSend_NavBit.Checked = false;
            this.textBox_profileFilePath.Text = this._autoReplyConfigFilePath;
            this.combo_box_profile.SelectedIndex = 0;
        }

        private void setLastTimeValues()
        {
            this.checkBox_AutoReply_HWCfg.Checked = this.comm.AutoReplyCtrl.HWCfgCtrl.Reply;
            this.comboBox_PreciseTimeAvail.SelectedIndex = this.comm.AutoReplyCtrl.HWCfgCtrl.PreciseTimeEnabled;
            this.comboBox_PreciseTimeTransDirection.SelectedIndex = this.comm.AutoReplyCtrl.HWCfgCtrl.PreciseTimeDirection;
            this.comboBox_FreqTransAvail.SelectedIndex = this.comm.AutoReplyCtrl.HWCfgCtrl.FreqAidEnabled;
            this.comboBox_FreqTransMethod.SelectedIndex = this.comm.AutoReplyCtrl.HWCfgCtrl.FreqAidMethod;
            this.comboBox_RTCAvail.SelectedIndex = this.comm.AutoReplyCtrl.HWCfgCtrl.RTCAvailabe;
            this.comboBox_RTC_Resource.SelectedIndex = this.comm.AutoReplyCtrl.HWCfgCtrl.RTCSource;
            this.comboBox_CoarseTimeTransAvail.SelectedIndex = this.comm.AutoReplyCtrl.HWCfgCtrl.CoarseTimeEnabled;
            this.comboBox_RefClkStatus.SelectedIndex = this.comm.AutoReplyCtrl.HWCfgCtrl.RefClkEnabled;
            this.comboBox_NorminalFreq.Text = this.comm.AutoReplyCtrl.HWCfgCtrl.NorminalFreqHz.ToString();
            this.textBox_NWEnhanceType.Text = this.comm.AutoReplyCtrl.HWCfgCtrl.NetworkEnhanceType.ToString();
            this.checkBox_AutoReply_ApproxPos.Checked = this.comm.AutoReplyCtrl.ApproxPositionCtrl.Reply;
            this.numericUpDown_EstHoriErr.Value = (decimal) this.comm.AutoReplyCtrl.ApproxPositionCtrl.EstHorrErr;
            this.numericUpDown_EstVertErr.Value = (decimal) this.comm.AutoReplyCtrl.ApproxPositionCtrl.EstVertiErr;
            this.textBox_distance_skew.Text = this.comm.AutoReplyCtrl.ApproxPositionCtrl.DistanceSkew.ToString();
            this.textBox_heading_skew.Text = this.comm.AutoReplyCtrl.ApproxPositionCtrl.HeadingSkew.ToString();
            this.checkBox_Reject_ApproxPos.Checked = this.comm.AutoReplyCtrl.ApproxPositionCtrl.Reject;
            this.checkBox_AutoReply_Freq.Checked = this.comm.AutoReplyCtrl.FreqTransferCtrl.Reply;
            if (this.comm.AutoReplyCtrl.FreqTransferCtrl.TimeTag == uint.MaxValue)
            {
                this.comboBox_TimeTagValid.SelectedIndex = 1;
            }
            else
            {
                this.comboBox_TimeTagValid.SelectedIndex = 0;
            }
            if (this.comm.AutoReplyCtrl.FreqTransferCtrl.IncludeNormFreq)
            {
                this.comboBox_IncNormFreq_B4.SelectedIndex = 1;
            }
            else
            {
                this.comboBox_IncNormFreq_B4.SelectedIndex = 0;
            }
            this.comboBox_ClkSrc_B1.SelectedIndex = this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqAidingMethod;
            this.comboBox_RFDefaultFreq.SelectedIndex = this.comm.AutoReplyCtrl.FreqTransferCtrl.DefaultFreqIndex;
            this.radioButton_SpecifyFreqParam.Checked = this.comm.AutoReplyCtrl.FreqTransferCtrl.SpecifiedRefFreq;
            this.radioButton_UseRxRptFreq.Checked = this.comm.AutoReplyCtrl.FreqTransferCtrl.SLCReportFreqGuiIndex;
            this.comboBox_RefClkReq_B3.SelectedIndex = this.comm.AutoReplyCtrl.FreqTransferCtrl.RefClockRequestGuiIndex;
            this.comboBox_RefClkOnOff_B2.SelectedIndex = this.comm.AutoReplyCtrl.FreqTransferCtrl.RefClockOnOffGuiIndex;
            this.comboBox_NormFreq_Freq.SelectedIndex = this.comm.AutoReplyCtrl.FreqTransferCtrl.ExtRefClockGuiIndex;
            this.textBox_Freq.Text = this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqOffsetUserSpecifiedGui.ToString();
            this.textBox_FreqAuto.Text = this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqOffsetFromRxGui.ToString();
            this.textBox_Accuracy.Text = this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqAccUserSpecifiedGui.ToString();
            this.textBox_AccuracyAuto.Text = this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqAccFromRxGui.ToString();
            if (this.comm.AutoReplyCtrl.FreqTransferCtrl.ScaledFreqOffsetGuiIndex == 1)
            {
                this.radioButton_1Hz.Checked = true;
            }
            else if (this.comm.AutoReplyCtrl.FreqTransferCtrl.ScaledFreqOffsetGuiIndex == 2)
            {
                this.radioButton_2Hz.Checked = true;
            }
            else if (this.comm.AutoReplyCtrl.FreqTransferCtrl.ScaledFreqOffsetGuiIndex == 3)
            {
                this.radioButton_4Hz.Checked = true;
            }
            if (this.comm.AutoReplyCtrl.FreqTransferCtrl.UseFreqAiding == 2)
            {
                this.radioButton_Reject_Freq.Checked = true;
            }
            else
            {
                this.radioButton_Reject_Freq.Checked = false;
            }
            this.checkBox_useTTBFreq.Checked = this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForFreqAid;
            this.checkBox_ignoreXO.Checked = this.comm.AutoReplyCtrl.AutoReplyParams.FreqAidingIgnoreXO;
            this.checkBox_AutoReply_TimeTrans.Checked = this.comm.AutoReplyCtrl.TimeTransferCtrl.Reply;
            this.textBox_Accuracy_TimeTrans.Text = this.comm.AutoReplyCtrl.TimeTransferCtrl.Accuracy.ToString();
            this.textBox_Skew_TimeTrans.Text = this.comm.AutoReplyCtrl.TimeTransferCtrl.Skew.ToString();
            this.textBox_UtcOffset.Text = this.comm.RxCtrl.UTCOffset.ToString();
            this.radioButton_useDOSTimeForCoarseTimeAiding.Checked = this.comm.AutoReplyCtrl.AutoReplyParams.UseDOS_ForTimeAid;
            this.radioButton_useTTBTimeAiding.Checked = this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForTimeAid;
            this.radioButton_Reject_TimeTrans.Checked = this.comm.AutoReplyCtrl.TimeTransferCtrl.Reject;
            this.numericUpDown_NumOfFixes.Value = this.comm.AutoReplyCtrl.PositionRequestCtrl.NumFixes;
            this.numericUpDown_TimeBetweenFixes.Value = this.comm.AutoReplyCtrl.PositionRequestCtrl.TimeBtwFixes;
            this.numericUpDown_HorrErrMax.Value = this.comm.AutoReplyCtrl.PositionRequestCtrl.HorrErrMax;
            this.numericUpDown_VertErrMax.Value = this.comm.AutoReplyCtrl.PositionRequestCtrl.VertErrMax;
            this.numericUpDown_ResponseTimeMax.Value = this.comm.AutoReplyCtrl.PositionRequestCtrl.RespTimeMax;
            this.comboBox_Priority.SelectedIndex = this.comm.AutoReplyCtrl.PositionRequestCtrl.TimeAccPriority;
            this.comboBox_PositionMethod.SelectedIndex = this.comm.AutoReplyCtrl.PositionRequestCtrl.LocMethod;
            this.checkBox_AutoSend_PosRequest.Checked = this.comm.AutoReplyCtrl.AutoReplyParams.AutoPosReq;
            if ((!this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromTTB && !this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromFile) && !this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_ExtEph_fromFile)
            {
                this.radioButton_EphSrc_None.Checked = true;
                this.checkBox_AutoSend_Eph.Checked = false;
            }
            else if (this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromTTB)
            {
                this.radioButton_EphSrc_TTB.Checked = true;
                this.checkBox_AutoSend_Eph.Checked = true;
            }
            else if (this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Eph_fromFile)
            {
                this.radioButton_EphSrc_File.Checked = true;
                this.checkBox_AutoSend_Eph.Checked = true;
                this.textBox_EphSrc_FilePath.Text = this.comm.AutoReplyCtrl.EphFilePath;
            }
            else if (this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_ExtEph_fromFile)
            {
                this.radioButton_EphSrc_ExtEphFile.Checked = true;
                this.checkBox_AutoSend_Eph.Checked = true;
                this.textBox_EphSrc_ExtEphFile.Text = this.comm.AutoReplyCtrl.EphFilePath;
            }
            else
            {
                this.radioButton_EphSrc_None.Checked = true;
                this.checkBox_AutoSend_Eph.Checked = false;
            }
            if (!this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_AcqData_fromFile && !this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_AcqData_fromTTB)
            {
                this.radioButton_AcqAssistSrc_None.Checked = true;
                this.checkBox_AutoSend_AcqAssist.Checked = false;
            }
            else if (this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_AcqData_fromFile)
            {
                this.radioButton_AcqAssistSrc_File.Checked = true;
                this.checkBox_AutoSend_AcqAssist.Checked = true;
                this.textBox_AcqAssistSrc_File.Text = this.comm.AutoReplyCtrl.AcqDataFilePath.ToString();
            }
            else if (this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_AcqData_fromTTB)
            {
                this.radioButton_AcqAssistSrc_TTB.Checked = true;
                this.checkBox_AutoSend_AcqAssist.Checked = true;
            }
            else
            {
                this.radioButton_AcqAssistSrc_None.Checked = true;
                this.checkBox_AutoSend_AcqAssist.Checked = false;
            }
            if (this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_Alm)
            {
                this.radioButton_AlmanacSrc_TTB.Checked = true;
                this.checkBox_AutoSend_Almanac.Checked = true;
            }
            else
            {
                this.radioButton_AlmanacSrc_None.Checked = true;
                this.checkBox_AutoSend_Almanac.Checked = false;
            }
            if (this.comm.AutoReplyCtrl.AutoReplyParams.AutoAid_NavBit)
            {
                this.radioButton_NavBitSrc_TTB.Checked = true;
                this.checkBox_AutoSend_NavBit.Checked = true;
            }
            else
            {
                this.radioButton_NavBitSrc_None.Checked = true;
                this.checkBox_AutoSend_NavBit.Checked = false;
            }
            this.setTTBTimeAiding();
        }

        private void setRefClkInfo(bool enable)
        {
            this.comboBox_ClkSrc_B1.Enabled = enable;
            this.comboBox_RefClkOnOff_B2.Enabled = enable;
            this.comboBox_RefClkReq_B3.Enabled = enable;
            this.comboBox_IncNormFreq_B4.Enabled = enable;
            this.comboBox_NormFreq_Freq.Enabled = enable;
            this.comboBox_SkewPPM.Enabled = enable;
            if (this.comboBox_IncNormFreq_B4.Enabled && (this.comboBox_IncNormFreq_B4.Text == "No"))
            {
                this.comboBox_NormFreq_Freq.Enabled = false;
                this.comboBox_SkewPPM.Enabled = false;
            }
            this.comboBox_TimeTagValid.Enabled = enable;
        }

        private void setTimeAidingGUIStatus()
        {
            if (this.radioButton_useTTBTimeAiding.Checked)
            {
                this.checkBox_AutoReply_TimeTrans.Enabled = false;
                this.checkBox_FwdToCP_TimeTrans.Enabled = false;
                this.checkBox_ScreenCPResp_TimeTrans.Enabled = false;
                this.textBox_Accuracy_TimeTrans.Enabled = false;
                this.textBox_Skew_TimeTrans.Enabled = false;
                this.textBox_UtcOffset.Enabled = false;
                this.button_SET_TimeTrans.Enabled = false;
                this.TTBTimeAidingSetBtn.Enabled = true;
                this.button1_CancelTTBTimeAidingCfg.Enabled = true;
                this.TTBTimeAidingEnableComboBox.Enabled = true;
                this.TTBTimeAidingTypeComboBox.Enabled = true;
                this.TTBTimeAidingAccTxtBox.Enabled = true;
                this.TTBTimeAidingSkewTxtBox.Enabled = true;
            }
            else
            {
                this.TTBTimeAidingSetBtn.Enabled = false;
                this.button1_CancelTTBTimeAidingCfg.Enabled = false;
                this.TTBTimeAidingEnableComboBox.Enabled = false;
                this.TTBTimeAidingTypeComboBox.Enabled = false;
                this.TTBTimeAidingAccTxtBox.Enabled = false;
                this.TTBTimeAidingSkewTxtBox.Enabled = false;
                this.checkBox_AutoReply_TimeTrans.Enabled = true;
                this.checkBox_FwdToCP_TimeTrans.Enabled = true;
                this.checkBox_ScreenCPResp_TimeTrans.Enabled = true;
                if (this.radioButton_useDOSTimeForCoarseTimeAiding.Checked)
                {
                    this.textBox_Accuracy_TimeTrans.Enabled = true;
                    this.textBox_Skew_TimeTrans.Enabled = true;
                    this.textBox_UtcOffset.Enabled = true;
                }
                else
                {
                    this.textBox_Accuracy_TimeTrans.Enabled = false;
                    this.textBox_Skew_TimeTrans.Enabled = false;
                    this.textBox_UtcOffset.Enabled = false;
                }
                this.button_SET_TimeTrans.Enabled = true;
            }
        }

        private void setTTBTimeAiding()
        {
            if (this.comm.AutoReplyCtrl.TTBTimeAidingParams.Enable)
            {
                this.TTBTimeAidingEnableComboBox.SelectedIndex = 1;
            }
            else
            {
                this.TTBTimeAidingEnableComboBox.SelectedIndex = 0;
            }
            this.TTBTimeAidingTypeComboBox.SelectedIndex = this.comm.AutoReplyCtrl.TTBTimeAidingParams.Type;
            this.TTBTimeAidingAccTxtBox.Text = (this.comm.AutoReplyCtrl.TTBTimeAidingParams.Accuracy / 0x3e8).ToString();
            string str = string.Empty;
            if (this.comm.AutoReplyCtrl.TTBTimeAidingParams.Type == 1)
            {
                str = (((double) this.comm.AutoReplyCtrl.TTBTimeAidingParams.Skew) / 1000.0).ToString();
                this.TTBTimeAidingSkewUnitLabel.Text = "ms";
                this.TTBTimeAidingAccUnitLabel.Text = "sec";
            }
            else
            {
                str = this.comm.AutoReplyCtrl.TTBTimeAidingParams.Skew.ToString();
                this.TTBTimeAidingSkewUnitLabel.Text = "ns";
                this.TTBTimeAidingAccUnitLabel.Text = "us";
            }
            this.TTBTimeAidingSkewTxtBox.Text = str;
        }

        private string setupFreqRespMsgs()
        {
            double num;
            uint maxValue;
            short lastClockDrift;
            byte num2 = 0;
            short num3 = 0;
            if (this.comboBox_ClkSrc_B1.SelectedIndex != this.comm.AutoReplyCtrl.HWCfgCtrl.FreqAidMethod)
            {
                MessageBox.Show("Freq aiding method doesn't match one set in hardware configure autoreply", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            double num5 = Convert.ToDouble(this.comboBox_SkewPPM.Text);
            long num6 = Convert.ToInt64(this.comboBox_NormFreq_Freq.Text);
            bool flag = true;
            if (this.comboBox_IncNormFreq_B4.Text == "No")
            {
                flag = false;
            }
            int num7 = 0;
            if (this.radioButton_Reject_Freq.Checked)
            {
                num7 = 2;
            }
            byte num9 = 0;
            byte num10 = 0;
            if (this.checkBox_ignoreXO.Checked)
            {
                num10 = 1;
            }
            num2 = (byte) (num10 << 7);
            if (this.radioButton_2Hz.Checked)
            {
                num9 = 1;
                num2 = (byte) (num2 | ((byte) ((num9 & 1) << 4)));
            }
            else if (this.radioButton_4Hz.Checked)
            {
                num9 = 2;
                num2 = (byte) (num2 | ((byte) ((num9 & 2) << 4)));
            }
            num2 = (byte) (num2 | ((byte) (Convert.ToByte(this.comboBox_IncNormFreq_B4.SelectedIndex) << 3)));
            num2 = (byte) (num2 | ((byte) (Convert.ToByte(this.comboBox_RefClkReq_B3.SelectedIndex) << 2)));
            num2 = (byte) (num2 | ((byte) (Convert.ToByte(this.comboBox_RefClkOnOff_B2.SelectedIndex) << 1)));
            num2 = (byte) (num2 | Convert.ToByte(this.comboBox_ClkSrc_B1.SelectedIndex));
            int num11 = Convert.ToInt32(this.comboBox_RFDefaultFreq.Text);
            int num12 = 0;
            int num13 = 0;
            try
            {
                num12 = Convert.ToInt32(this.textBox_Freq.Text);
                num13 = Convert.ToInt32(this.textBox_FreqAuto.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Specify Frequency Parameters: " + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            double num14 = 0.0;
            double num15 = 0.0;
            double num16 = 0.0;
            try
            {
                num14 = Convert.ToDouble(this.textBox_Accuracy.Text);
                num15 = Convert.ToDouble(this.textBox_AccuracyAuto.Text);
            }
            catch (Exception exception2)
            {
                MessageBox.Show("Use Receiver Reported Frequency Offset: " + exception2.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqOffsetFromRxGui = num13;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqAccFromRxGui = num15;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqOffsetUserSpecifiedGui = num12;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqAccUserSpecifiedGui = num14;
            if (this.radioButton_SpecifyFreqParam.Checked)
            {
                if (this.comboBox_ClkSrc_B1.SelectedIndex == 1)
                {
                    lastClockDrift = (byte) (num11 - num12);
                }
                else
                {
                    lastClockDrift = (short) num12;
                }
                num = num14;
                num16 = num12;
            }
            else if (this.comm.LastClockDrift == 0)
            {
                if (this.comboBox_ClkSrc_B1.SelectedIndex == 1)
                {
                    lastClockDrift = (short) (num11 - num13);
                }
                else
                {
                    lastClockDrift = (short) num13;
                }
                num = num15;
                num16 = num13;
            }
            else
            {
                if (this.comboBox_ClkSrc_B1.SelectedIndex == 1)
                {
                    lastClockDrift = (short) (num11 - this.comm.LastClockDrift);
                }
                else
                {
                    lastClockDrift = (short) this.comm.LastClockDrift;
                }
                num = num15;
                num16 = num13;
            }
            num3 = lastClockDrift;
            if (this.comboBox_TimeTagValid.Text == "invalid")
            {
                maxValue = uint.MaxValue;
            }
            else
            {
                maxValue = 0xfffffffe;
            }
            this.comm.AutoReplyCtrl.FreqTransferCtrl.Reply = this.checkBox_AutoReply_Freq.Checked;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.UseFreqAiding = num7;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.TimeTag = maxValue;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.RefClkInfo = num2;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.Accuracy = num;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.ScaledFreqOffset = num3;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.ExtClkSkewppm = num5;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.NomFreq = num6;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.IncludeNormFreq = flag;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqOffset = num16;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqAidingMethod = this.comboBox_ClkSrc_B1.SelectedIndex;
            string str3 = this.comm.AutoReplyCtrl.AutoReplyFreqTransferResp();
            this.comm.AutoReplyCtrl.FreqTransferCtrl.DefaultFreqIndex = this.comboBox_RFDefaultFreq.SelectedIndex;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.SpecifiedRefFreq = this.radioButton_SpecifyFreqParam.Checked;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.SLCReportFreqGuiIndex = this.radioButton_UseRxRptFreq.Checked;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.RefClockOnOffGuiIndex = this.comboBox_RefClkOnOff_B2.SelectedIndex;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.RefClockRequestGuiIndex = this.comboBox_RefClkReq_B3.SelectedIndex;
            this.comm.AutoReplyCtrl.FreqTransferCtrl.ExtRefClockGuiIndex = this.comboBox_NormFreq_Freq.SelectedIndex;
            if (this.radioButton_1Hz.Checked)
            {
                this.comm.AutoReplyCtrl.FreqTransferCtrl.ScaledFreqOffsetGuiIndex = 1;
                return str3;
            }
            if (this.radioButton_2Hz.Checked)
            {
                this.comm.AutoReplyCtrl.FreqTransferCtrl.ScaledFreqOffsetGuiIndex = 2;
                return str3;
            }
            if (this.radioButton_4Hz.Checked)
            {
                this.comm.AutoReplyCtrl.FreqTransferCtrl.ScaledFreqOffsetGuiIndex = 3;
                return str3;
            }
            this.comm.AutoReplyCtrl.FreqTransferCtrl.ScaledFreqOffsetGuiIndex = 0;
            return str3;
        }

        private string setupPosRequesMsg()
        {
            try
            {
                this.comm.AutoReplyCtrl.PositionRequestCtrl.NumFixes = (byte) this.numericUpDown_NumOfFixes.Value;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.TimeBtwFixes = (byte) this.numericUpDown_TimeBetweenFixes.Value;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.HorrErrMax = (byte) this.numericUpDown_HorrErrMax.Value;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.VertErrMax = (byte) this.numericUpDown_VertErrMax.Value;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.RespTimeMax = (byte) this.numericUpDown_ResponseTimeMax.Value;
                this.comm.AutoReplyCtrl.PositionRequestCtrl.TimeAccPriority = Convert.ToByte(this.comboBox_Priority.SelectedIndex);
                this.comm.AutoReplyCtrl.PositionRequestCtrl.LocMethod = Convert.ToByte(this.comboBox_PositionMethod.SelectedIndex);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return this.comm.AutoReplyCtrl.AutoSendPositionRequestMsg();
        }

        private void subframe123ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            byte networkEnhanceType = this.comm.AutoReplyCtrl.HWCfgCtrl.NetworkEnhanceType;
            if (this.subframe123ChkBox.Checked)
            {
                networkEnhanceType = (byte) (networkEnhanceType | 8);
            }
            else
            {
                networkEnhanceType = (byte) (networkEnhanceType & 0xf7);
            }
            this.textBox_NWEnhanceType.Text = networkEnhanceType.ToString();
        }

        private void subframe45ChkBox_CheckedChanged(object sender, EventArgs e)
        {
            byte networkEnhanceType = this.comm.AutoReplyCtrl.HWCfgCtrl.NetworkEnhanceType;
            if (this.subframe45ChkBox.Checked)
            {
                networkEnhanceType = (byte) (networkEnhanceType | 0x10);
            }
            else
            {
                networkEnhanceType = (byte) (networkEnhanceType & 0xef);
            }
            this.textBox_NWEnhanceType.Text = networkEnhanceType.ToString();
        }

        private void TimeTransGUIUpdate()
        {
            this.button_SET_TimeTrans.ForeColor = Color.Brown;
            this.comm.AutoReplyCtrl.TimeTransferCtrl.Reject = this.radioButton_Reject_TimeTrans.Checked;
            this.comm.AutoReplyCtrl.TimeTransferCtrl.Reply = this.checkBox_AutoReply_TimeTrans.Checked;
            this.comm.AutoReplyCtrl.AutoReplyParams.UseDOS_ForTimeAid = this.radioButton_useDOSTimeForCoarseTimeAiding.Checked;
            this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForTimeAid = this.radioButton_useTTBTimeAiding.Checked;
            if (this.radioButton_useDOSTimeForCoarseTimeAiding.Checked)
            {
                ushort num = 0;
                ulong num2 = 0L;
                double num3 = 0.0;
                byte num4 = 15;
                try
                {
                    num4 = Convert.ToByte(this.textBox_UtcOffset.Text);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("UTC offset: " + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                this.comm.RxCtrl.UTCOffset = num4;
                GPSDateTime time = this.comm.RxCtrl.GPSTimerEngine.GetTime();
                num = (ushort) (time.GetGPSWeek() + 0x400);
                try
                {
                    num3 = Convert.ToDouble(this.textBox_Skew_TimeTrans.Text);
                    num2 = ((ulong) time.GetGPSTOW()) + ((ulong) (num3 * 100.0));
                }
                catch (Exception exception2)
                {
                    MessageBox.Show("Skew: " + exception2.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                this.comm.AutoReplyCtrl.TimeTransferCtrl.Reply = true;
                this.comm.AutoReplyCtrl.TimeTransferCtrl.WeekNum = num;
                this.comm.AutoReplyCtrl.TimeTransferCtrl.TimeOfWeek = num2;
                this.comm.AutoReplyCtrl.TimeTransferCtrl.Skew = num3;
                try
                {
                    this.comm.AutoReplyCtrl.TimeTransferCtrl.Accuracy = Convert.ToDouble(this.textBox_Accuracy_TimeTrans.Text);
                }
                catch (Exception exception3)
                {
                    MessageBox.Show("Accuracy: " + exception3.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                this.comm.AutoReplyCtrl.TimeTransferCtrl.TTType = 0;
                this.comm.AutoReplyCtrl.AutoReplyTimeTransferResp();
            }
            this.comm.AutoReplyCtrl.AutoReplyParams.AutoReplyTimeTrans = this.comm.AutoReplyCtrl.TimeTransferCtrl.Reply;
            this.comm.AutoReplyCtrl.UpdateAutoReplyStatus();
        }

        private void TTBTimeAidingSetBtn__Click(object sender, EventArgs e)
        {
            uint num = 0;
            this.comm.AutoReplyCtrl.AutoReplyParams.UseDOS_ForTimeAid = false;
            this.comm.AutoReplyCtrl.TimeTransferCtrl.Reject = false;
            if (this.TTBTimeAidingEnableComboBox.SelectedIndex == 1)
            {
                this.comm.AutoReplyCtrl.TTBTimeAidingParams.Enable = true;
                this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForTimeAid = true;
                this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForHwCfg = true;
                this.comm.AutoReplyCtrl.AutoReplyParams.AutoReplyTimeTrans = true;
                this.comm.AutoReplyCtrl.AutoReplyParams.AutoReply = true;
            }
            else
            {
                this.comm.AutoReplyCtrl.TTBTimeAidingParams.Enable = false;
                this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForTimeAid = false;
                this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForHwCfg = false;
                this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForFreqAid = false;
            }
            try
            {
                this.comm.AutoReplyCtrl.TTBTimeAidingParams.Accuracy = 0x3e8 * Convert.ToUInt32(this.TTBTimeAidingAccTxtBox.Text);
                num = Convert.ToUInt32(this.TTBTimeAidingSkewTxtBox.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            this.comm.AutoReplyCtrl.TTBTimeAidingParams.Type = (byte) this.TTBTimeAidingTypeComboBox.SelectedIndex;
            if (this.comm.AutoReplyCtrl.TTBTimeAidingParams.Type == 1)
            {
                this.comm.AutoReplyCtrl.TTBTimeAidingParams.Skew = 0x3e8 * num;
            }
            else
            {
                this.comm.AutoReplyCtrl.TTBTimeAidingParams.Skew = num;
            }
            this.TTBTimeAidingSetBtn.ForeColor = Color.Brown;
            if (this.comm.IsSourceDeviceOpen())
            {
                string tTBTimeAidingCfgMsg = this.comm.AutoReplyCtrl.GetTTBTimeAidingCfgMsg();
                if (this.comm.TTBPort.IsOpen)
                {
                    this.comm.WriteData_TTB(tTBTimeAidingCfgMsg);
                    this.comm.waitforMsgFromTTB(0xcc, 80);
                    this.comm.AutoReplyCtrl.AutoReplyParams.AutoReplyTimeTrans = this.comm.AutoReplyCtrl.TimeTransferCtrl.Reply;
                    this.comm.AutoReplyCtrl.UpdateAutoReplyStatus();
                }
                else
                {
                    MessageBox.Show("TTB port is not connected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void TTBTimeAidingTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updateUnitLabel();
        }

        private void updateAidingCtrlsOnAcqAssistSrcRadioChange()
        {
            if (this.radioButton_AcqAssistSrc_None.Checked)
            {
                this.button_Send_AcqAssist.Enabled = false;
                this.textBox_AcqAssistSrc_File.Enabled = false;
                this.button_Browse_AcqAssistSrc_File.Enabled = false;
                this.checkBox_AutoSend_AcqAssist.Enabled = false;
            }
            else if (this.radioButton_AcqAssistSrc_File.Checked)
            {
                this.button_Send_AcqAssist.Enabled = true;
                this.textBox_AcqAssistSrc_File.Enabled = true;
                this.button_Browse_AcqAssistSrc_File.Enabled = true;
                this.checkBox_AutoSend_AcqAssist.Enabled = true;
            }
            else if (this.radioButton_AcqAssistSrc_TTB.Checked)
            {
                this.button_Send_AcqAssist.Enabled = true;
                this.textBox_AcqAssistSrc_File.Enabled = false;
                this.button_Browse_AcqAssistSrc_File.Enabled = false;
                this.checkBox_AutoSend_AcqAssist.Enabled = true;
            }
        }

        private void updateAidingCtrlsOnEphSrcRadioChange()
        {
            if (this.radioButton_EphSrc_None.Checked)
            {
                this.button_Send_EphClkCorr.Enabled = false;
                this.textBox_EphSrc_FilePath.Enabled = false;
                this.button_Browse_EphSrc_File.Enabled = false;
                this.textBox_EphSrc_ExtEphFile.Enabled = false;
                this.button_Browse_EphSrc_ExtEphFile.Enabled = false;
                this.checkBox_AutoSend_Eph.Enabled = false;
            }
            else if (this.radioButton_EphSrc_File.Checked)
            {
                this.button_Send_EphClkCorr.Enabled = true;
                this.textBox_EphSrc_FilePath.Enabled = true;
                this.button_Browse_EphSrc_File.Enabled = true;
                this.textBox_EphSrc_ExtEphFile.Enabled = false;
                this.button_Browse_EphSrc_ExtEphFile.Enabled = false;
                this.checkBox_AutoSend_Eph.Enabled = true;
            }
            else if (this.radioButton_EphSrc_TTB.Checked)
            {
                this.button_Send_EphClkCorr.Enabled = true;
                this.textBox_EphSrc_FilePath.Enabled = false;
                this.button_Browse_EphSrc_File.Enabled = false;
                this.textBox_EphSrc_ExtEphFile.Enabled = false;
                this.button_Browse_EphSrc_ExtEphFile.Enabled = false;
                this.checkBox_AutoSend_Eph.Enabled = true;
            }
            else if (this.radioButton_EphSrc_ExtEphFile.Checked)
            {
                this.button_Send_EphClkCorr.Enabled = true;
                this.textBox_EphSrc_FilePath.Enabled = false;
                this.button_Browse_EphSrc_File.Enabled = false;
                this.textBox_EphSrc_ExtEphFile.Enabled = true;
                this.button_Browse_EphSrc_ExtEphFile.Enabled = true;
                this.checkBox_AutoSend_Eph.Enabled = true;
            }
        }

        private void updateAidingCtrlsOnNavBitSrcRadioChange()
        {
            if (this.radioButton_NavBitSrc_None.Checked)
            {
                this.checkBox_AutoSend_NavBit.Enabled = false;
                this.button_sendNow_NavBit.Enabled = false;
            }
            else if (this.radioButton_NavBitSrc_TTB.Checked)
            {
                this.checkBox_AutoSend_NavBit.Enabled = true;
                this.button_sendNow_NavBit.Enabled = true;
            }
        }

        private void updateConfigList(string updatedName)
        {
            this.comm.m_NavData.RefLocationName = updatedName;
            this.updateReferenceLocationComboBox();
        }

        private void updateCtrlsWhenClkSrcChange()
        {
            if (this.comboBox_ClkSrc_B1.SelectedIndex == 0)
            {
                this.comboBox_RefClkOnOff_B2.Enabled = true;
                this.comboBox_RefClkReq_B3.Enabled = true;
                this.comboBox_IncNormFreq_B4.Enabled = true;
                this.updateCtrlsWhenIncNormFreqChange();
            }
            else
            {
                this.comboBox_IncNormFreq_B4.SelectedIndex = 0;
                this.comboBox_RefClkOnOff_B2.Enabled = false;
                this.comboBox_RefClkReq_B3.Enabled = false;
                this.comboBox_IncNormFreq_B4.Enabled = false;
                this.comboBox_NormFreq_Freq.Enabled = false;
                this.comboBox_SkewPPM.Enabled = false;
            }
        }

        private void updateCtrlsWhenIncNormFreqChange()
        {
            if (this.comboBox_IncNormFreq_B4.Text == "Yes")
            {
                this.comboBox_NormFreq_Freq.Enabled = true;
                this.comboBox_SkewPPM.Enabled = true;
            }
            else
            {
                this.comboBox_NormFreq_Freq.Enabled = false;
                this.comboBox_SkewPPM.Enabled = false;
            }
        }

        private void updateDefautlReferenceLocationComboBox()
        {
            this._maxItemWidth = this._defaultWidth;
            if (this.frmAutoReplyRefLocationComboBox.Items.Count == 0)
            {
                ArrayList referenceLocationName = new ArrayList();
                referenceLocationName = this.comm.m_NavData.GetReferenceLocationName();
                for (int i = 0; i < referenceLocationName.Count; i++)
                {
                    this.frmAutoReplyRefLocationComboBox.Items.Add(referenceLocationName[i]);
                    if (this._maxItemWidth < (referenceLocationName[i].ToString().Length * 6))
                    {
                        this._maxItemWidth = referenceLocationName[i].ToString().Length * 6;
                    }
                }
                this.frmAutoReplyRefLocationComboBox.Items.Add("USER_DEFINED");
            }
            this.frmAutoReplyRefLocationComboBox.Text = this.comm.m_NavData.RefLocationName;
            this.updateReferencePositionTextBox();
        }

        private void updateFreqCtrlsWhenRadioChange()
        {
            this.checkBox_useTTBFreq.Enabled = true;
            if (this.radioButton_SpecifyFreqParam.Enabled && this.radioButton_SpecifyFreqParam.Checked)
            {
                this.textBox_Freq.Enabled = true;
                this.textBox_Accuracy.Enabled = true;
                this.textBox_FreqAuto.Enabled = false;
                this.textBox_AccuracyAuto.Enabled = false;
                this.setRefClkInfo(true);
                this.updateCtrlsWhenClkSrcChange();
                this.comboBox_FreqTransMethod.SelectedIndex = this.comboBox_ClkSrc_B1.SelectedIndex;
                this.updateRefClkNomFreqWhenFreqTransMethodChange();
            }
            else if (this.radioButton_UseRxRptFreq.Enabled && this.radioButton_UseRxRptFreq.Checked)
            {
                this.textBox_Freq.Enabled = false;
                this.textBox_Accuracy.Enabled = false;
                this.textBox_FreqAuto.Enabled = true;
                this.textBox_AccuracyAuto.Enabled = true;
                this.setRefClkInfo(true);
                this.updateCtrlsWhenClkSrcChange();
                this.comboBox_FreqTransMethod.SelectedIndex = this.comboBox_ClkSrc_B1.SelectedIndex;
                this.updateRefClkNomFreqWhenFreqTransMethodChange();
            }
            else if (this.radioButton_Reject_Freq.Enabled && this.radioButton_Reject_Freq.Checked)
            {
                this.textBox_Freq.Enabled = false;
                this.textBox_Accuracy.Enabled = false;
                this.textBox_FreqAuto.Enabled = false;
                this.textBox_AccuracyAuto.Enabled = false;
                this.setRefClkInfo(false);
                this.checkBox_useTTBFreq.Enabled = false;
            }
        }

        private void updateRefClkNomFreqWhenFreqTransMethodChange()
        {
            if ((this.comboBox_FreqTransMethod.SelectedIndex == 0) && (this.comboBox_FreqTransAvail.SelectedIndex == 1))
            {
                this.comboBox_RefClkStatus.Enabled = true;
                this.comboBox_NorminalFreq.Enabled = true;
            }
            else
            {
                this.comboBox_RefClkStatus.SelectedIndex = 1;
                this.comboBox_RefClkStatus.Enabled = false;
                this.comboBox_NorminalFreq.Enabled = false;
                this.comboBox_RefClkOnOff_B2.SelectedIndex = 1;
                this.checkBox_useTTBFreq.Checked = false;
            }
        }

        private void updateReferenceLocationComboBox()
        {
            if (!this.frmAutoReplyRefLocationComboBox.Items.Contains(this.comm.m_NavData.RefLocationName))
            {
                this.frmAutoReplyRefLocationComboBox.Items.Add(this.comm.m_NavData.RefLocationName);
                this.frmAutoReplyRefLocationComboBox.Text = this.comm.m_NavData.RefLocationName;
                this.textBox_Latitude.Enabled = true;
                this.textBox_Longitude.Enabled = true;
                this.textBox_Altitude.Enabled = true;
            }
            else
            {
                this.frmAutoReplyRefLocationComboBox.Text = this.comm.m_NavData.RefLocationName;
                this.textBox_Latitude.Enabled = false;
                this.textBox_Longitude.Enabled = false;
                this.textBox_Altitude.Enabled = false;
            }
            this.updateReferencePositionTextBox();
        }

        private void updateReferencePositionTextBox()
        {
            PositionInLatLonAlt referencePosition = this.comm.m_NavData.GetReferencePosition(this.comm.m_NavData.RefLocationName);
            this.textBox_Latitude.Text = referencePosition.latitude.ToString();
            this.textBox_Longitude.Text = referencePosition.longitude.ToString();
            this.textBox_Altitude.Text = referencePosition.altitude.ToString();
            this.frmAutoReplyRefLocationComboBox.Text = referencePosition.name;
            this.frmAutoReplyRefLocationComboBox.Width = this._defaultWidth;
            if (referencePosition.name == "Error")
            {
                this.textBox_Latitude.Enabled = true;
                this.textBox_Longitude.Enabled = true;
                this.textBox_Altitude.Enabled = true;
            }
        }

        private void updateUnitLabel()
        {
            if (this.TTBTimeAidingTypeComboBox.SelectedIndex == 0)
            {
                this.TTBTimeAidingAccUnitLabel.Text = "us";
                this.TTBTimeAidingSkewUnitLabel.Text = "ns";
            }
            else
            {
                this.TTBTimeAidingAccUnitLabel.Text = "sec";
                this.TTBTimeAidingSkewUnitLabel.Text = "ms";
            }
        }

        private void updatewhenFreqTransAvailchange()
        {
            if (this.comboBox_FreqTransAvail.SelectedIndex == 0)
            {
                this.comboBox_FreqTransMethod.Enabled = false;
                this.disableEnableFreqTransAutoReplyPage(false);
                this.comboBox_RefClkStatus.Enabled = false;
                this.comboBox_NorminalFreq.Enabled = false;
            }
            else
            {
                this.comboBox_FreqTransMethod.Enabled = true;
                this.disableEnableFreqTransAutoReplyPage(true);
                this.updateFreqCtrlsWhenRadioChange();
                this.updateCtrlsWhenClkSrcChange();
                this.updateRefClkNomFreqWhenFreqTransMethodChange();
                if (this.comboBox_FreqTransMethod.SelectedIndex == 0)
                {
                    this.comboBox_RefClkStatus.Enabled = true;
                    this.comboBox_NorminalFreq.Enabled = true;
                }
            }
        }

        private void writeAutoReplyData(string filepath)
        {
            this.Cursor = Cursors.WaitCursor;
            IniHelper helper = new IniHelper(filepath);
            string section = string.Empty;
            string key = string.Empty;
            section = "HW_CONFIG";
            key = "REPLY";
            if (this.comm.AutoReplyCtrl.HWCfgCtrl.Reply)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            key = "PRECISE_TIME_ENABLED";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.HWCfgCtrl.PreciseTimeEnabled.ToString());
            key = "PRECISE_TIME_DIRECTION";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.HWCfgCtrl.PreciseTimeDirection.ToString());
            key = "FREQ_AIDED_ENABLED";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.HWCfgCtrl.FreqAidEnabled.ToString());
            key = "FREQ_AIDED_ENABLED";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.HWCfgCtrl.FreqAidEnabled.ToString());
            key = "FREQ_AIDED_METHOD";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.HWCfgCtrl.FreqAidMethod.ToString());
            key = "RTC_AVAILABLE";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.HWCfgCtrl.RTCAvailabe.ToString());
            key = "RTC_SOURCE";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.HWCfgCtrl.RTCSource.ToString());
            key = "COARSE_TIME_ENABLE";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.HWCfgCtrl.CoarseTimeEnabled.ToString());
            key = "REF_CLOCK_ENABLED";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.HWCfgCtrl.RefClkEnabled.ToString());
            key = "NORMINAL_FREQ_HZ";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.HWCfgCtrl.NorminalFreqHz.ToString());
            key = "ENHANCED_NETWORK";
            if (this.comm.AutoReplyCtrl.HWCfgCtrl.PreciseTimeEnabled == 0)
            {
                helper.IniWriteValue(section, key, "0");
                this.comm.AutoReplyCtrl.HWCfgCtrl.NetworkEnhanceType = 0;
            }
            else
            {
                helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.HWCfgCtrl.NetworkEnhanceType.ToString());
            }
            section = "APPROXIMATE_POSITION";
            key = "REPLY";
            if (this.comm.AutoReplyCtrl.ApproxPositionCtrl.Reply)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            key = "REJECT";
            if (this.comm.AutoReplyCtrl.ApproxPositionCtrl.Reject)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            key = "LAT";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.ApproxPositionCtrl.Lat.ToString());
            key = "LON";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.ApproxPositionCtrl.Lon.ToString());
            key = "ALT";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.ApproxPositionCtrl.Alt.ToString());
            key = "EST_HOR_ERR";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.ApproxPositionCtrl.EstHorrErr.ToString());
            key = "EST_VER_ERR";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.ApproxPositionCtrl.EstVertiErr.ToString());
            key = "LAT_SKEW";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.ApproxPositionCtrl.DistanceSkew.ToString());
            key = "LON_SKEW";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.ApproxPositionCtrl.HeadingSkew.ToString());
            key = "LOC_NAME";
            helper.IniWriteValue(section, key, this.comm.m_NavData.RefLocationName);
            section = "TIME_AIDING";
            key = "REPLY";
            if (this.comm.AutoReplyCtrl.TimeTransferCtrl.Reply)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            key = "REJECT";
            if (this.comm.AutoReplyCtrl.TimeTransferCtrl.Reject)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            key = "TIME_AIDING_TYPE";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.TimeTransferCtrl.TTType.ToString());
            key = "TIME_ACC";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.TimeTransferCtrl.Accuracy.ToString());
            key = "SKEW";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.TimeTransferCtrl.Skew.ToString());
            key = "UTC_OFFSET";
            helper.IniWriteValue(section, key, this.comm.RxCtrl.UTCOffset.ToString());
            key = "TIME_AIDING_SOURCE";
            int num = 0;
            if (this.comm.AutoReplyCtrl.AutoReplyParams.UseDOS_ForTimeAid)
            {
                num = 1;
                this.comm.AutoReplyCtrl.TTBTimeAidingParams.Enable = false;
            }
            else if (this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForTimeAid)
            {
                num = 2;
                this.comm.AutoReplyCtrl.TTBTimeAidingParams.Enable = true;
            }
            else
            {
                num = 0;
                this.comm.AutoReplyCtrl.TTBTimeAidingParams.Enable = false;
            }
            helper.IniWriteValue(section, key, num.ToString());
            section = "TTB_TIME_AIDING";
            key = "ENABLE";
            if (this.comm.AutoReplyCtrl.TTBTimeAidingParams.Enable)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            key = "TYPE";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.TTBTimeAidingParams.Type.ToString());
            key = "TIME_ACC";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.TTBTimeAidingParams.Accuracy.ToString());
            key = "TIME_SKEW";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.TTBTimeAidingParams.Skew.ToString());
            section = "FREQ_AIDING";
            key = "REPLY";
            if (this.comm.AutoReplyCtrl.FreqTransferCtrl.Reply)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            key = "USE_FREQ_AIDING";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.UseFreqAiding.ToString());
            key = "TIME_TAG";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.TimeTag.ToString());
            key = "REF_CLOCK_INFO";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.RefClkInfo.ToString());
            key = "REL_FREQ_ACC";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.Accuracy.ToString());
            key = "SCALED_FREQ_OFFSET";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.ScaledFreqOffset.ToString());
            key = "EXT_CLOCK_SKEW";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.ExtClkSkewppm.ToString());
            key = "NORMIMAL_FREQ";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.NomFreq.ToString());
            key = "INCLUDE_NORM_FREQ";
            if (this.comm.AutoReplyCtrl.FreqTransferCtrl.IncludeNormFreq)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            key = "FREQ_METHOD";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqAidingMethod.ToString());
            key = "DEFAULT_FREQ_GUI_INDEX";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.DefaultFreqIndex.ToString());
            key = "SPECIFIED_FREQ_GUI_INDEX";
            if (this.comm.AutoReplyCtrl.FreqTransferCtrl.SpecifiedRefFreq)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            key = "SLC_REPORT_FREQ_GUI_INDEX";
            if (this.comm.AutoReplyCtrl.FreqTransferCtrl.SLCReportFreqGuiIndex)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            key = "REF_CLOCK_REQUEST_GUI_INDEX";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.RefClockRequestGuiIndex.ToString());
            key = "REF_CLOCK_ONOFF_GUI_INDEX";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.RefClockOnOffGuiIndex.ToString());
            key = "EXT_REF_CLOCK_GUI_INDEX";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.ExtRefClockGuiIndex.ToString());
            key = "SCALED_FREQ_OFFSET_GUI_INDEX";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.ScaledFreqOffsetGuiIndex.ToString());
            key = "FREQ_ACC_USER_GUI";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqAccUserSpecifiedGui.ToString());
            key = "FREQ_OFFSET_USER_GUI";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqOffsetUserSpecifiedGui.ToString());
            key = "FREQ_ACC_RX_GUI";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqAccFromRxGui.ToString());
            key = "FREQ_OFFSET_RX_GUI";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.FreqTransferCtrl.FreqOffsetFromRxGui.ToString());
            key = "USE_TTB_FREQ";
            if (this.comm.AutoReplyCtrl.AutoReplyParams.UseTTB_ForFreqAid)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            key = "IGNORE_XO";
            if (this.comm.AutoReplyCtrl.AutoReplyParams.FreqAidingIgnoreXO)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            section = "POSITION_AIDING";
            key = "REPLY";
            if (this.comm.AutoReplyCtrl.AutoReplyParams.AutoPosReq)
            {
                helper.IniWriteValue(section, key, "1");
            }
            else
            {
                helper.IniWriteValue(section, key, "0");
            }
            key = "NUM_FIXED";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.NumFixes.ToString());
            key = "TIME_BETWEEN_FIXES";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.TimeBtwFixes.ToString());
            key = "HOR_ERR_MAX";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.HorrErrMax.ToString());
            key = "VERT_ERR_MAX";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.VertErrMax.ToString());
            key = "RESP_TIME_MAX";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.RespTimeMax.ToString());
            key = "TIME_ACC_PRIORITY";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.TimeAccPriority.ToString());
            key = "LOC_METHOD";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.LocMethod.ToString());
            key = "EPH_SOURCE";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.EphSource.ToString());
            key = "EPH_FILEPATH";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.EphFilePath.ToString());
            key = "EPH_REPLY";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.EphReply.ToString());
            key = "ACQ_ASSIST_SOURCE";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.AcqAssistSource.ToString());
            key = "ACQ_ASSIST_REPLY";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.AcqAssistReply.ToString());
            key = "ACQ_ASSIST_FILEPATH";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.AcqDataFilePath.ToString());
            key = "ALM_SOURCE";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.AlmSource.ToString());
            key = "ALM_REPLY";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.AlmReply.ToString());
            key = "NAVBIT_SOURCE";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.NavBitSource.ToString());
            key = "NAVBIT_REPLY";
            helper.IniWriteValue(section, key, this.comm.AutoReplyCtrl.PositionRequestCtrl.NavBitReply.ToString());
            this.Cursor = Cursors.Default;
        }

        public string AutoReplyConfigFilePath
        {
            get
            {
                return this._autoReplyConfigFilePath;
            }
            set
            {
                this._autoReplyConfigFilePath = value;
            }
        }

        public CommunicationManager CommWindow
        {
            get
            {
                return this.comm;
            }
            set
            {
                this.comm = value;
            }
        }
    }
}


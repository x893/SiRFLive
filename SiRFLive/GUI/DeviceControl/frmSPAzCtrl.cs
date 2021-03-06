﻿namespace SiRFLive.GUI.DeviceControl
{
    using SiRFLive.DeviceControl;
    using SiRFLive.Utilities;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmSPAzCtrl : Form
    {
        private SPAzMgr _SPAzMgr;
        private byte atten;
        private Button btn_Exit;
        private Button btn_ReadAttenuation;
        private Button btn_ReadPower;
        private Button btn_SetAttenuation;
        private CheckBox chkBox_OnOffPower;
        private CheckBox chkBox_OnOffRF;
        private IContainer components;
        public ObjectInterface cpGuiCtrl;
        private Label label1;
        private Label label2;
        private static frmSPAzCtrl m_SChildform;
        private RadioButton rbtn_PowerStatus;
        private TextBox textBox_AttenuationToSet;
        private TextBox textBox_readAttenuation;

        public frmSPAzCtrl()
        {
            this.cpGuiCtrl = new ObjectInterface();
            this._SPAzMgr = new SPAzMgr(0x378);
            this.InitializeComponent();
        }

        public frmSPAzCtrl(int address)
        {
            this.cpGuiCtrl = new ObjectInterface();
            this._SPAzMgr = new SPAzMgr(address);
            this.InitializeComponent();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            base.Close();
            m_SChildform = null;
        }

        private void btn_ReadAttenuation_Click(object sender, EventArgs e)
        {
            string text = this._SPAzMgr.ReadSPAzAtten();
            if (!text.Contains("Exception"))
            {
                this.cpGuiCtrl.SetTextBoxText(this.textBox_readAttenuation, text);
            }
            else
            {
                MessageBox.Show(text, "ERROR!");
            }
        }

        private void btn_ReadPower_Click(object sender, EventArgs e)
        {
            string text = this._SPAzMgr.ReadSPAzPower();
            if (text == "ON")
            {
                this.cpGuiCtrl.SetRadioBoxChecked(this.rbtn_PowerStatus, true);
            }
            else if (text == "OFF")
            {
                this.cpGuiCtrl.SetRadioBoxChecked(this.rbtn_PowerStatus, false);
            }
            else
            {
                MessageBox.Show(text, "ERROR!");
            }
        }

        private void btn_SetAttenuation_Click(object sender, EventArgs e)
        {
            string text = this.textBox_AttenuationToSet.Text;
            string str2 = "";
            if (text == "")
            {
                MessageBox.Show(" No Attenuation value specified!");
            }
            else
            {
                try
                {
                    byte num = Convert.ToByte(text);
                    str2 = this._SPAzMgr.WriteSPAzAtten(num);
                    this.atten = num;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "ERROR!");
                }
                if (str2.Contains("Exception"))
                {
                    MessageBox.Show(str2, "ERROR!");
                }
            }
        }

        private void chkBox_OnOffPower_CheckedChanged(object sender, EventArgs e)
        {
            string text = "";
            if (this.chkBox_OnOffPower.Checked)
            {
                text = this._SPAzMgr.WriteSPAzPower(true);
            }
            else
            {
                text = this._SPAzMgr.WriteSPAzPower(false);
            }
            if (text.Contains("Exception"))
            {
                MessageBox.Show(text, "ERROR!");
            }
        }

        private void chkBox_OnOffRF_CheckedChanged(object sender, EventArgs e)
        {
            string text = "";
            if (this.chkBox_OnOffRF.Checked)
            {
                text = this._SPAzMgr.WriteSPAzAtten(this.atten);
            }
            else
            {
                text = this._SPAzMgr.WriteSPAzAtten(0x7f);
            }
            if (text.Contains("Exception"))
            {
                MessageBox.Show(text, "ERROR!");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSPAzCtrl_Load(object sender, EventArgs e)
        {
            this.textBox_readAttenuation.Enabled = false;
            this.chkBox_OnOffRF.Checked = true;
            this.chkBox_OnOffPower.Checked = true;
        }

        public static frmSPAzCtrl GetChildInstance()
        {
            if (m_SChildform == null)
            {
                m_SChildform = new frmSPAzCtrl();
            }
            return m_SChildform;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmSPAzCtrl));
            this.btn_ReadPower = new Button();
            this.btn_SetAttenuation = new Button();
            this.btn_ReadAttenuation = new Button();
            this.rbtn_PowerStatus = new RadioButton();
            this.textBox_AttenuationToSet = new TextBox();
            this.label1 = new Label();
            this.textBox_readAttenuation = new TextBox();
            this.label2 = new Label();
            this.btn_Exit = new Button();
            this.chkBox_OnOffPower = new CheckBox();
            this.chkBox_OnOffRF = new CheckBox();
            base.SuspendLayout();
            this.btn_ReadPower.Location = new Point(0x1d, 0x3d);
            this.btn_ReadPower.Name = "btn_ReadPower";
            this.btn_ReadPower.Size = new Size(0x77, 0x18);
            this.btn_ReadPower.TabIndex = 1;
            this.btn_ReadPower.Text = "Read Power";
            this.btn_ReadPower.UseVisualStyleBackColor = true;
            this.btn_ReadPower.Click += new EventHandler(this.btn_ReadPower_Click);
            this.btn_SetAttenuation.Location = new Point(0x1d, 0x69);
            this.btn_SetAttenuation.Name = "btn_SetAttenuation";
            this.btn_SetAttenuation.Size = new Size(0x77, 0x18);
            this.btn_SetAttenuation.TabIndex = 3;
            this.btn_SetAttenuation.Text = "Set Attenuation";
            this.btn_SetAttenuation.UseVisualStyleBackColor = true;
            this.btn_SetAttenuation.Click += new EventHandler(this.btn_SetAttenuation_Click);
            this.btn_ReadAttenuation.Location = new Point(0x1d, 0x95);
            this.btn_ReadAttenuation.Name = "btn_ReadAttenuation";
            this.btn_ReadAttenuation.Size = new Size(0x77, 0x18);
            this.btn_ReadAttenuation.TabIndex = 6;
            this.btn_ReadAttenuation.Text = "Read Attenuation";
            this.btn_ReadAttenuation.UseVisualStyleBackColor = true;
            this.btn_ReadAttenuation.Click += new EventHandler(this.btn_ReadAttenuation_Click);
            this.rbtn_PowerStatus.AutoSize = true;
            this.rbtn_PowerStatus.Location = new Point(0x102, 0x41);
            this.rbtn_PowerStatus.Name = "rbtn_PowerStatus";
            this.rbtn_PowerStatus.Size = new Size(0x58, 0x11);
            this.rbtn_PowerStatus.TabIndex = 2;
            this.rbtn_PowerStatus.TabStop = true;
            this.rbtn_PowerStatus.Text = "Power Status";
            this.rbtn_PowerStatus.UseVisualStyleBackColor = true;
            this.textBox_AttenuationToSet.Location = new Point(0x102, 0x6b);
            this.textBox_AttenuationToSet.Name = "textBox_AttenuationToSet";
            this.textBox_AttenuationToSet.Size = new Size(0x77, 20);
            this.textBox_AttenuationToSet.TabIndex = 5;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0xa1, 0x6f);
            this.label1.Name = "label1";
            this.label1.Size = new Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Set Attenuation";
            this.textBox_readAttenuation.Location = new Point(0x102, 0x97);
            this.textBox_readAttenuation.Name = "textBox_readAttenuation";
            this.textBox_readAttenuation.Size = new Size(0x77, 20);
            this.textBox_readAttenuation.TabIndex = 8;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xa1, 0x9b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(90, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Read Attenuation";
            this.btn_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Exit.Location = new Point(0xa6, 0xce);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new Size(0x4b, 0x17);
            this.btn_Exit.TabIndex = 9;
            this.btn_Exit.Text = "E&xit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new EventHandler(this.btn_Exit_Click);
            this.chkBox_OnOffPower.AutoSize = true;
            this.chkBox_OnOffPower.Location = new Point(0x1d, 0x18);
            this.chkBox_OnOffPower.Name = "chkBox_OnOffPower";
            this.chkBox_OnOffPower.Size = new Size(0x5c, 0x11);
            this.chkBox_OnOffPower.TabIndex = 0;
            this.chkBox_OnOffPower.Text = "Power On/Off";
            this.chkBox_OnOffPower.UseVisualStyleBackColor = true;
            this.chkBox_OnOffPower.CheckedChanged += new EventHandler(this.chkBox_OnOffPower_CheckedChanged);
            this.chkBox_OnOffRF.AutoSize = true;
            this.chkBox_OnOffRF.Location = new Point(0xa4, 0x18);
            this.chkBox_OnOffRF.Name = "chkBox_OnOffRF";
            this.chkBox_OnOffRF.Size = new Size(0x4c, 0x11);
            this.chkBox_OnOffRF.TabIndex = 0;
            this.chkBox_OnOffRF.Text = "RF On/Off";
            this.chkBox_OnOffRF.UseVisualStyleBackColor = true;
            this.chkBox_OnOffRF.CheckedChanged += new EventHandler(this.chkBox_OnOffRF_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            base.CancelButton = this.btn_Exit;
            base.ClientSize = new Size(0x197, 0xfc);
            base.Controls.Add(this.chkBox_OnOffRF);
            base.Controls.Add(this.chkBox_OnOffPower);
            base.Controls.Add(this.btn_Exit);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textBox_readAttenuation);
            base.Controls.Add(this.textBox_AttenuationToSet);
            base.Controls.Add(this.rbtn_PowerStatus);
            base.Controls.Add(this.btn_ReadAttenuation);
            base.Controls.Add(this.btn_SetAttenuation);
            base.Controls.Add(this.btn_ReadPower);
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.Name = "frmSPAzCtrl";
            base.ShowInTaskbar = false;
            base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SPAz Control";
            base.Load += new EventHandler(this.frmSPAzCtrl_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void OnClosed(EventArgs e)
        {
            m_SChildform = null;
        }
    }
}


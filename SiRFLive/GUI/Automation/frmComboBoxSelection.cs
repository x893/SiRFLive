﻿namespace SiRFLive.GUI.Automation
{
    using SiRFLive.General;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class frmComboBoxSelection : Form
    {
        private IContainer components;
        private ComboBox frmComboBoxSelectionCB;
        private Label frmComboBoxSelectionLabel;
        private Button frmComboBoxSelectionSaveBtn;
        private Button frmComboxSelectionExitBtn;
        private static frmComboBoxSelection m_SChildform;

        internal event updateParentEventHandler updateParent;

        internal frmComboBoxSelection(string source)
        {
            this.InitializeComponent();
            switch (source)
            {
                case "SIGNAL_SOURCE":
                    this.frmComboBoxSelectionCB.Items.Clear();
                    this.frmComboBoxSelectionCB.Items.Add(clsGlobal.RF_PLAYBACK);
                    this.frmComboBoxSelectionCB.Items.Add(clsGlobal.SIM);
                    this.frmComboBoxSelectionCB.Items.Add(clsGlobal.LIVE);
                    this.frmComboBoxSelectionCB.SelectedIndex = 0;
                    return;

                case "ATTEN_SOURCE":
                case "POWER_SOURCE":
                    this.frmComboBoxSelectionCB.Items.Clear();
                    this.frmComboBoxSelectionCB.Items.Add(clsGlobal.SPAZ);
                    this.frmComboBoxSelectionCB.Items.Add(clsGlobal.TESTRACK);
                    this.frmComboBoxSelectionCB.Items.Add(clsGlobal.SIM);
                    this.frmComboBoxSelectionCB.Items.Add(clsGlobal.MANUAL);
                    this.frmComboBoxSelectionCB.SelectedIndex = 0;
                    return;
            }
            this.frmComboBoxSelectionCB.Items.Clear();
            this.frmComboBoxSelectionCB.Items.Add(clsGlobal.RF_PLAYBACK);
            this.frmComboBoxSelectionCB.Items.Add(clsGlobal.SIM);
            this.frmComboBoxSelectionCB.Items.Add(clsGlobal.LIVE);
            this.frmComboBoxSelectionCB.SelectedIndex = 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmComboBoxSelectionSaveBtn_Click(object sender, EventArgs e)
        {
            if (this.updateParent != null)
            {
                this.updateParent(this.frmComboBoxSelectionCB.SelectedItem.ToString());
            }
            m_SChildform = null;
            base.Close();
        }

        private void frmComboxSelectionExitBtn_Click(object sender, EventArgs e)
        {
            m_SChildform = null;
            base.Close();
        }

        internal static frmComboBoxSelection GetChildInstance(string source)
        {
            if (m_SChildform == null)
            {
                m_SChildform = new frmComboBoxSelection(source);
            }
            return m_SChildform;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmComboBoxSelection));
            this.frmComboBoxSelectionCB = new ComboBox();
            this.frmComboBoxSelectionLabel = new Label();
            this.frmComboBoxSelectionSaveBtn = new Button();
            this.frmComboxSelectionExitBtn = new Button();
            base.SuspendLayout();
            this.frmComboBoxSelectionCB.FormattingEnabled = true;
            this.frmComboBoxSelectionCB.Items.AddRange(new object[] { "RF_PLAYBACK", "SIM", "LIVE" });
            this.frmComboBoxSelectionCB.Location = new Point(0x71, 0x23);
            this.frmComboBoxSelectionCB.Name = "frmComboBoxSelectionCB";
            this.frmComboBoxSelectionCB.Size = new Size(0x79, 0x15);
            this.frmComboBoxSelectionCB.TabIndex = 0;
            this.frmComboBoxSelectionLabel.AutoSize = true;
            this.frmComboBoxSelectionLabel.Location = new Point(0x36, 0x26);
            this.frmComboBoxSelectionLabel.Name = "frmComboBoxSelectionLabel";
            this.frmComboBoxSelectionLabel.Size = new Size(0x29, 13);
            this.frmComboBoxSelectionLabel.TabIndex = 0;
            this.frmComboBoxSelectionLabel.Text = "Source";
            this.frmComboBoxSelectionSaveBtn.Location = new Point(0x39, 0x5e);
            this.frmComboBoxSelectionSaveBtn.Name = "frmComboBoxSelectionSaveBtn";
            this.frmComboBoxSelectionSaveBtn.Size = new Size(0x4b, 0x17);
            this.frmComboBoxSelectionSaveBtn.TabIndex = 1;
            this.frmComboBoxSelectionSaveBtn.Text = "&Save";
            this.frmComboBoxSelectionSaveBtn.UseVisualStyleBackColor = true;
            this.frmComboBoxSelectionSaveBtn.Click += new EventHandler(this.frmComboBoxSelectionSaveBtn_Click);
            this.frmComboxSelectionExitBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.frmComboxSelectionExitBtn.Location = new Point(0x9f, 0x5e);
            this.frmComboxSelectionExitBtn.Name = "frmComboxSelectionExitBtn";
            this.frmComboxSelectionExitBtn.Size = new Size(0x4b, 0x17);
            this.frmComboxSelectionExitBtn.TabIndex = 2;
            this.frmComboxSelectionExitBtn.Text = "&Cancel";
            this.frmComboxSelectionExitBtn.UseVisualStyleBackColor = true;
            this.frmComboxSelectionExitBtn.Click += new EventHandler(this.frmComboxSelectionExitBtn_Click);
            base.AcceptButton = this.frmComboBoxSelectionSaveBtn;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.CancelButton = this.frmComboxSelectionExitBtn;
            base.ClientSize = new Size(0x124, 0x8d);
            base.Controls.Add(this.frmComboxSelectionExitBtn);
            base.Controls.Add(this.frmComboBoxSelectionSaveBtn);
            base.Controls.Add(this.frmComboBoxSelectionLabel);
            base.Controls.Add(this.frmComboBoxSelectionCB);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "frmComboBoxSelection";
            base.ShowInTaskbar = false;
            base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Source Selection";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected override void OnClosed(EventArgs e)
        {
            m_SChildform = null;
        }

        internal delegate void updateParentEventHandler(string updatedData);
    }
}


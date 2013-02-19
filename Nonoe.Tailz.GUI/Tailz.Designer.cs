namespace Nonoe.Tailz.GUI
{
    partial class Tailz
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.browseForTailfileButton = new System.Windows.Forms.Button();
            this.tailFilenameTextbox = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.grdTails = new System.Windows.Forms.DataGridView();
            this.btnAddWatcher = new System.Windows.Forms.Button();
            this.chkApplicationEventLog = new System.Windows.Forms.CheckBox();
            this.chkSystemEventLog = new System.Windows.Forms.CheckBox();
            this.chkSecurityEventLog = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCreatePlugin = new System.Windows.Forms.Button();
            this.btnMakeInactive = new System.Windows.Forms.Button();
            this.btnMakeActive = new System.Windows.Forms.Button();
            this.grdInactivePlugins = new System.Windows.Forms.DataGridView();
            this.grdActivePlugins = new System.Windows.Forms.DataGridView();
            this.grdLogs = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdTails)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdInactivePlugins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdActivePlugins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // browseForTailfileButton
            // 
            this.browseForTailfileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseForTailfileButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.browseForTailfileButton.Location = new System.Drawing.Point(215, 19);
            this.browseForTailfileButton.Name = "browseForTailfileButton";
            this.browseForTailfileButton.Size = new System.Drawing.Size(72, 23);
            this.browseForTailfileButton.TabIndex = 10;
            this.browseForTailfileButton.Text = "Browse ...";
            this.browseForTailfileButton.Click += new System.EventHandler(this.browseForTailfileButton_Click);
            // 
            // tailFilenameTextbox
            // 
            this.tailFilenameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tailFilenameTextbox.Location = new System.Drawing.Point(6, 22);
            this.tailFilenameTextbox.Name = "tailFilenameTextbox";
            this.tailFilenameTextbox.Size = new System.Drawing.Size(203, 20);
            this.tailFilenameTextbox.TabIndex = 9;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClear.Location = new System.Drawing.Point(6, 44);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(56, 23);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStart.Location = new System.Drawing.Point(6, 15);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(56, 23);
            this.btnStart.TabIndex = 15;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.startTailButton_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStop.Location = new System.Drawing.Point(68, 15);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(56, 23);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.stopTailButton_Click);
            // 
            // grdTails
            // 
            this.grdTails.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.grdTails.AllowUserToAddRows = false;
            this.grdTails.AllowUserToResizeRows = false;
            this.grdTails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdTails.CausesValidation = false;
            this.grdTails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTails.Location = new System.Drawing.Point(6, 54);
            this.grdTails.MultiSelect = false;
            this.grdTails.Name = "grdTails";
            this.grdTails.ReadOnly = true;
            this.grdTails.RowHeadersVisible = false;
            this.grdTails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTails.ShowCellErrors = false;
            this.grdTails.ShowCellToolTips = false;
            this.grdTails.ShowEditingIcon = false;
            this.grdTails.ShowRowErrors = false;
            this.grdTails.Size = new System.Drawing.Size(359, 113);
            this.grdTails.TabIndex = 19;
            this.grdTails.TabStop = false;
            // 
            // btnAddWatcher
            // 
            this.btnAddWatcher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddWatcher.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAddWatcher.Location = new System.Drawing.Point(293, 19);
            this.btnAddWatcher.Name = "btnAddWatcher";
            this.btnAddWatcher.Size = new System.Drawing.Size(72, 23);
            this.btnAddWatcher.TabIndex = 20;
            this.btnAddWatcher.Text = "Add";
            this.btnAddWatcher.Click += new System.EventHandler(this.btnAddWatcher_Click);
            // 
            // chkApplicationEventLog
            // 
            this.chkApplicationEventLog.AutoSize = true;
            this.chkApplicationEventLog.Location = new System.Drawing.Point(6, 19);
            this.chkApplicationEventLog.Name = "chkApplicationEventLog";
            this.chkApplicationEventLog.Size = new System.Drawing.Size(78, 17);
            this.chkApplicationEventLog.TabIndex = 23;
            this.chkApplicationEventLog.Text = "Application";
            this.chkApplicationEventLog.UseVisualStyleBackColor = true;
            // 
            // chkSystemEventLog
            // 
            this.chkSystemEventLog.AutoSize = true;
            this.chkSystemEventLog.Location = new System.Drawing.Point(6, 65);
            this.chkSystemEventLog.Name = "chkSystemEventLog";
            this.chkSystemEventLog.Size = new System.Drawing.Size(60, 17);
            this.chkSystemEventLog.TabIndex = 24;
            this.chkSystemEventLog.Text = "System";
            this.chkSystemEventLog.UseVisualStyleBackColor = true;
            // 
            // chkSecurityEventLog
            // 
            this.chkSecurityEventLog.AutoSize = true;
            this.chkSecurityEventLog.Location = new System.Drawing.Point(6, 42);
            this.chkSecurityEventLog.Name = "chkSecurityEventLog";
            this.chkSecurityEventLog.Size = new System.Drawing.Size(64, 17);
            this.chkSecurityEventLog.TabIndex = 25;
            this.chkSecurityEventLog.Text = "Security";
            this.chkSecurityEventLog.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkApplicationEventLog);
            this.groupBox1.Controls.Add(this.chkSystemEventLog);
            this.groupBox1.Controls.Add(this.chkSecurityEventLog);
            this.groupBox1.Location = new System.Drawing.Point(569, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(107, 173);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EventLogs";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdTails);
            this.groupBox2.Controls.Add(this.tailFilenameTextbox);
            this.groupBox2.Controls.Add(this.btnAddWatcher);
            this.groupBox2.Controls.Add(this.browseForTailfileButton);
            this.groupBox2.Location = new System.Drawing.Point(186, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 173);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Files";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnStop);
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Controls.Add(this.btnClear);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 173);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Controls";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(946, 194);
            this.panel2.TabIndex = 31;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnCreatePlugin);
            this.groupBox4.Controls.Add(this.btnMakeInactive);
            this.groupBox4.Controls.Add(this.btnMakeActive);
            this.groupBox4.Controls.Add(this.grdInactivePlugins);
            this.groupBox4.Controls.Add(this.grdActivePlugins);
            this.groupBox4.Location = new System.Drawing.Point(682, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(253, 173);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Plugins";
            // 
            // btnCreatePlugin
            // 
            this.btnCreatePlugin.Location = new System.Drawing.Point(8, 145);
            this.btnCreatePlugin.Name = "btnCreatePlugin";
            this.btnCreatePlugin.Size = new System.Drawing.Size(99, 22);
            this.btnCreatePlugin.TabIndex = 4;
            this.btnCreatePlugin.Text = "Create plugin";
            this.btnCreatePlugin.UseVisualStyleBackColor = true;
            this.btnCreatePlugin.Click += new System.EventHandler(this.btnManagePlugins_Click);
            // 
            // btnMakeInactive
            // 
            this.btnMakeInactive.Location = new System.Drawing.Point(113, 75);
            this.btnMakeInactive.Name = "btnMakeInactive";
            this.btnMakeInactive.Size = new System.Drawing.Size(27, 22);
            this.btnMakeInactive.TabIndex = 3;
            this.btnMakeInactive.Text = ">>";
            this.btnMakeInactive.UseVisualStyleBackColor = true;
            this.btnMakeInactive.Click += new System.EventHandler(this.btnMakeInactive_Click);
            // 
            // btnMakeActive
            // 
            this.btnMakeActive.Location = new System.Drawing.Point(113, 45);
            this.btnMakeActive.Name = "btnMakeActive";
            this.btnMakeActive.Size = new System.Drawing.Size(27, 22);
            this.btnMakeActive.TabIndex = 2;
            this.btnMakeActive.Text = "<<";
            this.btnMakeActive.UseVisualStyleBackColor = true;
            this.btnMakeActive.Click += new System.EventHandler(this.btnMakeActive_Click);
            // 
            // grdInactivePlugins
            // 
            this.grdInactivePlugins.AllowUserToAddRows = false;
            this.grdInactivePlugins.AllowUserToDeleteRows = false;
            this.grdInactivePlugins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdInactivePlugins.Location = new System.Drawing.Point(146, 15);
            this.grdInactivePlugins.Name = "grdInactivePlugins";
            this.grdInactivePlugins.ReadOnly = true;
            this.grdInactivePlugins.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Red;
            this.grdInactivePlugins.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.grdInactivePlugins.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdInactivePlugins.Size = new System.Drawing.Size(99, 124);
            this.grdInactivePlugins.TabIndex = 1;
            // 
            // grdActivePlugins
            // 
            this.grdActivePlugins.AllowUserToAddRows = false;
            this.grdActivePlugins.AllowUserToDeleteRows = false;
            this.grdActivePlugins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdActivePlugins.Location = new System.Drawing.Point(8, 15);
            this.grdActivePlugins.Name = "grdActivePlugins";
            this.grdActivePlugins.ReadOnly = true;
            this.grdActivePlugins.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.grdActivePlugins.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.grdActivePlugins.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdActivePlugins.Size = new System.Drawing.Size(99, 124);
            this.grdActivePlugins.TabIndex = 0;
            // 
            // grdLogs
            // 
            this.grdLogs.AllowUserToAddRows = false;
            this.grdLogs.AllowUserToDeleteRows = false;
            this.grdLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLogs.Location = new System.Drawing.Point(0, 194);
            this.grdLogs.Name = "grdLogs";
            this.grdLogs.ReadOnly = true;
            this.grdLogs.RowHeadersVisible = false;
            this.grdLogs.Size = new System.Drawing.Size(946, 465);
            this.grdLogs.TabIndex = 32;
            // 
            // Tailz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 659);
            this.Controls.Add(this.grdLogs);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(821, 370);
            this.Name = "Tailz";
            this.Text = "Tailz";
            ((System.ComponentModel.ISupportInitialize)(this.grdTails)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdInactivePlugins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdActivePlugins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button browseForTailfileButton;
        private System.Windows.Forms.TextBox tailFilenameTextbox;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.DataGridView grdTails;
        private System.Windows.Forms.Button btnAddWatcher;
        private System.Windows.Forms.CheckBox chkApplicationEventLog;
        private System.Windows.Forms.CheckBox chkSystemEventLog;
        private System.Windows.Forms.CheckBox chkSecurityEventLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView grdLogs;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnMakeInactive;
        private System.Windows.Forms.Button btnMakeActive;
        private System.Windows.Forms.DataGridView grdInactivePlugins;
        private System.Windows.Forms.DataGridView grdActivePlugins;
        private System.Windows.Forms.Button btnCreatePlugin;
    }
}


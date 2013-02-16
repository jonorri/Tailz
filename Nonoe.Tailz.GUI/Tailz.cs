namespace Nonoe.Tailz.GUI
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Security;
    using System.Windows.Forms;
    using Nonoe.Tailz.Core;

    public partial class Tailz : Form
    {
        private BindingList<Tail> tails;

        private EventLog applicationEventLog;

        private EventLog securityEventLog;

        private EventLog systemEventLog;

        public Tailz()
        {
            this.InitializeComponent();
            this.tails = new BindingList<Tail>();
            this.grdTails.DataSource = this.tails;

            this.grdLogs.Columns.Add("File", "File");
            this.grdLogs.Columns.Add("Message", "Message");

            applicationEventLog = new EventLog { Log = "Application", MachineName = "." };
            applicationEventLog.EntryWritten += new EntryWrittenEventHandler(this.OnEventLog);
            securityEventLog = new EventLog { Log = "Security", MachineName = "." };
            securityEventLog.EntryWritten += new EntryWrittenEventHandler(this.OnEventLog);
            systemEventLog = new EventLog { Log = "System", MachineName = "." };
            systemEventLog.EntryWritten += new EntryWrittenEventHandler(this.OnEventLog);
        }

        private void startTailButton_Click(object sender, EventArgs e)
        {
            foreach (var tail in this.tails)
            {
                tail.Start();    
            }

            btnStop.Enabled = true;
            btnStart.Enabled = false;
            
            try
            {
                if (this.chkApplicationEventLog.Checked)
                {
                    applicationEventLog.EnableRaisingEvents = true;
                }

                if (this.chkSecurityEventLog.Checked)
                {
                    securityEventLog.EnableRaisingEvents = true;
                }

                if (this.chkSystemEventLog.Checked)
                {
                    systemEventLog.EnableRaisingEvents = true;
                }
            }
            catch (SecurityException)
            {
                MessageBox.Show("You do not have the correct privileges to run this now.");
            }
        }

        private void stopTailButton_Click(object sender, EventArgs e)
        {
            foreach (var tail in tails)
            {
                tail.Stop();
            }

            if (this.chkApplicationEventLog.Checked)
            {
                applicationEventLog.EnableRaisingEvents = false;
            }

            if (this.chkSecurityEventLog.Checked)
            {
                securityEventLog.EnableRaisingEvents = false;
            }

            if (this.chkSystemEventLog.Checked)
            {
                systemEventLog.EnableRaisingEvents = false;
            }

            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.grdLogs.DataSource = null;
        }

        private void browseForTailfileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.tailFilenameTextbox.Text = openFileDialog.FileName;
            }
        }

        private void myTail_MoreData(object tailObject, string fileName, string newData)
        {
            this.AddRow(grdLogs, fileName, newData);
        }

        delegate void addRow(DataGridView grid, string fileName, string message);
        private void AddRow(DataGridView grid, string fileName, string message)
        {
            if (grid.InvokeRequired)
            {
                addRow callbackMethod = new addRow(this.AddRow);
                this.Invoke(callbackMethod, this.grdLogs, fileName, message);
            }
            else
            {
                foreach (string line in message.Split('\n'))
                {
                    string lineToSet = line.Replace("\n", string.Empty).Replace("\r", string.Empty);
                    if (!string.IsNullOrWhiteSpace(lineToSet))
                    { 
                        this.grdLogs.Rows.Add(fileName, lineToSet);
                    }
                }
            }
        }

        delegate void delRemoveText(TextBox ctl, int length);
        private void RemoveText(TextBox ctl, int length)
        {
            if (ctl.InvokeRequired)
            {
                delRemoveText callbackMethod = new delRemoveText(this.RemoveText);
                this.Invoke(callbackMethod, ctl, length);
            }
            else
            {
                ctl.Text = ctl.Text.Remove(0, length);
            }
        }

        delegate void delAppendText(TextBox ctl, string text);
        private void AppendText(TextBox ctl, string text)
        {
            if (ctl.InvokeRequired)
            {
                delAppendText callbackMethod = new delAppendText(this.AppendText);
                this.Invoke(callbackMethod, ctl, text);
            }
            else
            {
                ctl.AppendText(text);
            }
        }

        private void btnAddWatcher_Click(object sender, EventArgs e)
        {
            var tail = new Tail(this.tailFilenameTextbox.Text);
            tail.MoreData += this.myTail_MoreData;
            this.tails.Add(tail);
            this.CheckWhichButtonsShouldBeEnabled();
        }

        public void OnEventLog(object source, EntryWrittenEventArgs entryWrittenEventArgs)
        {
            this.AddRow(this.grdLogs, entryWrittenEventArgs.Entry.Source, entryWrittenEventArgs.Entry.Message);
        }

        private void grdTails_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (!this.tails.Any() && !chkApplicationEventLog.Checked && !chkSecurityEventLog.Checked && !chkSystemEventLog.Checked)
            {
                this.stopTailButton_Click(null, null);
            }
            CheckWhichButtonsShouldBeEnabled();
        }

        private void chkApplicationEventLog_CheckedChanged(object sender, EventArgs e)
        {
            CheckWhichButtonsShouldBeEnabled();
        }

        private void chkSecurityEventLog_CheckedChanged(object sender, EventArgs e)
        {
            CheckWhichButtonsShouldBeEnabled();
        }

        private void chkSystemEventLog_CheckedChanged(object sender, EventArgs e)
        {
            CheckWhichButtonsShouldBeEnabled();
        }

        private void CheckWhichButtonsShouldBeEnabled()
        {
            if (chkApplicationEventLog.Checked || chkSecurityEventLog.Checked || chkSystemEventLog.Checked
                || tails.Any())
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
            else
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
        }
    }
}

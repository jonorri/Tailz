// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tailz.cs" company="Nonoe">
//   No copyright
// </copyright>
// <summary>
//   TODO The tailz.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Nonoe.Tailz.GUI
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Security;
    using System.Windows.Forms;

    using Nonoe.Tailz.Core;

    /// <summary>The main/only form.</summary>
    public partial class Tailz : Form
    {
        #region Fields

        /// <summary>TODO The application event log.</summary>
        private readonly EventLog applicationEventLog;

        /// <summary>TODO The security event log.</summary>
        private readonly EventLog securityEventLog;

        /// <summary>TODO The system event log.</summary>
        private readonly EventLog systemEventLog;

        /// <summary>TODO The tails.</summary>
        private readonly BindingList<Tail> tails;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Tailz"/> class.</summary>
        public Tailz()
        {
            this.InitializeComponent();
            this.tails = new BindingList<Tail>();
            this.grdTails.DataSource = this.tails;

            this.grdLogs.Columns.Add("File", "File");
            this.grdLogs.Columns.Add("Message", "Message");

            this.applicationEventLog = new EventLog { Log = "Application", MachineName = "." };
            this.applicationEventLog.EntryWritten += this.OnEventLog;
            this.securityEventLog = new EventLog { Log = "Security", MachineName = "." };
            this.securityEventLog.EntryWritten += this.OnEventLog;
            this.systemEventLog = new EventLog { Log = "System", MachineName = "." };
            this.systemEventLog.EntryWritten += this.OnEventLog;
        }

        #endregion

        #region Delegates

        /// <summary>TODO The add row.</summary>
        /// <param name="grid">TODO The grid.</param>
        /// <param name="fileName">TODO The file name.</param>
        /// <param name="message">TODO The message.</param>
        private delegate void addRow(DataGridView grid, string fileName, string message);

        #endregion

        #region Public Methods and Operators

        /// <summary>TODO The on event log.</summary>
        /// <param name="source">TODO The source.</param>
        /// <param name="entryWrittenEventArgs">TODO The entry written event args.</param>
        public void OnEventLog(object source, EntryWrittenEventArgs entryWrittenEventArgs)
        {
            this.AddRow(this.grdLogs, entryWrittenEventArgs.Entry.Source, entryWrittenEventArgs.Entry.Message);
        }

        #endregion

        #region Methods

        /// <summary>TODO The add row.</summary>
        /// <param name="grid">TODO The grid.</param>
        /// <param name="fileName">TODO The file name.</param>
        /// <param name="message">TODO The message.</param>
        private void AddRow(DataGridView grid, string fileName, string message)
        {
            if (grid.InvokeRequired)
            {
                addRow callbackMethod = this.AddRow;
                this.Invoke(callbackMethod, this.grdLogs, fileName, message);
            }
            else
            {
                foreach (string lineToSet in message.Split('\n').Select(line => line.Replace("\n", string.Empty).Replace("\r", string.Empty)).Where(lineToSet => !string.IsNullOrWhiteSpace(lineToSet)))
                {
                    this.grdLogs.Rows.Add(fileName, lineToSet);
                }
            }
        }

        /// <summary>TODO The browse for tailfile button_ click.</summary>
        /// <param name="sender">TODO The sender.</param>
        /// <param name="e">TODO The e.</param>
        private void browseForTailfileButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.tailFilenameTextbox.Text = openFileDialog.FileName;
            }
        }

        /// <summary>TODO The btn add watcher_ click.</summary>
        /// <param name="sender">TODO The sender.</param>
        /// <param name="e">TODO The e.</param>
        private void btnAddWatcher_Click(object sender, EventArgs e)
        {
            var tail = new Tail(this.tailFilenameTextbox.Text);
            tail.MoreData += this.myTail_MoreData;
            this.tails.Add(tail);
        }

        /// <summary>TODO The clear button_ click.</summary>
        /// <param name="sender">TODO The sender.</param>
        /// <param name="e">TODO The e.</param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            this.grdLogs.DataSource = null;
        }

        /// <summary>TODO The grd tails_ user deleted row.</summary>
        /// <param name="sender">TODO The sender.</param>
        /// <param name="e">TODO The e.</param>
        private void grdTails_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (!this.tails.Any() && !this.chkApplicationEventLog.Checked && !this.chkSecurityEventLog.Checked
                && !this.chkSystemEventLog.Checked)
            {
                this.stopTailButton_Click(null, null);
            }
        }

        /// <summary>TODO The my tail_ more data.</summary>
        /// <param name="tailObject">TODO The tail object.</param>
        /// <param name="fileName">TODO The file name.</param>
        /// <param name="newData">TODO The new data.</param>
        private void myTail_MoreData(object tailObject, string fileName, string newData)
        {
            this.AddRow(this.grdLogs, fileName, newData);
        }

        /// <summary>TODO The start tail button_ click.</summary>
        /// <param name="sender">TODO The sender.</param>
        /// <param name="e">TODO The e.</param>
        private void startTailButton_Click(object sender, EventArgs e)
        {
            foreach (Tail tail in this.tails)
            {
                tail.Start();
            }

            try
            {
                if (this.chkApplicationEventLog.Checked)
                {
                    this.applicationEventLog.EnableRaisingEvents = true;
                }

                if (this.chkSecurityEventLog.Checked)
                {
                    this.securityEventLog.EnableRaisingEvents = true;
                }

                if (this.chkSystemEventLog.Checked)
                {
                    this.systemEventLog.EnableRaisingEvents = true;
                }
            }
            catch (SecurityException)
            {
                MessageBox.Show("You do not have the correct privileges to run this now.");
            }

            this.btnStop.Enabled = true;
            this.btnStart.Enabled = false;
            this.grdTails.Enabled = false;
            this.btnAddWatcher.Enabled = false;
            this.chkApplicationEventLog.Enabled = false;
            this.chkSecurityEventLog.Enabled = false;
            this.chkSystemEventLog.Enabled = false;
        }

        /// <summary>TODO The stop tail button_ click.</summary>
        /// <param name="sender">TODO The sender.</param>
        /// <param name="e">TODO The e.</param>
        private void stopTailButton_Click(object sender, EventArgs e)
        {
            foreach (Tail tail in this.tails)
            {
                tail.Stop();
            }

            if (this.chkApplicationEventLog.Checked)
            {
                this.applicationEventLog.EnableRaisingEvents = false;
            }

            if (this.chkSecurityEventLog.Checked)
            {
                this.securityEventLog.EnableRaisingEvents = false;
            }

            if (this.chkSystemEventLog.Checked)
            {
                this.systemEventLog.EnableRaisingEvents = false;
            }

            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
            this.grdTails.Enabled = true;
            this.btnAddWatcher.Enabled = true;
            this.chkApplicationEventLog.Enabled = true;
            this.chkSecurityEventLog.Enabled = true;
            this.chkSystemEventLog.Enabled = true;
        }

        #endregion
    }
}
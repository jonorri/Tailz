// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tailz.cs" company="Nonoe">
//   No copyright
// </copyright>
// <summary>
//   The main and only form in this application.
//   It shows which tailz are running on which logs.
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

        /// <summary>The application event log.</summary>
        private readonly EventLog applicationEventLog;

        /// <summary>The security event log.</summary>
        private readonly EventLog securityEventLog;

        /// <summary>The system event log.</summary>
        private readonly EventLog systemEventLog;

        /// <summary>The log tails that are running.</summary>
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

        /// <summary>The add row delegate.</summary>
        /// <param name="grid">The grid to add the row to.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="message">The message.</param>
        private delegate void addRow(DataGridView grid, string fileName, string message);

        #endregion

        #region Public Methods and Operators

        /// <summary>The on event log event handler.</summary>
        /// <param name="source">The source of the event log.</param>
        /// <param name="entryWrittenEventArgs">The entry written event argument.</param>
        public void OnEventLog(object source, EntryWrittenEventArgs entryWrittenEventArgs)
        {
            this.AddRow(this.grdLogs, entryWrittenEventArgs.Entry.Source, entryWrittenEventArgs.Entry.Message);
        }

        #endregion

        #region Methods

        /// <summary>The add row method.</summary>
        /// <param name="grid">The grid to add the row to.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="message">The message.</param>
        private void AddRow(DataGridView grid, string fileName, string message)
        {
            if (grid.InvokeRequired)
            {
                addRow callbackMethod = this.AddRow;
                this.Invoke(callbackMethod, this.grdLogs, fileName, message);
            }
            else
            {
                foreach (var lineToSet in message.Split('\n').Select(line => line.Replace("\n", string.Empty).Replace("\r", string.Empty)).Where(lineToSet => !string.IsNullOrWhiteSpace(lineToSet)))
                {
                    this.grdLogs.Rows.Add(fileName, lineToSet);
                }
            }
        }

        /// <summary>The browse for tail file button click event handler.</summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event argument.</param>
        private void browseForTailfileButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.tailFilenameTextbox.Text = openFileDialog.FileName;
            }
        }

        /// <summary>The add watcher button click event handler.</summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event argument.</param>
        private void btnAddWatcher_Click(object sender, EventArgs e)
        {
            var tail = new Tail(this.tailFilenameTextbox.Text);
            tail.MoreData += this.myTail_MoreData;
            this.tails.Add(tail);
        }

        /// <summary>The clear button click event handler.</summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event argument.</param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            this.grdLogs.DataSource = null;
        }

        /// <summary>The more data event handler.</summary>
        /// <param name="tailObject">The tail object.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="newData">The new data.</param>
        private void myTail_MoreData(object tailObject, string fileName, string newData)
        {
            this.AddRow(this.grdLogs, fileName, newData);
        }

        /// <summary>The start tail button click event handler.</summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event argument.</param>
        private void startTailButton_Click(object sender, EventArgs e)
        {
            foreach (var tail in this.tails)
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
                MessageBox.Show("You have to run the application as administrator to perform this action.");
            }

            this.btnStop.Enabled = true;
            this.btnStart.Enabled = false;
            this.grdTails.Enabled = false;
            this.btnAddWatcher.Enabled = false;
            this.chkApplicationEventLog.Enabled = false;
            this.chkSecurityEventLog.Enabled = false;
            this.chkSystemEventLog.Enabled = false;
        }

        /// <summary>The stop tail button click event handler.</summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event argument.</param>
        private void stopTailButton_Click(object sender, EventArgs e)
        {
            foreach (var tail in this.tails)
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
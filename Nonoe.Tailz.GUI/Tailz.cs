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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Security;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    using Nonoe.Tailz.Core;
    using Nonoe.Tailz.Core.Objects;
    using Nonoe.Tailz.Core.Ruby;

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

        /// <summary>The active plugins that the application supports</summary>
        private readonly BindingList<Plugin> activePlugins;

        /// <summary>The inactive plugins that the application supports</summary>
        private readonly BindingList<Plugin> inactivePlugins;

        /// <summary>The plugin business implementation</summary>
        private readonly Plugins pluginBusiness;

        /// <summary>All the logs that the system is monitoring</summary>
        private readonly BindingList<Log> logs;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Tailz"/> class.</summary>
        public Tailz()
        {
            this.InitializeComponent();
            this.tails = new BindingList<Tail>();
            this.grdTails.DataSource = this.tails;

            this.logs = new BindingList<Log>();
            this.grdLogs.DataSource = this.logs;

            this.applicationEventLog = new EventLog { Log = "Application", MachineName = "." };
            this.applicationEventLog.EntryWritten += this.OnEventLog;
            this.securityEventLog = new EventLog { Log = "Security", MachineName = "." };
            this.securityEventLog.EntryWritten += this.OnEventLog;
            this.systemEventLog = new EventLog { Log = "System", MachineName = "." };
            this.systemEventLog.EntryWritten += this.OnEventLog;

            this.pluginBusiness = new Plugins();
            this.pluginBusiness.Plugin += this.OnPluginCreated;

            // Fetch from the data store.
            this.inactivePlugins = new BindingList<Plugin>(this.pluginBusiness.GetPluginsByActivity(false));
            this.activePlugins = new BindingList<Plugin>(this.pluginBusiness.GetPluginsByActivity(true));

            this.grdActivePlugins.DataSource = this.activePlugins;
            this.grdInactivePlugins.DataSource = this.inactivePlugins;
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
                this.logs.Insert(0, new Log { File = fileName, Message = message, Date = DateTime.UtcNow });
                while (this.grdLogs.RowCount > this.nmbMaxLines.Value)
                {
                    this.logs.RemoveAt(this.grdLogs.RowCount - 1);
                }

                this.txtSearch_TextChanged(new object(), new EventArgs());
            }
        }

        private void OnPluginCreated(object sender, string pluginName, string rubyScript)
        {
            this.inactivePlugins.Add(new Plugin { Active = false, PluginName = pluginName, RubyScript = rubyScript });
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
            this.logs.Clear();
        }

        /// <summary>The more data event handler.</summary>
        /// <param name="tailObject">The tail object.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="newData">The new data.</param>
        private void myTail_MoreData(object tailObject, string fileName, string newData)
        {
            foreach (string lineToUse in newData.Split('\n').Select(line => line.Replace("\n", string.Empty).Replace("\r", string.Empty)).Where(lineToSet => !string.IsNullOrWhiteSpace(lineToSet)))
            {
                string lineToSet = lineToUse;
                foreach (var activePlugin in this.activePlugins)
                {
                    lineToSet = RubyRunner.Run(activePlugin.RubyScript, lineToUse);
                }

                this.AddRow(this.grdLogs, fileName, lineToSet);
            }
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

        private void btnMakeActive_Click(object sender, EventArgs e)
        {
            // TODO: This shouldn't be done here. Have to fix this with for example Event InactivePluginsChanged && ActivePluginsChanged
            // This is just a temporary mix. Only for testing purposes.
            foreach (DataGridViewRow row in this.grdInactivePlugins.SelectedRows)
            {
                string pluginName = this.grdInactivePlugins.Rows[row.Index].Cells["PluginName"].Value.ToString();
                string pluginContent = this.grdInactivePlugins.Rows[row.Index].Cells["RubyScript"].Value.ToString();
                this.pluginBusiness.ChangePluginActivity(pluginName, pluginContent, true);
                var plugin = this.inactivePlugins.First(x => x.PluginName == pluginName && x.RubyScript == pluginContent);
                this.inactivePlugins.Remove(plugin);
                plugin.Active = true;
                this.activePlugins.Add(plugin);
            }
        }

        private void btnMakeInactive_Click(object sender, EventArgs e)
        {
            // TODO: This shouldn't be done here. Have to fix this with for example Event InactivePluginsChanged && ActivePluginsChanged
            // This is just a temporary mix. Only for testing purposes.
            foreach (DataGridViewRow row in this.grdActivePlugins.SelectedRows)
            {
                string pluginName = this.grdActivePlugins.Rows[row.Index].Cells["PluginName"].Value.ToString();
                string pluginContent = this.grdActivePlugins.Rows[row.Index].Cells["RubyScript"].Value.ToString();
                this.pluginBusiness.ChangePluginActivity(pluginName, pluginContent, false);
                var plugin = this.activePlugins.First(x => x.PluginName == pluginName && x.RubyScript == pluginContent);
                this.activePlugins.Remove(plugin);
                plugin.Active = false;
                this.inactivePlugins.Add(plugin);
            }
        }

        private void btnManagePlugins_Click(object sender, EventArgs e)
        {
            string pluginName = string.Empty;
            string pluginContent = string.Empty;
            if (GuiHelpers.InputBox(ref pluginName, ref pluginContent) == DialogResult.OK)
            {
                this.pluginBusiness.CreatePlugin(pluginName, pluginContent);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.txtSearch.ForeColor = Color.Black;
            if (string.IsNullOrWhiteSpace(this.txtSearch.Text))
            {
                this.grdLogs.DataSource = null;
                this.grdLogs.DataSource = this.logs;
            }
            else
            {
                this.grdLogs.DataSource = null;
                try
                {
                    if (this.chkEnableRegExSearch.Checked)
                    {
                        Regex regEx = new Regex(this.txtSearch.Text);
                        this.grdLogs.DataSource = new BindingList<Log>(this.logs.Where(x => regEx.IsMatch(x.File) || regEx.IsMatch(x.Message)).ToList());
                    }
                    else
                    {
                        this.grdLogs.DataSource = new BindingList<Log>(this.logs.Where(x => x.File.ToLower().Contains(this.txtSearch.Text.ToLower()) || x.Message.ToLower().Contains(this.txtSearch.Text.ToLower())).ToList());
                    }
                }
                catch (ArgumentException)
                {
                    this.txtSearch.ForeColor = Color.Red;
                }
            }
        }
    }
}
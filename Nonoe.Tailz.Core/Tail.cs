// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tail.cs" company="Nonoe">
//   Copyright JOK 2013
// </copyright>
// <summary>
//   The tail.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Nonoe.Tailz.Core
{
    using System.IO;
    using System.Text;

    /// <summary>
    /// The tail.
    /// </summary>
    public class Tail
    {
        #region Fields

        /// <summary>
        /// The file system watcher.
        /// </summary>
        private FileSystemWatcher fileSystemWatcher;

        /// <summary>
        /// The max bytes.
        /// </summary>
        private int maxBytes = 1024 * 16;

        /// <summary>
        /// The previous seek position.
        /// </summary>
        private long previousSeekPosition;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tail"/> class.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        public Tail(string filename)
        {
            this.FileName = filename;
        }

        #endregion

        #region Delegates

        /// <summary>
        /// The more data handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="newData">The new data.</param>
        public delegate void MoreDataHandler(object sender, string fileName, string newData);

        #endregion

        #region Public Events

        /// <summary>
        /// The more data.
        /// </summary>
        public event MoreDataHandler MoreData;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the max bytes.
        /// </summary>
        public int MaxBytes
        {
            get
            {
                return this.maxBytes;
            }

            set
            {
                this.maxBytes = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The read full file.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string ReadFullFile()
        {
            using (var streamReader = new StreamReader(this.FileName))
            {
                return streamReader.ReadToEnd();
            }
        }

        /// <summary>
        /// The start.
        /// </summary>
        public void Start()
        {
            var targetFile = new FileInfo(this.FileName);

            this.previousSeekPosition = 0;

            this.fileSystemWatcher = new FileSystemWatcher();
            this.fileSystemWatcher.IncludeSubdirectories = false;
            this.fileSystemWatcher.Path = targetFile.DirectoryName;
            this.fileSystemWatcher.Filter = targetFile.Name;

            if (!targetFile.Exists)
            {
                this.fileSystemWatcher.Created += this.TargetFile_Created;
                this.fileSystemWatcher.EnableRaisingEvents = true;
            }
            else
            {
                this.TargetFile_Changed(null, null);
                this.StartMonitoring();
            }
        }

        /// <summary>
        /// The start monitoring.
        /// </summary>
        public void StartMonitoring()
        {
            this.fileSystemWatcher.Changed += this.TargetFile_Changed;
            this.fileSystemWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// The stop.
        /// </summary>
        public void Stop()
        {
            this.fileSystemWatcher.EnableRaisingEvents = false;
            this.fileSystemWatcher.Dispose();
        }

        /// <summary>
        /// The target file_ changed.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The e.</param>
        public void TargetFile_Changed(object source, FileSystemEventArgs e)
        {
            // read from current seek position to end of file
            var bytesRead = new byte[this.maxBytes];
            var fs = new FileStream(this.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            if (fs.Length > this.maxBytes)
            {
                this.previousSeekPosition = fs.Length - this.maxBytes;
            }

            this.previousSeekPosition = (int)fs.Seek(this.previousSeekPosition, SeekOrigin.Begin);
            int numBytes = fs.Read(bytesRead, 0, this.maxBytes);
            fs.Close();
            this.previousSeekPosition += numBytes;

            var sb = new StringBuilder();
            for (int i = 0; i < numBytes; i++)
            {
                sb.Append((char)bytesRead[i]);
            }

            // call delegates with the string
            this.MoreData(this, this.FileName, sb.ToString());
        }

        /// <summary>
        /// The target file_ created.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The e.</param>
        public void TargetFile_Created(object source, FileSystemEventArgs e)
        {
            this.StartMonitoring();
        }

        #endregion
    }
}
﻿namespace Nonoe.Tailz.Core
{
    using System.IO;
    using System.Text;

    public class Tail
    {
        public string FileName { get; set; }

        FileSystemWatcher fileSystemWatcher = null;
        long previousSeekPosition;

        public delegate void MoreDataHandler(object sender, string fileName, string newData);
        public event MoreDataHandler MoreData;

        private int maxBytes = 1024 * 16;
        public int MaxBytes
        {
            get { return this.maxBytes; }
            set { this.maxBytes = value; }
        }

        public Tail(string filename)
        {
            this.FileName = filename;
        }

        public void Start()
        {
            FileInfo targetFile = new FileInfo(this.FileName);

            previousSeekPosition = 0;

            fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.IncludeSubdirectories = false;
            fileSystemWatcher.Path = targetFile.DirectoryName;
            fileSystemWatcher.Filter = targetFile.Name;

            if (!targetFile.Exists)
            {
                fileSystemWatcher.Created += new FileSystemEventHandler(TargetFile_Created);
                fileSystemWatcher.EnableRaisingEvents = true;
            }
            else
            {
                TargetFile_Changed(null, null);
                StartMonitoring();
            }
        }

        public void Stop()
        {
            fileSystemWatcher.EnableRaisingEvents = false;
            fileSystemWatcher.Dispose();
        }

        public string ReadFullFile()
        {
            using (StreamReader streamReader = new StreamReader(this.FileName))
            {
                return streamReader.ReadToEnd();
            }
        }

        public void StartMonitoring()
        {
            fileSystemWatcher.Changed += new FileSystemEventHandler(TargetFile_Changed);
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void TargetFile_Created(object source, FileSystemEventArgs e)
        {
            StartMonitoring();
        }

        public void TargetFile_Changed(object source, FileSystemEventArgs e)
        {
            //read from current seek position to end of file
            byte[] bytesRead = new byte[maxBytes];
            FileStream fs = new FileStream(this.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            if (fs.Length > maxBytes)
            {
                this.previousSeekPosition = fs.Length - maxBytes;
            }

            this.previousSeekPosition = (int)fs.Seek(this.previousSeekPosition, SeekOrigin.Begin);
            int numBytes = fs.Read(bytesRead, 0, maxBytes);
            fs.Close();
            this.previousSeekPosition += numBytes;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numBytes; i++)
            {
                sb.Append((char)bytesRead[i]);
            }

            //call delegates with the string
            this.MoreData(this, this.FileName, sb.ToString());
        }
    }
}

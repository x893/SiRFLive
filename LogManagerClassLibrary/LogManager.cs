﻿namespace LogManagerClassLibrary
{
    using CommonClassLibrary;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    public class LogManager
    {
        private DateTime _DelayedLoggingStartTime;
        private Label _DurationLoggingStatusLabel;
        private DateTime _DurationLoggingStopTime;
        private string _errorFilename;
        private string _filename;
        private bool _isBin;
        private bool _isFileOpen;
        private object _lockFileOpenIndication;
        private string _logDirectory;
        private LoggingStates _LoggingState;
        private Label _LoggingStatusLabel;
        private writeModule _myWriter;
        private string _newFilePath;
        private string _version;
        public string DUTName;
        public string FirmwareSWVersion;
        private object lockErrorWrite;
        private object lockread;
        private object lockwrite;
        private DateTime LoggingStoppedTime;
        private BinaryWriter m_binaryWriter;
        private bool m_ErrorFileOpen;
        private StreamWriter m_errorLogWriter;
        private FileStream m_fileWriter;
        private StreamReader m_streamreader;
        private StreamWriter m_streamwriter;
        private string PCname;
        public string PhysicalConnection;

        public event UpdateParentEventHandler UpdateMainWindow;

        public LogManager()
        {
            _lockFileOpenIndication = new object();
            lockwrite = new object();
            lockread = new object();
            lockErrorWrite = new object();
            _version = string.Empty;
            PCname = Environment.MachineName;
            PhysicalConnection = "UART";
            FirmwareSWVersion = string.Empty;
            DUTName = string.Empty;
            _logDirectory = string.Empty;
            _filename = string.Empty;
            _newFilePath = string.Empty;
            m_FileOpen = false;
            _LoggingState = LoggingStates.idle;
        }

        public LogManager(string version)
        {
            _lockFileOpenIndication = new object();
            lockwrite = new object();
            lockread = new object();
            lockErrorWrite = new object();
            _version = string.Empty;
            PCname = Environment.MachineName;
            PhysicalConnection = "UART";
            FirmwareSWVersion = string.Empty;
            DUTName = string.Empty;
            _logDirectory = string.Empty;
            _filename = string.Empty;
            _newFilePath = string.Empty;
            m_FileOpen = false;
            _version = version;
            _LoggingState = LoggingStates.idle;
            FirmwareSWVersion = string.Empty;
        }

        public bool CloseFile()
        {
            EventHandler method = null;
            _LoggingState = LoggingStates.idle;
            if (_DurationLoggingStatusLabel != null)
            {
                if (method == null)
                {
                    method = delegate {
                        _DurationLoggingStatusLabel.Text = filename;
                    };
                }
                _DurationLoggingStatusLabel.BeginInvoke(method);
            }
            return closeFileHandler();
        }

        private bool closeFileHandler()
        {
            try
            {
                if (m_FileOpen)
                {
                    m_FileOpen = false;
                    m_ErrorFileOpen = false;
                    _myWriter.Close();
                    if (m_streamwriter != null)
                    {
                        lock (lockwrite)
                        {
                            m_streamwriter.Close();
                        }
                        m_streamwriter.Dispose();
                    }
                    if (m_streamreader != null)
                    {
                        m_streamreader.Close();
                        m_streamreader.Dispose();
                    }
                    if (m_errorLogWriter != null)
                    {
                        m_errorLogWriter.Close();
                        m_errorLogWriter.Dispose();
                    }
                }
                if (UpdateMainWindow != null)
                {
                    UpdateMainWindow();
                }
            }
            catch (Exception exception)
            {
                string message = exception.Message;
                return false;
            }
            return true;
        }

        public void ErrorWriteLine(string msg)
        {
            if (m_ErrorFileOpen && (m_errorLogWriter != null))
            {
                lock (lockErrorWrite)
                {
                    m_errorLogWriter.WriteLine(msg);
                }
            }
        }

        public bool IsFileOpen()
        {
            return m_FileOpen;
        }

        public bool OpenFile()
        {
            return OpenFile(_filename);
        }

        public bool OpenFile(string inFilename)
        {
            try
            {
                closeFileHandler();
                _filename = inFilename;
                if (File.Exists(inFilename))
                {
                    new FileInfo(inFilename);
                    string str = string.Format("File exists: {0}", inFilename);
                    frmDialogWith3Button button = new frmDialogWith3Button("Log Message", "&Append", "&Overwrite", "&Cancel");
                    button.DisplayMessage = str;
                    switch (button.ShowDialog())
                    {
                        case DialogResult.OK:
                            m_fileWriter = new FileStream(inFilename, FileMode.Append);
                            goto Label_009F;

                        case DialogResult.Yes:
                            m_fileWriter = new FileStream(inFilename, FileMode.Create);
                            goto Label_009F;
                    }
                    if (UpdateMainWindow != null)
                    {
                        UpdateMainWindow();
                    }
                    return false;
                }
                m_fileWriter = new FileStream(inFilename, FileMode.Create);
            Label_009F:
                if (_isBin)
                {
                    m_binaryWriter = new BinaryWriter(m_fileWriter);
                    _myWriter = new writeBin(m_binaryWriter);
                }
                else
                {
                    m_streamwriter = new StreamWriter(m_fileWriter);
                    _myWriter = new writeText(m_streamwriter);
                }
                if (_LoggingState == LoggingStates.idle)
                {
                    _LoggingState = LoggingStates.logging;
                }
                m_FileOpen = true;
                if ((_version != string.Empty) && !inFilename.EndsWith(".csv"))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("SiRFLive Version: ");
                    builder.Append(_version);
                    builder.Append("\r\n");
                    builder.Append(DateTime.Now.ToLongDateString());
                    builder.Append("\r\nPC: ");
                    builder.Append(PCname);
                    builder.Append("\r\nPhysical Connection: ");
                    builder.Append(PhysicalConnection);
                    builder.Append("\r\n");
                    builder.Append(FirmwareSWVersion);
                    builder.Append("\r\nDUT Name: ");
                    builder.Append(DUTName);
                    builder.Append("\r\n");
                    Write(builder.ToString());
                    FileInfo info = new FileInfo(inFilename);
                    if (info.Extension == string.Empty)
                    {
                        _errorFilename = inFilename + "_error.txt";
                    }
                    else
                    {
                        _errorFilename = inFilename.Replace(info.Extension, "_error.txt");
                    }
                    m_errorLogWriter = new StreamWriter(_errorFilename, false);
                    m_ErrorFileOpen = true;
                }
                FileInfo info2 = new FileInfo(_filename);
                _logDirectory = info2.DirectoryName;
                if (UpdateMainWindow != null)
                {
                    UpdateMainWindow();
                }
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "SiRFLive...", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                if (UpdateMainWindow != null)
                {
                    UpdateMainWindow();
                }
                return false;
            }
        }

        public void OpenFile(string filename, bool append)
        {
            if (append)
            {
                _filename = filename;
                m_fileWriter = new FileStream(_filename, FileMode.Append);
                if (_isBin)
                {
                    m_binaryWriter = new BinaryWriter(m_fileWriter);
                    _myWriter = new writeBin(m_binaryWriter);
                }
                else
                {
                    m_streamwriter = new StreamWriter(m_fileWriter);
                    _myWriter = new writeText(m_streamwriter);
                }
                m_FileOpen = true;
                _LoggingState = LoggingStates.logging;
                if (UpdateMainWindow != null)
                {
                    UpdateMainWindow();
                }
            }
        }

        public bool OpenFileRead(string inFilename)
        {
            try
            {
                _filename = inFilename;
                if (File.Exists(inFilename))
                {
                    m_streamreader = File.OpenText(inFilename);
                    m_FileOpen = true;
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "SiRFLive...", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        public string ReadLine()
        {
            try
            {
                if (m_FileOpen)
                {
                    return m_streamreader.ReadLine();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public void RunCallBackFnc()
        {
            if (UpdateMainWindow != null)
            {
                UpdateMainWindow();
            }
        }

        public void SetDurationLoggingStatusLabel(string inputString)
        {
            EventHandler method = null;
            if (_DurationLoggingStatusLabel.InvokeRequired)
            {
                if (method == null)
                {
                    method = delegate {
                        _DurationLoggingStatusLabel.Text = inputString;
                    };
                }
                _DurationLoggingStatusLabel.BeginInvoke(method);
            }
            else
            {
                _DurationLoggingStatusLabel.Text = inputString;
            }
        }

        private void updateFilePath(string inputString)
        {
            _newFilePath = inputString;
        }

        private LoggingStates UpdateLoggingState()
        {
            if ((_LoggingState == LoggingStates.delayed_logging) && (DateTime.Now.CompareTo(_DelayedLoggingStartTime) >= 0))
            {
                if (_DurationLoggingStopTime > DateTime.Now)
                {
                    _LoggingState = LoggingStates.duration_logging;
                }
                else
                {
                    _LoggingState = LoggingStates.logging;
                }
                return _LoggingState;
            }
            if (_LoggingState == LoggingStates.duration_logging)
            {
                string msgStr = string.Format("Duration Logging Stop Time: {0} -- {1}", _DurationLoggingStopTime.ToString(), filename);
				_DurationLoggingStatusLabel.BeginInvoke((MethodInvoker)delegate
				{
                    _DurationLoggingStatusLabel.Text = msgStr;
                });
                if (DateTime.Now.CompareTo(_DurationLoggingStopTime) >= 0)
                {
                    LoggingStoppedTime = DateTime.Now;
                    _LoggingState = LoggingStates.stopped_logging;
                    closeFileHandler();
                    return _LoggingState;
                }
            }
            return _LoggingState;
        }

        public bool Write(string msg)
        {
            EventHandler method = null;
            try
            {
                if (_LoggingState == LoggingStates.idle)
                {
                    return true;
                }
                if (((_LoggingState == LoggingStates.logging) || (_LoggingState == LoggingStates.duration_logging)) && m_FileOpen)
                {
                    lock (lockwrite)
                    {
                        _myWriter.Write(msg);
                    }
                }
                if ((_LoggingState == LoggingStates.delayed_logging) || (_LoggingState == LoggingStates.duration_logging))
                {
                    LoggingState = UpdateLoggingState();
                }
                if (_LoggingState == LoggingStates.stopped_logging)
                {
                    if (method == null)
                    {
                        method = delegate {
                            _DurationLoggingStatusLabel.Text = "Logging Stopped at: " + LoggingStoppedTime.ToString();
                        };
                    }
                    _DurationLoggingStatusLabel.BeginInvoke(method);
                    if (UpdateMainWindow != null)
                    {
                        UpdateMainWindow();
                    }
                    _LoggingState = LoggingStates.idle;
                }
                LoggingStates states1 = _LoggingState;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Write(byte[] msgB)
        {
            EventHandler method = null;
            try
            {
                if (_LoggingState == LoggingStates.idle)
                {
                    return true;
                }
                if (((_LoggingState == LoggingStates.logging) || (_LoggingState == LoggingStates.duration_logging)) && m_FileOpen)
                {
                    lock (lockwrite)
                    {
                        _myWriter.Write(msgB);
                    }
                }
                if ((_LoggingState == LoggingStates.delayed_logging) || (_LoggingState == LoggingStates.duration_logging))
                {
                    LoggingState = UpdateLoggingState();
                }
                if (_LoggingState == LoggingStates.stopped_logging)
                {
                    if (method == null)
                    {
                        method = delegate {
                            _DurationLoggingStatusLabel.Text = "Logging Stopped at: " + LoggingStoppedTime.ToString();
                        };
                    }
                    _DurationLoggingStatusLabel.BeginInvoke(method);
                    if (UpdateMainWindow != null)
                    {
                        UpdateMainWindow();
                    }
                    _LoggingState = LoggingStates.idle;
                }
                LoggingStates states1 = _LoggingState;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool WriteLine(string msg)
        {
            EventHandler method = null;
            try
            {
                if (_LoggingState == LoggingStates.idle)
                {
                    return true;
                }
                if (((_LoggingState == LoggingStates.logging) || (_LoggingState == LoggingStates.duration_logging)) && m_FileOpen)
                {
                    lock (lockwrite)
                    {
                        _myWriter.WriteLine(msg);
                    }
                }
                if ((_LoggingState == LoggingStates.delayed_logging) || (_LoggingState == LoggingStates.duration_logging))
                {
                    LoggingState = UpdateLoggingState();
                }
                if (_LoggingState == LoggingStates.stopped_logging)
                {
                    if (method == null)
                    {
                        method = delegate {
                            _DurationLoggingStatusLabel.Text = "Logging Stopped at: " + LoggingStoppedTime.ToString();
                        };
                    }
                    _DurationLoggingStatusLabel.BeginInvoke(method);
                    _LoggingState = LoggingStates.idle;
                    if (UpdateMainWindow != null)
                    {
                        UpdateMainWindow();
                    }
                }
                LoggingStates states1 = _LoggingState;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public DateTime DelayedLoggingStartTime
        {
            get
            {
                return _DelayedLoggingStartTime;
            }
            set
            {
                _DelayedLoggingStartTime = value;
            }
        }

        public Label DurationLoggingStatusLabel
        {
            get
            {
                return _DurationLoggingStatusLabel;
            }
            set
            {
                _DurationLoggingStatusLabel = value;
            }
        }

        public DateTime DurationLoggingStopTime
        {
            get
            {
                return _DurationLoggingStopTime;
            }
            set
            {
                _DurationLoggingStopTime = value;
            }
        }

        public string filename
        {
            get
            {
                return _filename;
            }
            set
            {
                try
                {
                    FileInfo info = new FileInfo(value);
                    _filename = info.FullName;
                }
                catch
                {
                    _filename = value;
                }
            }
        }

        public bool IsBin
        {
            get
            {
                return _isBin;
            }
            set
            {
                _isBin = value;
            }
        }

        public string LogDirectory
        {
            get
            {
                return _logDirectory;
            }
            set
            {
                _logDirectory = value;
            }
        }

        public LoggingStates LoggingState
        {
            get
            {
                return _LoggingState;
            }
            set
            {
                _LoggingState = value;
            }
        }

        public Label LoggingStatusLabel
        {
            get
            {
                return _LoggingStatusLabel;
            }
            set
            {
                _LoggingStatusLabel = value;
            }
        }

        private bool m_FileOpen
        {
            get
            {
                return _isFileOpen;
            }
            set
            {
                lock (_lockFileOpenIndication)
                {
                    _isFileOpen = value;
                }
            }
        }

        public string StartStopLog
        {
            get
            {
                if (m_FileOpen)
                {
                    return "Stop Log";
                }
                return "Start Log";
            }
            set
            {
            }
        }

        public enum LoggingStates
        {
            idle,
            delayed_logging,
            logging,
            duration_logging,
            stopped_logging
        }

        public delegate void UpdateParentEventHandler();
    }
}


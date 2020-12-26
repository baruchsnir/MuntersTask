////////////////////////////////////////////////////////////////////////////////
// Unit        : ULog                                                          |
// Project     : Scopus Network Technologies Ltd.                              |
// Copyright   :                                                               |
// Developer   : Baruch Snir                                                   |
// Date        : 24.01.04                                                      |
// Description : Log class.                                                    |
// Updates     : -                                                             |
// TODOs       : -                                                             |
//----------------------------------------------------------------------------/
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FT.CommonDll.Ini;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace FT.CommonDll
{
	/// <summary>
	/// Summary description for Log Type.
	/// enum for the 5 types of update RICHEDIT LINES
	/// </summary>
	public enum LogType
	{
        ltGeneral = 0, ltInfo, ltSuccess, ltWarning, ltError, ltBlue, ltGreen, ltMagenta
	}

    #region LogLineData
    /// <summary>
    /// Class that hold data of log line with the logtype
    /// </summary>
    public class LogLineData
    {
        /// <summary>
        /// hold data of log line with the logtype
        /// </summary>
        /// <param name="line">The line text</param>
        /// <param name="logType">The Log type of this line</param>
        public LogLineData(string line, LogType logType)
        {
            this.line = line;
            this.logType = logType;
        }
        /// <summary>
        ///  The line text
        /// </summary>
        public string Line
        {
            get { return this.line; }
            set { this.line = value; }
        }
        /// <summary>
        ///  The Log type of this line
        /// </summary>
        public LogType Log_Type
        {
            get { return this.logType; }
            set { this.logType = value; }
        }
        private string line;
        private LogType logType;
    }
    #endregion
	/// <summary>
	/// Control saving all messages in the system to Log Files
	/// </summary>
	public class Log
	{
        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Constractor for Class Log 
        /// </summary>
        public Log(string AFileName, string debug_command, string logDir)
        {
            string[] MyMonthNames = new string[12]{"January","February","March","April","May","June",
                                                   "July","August","September","October","November","December"};

            this.LogDir = logDir;
            LogLines = new ArrayList();
            IniFile iniFile;
            addLog = false;
            string temp;
            iniFile = new IniFile(Layers_Handler.instance().RunDir + @"\setup.ini");
            temp = iniFile.ReadString("General", "Add Logs", "");
            if (temp == "")
            {
                iniFile.WriteString("General", "Add Logs", "1");
            }
            if (temp == "1")
                addLog = true;
            if (iniFile.ReadString("General", "LogsDirectory", "") == "")
            {
                iniFile.WriteString("General", "LogsDirectory", Application.StartupPath + "\\logs");
            }

            Layers_Handler.instance().LogsDirectory = iniFile.ReadString("General", "LogsDirectory", "");

            DateTime date = DateTime.Now;
             string dd = date.Day.ToString(), mm = date.Month.ToString();
            string hh = date.Hour.ToString(), mmn = date.Minute.ToString(), unit_file_nane;
            if (dd.Length == 1)
                dd = "0" + dd;
            if (mm.Length == 1)
                mm = "0" + mm;
            if (hh.Length == 1)
                hh = "0" + hh;
            if (mmn.Length == 1)
                mmn = "0" + mmn;
            Layers_Handler.instance().LogsDirectory += @"\" + date.Year.ToString() + @"\" + MyMonthNames[date.Month - 1] + @"\" + dd + "_" + mm + @"_" + date.Year.ToString() + "_" + hh + mmn;

            filename = Layers_Handler.instance().LogsDirectory + @"\" + dd + "_" + mm + @"_" + date.Year.ToString() + "_" + hh + mmn;

            DirectoryInfo dir = Directory.CreateDirectory(Layers_Handler.instance().LogsDirectory);

            FFileName = filename + @".log";
        }		
        #endregion

		#region Distractor
		// DISTRACTOR
		// ------
		/// <summary>
		/// Distractor for Class Log 
		/// </summary>
        public void Dispose()
        {
			try
			{
              //  LogEvent = null;
				LogOutput.Close();
				
				debugFile.Close();
				GC.SuppressFinalize(this);
			}
			catch(Exception){;}
        }
        ~ Log()
		{
            Dispose();
        }
		#endregion

		#region Methods
		// METHODS
		// ----------
		#region Add Log Line
		/// <summary>
		/// AddLine for Log file string S,LogType logType,string Prefix
		/// </summary>
		/// <param name="sender">The sender of this line</param>
		/// <param name="Message">The line to write</param>
		/// <param name="logType">The type of color</param>
		/// <param name="Prefix">The perfix to add to line</param>
		/// <returns>The log line as was printed on the file</returns>
		public string AddLine(object sender, string Message, LogType logType, string Prefix)
		{
            string[] fLogLine = new string[] { Message };
            return this.AddLine(sender, fLogLine, logType, Prefix);
		}
		#endregion

        #region Add Log Lines
        /// <summary>
        /// AddLine for Log file string S,LogType logType,string Prefix
        /// </summary>
        /// <param name="sender">The sender of this line</param>
        /// <param name="Message">The lines to write</param>
        /// <param name="logType">The type of color</param>
        /// <param name="Prefix">The perfix to add to line</param>
        /// <returns>The time of this log Line</returns>
        public string AddLine(object sender, string[] Messages, LogType logType, string Prefix)
        {
            if (!addLog)
                return "";
            string[] fLogLine = new string[Messages.Length];
            string prefix = "";
            string time = DateTime.Now.ToString();
            string results = "";
            try
            {
                lock (this.lockLog)
                {

                    busy = true;
                    if (Prefix == null)
                        prefix = "";
                    else
                        prefix = Prefix;
                    if ((prefix == "Game") || (prefix == "Debug") || (prefix == "CEC") || (prefix.ToLower() == "oven"))
                        for (int i = 0; i < Messages.Length; i++)
                        {
                            fLogLine[i] = time + " " + Messages[i];
                            results = time + " ";
                        }
                    else
                        if (prefix != "")
                        {
                            results = time + " " + prefix + " ";
                            for (int i = 0; i < Messages.Length; i++)
                                fLogLine[i] = time + " " + prefix + " " + Messages[i];
                        }
                        else
                            if (FLinePrefix != "")
                            {
                                results = time + " " + FLinePrefix + " ";
                                for (int i = 0; i < Messages.Length; i++)
                                    fLogLine[i] = time + " " + FLinePrefix + " " + Messages[i];
                            }
                            else
                            {
                                results = time + " ";
                                for (int i = 0; i < Messages.Length; i++)
                                    fLogLine[i] = time + " " + Messages[i];
                            }

                    LogLine logLine = new LogLine(sender, fLogLine, logType, prefix);
                    this.managLogLine(logLine);
                }

            }
            catch (Exception ex)
            {
                Layers_Handler.instance().sendMessageBox("Problem in file Log.cs method AddLine ", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                busy = false;
            }
            return results;
        }
        #endregion

        #region Add Log Lines
        /// <summary>
        /// AddLine for Log file string S,LogType logType,string Prefix
        /// </summary>
        /// <param name="sender">The sender of this line</param>
        /// <param name="Message">The lines to write</param>
        /// <param name="logType">The type of color</param>
        /// <param name="Prefix">The perfix to add to line</param>
        /// <returns>The log line as was printed on the file</returns>
        public string AddLine(object sender, List<string> Messages, LogType logType, string Prefix)
        {
            if (!addLog)
                return "";
            string[] fLogLine = new string[Messages.Count];
            Messages.CopyTo(fLogLine);
            return this.AddLine(sender, fLogLine, logType, Prefix);

        }
        #endregion

        #region Add Log Lines
        public string AddLine(object sender, List<LogLineData> Messages, string Prefix)
        {
            if (!addLog)
                return "";
            if (Messages == null)
                return "";
            string prefix;
            string time = DateTime.Now.ToString();
            string results = "";
            try
            {
                lock (this.lockLog)
                {

                    busy = true;
                    if (Prefix == null)
                        prefix = "";
                    else
                        prefix = Prefix;
                    if ((prefix == "PQMreset") || (prefix == "Debug") || (prefix == "CEC") || (prefix.ToLower() == "oven"))
                        for (int i = 0; i < Messages.Count; i++)
                        {
                            Messages[i].Line = time + " " + Messages[i].Line;
                            results = time + " ";
                        }
                    else
                        if (prefix != "")
                        {
                            results = time + " " + prefix + " ";
                            for (int i = 0; i < Messages.Count; i++)
                                Messages[i].Line = time + " " + prefix + " " + Messages[i].Line;
                        }
                        else
                            if (FLinePrefix != "")
                            {
                                results = time + " " + FLinePrefix + " ";
                                for (int i = 0; i < Messages.Count; i++)
                                    Messages[i].Line = time + " " + FLinePrefix + " " + Messages[i].Line;
                            }
                            else
                            {
                                results = time + " ";
                                for (int i = 0; i < Messages.Count; i++)
                                    Messages[i].Line = time + " " + Messages[i].Line;
                            }

                    LogLine logLine = new LogLine(sender, Messages, prefix);
                    this.managLogLine(logLine);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Thread was being aborted") > -1)
                    throw new Exception(ex.Message + "\nLog Throw in AddLine", ex.InnerException);
                else
                    Layers_Handler.instance().sendMessageBox("Problem in file Log.cs method AddLine ", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error, this.ToString());
            }
            finally
            {
                busy = false;
            }
            return results;
        }
        #endregion

		#region manag Log Line
		private void managLogLine(LogLine logLine)
		{
            if (!addLog)
                return ;
            try
			{
				this.FLogType = logLine.logType;
				this.FPrefix = logLine.Prefix;
				this.FLogLines = logLine.Messages;
                this.logLinesData = logLine.logLinesData;
                lock (this)
                {
                    write();
                }
			}
			catch (Exception ex)
			{
				Layers_Handler.instance().sendMessageBox("Problem in file Log method managLogLine ",ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion

        #region WriteLogsToFile
        private void WriteLogsToFile()
        {
            addLoginprogress = true;
            LogLine logLine = null;
            try
            {
                while (LogLines.Count > 0)
                {
                    logLine = (LogLine)LogLines[0];
                    LogLines.RemoveAt(0);
                    if (logLine != null)
                        managLogLine(logLine);
                }
            }
            catch (Exception ex)
            {
                Layers_Handler.instance().sendMessageBox("Problem in file Log.cs method WriteLogsToFile ", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                addLoginprogress = false;
            }
        }
        #endregion

        #region write
        private void write()
        {
            if (!addLog)
                return ;
            string num;
            int index = 0;
            string[] lines;
            string message = "Log Sender : " + this.FPrefix + "\nLog Data : ";
            DateTime stoptime = DateTime.Now.AddMinutes(1);
            try
            {
                if (log_files_are_moving)
                {
                    while ((stoptime.Ticks > DateTime.Now.Ticks) && (!Layers_Handler.instance().StopRun))
                    {
                        Thread.Sleep(1000);
                        if (stoptime.Ticks < DateTime.Now.Ticks)
                            break;
                    }
                }
                log_files_are_moving = false;
                FFileName = filename + @".log";
                if (!File.Exists(FFileName))
                    LogOutput = File.CreateText(FFileName);
                else
                {
                    for (int k = 0; k < 3; k++)
                    {
                        try
                        {
                            LogOutput = File.AppendText(FFileName);
                            break;
                        }
                        catch(Exception ex)
                        {
                            if (ex.Message.Contains("The process cannot access the file"))
                            {
                                Thread.Sleep(5000);
                            }
                            else
                            {
                                throw ex;
                            }

                        }
                    }
                }
                if (this.logLinesData != null)
                {
                    for (int k = 0; k < logLinesData.Count; k++)
                    {
                        LogOutput.WriteLine(logLinesData[k].Line);
                        message += "\n" + logLinesData[k].Line;
                    }
                }
                else
                {
                    for (int k = 0; k < FLogLines.Length; k++)
                    {
                        LogOutput.WriteLine(FLogLines[k]);
                        message += "\n" + FLogLines[k];
                    }
                }
                LogOutput.Close();
                //add line just if there is more then one encoder in the run set
               // if (Layers_Handler.instance().NumOfIRPsInThisRunSet > 1)
                {
                    //Find if this line of encoder
                    if ((this.FPrefix != null) && (this.FPrefix != "") && ((this.FPrefix.ToLower().IndexOf("game #") > -1)))
                    {
                        try
                        {
                            lines = this.FPrefix.Split(new string[] { "#", " " }, StringSplitOptions.RemoveEmptyEntries);
                            //if (this.FPrefix.IndexOf("Front End #") > -1)
                            //    num = this.FPrefix.Substring(this.FPrefix.IndexOf("#") + 1, this.FPrefix.IndexOf("Front End #") - this.FPrefix.IndexOf("#") - 1);
                            //else
                            //    num = this.FPrefix.Substring(this.FPrefix.IndexOf("#") + 1, this.FPrefix.Length - this.FPrefix.IndexOf("#") - 1);
                            if (lines.Length > 1)
                            {
                                num = lines[1].Trim();
                                index = Convert.ToInt32(num);
                            }
                        }

                        catch { index = 0; }
                    }
                    if ((this.FPrefix.ToLower().IndexOf("game #") > -1) && (index == 0))
                        System.Diagnostics.Trace.WriteLine("Log game and index = 0");
                }
            }
            catch (Exception ex)
            {

                busy = false;
                if (ex.Message.IndexOf("Thread was being aborted") > -1)
                {
                    EventLog MyLog = new EventLog();
                    MyLog.Source = "Munters Controller";
                    message += "Error Message : " + ex.Message + "\nExeption Message : " + ex.Message + "\nExeption StackTrace : " + ex.StackTrace;

                    MyLog.WriteEntry(message);

                    //    throw new Exception(ex.Message +"\nTrace : "+ex.StackTrace+"\nLog Throw in write", ex.InnerException);
                }
                else
                    Layers_Handler.instance().sendMessageBox("Problem in file Log method write", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error, this.ToString());
                LogOutput.Close();
            }
            

            busy = false;
        }
        #endregion



        #region Event Raising when Log Line added
        //        //step 3 : The event definition
        //        //------  
        //        /// <summary>
        //        /// Event for call Back method for Log Line added
        //        /// </summary>
        //        private event LogEventHandler LogEvent;
        //        //step 5 : Fire the event, generating recievers callback methods activation.
        //-------
        public string  OnLogEvent(object sender ,LogEventArgs e)
        {
            //return this.AddLine(sender, e.Messages, e.LogType, e.Prefix);
            if (e.LogLinesData != null)
            {
                return this.AddLine(sender, e.LogLinesData, e.Prefix);
            }
            else
                return this.AddLine(sender, e.Messages, e.LogType, e.Prefix);
        }
		#endregion

        #region Add Error Log Line
        /// <summary>
        /// AddLine for Log file string S,LogType logType,string Prefix
        /// </summary>
        /// <param name="S">The line to write</param>
        /// <param name="logType">The type of color</param>
        /// <param name="Prefix">The perfix to add to line</param>
        /// <returns>The log line as was printed on the file</returns>
        public string AddErrorLine(object sender, string Message, LogType logType, string Prefix)
        {
            bool temp = startdebug;
            string FLogLine = "";
            try
            {
                while (busy)
                    Thread.Sleep(1);
                FLogLine = AddLine(sender, Message, logType, Prefix);
                startdebug = temp;
            }
            catch (Exception ex)
            {
                Layers_Handler.instance().sendMessageBox("Problem in file Log method AddLine", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error, this.ToString());
                startdebug = temp;
            }
            return FLogLine;
        }
        #endregion
        #endregion

		#region Properties
		// PROPERTIES
		// ----------
		public string debugfilename
		{
			get{return Fdebugfilename;}
		}
		public string LinePrefix
		{
			get{return FLinePrefix;}
			set	{FLinePrefix = value;}
		}
		public LogType ShowTypes
		{
			get{return FShowTypes;}
			set	{FShowTypes = value;}
		}
		public string Debug
		{
			get{return FDebug;}
			set	{FDebug = value;}
		}
		public string FileName
		{
			get{return FFileName;}
		}
		public bool Busy
		{
			get{return busy;}
			set	{busy = value;}
		}
        /// <summary>
        /// The start of the name of each irp log file name
        /// </summary>
        public string LogsFileName
        {
            get { return this.filename; }
            set { this.filename = value ; }
        }
        /// <summary>
        /// Hold The Date of the start of the test
        /// </summary>
        public DateTime Start_date
        {
            get { return this.date; }
        }

        public bool AddLog
        {
            get { return this.addLog; }
            set { this.addLog = value; }
        }
        #endregion

        #region Fields
        // FIELDS
        // ------
        //delegate
        private delegate void AddLigLineEventHandler(object sender,LogEventArgs args);
        //Event
//		event AddLigLineEventHandler AddLigLineEvent;
//		private const int MAX_RICHEDIT_LINES = 5000;
		private bool busy=false;
//		private RichTextBox FLog;
//		private TextWriter FFile;
        private bool startdebug = false;
		private string FFileName;
		private LogType FShowTypes;
		private string FLinePrefix;
		private TextWriter debugFile;
		private string Fdebugfilename;
		//private string FLogLine;
        private string[] FLogLines;
        private List<LogLineData> logLinesData = new List<LogLineData>();
		private LogType FLogType;
		private string FPrefix;
		private string FDebug ;
		private TextWriter LogOutput;
        private bool log_files_are_moving = false;
        private string LogDir;
        private string filename;
		private ArrayList LogLines;
		/// <summary>
		/// Indication that the process of adding the Trap lines is active
		/// </summary>
		private bool addtrapinprogress = false;
		/// <summary>
		/// Indication that the process of adding the Log lines is active
		/// </summary>
		private bool addLoginprogress = false;
        private bool addLog = false;
        private object lockTrap = new object();
        private object lockLog = new object();
        private DateTime date;
		#region Class That hold line data
		/// <summary>
		/// Class That hold line data
		/// </summary>
        private class LogLine
        {
            /// <summary>
            /// Class That hold line data
            /// </summary>
            /// <param name="sender">The sender of this line</param>
            /// <param name="Message">The lines to write</param>
            /// <param name="logType">The type of color</param>
            /// <param name="Prefix"> The perfix to add to line</param>
            public LogLine(object sender, string[] Messages, LogType logType, string Prefix)
            {
                this.sender = sender;
                this.Prefix = Prefix;
                this.Messages = Messages;
                this.logType = logType;
            }
            public LogLine(object sender, List<LogLineData> logLinesData, string Prefix)
            {
                this.sender = sender;
                this.Prefix = Prefix;
                this.logLinesData = logLinesData;
            }
            /// <summary>
            /// The sender of this line
            /// </summary>
            public object sender;
            /// <summary>
            /// The line to write
            /// </summary>
            public string[] Messages = new string[] { "" };
            /// <summary>
            /// The type of color
            /// </summary>
            public LogType logType;
            /// <summary>
            /// The perfix to add to line
            /// </summary>
            public string Prefix = "";
            /// <summary>
            /// List of lines with there type of color
            /// </summary>
            public List<LogLineData> logLinesData = null;
        }
		#endregion
		#endregion

   }
}

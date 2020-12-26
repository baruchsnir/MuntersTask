//#define PhantomJs
#define disableCS_new
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using FT.CommonDll.Ini;
using FT.CommonDll;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium.Firefox;
using Excel = Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;
using Workbook = Microsoft.Office.Interop.Excel.Workbook;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Configuration;
using OpenQA.Selenium.PhantomJS;
using System.Runtime.InteropServices;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace FT.CommonDll
{

    public enum TRunState
    { rsNoRunSet = 0, rsRunSetSelected, rsSetRunning, rsSetStopping }
    public enum Tester_Station_ID
    { ATP_1 = 1, ATP_2 = 2, ATP_3 = 3, ATP_4 = 4, ATP_5 = 5, ATP_6 = 6, ATP_7 = 7, SW_download = 8 }

    /// <summary>
    /// Static object for Singlton Layers Handler class.
    /// Hold data to pass between the layares
    /// </summary>
    [Serializable]
    public class Layers_Handler
    {

        #region Constractor
        // CONSTRACTOR
        // ------
        private Layers_Handler()
        {
        }
        #endregion

        #region Destractor
        // DESTRACTOR
        // ------
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            try
            {
                srviceHandler = null;
                GC.SuppressFinalize(this);
            }
            catch (Exception) {; }


        }
        #endregion

        #region Methods
        // METHODS
        // ----------
        #region Load Setup
        /// <summary>
        /// Loads the setup from ini file
        /// </summary>
        public void LoadSetup(string base_dir)
        {
            UTools.Configure_Path = base_dir;
            IniFile iniFile;
            iniFile = new IniFile(Layers_Handler.instance().RunDir + @"\" + SETUP_FILE);
            string temp = UTools.Configure_Path+ @"\Logs";
            logsDirectory = temp;
            iniFile.WriteString("General", "LogsDirectory", logsDirectory);
            if (!Directory.Exists(logsDirectory))
                Directory.CreateDirectory(logsDirectory);
        }
        #endregion

        #region sendMessageBox
        /// <summary>
        /// Send Message box Message to all forms to show the problem
        /// </summary>
        /// <param name="message">The messag to show</param>
        /// <param name="ex">The Exception that was made if thie is error</param>
        /// <param name="capture">The capture of the message box</param>
        /// <param name="buttons">The buttons that will be on the message box</param>
        /// <param name="icon">The type of the icon like error or information</param>
        public void sendMessageBox(string message, Exception ex, string capture, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            try
            {
                if (ex != null)
                    Layers_Handler.instance().Test = true;
                if ((ex != null) &&(this.Flog != null))
                     this.Flog.AddLine(this, message+"\n"+ex.Message+"\n"+ex.StackTrace, LogType.ltSuccess, "CEC");
            }
            catch {; }
            MessageBoxEventsargs args = new MessageBoxEventsargs(message, ex, capture, buttons, icon);
            this.OnMessageBoxEventArrived(this, args);

        }
        /// <summary>
        /// Send Message box Message to all forms to show the problem
        /// and add log line
        /// </summary>
        /// <param name="message">The messag to show</param>
        /// <param name="capture">The capture of the message box</param>
        /// <param name="buttons">The buttons that will be on the message box</param>
        /// <param name="icon">The type of the icon like error or information</param>
        public void sendMessageBox(string message, Exception ex, string capture, MessageBoxButtons buttons, MessageBoxIcon icon, object sender)
        {

            try
            {
                if (ex != null)
                    Layers_Handler.instance().Test = true;
                if ((Flog != null)&&(ex != null))
                          this.Flog.AddErrorLine(this, message + "\n" + ex.Message + "\n" + ex.StackTrace + "\n From Sender : " + sender.ToString(), LogType.ltSuccess, "CEC");
               
            }
            catch (Exception exs)
            {
                Console.WriteLine(exs.Message);
                Console.WriteLine(exs.StackTrace);
            }
            if ((stopRun) && (ex != null)) return;
            MessageBoxEventsargs args = new MessageBoxEventsargs(message, ex, capture, buttons, icon);
            this.OnMessageBoxEventArrived(sender, args);
        }
        #endregion

        #region sendSmallMessageBox
        public void sendSmallMessageBox(string message, string capture, MessageBoxButtons buttons, MessageBoxIcon icon)
        {

            try
            {
                /*      if (Flog != null)
                          this.Flog.AddErrorLine(this, message, LogType.ltSuccess, "CEC");
                          */
            }
            catch (Exception exs)
            {
                Console.WriteLine(exs.Message);
                Console.WriteLine(exs.StackTrace);
            }
            MessageBoxEventsargs args = new MessageBoxEventsargs(message, null, capture, buttons, icon);
            this.OnMessageBoxEventArrived(this, args);
        }

        #endregion

        #region Thread Sleep

        /// <summary>
        /// The thread sleeps for the given time or until Terminated
        /// </summary>
        /// <param name="Seconds">Time to sleep in seconds</param>
        public void ThreadSleep(int Seconds)
        {
            try
            {
                DateTime FOff_Time_ToStop = DateTime.Now;
                FOff_Time_ToStop = FOff_Time_ToStop.AddSeconds(Seconds);
                while ((FOff_Time_ToStop.Ticks > DateTime.Now.Ticks))
                {
                    if (stopRun)
                        return;
                    Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                Layers_Handler.instance().sendMessageBox("Problem in file " + this.ToString() + " method ThreadSleep \n", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error, this.ToString());
            }
        }
        #endregion

        #region FLog Add Line
        /// <summary>
        /// Path the line to log file
        /// </summary>
        /// <param name="line">The test to write</param>
        /// <param name="logType">the type - like error or general</param>
        /// <param name="Prefix">The perfix to add to line</param>
        public void FLogAddLine(string line, LogType logType, string Prefix)
        {

              LogEventArgs args = new LogEventArgs(line, logType, Prefix);
              TrigerAddLineToLogEvent(this, args);
        }
        #endregion



        #region GetProcess_ID
        /// <summary>
        /// Get Process ID by name
        /// </summary>
        public int GetProcess_ID(string name,int ix)
        {
            try
            {

                if ((name.ToLower() == "phantomjs") || (name.ToLower() == "firefox"))
                {
                   if (this.phantomjs_id.ContainsKey(ix))
                    {
                        if (this.phantomjs_id[ix] != 0)
                            return this.phantomjs_id[ix];
                    }
                }
                Process[] aProcesses = Process.GetProcesses(Environment.MachineName);

                // Get the appSettings.
                NameValueCollection appSettings = ConfigurationManager.AppSettings;

                // Get the collection enumerator.
                IEnumerator appSettingsEnum = appSettings.Keys.GetEnumerator();

                // Loop through the collection and
                // display the appSettings key, value pairs.
                int i = 0;
                Console.WriteLine("App settings.");
                string s1 = "";
                bool find_one;
                while (appSettingsEnum.MoveNext())
                {
                    string key = appSettings.Keys[i];
                    Console.WriteLine("Name: {0} Value: {1}",
                                      key, appSettings[key]);
                    s1 = string.Format("Name: {0} Value: {1}",
                                      key, appSettings[key]);
                    i += 1;
                }

                string strRemark = "";
                foreach (Process p in aProcesses)
                {
                    if (p.Id == System.Diagnostics.Process.GetCurrentProcess().Id)
                        // the process id is unique in the system
                        strRemark = " < = my application";
                    else
                        strRemark = "";
                    Console.WriteLine(strRemark + " id - " + p.Id);
                    if (p.Id != Process.GetCurrentProcess().Id)
                    {
                        if (p.ProcessName.ToLower().IndexOf(name) == 0)
                        {
                            if ((name.ToLower() == "phantomjs") || (name.ToLower() == "firefox"))
                            {
                                find_one = false;
                                foreach (int key in this.phantomjs_id.Keys)
                                {
                                    if (key != ix)
                                    {
                                        if (this.phantomjs_id[key] != 0)
                                        {
                                            if (this.phantomjs_id[key] == p.Id)
                                            {
                                                find_one = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (find_one)
                                    continue;
                                if (this.phantomjs_id.ContainsKey(ix))
                                {
                                    if (this.phantomjs_id[ix] == 0)
                                       this.phantomjs_id[ix] = p.Id;
                                    else
                                    {
                                        if (this.phantomjs_id[ix] != p.Id)
                                        {
                                            this.phantomjs_id[ix] = p.Id;
                                        }
                                    }
                                }
                                else
                                {
                                    this.phantomjs_id[ix] = p.Id;
                                }
                            }
                            return p.Id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Layers_Handler.instance().sendMessageBox("Problem in  " + this.ToString() + " in KillExcelProcess", ex, "Error in ToTo Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 0;
        }
        #endregion

        #region HideProcess
        /// <summary>
        /// HideProcess
        /// </summary>
        public void HideProcess(string name)
        {
            try
            {

                Process[] aProcesses = Process.GetProcesses(Environment.MachineName);

                // Get the appSettings.
                NameValueCollection appSettings = ConfigurationManager.AppSettings;

                // Get the collection enumerator.
                IEnumerator appSettingsEnum = appSettings.Keys.GetEnumerator();

                // Loop through the collection and
                // display the appSettings key, value pairs.
                int i = 0;
                Console.WriteLine("App settings.");
                string s1 = "";
                while (appSettingsEnum.MoveNext())
                {
                    string key = appSettings.Keys[i];
                    Console.WriteLine("Name: {0} Value: {1}",
                                      key, appSettings[key]);
                    s1 = string.Format("Name: {0} Value: {1}",
                                      key, appSettings[key]);
                    i += 1;
                }

                string strRemark = "";
                foreach (Process p in aProcesses)
                {
                    if (p.Id == System.Diagnostics.Process.GetCurrentProcess().Id)
                        // the process id is unique in the system
                        strRemark = " < = my application";
                    else
                        strRemark = "";
                    if (p.Id != Process.GetCurrentProcess().Id)
                    {
                        if (p.ProcessName.ToLower().IndexOf(name) == 0)
                        {
                            try
                            {
                                ShowWindowAsync(p.MainWindowHandle, SW_HIDE);

                            }
                            catch {; }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Layers_Handler.instance().sendMessageBox("Problem in  " + this.ToString() + " in KillExcelProcess", ex, "Error in ToTo Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Get_Driver

        /// <summary>
        /// Get Web Driver
        /// </summary>
        /// <param name="new_driver">Indication to new one</param>
        /// <returns></returns>
        public RemoteWebDriver Get_Driver(bool new_driver, string url, bool quit)
        {
            bool Firefox_Driver = false;
            if (new_driver)
            {
                if (driver != null)
                {
                    try
                    {
                        if (quit)
                            driver.Quit();
                    }
                    catch {; }
                    try
                    {
                        if (quit)
                            driver.Dispose();
                    }
                    catch {; }
                    driver = null;
                }
            }


            ChromeDriver driver_c = new ChromeDriver();
            driver = (RemoteWebDriver)driver_c;
            HideProcess("chromedriver");
            driver.Navigate().GoToUrl(url);
            return driver;
        }
        #endregion

        #region Close_Driver
        /// <summary>
        /// Close Web Driver
        /// </summary>
        /// <param name="index">The index of Driver 0 - 13</param>
        /// <returns></returns>
        public void Close_Driver()
        {
            if (driver != null)
            {
                try
                {
                    driver.Quit();
                }
                catch {; }
                try
                {
                    driver.Dispose();
                }
                catch {; }
                driver = null;
            }
        }
        #endregion

        #region WaitForPageToLoad
        public bool WaitForPageToLoad(IWebDriver driver)
        {
            try
            {
                TimeSpan timeout = new TimeSpan(0, 0, 30);
                WebDriverWait wait = new WebDriverWait(driver, timeout);

                IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
                if (javascript == null)
                    throw new ArgumentException("driver", "Driver must support javascript execution");
                string readyState = "";
                wait.Until((d) =>
                {
                    Thread.Sleep(4000);
                    try
                    {
                        readyState = javascript.ExecuteScript(
                        "if (document.readyState) return document.readyState;").ToString();
                        return readyState.ToLower() == "complete";
                    }
                    catch (InvalidOperationException e)
                    {
                        //Window is no longer available
                        return e.Message.ToLower().Contains("unable to get browser");
                    }
                    catch (WebDriverException e)
                    {
                        //Browser is no longer available
                        return e.Message.ToLower().Contains("unable to connect");
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
                return (readyState.ToLower() == "complete");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ThrowTimeoutException"))
                {

                    return false;
                }
            }
            return false;
        }
        #endregion

        [DllImport("user32.dll")]
        private static extern
        bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        #endregion

        #region Properties
        // PROPERTIES
        // ---------
        public static Layers_Handler instance()
        {
            if (srviceHandler == null)
                srviceHandler = new Layers_Handler();
            return srviceHandler;
        }
        public string LogsDirectory
        {
            get { return logsDirectory; }
            set { logsDirectory = value; }
        }
        public string LogDir
        {
            set { this.logDir = value; }
            get { return this.logDir; }
        }
        public bool StopRun
        {
            get { return stopRun; }
            set { stopRun = value; }
        }
        public string RunDir
        {
            get {
                if (UTools.Configure_Path == "")
                    return  Application.StartupPath;
                else
                    return UTools.Configure_Path;
            }
        }
        public string SETUP_FILE
        {
            get { return setupini; }
        }


        public RemoteWebDriver Driver
        {
            get
            {
                if (driver == null)
                {
                    ChromeDriver driver_c = new ChromeDriver();
                    driver = (RemoteWebDriver)driver_c;
                    driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 240));
                }
                return driver;
            }
            set { driver = value; }
        }
        //public RemoteWebDriver[] Driver_Arr
        //{
        //    get
        //    {
        //        // if (driver == null)
        //        // {
        //        //     driver = new FirefoxDriver();
        //        //      driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 240));
        //        //  }
        //        return driver_Arr;
        //    }
        //    set { driver_Arr = value; }
        //}
        /// <summary>
        /// Hold All Excel Work Books
        /// </summary>
        public Dictionary<string, string> Games_Workbooks
        {
            get { return games_Workbooks; }
            set { games_Workbooks = value; }
        }
        /// <summary>
        /// Get all Games from excel workshit
        /// </summary>
        public bool Get_games_from_excel
        {
            get { return get_games_from_excel; }
            set { get_games_from_excel = value; }
        }
        /// <summary>
        /// Hold The Id handle of the Phantom JS
        /// </summary>
        public Dictionary<int, int> Phantomjs_id
        {
            get { return phantomjs_id; }
            set { phantomjs_id = value; }
        }
        public Log LOG
        {
            set { this.Flog = value; }
            get { return this.Flog; }
        }

        public bool Run { get => run; set => run = value; }
        public bool Test
        {
            set { this.test = value;
                if (value)
                    this.run = true;
            }
            get { return this.test; }
        }
        #endregion

        #region Fields
        // FIELDS
        // ------

        private const int SW_HIDE = 0;
        private static Layers_Handler srviceHandler;

        private string logsDirectory = @"c:\Pintresrt_Bot";
        private bool stopRun = false;
        private string logDir = Application.StartupPath+@"\Logs";
        private string setupini = "setup.ini";
        private RemoteWebDriver driver = null;
        private Dictionary<string, string> games_Workbooks = new Dictionary<string, string>();
        private bool get_games_from_excel = false;
        private Dictionary<int, int> phantomjs_id = new Dictionary<int, int>();
        private bool run = false;
        private bool test = false;
        private Log Flog;
        #endregion

        #region Events
        #region Event Raising Message Box Even
        // step 2: Delegate type defining the prototype of the callback method
        public delegate void MessageBoxEventsHandler(object sender, MessageBoxEventsargs args);
        //step 3 : The event definition
        //------
        /// <summary>
        /// event to Hold the delegates 
        /// </summary>
        public event MessageBoxEventsHandler MessageBoxEven;
        //step 4 : Trigger the event
        //-------
        /// <summary>
        /// Send The message to main form
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">The message params</param>
        public void TrigerMessageBoxEvents(object sender, MessageBoxEventsargs e)
        {
            this.OnMessageBoxEventArrived(sender, e);
        }
        //step 5 : Fire the event, generating recievers callback methods activation.
        //-------
        /// <summary>
        /// Protected method Send The message to main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnMessageBoxEventArrived(object sender, MessageBoxEventsargs e)
        {
            if (MessageBoxEven != null)
                MessageBoxEven(sender, e);
        }

        #endregion

        #region Event Raising Bests Even
        //step 3 : The event definition
        //------
        /// <summary>
        /// event to Hold the delegates 
        /// </summary>
        public event BetsEventHandler BetsEvent;
        //step 4 : Trigger the event
        //-------
        /// <summary>
        /// Send The message to main form
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">The message params</param>
        public void TrigerBetsEvents(object sender, BetsEventArgs e)
        {
            this.OnBetsEventArrived(sender, e);
        }
        //step 5 : Fire the event, generating recievers callback methods activation.
        //-------
        /// <summary>
        /// Protected method Send The message to main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnBetsEventArrived(object sender, BetsEventArgs e)
        {
            if (BetsEvent != null)
                BetsEvent(sender, e);
        }

        #endregion

        #region Event Raising when Log Line added
        // step 2: Delegate type defining the prototype of the callback method
        // ------  that receivers must implement
        /// <summary>
        /// delegate for call Back method for Log Line added
        /// </summary>
        public delegate void LogEventHandler(Object sender, LogEventArgs args);

        //step 3 : The event definition
        //------  
        /// <summary>
        /// Event for call Back method for Log Line added
        /// </summary>
        public event LogEventHandler LogEvent;
        /// <summary>
        /// Event for call Back method for add Log Line added to display
        /// </summary>
        public event LogEventHandler DisplayLogEvent;
        //step 4 : Trigger the event for adding line to file
        //-------
        /// <summary>
        /// Triger The Event for call Back method for Log Line added
        /// </summary>
        /// <param name="e"></param>
        public void TrigerAddLineToLogEvent(object sender, LogEventArgs e)
        {
            this.OnMsgArrived(sender, e);
        }
        //step 5 : Fire the event, generating recievers callback methods activation.
        //-------
        protected virtual void OnMsgArrived(object sender, LogEventArgs e)
        {
            if (Flog != null)
            {
                string s;
                do
                {
                    Thread.Sleep(100);
                }
                while (Flog.Busy);
                s = Flog.OnLogEvent(sender, e);
                if (s != "")
                {
                    if (e.LogLinesData != null)
                    {
                        //for (int i = 0; i < e.LogLinesData.Count; i++)
                        //    e.LogLinesData[i].Line = s + e.LogLinesData[i].Line;
                    }
                    else
                    {
                        for (int i = 0; i < e.Messages.Length; i++)
                            e.Messages[i] = s + e.Messages[i];
                    }

                }
                if (e.LogType != LogType.ltSuccess)
                    if (LogEvent != null)
                        LogEvent(this, e);
            }

        }


        //step 4 : Trigger the event
        //-------
        /// <summary>
        /// Triger The Event for call Back method for Log Line added
        /// </summary>
        /// <param name="e"></param>
        public void TrigerAddLineToDisplayEvent(object sender, LogEventArgs e)
        {
            this.OnDisplayLogEventArrived(sender, e);
        }
        //step 5 : Fire the event, generating recievers callback methods activation.
        //-------
        protected virtual void OnDisplayLogEventArrived(object sender, LogEventArgs e)
        {
            if (DisplayLogEvent != null)
                DisplayLogEvent(this, e);
        }
        #endregion

        #region Event For Display Event for main Display Form
        // step 2: Delegate type defining the prototype of the callback method
        // ------  that receivers must implement
        public delegate void DisplayEventHandler(Object sender, DisplayEventArgs args);

        //step 3 : The event definition
        //------  
        public event DisplayEventHandler DisplayEvent;

        //step 4 : This method is called when a message arrives
        //------   It translates the input into the event
        public void SendDisplayEvent(object sender, DisplayEventArgs args)
        {
            // to pass to the receivers of our notification
            OnDisplayEventArrived(sender, args);
        }
        //step 5 : Fire the event, generating recievers callback methods activation.
        //-------
        protected virtual void OnDisplayEventArrived(object sender, DisplayEventArgs e)
        {
            if (DisplayEvent != null)
                DisplayEvent(sender, e);
        }
        #endregion
        #endregion
    }
}

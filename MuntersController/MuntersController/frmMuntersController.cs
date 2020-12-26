using FT.CommonDll;
using FT.CommonDll.Ini;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using EventLogWriter;

namespace MuntersController
{
    public partial class frmMuntersController : Form
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        public frmMuntersController(string[] args)
        {
            this.args = args;
            InitializeComponent();
            UTools.Configure_Path = System.Windows.Forms.Application.StartupPath;
            if (args.Length > 0)
            {

                for (int i = 0; i < args.Length; i++)
                {
                    if (i <= (args.Length - 1))
                    {
                        if (args[i].Trim().ToLower() == "-p")
                        {
                            if (Directory.Exists(args[i + 1]))
                            {
                                UTools.Configure_Path = args[i + 1];
                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Methods
        // METHODS
        // ----------
        #region frmSendBets_Load
        private void frmSendBets_Load(object sender, EventArgs e)
        {
            InstallerGet_EvenLog inst = new InstallerGet_EvenLog();
            string temp;
            m_AddDisplayEventMessage = new AddDisplayEventMessage(this.UpdateDisplay);
            temp = Layers_Handler.instance().RunDir;
            Layers_Handler.instance().LoadSetup(temp);

            temp += @"\Setup.ini";
            ini = new IniFile(temp);
            Read_Data_From_ini_File();

            Layers_Handler.instance().MessageBoxEven += this.OnMessageBoxEvent;
            Layers_Handler.instance().DisplayEvent+= this.OnDisplayEvent;
            Layers_Handler.instance().LogEvent += this.OnChangLogLinesEvent;
        }
        #endregion

        #region Read_Data_From_ini_File
        private void Read_Data_From_ini_File()
        {
            txtUserName.Text = ini.ReadString("General", "User", "");
            txtPassword.Text = ini.ReadString("General", "Password", "");
        }
        #endregion

        #region btnLogin_Click
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Layers_Handler.instance().StopRun = false;
                ini.WriteString("General", "User", txtUserName.Text);
                ini.WriteString("General", "Password", txtPassword.Text);
                enable = false;
                Enable_Button();
                Thread td = new Thread(new ThreadStart(login));
                td.IsBackground = true;
                td.Name = "send login - " + DateTime.Now.ToString();
                td.Start();
            }
            catch (Exception ex)
            {
                Layers_Handler.instance().sendMessageBox("Problem in  " + this.ToString() + " in btnLogin_Click ", ex, "Error in Toto Game", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        #endregion

        #region login
        private void login()
        {
            tmrUpdateLogs.Enabled = true;

            try
            {
                if (!Directory.Exists(Layers_Handler.instance().LogDir))
                {
                    Directory.CreateDirectory(Layers_Handler.instance().LogDir);
                }
                mng = new ManageMuntersTest();
                mng.SetupTest();
                mng.TestCase_Login();
                mng.TestCase_GoToMenueTemperatureCurve();
                mng.TestCase_OpenTemperatureGurveGrid();
                mng.TestCase_FillTheGrid();
                mng.TestSave();
                mng.TeardownTest();
            }
            catch (Exception ex)
            {
                Layers_Handler.instance().sendMessageBox("Problem in  " + this.ToString() + " in login", ex, "Error in Pintrest_Bot", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                enable = true;
                MethodInvoker mi = new MethodInvoker(Enable_Button);
                this.BeginInvoke(mi);
            }
            tmrUpdateLogs.Enabled = false;
        }
        #endregion

        #region Enable Button 
        private void Enable_Button()
        {
            if (this.InvokeRequired)
            {
                MethodInvoker mi = new MethodInvoker(Enable_Button);
                this.BeginInvoke(mi);
                return;
            }
            btnLogin.Enabled = enable;
        }
        #endregion

        #region On recive message to open message box
        /// <summary>
        /// On recive message to open message box 
        /// </summary>
        /// <param name="sender">The object that send the message</param>
        /// <param name="args">The arguments Of the message Box</param>
        private void OnMessageBoxEvent(object sender, MessageBoxEventsargs args)
        {
            try
            {
                string line = "Message : " + args.Message + "\nExeption Message : " + args.ErrorExeption.Message + "\nExeption StackTrace : " + args.ErrorExeption.StackTrace;
                if (args.ErrorExeption != null)
                {
                    System.Diagnostics.Trace.WriteLine(line);
                    // Create the source, if it does not already exist.
                    if (!EventLog.SourceExists("Munters Controller"))
                    {
                        EventLog.CreateEventSource("Munters Controller", "Munters Controller");
                    }
                    MyLog = new EventLog();
                    MyLog.Source = "Munters Controller";
                    MyLog.WriteEntry(line);

                }
                else
                {
                    if (args.TypeIcon == MessageBoxIcon.Error)
                        MyLog.WriteEntry("Message : " + args.Message);
                }

            }
            catch {; }
            MessageBox.Show(args.Message, args.Capture, args.TypeButtons, args.TypeIcon);
        }
        #endregion

        #region OnDisplayEvent
        private void OnDisplayEvent(object sender, DisplayEventArgs args)
        {
            try
            {
                BeginInvoke(m_AddDisplayEventMessage, args);
            }
            catch (Exception ex)
            {
                Layers_Handler.instance().sendMessageBox("Problem in " + this.ToString()+ "  in OnChangeDisplayEvent", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error, sender);
            }
        }
        private void UpdateDisplay(DisplayEventArgs Displayargs)
        {
            try
            {

                if (Displayargs.Line != "")
                 this.toolStripBetsStatus.Text = Displayargs.Line;
                if (Displayargs.StartRunSetTest)
                    this.btnTestStatus.Visible = false;
                if (Displayargs.EndRunSetTest)
                {
                    this.btnTestStatus.Visible = true;
                    if (Layers_Handler.instance().Test)
                    {
                        this.btnTestStatus.Text = "Test Fail";
                        this.btnTestStatus.BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        this.btnTestStatus.Text = "Test Pass";
                        this.btnTestStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
                    }
                    PrintMainWindow.Print_Main_Window(this.Handle);
                }

            }
            catch (Exception ex)
            {
                Layers_Handler.instance().sendMessageBox("Problem in " + this.ToString()+ "  in UpdateDisplay", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error, this);
            }
        }
        #endregion

        #region UpdateBetsEvent
        /// <summary>
        /// On recive Update Bets Event 
        /// </summary>
        /// <param name="args">The arguments Of the message Box</param>
        private void UpdateBetsEvent(BetsEventArgs args)
        {
            try
            {
                if (args.Upupdate_txtRead)
                {
                    this.toolStripBetsStatus.Text = args.SiteName + " - " + args.GameName + " - " + args.File_Name;
                }
            }
            catch {; }
        }
        #endregion

        #region tmrUpdateLogs_Tick
        private void tmrUpdateLogs_Tick(object sender, EventArgs e)
        {
            tmrUpdateLogs.Enabled = false;
            BetsEventArgs arg;
            while (betsList.Count > 0)
            {
                arg = betsList[0];
                UpdateBetsEvent(arg);
                betsList.Remove(arg);
            }
            tmrUpdateLogs.Enabled = true;
        }
        #endregion


        #region On Chang Log Lines Event
        private void OnChangLogLinesEvent(object sender, LogEventArgs args)
        {
            string message = "Message : \n" + args.Messages + " \n" + args.Prefix;
            if (args.LogLinesData != null)
            {
                for (int k = 0; k < args.LogLinesData.Count; k++)
                    message += "\n" + args.LogLinesData[k].Line;
            }
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Layers_Handler.LogEventHandler(UpdateRedLogLines), new object[] { sender, args });
                }
                else
                    UpdateRedLogLines(sender, args);
            }
            catch (Exception ex)
            {

                EventLog MyLog = new EventLog();
                MyLog.Source = "Tuner Errors";
                message += "\nError Message : " + ex.Message + "\nExeption Message : " + ex.Message + "\nExeption StackTrace : " + ex.StackTrace;

                MyLog.WriteEntry(message);
                //Layers_Handler.instance().sendMessageBox("Problem in  frmMainDisplayDebug in OnChangLogLinesEvent", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error, this.ToString());
            }
        }
        private void UpdateRedLogLines(object sender, LogEventArgs Logargs)
        {

            try
            {
                System.Drawing.Font currentFont = redLog.SelectionFont;
                string line;
                LogLineData logLine;
                System.Drawing.Font font = new System.Drawing.Font(
                                        currentFont.FontFamily,
                                        currentFont.Size,
                                        FontStyle.Bold);
                Color selectColor = Color.Black;
                switch (Logargs.LogType.ToString())
                {
                    case "ltGeneral":
                    case "ltInfo": selectColor = Color.Black; break;
                    case "ltSuccess": selectColor = Color.Green; break;
                    case "ltWarning": selectColor = Color.BlueViolet; break;
                    case "ltError": selectColor = Color.Red; break;
                    case "ltBlue": selectColor = Color.Blue; break;
                    case "ltGreen": selectColor = Color.Green; break;
                    case "ltMagenta": selectColor = Color.Magenta; break;
                }


                if (redLog.Lines.Length > 50000)
                    redLog.Clear();
                if (Logargs.LogLinesData != null)
                {
                    if (Logargs.LogLinesData.Count > 0)
                    {
                        for (int kk = 0; kk < Logargs.LogLinesData.Count; kk++)
                        {
                            logLine = Logargs.LogLinesData[kk];
                            if (logLine != null)
                            {
                                switch (logLine.Log_Type.ToString())
                                {
                                    case "ltGeneral":
                                    case "ltInfo": selectColor = Color.Black; break;
                                    case "ltSuccess": selectColor = Color.Green; break;
                                    case "ltWarning": selectColor = Color.BlueViolet; break;
                                    case "ltError": selectColor = Color.Red; break;
                                    case "ltBlue": selectColor = Color.Blue; break;
                                    case "ltGreen": selectColor = Color.Green; break;
                                    case "ltMagenta": selectColor = Color.Magenta; break;
                                }
                                redLog.SelectionFont = font;
                                redLog.SelectionColor = selectColor;
                                try
                                {
                                    line = logLine.Line + "\n";
                                }
                                catch
                                {
                                    line = "";
                                }
                                if (line != "")
                                    redLog.AppendText(line);
                            }
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < Logargs.Messages.Length; k++)
                    {
                        redLog.SelectionFont = font;
                        redLog.SelectionColor = selectColor;
                        redLog.AppendText(Logargs.Messages[k] + "\n");
                    }
                }
                redLog.Focus();

            }
            catch (Exception ex)
            {
                WriteEvenLog.Write("Problem in frmMainDisplayDebug in UpdateRedLogLines", ex);
                //  Layers_Handler.instance().sendMessageBox("Problem in frmMainDisplayDebug in UpdateRedLogLines", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error, sender.ToString());
            }
        }
        #endregion
        #endregion

        #region Fields
        // FIELDS
        // ------
        private string[] args;
        delegate void AddDisplayEventMessage(DisplayEventArgs args);
        private event AddDisplayEventMessage m_AddDisplayEventMessage;
        private IniFile ini = new IniFile( Layers_Handler.instance().RunDir + @"\Setup.ini");
        private ManageMuntersTest mng;
        private EventLog MyLog;
        private List<BetsEventArgs> betsList = new List<BetsEventArgs>();
        private bool enable = false;
        private List<string> users = new List<string>();
        private string last_url = "";

        #endregion


    }
}

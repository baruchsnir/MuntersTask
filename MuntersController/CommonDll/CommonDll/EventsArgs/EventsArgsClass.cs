using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace FT.CommonDll
{
	#region Display Event Args Class
	// step 1: EventArgs definition
	// ------
	// Type defining the event's information.
	/// <summary>
	/// Class For Sending Events of Display To Main form
	/// </summary>
	public class DisplayEventArgs : EventArgs 
	{	
		#region Constractor
		// CONSTRACTOR
		// ------
		public DisplayEventArgs() 
		{
		}
        public DisplayEventArgs(string line)
        {
            this.line = line;
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------

		public bool AddRedTestline
		{
			get{return this.addRedTestline;}
			set{this.addRedTestline = value;}
		}
		public bool AddRedLogline
		{
			get{return this.addRedLogline;}
			set{this.addRedLogline = value;}
		}
		public bool UpdateLamps
		{
			get{return this.updateLamps;}
			set{this.updateLamps = value;}
		}
		public bool ClearRedLog
		{
			get{return this.clearRedLog;}
			set{this.clearRedLog = value;}
		}
		public bool ClearRedTests
		{
			get{return this.clearRedTests;}
			set{this.clearRedTests = value;}
		}
		
		public string Line
		{
			get{return this.line;}
			set{this.line = value;}
		}
        
        /// <summary>
        /// Indication That this is start of Run Set Test
        /// </summary>
        /// <value></value>
        public bool StartRunSetTest
        {
            get { return this.startRunSetTest; }
            set { this.startRunSetTest = value; }
        }
        /// <summary>
        /// Indication That this is end of Run Set Test
        /// </summary>
        /// <value></value>

        public bool EndRunSetTest
        {
            get { return this.endRunSetTest; }
            set { this.endRunSetTest = value; }
        }
        
        public bool RemoveUser
        {
            get { return this.removeUser; }
            set { this.removeUser = value; }
        }
        public string User
        {
            get { return this.user; }
            set { this.user = value; }
        }
        #endregion

        #region Fields
        // FIELDS
        // ------
        private bool removeUser = false;
        private string user = "";
        private string line = "";
        private bool addRedTestline=false,addRedLogline=false,updateLamps=false,clearRedTests=false,clearRedLog=false;
        private bool startRunSetTest = false;
        private bool endRunSetTest = false;
		#endregion
	}
	#endregion

    #region Update Encoder / decoder Event Args Class
    // step 1: EventArgs definition
    // ------
    // Type defining the event's information.
    /// <summary>
    /// Class For Sending Events of Update Target To Main form
    /// Update Encoder / decoder Event Args Class
    /// </summary>
    public class UpdateTargetTypeEventArgs : EventArgs
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Class For Sending Events of Update Target To Main form
        /// Update Encoder / decoder /Ivg Event Args Class
        /// </summary>
        public UpdateTargetTypeEventArgs()
        {
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        /// <summary>
        /// The string to update Encoder Type 
        /// </summary>
        public string EncoderType
        {
            get { return encoderType; }
            set { this.encoderType = value; }
        }
        /// <summary>
        /// The string to update Decoder Type 
        /// </summary>
        public string DecoderType
        {
            get { return decoderType; }
            set { this.decoderType = value; }
        }
        /// <summary>
        /// The string to update Ivg Type 
        /// </summary>
        public string IvgType
        {
            get { return ivgType; }
            set { this.ivgType = value; }
        }
        /// <summary>
        /// Indicateion to update Encoder
        /// </summary>
        public bool Update_Encoder
        {
            get { return update_Encoder; }
            set { this.update_Encoder = value; }
        }
        /// <summary>
        /// Indicateion to update Decoder
        /// </summary>
        public bool Update_Decoder
        {
            get { return update_Decoder; }
            set { this.update_Decoder = value; }
        }
        /// <summary>
        /// Indicateion to update IVG
        /// </summary>
        public bool Update_IVGType
        {
            get { return update_IVGType; }
            set { this.update_IVGType = value; }
        }
        /// <summary>
        /// Indicateion to update IRP
        /// </summary>
        public bool Update_IRP
        {
            get { return update_IrpType; }
            set { this.update_IrpType = value; }
        }
        
        /// <summary>
        /// Indicateion to update IVG
        /// </summary>
        public bool Update_IRPType
        {
            get { return update_IrpType; }
            set { this.update_IrpType = value; }
        }
        /// <summary>
        /// The index of this target in the system 0-100
        /// </summary>
        public int Targetrindex
        {
            get { return targetrindex; }
            set { this.targetrindex = value; }
        }
        public string IrpType
        {
            get { return irp_type; }
            set { this.irp_type = value; }
        }
        #endregion

        #region Fields
        // FIELDS
        // ------
        private bool update_Encoder = false, update_Decoder = false, update_IVGType = false, update_IrpType = false;
        private string decoderType = "";
        private string ivgType = "";
        private string irp_type = "";
        private string encoderType = "";
        private int targetrindex = 0;
        #endregion
    }
    #endregion

	#region Menu Event Args Class
	// step 1: EventArgs definition
	// ------
	// Type defining the event's information.
	/// <summary>
	/// Class For Sending Events of Menu to Display of Main form
	/// </summary>
	public class MenuEventArgs : EventArgs 
	{	
		#region Constractor
		// CONSTRACTOR
		// ------
		/// <summary>
		/// Class for sending menu parameters to display
		/// </summary>
		public MenuEventArgs() 
		{
			menu = new SortedList();
            changeMenuNem = new SortedList();
        }
		#endregion

		#region Methods
		// METHODS
		// ----------
		#region SetMenu
		/// <summary>
		/// Set The Menu By name
		/// </summary>
		/// <param name="menuname">Menu Name as string</param>
		/// <param name="enable">Enable as bool</param>
		public void SetMenu(string menuname,bool enable)
		{ 
			//if the menu exist we change it ,else we add it to list
			if (menu[menuname] != null)
              menu[menuname] = enable;
			else
			  menu.Add(menuname,enable);
		}
        /// <summary>
        /// Set The Menu By name
        /// </summary>
        /// <param name="menuname">Menu Name as string</param>
        /// <param name="text">The text for this menu</param>
        public void SetNemuTest(string menuname, string text)
        {
          //if the menu exist we change it ,else we add it to list
            if (changeMenuNem[menuname] != null)
                changeMenuNem[menuname] = text;
            else
                changeMenuNem.Add(menuname, text);
        }
		#endregion
		
		#endregion

		#region Properties
		// PROPERTIES
		// ----------
		/// <summary>
		/// Hash table of menus as menu name as a Key and enable/dispale as value
		/// </summary>
		public SortedList Menu
		{
			get{return menu;}
			set{this.menu= value;}
		}

		/// <summary>
		/// Hash table of menus as menu name as a Key and text as the text to be for this menu
		/// </summary>
        public SortedList ChangeMenuNem
        {
            get { return changeMenuNem; }
            set { this.changeMenuNem = value; }
        }
		/// <summary>
		/// Enable Or Disable The Combo Box Of Test Names
		/// </summary>
		public bool CbxSelectEnabled
		{
			get{return cbxSelectEnabled;}
			set{this.cbxSelectEnabled= value;}
		}
		/// <summary>
		/// Enable Or Disable The Panel Of Encoders serial Text Box And Check Boxs
		/// </summary>
		public bool PnlEncodersEnabled
		{
			get{return pnlEncodersEnabled;}
			set{this.pnlEncodersEnabled= value;}
		}
		#endregion

		#region Fields
		// FIELDS
		// ------
		private SortedList menu;
        private SortedList changeMenuNem;
        private bool cbxSelectEnabled = false,pnlEncodersEnabled=false;
		#endregion
	}
	#endregion

    #region Log Event Args class
    // EVENT HANDLER -if Add Line To log We Send The line as message 
    // we have to send message to all forms
    // ----------
    // step 1: EventArgs definition
    // ------
    // Type defining the event's information.
    /// <summary>
    /// Class For Sending Event Log Line To Main form
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Constractor for Defualt Log event
        /// </summary>
        public LogEventArgs()
        {
        }
        /// <summary>
        /// Constractor for Defualt Log event
        /// </summary>
        /// <param name="message">The line to write</param>
        public LogEventArgs(string message)
        {
            this.logType = LogType.ltInfo;
            this.prefix = "";
            this.messages[0] = message;
        }
        /// <summary>
        /// AddLine for Log file string message,LogType logType
        /// </summary>
        /// <param name="message">The line to write</param>
        /// <param name="logType">The type of color</param>
        public LogEventArgs(string message, LogType logType)
        {
            this.logType = logType;
            this.prefix = "";
            this.messages[0] = message;
        }
        /// <summary>
        /// AddLines for Log file string message,LogType logType
        /// </summary>
        /// <param name="message">The lines to write</param>
        /// <param name="logType">The type of color</param>
        public LogEventArgs(string[] messages, LogType logType)
        {
            this.logType = logType;
            this.prefix = "";
            this.messages = messages;
        }
        /// <summary>
        /// AddLine for Log file string message,LogType logType,string Prefix
        /// </summary>
        /// <param name="message">The line to write</param>
        /// <param name="logType">The type of color</param>
        /// <param name="Prefix">The perfix to add to line</param>
        public LogEventArgs(string message, LogType logType, string Prefix)
        {
            this.logType = logType;
            this.prefix = Prefix;
            this.messages[0] = message;
        }
        /// <summary>
        /// AddLine for Log file string message,LogType logType,string Prefix
        /// </summary>
        /// <param name="message">The line to write</param>
        /// <param name="Prefix">The perfix to add to line</param>
        public LogEventArgs(string message, string Prefix)
        {
            this.logType = LogType.ltGeneral;
            this.prefix = Prefix;
            this.messages[0] = message;
        }
        /// <summary>
        /// AddLine for Log file string message,LogType logType,string Prefix
        /// </summary>
        /// <param name="message">The line to write</param>
        /// <param name="logType">The type of color</param>
        /// <param name="Prefix">The perfix to add to line</param>
        public LogEventArgs(string[] messages, LogType logType, string Prefix)
        {
            this.logType = logType;
            this.prefix = Prefix;
            this.messages = messages;
        }
        /// <summary>
        /// AddLine for Log file string message,LogType logType,string Prefix
        /// </summary>
        /// <param name="message">The line to write</param>
        /// <param name="logType">The type of color</param>
        /// <param name="Prefix">The perfix to add to line</param>
        public LogEventArgs(List<string> messages, LogType logType, string Prefix)
        {
            this.logType = logType;
            this.prefix = Prefix;
            this.messages = messages.ToArray();
        }
        /// <summary>
        /// AddLine for Log file string message,LogType logType,string Prefix
        /// </summary>
        /// <param name="message">The line to write</param>
        /// <param name="logType">The type of color</param>
        /// <param name="Prefix">The perfix to add to line</param>
        public LogEventArgs(List<LogLineData> logLinesData, string Prefix)
        {
            this.logType = LogType.ltGeneral;
            this.prefix = Prefix;
            this.logLinesData = logLinesData;
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        public string[] Messages
        {
            set { this.messages = value; }
            get { return this.messages; }
        }
        public LogType LogType
        {
            set { this.logType = value; }
            get { return this.logType; }
        }
        public string Prefix
        {
            set { this.prefix = value; }
            get { return this.prefix; }
        }
        /// <summary>
        /// List of lines with LogType
        /// </summary>
        public List<LogLineData> LogLinesData
        {
            set { this.logLinesData = value; }
            get { return this.logLinesData; }
        }
        #endregion

        #region Fields
        // FIELDS
        // ------
        private string[] messages = new string[] { "" };
        private LogType logType = LogType.ltInfo;
        private string prefix = "";
        private List<LogLineData> logLinesData = null;
        #endregion
    }
    #endregion

    #region Fail Encoder Event Args class
    // step 1: EventArgs definition
    // ------
    // Type defining the event's information.
    /// <summary>
    /// Fail Encoder Event Args class to pass the data of the fail of this encoder
    /// </summary>
    public class FailEncoderEventArgs : EventArgs
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Event Args class for information of Fail Encoder Error
        /// </summary>
        public FailEncoderEventArgs()
        {
        }
        /// <summary>
        /// Event Args class for information of Fail Encoder Error
        /// </summary>
        /// <param name="Reason">The reason of The Fail</param>
        /// <param name="FatalError">If this is fatal error</param>
        /// <param name="encoderIndex">The index of the Encoder that fail 0-1f</param>
        /// <param name="failTime">Indicate the time of the alarm</param>
        public FailEncoderEventArgs(string Reason, bool FatalError, int encoderIndex, DateTime failTime)
        {
            this.reason = Reason;
            this.fatalError = FatalError;
            this.encoderIndex = encoderIndex;
            this.failTime = failTime;
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        /// <summary>
        /// The Fail Description
        /// </summary>
        public string Reason
        {
            set { this.reason = value; }
            get { return this.reason; }
        }
        /// <summary>
        /// The Encoder Index 1 - 16
        /// </summary>
        public int EncoderIndex
        {
            set { this.encoderIndex = value; }
            get { return this.encoderIndex; }
        }
        /// <summary>
        /// Indicate if this is fatal Error
        /// </summary>
        public bool FatalError
        {
            set { this.fatalError = value; }
            get { return this.fatalError; }
        }
        /// <summary>
        /// Indicate that this fail was in Intilization
        /// </summary>
        public bool IntilizationStep
        {
            set { this.intilizationStep = value; }
            get { return this.intilizationStep; }
        }
        /// <summary>
        /// Indicate the time of the alarm
        /// </summary>
        public DateTime FailTime
        {
            set { this.failTime = value; }
            get { return this.failTime; }
        }
        /// <summary>
        /// The name of the test
        /// </summary>
        public string CurrentTest
        {
            get { return currentTest; }
            set { currentTest = value; }
        }

        /// <summary>
        /// The name of the step in the test
        /// </summary>
        public string CurrentStep
        {
            get { return currentStep; }
            set { currentStep = value; }
        }
        /// <summary>
        /// The current Step index of this test
        /// </summary>
        public string Current_Step_index
        {
            set { this.FCurrent_Step_index = value; }
            get { return this.FCurrent_Step_index; }
        }
        /// <summary>
        /// The current test index in this Run Set
        /// </summary>
        public int Current_Test_Index
        {
            set { this.FCurrent_Test_Index = value; }
            get { return this.FCurrent_Test_Index; }
        }
        #endregion

        #region Fields
        // FIELDS
        // ------
        protected bool fatalError = false;
        protected int encoderIndex;
        protected string reason = "";
        protected DateTime failTime;
        protected bool intilizationStep = false;
        protected string currentTest = "";
        protected string currentStep = "";
        protected string FCurrent_Step_index = "0";
        protected int FCurrent_Test_Index = 0;
        #endregion
    }
    public class FailIRPEventArgs : FailEncoderEventArgs
    {
                /// <summary>
        /// Event Args class for information of Fail Encoder Error
        /// </summary>
        /// <param name="Reason">The reason of The Fail</param>
        /// <param name="FatalError">If this is fatal error</param>
        /// <param name="encoderIndex">The index of the Encoder that fail 0-1f</param>
        /// <param name="failTime">Indicate the time of the alarm</param>
        public FailIRPEventArgs(string Reason, bool FatalError, int encoderIndex, DateTime failTime)
        {
            this.reason = Reason;
            this.fatalError = FatalError;
            this.encoderIndex = encoderIndex;
            this.failTime = failTime;
        }
    }
    #endregion

    #region delegate Raising when Log Line added
    // step 2: Delegate type defining the prototype of the callback method
    // ------  that receivers must implement
    /// <summary>
    /// delegate for call Back method for Log Line added
    /// </summary>
    public delegate void LogEventHandler(Object sender, LogEventArgs args);
    #endregion

	#region Open Log Form Event Args class
	// EVENT HANDLER -if Add Line To log We Send The line as message 
	// we have to send message to all forms
	// ----------
	// step 1: EventArgs definition
	// ------
	// Type defining the event's information.
	/// <summary>
	/// Class For Sending Event Log Line To Main form
	/// </summary>
	public class OpenLogFormEventArgs : EventArgs 
	{	
		#region Constractor
		// CONSTRACTOR
		// ------
		public OpenLogFormEventArgs() 
		{
		}
		#endregion

		#region Properties
		// PROPERTIES
		// ----------
		public string FileName
		{
			set{this.fileName = value;}
			get{return this.fileName;}
		}
		public string DirName
		{
			set{this.dirName = value;}
			get{return this.dirName;}
		}
		
		public string EncoderName
		{
			set{this.encoderName = value;}
			get{return this.encoderName;}
		}
		public bool CurrentRunSet
		{
			set{this.currentRunSet = value;}
			get{return this.currentRunSet;}
		}
		#endregion

		#region Fields
		// FIELDS
		// ------
		private string fileName="",dirName="";
		private string encoderName="";
		private bool currentRunSet=false;
		#endregion
	}


	#endregion

	#region Open Test Builder Form Or Run Set Form Event Args class
	// EVENT HANDLER -if Add Line To log We Send The line as message 
	// we have to send message to all forms
	// ----------
	// step 1: EventArgs definition
	// ------
	// Type defining the event's information.
	/// <summary>
	/// Class For Sending Event Test Builder or Run Set To Main form
	/// </summary>
	public class OpenFormEventArgs : EventArgs 
	{	
		#region Constractor
		// CONSTRACTOR
		// ------
		public OpenFormEventArgs() 
		{
		}
		#endregion

		#region Properties
		// PROPERTIES
		// ----------
		/// <summary>
		/// The Name of the File To Open
		/// </summary>
		public string FileName
		{
			set{this.fileName = value;}
			get{return this.fileName;}
		}
		/// <summary>
		/// To Open From File or From Memory
		/// </summary>
		public bool FromFile
		{
			set{this.fromfile = value;}
			get{return this.fromfile;}
		}
        /// <summary>
        /// The index in the Run Set Test of the test to open in format "test1"
        /// </summary>
        public string TestIndex
        {
            set { this.testIndex = value; }
            get { return this.testIndex; }
        }
        /// <summary>
        /// The encoder index 1..max encoders
        /// </summary>
        public string EncoderIndex
        {
            set { this.encoderIndex = value; }
            get { return this.encoderIndex; }
        }
        /// <summary>
        /// Close the Form
        /// </summary>
        public bool Close_Form
        {
            set { this.FClose_Form = value; }
            get { return this.FClose_Form; }
        }
		#endregion

		#region Fields
		// FIELDS
		// ------
		private string fileName="";
		private bool fromfile=true;
        private string testIndex = "";
        private string encoderIndex = "0";
        private bool FClose_Form = false;
		#endregion
	}


	#endregion

	#region Update Test Builder Form Or Run Set Form With Steps Event Args class
	// step 1: EventArgs definition
	// ------
	// Type defining the event's information.
	/// <summary>
	/// Update Test Builder Form Or Run Set Form With Steps Event Args class
	/// </summary>
	public class UpdateStepsInFormEventArgs : EventArgs 
	{	
		#region Constractor
		// CONSTRACTOR
		// ------
		public UpdateStepsInFormEventArgs() 
		{
		}
		#endregion

		#region Properties
		// PROPERTIES
		// ----------
		/// <summary>
		/// The Name of the File To Update
		/// </summary>
		public string FileName
		{
			set{this.fileName = value;}
			get{return this.fileName;}
		}
		/// <summary>
		/// Indicating Updating Run Set Editor
		/// </summary>
		public bool RunSetSteps
		{
			set{this.runSetSteps = value;}
			get{return this.runSetSteps;}
		}
		/// <summary>
		/// Indicating Updating Test Scripts Editor
		/// </summary>
		public bool TestSteps
		{
			set{this.testSteps = value;}
			get{return this.testSteps;}
		}
        /// <summary>
        /// Indicating the Test index in the run set
        /// </summary>
        public int TestIndex
        {
            set { this.testIndex = value; }
            get { return this.testIndex; }
        }
		#endregion

		#region Fields
		// FIELDS
		// ------
		private string fileName="";
		private bool runSetSteps=false,testSteps=false;
        private int testIndex=0;
		#endregion
	}


	#endregion

	#region Oven Event Args class
	// step 1: EventArgs definition
	// ------
	// Type defining the event's information.
	/// <summary>
	/// Class Oven Sending Event  To Main form
	/// </summary>
	public class OvenEventArgs : EventArgs 
	{	
		#region Constractor
		// CONSTRACTOR
		// ------
		/// <summary>
		/// Class to Update Oven Current Temp
		/// </summary>
		public OvenEventArgs() 
		{
		}
		#endregion

		#region Properties
		// PROPERTIES
		// ----------
        /// <summary>
        /// Oven Current Temprature in format xxx -:- 25 C = 250
        /// </summary>
		public int OvenCurrentTemp
		{
			set{this.ovenCurrentTemp = value;}
			get{return this.ovenCurrentTemp;}
		}
		/// <summary>
		/// Oven Current Temprature in format xx.x
		/// </summary>
		public string CecCurrentTemp
		{
			set{this.cecCurrentTemp = value;}
			get{return this.cecCurrentTemp;}
		}

		/// <summary>
		/// Add star near the Oven temrature to indicate Port Fail
		/// </summary>
		public string LblCecFail
		{
			set{this.lblCecFail = value;}
			get{return this.lblCecFail;}
		}
		#endregion

		#region Fields
		// FIELDS
		// ------
		private int ovenCurrentTemp=0;
		private string cecCurrentTemp="0.0";
		private string lblCecFail="";
		#endregion
	}
	#endregion

	#region Bet Event Args class
	// step 1: EventArgs definition
	// ------
	// Type defining the event's information.
	/// <summary>
	/// Event Args class for information of Status Test Step
	/// If there is a Fail in encoder we add the Fail parameters
	/// </summary>
	public class BetsEventArgs : EventArgs 
	{	
		#region Constractor
		// CONSTRACTOR
		// ------
		/// <summary>
		/// Event Args class for information of Status Test Step 
		/// If there is a Fail in encoder we add the Fail parameters
		/// </summary>
		public BetsEventArgs() 
		{
		}
        public BetsEventArgs(string siteName, string game, string betName)
        {
            this.siteName = siteName;
            this.gameName = game;
            this.file_name = betName;
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        /// <summary>
        /// Oven Current Temprature in format xxx -:- 25 C = 250
        /// </summary>
        public string OvenCurrentTemp
		{
			set{this.ovenCurrentTemp = value;}
			get{return this.ovenCurrentTemp;}
		}
        /// <summary>
        /// Text Read
        /// </summary>
        public string TextRead
        {
            set { this.textRead = value; }
            get { return this.textRead; }
        }
        /// <summary>
		/// Current Site name 
		/// </summary>
		public string SiteName
        {
			set{this.siteName = value;}
			get{return this.siteName;}
		}
        /// <summary>
        /// Current Test index in format test1-k 
        /// </summary>
        public string TestIndex
        {
            set { this.testIndex = value; }
            get { return this.testIndex; }
        }
        /// <summary>
        /// Current Game Name
        /// </summary>
		public string GameName
        {
			set{this.gameName = value;}
			get{return this.gameName;}
		}
		/// <summary>
		/// The Color type of the Test Step
		/// </summary>
		public LogType ColorType
		{
			set{this.colorType = value;}
			get{return this.colorType;}
		}
		/// <summary>
		/// The index of the Encoder that fail
		/// </summary>
		public int EncoderIndex
		{
			set{this.encoderIndex = value;}
			get{return this.encoderIndex;}
		}
		/// <summary>
        /// The index of the Step in this test
        /// </summary>
        public string TestStepIndex
        {
            set { this.testStepIndex = value; }
            get { return this.testStepIndex; }
        }

		/// <summary>
		/// The fail Reason
		/// </summary>
		public string FailReason
		{
			set{this.failReason = value;}
			get{return this.failReason;}
		}
		/// <summary>
		/// The Bet Name
		/// </summary>
		public string File_Name
        {
			set{this.file_name = value;}
			get{return this.file_name; }
		}
		/// <summary>
		/// Indicate if the Font is Bold
		/// </summary>
		public bool FontBold
		{
			set{this.fontBold = value;}
			get{return this.fontBold;}
		}
        /// <summary>
        /// Indicate if update Test
        /// </summary>
        public bool UpdateTest
        {
            set { this.updateTest = value; }
            get { return this.updateTest; }
        }
        /// <summary>
        /// Indicate if update text Box Read
        /// </summary>
        public bool Upupdate_txtRead
        {
            set { this.upupdate_txtRead = value; }
            get { return this.upupdate_txtRead; }
        }
        /// <summary>
        /// Current Test name 
        /// </summary>
        public DateTime CurrentDateTime
        {
            set { this.currentDateTime = value; }
            get { return this.currentDateTime; }
        }
        /// <summary>
        /// Indicate if update Intilization Step
        /// </summary>
        public bool UpdateIntilizationStep
        {
            set { this.updateIntilizationStep = value; }
            get { return this.updateIntilizationStep; }
        }
        /// <summary>
        /// Indiacetion that we update the last excel file name
        /// </summary>
        public bool Update_excel_name
        {
            set { this.update_excel_name = value; }
            get { return this.update_excel_name; }

        }
        public bool Single_file_exist
        {
            set { this.single_file_exist = value; }
            get { return this.single_file_exist; }
        }
        #endregion

        #region Fields
        // FIELDS
        // ------
        private bool single_file_exist = false;
        private string ovenCurrentTemp="";
        private string textRead = "";
        private bool upupdate_txtRead= false;
        private bool updateIntilizationStep = false;
		private string failReason="";
		private string file_name = "";
		private int encoderIndex=-1;
		private string siteName="";
		private string gameName="";
        private string testStepIndex = "";
        private LogType colorType;
		private bool fontBold=false;
        private bool updateTest = false;
        private bool updateStep = false;
        private string testIndex = "";
        private DateTime currentDateTime = DateTime.Now;
        private bool update_excel_name = false;
		#endregion
	}
	#endregion

	#region Stop Event Args class
	// step 1: EventArgs definition
	// ------
	// Type defining the event's information.
	/// <summary>
	/// Event Args class for information of Stop Test Run
	/// </summary>
	public class StopEventArgs : EventArgs 
	{	
		#region Constractor
		// CONSTRACTOR
		// ------
		/// <summary>
		/// Event Args class for information of Stop Run Set Test
		/// </summary>
		public StopEventArgs() 
		{
		}
        public StopEventArgs(bool stopRun, bool suspendRun, string description)
        {
            this.stopRun = false;
            this.suspendRun = false;
            this.description = "";
            this.description = description;
            this.suspendRun = suspendRun;
            this.stopRun = stopRun;
        }

 

		#endregion

		#region Properties
		// PROPERTIES
		// ----------
		/// <summary>
		/// The Stop Description
		/// </summary>
		public string Description
		{
			set{this.description = value;}
			get{return this.description;}
		}
		/// <summary>
		/// The Stop Reason
		/// </summary>
		public TStopReason StopReason
		{
			set{this.stopReason = value;}
			get{return this.stopReason;}
		}
		/// <summary>
		/// Indicate to stop this run
		/// </summary>
		public bool StopRun
		{
			set{this.stopRun = value;}
			get{return this.stopRun;}
		}
        /// <summary>
        /// Indication To suspend the Run Test
        /// </summary>
        public bool SuspendRun
        {
            set { this.suspendRun = value; }
            get { return this.suspendRun; }
       
        }
		#endregion

		#region Fields
		// FIELDS
		// ------
		private bool stopRun=true;
		private TStopReason stopReason;
		private string description="";
        private bool suspendRun = false;
		#endregion
	}
	#endregion

    //#region Fail Encoder Event Args class
    //// step 1: EventArgs definition
    //// ------
    //// Type defining the event's information.
    ///// <summary>
    ///// Fail Encoder Event Args class to pass the data of the fail of this encoder
    ///// </summary>
    //public class FailEncoderEventArgs : EventArgs 
    //{	
    //    #region Constractor
    //    // CONSTRACTOR
    //    // ------
    //    /// <summary>
    //    /// Event Args class for information of Fail Encoder Error
    //    /// </summary>
    //    public FailEncoderEventArgs() 
    //    {
    //    }
    //    /// <summary>
    //    /// Event Args class for information of Fail Encoder Error
    //    /// </summary>
    //    /// <param name="Reason">The reason of The Fail</param>
    //    /// <param name="FatalError">If this is fatal error</param>
    //    /// <param name="EncoderIndex">The index of the Encoder that fail 1-7</param>
    //    /// <param name="failTime">Indicate the time of the alarm</param>
    //    public FailEncoderEventArgs(string Reason,bool FatalError,int EncoderIndex,DateTime failTime)
    //    {
    //        this.reason = Reason;
    //        this.fatalError = FatalError;
    //        this.encoderIndex = EncoderIndex;
    //        this.failTime = failTime;
    //    }
    //    #endregion

    //    #region Properties
    //    // PROPERTIES
    //    // ----------
    //    /// <summary>
    //    /// The Fail Description
    //    /// </summary>
    //    public string Reason
    //    {
    //        set{this.reason = value;}
    //        get{return this.reason;}
    //    }
    //    /// <summary>
    //    /// The Encoder Index 1 - 7
    //    /// </summary>
    //    public int EncoderIndex
    //    {
    //        set{this.encoderIndex = value;}
    //        get{return this.encoderIndex;}
    //    }
    //    /// <summary>
    //    /// Indicate if this is fatal Error
    //    /// </summary>
    //    public bool FatalError 
    //    {
    //        set{this.fatalError = value;}
    //        get{return this.fatalError;}
    //    }
    //    /// <summary>
    //    /// Indicate the time of the alarm
    //    /// </summary>
    //    public DateTime FailTime
    //    {
    //        set { this.failTime = value; }
    //        get { return this.failTime; }
    //    }
    //    /// <summary>
    //    /// The name of the test
    //    /// </summary>
    //    public string CurrentTest
    //    {
    //        get { return currentTest; }
    //        set { currentTest = value; }
    //    }

    //    /// <summary>
    //    /// The name of the step in the test
    //    /// </summary>
    //    public string CurrentStep
    //    {
    //        get { return currentStep; }
    //        set { currentStep = value; }
    //    }
    //    /// <summary>
    //    /// The current Step index of this test
    //    /// </summary>
    //    public string Current_Step_index
    //    {
    //        set { this.FCurrent_Step_index = value; }
    //        get { return this.FCurrent_Step_index; }
    //    }
    //    /// <summary>
    //    /// The current test index in this Run Set
    //    /// </summary>
    //    public int Current_Test_Index
    //    {
    //        set { this.FCurrent_Test_Index = value; }
    //        get { return this.FCurrent_Test_Index; }
    //    }
    //    #endregion

    //    #region Fields
    //    // FIELDS
    //    // ------
    //    private bool fatalError=false;
    //    private int encoderIndex;
    //    private string reason="";
    //    private DateTime failTime;
    //    private string currentTest = "";
    //    private string currentStep = "";
    //    private string FCurrent_Step_index = "0";
    //    private int FCurrent_Test_Index = 0;
    //    #endregion
    //}
    //#endregion

    #region Encoder Rates status Event Args class
    // EVENT HANDLER -Send Rate value of  
    // we have to send message to all forms
    // ----------
    // step 1: EventArgs definition
    // ------
    // Type defining the event's information.
    /// <summary>
    /// Class For Sending Event Rate value of encoder
    /// </summary>
    public class EncoderRatesStatusEventArgs : EventArgs
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Constractor for Defualt Encoder Rates Status event
        /// </summary>
        public EncoderRatesStatusEventArgs()
        {
        }
        /// <summary>
        /// Constractor for Encoder Rates Status event with all params
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="video"></param>
        /// <param name="audio"></param>
        /// <param name="pcr"></param>
        /// <param name="index"></param>
        /// <param name="configured"></param>
        /// <param name="allocated"></param>
        /// <param name="measured"></param>
        public EncoderRatesStatusEventArgs(string rate,bool video,bool audio,bool pcr,int index,bool configured,bool allocated ,bool measured)
        {
            this.rate = rate;
            this.video = video;
            this.audio = audio;
            this.pcr = pcr;
            this.index = index;
            this.allocated = allocated;
            this.configured = configured;
            this.measured = measured;
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        /// <summary>
        /// The rate that was Configured,Allocated,Measured
        /// </summary>
        /// <value></value>
        public string Rate
        {
            set { this.rate = value; }
            get { return this.rate; }
        }
        /// <summary>
        /// Indicate Video Rates
        /// </summary>
        /// <value></value>
        public bool Video
        {
            set { this.video = value; }
            get { return this.video; }
        }
        /// <summary>
        /// Indicate Audio Rates
        /// </summary>
        /// <value></value>
        public bool Audio
        {
            set { this.audio = value; }
            get { return this.audio; }
        }
        /// <summary>
        /// Indicate Pcr Rates
        /// </summary>
        /// <value></value>
        public bool Pcr
        {
            set { this.pcr = value; }
            get { return this.pcr; }
        }
        /// <summary>
        /// Indicate Configured Rates
        /// </summary>
        /// <value></value>
        public bool Configured
        {
            set { this.configured = value; }
            get { return this.configured; }
        }
        /// <summary>
        /// Indicate Allocated Rates
        /// </summary>
        /// <value></value>
        public bool Allocated
        {
            set { this.allocated = value; }
            get { return this.allocated; }
        }
        /// <summary>
        /// Indicate Measured Rates
        /// </summary>
        /// <value></value>
        public bool Measured
        {
            set { this.measured = value; }
            get { return this.measured; }
        }
        /// <summary>
        /// The index of the configured Rate 0-4 Video,0-7 Audio,0-11 Pcr
        /// </summary>
        /// <value></value>
        public int Index
        {
            set { this.index = value; }
            get { return this.index; }
        }
        /// <summary>
        /// The index of the Encoder
        /// </summary>
        /// <value></value>
        public int EncoderIndex
        {
            set { this.encoderindex = value; }
            get { return this.encoderindex; }
        }

        #endregion

        #region Fields
        // FIELDS
        // ------
        private string rate="";
        private bool video = false;
        private bool audio = false;
        private bool pcr = false;
        private bool configured = false;
        private bool allocated = false;
        private bool measured = false;
        private int index = 0;
        private int encoderindex = 0;
        #endregion
    }
    #endregion

    #region Rechive Char Event args for com port
    // step 1: EventArgs definition
    // ------
    // Type defining the event's information.
    /// <summary>
    /// Class Rechive Char Event args for com port
    /// </summary>
    public class RechiveCharEventargs : EventArgs
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Class Rechive Char Event args for com port
        /// </summary>
        public RechiveCharEventargs()
        {
        }
        /// <summary>
        /// Class Rechive Char Event args for com port
        /// </summary>
        /// <param name="message">The string that was arrived</param>
        public RechiveCharEventargs(string message)
        {
            this.mesaage = message;
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        /// <summary>
        /// The string of Chars that were recived
        /// </summary>
        public string Mesaage
        {
            set { this.mesaage = value; }
            get { return this.mesaage; }
        }


        #endregion

        #region Fields
        // FIELDS
        // ------
        private string mesaage = "";
        #endregion
    }
    #endregion


    #region delegate for Open Terminal form
    // step 2: Delegate type defining the prototype of the callback method
    // ------  that receivers must implement
    /// <summary>
    /// delegate for call Back method for menu event to Open Terminal form
    /// </summary>
    public delegate void Open_Terminal_FormEventHandler(object sender, Open_TerminalEventArgs args);
    #endregion

    #region Open_TerminalEventArgs
    /// <summary>
    /// Hold data on Open_Terminal Event   
    /// </summary>
    public class Open_TerminalEventArgs
    {
        #region Fields
        // FIELDS
        // ------
        private int targetIndex = 0;
        private bool open = false;
        private bool close = false;
        #endregion

        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Hold data on Open_Terminal Event 
        /// </summary>
        public Open_TerminalEventArgs()
        {
        }

        /// <summary>
        /// Hold data on Open_Terminal Event 
        /// </summary>
        /// <param name="open">Indication to open form</param>
        /// <param name="close">Indication to close form</param>
        /// <param name="targetIndex">The index of this Encoder 0-15</param>
        public Open_TerminalEventArgs(int targetIndex, bool open, bool close)
        {
            this.close = close;
            this.open = open;
            this.targetIndex = targetIndex;
        }
        #endregion

        #region Methods
        // METHODS
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        /// <summary>
        /// The index of this Encoder 0-15
        /// </summary>
        public int TargetIndex
        {
            set { this.targetIndex = value; }
            get { return this.targetIndex; }
        }


        /// <summary>
        /// Indication to Close The form
        /// </summary>
        public bool Close
        {
            set { this.close = value; }
            get { return close; }
        }
        /// <summary>
        /// Indication to Open Form
        /// </summary>
        public bool Open
        {
            set { this.open = value; }
            get { return open; }

        }

        #endregion
    }
    #endregion

    #region User Ask Question Event Args class
    // we have to Open Form to ask the user if the Test is Ok
    // ----------
    // step 1: EventArgs definition
    // ------
    // Type defining the event's information.
    /// <summary>
    /// Class For Sending Question Event To User by the Main form
    /// </summary>
    public class AskUserQuestionArgs : EventArgs
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Constractor for Defualt Sending Question Event To User by the Main form
        /// </summary>
        public AskUserQuestionArgs()
        {
        }
        /// <summary>
        /// Sending Question Event To User by the Main form
        /// </summary>
        /// <param name="message">The message to show the user</param>
        /// <param name="encoderIndex">The encoder index in th system 0-15</param>
        public AskUserQuestionArgs(string message, int encoderIndex)
        {
            this.message = message;
            this.encoderIndex = encoderIndex;
            this.video_audio_test = false;
        }
        /// <summary>
        /// Sending Question Event To User by the Main form
        /// </summary>
        /// <param name="message">The message to show the user</param>
        /// <param name="encoderIndex">The encoder index in th system 0-15</param>
        /// <param name="video_audio_test">Indication taht we test video and audio tests</param>
        public AskUserQuestionArgs(string message, int encoderIndex,bool video_audio_test)
        {
            this.message = message;
            this.encoderIndex = encoderIndex;
            this.video_audio_test = video_audio_test;
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        /// <summary>
        /// The message to show the user
        /// </summary>
        public string Message
        {
            set { this.message = value; }
            get { return this.message; }
        }
        /// <summary>
        /// The irp index in th system 0-15
        /// </summary>
        public int EncoderIndex
        {
            get { return this.encoderIndex; }
            set { this.encoderIndex = value; }
        }
        /// <summary>
        /// The timeOut in seconds
        /// </summary>
        public int TimeOut_Seconds
        {
            get { return this.timeOut_Seconds; }
            set { this.timeOut_Seconds = value; }
        }
        /// <summary>
        /// The timeOut in Minutes
        /// </summary>
        public int TimeOut_Minutes
        {
            get { return this.timeOut_Minutes; }
            set { this.timeOut_Minutes = value; }
        }
        /// <summary>
        /// The timeOut in Hours
        /// </summary>
        public int TimeOut_Hours
        {
            get { return this.timeOut_Hours; }
            set { this.timeOut_Hours = value; }
        }
        /// <summary>
        /// Inidication that we open Video & Audio Test Form
        /// </summary>
        public bool Video_Audio_Test
        {
            get { return this.video_audio_test; }
            set { this.video_audio_test = value; }
        }
        /// <summary>
        /// Inidication that we fail the test with Main Form or with current thread
        /// </summary>
        public bool Fail_The_test_With_Main_Form
        {
            get { return this.fail_The_test_With_Main_Form; }
            set { this.fail_The_test_With_Main_Form = value; }
        }
        /// <summary>
        /// Indication to Open Form
        /// </summary>
        public bool Open_Form
        {
            set { this.FOpen_Form = value; }
            get { return this.FOpen_Form; }
        }
        /// <summary>
        /// Indication to show Allarm
        /// </summary>
        public bool Show_Allarm
        {
            set { this.FShow_Allarm = value; }
            get { return this.FShow_Allarm; }
        }
        /// <summary>
        /// The text that will be on the Allarm
        /// </summary>
        public string Allarm
        {
            set { this.FAllarm = value; }
            get { return this.FAllarm; }
        }
        /// <summary>
        /// Indication that we test 24v power feeding 
        /// </summary>
        public bool Power_feeding_test
        {
            set { this.power_feeding_test = value; }
            get { return this.power_feeding_test; }
        }

        #endregion

        #region Fields
        // FIELDS
        // ------
        private bool FOpen_Form = true;
        private bool FShow_Allarm = false;
        private string FAllarm = "";
        private string message = "";
        private int encoderIndex = 0;
        private int timeOut_Seconds = 0;
        private int timeOut_Minutes = 0;
        private int timeOut_Hours = 0;
        private bool video_audio_test = false;
        private bool fail_The_test_With_Main_Form = true;
        private bool power_feeding_test = false;
        #endregion
    }
    // step 2: Delegate type defining the prototype of the callback method
    // ------  that receivers must implement
    /// <summary>
    /// delegate for call Back method for  Sending Question Event To User by the Main form
    /// </summary>
    public delegate void AskUserQuestionEventHandler(Object sender, AskUserQuestionArgs args);
    #endregion

    #region Serial Number Args Class
    // step 1: EventArgs definition
    // ------
    // Type defining the event's information.
    /// <summary>
    /// Class For Sending Events of Serial Number To Main form
    /// </summary>
    public class SerialNumberEventArgs : EventArgs
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        public SerialNumberEventArgs()
        {
        }
        /// <summary>
        /// Class For Sending Events of Serial Number To Main form
        /// </summary>
        /// <param name="serialNumber">IRP Serial Number</param>
        /// <param name="index">IRP Index in the system 0-15</param>
        public SerialNumberEventArgs(string serialNumber, int index)
        {
            this.FserialNumber = serialNumber;
            this.IRP_Index = index;
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        /// <summary>
        /// IRP Serial Number
        /// </summary>
        public string SerialNumber
        {
            get { return FserialNumber; }
            set { this.FserialNumber = value; }
        }
        /// <summary>
        /// IRP Index in the system 0-15
        /// </summary>
        public int IRP_Index
        {
            get { return FIRP_Index; }
            set { this.FIRP_Index = value; }
        }

        #endregion

        #region Fields
        // FIELDS
        // ------
        private string FserialNumber = "";
        private int FIRP_Index = 0;
        #endregion
    }
    #endregion

    #region Parallel Gpi Test Event Args
    /// <summary>
    /// Hold data on Parallel Gpi Test
    /// </summary>
    public class Parallel_Gpi_TestEventArgs
    {
        #region Fields
        // FIELDS
        // ------
        private string picture_box_name = "";
        private bool status = false;
        private bool _Open_Form = false;
        private bool _Close_Form = false;
        private bool update_picture_box = true;
        private int data = 0;
        #endregion

        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Hold data on Parallel Gpi Test
        /// </summary>
        public Parallel_Gpi_TestEventArgs()
        {

        }
        /// <summary>
        /// Hold data on Parallel Gpi Test
        /// </summary>
        /// <param name="_BitField">The Flag input of this Parallel Port</param>
        /// <param name="picture_box_name">The Name of this Pixture Box of the GPI Tester Form</param>
        /// <param name="status">Indication to status of the Bit</param>
        public Parallel_Gpi_TestEventArgs(string picture_box_name, bool status)
        {
            this.status = status;
            this.picture_box_name = picture_box_name;
        }
        #endregion

        #region Methods
        // METHODS
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        /// <summary>
        /// The Name of this Pixture Box of the GPI Tester Form 
        /// </summary>
        public string Picture_box_name
        {
            set { this.picture_box_name = value; }
            get { return this.picture_box_name; }
        }
        /// <summary>
        /// Indication to status of the Bit
        /// </summary>
        public bool Status
        {
            set { this.Status = value; }
            get { return this.status; }
        }
        /// <summary>
        /// Indication to Open th GPI Form
        /// </summary>
        public bool Open_Form
        {
            set { this._Open_Form = value; }
            get { return this._Open_Form; }
        }
        /// <summary>
        /// Indication to Close th GPI Form
        /// </summary>
        public bool Close_Form
        {
            set { this._Close_Form = value; }
            get { return this._Close_Form; }
        }
        /// <summary>
        /// Indication that We update the Picture Box or the inputs
        /// </summary>
        public bool Update_picture_box
        {
            set { this.update_picture_box = value; }
            get { return this.update_picture_box; }
        }
        /// <summary>
        /// The Value of the Out Put
        /// </summary>
        public int Data
        {
            set { this.data = value; }
            get { return this.data; }
        }
        #endregion
    }
    #endregion

    #region delegate to open Update Parallel Gpio Test Form
    /// <summary>
    /// delegate to open Update Parallel Gpio Test Form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate void Parallel_Gpi_TestEventHandler(object sender, Parallel_Gpi_TestEventArgs e);
    #endregion

    #region Ini Test Event Args class
    // step 1: EventArgs definition
    // ------
    // Type defining the event's information for Run Commands From Ini file.
    /// <summary>
    /// Event Args class for information of Status Test Step
    /// If there is a Fail in encoder we add the Fail parameters
    /// </summary>
    public class IniTestEventArgs : EventArgs
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Event Args class for information of Status Test Step that Run Commands From Ini file
        /// If there is a Fail in encoder we add the Fail parameters
        /// </summary>
        public IniTestEventArgs()
        {
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        /// <summary>
        /// Current Test name 
        /// </summary>
        public string TestName
        {
            set { this.testName = value; }
            get { return this.testName; }
        }
        /// <summary>
        /// Current Test index in format test1-k 
        /// </summary>
        public string TestIndex
        {
            set { this.testIndex = value; }
            get { return this.testIndex; }
        }
        /// <summary>
        /// Current Step in the test
        /// </summary>
        public string TestStepName
        {
            set { this.testStepName = value; }
            get { return this.testStepName; }
        }
        /// <summary>
        /// The Color type of the Test Step
        /// </summary>
        public LogType ColorType
        {
            set { this.colorType = value; }
            get { return this.colorType; }
        }
        /// <summary>
        /// The index of the Unit that fail
        /// </summary>
        public int IrdIndex
        {
            set { this.irdIndex = value; }
            get { return this.irdIndex; }
        }
        /// <summary>
        /// The index of the Modulation in the script
        /// </summary>
        public int ModulationIndex
        {
            set { this.modulationIndex = value; }
            get { return this.modulationIndex; }
        }
        /// <summary>
        /// The index of the Power Level index in the script
        /// </summary>
        public int PowerIndex
        {
            set { this.powerIndex = value; }
            get { return this.powerIndex; }
        }
        /// <summary>
        /// The index of the SymboleRate in the script
        /// </summary>
        public int SymboleRateIndex
        {
            set { this.symboleRateIndex = value; }
            get { return this.symboleRateIndex; }
        }
        /// <summary>
        /// The index of the Fec in the script
        /// </summary>
        public int FecIndex
        {
            set { this.fecIndex = value; }
            get { return this.fecIndex; }
        }

        /// <summary>
        /// The index of the Step in this test
        /// </summary>
        public string TestStepIndex
        {
            set { this.testStepIndex = value; }
            get { return this.testStepIndex; }
        }
        /// <summary>
        /// Indicate that is fail in IRD
        /// </summary>
        public bool FailIRD
        {
            set { this.failIRD = value; }
            get { return this.failIRD; }
        }
        /// <summary>
        /// The fail Reason
        /// </summary>
        public string FailReason
        {
            set { this.failReason = value; }
            get { return this.failReason; }
        }
        /// <summary>
        /// The Time that the encoder fail
        /// </summary>
        public string CurrentTime
        {
            set { this.currentTime = value; }
            get { return this.currentTime; }
        }
        /// <summary>
        /// The Current Temprature inside IRD
        /// </summary>
        public string IRD_CurrentTemprature
        {
            set { this.irdCurrentTemprature = value; }
            get { return this.irdCurrentTemprature; }
        }
        public string OvenCurrentTemp
        {
            set { this.ovenCurrentTemp = value; }
            get { return this.ovenCurrentTemp; }
        }
        /// <summary>
        /// Indicate if the Font is Bold
        /// </summary>
        public bool FontBold
        {
            set { this.fontBold = value; }
            get { return this.fontBold; }
        }
        /// <summary>
        /// Indicate if update Test
        /// </summary>
        public bool UpdateTest
        {
            set { this.updateTest = value; }
            get { return this.updateTest; }
        }
        /// <summary>
        /// Indicate if update Step
        /// </summary>
        public bool UpdateStep
        {
            set { this.updateStep = value; }
            get { return this.updateStep; }
        }
        /// <summary>
        /// Indicate if update Intilization Step
        /// </summary>
        public bool UpdateIntilizationStep
        {
            set { this.updateIntilizationStep = value; }
            get { return this.updateIntilizationStep; }
        }
        /// <summary>
        /// Current Test name 
        /// </summary>
        public DateTime CurrentDateTime
        {
            set { this.currentDateTime = value; }
            get { return this.currentDateTime; }
        }
        /// <summary>
        /// Indication to Update the Modulation Tree Node
        /// </summary>
        public bool UpdateModulation
        {
            get { return _UpdateModulation; }
            set { _UpdateModulation = value; }
        }
        /// <summary>
        /// Indication to Update the Modulation Tree Node
        /// </summary>
        public bool UpdatePower
        {
            get { return _UpdatePower; }
            set { _UpdatePower = value; }
        }
        /// <summary>
        /// Indication to Update the Fec Tree Node
        /// </summary>
        public bool UpdateFec
        {
            get { return _UpdateFec; }
            set { _UpdateFec = value; }
        }
        /// <summary>
        /// Indication to Update the SymbolRate Tree Node
        /// </summary>
        public bool UpdateSymbolRate
        {
            get { return _UpdateSymbolRate; }
            set { _UpdateSymbolRate = value; }
        }
        /// <summary>
        /// The name of moduclation
        /// </summary>
        public string Modulation
        {
            get { return modulation; }
            set { modulation = value; }
        }
        /// <summary>
        /// The name of Fec
        /// </summary>
        public string Fec
        {
            get { return fec; }
            set { fec = value; }
        }
        /// <summary>
        /// The Name of SymbolRate
        /// </summary>
        public string SymbolRate
        {
            get { return symbolRate; }
            set { symbolRate = value; }
        }
        /// <summary>
        /// The value of power
        /// </summary>
        public string Power
        {
            get { return power; }
            set { power = value; }
        }
        /// <summary>
        /// The value of power
        /// </summary>
        public string Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }
        /// <summary>
        /// The value of power
        /// </summary>
        public int Frequency_Index
        {
            get { return frequency_Index; }
            set { frequency_Index = value; }
        }
        /// <summary>
        /// The number of Front End 1 - 4
        /// </summary>
        public int FrontEndNumber
        {
            get { return frontEndNumber; }
            set { frontEndNumber = value; }
        }
        /// <summary>
        /// The value of Pilot
        /// </summary>
        public string Pilot
        {
            get { return pilot; }
            set { pilot = value; }
        }
        /// <summary>
        /// The value of index Pilot
        /// </summary>
        public int Pilot_Index
        {
            get { return pilot_Index; }
            set { pilot_Index = value; }
        }
        /// <summary>
        /// The value of RollOff
        /// </summary>
        public string RollOff
        {
            get { return rolloff; }
            set { rolloff = value; }
        }
        /// <summary>
        /// The value of index of RollOff
        /// </summary>
        public int RollOff_Index
        {
            get { return rollOff_Index; }
            set { rollOff_Index = value; }
        }
        #endregion

        #region Fields
        // FIELDS
        // ------
        private bool failIRD = false;
        private string failReason = "";
        private string currentTime = "";
        private int irdIndex = -1;
        private int fecIndex = -1;
        private int modulationIndex = -1;
        private int symboleRateIndex = -1;
        private int powerIndex = -1;
        private string testName = "";
        private string testStepName = "";
        private string testStepIndex = "";
        private LogType colorType;
        private bool fontBold = false;
        private bool updateTest = false;
        private bool updateStep = false;
        private bool updateIntilizationStep = false;
        private string testIndex = "";
        private string irdCurrentTemprature = "";
        private string ovenCurrentTemp = "";
        private DateTime currentDateTime = DateTime.Now;
        private bool _UpdateSymbolRate;
        private bool _UpdateFec;
        private bool _UpdatePower;
        private bool _UpdateModulation;
        private string modulation = "";
        private string fec = "";
        private string symbolRate = "";
        private string power = "";
        private string frequency = "";
        private string pilot = "";
        private string rolloff = "";
        private int rollOff_Index = -1;
        private int pilot_Index = -1;
        private int frequency_Index = -1;
        private int frontEndNumber = 0;
        #endregion
    }
    #endregion

    #region Update Low Speed Data Event Args
    public class UpdateLowSpeedDataEventArgs : EventArgs
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Defualt Constractor For Update Low Speed Data Form
        /// </summary>
        public UpdateLowSpeedDataEventArgs()
        {
        }
        /// <summary>
        /// Constractor For Update Low Speed Data Form at start of the Test
        /// </summary>
        /// <param name="startTest">Indication To start Test</param>
        /// <param name="encoderIndex">The index of the encoder</param>
        /// <param name="testName">The test Name</param>
        /// <param name="outPutRate">The Out Put Rate of the encoder</param>
        /// <param name="testPeriod">The period of the test</param>
        public UpdateLowSpeedDataEventArgs(bool startTest, int encoderIndex, string testName, string outPutRate, int testPeriod)
        {
            this.startTest = startTest;
            this.outPutRate = outPutRate;
            this.encoderIndex = encoderIndex;
            this.testName = testName;
            this.testPeriod = testPeriod;
        }
        /// <summary>
        /// Constractor For Update Low Speed Data Form at end of the Test
        /// </summary>
        /// <param name="endTest">Indication To end of the Test</param>
        /// <param name="numOfErrors">The Number of errors</param>
        /// <param name="encoderIndex">The index of the encoder</param>
        /// <param name="testName">The test Name</param>
        public UpdateLowSpeedDataEventArgs(bool endTest, long numOfErrors, int encoderIndex, string testName)
        {
            this.endTest = endTest;
            this.numOfErrors = numOfErrors;
            this.encoderIndex = encoderIndex;
            this.testName = testName;
        }
        /// <summary>
        /// Constractor For Update Low Speed Data Form with The line that was send to encoder/Recive From IRD
        /// </summary>
        /// <param name="line">The line that was send to encoder or Recive from IRD</param>
        /// <param name="lineColor">The color of the line</param>
        /// <param name="encoderIndex">The index of the encoder</param>
        /// <param name="testName">The test Name</param>
        public UpdateLowSpeedDataEventArgs(string line, Color lineColor, int encoderIndex, string testName, bool updateEncoder, bool updateIrd, long numOfErrors)
        {
            this.numOfErrors = numOfErrors;
            this.encoderIndex = encoderIndex;
            this.testName = testName;
            this.line = line;
            this.lineColor = lineColor;
            this.updateIrd = updateIrd;
            this.updateEncoder = updateEncoder;
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        /// <summary>
        /// The string that came from Ird Port/Send to Encoder
        /// </summary>
        public string Line
        {
            set { this.line = value; }
            get { return this.line; }
        }
        /// <summary>
        /// Indication To update Encoder Text Box
        /// </summary>
        public bool UpdateEncoder
        {
            set { this.updateEncoder = value; }
            get { return this.updateEncoder; }
        }
        /// <summary>
        /// Indication To update IRD Text Box
        /// </summary>
        public bool UpdateIrd
        {
            set { this.updateIrd = value; }
            get { return this.updateIrd; }
        }
        /// <summary>
        /// The number of error of this test
        /// </summary>
        public long NumOfErrors
        {
            set { this.numOfErrors = value; }
            get { return this.numOfErrors; }
        }

        /// <summary>
        /// The test Name
        /// </summary>
        public string TestName
        {
            set { this.testName = value; }
            get { return this.testName; }
        }

        /// <summary>
        /// Indication for end of test
        /// </summary>
        public bool EndTest
        {
            set { this.endTest = value; }
            get { return this.endTest; }
        }
        /// <summary>
        /// Indication for start of test
        /// </summary>
        public bool StartTest
        {
            set { this.startTest = value; }
            get { return this.startTest; }
        }

        /// <summary>
        /// The color of the line 
        /// </summary>
        public Color LineColor
        {
            set { this.lineColor = value; }
            get { return this.lineColor; }
        }
        /// <summary>
        /// The index of the encoder 1...Max encoders
        /// </summary>
        public int EncoderIndex
        {
            set { this.encoderIndex = value; }
            get { return this.encoderIndex; }
        }
        /// <summary>
        /// The Out Put Rate of the encoder
        /// </summary>
        public string OutPutRate
        {
            set { this.outPutRate = value; }
            get { return this.outPutRate; }
        }
        /// <summary>
        /// The period of the test
        /// </summary>
        public int TestPeriod
        {
            set { this.testPeriod = value; }
            get { return this.testPeriod; }
        }
        /// <summary>
        /// The test baud rate
        /// </summary>
        public string BaudRate
        {
            set { this.baudRate = value; }
            get { return this.baudRate; }
        }

        #endregion

        #region Fields
        // FIELDS
        // ------
        private string line = "";
        private bool updateEncoder = false;
        private bool updateIrd = false;
        private bool endTest = false;
        private bool startTest = false;
        private System.Drawing.Color lineColor = Color.Black;
        private int encoderIndex = 1;
        private string testName = "";
        private long numOfErrors = 0;
        private string outPutRate = "";
        private int testPeriod = 0;
        private string baudRate = "";
        #endregion
    }
    #endregion

    #region delegate to open 8Vsb Params Form
    /// <summary>
    /// delegate to open Main Board SN Form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate bool OpenModulatorSNFormEventHandler(object sender);
    #endregion
}



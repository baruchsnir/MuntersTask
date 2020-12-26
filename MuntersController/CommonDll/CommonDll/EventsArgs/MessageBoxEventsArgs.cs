using System;
using System.Windows.Forms;

namespace FT.CommonDll
{
	/// <summary>
	/// Summary description for MessageBoxEvents.
	/// Send the parametesr to Forms from all classes
	/// </summary>
	public class MessageBoxEventsargs : EventArgs
	{
        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Argumenst of the message box to show
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="ex">The exception of this error</param>
        /// <param name="capture">The caprure of the message</param>
        /// <param name="typebuttons">The buttons to display</param>
        /// <param name="typeicon">The icon to display</param>
        public MessageBoxEventsargs(string message, Exception ex, string capture, MessageBoxButtons typebuttons, MessageBoxIcon typeicon)
        {
            this.message = message;
            this.capture = capture;
            this.typeicon = typeicon;
            this.typebuttons = typebuttons;
            this.errorExeption = ex;
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        /// <summary>
        /// The message to display
        /// </summary>
        public string Message
        {
            set { this.message = value; }
            get { return this.message; }
        }
        /// <summary>
        /// The caprure of the message
        /// </summary>
        public string Capture
        {
            set { this.capture = value; }
            get { return this.capture; }
        }
        /// <summary>
        /// The icon to display
        /// </summary>
        public MessageBoxIcon TypeIcon
        {
            set { this.typeicon = value; }
            get { return this.typeicon; }
        }
        /// <summary>
        /// The buttons to display
        /// </summary>
        public MessageBoxButtons TypeButtons
        {
            set { this.typebuttons = value; }
            get { return this.typebuttons; }
        }
        /// <summary>
        /// The exception of this error
        /// </summary>
        public Exception ErrorExeption
        {
            set { this.errorExeption = value; }
            get { return this.errorExeption; }
        }
        #endregion

        #region Fields
        // FIELDS
        // ------
        private Exception errorExeption = null;
        private string message = "";
        private string capture = "";
        private MessageBoxIcon typeicon;
        private MessageBoxButtons typebuttons;
        #endregion
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace FT.CommonDll
{
	public class WriteEvenLog
	{
		
        #region Constractor
        // CONSTRACTOR
        // ---
        public WriteEvenLog()
        {

        }
        #endregion

        #region Destractor
        // DESTRACTOR
        // ------
        /// <summary>
        /// Distractor for Io WriteEvenLog
        /// </summary>
        protected void Dispose()
        {
            try
            {
            }
            catch (Exception) { ;}
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        #endregion

        #region Methods
        // METHODS
        // ----------
        #region Write
        /// <summary>
        /// Writr message and Exeption to EventLog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Write(string message, Exception ex)
        {
            // Create the source, if it does not already exist.
            if (!EventLog.SourceExists("Tuner Errors"))
            {
                EventLog.CreateEventSource("Tuner Errors", "Tuner Errors");
            }
            EventLog MyLog = new EventLog();
            MyLog.Source = "Tuner Errors";

            if (ex != null)
                MyLog.WriteEntry("Message : " + message + "\nExeption Message : " + ex.Message + "\nExeption StackTrace : " + ex.StackTrace);
            else
                MyLog.WriteEntry("Message : " + message);


        }
        #endregion
        #endregion

        #region Fields
        // FIELDS
        // ------

       #endregion
	} 
}

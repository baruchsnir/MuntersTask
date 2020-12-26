
#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
#endregion

namespace FT.CommonDll
{
    public class MyMessageBox
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        public MyMessageBox()
        {
        }
        #endregion

        #region Methods
        // METHODS
        // ----------
        #region Show regular message box
        /// <summary>
        /// Show regular message box
        /// </summary>
        /// <param name="message"></param>
        public static void Show(string message)
        {
            MessageBox.Show(message);
        }
        #endregion

        #region Show regular message box
        /// <summary>
        /// Show regular message box with exception
        /// </summary>
        /// <param name="message"></param>
        public static void Show(string message,Exception ex,object sender)
        {
           // SendMessageBox.SendSmallMessage(message, ex);
            //MessageBox.Show(message+"\n"+ex.Message);
            Layers_Handler.instance().sendMessageBox(message, ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error, sender);
        }
        #endregion

        #region Show regular message box and caption
        /// <summary>
        /// Show regular message box and caption
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption">The caption of this message</param>
        public static void Show(string message,string caption)
        {
            MessageBox.Show(message,caption);
        }
        #endregion

        #region Show regular message box and caption and error
        /// <summary>
        /// Show regular message box and caption with error from exception
        /// The exception data is store in EventLog as MyMusicStore
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption">The caption of this message</param>
        public static void Show(string message,Exception ex,Log _log)
        {
            if (ex != null)
            {
                string caption = "IRD Mudulation Tester Error";
                MessageBox.Show("The message : " + message + "\nException Message : " + ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                EventLog myEventLog = new EventLog();
                if (!EventLog.SourceExists("Ellipse_Atp_Error"))
                {
                    EventLog.CreateEventSource("Ellipse_Atp_Error", "Ellipse_Atp_Error");
                }
                myEventLog.Source = "Ellipse_Atp_Error";
                myEventLog.WriteEntry("The message : " + message + "\nException Message : " + ex.Message + "\nException StackTrace : " + ex.StackTrace);
                _log.AddLine(null, "The message : " + message + "\nException Message : " + ex.Message + "\nException StackTrace : " + ex.StackTrace, LogType.ltError, ""); 
            }
            else
            {
                string caption = "IRD Mudulation Tester Error";
                MessageBox.Show("The message : " + message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #endregion
    }
}

using System;
using System.Collections;
using System.Windows.Forms;


namespace FT.CommonDll
{


	/// <summary>
	/// Summary description for Singlton Time_Handler class.
	/// </summary>
	[Serializable]
	public class Layers_Handler
	{

		// STATIC method that will be call by evry one
		// and will hold 1 instance of program manager 
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
            }
			catch(Exception){;}
		}
		#endregion

        #region Methods
		// METHODS
		// ----------
        #region Event Raising when MessageBox
        //step 3 : The event definition
        //------  
        /// <summary>
        /// Event for call Back method for MessageBox
        /// </summary>
        private event MessageBoxEventsHandler MessageBoxEven;

        #region sendMessageBox
        /// <summary>
        /// Send Message box Message to all forms to show the problem
        /// </summary>
        /// <param name="message">The messag to show</param>
        /// <param name="capture">The capture of the message box</param>
        /// <param name="buttons">The buttons that will be on the message box</param>
        /// <param name="icon">The type of the icon like error or information</param>
        public void sendMessageBox(string message, string capture, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            MessageBoxEventsargs args = new MessageBoxEventsargs(message, capture, buttons, icon);
            if (MessageBoxEven != null)
                MessageBoxEven(this, args);
        }
        #endregion
        #endregion


        #endregion

		#region Properties
		// PROPERTIES
		// ----------
        public static Layers_Handler instance()
        {
			if (srviceHandler == null)
                srviceHandler = new Layers_Handler();
            return srviceHandler;
		}
		#endregion

		#region Fields
		// FIELDS
		// ------
        private static Layers_Handler srviceHandler;
		#endregion
	}
}

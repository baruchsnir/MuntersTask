using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace FT.CommonDll
{
    public class ProcessHiden
    {
        #region Hide Process
        public static void Hide_Process(string processNmae, Process p)
        {
            Window.ShowWindowAsync(p.MainWindowHandle, SW_HIDE);
        }
        #endregion

        #region Show Process
        public static void Show_Process(string processNmae, Process p)
        {
            Window.ShowWindowAsync(p.Handle, SW_SHOWNORMAL);
        }
        #endregion

        #region Win32 API Constants
        /// <summary>
        /// Win32 API Constants for ShowWindowAsync()
        /// </summary>
        private static  int SW_HIDE = 0;
        private static  int SW_SHOWNORMAL = 1;
        private static  int SW_SHOWMINIMIZED = 2;
        private static  int SW_SHOWMAXIMIZED = 3;
        private static  int SW_SHOWNOACTIVATE = 4;
        private static  int SW_RESTORE = 9;
        private static  int SW_SHOWDEFAULT = 10;
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing.Imaging;
using System.Diagnostics;
using CodeReflection.ScreenCapturingDemo;
using System.Drawing;

namespace FT.CommonDll
{
    public class PrintMainWindow
    {
        #region PrintMAinWindow
        /// <summary>
        /// Print The main window
        /// </summary>
        /// <param name="handl">The handle of this windows</param>
        public static void Print_Main_Window(IntPtr handl)
        {
            ScreenCaptureWin2000 w2k;
            string RunPAth = Layers_Handler.instance().LogsDirectory; 
            string newName =RunPAth+ @"\Test.jpg";
            Image map;
            if (handl != IntPtr.Zero)
            {
                map = ScreenCapturing.GetWindowCaptureAsBitmap(handl);
                if (map != null)
                {
                    map.Save(newName, ImageFormat.Jpeg);
                }
                else
                {
                    w2k = new ScreenCaptureWin2000();
                    map = w2k.CaptureWindow(handl);
                    if (map != null)
                    {
                        map.Save(newName, ImageFormat.Jpeg);
                    }
                }
            }


        }
        #endregion
    }
}

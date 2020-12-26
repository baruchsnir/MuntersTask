using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuntersController
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                frmMuntersController mainForm = new frmMuntersController(args);
                Application.Run(mainForm);
            }
            catch(Exception ex)
            {
                if (!EventLog.SourceExists("Munters Controller"))
                {
                    EventLog.CreateEventSource("Munters Controller", "Munters Controller");
                }
                EventLog MyLog = new EventLog();
                MyLog.Source = "Munters Controller";
                MyLog.WriteEntry("Message : " + ex.Message + "\nStackTrace : " + ex.StackTrace+"\n");


            }
        }
    }
}

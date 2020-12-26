using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

using System.Windows.Forms;
using System.IO;


namespace EventLogWriter
{
    [RunInstaller(true)]
    public partial class InstallerGet_EvenLog : Installer
    {

        public InstallerGet_EvenLog()
        {
            InitializeComponent();
        }

        string sLog = "Munters Controller";
        string sSource = "Munters Controller";

        public override void Install(IDictionary savedState)
        {
            string temp = "";
            FT.CommonDll.Ini.IniFile ini = new FT.CommonDll.Ini.IniFile(Application.StartupPath + @"\setup.ini");
            if (File.Exists(Application.StartupPath + @"\setup.ini"))
            {
                temp = ini.ReadString("General", "Even Log Source", "");
            }
            if (temp == "")
            {

                temp = sLog;
                ini.WriteString("General", "Even Log Source", sLog);

            }
            sLog = temp;
            sSource = temp;
            base.Install(savedState);

            // Create the source, if it does not already exist.
            if (!System.Diagnostics.EventLog.SourceExists(sSource))
            {
                System.Diagnostics.EventLog.CreateEventSource(sSource, sLog);
            }
        }


        public override void Uninstall(IDictionary savedState)
        {

            // Delete the source, if it exists.
            if (System.Diagnostics.EventLog.SourceExists(sSource))
            {
                System.Diagnostics.EventLog.DeleteEventSource(sSource);
                System.Diagnostics.EventLog.Delete(sLog);
            }

            base.Uninstall(savedState);
        }

    }
}

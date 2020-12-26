using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;


namespace FT.CommonDll
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        
        public Installer1()
        {
            InitializeComponent();
        }

        string sLog = "Munters Controller";
        string sSource = "Munters Controller";

        public override void Install(IDictionary savedState)
        {
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

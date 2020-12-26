using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace FT.CommonDll
{
    /// <summary>
    /// Deleagte For sending string
    /// </summary>
    /// <param name="line">The string to send</param>
    public delegate void SendStringEventHandler(string line);
    /// <summary>
    /// Deleagte For sending Several strings
    /// </summary>
    /// <param name="line">The string to send</param>
    public delegate void SendSeveralStringEventHandler(string[] line);
    /// <summary>
    /// Deleagte For sending some string and Some Color
    /// </summary>
    /// <param name="line">The string to send</param>
    /// <param name="color">The color of this line</param>
    public delegate void SendStringWithColorEventHandler(string line, Color color);

    #region delegate Ini Test Steps That run from Preset Script File file
    /// <summary>
    /// delegate Ini Test Steps That run from Preset Script File file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void IniTestEventHandler(object sender, IniTestEventArgs args);
    #endregion

    #region Terminal Event Handler
    // step 2: Delegate type defining the prototype of the callback method
    // ------  that receivers must implement
    public delegate void TerminalEventHandler(Object sender, string line, LogType logType);
    #endregion

    #region Display Event Handler
    // step 2: Delegate type defining the prototype of the callback method
    // ------  that receivers must implement
    public delegate void DisplayEventHandler(Object sender, DisplayEventArgs args);
    #endregion

    #region BetsEventHandler
    public delegate void BetsEventHandler(Object sender, BetsEventArgs args);
    #endregion
}

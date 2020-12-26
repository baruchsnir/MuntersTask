using System;
using System.Windows.Forms; 
using System.Collections;
using System.Data;
using System.Threading;
using System.IO;
using FT.CommonDll.Ini;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;

namespace FT.CommonDll
{
    public enum TStopReason
    {
        srUser = 0, srFatalError
    }
    /// <summary>
	/// Collection of Tools Functions.
	/// </summary>
	public class UTools
	{
		#region Constractor
		// CONSTRACTOR
		// ------
		/// <summary>
		/// Summary description for UTools Constractor.
		/// </summary>
		public UTools()
		{
		}
		#endregion

		#region Methods
		// METHODS
        // ----------
        #region FileExists
        public bool FileExists(string fileName)
		{ 
			FileInfo file = new FileInfo(fileName);
			if (file != null)
			  return file.Exists;
			else
			  return false;
      }
      #endregion

        #region StrToIntDef
      public int StrToIntDef(string str,int defaultInt)
		{
			try
			{
               return int.Parse(str);
			}
			catch{return defaultInt;}
        }
      #endregion

        #region Abs
        public int Abs(int number)
		{
           if (number < 0 )
		     return -1*number;
		   return number;
       }
        #endregion

        #region LastCharPos
       public static int LastCharPos(char C,string S)
		{
			int I;
		
			I = S.Length-1;
			do
			{
			}
			while ((I > 0) && (S[I--] != C));
			return I;
        }
       #endregion

        #region StrToken
        public string StrToken(string S,string LeftStr,string RightStr)
		{
			int Index, Count, P;
			string TempStr ;
			string Result = "";
			try
			{
				 // Left side
				 if (LeftStr == "")
					 Index = 1;
				 else
				 {
					 P = S.IndexOf(LeftStr);
					 if (P < 0)
						 return Result;
					 else
						 Index = P + LeftStr.Length;
				 }

				 TempStr = S.Substring(Index,S.Length - Index);

				 // Right side
				 if (RightStr == "")
					 Count = S.Length;
				 else
				 {
					 P = TempStr.IndexOf(RightStr);
					 if (P == 0 )
						 return Result;
					 else
						 Count = P;
				 }

				 Result = S.Substring(Index, Count);
			}
			catch(Exception ex)
			{
                System.Diagnostics.Trace.WriteLine("Problem in file UTools method StrToken " + ex.Message+"\n"+ex.StackTrace);
                //sendMessageBox("Problem in file UTools method StrToken ",ex,"Error Alarm",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			return Result;
        }
        #endregion

        #region Val
        public void Val(string S,ref int  V,ref int Code)
		{
			Code = 0;
			try
			{
				V = Convert.ToInt32(S);
			}
			catch{Code = 999;}
        }
        #endregion

        #region StrIsInt
        public bool StrIsInt(string S)
		{
			int Value=0, Code=0;
			Val(S,ref Value,ref Code);
			return (Code == 0);
        }
        #endregion

        #region StrIsSN
        public bool StrIsSN(string S)
		{
			int I;
			bool Result;
			if (S.Length > 0)
			{
				Result = true;
				for( I = 0 ;I <  S.Length;I++)
				{
					if (! ((S[I] <= '9')&&(S[I] >= '0') || ((S[I] <= 'Z')&&(S[I] >= 'A'))))
					{
						Result = false;
						return Result;
					}
				}
			}
			else
				Result = false;
			return Result;
        }
        #endregion

        #region IntToHex
        /// <summary>
        /// Convert int to Hex with format of 4 digits
        /// </summary>
        /// <param name="number">The number to convert</param>
        /// <returns>return string of int as hex format</returns>
		public string IntToHex(int number)
		{
			return String.Format("{0:x4}", number);
        }
        #endregion

        #region IntToHex
        /// <summary>
        /// Convert int to Hex with format of 4 digits
        /// </summary>
        /// <param name="number">The number to convert</param>
        /// <returns>return string of int as hex format</returns>
        public string IntToHex(long number)
        {
            return String.Format("{0:x4}", number);
        }
        #endregion

        #region Int To Hex
        /// <summary>
        /// Convert int to Hex with format of number of given digits
        /// </summary>
        /// <param name="number">The number to convert</param>
        /// <param name="Digits">The number of Digits to show</param>
        /// <returns>return string of int as hex format</returns>
		public string IntToHex(long number,int Digits)
		{
			return String.Format("{0:x"+Digits.ToString()+"}", number);
		}
        /// <summary>
        /// Convert int to Hex with format of number of given digits
        /// </summary>
        /// <param name="number">The number to convert</param>
        /// <param name="Digits">The number of Digits to show</param>
        /// <returns>return string of int as hex format</returns>
        public string IntToHex(int number, int Digits)
        {
            return String.Format("{0:x" + Digits.ToString() + "}", number);
        }
		public int HexToInt(string hexString)
		{
            //string temp =hexString;
            //if (hexString.IndexOf("0x")> -1) temp = hexString.Substring(2);
            //return int.Parse(temp, 
            //    System.Globalization.NumberStyles.HexNumber, null);
            if (hexString.IndexOf("0x") == 0)
                hexString = hexString.Substring(2);
            int num = 0;
            int hexint =0;
            try
            {
                //num = int.Parse(hexString,System.Globalization.NumberStyles.HexNumber, null);
                //  This method converts a hexvalues string as 80FF into a integer.
                //	Note that you may not put a '#' at the beginning of string! There
                //  is not much error checking in this method. If the string does not
                //  represent a valid hexadecimal value it returns 0.
                int counter;
                char[] hexarr;
                hexint = 0;
                
                hexString = hexString.ToUpper();
                hexarr = hexString.ToCharArray();
                for (counter = hexarr.Length - 1; counter >= 0; counter--)
                {
                    if ((hexarr[counter] >= '0') && (hexarr[counter] <= '9'))
                    {
                        hexint += (hexarr[counter] - 48) * ((int)(Math.Pow(16, hexarr.Length - 1 - counter)));
                    }
                    else
                    {
                        if ((hexarr[counter] >= 'A') && (hexarr[counter] <= 'F'))
                        {
                            hexint += (hexarr[counter] - 55) * ((int)(Math.Pow(16, hexarr.Length - 1 - counter)));
                        }
                        else
                        {
                            hexint = 0;
                            break;
                        }
                    }
                }

            }
            catch { ;}
            return hexint;

        }

        public long HexToLong(string hexString)
        {
            //string temp =hexString;
            //if (hexString.IndexOf("0x")> -1) temp = hexString.Substring(2);
            //return int.Parse(temp, 
            //    System.Globalization.NumberStyles.HexNumber, null);
            if (hexString.IndexOf("0x") == 0)
                hexString = hexString.Substring(2);
            int num = 0;
            long hexint = 0;
            try
            {
                //num = int.Parse(hexString,System.Globalization.NumberStyles.HexNumber, null);
                //  This method converts a hexvalues string as 80FF into a integer.
                //	Note that you may not put a '#' at the beginning of string! There
                //  is not much error checking in this method. If the string does not
                //  represent a valid hexadecimal value it returns 0.
                int counter;
                char[] hexarr;
                hexint = 0;
                hexint = long.Parse(hexString, System.Globalization.NumberStyles.HexNumber, null);
                //hexString = hexString.ToUpper();
                //hexarr = hexString.ToCharArray();
                //for (counter = hexarr.Length - 1; counter >= 0; counter--)
                //{
                //    if ((hexarr[counter] >= '0') && (hexarr[counter] <= '9'))
                //    {
                //        hexint += (hexarr[counter] - 48) * ((int)(Math.Pow(16, hexarr.Length - 1 - counter)));
                //    }
                //    else
                //    {
                //        if ((hexarr[counter] >= 'A') && (hexarr[counter] <= 'F'))
                //        {
                //            hexint += (hexarr[counter] - 55) * ((int)(Math.Pow(16, hexarr.Length - 1 - counter)));
                //        }
                //        else
                //        {
                //            hexint = 0;
                //            break;
                //        }
                //    }
                //}

            }
            catch { ;}
            return hexint;

        }
        #endregion

        #region  Str100ToFloat
        public float Str100ToFloat(string S)
		{
		    float E ;
			try
			{
				E = Convert.ToInt32(S);
				return  E / 100;
			}
			catch{return 0;}
        }
        #endregion

        #region FloatToStr100
        public string FloatToStr100(float E)
		{
		   return Convert.ToString(100 * E);
       }
        #endregion

        #region HHMMToMins
       public int HHMMToMins(string HHMM)
		{
			int Hours, Mins,Result=0 ;
				try
				{
					Hours = Convert.ToInt32(HHMM.Substring(0,2));
				}
				catch{Hours=-1;}
				try
				{
					Mins = Convert.ToInt32(HHMM.Substring(3,2));
				}
				catch{Mins=-1;}

				if ((Hours > -1) && (Mins > -1) && (HHMM[2] == ':'))
					Result = Mins + Hours * 60;
				else
				  Result = 0;
			return Result;
        }
       #endregion

        #region MinsToHHMM
        public string MinsToHHMM(int Mins)
		{
			string Result="";
			int div = Mins / 60;
			int mod = Mins - (div * 60);
			if (div < 10 )
				Result = "0"+div.ToString()+":";
			else
               Result = div.ToString()+":";
			if (mod < 10)
				Result += "0"+mod.ToString();
			else
				Result += mod.ToString();
			return Result;
        }
        #endregion

        #region MinsToTime
        public DateTime MinsToTime(int Mins)
		{
		    DateTime Result = new DateTime(0);
            Result = Result.AddMinutes(Mins); 
			return Result;
        }
        #endregion

        #region MsecsToTime
        public DateTime MsecsToTime(int Msecs )
		{
			DateTime Result = new DateTime(0);
			Result = Result.AddMilliseconds(Msecs); 
			return Result;
        }
        #endregion

        #region TimeCodeDelta
        public int TimeCodeDelta(string StartTimecode,string EndTimecode)
		{
		   DateTime StartTime, EndTime;
		   int Result=0;
		   try
           {
			    StartTime = Convert.ToDateTime(StartTimecode);
                EndTime   = Convert.ToDateTime(EndTimecode);
		    	Result = 24 * 60 * (EndTime.Minute - StartTime.Minute);
		   }
		   catch{Result = 0;}
           return Result;
       }
        #endregion

        #region TimeCodeToSec
       public int TimeCodeToSec(string Timecode)
		{
		   int Result=0;
		   DateTime date ;
           try
           {
		      date = Convert.ToDateTime(Timecode);
		      long tictomin = date.Ticks/(100000000);
			  Result = 24 * 60 * 60 *(int)tictomin;
		   }
		   catch{Result = 0;}
           return Result;
       }
       #endregion

        #region CreateLogFileName
       public string CreateLogFileName()
		{
			DateTime date = DateTime.Now; 
			string dd=date.Day.ToString(),mm=date.Month.ToString();
			string hh = date.Hour.ToString(),mmn = date.Minute.ToString();
			if (hh.Length == 1) 
				hh = "0"+hh;
			if (mmn.Length == 1) 
				mmn = "0"+mmn;
			if (dd.Length == 1) 
				dd = "0"+dd;
			if (mm.Length == 1) 
				mm = "0"+mm;

			return GetLogDir() + @"\" +dd+"-" + mm+@"-" + date.Year.ToString()+"_"+hh+mmn+".log";
        }
        #endregion

        #region GetBaseDir
        /// <summary>
        /// Get The Base Dir Where the application run
        /// </summary>
        /// <returns></returns>
		public static string GetBaseDir()
        {
            string CurDir = "", Result = Configure_Path, s = @"\";
            int Len;
            if (Configure_Path == "")
            {
                CurDir = Application.StartupPath;
                Len = LastCharPos(s[0], CurDir) + 1;
                Result = CurDir.Substring(0, Len);
                CurDir = Result;
                return CurDir;
            }
            return Result;
        }
        #endregion

        #region GetRunDir
        public static string GetRunDir()
		{
		    return GetBaseDir() + @"\"+ RUN_DIR ;
        }
        #endregion

        #region GetTestsDir
        public static string GetTestsDir()
		{
		    return GetBaseDir() + @"\"+ TESTS_DIR ;
        }
        #endregion

        #region GetLogDir
        public static string GetLogDir()
		{
		    return GetBaseDir() + @"\"+ LOG_DIR ;
        }
        #endregion

        #region GetMibDir
        public static string GetMibDir()
        {
            return GetBaseDir() + @"\Mibs";
        }
        #endregion

        #region GetLogsDirectory
        public static string GetLogsDirectory()
        {
            string temp ="";
            IniFile iniFile = new IniFile();
            iniFile = new IniFile(GetRunDir() + @"\" + SETUP_FILE);

            temp = iniFile.ReadString("General", "LogsDirectory", "");
            return temp;

        }
        #endregion

        #region LegalFileName
        public bool LegalFileName(string FileName )
		{
			int I;
			string ILLEGAL_CHARS =@"|;\/:*?<>";
			bool Result = false;
			if (FileName.Length == 0)
				return Result;
			for( I = 0 ; I< ILLEGAL_CHARS.Length;I++)
			 if (FileName.IndexOf(ILLEGAL_CHARS[I]) >= 0)
				 return Result;
			if ( FileName.IndexOf((char)0x22) >= 0) 
				 return Result;
			Result = true;
			return Result;
		}
//
//		public bool  GetIPFromHost(ref string HostName,ref string IPaddr,ref string WSAErr )
//		type
//		Name = array[0..100] of Char;
//		PName = ^Name;
//		var
//		HEnt: PHostEnt;
//		HName: PName;
//		WSAData: TWSAData;
//		I: int;
//		{
//		Result = False;
//		if WSAStartup($0101, WSAData) != 0 then
//		{
//			WSAErr = "Winsock is not responding."";
//			Exit;
//		}
//		IPaddr = "";
//		New(HName);
//		if GetHostName(HName^, SizeOf(Name)) = 0 then
//		{
//			HostName = StrPas(HName^);
//			HEnt = GetHostByName(HName^);
//			for I = 0 to HEnt^.h_length - 1 do
//			IPaddr = Concat(IPaddr, IntToStr(Ord(HEnt^.h_addr_list^[I])) + ".");
//			SetLength(IPaddr, Length(IPaddr) - 1);
//			Result = True;
//		end
//		else
//		case WSAGetLastError of
//			WSANOTINITIALISED: WSAErr = "WSANotInitialised";
//			WSAENETDOWN      : WSAErr = "WSAENetDown";
//			WSAEINPROGRESS   : WSAErr = "WSAEInProgress";
//		}
//		Dispose(HName);
//		WSACleanup;
        //		}
        #endregion

        #region DateTimeToStrEx
        public string DateTimeToStrEx(DateTime date)
		{
//			IFormatProvider culture = 
//				new System.Globalization.CultureInfo("fr-HB", true);
//			// Get the short date formats using the "fr-HB" culture.
//			string [] frenchJuly28Formats = 
//				date.GetDateTimeFormats('d', culture);
//			return "";
			int dd = date.Day;
			int mm = date.Month;
			string result,yy = date.ToString();
			int pos = yy.IndexOf(date.Year.ToString());
			yy = yy.Substring(pos,yy.Length - pos);
			if  (dd < 10 )
               result = "0"+ dd.ToString()+"/";
			else
 			   result = dd.ToString()+"/";
			if  (mm < 10 )
				result += "0"+ mm.ToString()+"/"+yy;
			else
				result += mm.ToString()+"/"+yy;

            return result;
		    //return date.Day.ToString()+"/"+date.Month.ToString()++"/"+date.Year.ToString()+" "+date.Hour.ToString()+":"+date.Minute.ToString()+":"+date.Second.ToString()+" "+date.GetDateTimeFormats
//			Result := FormatDateTime('dd"/"mm"/"yyyy" "hh":"mm":"ss AM/PM',DateTime);
        }
        #endregion

        #region StrToTimeSpan
        public TimeSpan StrToTimeSpan(string time)
		{
             string[] times = time.Split(':');
			 int hour=0,min=0,sec=0;
			 try
			 {
				 hour = int.Parse(times[0]);
			 }
			 catch{hour = 0;}
			try
			{
				min = int.Parse(times[1]);
			}
			catch{sec = 0;}
			try
			{
				sec = int.Parse(times[2]);
			}
			catch{hour = 0;}
            if (times.Length == 3)
			   return new TimeSpan(hour,min,sec);
			else
			   return new TimeSpan(0,0,0);
       }
        #endregion

        #region DateTimeToStrExWithMilliseconds
       /// <summary>
        /// Return Date with Millis econds
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string DateTimeToStrExWithMilliseconds(DateTime date)
        {
            int dd = date.Day;
            int mm = date.Month;
            string temp  = date.ToString();
            int mili = date.Millisecond;
            string result, yy;
            int pos = temp.IndexOf(date.Year.ToString());
              temp.Substring(pos, temp.Length - pos);
              yy = temp.Substring(pos, temp.Length - pos - 3) + ":" + mili.ToString() + temp.Substring(temp.Length - 3, 3);
              if (dd < 10)
                result = "0" + dd.ToString()+ "/";
            else
                result = dd.ToString()+"/";
            if (mm < 10)
                result += "0" + mm.ToString() + "/" + yy ;
            else
                result += mm.ToString() + "/" + yy ;

            return result;
        }
       #endregion

        #region ByteToString
        //converter hex string to byte and byte to hex string
        /// <summary>
        /// converter byte to hex string  
        /// </summary>
        /// <param name="InBytes">Byte array</param>
        /// <returns>string from byte array</returns>
        public string ByteToString(byte[] InBytes)
        {
            try
            {
                String StringOut = "";
                Char[] a = new Char[InBytes.Length];
                Array.Copy(InBytes, a, InBytes.Length);
                for (int i = 0; i < InBytes.Length; i++)
                    StringOut += a[i];
                return StringOut;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Problem in HexCon.ByteToString " + ex.Message+"\n"+ex.StackTrace);
                return "";
            }

        }
        #endregion

        #region StringToByte
        /// <summary>
        ///  converter hex string to byte 
        /// </summary>
        /// <param name="InString"></param>
        /// <returns></returns>
        public byte[] StringToByte(string InString)
        {
            try
            {
                byte[] ByteOut = System.Text.ASCIIEncoding.ASCII.GetBytes(InString);
                return ByteOut;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Problem in HexCon.ByteToString \n" + ex.Message+"\n"+ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region GetDateTimeFromE9000Format
        public static string GetDateTimeFromE9000Format(string pOctet)
        {
            try
            {
               
                int u32;
 
                short  pu16; 
                string val = "";
                for (int i = 0; i < pOctet.Length; i++)
                {
                    pu16 = (short)pOctet[i];
                    u32 = (int)(pu16);
                    val += String.Format("{0:x2}", u32);
                }

                string yy, mm, dd, hh, mn, ss, ml,UTC="",UTC_D="",UTC_M="";
                char utc_d= (char)20;
                string date;
                /* year */
                yy = val.Substring(0, 4);
                /* month */
                mm = val.Substring(4, 2);
                 /* day */
                dd = val.Substring(6, 2);
                /* hour */
                hh = val.Substring(8, 2);
                /* minutes */
                mn = val.Substring(10, 2);
                /* seconds */
                ss = val.Substring(12, 2);
                /* tenth of seconds */
                ml = val.Substring(14, 2);
                if (val.Length > 21)
                { 
                    /* direction from UTC */
                    UTC_D = val.Substring(16, 2);
                     /* hours from UTC */
                    UTC = val.Substring(18, 2);
                    /* minutes from UTC */
                    UTC_M = val.Substring(20, 2);
                }
                int Year, Month, Day, Hour, Min, Sec, mils,utc_h=0,utc_m=0;
     
                //if (hexString.IndexOf("0x")> -1) temp = hexString.Substring(2);
                Year = int.Parse(yy, System.Globalization.NumberStyles.HexNumber, null);
                Month = int.Parse(mm, System.Globalization.NumberStyles.HexNumber, null);
                Day = int.Parse(dd, System.Globalization.NumberStyles.HexNumber, null);
                Hour = int.Parse(hh, System.Globalization.NumberStyles.HexNumber, null);
                Min = int.Parse(mn, System.Globalization.NumberStyles.HexNumber, null);
                Sec = int.Parse(ss, System.Globalization.NumberStyles.HexNumber, null);
                mils = int.Parse(ml, System.Globalization.NumberStyles.HexNumber, null);
                if (val.Length > 21)
                {
                    /* direction from UTC */
                    UTC_D = val.Substring(16, 2);
                    /* hours from UTC */
                    UTC = val.Substring(18, 2);
                    /* minutes from UTC */
                    UTC_M = val.Substring(20, 2);
                    utc_d = (char)int.Parse(UTC_D, System.Globalization.NumberStyles.HexNumber, null);
                    utc_m =  int.Parse(UTC_M, System.Globalization.NumberStyles.HexNumber, null);
                    utc_h = int.Parse(UTC, System.Globalization.NumberStyles.HexNumber, null);
                }
                 date = Day + "/" + Month + "/" + Year + " " + Hour + ":" + Min + ":" + Sec + "." + mils + " " + utc_d + utc_h + ":" + utc_m;
                return date;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Problem in Tools in GetDateTimeFromE9000Format " + ex.Message + "\n" + ex.StackTrace);
                return "";
            }
        }
        #endregion

        #region SetDateTimeToE9000Format
        public static string SetDateTimeToE9000Format(DateTime date)
        {
            try
            {
                string system_date_Time;
                string yy = "", mm = "", dd = "", hh = "", mn = "", ss = "", ml = "";
                int u32;

                short pu16;
                string val = "";
                string results = "";
                yy = string.Format("{0:x4}", date.Year);
                mm = string.Format("{0:x2}", date.Month);
                dd = string.Format("{0:x2}", date.Day);
                hh = string.Format("{0:x2}", date.Hour);
                mn = string.Format("{0:x2}", date.Minute);
              //  mn = string.Format("{0:x2", date.Minute);
                ss = string.Format("{0:x2}", date.Second);
                ml = string.Format("{0:x2}", date.Millisecond /10);
               // UTC_D = string.Format("{0:x2}", "+");
                system_date_Time = yy + mm + dd + hh + mn + ss + ml + "2B0200";
                for (int i = 0; i < system_date_Time.Length; i= i+2 )
                {
                    val = system_date_Time.Substring(i,2);
                    u32 = int.Parse(val,System.Globalization.NumberStyles.HexNumber, null);
                    pu16 = (short)u32;
                    results += (char)pu16;
               }
                return results;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Problem in Tools in SetDateTimeToE9000Format " + ex.Message + "\n" + ex.StackTrace);
                return "";
            }

        }
        #endregion

        #region Get_String_Format_Of_Given_Date
        /// <summary>
        /// Build String Format of Giben Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string Get_String_Format_Of_Given_Date(DateTime date)
        {
            string dd = date.Day.ToString(), mm = date.Month.ToString();
            string hh = date.Hour.ToString(), mmn = date.Minute.ToString();
            if (dd.Length == 1)
                dd = "0" + dd;
            if (mm.Length == 1)
                mm = "0" + mm;
            if (hh.Length == 1)
                hh = "0" + hh;
            if (mmn.Length == 1)
                mmn = "0" + mmn;
            string dateS =  dd + ":" + mm + ":" + date.Year.ToString() + " " + hh +":"+ mmn;
            return dateS;
        }
        #endregion
    
        #region Kill Crt Process
        public static void KillCRTProcess(string name)
        {
            Process[] aProcesses = Process.GetProcesses(Environment.MachineName);
            foreach (Process p in aProcesses)
            {
                if (p.Id != Process.GetCurrentProcess().Id)
                {
                    if (p.ProcessName.ToLower().IndexOf(name) == 0)
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch { ;}
                    }
                }
            }
        }
        #endregion 
        
        #region Write_Error_To_event_Log
        public static void Write_Error_To_event_Log(string error)
        {
            // Create the source, if it does not already exist.
            //if (!EventLog.SourceExists("Ellipse_Atp_Error"))
            //{
            //    EventLog.CreateEventSource("Ellipse_Atp_Error", "Ellipse_Atp_Error");
            //}
            //EventLog MyLog = new EventLog();
            //MyLog.Source = "Ellipse_Atp_Error";

            TextWriter LogOutput;
            //MyLog.WriteEntry(error);
            string filename = Layers_Handler.instance().LogsDirectory + @"\event.log";
            if (!File.Exists(filename))
            {
                LogOutput = File.CreateText(filename);
                LogOutput.Close();
            }
            LogOutput = File.AppendText(filename);
            LogOutput.WriteLine(error);
            LogOutput.Close();


        }
        #endregion

        #region Check_Ip_address
        /// <summary>
        /// Check if thsi is ligal Ip address
        /// </summary>
        /// <param name="_IP"></param>
        /// <returns></returns>
        public static bool Check_Ip_address(string _IP)
        {
            try
            {
                if ((_IP != null) && (_IP.Length == 0))
                {
                    return false;
                }
                else
                {
                    string[] lines = _IP.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    int num;
                    if (lines.Length != 4)
                    {
                        return false;
                    }
                    else
                    {

                        num = -1;
                        for (int i = 0; i < 4; i++)
                        {
                            try
                            {
                                num = int.Parse(lines[i]);
                            }
                            catch { num = 256; }
                            if ((num > 255) || (num < 0))
                            {
                                break;
                            }
                        }
                        if ((num == -1) || (num > 255) || (num < 0) || (lines[0] == "0") || (lines[3] == "0"))
                        {
                            return false;
                        }
                        return true;
                    }

                }
            }
            catch (Exception exception)
            {
                Layers_Handler.instance().sendMessageBox("Problem in UTools in method Check_Ip_address", exception, "Error Allarm", MessageBoxButtons.OK, MessageBoxIcon.Error, "utools");
            }
            return false;
        }
        #endregion

        #region Kill Excel Process
        /// <summary>
        /// Kill all excel process
        /// </summary>
        public static void KillExcelProcess()
        {
            try
            {

                Process[] aProcesses = Process.GetProcesses(Environment.MachineName);

                // Get the appSettings.
                NameValueCollection appSettings = ConfigurationManager.AppSettings;

                // Get the collection enumerator.
                IEnumerator appSettingsEnum = appSettings.Keys.GetEnumerator();

                // Loop through the collection and
                // display the appSettings key, value pairs.
                int i = 0;
                Console.WriteLine("App settings.");
                string s1 = "";
                while (appSettingsEnum.MoveNext())
                {
                    string key = appSettings.Keys[i];
                    Console.WriteLine("Name: {0} Value: {1}",
                                      key, appSettings[key]);
                    s1 = string.Format("Name: {0} Value: {1}",
                                      key, appSettings[key]);
                    i += 1;
                }


                foreach (Process p in aProcesses)
                {
                    string strRemark = "";
                    if (p.Id == System.Diagnostics.Process.GetCurrentProcess().Id)
                        // the process id is unique in the system
                        strRemark = " < = my application";
                    else
                        strRemark = "";
                    if (p.Id != Process.GetCurrentProcess().Id)
                    {
                        if (p.ProcessName.ToLower().IndexOf("excel") == 0)
                        {
                            try
                            {
                                p.Kill();
                                Layers_Handler.instance().FLogAddLine("Process " + p.ProcessName + " was kiled", LogType.ltBlue, "CEC");
                            }
                            catch { ;}

                        }
                    }
                }

                //Process[] aProcesses = Process.GetProcesses(Environment.MachineName);
                //foreach (Process p in aProcesses)
                //{
                //    if (p.ProcessName.ToLower().IndexOf("excel") == 0)
                //    {
                //        p.Kill();
                //    }
                //}
            }
            catch (Exception ex)
            {
              Layers_Handler.instance().sendMessageBox("Problem in Utools in KillExcelProcess", ex, "Errora Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion 

        #region GetConfigurationKeyValue
        /// <summary>
        /// Get The Value of Configuration Key in section userSettings
        /// </summary> 
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigurationKeyValue(string key)
        {


            System.Configuration.Configuration config =
               ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Get the collection of the section groups.
            ConfigurationSectionGroupCollection sectionGroups = config.SectionGroups;

            // Show the configuration values

            ClientSettingsSection clientSection;
            SettingValueElement value;

            foreach (ConfigurationSectionGroup group in sectionGroups)
            // Loop over all groups
            {
                if (!group.IsDeclared)
                    // Only the ones which are actually defined in app.config
                    continue;

                if ("userSettings" == group.Name)
                {
                    int counter = 0;
                    // get all sections inside group

                    foreach (ConfigurationSection section in group.Sections)
                    {
                        clientSection = section as ClientSettingsSection;
                        if (clientSection == null)
                            continue;

                        foreach (SettingElement set in clientSection.Settings)
                        {
                            value = set.Value as SettingValueElement;
                            if (set.Name == key)
                            {
                                return value.ValueXml.InnerText;
                            }
                        }
                    }
                    break;
                }
            }
            return "";
        }
        #endregion
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        //		public bool Online
        //		{
        //			get{return FOnline;}
        //			set{this.FOnline=value;}
        //		} 
        #endregion

        #region Fields
        // FIELDS
        // ------
        private static string RUN_DIR = "Run";
        private static string TESTS_DIR = "Tests";
        private static string LOG_DIR = "Log";
        private static string SETUP_FILE = "Setup.ini";
        public static string Configure_Path = "";

        #endregion


    }


    /// <summary>
    /// Configuring a website through a web interface can be tricky. If one is to read and write
    /// various files, it is useful to know in advance if you have the authority to do so.
    /// 
    /// This class contains a simple answer to a 
    /// potentially complicated question "Can I read this file or can I write to this file?"
    /// 
    /// Using the "rule of least privilege", one must check not only is access granted but 
    /// is it denied at any point including a possibly recursive check of groups.
    /// 
    /// For this simple check, a look at the user and immediate groups are only checked.
    /// 
    /// This class could be expanded to identify if the applicable allow/deny rule
    /// was explicit or inherited
    /// 
    /// </summary>
    public class UserFileAccessRights 
    {

        #region Fields
        // FIELDS
        // ------
        private string _path;
        private System.Security.Principal.WindowsIdentity _principal;

        private bool _denyAppendData = false;
        private bool _denyChangePermissions = false;
        private bool _denyCreateDirectories = false;
        private bool _denyCreateFiles = false;
        private bool _denyDelete = false;
        private bool _denyDeleteSubdirectoriesAndFiles = false;
        private bool _denyExecuteFile = false;
        private bool _denyFullControl = false;
        private bool _denyListDirectory = false;
        private bool _denyModify = false;
        private bool _denyRead = false;
        private bool _denyReadAndExecute = false;
        private bool _denyReadAttributes = false;
        private bool _denyReadData = false;
        private bool _denyReadExtendedAttributes = false;
        private bool _denyReadPermissions = false;
        private bool _denySynchronize = false;
        private bool _denyTakeOwnership = false;
        private bool _denyTraverse = false;
        private bool _denyWrite = false;
        private bool _denyWriteAttributes = false;
        private bool _denyWriteData = false;
        private bool _denyWriteExtendedAttributes = false;


        private bool _allowAppendData = false;
        private bool _allowChangePermissions = false;
        private bool _allowCreateDirectories = false;
        private bool _allowCreateFiles = false;
        private bool _allowDelete = false;
        private bool _allowDeleteSubdirectoriesAndFiles = false;
        private bool _allowExecuteFile = false;
        private bool _allowFullControl = false;
        private bool _allowListDirectory = false;
        private bool _allowModify = false;
        private bool _allowRead = false;
        private bool _allowReadAndExecute = false;
        private bool _allowReadAttributes = false;
        private bool _allowReadData = false;
        private bool _allowReadExtendedAttributes = false;
        private bool _allowReadPermissions = false;
        private bool _allowSynchronize = false;
        private bool _allowTakeOwnership = false;
        private bool _allowTraverse = false;
        private bool _allowWrite = false;
        private bool _allowWriteAttributes = false;
        private bool _allowWriteData = false;
        private bool _allowWriteExtendedAttributes = false;
        #endregion

        #region Methods
        // METHODS
        // ----------
        public bool canAppendData() { return !_denyAppendData&&_allowAppendData; }
        public bool canChangePermissions() { return !_denyChangePermissions&&_allowChangePermissions; }
        public bool canCreateDirectories() { return !_denyCreateDirectories&&_allowCreateDirectories; }
        public bool canCreateFiles() { return !_denyCreateFiles&&_allowCreateFiles; }
        public bool canDelete() { return !_denyDelete && _allowDelete; }
        public bool canDeleteSubdirectoriesAndFiles() { return !_denyDeleteSubdirectoriesAndFiles && _allowDeleteSubdirectoriesAndFiles; }
        public bool canExecuteFile() { return !_denyExecuteFile && _allowExecuteFile; }
        public bool canFullControl() { return !_denyFullControl && _allowFullControl; }
        public bool canListDirectory() { return !_denyListDirectory && _allowListDirectory; }
        public bool canModify() { return !_denyModify && _allowModify; }
        public bool canRead() { return !_denyRead && _allowRead; }
        public bool canReadAndExecute() { return !_denyReadAndExecute && _allowReadAndExecute; }
        public bool canReadAttributes() { return !_denyReadAttributes && _allowReadAttributes; }
        public bool canReadData() { return !_denyReadData && _allowReadData; }
        public bool canReadExtendedAttributes() { return !_denyReadExtendedAttributes && _allowReadExtendedAttributes; }
        public bool canReadPermissions() { return !_denyReadPermissions && _allowReadPermissions; }
        public bool canSynchronize() { return !_denySynchronize && _allowSynchronize; }
        public bool canTakeOwnership() { return !_denyTakeOwnership && _allowTakeOwnership; }
        public bool canTraverse() { return !_denyTraverse && _allowTraverse; }
        public bool canWrite() { return !_denyWrite && _allowWrite; }
        public bool canWriteAttributes() { return !_denyWriteAttributes && _allowWriteAttributes; }
        public bool canWriteData() { return !_denyWriteData && _allowWriteData; }
        public bool canWriteExtendedAttributes() { return !_denyWriteExtendedAttributes && _allowWriteExtendedAttributes; }

        #region getWindowsIdentity
        /// <summary>
        /// Simple accessor
        /// </summary>
        /// <returns></returns>
        public System.Security.Principal.WindowsIdentity getWindowsIdentity()
        {
            return _principal;
        }
        #endregion

        #region getPath
        /// <summary>
        /// Simple accessor
        /// </summary>
        /// <returns></returns>
        public String getPath()
        {
            return _path;
        }
        #endregion

        #region ToString
        /// </xmp>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override String ToString() {
            string str = "";
        
            if (canAppendData()) {if(!String.IsNullOrEmpty(str)) str+=",";str+="AppendData";}
            if (canChangePermissions()) {if(!String.IsNullOrEmpty(str)) str+=",";str+="ChangePermissions";}
            if (canCreateDirectories()) {if(!String.IsNullOrEmpty(str)) str+=",";str+="CreateDirectories";}
            if (canCreateFiles()) {if(!String.IsNullOrEmpty(str)) str+=",";str+="CreateFiles";}
            if (canDelete()) {if(!String.IsNullOrEmpty(str)) str+=",";str+="Delete";}
            if (canDeleteSubdirectoriesAndFiles()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "DeleteSubdirectoriesAndFiles"; }
            if (canExecuteFile()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "ExecuteFile"; }
            if (canFullControl()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "FullControl"; }
            if (canListDirectory()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "ListDirectory"; }
            if (canModify()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "Modify"; }
            if (canRead()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "Read"; }
            if (canReadAndExecute()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "ReadAndExecute"; }
            if (canReadAttributes()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "ReadAttributes"; }
            if (canReadData()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "ReadData"; }
            if (canReadExtendedAttributes()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "ReadExtendedAttributes"; }
            if (canReadPermissions()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "ReadPermissions"; }
            if (canSynchronize()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "Synchronize"; }
            if (canTakeOwnership()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "TakeOwnership"; }
            if (canTraverse()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "Traverse"; }
            if (canWrite()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "Write"; }
            if (canWriteAttributes()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "WriteAttributes"; }
            if (canWriteData()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "WriteData"; }
            if (canWriteExtendedAttributes()) { if (!String.IsNullOrEmpty(str)) str += ","; str += "WriteExtendedAttributes"; }
            if (String.IsNullOrEmpty(str))
                str = "None";
            return str;
        }
        #endregion

        #region contains
        /// <summary>
        /// Convenience method to test if the right exists within the given rights
        /// </summary>
        /// <param name="right"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public bool contains(System.Security.AccessControl.FileSystemRights right,System.Security.AccessControl.FileSystemAccessRule rule ) 
        {
            return (((int)right & (int)rule.FileSystemRights) == (int)right);
        }
        #endregion
        #endregion

        #region Constractor
        // CONSTRACTOR
        // ------
        /// <summary>
        /// Convenience constructor assumes the current user
        /// </summary>
        /// <param name="path"></param>
        public UserFileAccessRights(string path)
            :
            this(path, System.Security.Principal.WindowsIdentity.GetCurrent()) { }


        /// <summary>
        /// Supply the path to the file or directory and a user or group. Access checks are done
        /// during instanciation to ensure we always have a valid object
        /// </summary>
        /// <param name="path"></param>
        /// <param name="principal"></param>
        public UserFileAccessRights(string path, System.Security.Principal.WindowsIdentity principal)
        {
            this._path = path;
            this._principal = principal;

            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(_path);
                AuthorizationRuleCollection acl = fi.GetAccessControl().GetAccessRules(true, true, typeof(SecurityIdentifier));
                for (int i = 0; i < acl.Count; i++)
                {
                    System.Security.AccessControl.FileSystemAccessRule rule = (System.Security.AccessControl.FileSystemAccessRule)acl[i];
                    if (_principal.User.Equals(rule.IdentityReference))
                    {
                        if (System.Security.AccessControl.AccessControlType.Deny.Equals(rule.AccessControlType))
                        {
                            if (contains(FileSystemRights.AppendData, rule)) _denyAppendData = true;
                            if (contains(FileSystemRights.ChangePermissions, rule)) _denyChangePermissions = true;
                            if (contains(FileSystemRights.CreateDirectories, rule)) _denyCreateDirectories = true;
                            if (contains(FileSystemRights.CreateFiles, rule)) _denyCreateFiles = true;
                            if (contains(FileSystemRights.Delete, rule)) _denyDelete = true;
                            if (contains(FileSystemRights.DeleteSubdirectoriesAndFiles, rule)) _denyDeleteSubdirectoriesAndFiles = true;
                            if (contains(FileSystemRights.ExecuteFile, rule)) _denyExecuteFile = true;
                            if (contains(FileSystemRights.FullControl, rule)) _denyFullControl = true;
                            if (contains(FileSystemRights.ListDirectory, rule)) _denyListDirectory = true;
                            if (contains(FileSystemRights.Modify, rule)) _denyModify = true;
                            if (contains(FileSystemRights.Read, rule)) _denyRead = true;
                            if (contains(FileSystemRights.ReadAndExecute, rule)) _denyReadAndExecute = true;
                            if (contains(FileSystemRights.ReadAttributes, rule)) _denyReadAttributes = true;
                            if (contains(FileSystemRights.ReadData, rule)) _denyReadData = true;
                            if (contains(FileSystemRights.ReadExtendedAttributes, rule)) _denyReadExtendedAttributes = true;
                            if (contains(FileSystemRights.ReadPermissions, rule)) _denyReadPermissions = true;
                            if (contains(FileSystemRights.Synchronize, rule)) _denySynchronize = true;
                            if (contains(FileSystemRights.TakeOwnership, rule)) _denyTakeOwnership = true;
                            if (contains(FileSystemRights.Traverse, rule)) _denyTraverse = true;
                            if (contains(FileSystemRights.Write, rule)) _denyWrite = true;
                            if (contains(FileSystemRights.WriteAttributes, rule)) _denyWriteAttributes = true;
                            if (contains(FileSystemRights.WriteData, rule)) _denyWriteData = true;
                            if (contains(FileSystemRights.WriteExtendedAttributes, rule)) _denyWriteExtendedAttributes = true;
                        }
                        else if (System.Security.AccessControl.AccessControlType.Allow.Equals(rule.AccessControlType))
                        {
                            if (contains(FileSystemRights.AppendData, rule)) _allowAppendData = true;
                            if (contains(FileSystemRights.ChangePermissions, rule)) _allowChangePermissions = true;
                            if (contains(FileSystemRights.CreateDirectories, rule)) _allowCreateDirectories = true;
                            if (contains(FileSystemRights.CreateFiles, rule)) _allowCreateFiles = true;
                            if (contains(FileSystemRights.Delete, rule)) _allowDelete = true;
                            if (contains(FileSystemRights.DeleteSubdirectoriesAndFiles, rule)) _allowDeleteSubdirectoriesAndFiles = true;
                            if (contains(FileSystemRights.ExecuteFile, rule)) _allowExecuteFile = true;
                            if (contains(FileSystemRights.FullControl, rule)) _allowFullControl = true;
                            if (contains(FileSystemRights.ListDirectory, rule)) _allowListDirectory = true;
                            if (contains(FileSystemRights.Modify, rule)) _allowModify = true;
                            if (contains(FileSystemRights.Read, rule)) _allowRead = true;
                            if (contains(FileSystemRights.ReadAndExecute, rule)) _allowReadAndExecute = true;
                            if (contains(FileSystemRights.ReadAttributes, rule)) _allowReadAttributes = true;
                            if (contains(FileSystemRights.ReadData, rule)) _allowReadData = true;
                            if (contains(FileSystemRights.ReadExtendedAttributes, rule)) _allowReadExtendedAttributes = true;
                            if (contains(FileSystemRights.ReadPermissions, rule)) _allowReadPermissions = true;
                            if (contains(FileSystemRights.Synchronize, rule)) _allowSynchronize = true;
                            if (contains(FileSystemRights.TakeOwnership, rule)) _allowTakeOwnership = true;
                            if (contains(FileSystemRights.Traverse, rule)) _allowTraverse = true;
                            if (contains(FileSystemRights.Write, rule)) _allowWrite = true;
                            if (contains(FileSystemRights.WriteAttributes, rule)) _allowWriteAttributes = true;
                            if (contains(FileSystemRights.WriteData, rule)) _allowWriteData = true;
                            if (contains(FileSystemRights.WriteExtendedAttributes, rule)) _allowWriteExtendedAttributes = true;
                        }
                    }
                }

                IdentityReferenceCollection groups = _principal.Groups;
                for (int j = 0; j < groups.Count; j++)
                {
                    for (int i = 0; i < acl.Count; i++)
                    {
                        System.Security.AccessControl.FileSystemAccessRule rule = (System.Security.AccessControl.FileSystemAccessRule)acl[i];
                        if (groups[j].Equals(rule.IdentityReference))
                        {
                            if (System.Security.AccessControl.AccessControlType.Deny.Equals(rule.AccessControlType))
                            {
                                if (contains(FileSystemRights.AppendData, rule)) _denyAppendData = true;
                                if (contains(FileSystemRights.ChangePermissions, rule)) _denyChangePermissions = true;
                                if (contains(FileSystemRights.CreateDirectories, rule)) _denyCreateDirectories = true;
                                if (contains(FileSystemRights.CreateFiles, rule)) _denyCreateFiles = true;
                                if (contains(FileSystemRights.Delete, rule)) _denyDelete = true;
                                if (contains(FileSystemRights.DeleteSubdirectoriesAndFiles, rule)) _denyDeleteSubdirectoriesAndFiles = true;
                                if (contains(FileSystemRights.ExecuteFile, rule)) _denyExecuteFile = true;
                                if (contains(FileSystemRights.FullControl, rule)) _denyFullControl = true;
                                if (contains(FileSystemRights.ListDirectory, rule)) _denyListDirectory = true;
                                if (contains(FileSystemRights.Modify, rule)) _denyModify = true;
                                if (contains(FileSystemRights.Read, rule)) _denyRead = true;
                                if (contains(FileSystemRights.ReadAndExecute, rule)) _denyReadAndExecute = true;
                                if (contains(FileSystemRights.ReadAttributes, rule)) _denyReadAttributes = true;
                                if (contains(FileSystemRights.ReadData, rule)) _denyReadData = true;
                                if (contains(FileSystemRights.ReadExtendedAttributes, rule)) _denyReadExtendedAttributes = true;
                                if (contains(FileSystemRights.ReadPermissions, rule)) _denyReadPermissions = true;
                                if (contains(FileSystemRights.Synchronize, rule)) _denySynchronize = true;
                                if (contains(FileSystemRights.TakeOwnership, rule)) _denyTakeOwnership = true;
                                if (contains(FileSystemRights.Traverse, rule)) _denyTraverse = true;
                                if (contains(FileSystemRights.Write, rule)) _denyWrite = true;
                                if (contains(FileSystemRights.WriteAttributes, rule)) _denyWriteAttributes = true;
                                if (contains(FileSystemRights.WriteData, rule)) _denyWriteData = true;
                                if (contains(FileSystemRights.WriteExtendedAttributes, rule)) _denyWriteExtendedAttributes = true;
                            }
                            else if (System.Security.AccessControl.AccessControlType.Allow.Equals(rule.AccessControlType))
                            {
                                if (contains(FileSystemRights.AppendData, rule)) _allowAppendData = true;
                                if (contains(FileSystemRights.ChangePermissions, rule)) _allowChangePermissions = true;
                                if (contains(FileSystemRights.CreateDirectories, rule)) _allowCreateDirectories = true;
                                if (contains(FileSystemRights.CreateFiles, rule)) _allowCreateFiles = true;
                                if (contains(FileSystemRights.Delete, rule)) _allowDelete = true;
                                if (contains(FileSystemRights.DeleteSubdirectoriesAndFiles, rule)) _allowDeleteSubdirectoriesAndFiles = true;
                                if (contains(FileSystemRights.ExecuteFile, rule)) _allowExecuteFile = true;
                                if (contains(FileSystemRights.FullControl, rule)) _allowFullControl = true;
                                if (contains(FileSystemRights.ListDirectory, rule)) _allowListDirectory = true;
                                if (contains(FileSystemRights.Modify, rule)) _allowModify = true;
                                if (contains(FileSystemRights.Read, rule)) _allowRead = true;
                                if (contains(FileSystemRights.ReadAndExecute, rule)) _allowReadAndExecute = true;
                                if (contains(FileSystemRights.ReadAttributes, rule)) _allowReadAttributes = true;
                                if (contains(FileSystemRights.ReadData, rule)) _allowReadData = true;
                                if (contains(FileSystemRights.ReadExtendedAttributes, rule)) _allowReadExtendedAttributes = true;
                                if (contains(FileSystemRights.ReadPermissions, rule)) _allowReadPermissions = true;
                                if (contains(FileSystemRights.Synchronize, rule)) _allowSynchronize = true;
                                if (contains(FileSystemRights.TakeOwnership, rule)) _allowTakeOwnership = true;
                                if (contains(FileSystemRights.Traverse, rule)) _allowTraverse = true;
                                if (contains(FileSystemRights.Write, rule)) _allowWrite = true;
                                if (contains(FileSystemRights.WriteAttributes, rule)) _allowWriteAttributes = true;
                                if (contains(FileSystemRights.WriteData, rule)) _allowWriteData = true;
                                if (contains(FileSystemRights.WriteExtendedAttributes, rule)) _allowWriteExtendedAttributes = true;
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                //Deal with io exceptions if you want
                throw e;
            }

        }
        #endregion
    }
}

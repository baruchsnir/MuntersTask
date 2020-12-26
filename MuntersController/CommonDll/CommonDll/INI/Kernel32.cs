#region Copyright
//************************************************************************************
// This file created by Lasse Vågsæther Karlsen, 9. Nov 2003.
// Copyright (C) 2003 Lasse Vågsæther Karlsen, All rights reserved.
// This file published under the Creative Commons license Attribution-ShareAlike 1.0.
// http://creativecommons.org/licenses/by-sa/1.0/
//************************************************************************************
#endregion

#region Using

using System;
using System.Text;
using System.Runtime.InteropServices;

#endregion

#region Source Control Information Tags
// $Author: lassevk$
// $Date: 10. november 2003 00:20:09$
// $Revision: 1.2$
#endregion

namespace LVK.Interop
{
    /// <summary>
    /// Static class containing import definitions for functions found in
    /// kernel32.dll.
    /// </summary>
    /// <remarks>
    /// Please note that this class only contains import definitions for the methods
    /// I intend to use in my own library. If for some reason you'd like me to add a few
    /// extras, please let me know by <see href="mailto:lasse@vkarlsen.no">sending me an email</see>.
    /// </remarks>
	public class Kernel32
	{
        #region Construction & Destruction

        private Kernel32()
        {
            // Do nothing
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The GetPrivateProfileInt function retrieves an integer associated
        /// with a key in the specified section of an initialization file
        /// </summary>
        /// <param name="lpAppName">[in] Pointer to a null-terminated string
        /// specifying the name of the section in the initialization file.</param>
        /// <param name="lpKeyName">in] Pointer to the null-terminated string
        /// specifying the name of the key whose value is to be retrieved.
        /// This value is in the form of a string; the GetPrivateProfileInt
        /// function converts the string into an integer and returns the integer.</param>
        /// <param name="nDefault">[in] Default value to return if the key name
        /// cannot be found in the initialization file.</param>
        /// <param name="lpFileName">[in] Pointer to a null-terminated string
        /// that specifies the name of the initialization file. If this parameter
        /// does not contain a full path to the file, the system searches for the
        /// file in the Windows directory.</param>
        /// <returns>The return value is the integer equivalent of the string following
        /// the specified key name in the specified initialization file. If the key
        /// is not found, the return value is the specified default value.</returns>
        /// <remarks>
        /// This method is imported from kernel32.dll.<para/>
        /// 
        /// For more information, check out the MSDN Documentation for
        /// <see href="http://msdn.microsoft.com/library/en-us/sysinfo/base/getprivateprofileint.asp?frame=true">GetPrivateProfileInt</see>.
        /// </remarks>
        [DllImport("kernel32", CharSet=CharSet.Unicode, SetLastError=true)]
        public static extern Int32 GetPrivateProfileInt(
            String lpAppName,
            String lpKeyName,
            Int32 nDefault,
            String lpFileName);

        /// <summary>
        /// The GetPrivateProfileSection function retrieves all the keys and values for
        /// the specified section of an initialization file.
        /// </summary>
        /// <param name="lpAppName">[in] Pointer to a null-terminated string specifying
        /// the name of the section in the initialization file.</param>
        /// <param name="lpReturnedString">[out] Pointer to a buffer that receives the
        /// key name and value pairs associated with the named section. The buffer is
        /// filled with one or more null-terminated strings; the last string is followed
        /// by a second null character.</param>
        /// <param name="nSize">[in] Size of the buffer pointed to by the lpReturnedString
        /// parameter, in TCHARs.</param>
        /// <param name="lpFileName">[in] Pointer to a null-terminated string that specifies
        /// the name of the initialization file. If this parameter does not contain a full
        /// path to the file, the system searches for the file in the Windows directory.</param>
        /// <returns>The return value specifies the number of characters copied to the
        /// buffer, not including the terminating null character. If the buffer is not
        /// large enough to contain all the key name and value pairs associated with the
        /// named section, the return value is equal to nSize minus two.</returns>
        /// <remarks>
        /// This method is imported from kernel32.dll.<para/>
        /// 
        /// For more information, check out the MSDN Documentation for
        /// <see href="http://msdn.microsoft.com/library/en-us/sysinfo/base/getprivateprofilesection.asp?frame=true">GetPrivateProfileSection</see>.
        /// </remarks>
        [DllImport("kernel32", CharSet=CharSet.Unicode, SetLastError=true)]
        public static extern Int32 GetPrivateProfileSection(
            String lpAppName,
            Byte[] lpReturnedString,
            Int32 nSize,
            String lpFileName);

        /// <summary>
        /// The GetPrivateProfileSectionNames function retrieves the names of all sections
        /// in an initialization file.
        /// </summary>
        /// <param name="lpszReturnBuffer">[out] Pointer to a buffer that receives the section
        /// names associated with the named file. The buffer is filled with one or more
        /// null-terminated strings; the last string is followed by a second null character.</param>
        /// <param name="nSize">[in] Size of the buffer pointed to by the lpszReturnBuffer
        /// parameter, in TCHARs.</param>
        /// <param name="lpFileName">[in] Pointer to a null-terminated string that specifies
        /// the name of the initialization file. If this parameter is NULL, the function
        /// searches the Win.ini file. If this parameter does not contain a full path to the
        /// file, the system searches for the file in the Windows directory.</param>
        /// <returns>The return value specifies the number of characters copied to the
        /// specified buffer, not including the terminating null character. If the buffer
        /// is not large enough to contain all the section names associated with the
        /// specified initialization file, the return value is equal to the size specified
        /// by nSize minus two.</returns>
        /// <remarks>
        /// This method is imported from kernel32.dll.<para/>
        /// 
        /// For more information, check out the MSDN Documentation for
        /// <see href="http://msdn.microsoft.com/library/en-us/sysinfo/base/getprivateprofilesectionnames.asp?frame=true">GetPrivateProfileSectionNames</see>.
        /// </remarks>
        [DllImport("kernel32", CharSet=CharSet.Unicode, SetLastError=true)]
        public static extern Int32 GetPrivateProfileSectionNames(
            Byte[] lpszReturnBuffer,
            Int32 nSize,
            String lpFileName);

        /// <summary>
        /// The GetPrivateProfileString function retrieves a string from the specified
        /// section in an initialization file.
        /// </summary>
        /// <param name="lpAppName">[in] Pointer to a null-terminated string that specifies
        /// the name of the section containing the key name. If this parameter is NULL, the
        /// GetPrivateProfileString function copies all section names in the file to the
        /// supplied buffer.</param>
        /// <param name="lpKeyName">[in] Pointer to the null-terminated string specifying the
        /// name of the key whose associated string is to be retrieved. If this parameter is
        /// NULL, all key names in the section specified by the lpAppName parameter are copied
        /// to the buffer specified by the lpReturnedString parameter.</param>
        /// <param name="lpDefault">[in] Pointer to a null-terminated default string. If the
        /// lpKeyName key cannot be found in the initialization file, GetPrivateProfileString
        /// copies the default string to the lpReturnedString buffer. This parameter cannot be
        /// NULL. <para/>
        /// 
        /// Avoid specifying a default string with trailing blank characters. The function
        /// inserts a null character in the lpReturnedString buffer to strip any trailing
        /// blanks.<para/>
        /// 
        /// <b>Windows Me/98/95:</b>  Although lpDefault is declared as a constant parameter, the system
        /// strips any trailing blanks by inserting a null character into the lpDefault string
        /// before copying it to the lpReturnedString buffer.</param>
        /// <param name="lpReturnedString">[out] Pointer to the buffer that receives the
        /// retrieved string.<para/>
        /// 
        /// <b>Windows Me/98/95:</b>  The string cannot contain control characters (character code
        /// less than 32). Strings containing control characters may be truncated.</param>
        /// <param name="nSize">[in] Size of the buffer pointed to by the lpReturnedString
        /// parameter, in TCHARs.</param>
        /// <param name="lpFileName">[in] Pointer to a null-terminated string that specifies
        /// the name of the initialization file. If this parameter does not contain a full
        /// path to the file, the system searches for the file in the Windows directory.</param>
        /// <returns>The return value is the number of characters copied to the buffer, not
        /// including the terminating null character.<para/>
        /// 
        /// If neither lpAppName nor lpKeyName is NULL and the supplied destination buffer is
        /// too small to hold the requested string, the string is truncated and followed by a
        /// null character, and the return value is equal to nSize minus one.<para/>
        /// 
        /// If either lpAppName or lpKeyName is NULL and the supplied destination buffer is too
        /// small to hold all the strings, the last string is truncated and followed by two null
        /// characters. In this case, the return value is equal to nSize minus two.
        /// </returns>
        /// <remarks>
        /// This method is imported from kernel32.dll.
        /// 
        /// For more information, check out the MSDN Documentation for
        /// <see href="http://msdn.microsoft.com/library/en-us/sysinfo/base/getprivateprofilestring.asp?frame=true">GetPrivateProfileString</see>.
        /// </remarks>
        [DllImport("kernel32", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern Int32 GetPrivateProfileString(
            String lpAppName,
            String lpKeyName,
            String lpDefault,
            StringBuilder lpReturnedString,
            Int32 nSize,
            String lpFileName);

        /// <summary>
        /// The WritePrivateProfileSection function replaces the keys and values for the
        /// specified section in an initialization file.
        /// </summary>
        /// <param name="lpAppName">[in] Pointer to a null-terminated string specifying the
        /// name of the section in which data is written. This section name is typically the
        /// name of the calling application.</param>
        /// <param name="lpString">[in] Pointer to a buffer containing the new key names and
        /// associated values that are to be written to the named section. This string is
        /// limited to 65,535 bytes.</param>
        /// <param name="lpFileName">[in] Pointer to a null-terminated string containing the
        /// name of the initialization file. If this parameter does not contain a full path
        /// for the file, the function searches the Windows directory for the file. If the
        /// file does not exist and lpFileName does not contain a full path, the function
        /// creates the file in the Windows directory. The function does not create a file
        /// if lpFileName contains the full path and file name of a file that does not exist.</param>
        /// <returns>If the function succeeds, the return value is nonzero.<para/>
        /// 
        /// If the function fails, the return value is zero. To get extended error
        /// information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// This method is imported from kernel32.dll.<para/>
        /// 
        /// For more information, check out the MSDN Documentation for
        /// <see href="http://msdn.microsoft.com/library/en-us/sysinfo/base/writeprivateprofilesection.asp?frame=true">WritePrivateProfileSection</see>.
        /// </remarks>
        [DllImport("kernel32", CharSet=CharSet.Unicode, SetLastError=true)]
        public static extern Int32 WritePrivateProfileSection(
            String lpAppName,
            String lpString,
            String lpFileName);

        /// <summary>
        /// The WritePrivateProfileString function copies a string into the specified
        /// section of an initialization file.
        /// </summary>
        /// <param name="lpAppName">[in] Pointer to a null-terminated string containing the
        /// name of the section to which the string will be copied. If the section does not
        /// exist, it is created. The name of the section is case-independent; the string
        /// can be any combination of uppercase and lowercase letters.</param>
        /// <param name="lpKeyName">[in] Pointer to the null-terminated string containing
        /// the name of the key to be associated with a string. If the key does not exist
        /// in the specified section, it is created. If this parameter is NULL, the entire
        /// section, including all entries within the section, is deleted.</param>
        /// <param name="lpString">[in] Pointer to a null-terminated string to be written to
        /// the file. If this parameter is NULL, the key pointed to by the lpKeyName
        /// parameter is deleted.<para/>
        /// 
        /// <b>Windows Me/98/95:</b> The system does not support the use of the TAB (\t) character
        /// as part of this parameter.</param>
        /// <param name="lpFileName">[in] Pointer to a null-terminated string that specifies
        /// the name of the initialization file.</param>
        /// <returns>If the function successfully copies the string to the initialization file,
        /// the return value is nonzero.<para/>
        /// 
        /// If the function fails, or if it flushes the cached version of the most recently
        /// accessed initialization file, the return value is zero. To get extended error
        /// information, call GetLastError.</returns>
        /// <remarks>
        /// This method is imported from kernel32.dll.<para/>
        /// 
        /// For more information, check out the MSDN Documentation for
        /// <see href="http://msdn.microsoft.com/library/en-us/sysinfo/base/writeprivateprofilestring.asp?frame=true">WritePrivateProfileString</see>.
        /// </remarks>
        [DllImport("kernel32", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern Int32 WritePrivateProfileString(
            String lpAppName,
            String lpKeyName, 
            String lpString, 
            String lpFileName);

        #endregion
	}
}
#region Source Control History
//
// $Log$
// lassevk - 10. november 2003 00:20:09
// Changed parameter of GetPrivateProfileSectionNames from StringBuilder to Byte[]
// lassevk - 10. november 2003 00:17:43
// Changed parameter of GetPrivateProfileString from StringBuilder to Byte[]
// lassevk - 9. november 2003 22:44:15
// Added and documented Kernel32 import class.
//
#endregion
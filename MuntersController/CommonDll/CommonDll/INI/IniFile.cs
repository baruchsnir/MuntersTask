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
using System.Reflection;
using System.Collections;
using LVK.Interop;

#endregion

// TODO Get rid of dependance on System.Windows.Forms
#region Source Control Information Tags
// $Author: lassevk$
// $Date: 10. november 2003 20:21:13$
// $Revision: 1.4$
#endregion

namespace FT.CommonDll.Ini
{
    /// <summary>
    /// Implements support for .INI files.
    /// </summary>
    /// <remarks>
    /// Internally, this class uses the import methods defined in the <see cref="Kernel32">Kernel32</see>
    /// class to do the real work, which means that native Win32 api functions are used.
    /// </remarks>
    public class IniFile
    {
        #region Private Member Variables

        private const Int32 _BufferSize = 65536;
        private String _FileName;

        #endregion
        #region Construction & Destruction

        /// <summary>
        /// Creates an instance of the ini file class, using the specified ini file.
        /// </summary>
        /// <overloads>
        /// Initializes a new instance of the IniFile class.
        /// </overloads>
        /// <param name="fileName">The full path to and name of the ini file to use. This
        /// parameter cannot be null or empty.</param>
        /// <remarks>
        /// This constructor does not check wether the file exists or not, and does not create
        /// it if it doesn't exist. Only when you try to read from or write to the file will
        /// these checks and actions be performed.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception is thrown if the fileName
        /// parameter is null or empty.</exception>
        public IniFile(String fileName)
        {
            #region Parameter Validation

            if (fileName == null || fileName == String.Empty)
                throw new ArgumentNullException("fileName", "Filename cannot be null or empty");

            #endregion

            _FileName = fileName;
        }

        /// <summary>
        /// Creates an instance of the ini file class, using the default ini
        /// file name which is the same filename as the executable file, only with
        /// a .ini extension.
        /// </summary>
        /// <remarks>
        /// See <see cref="IniFile(String)">IniFile(String)</see> for more information.
        /// </remarks>
        public IniFile()
        {
            String fileName = Assembly.GetEntryAssembly().Location;
            if (fileName.ToUpper().EndsWith(".EXE"))
                fileName = fileName.Substring(0, fileName.Length-3) + "ini";
            else
                fileName = fileName + ".ini";
            _FileName = fileName;
        }

        #endregion
        #region Private Methods

        /// <summary>
        /// This method converts a Byte[] array containing unicode characters to an array
        /// of Strings.
        /// </summary>
        /// <param name="buffer">The Byte[] array to convert.</param>
        /// <param name="bufferLength">The number of characters in the array.</param>
        /// <returns>A String[] array containing the individual strings.</returns>
        /// <remarks>
        /// Note that the <paramref name="bufferLength">bufferLength</paramref> parameter specifies the
        /// number of characters, not bytes.
        /// </remarks>
        private String[] ByteArrayToStringArray(Byte[] buffer, Int32 bufferLength)
        {
            ArrayList resultList = new ArrayList();
            Int32 startIndex = 0;
            for (Int32 index = 0; index < bufferLength * 2; index += 2)
            {
                if (buffer[index] == 0 && buffer[index+1] == 0)
                {
                    Byte[] temp = new Byte[index - startIndex];
                    Array.Copy(buffer, startIndex, temp, 0, index - startIndex);
                    String value = System.Text.Encoding.Unicode.GetString(temp);
                    resultList.Add(value);

                    startIndex = index + 2;
                }
            }

            String[] result = new String[resultList.Count];
            for (Int32 index = 0; index < resultList.Count; index++)
                result[index] = (String)resultList[index];

            return result;
        }

        #endregion
        #region Strings

        /// <summary>
        /// Writes a string value to the ini file.
        /// <seealso cref="ReadString"/>
        /// </summary>
        /// <param name="section">The section to write the value to. This parameter cannot be null or empty.</param>
        /// <param name="key">The name of the value. This parameter cannot be null or empty.</param>
        /// <param name="value">The value to write. This parameter cannot be null.</param>
        /// <exception cref="ArgumentNullException">This exception will be thrown if the
        /// section, key or value parameters are null or empty.</exception>
        /// <remarks>
        /// Will call <see cref="LVK.Interop.Kernel32.WritePrivateProfileString">LVK.Interop.Kernel32.WritePrivateProfileString</see>
        /// internally.
        /// </remarks>
        /// <exception cref="System.ComponentModel.Win32Exception">This exception will be called if, for whatever reason,
        /// the call to the <see cref="Kernel32.WritePrivateProfileString">Kernel32.WritePrivateProfileString</see>
        /// method fails.</exception>
        public void WriteString(String section, String key, String value)
        {
            #region Parameter Validation

            if (value == null)
                throw new ArgumentNullException("value", "The value parameter cannot be null");
            if (key == null || key == String.Empty)
                throw new ArgumentNullException("key", "The key parameter cannot be null or empty");
            if (section == null || section == String.Empty)
                throw new ArgumentNullException("section", "The section parameter cannot be null or empty");

            #endregion

            Int32 result = Kernel32.WritePrivateProfileString(section, key, value, _FileName);
            if (result == 0)
                throw new System.ComponentModel.Win32Exception();
        }

        /// <summary>
        /// Reads a string value from the ini file.
        /// <seealso cref="WriteString"/>
        /// </summary>
        /// <param name="section">The section to read the value from. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The name of the value. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="defaultValue">The default value to return if the
        /// value is not present in the ini file. This parameter cannot be null.</param>
        /// <returns>The string value from the ini file, or the default value
        /// if the value is not present in the ini file.</returns>
        /// <remarks>
        /// Will call <see cref="LVK.Interop.Kernel32.GetPrivateProfileString">LVK.Interop.Kernel32.GetPrivateProfileString</see> internal
        /// </remarks>
        /// <exception cref="System.ComponentModel.Win32Exception">This exception will be called if, for whatever reason,
        /// the call to the <see cref="Kernel32.GetPrivateProfileString">Kernel32.GetPrivateProfileString</see>
        /// method fails.</exception>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref>, <paramref name="key">key</paramref> or <paramref name="defaultValue">defaultValue</paramref>
        /// parameters are null or empty.</exception>
        public String ReadString(String section, String key, String defaultValue)
        {
            StringBuilder buffer = new StringBuilder(_BufferSize);
            Int32 result = Kernel32.GetPrivateProfileString(section, key, defaultValue, buffer, _BufferSize, _FileName);
            if (result == 0)
                return defaultValue;

            return buffer.ToString();
        }

        /// <summary>
        /// Reads a string value from the ini file.
        /// <seealso cref="WriteString"/>
        /// </summary>
        /// <param name="section">The section to read the value from. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The name of the value. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <returns>The string value from the ini file, or "" if the value 
        /// is not present in the ini file.</returns>
        /// <remarks>
        /// Will call <see cref="ReadString(String,String,String)">ReadString(String,String,String)</see>
        /// internally.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref> or <paramref name="key">key</paramref>
        /// parameters are null or empty.</exception>
        public String ReadString(String section, String key)
        {
            // Parameter validation is done in ReadString(String,String,String)

            return ReadString(section, key, "");
        }
        #endregion
        #region Int32

        /// <summary>
        /// Writes an integer value to the ini file.
        /// <seealso cref="ReadInt32"/>
        /// </summary>
        /// <param name="section">The section to write the value to. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The name of the value. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="value">The value to write.</param>
        /// <remarks>
        /// Will call <see cref="WriteString(String,String,String)">WriteString(String,String,String)</see>
        /// internally.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref> or <paramref name="key">key</paramref>
        /// parameters are null or empty.</exception>
        public void WriteInt32(String section, String key, Int32 value)
        {
            // Parameter validation is done in WriteString(String,String,String)

            WriteString(section, key, value.ToString());
        }

        /// <summary>
        /// Reads an integer value from the ini file.
        /// <seealso cref="WriteInt32"/>
        /// </summary>
        /// <param name="section">The section to read the value from. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The name of the value. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="defaultValue">The default value to return if the
        /// value is not present in the ini file.</param>
        /// <returns>The integer value from the ini file, or the default value
        /// if the value is not present in the ini file.</returns>
        /// <remarks>
        /// Will call <see cref="ReadString(String,String,String)">ReadString(String,String,String)</see>
        /// internally.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref> or <paramref name="key">key</paramref>
        /// parameters are null or empty.</exception>
        public Int32 ReadInt32(String section, String key, Int32 defaultValue)
        {
            // Parameter validation is done in ReadString(String,String,String)

            return Int32.Parse(ReadString(section, key, defaultValue.ToString()));
        }

        /// <summary>
        /// Reads an integer value from the ini file.
        /// <seealso cref="WriteInt32"/>
        /// </summary>
        /// <param name="section">The section to read the value from. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The name of the value. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <returns>The integer value from the ini file, or 0 if the value 
        /// is not present in the ini file.</returns>
        /// <remarks>
        /// Will call <see cref="ReadInt32(String,String,Int32)">ReadInt32(String,String,Int32)</see>
        /// internally.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref> or <paramref name="key">key</paramref>
        /// parameters are null or empty.</exception>
        public int ReadInt32(String section, String key)
        {
            // Parameter validation is done in ReadInt32(String,String,Int32)

            return ReadInt32(section, key, 0);
        }

        #endregion
        #region Float
        /// <summary>
        /// Reads an integer value from the ini file.
        /// <seealso cref="WriteInt32"/>
        /// </summary>
        /// <param name="section">The section to read the value from. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The name of the value. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="defaultValue">The default value to return if the
        /// value is not present in the ini file.</param>
        /// <returns>The integer value from the ini file, or the default value
        /// if the value is not present in the ini file.</returns>
        /// <remarks>
        /// Will call <see cref="ReadString(String,String,String)">ReadString(String,String,String)</see>
        /// internally.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref> or <paramref name="key">key</paramref>
        /// parameters are null or empty.</exception>
        public float ReadFloat(string section, string key, double defaultValue)
        {
            return (float)Double.Parse(ReadString(section, key, defaultValue.ToString()));
        }
        /// <summary>
        /// Writes an integer value to the ini file.
        /// <seealso cref="ReadInt32"/>
        /// </summary>
        /// <param name="section">The section to write the value to. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The name of the value. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="value">The value to write.</param>
        /// <remarks>
        /// Will call <see cref="WriteString(String,String,String)">WriteString(String,String,String)</see>
        /// internally.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref> or <paramref name="key">key</paramref>
        /// parameters are null or empty.</exception>
        public void WriteFloat(String section, String key, Double value)
        {
            // Parameter validation is done in WriteString(String,String,String)

            WriteString(section, key, value.ToString());
        }
        #endregion
        #region Booleans

        /// <summary>
        /// Writes a boolean value to the ini file.
        /// <seealso cref="ReadBoolean"/>
        /// </summary>
        /// <param name="section">The section to write the value to. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The name of the value. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="value">The value to write.</param>
        /// <remarks>
        /// Will call <see cref="WriteString(String,String,String)">WriteString(String,String,String)</see>
        /// internally.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref> or <paramref name="key">key</paramref>
        /// parameters are null or empty.</exception>
        public void WriteBoolean(String section, String key, Boolean value)
        {
            // Parameter validation is done in WriteString(String,String,String)
            string bools = "0";
            if (value)
			 bools = "1";
            WriteString(section, key, bools);
        }

        /// <summary>
        /// Reads a boolean value from the ini file. The specified default value will be returned if the
        /// specified key is not present in the specified section.
        /// <seealso cref="WriteBoolean"/>
        /// </summary>
        /// <overloads>
        /// Reads a boolean value from the ini file.
        /// </overloads>
        /// <param name="section">The section to read the value from. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The name of the value. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="defaultValue">The default value to return if the
        /// value is not present in the ini file.</param>
        /// <returns>The boolean value from the ini file, or the default value
        /// if the value is not present in the ini file.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref> or <paramref name="key">key</paramref>
        /// parameters are null or empty.</exception>
        public Boolean ReadBoolean(String section, String key, Boolean defaultValue)
        {
            // Parameter validation is done in ReadString(String,String,String)

            String value = ReadString(section, key, defaultValue.ToString());
            if (value.ToUpper() == "TRUE" || value == "1" || value.ToUpper() == "ON" || value.ToUpper() == "YES")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Reads a boolean value from the ini file. Default value of false will be returned if
        /// the specified key is not present in the specified section.
        /// <seealso cref="WriteBoolean"/>
        /// </summary>
        /// <param name="section">The section to read the value from. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The name of the value. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <returns>The string value from the ini file, or false if the value 
        /// is not present in the ini file.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref> or <paramref name="key">key</paramref>
        /// parameters are null or empty.</exception>
        public Boolean ReadBoolean(String section, String key)
        {
            // Parameter validation is done in ReadBoolean(String,String,Boolean)

            return ReadBoolean(section, key, false);
        }

        #endregion
        #region Enumerations

        /// <summary>
        /// Writes the value of the enumerated value to the .ini file.
        /// <seealso cref="ReadEnum"/>
        /// </summary>
        /// <param name="section">The [section] to place the value in. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The key to use for naming the value. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="value">The value to write, will be converted to string. This parameter cannot be null or
        /// an ArgumentNullException will be thrown.</param>
        /// <remarks>
        /// Will call <see cref="WriteString(String,String,String)">WriteString(String,String,String)</see>
        /// internally.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref>, <paramref name="key">key</paramref> or <paramref name="value">value</paramref>
        /// parameters are null or empty.</exception>
        public void WriteEnum(String section, String key, Object value)
        {
            #region Parameter Validation

            if (value == null)
                throw new ArgumentNullException("value", "The value parameter cannot be null");

            // Rest of parameter validation is done in WriteString(String,String,String)

            #endregion

            WriteString(section, key, value.ToString());
        }

        /// <summary>
        /// Reads the value of an enumerated type value from the .ini file and returns it as an object.<para/>
        /// 
        /// The code that calls ReadEnum must typecast this object to the correct enumerated type
        /// before use.
        /// <seealso cref="WriteEnum"/>
        /// </summary>
        /// <overloads>
        /// Reads an enumerated type value from the .ini file.
        /// </overloads>
        /// <example>
        /// <code>Form1.WindowState = (WindowState)ini.ReadEnum("Form1", "WindowState", WindowState, WindowState.GetType());</code>
        /// </example>
        /// <param name="section">The [section] to read the value from. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The key of the value in the section. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="defaultValue">The default value to use if the key is not present
        /// in the specified section.</param>
        /// <param name="enumType">The type of the enumerated type to read. This parameter cannot be null or
        /// an ArgumentNullException will be thrown.</param>
        /// <returns>An object representing the enumerated type value.</returns>
        /// <remarks>
        /// Will call <see cref="ReadString(String,String,String)">ReadString(String,String,String)</see>
        /// internally.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref>, <paramref name="key">key</paramref> or <paramref name="enumType">enumType</paramref>
        /// parameters are null or empty.</exception>
        public Object ReadEnum(String section, String key, Object defaultValue, System.Type enumType)
        {
            #region Parameter Validation

            if (enumType == null)
                throw new ArgumentNullException("enumType", "The enumType parameter cannot be null");

            // Rest of parameter validation is done in ReadString(String,String,String)

            #endregion

            return Enum.Parse(enumType, ReadString(section, key, defaultValue.ToString()), true);
        }

        /// <summary>
        /// Reads the value of an enumerated type from the .ini file and returns it as an object.
        /// <seealso cref="WriteEnum"/>
        /// </summary>
        /// <param name="section">The [section] to read the value from. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The key of the value in the section. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="enumType">The type of the enumerated type to read. This parameter cannot be null or
        /// an ArgumentNullException will be thrown.</param>
        /// <returns>An object representing the enumerated type value.</returns>
        /// <remarks>
        /// Will call <see cref="ReadEnum(String,String,Object,System.Type)">ReadEnum(String,String,Object,System.Type)</see>
        /// internally.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref>, <paramref name="key">key</paramref> or <paramref name="enumType">enumType</paramref>
        /// parameters are null or empty.</exception>
        public Object ReadEnum(String section, String key, System.Type enumType)
        {
            #region Parameter Validation

            if (enumType == null)
                throw new ArgumentNullException("enumType", "The enumType parameter cannot be null");

            // Rest of parameter validation is done in ReadString(String,String)

            #endregion

            return Enum.Parse(enumType, ReadString(section, key), true);
        }

        #endregion
        #region Delete

        /// <summary>
        /// Deletes the ini file key from the specified section.
        /// </summary>
        /// <param name="section">The section to delete the key from. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The key to delete from the section. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <remarks>
        /// Internally, this method will call
        /// <see cref="Kernel32.WritePrivateProfileString(String,String,String,String)">Kernel32.WritePrivateProfileString(String,String,String,String)</see>
        /// with a null value to delete the entry in the ini file.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref> or <paramref name="key">key</paramref>
        /// parameters are null or empty.</exception>
        public void DeleteValue(String section, String key)
        {
            #region Parameter Validation

            if (section == null || section == String.Empty)
                throw new ArgumentNullException("section", "The section parameter cannot be null or empty");
            if (key == null || key == String.Empty)
                throw new ArgumentNullException("key", "The key parameter cannot be null or empty");

            #endregion

            Int32 result = Kernel32.WritePrivateProfileString(section, key, null, _FileName);
            if (result == 0)
                throw new System.ComponentModel.Win32Exception();
        }

        #endregion
        #region Utility functions

        /// <summary>
        /// Checks if a given value exists in a given section.
        /// </summary>
        /// <param name="section">The name of the section to look in. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <param name="key">The name of the value to look for. This parameter cannot be null or empty or
        /// an ArgumentNullException will be thrown.</param>
        /// <returns>Returns true if the value is present in the section
        /// (may have a blank value though), false if not.</returns>
        /// <remarks>
        /// This method will try to read a string with the given name from the
        /// given section, using a freshly generated guid as a default value,
        /// and if this guid is returned, then the value was not present in the
        /// ini file.
        /// </remarks>
        /// <exception cref="ArgumentNullException">This exception will be thrown if either
        /// the <paramref name="section">section</paramref> or <paramref name="key">key</paramref>
        /// parameters are null or empty.</exception>
        public Boolean KeyExists(String section, String key)
        {
            #region Parameter Validation

            if (section == null || section == String.Empty)
                throw new ArgumentNullException("section", "The section parameter cannot be null or empty");
            if (key == null || key == String.Empty)
                throw new ArgumentNullException("key", "The key parameter cannot be null or empty");

            #endregion

            String defaultValue = Guid.NewGuid().ToString();
            if (ReadString(section, key, defaultValue) != defaultValue)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Reads and returns a complete section as a String array, one element per value in the section.
        /// </summary>
        /// <param name="key">The section to read. This parameter cannot be null or empty or an ArgumentNullException
        /// will be thrown.</param>
        /// <returns>A String[] array containing the values in the section.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown if the
        /// <paramref name="key">key</paramref> parameter is null or empty.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">This exception will be thrown if the method fails to read the
        /// section for whatever reason.</exception>
        /// <remarks>
        /// This method will return each value in the section as a Name=Value pair in the String array.
        /// </remarks>
        public String[] ReadSection(String key)
        {
            #region Parameter Validation

            if (key == null || key == String.Empty)
                throw new ArgumentNullException("key", "The key parameter cannot be null or empty");

            #endregion

            Byte[] buffer = new Byte[_BufferSize * 2];
            Int32 stringLength = Kernel32.GetPrivateProfileSection(key, buffer, _BufferSize, _FileName);
            if (stringLength == 0)
                throw new Exception("Problem in ReadSection key :" + key + " in file name " + _FileName);

            return ByteArrayToStringArray(buffer, stringLength);
        }

       
        /// <summary>
        /// Write section and return true if succeed
        /// </summary>
        /// <param name="section">The section to write. This parameter cannot be null or empty or an ArgumentNullException
        /// will be thrown.</param>
        /// <param name="names">String[] array containing the values to write in to the section</param>/// 
        /// <returns>True if succeed </returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown if the
        /// <paramref name="key">key</paramref> parameter is null or empty.</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">This exception will be thrown if the method fails to write the
        /// section for whatever reason.</exception>
        public bool WriteSection(String section,String[] names)
        {
            #region Parameter Validation
            if (section == null || section == String.Empty)
                throw new ArgumentNullException("section", "The section parameter cannot be null or empty");
            if (names == null || names.Length == 0)
                throw new ArgumentNullException("names", "The names parameter cannot be null or empty");
            #endregion

//            Byte[] buffer = new Byte[_BufferSize * 2];
//            Int32 stringLength = Kernel32.GetPrivateProfileSection(key, buffer, _BufferSize, _FileName);
//            if (stringLength == 0)
//                throw new System.ComponentModel.Win32Exception();


            for (int j = names.Length ; j > 0; j--)
            {
                #region Parameter Validation
                if (names[j-1] == null || names[j-1] == String.Empty)
                    throw new ArgumentNullException("key", "The key parameter cannot be null or empty");
                #endregion
                Int32 result = Kernel32.WritePrivateProfileSection(section, names[j-1], _FileName);
                if (result == 0)
                {
                    throw new Exception("Problem in WriteSection section :" + section + " in file name " + _FileName);

//                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Reads and returns a list of section names from the specified ini file.
        /// </summary>
        /// <returns>A String[] array containing the section names from the file.</returns>
        /// <exception cref="System.ComponentModel.Win32Exception">This exception will be thrown if the method fails to read the
        /// section names for whatever reason.</exception>
        /// <remarks>
        /// This method will return each section name in a element of its own in the result array. Also note that the
        /// section names will be returned without the surrounding brackets.
        /// </remarks>
        public String[] ReadSectionNames()
        {
            Byte[] buffer = new Byte[_BufferSize * 2];
            Int32 stringLength = Kernel32.GetPrivateProfileSectionNames(buffer, _BufferSize, _FileName);
            if (stringLength == 0)
                throw new Exception("Problem in ReadSectionNames in file name " + _FileName);

            return ByteArrayToStringArray(buffer, stringLength);
        }

        #endregion
    }
}
#region Source Control History
//
// $Log$
// lassevk - 10. november 2003 20:21:13
// Fixed a bug where trying to read a nonexistant value would fail with an exception.
// lassevk - 10. november 2003 19:19:43
// lassevk - 10. november 2003 00:27:47
// Minor adjustments to documentation.
// lassevk - 10. november 2003 00:26:06
// Added ReadSection and ReadSectionNames methods
// lassevk - 9. november 2003 22:44:22
// Added and documented IniFile import class.
//
#endregion
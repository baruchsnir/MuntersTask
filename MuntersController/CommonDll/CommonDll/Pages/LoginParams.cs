using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FT.CommonDll;
using FT.CommonDll.Ini;
namespace FT.CommonDll.Pages
{

    public class LoginParams
    {

        #region Constructor
        // CONSTRUCTOR
        // ---
        public LoginParams()
        {

        }
        #endregion

        #region Destructor
        // DESTRUCTOR
        // ------
        /// <summary>
        /// Distractor for Io LoginParams
        /// </summary>
        protected void Dispose()
        {
            try
            {
            }
            catch (Exception) {; }
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        public static string User
        {
            get {
                Read_Data_From_ini_File();
                return user; }

        }
        public static string Password
        {
            get
            {
                Read_Data_From_ini_File();
                return password;
            }
        }
        #endregion

        #region Methods
        // METHODS
        // ----------
        #region Read_Data_From_ini_File
        /// <summary>
        /// Read User name and password from ini file
        /// </summary>
        /// <param name="setup_File_name"></param>
        private static void Read_Data_From_ini_File()
        {
            string setup_File_name  = Layers_Handler.instance().RunDir;
            setup_File_name += @"\Setup.ini";
            IniFile ini = new IniFile(setup_File_name);
            try
            {
                user = ini.ReadString("General", "User", "");
                password = ini.ReadString("General", "Password", "");
            }
            catch {; }
        }
        #endregion
        #endregion

        #region Fields
        // FIELDS
        // ------
        private static string user = "";
        private static string password = "";
        #endregion
    }



}

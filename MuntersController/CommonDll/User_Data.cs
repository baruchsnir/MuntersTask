using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.CommonDll
{
    /// <summary>
    /// Summary description for Goldenbet 
    /// </summary>
    [Serializable]
    public class User_Data
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        public User_Data(string user)
        {
           
        }
        #endregion

        #region Destractor
        // DESTRACTOR
        // ------
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
        }
        #endregion

        #region Methods
        // METHODS
        // ----------
        public override string ToString()
        {
            return string.Format("{0} - Href {1} , F - {2}, B - {3}",this.name,href,followers,boards);
        }
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Href
        {
            get { return this.href; }
            set { this.href = value; }
        }
        public DateTime Last_date
        {
            get { return this.last_date; }
            set { this.last_date = value; }
        }
        public int Boards
        {
            get { return this.boards; }
            set { this.boards = value; }
        }
        public int Followers
        {
            get { return this.followers; }
            set { this.followers = value; }
        }
        #endregion

        #region Fields
        // FIELDS
        // ------
        private string   name = "";
        private DateTime last_date;
        private string href = "";
        private int boards = 0;
        private int followers = 0;
        #endregion
    }
}

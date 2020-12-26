using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FT.CommonDll
{
    /// <summary>
    /// Summary description for Goldenbet 
    /// </summary>
    [Serializable]
    public class Pin_Data
    {
        #region Constractor
        // CONSTRACTOR
        // ------
        public Pin_Data(string pin_name)
        {
            this.name = pin_name;
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
            return string.Format("{0} - Href {1}", this.name, href);
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
        public Hashtable Repi_date
        {
            get { return this.repi_date; }
            set { this.repi_date = value; }
        }
        #endregion

        #region Fields
        // FIELDS
        // ------
        private string name = "";
        private DateTime last_date;
        private string href = "";
        private Hashtable repi_date = new Hashtable();
        #endregion
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Linq;

namespace FT.CommonDll.Pages
{

    public class FillGridPage
    {

        #region Constructor
        // CONSTRUCTOR
        // 
        /// <summary>
        /// Constructor for FillGridPage
        /// Manage the update of row in the grid
        /// </summary>
        /// <param name="driver">The given WebDriver</param>
        public FillGridPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
        }
        #endregion

        #region Destructor
        // DESTRUCTOR
        // ------
        /// <summary>
        /// Distractor for Open Temperature Gurve Grid
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
        #endregion

        #region Methods
        // METHODS
        // ----------
        #region Press House Menue Button
        /// <summary>
        /// Control The side bar menue to select the House 1 site
        /// </summary>
        public void FillFirstRow()
        {

            int frams = TooldPages.GetframeObjectsCount(driver);
            if (frams > 0)
            {
                driver.SwitchTo().Frame(0);
            }
            HtmlNode[] nodes = null;
            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(driver.PageSource);
            nodes = html.DocumentNode.SelectNodes("//tr").Where(n => n.GetAttributeValue("data-uid", "").ToLower().StartsWith("grid-row21")).ToArray();
            if (nodes != null)
            {
                if (nodes.Length > 0)
                {
                    foreach (HtmlNode item in nodes)
                    {
                        if (item.InnerText.Contains("<!----> 0"))
                        {
                            if (!fill_data_in_first_row(item,(RemoteWebDriver)driver))
                            {
                                Layers_Handler.instance().Test = true;
                            }

                            return;
                        }
                    }
                }
            }
        }
        #endregion

        #region fill_data_in_first_row
        /// <summary>
        /// Fill the first row in Grid with data
        /// </summary>
        /// <param name="first">First row element</param>
        /// <param name="driver">Web Driver</param>
        /// <returns>If succedd or fail</returns>
        private bool fill_data_in_first_row(HtmlNode first, RemoteWebDriver driver)
        {
            HtmlNode[] nodesinputs = null;
            Random r = new Random();
            int target = 15 + r.Next(0, 5);
            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
            //I bild default table - there is a problem with reading the data from range elemnt
            //Even there is a real value the web driver read all the time value 0 - 0
            Dictionary<int, float[]> defaultranges = new Dictionary<int, float[]>();
            defaultranges.Add(0, new float[] { 0, 0 });
            defaultranges.Add(1, new float[] { -40, 90 });
            defaultranges.Add(2, new float[] { -40, target });
            defaultranges.Add(3, new float[] { target, 90 });
            defaultranges.Add(4, new float[] { target, 90 });
            defaultranges.Add(5, new float[] { -40, target - (float)0.5 });
            defaultranges.Add(6, new float[] { target + (float)0.5, 90 });

            int zoneheat = target - r.Next(0, 2);
            int cool = 25 + r.Next(0, 5);
            int tunnel = 22 + r.Next(0, 5);
            int lowalarm = target - r.Next(0, 2);
            int highalarm = 30 + r.Next(0, 5);
            int count = 0;
            int[] rowdata = new int[] { 0, target, zoneheat, cool, tunnel, lowalarm, highalarm, 0, 0, 0, 0, 0 };
            float[] newData = new float[rowdata.Length];
            for (int i = 0; i < rowdata.Length; i++)
                newData[i] = -100;
            html.LoadHtml(first.InnerHtml);
            nodesinputs = html.DocumentNode.SelectNodes("//input").Where(n => n.GetAttributeValue("class", "").ToLower().StartsWith("k-input k-formatted-value")).ToArray();
            bool result = true;
            float val;
            foreach (HtmlNode item in nodesinputs)
            {
                if (rowdata[count] > 0)
                {
                    var idp = item.ParentNode.ParentNode.GetAttributeValue("id", "");
                    IWebElement parent = driver.FindElement(By.Id(idp));
                    var id = item.GetAttributeValue("id", "");
                    IWebElement input = driver.FindElement(By.Id(id));
                    if (input != null)
                    {
                        parent.Click();
                        val = rowdata[count];
                        if (count == 1)
                        {
                            input.SendKeys(val.ToString());
                        }
                        else
                        {
                            float[] val_get = get_range_from_controller(driver);
                            //input.Clear();
                            if (val_get[0] == val_get[1])
                                val_get = defaultranges[count];
                            if (count > 1)
                            {
                                if (val_get[0] != val_get[1])
                                {
                                    val = (val_get[0] + val_get[1]) / 2;
                                    newData[count] = val;
                                }
                            }
                            result &= send_value_to_filed(driver, input, ref val, target, defaultranges[count]);
                        }
                        newData[count] = val;
                    }
                }
                count++;
            }
            FLogAddLine("New Paremeters that were entered :", LogType.ltMagenta, "");
            FLogAddLine("----------------------------------", LogType.ltMagenta, "");
            for (int i = 0; i < newData.Length; i++)
            {
                if (newData[i] != -100)
                {
                    switch (i)
                    {
                        //target, cool, tunnel, lowalarm, fhighalarm
                        case 1: this.FLogAddLine(String.Format("Target       : {0:f1}", newData[i]), LogType.ltMagenta, ""); break;
                        case 2: this.FLogAddLine(String.Format("Zone Heat    : {0:f1}", newData[i]), LogType.ltMagenta, ""); break;
                        case 3: this.FLogAddLine(String.Format("Cool         : {0:f1}", newData[i]), LogType.ltMagenta, ""); break;
                        case 4: this.FLogAddLine(String.Format("Tunnel       : {0:f1}", newData[i]), LogType.ltMagenta, ""); break;
                        case 5: this.FLogAddLine(String.Format("Low T Alarm  : {0:f1}", newData[i]), LogType.ltMagenta, ""); break;
                        case 6: this.FLogAddLine(String.Format("High T Alarm : {0:f1}", newData[i]), LogType.ltMagenta, ""); break;
                    }
                }


            }
            return result;
        }
        #endregion

        #region send_value_to_filed
        /// <summary>
        /// Send value to field
        /// </summary>
        /// <param name="driver">The web Driver</param>
        /// <param name="input">Input Element</param>
        /// <param name="val">The new Value</param>
        /// <param name="target">The First Temrature Target</param>
        private bool send_value_to_filed(RemoteWebDriver driver, IWebElement input, ref float val, int target, float[] defaultrange)
        {
            input.SendKeys(val.ToString());
            //Thread.Sleep(1000);
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    float[] val_get = get_range_from_controller(driver);
                    if (val_get[0] == val_get[1])
                        val_get = defaultrange;
                    if (driver.PageSource.Contains("Number is not within valid range"))
                    {
                        if (val < val_get[0])
                            val = val_get[0];
                        if (val > val_get[1])
                            val = val_get[1];
                        // move_to_element(driver, input);
                        input.Click();
                        input.Clear();
                        input.SendKeys(val.ToString());
                        Thread.Sleep(1000);
                        continue;
                    }
                    else
                    {
                        if (driver.PageSource.Contains("Value must be below target"))
                        {
                            val = target - 1;
                            //move_to_element(driver, input);
                            input.Click();
                            input.Clear();
                            input.SendKeys(val.ToString());
                            Thread.Sleep(1000);
                            continue;
                        }
                    }
                    break;
                }
            }
            catch { return false; }
            return true;
        }
        #endregion

        #region get_range_from_controller
        /// <summary>
        /// Get The range from the web element in the main frame
        /// </summary>
        /// <param name="driver">The web driver</param>
        /// <returns>The minimum and maximum values</returns>
        private float[] get_range_from_controller(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
            Thread.Sleep(500);
            HtmlNode[] nodesinputs = null;
            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
            string range = range_info.Text;
            float[] vals = new float[2];
            string[] inputs;
            inputs = range.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (inputs.Length > 2)
            {
                float.TryParse(inputs[0], out vals[0]);
                float.TryParse(inputs[2], out vals[1]);
            }
            if (vals[1] != vals[0])
                return vals;


            html.LoadHtml(driver.PageSource);
            nodesinputs = html.DocumentNode.SelectNodes("//span").Where(n => n.GetAttributeValue("class", "").ToLower().Equals("range-info")).ToArray();


            foreach (HtmlNode item in nodesinputs)
            {

                //IWebElement range = driver.FindElement(By.XPath("//span[@class=\"range-info\"]"));
                if (item.InnerText != "")
                {
                    string temp = item.InnerText;
                    inputs = temp.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (inputs.Length > 2)
                    {
                        float.TryParse(inputs[0], out vals[0]);
                        float.TryParse(inputs[2], out vals[1]);
                    }
                    if (vals[1] > 0)
                        return vals;
                }
            }
            return vals;
        }
        #endregion

        #region Update Status
        /// <summary>
        /// Send Status to main form
        /// </summary>
        /// <param name="line">Line data</param>
        private void Update_Status(string line)
        {
            Layers_Handler.instance().SendDisplayEvent(this, new DisplayEventArgs(line));
        }
        #endregion

        #region FLogAddLine
        /// <summary>
        /// Add line to log file
        /// </summary>
        /// <param name="line">Line data</param>
        /// <param name="logType">The type of color</param>
        /// <param name="Prefix">The prefix to add to line</param>
        private void FLogAddLine(string line, LogType logType, string Prefix)
        {
            LogEventArgs args = new LogEventArgs(line, logType, Prefix);
            Layers_Handler.instance().TrigerAddLineToLogEvent(this, args);
        }
        #endregion
        #endregion

        #region Fields
        // FIELDS
        // ------
        private IWebDriver driver;
        [FindsBy(How = How.XPath, Using = "//div[@class=\"hosted-toolbar\"]")]
        [CacheLookup]
        private IWebElement toolbar;
        [FindsBy(How = How.XPath, Using = "//span[@class=\"range-info\"]")]
        [CacheLookup]
        private IWebElement range_info;
        #endregion
    }
}


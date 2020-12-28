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

    public class SaveButton
    {

        #region Constructor
        // CONSTRUCTOR
        // 
        /// <summary>
        /// Constructor for Save Button Page
        /// Manage the press save button 
        /// </summary>
        /// <param name="driver">The given WebDriver</param>
        public SaveButton(IWebDriver driver)
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
        public void PresTheButton()
        {
            HtmlNode[] nodes = null;
            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();

            IWebElement save = null;
            string xpath = "", classname = "";
            int frams = TooldPages.GetframeObjectsCount(driver);
            if (frams > 0)
            {
                driver.SwitchTo().Frame(0);
            }
            html.LoadHtml(driver.PageSource);
            nodes = html.DocumentNode.SelectNodes("//div").Where(n => n.GetAttributeValue("class", "").ToLower().StartsWith("toolbar-button label-button button-save")).ToArray();
            //driver.SwitchTo().DefaultContent();
            foreach (var item in nodes)
            {
                xpath = item.ParentNode.XPath;
                classname = item.ParentNode.GetAttributeValue("class", "");

            }
            

            var buttons = toolbar.FindElements(By.TagName("div"));
            foreach (var btn in buttons)
            {
                if (btn.Text.ToLower().Trim().Equals("save"))
                {
                    save = btn;
                    break;
                }
            }

            if (save != null)
            {
                save.Click();
            }
            string temp = "";
            if (save != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    temp = driver.PageSource.ToLower();
                    if (temp.Contains("changes saved") || temp.Contains("successfully") || temp.Contains("failed to save changes"))
                    {
                        break;
                    }
                }
            }
            if ((save != null) && (!classname.Contains("disabled")))
            {
                if (temp.Contains("changes saved") || temp.Contains("Successfully") || temp.Contains("failed to save changes"))
                {
                    if (temp.Contains("successfully") || temp.Contains("changes saved"))
                    {
                        this.Update_Status("Changes Saved Successfully !!!");
                        FLogAddLine("Test Case - Save Data Pass", LogType.ltBlue, "");
                    }
                    else
                    {
                        Layers_Handler.instance().Test = true;
                        this.Update_Status("Failed to Save Changes!!!");
                        FLogAddLine("Test Case - Save Data Fail", LogType.ltError, "");
                    }
                }
                else
                {
                    Layers_Handler.instance().Test = true;
                    FLogAddLine("Test Case - Save Data Fail", LogType.ltError, "");
                    this.Update_Status("Faile to get Saved Results!!!");
                }
            }
            if (classname.Contains("disabled"))
            {
                Layers_Handler.instance().Test = true;
                FLogAddLine("Test Case - Save Data Fail", LogType.ltError, "");
                this.Update_Status("Faile to get Saved Results!!!");
            }
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
        private IWebElement toolbar;
        #endregion
    }
}

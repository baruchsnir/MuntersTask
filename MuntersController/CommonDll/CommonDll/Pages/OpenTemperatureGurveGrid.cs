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

    public class OpenTemperatureGurveGrid
    {

        #region Constructor
        // CONSTRUCTOR
        // 
        /// <summary>
        /// Constructor for Open Temperature Gurve Grid
        /// </summary>
        /// <param name="driver">The given WebDriver</param>
        public OpenTemperatureGurveGrid(IWebDriver driver)
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
        public void PressTempratureGuveMenueButton()
        {
            int count = 0;
            waitForIfram(driver);
            int frams = TooldPages.GetframeObjectsCount(driver);
            if (frams > 0)
            {
                driver.SwitchTo().Frame(0);
            }
            var btns = elem_menu.FindElements(By.XPath("//div[@class=\"menu-table-name\"]"));
            foreach (var mnu in btns)
            {
                if (mnu.Text.StartsWith("Climate"))
                {
                    mnu.Click();
                }
                if (mnu.Text.StartsWith("Temperature Curve"))
                {
                    mnu.Click();
                    TooldPages.waitforstring(driver, "Temperature Curve</h1>");
                    Thread.Sleep(5000);
                    IWebElement editbtn = null;
                    while (count++ < 10)
                    {
                        Thread.Sleep(1000);

                        try
                        {
                            editbtn = driver.FindElement(By.XPath("//span[@class=\"icon-edit\"]"));
                        }
                        catch {; }
                        if (editbtn != null)
                        {
                            editbtn.Click();
                            Thread.Sleep(1000);
                            frams = TooldPages.GetframeObjectsCount(driver);
                            if (frams > 0)
                            {
                                driver.SwitchTo().Frame(0);
                            }
                            TooldPages.waitforstring(driver, "app-keypad-action");
                            return;
                        }

                    }
                    if (editbtn == null)
                    {
                        Layers_Handler.instance().Test = true;
                    }
                    Debug.Assert(editbtn != null, "Fail To Get Edit Button");


                }
                else
                {
                    Layers_Handler.instance().Test = true;
                    Debug.Assert(false, "Fail To Get Temperature Curve Menue");
                }

            }

        }
        #endregion

        #region waitForIfram
        /// <summary>
        /// Wait for the main Fram with the string VENTILATION 
        /// This is after we press the Temperture Curve button
        /// </summary>
        /// <param name="driver">The Web Driver</param>
        private void waitForIfram(IWebDriver driver)
        {
            HtmlNode[] nodes = null;
            HtmlNode[] nodes1 = null;
            int count = 0;
            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
            while (count++ < 20)
            {
                Thread.Sleep(500);


                int frams = TooldPages.GetframeObjectsCount(driver);
                if (frams > 0)
                {
                    driver.SwitchTo().Frame(0);
                }
                html.LoadHtml(driver.PageSource);
                nodes = html.DocumentNode.SelectNodes("//div").Where(n => n.GetAttributeValue("class", "").ToLower().StartsWith("big-card-title")).ToArray();
                if (nodes != null)
                {
                    foreach (HtmlNode item in nodes)
                    {
                        if (item.InnerText.StartsWith("VENTILATION"))
                        {
                            count = 1000;
                            break;
                        }
                    }
                }
            }
            IWebElement btn = null;
            for (int i = 0; i < 5; i++)
            {
                html.LoadHtml(driver.PageSource);
                nodes1 = html.DocumentNode.SelectNodes("//button").Where(n => n.GetAttributeValue("class", "").ToLower().StartsWith("header-item header-button")).ToArray();

                btn = null;
                if (nodes != null)
                {
                    foreach (HtmlNode item1 in nodes1)
                    {
                        if (item1.InnerHtml.Contains("button-icon-24 icon-small icon-menu"))
                        {
                            var xpath = item1.XPath;
                            btn = driver.FindElement(By.XPath(xpath));
                            if (btn != null)
                            {
                                btn.Click();
                            }
                        }
                        else
                        {
                            if (item1.InnerHtml.Contains("button-icon-24 icon-small icon-x"))
                                break;
                        }
                    }
                }
                Thread.Sleep(500);
            }
            TooldPages.waitforstring(driver, "Temperature Curve");
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

        [FindsBy(How = How.XPath, Using = "//div[@class=\"header-section\"]")]
        private IWebElement elem_menu;
        #endregion
    }
}


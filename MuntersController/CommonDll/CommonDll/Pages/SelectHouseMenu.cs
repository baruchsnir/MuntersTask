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

namespace FT.CommonDll.Pages
{

    public class SelectHouseMenu
    {

        #region Constructor
        // CONSTRUCTOR
        // 
        /// <summary>
        /// Constructor for Select House Menu
        /// Controll The Side Bar Menu to select the House Menue bar
        /// </summary>
        /// <param name="driver">The given WebDriver</param>
        public SelectHouseMenu(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0,0,30));
        }
        #endregion

        #region Destructor
        // DESTRUCTOR
        // ------
        /// <summary>
        /// Distractor for Select House Menu
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
        public void PressHouseMenueButton()
        {
            var menus = elem_menu.FindElements(By.XPath("//li[@role=\"treeitem\"]"));
            if (menus != null)
            {

                foreach (var menu in menus)
                {
                    if (menu.Text.ToLower().StartsWith("poultry demo"))
                    {
                        var attr = menu.GetAttribute("id");
                        if (attr != "")
                        {
                            if (!attr.Contains("treeview_active"))
                            {
                                menu.Click();
                            }
                        }
                        else
                            menu.Click();
                        Thread.Sleep(500);
                        var menus1 = menu.FindElements(By.XPath("//li[@role=\"treeitem\"]"));
                        foreach (var menu1 in menus1)
                        {
                            if (menu1.Text.ToLower().StartsWith("house"))
                            {
                                var attr1 = menu1.GetAttribute("id");
                                if (attr1 != "")
                                {
                                    if (!attr1.Contains("treeview_active"))
                                    {
                                        menu1.Click();
                                    }
                                }
                                else
                                    menu1.Click();
                                Thread.Sleep(500);
                                var controlers = menu1.FindElements(By.XPath("//li[@role=\"treeitem\"]"));
                                foreach (var control in controlers)
                                {
                                    if (control.Text.ToLower().StartsWith("1"))
                                    {
                                        control.Click();
                                        break;
                                    }
                                }
                                if (controlers == null)
                                {
                                    FLogAddLine("Test Case Go To Menue Temperature Curve Fail", LogType.ltError, "");
                                    Layers_Handler.instance().Test = true;
                                }
                                else
                                    FLogAddLine("Test Case Go To Menue Temperature", LogType.ltBlue, "");
                                Debug.Assert(controlers != null, "Fail to get the House Controller in menue");
                                break;
                            }
                        }
                        break;
                    }
                }
                for(int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    int frams = TooldPages.GetframeObjectsCount(driver);
                    if (frams > 0)
                    {
                        driver.SwitchTo().Frame(0);
                        break;
                    }
                }
                TooldPages.waitforstring(driver, "\"header-section\"");
            }

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
        [FindsBy(How = How.XPath, Using = "//div[@class=\"munters-treeview\"]")]
        private IWebElement elem_menu;
        #endregion
    }
}


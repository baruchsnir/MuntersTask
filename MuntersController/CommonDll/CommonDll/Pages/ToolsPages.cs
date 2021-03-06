﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
//using OpenQA.Selenium.Interactions;
//using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using HtmlAgilityPack;
using System.Linq;
namespace FT.CommonDll.Pages
{
    public class TooldPages
    {

        #region Constructor
        // CONSTRUCTOR
        // ---
        public TooldPages()
        {

        }
        #endregion

        #region Destructor
        // DESTRUCTOR
        // ------
        /// <summary>
        /// Distractor for Io TooldPages
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
        #region WaitForPageToLoad
        /// <summary>
        /// Wait for the page to load in status complete
        /// </summary>
        /// <param name="driver">The working web driver</param>
        /// <returns>Return if status is complete</returns>
        public static bool WaitForPageToLoad(IWebDriver driver)
        {
            try
            {
                TimeSpan timeout = new TimeSpan(0, 0, 30);
                WebDriverWait wait = new WebDriverWait(driver, timeout);
                
                IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
                if (javascript == null)
                    throw new ArgumentException("driver", "Driver must support" +
                        "  javascript execution");
                string readyState = "";
                wait.Until((d) =>
                {
                    Thread.Sleep(500);
                    try
                    {

                        readyState = javascript.ExecuteScript(
                        "if (document.readyState) return document.readyState;").ToString();
                        return readyState.ToLower() == "complete";
                    }
                    catch (InvalidOperationException e)
                    {
                        //Window is no longer available
                        return e.Message.ToLower().Contains("unable to get browser");
                    }
                    catch (WebDriverException e)
                    {
                        //Browser is no longer available
                        return e.Message.ToLower().Contains("unable to connect");
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
                return (readyState.ToLower() == "complete");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ThrowTimeoutException"))
                {

                    return false;
                }
            }
            return false;
        }

        #endregion

        #region wait for string
        /// <summary>
        /// Wait for string that is locate in the page sorce
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="waitfor"></param>
        /// <returns></returns>
        public static bool waitforstring(IWebDriver driver, string waitfor)
        {
            int count = 0;

            if (waitfor != "")
            {
                while (count++ < 600)
                {
                    Thread.Sleep(500);
                    if (driver.PageSource.Contains(waitfor))
                        return true;
                }
            }
            return false;
        }
        #endregion

        #region wait for xpath
        /// <summary>
        /// Wait for elemnt that will apper in the page sorce
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="xpath"></param>
        public static void waitforxpath(IWebDriver driver, string xpath)
        {
            int count = 0;

            if (xpath != "")
            {
                while (count++ < 600)
                {
                    Thread.Sleep(500);
                    try
                    {
                        var elm = driver.FindElement(By.XPath(xpath));
                        if (elm != null)
                            return;
                    }
                    catch {; }

                }
            }
        }
        #endregion

        #region Get frame object
        public static int GetframeObjectsCount(IWebDriver driver)
        {
            HtmlNode[] nodes = null;
            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(driver.PageSource);
            try
            {
                nodes = html.DocumentNode.SelectNodes("//iframe").ToArray();
                return nodes.Length;
            }
            catch {; }
            return 0;
        }
        #endregion
        #endregion

        #region Fields
        // FIELDS
        // ------

        #endregion
    }



}

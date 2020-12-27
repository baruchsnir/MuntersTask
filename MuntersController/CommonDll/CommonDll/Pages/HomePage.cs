using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
// For supporting Page Object Model
// Obsolete - using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
using System;

namespace FT.CommonDll.Pages
{

    public class HomePage
    {

        #region Constructor
        // CONSTRUCTOR
        // 
        /// <summary>
        /// Constructor for Home Page
        /// </summary>
        /// <param name="driver">The given WebDriver</param>
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        #endregion

        #region Destructor
        // DESTRUCTOR
        // ------
        /// <summary>
        /// Distractor for Io HomePage
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
        #region goToPage
        /// <summary>
        /// Go to the designated page
        /// </summary>
        public void goToPage()
        {
            driver.Navigate().GoToUrl(baseURL);
            TooldPages.WaitForPageToLoad(this.driver);
            TooldPages.waitforstring(driver, "<h3>Sign In</h3>");
        }
        #endregion

        #region getPageTitle
        /// <summary>
        ///  Returns the Page Title
        /// </summary>
        /// <returns></returns>
        public String getPageTitle()
        {
            return driver.Title;
        }
        #endregion


        public void LogInToController()
        {
            this.elem_user.Clear();
            this.elem_user.SendKeys(LoginParams.User);
            this.elem_password.Clear();
            this.elem_password.SendKeys(LoginParams.Password);
            this.elem_submit_button.Click();
            TooldPages.WaitForPageToLoad(this.driver);
        }

        #endregion

        #region Fields
        // FIELDS
        // ------
        string baseURL = "https://www.trioair.net/#/home";
        private IWebDriver driver;
 
        [FindsBy(How = How.Id, Using = "signInName")]
        [CacheLookup]
        private IWebElement elem_user;

        [FindsBy(How = How.Id, Using = "password")]
        [CacheLookup]
        private IWebElement elem_password;

        [FindsBy(How = How.XPath, Using = "//button[@form=\"localAccountForm\"]")]
        [CacheLookup]
        private IWebElement elem_submit_button;
        #endregion
    }
}

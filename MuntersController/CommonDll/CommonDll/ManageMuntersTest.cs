using FT.CommonDll.Ini;
using HtmlAgilityPack;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FT.CommonDll.Pages;


namespace FT.CommonDll
{
    [TestFixture]
    public class ManageMuntersTest
    {

        #region Methods
        // METHODS
        // ----------
        #region SetUp Test
        /// <summary>
        /// Start Test - setup web driver
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            try
            {
                UTools Tools = new UTools();

                Log FLog = new Log(Tools.CreateLogFileName(),"",  Layers_Handler.instance().LogDir);
                Layers_Handler.instance().LOG = FLog;
                FLogAddLine("***   Start Controller Test ***", LogType.ltGeneral, "");
                DisplayEventArgs args = new DisplayEventArgs();
                args.StartRunSetTest  = true;
                Layers_Handler.instance().SendDisplayEvent(this, args);
                Layers_Handler.instance().Run = false;
                Layers_Handler.instance().Test = false;
                this.Update_Status("Open Chrom with www.trioair.net");
                baseURL = "https://www.trioair.net/#/home";
                driver = Layers_Handler.instance().Get_Driver(true, baseURL, false);
                //Resize current window to the set dimension
                driver.Manage().Window.Maximize();
                verificationErrors = new StringBuilder();
                string temp = Layers_Handler.instance().RunDir;
                temp += @"\Setup.ini";
               // this.Read_Data_From_ini_File(temp);
               // WaitForPageToLoad(driver);
            }
            catch (Exception ex)
            {
                Layers_Handler.instance().sendMessageBox("Problem in " + this.ToString() + " method SetupTest", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region TeardownTest
        /// <summary>
        /// End of Test
        /// </summary>
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                ITakesScreenshot ssdriver = driver as ITakesScreenshot;
                Screenshot screenshot = ssdriver.GetScreenshot();

                Screenshot tempImage = screenshot;

                tempImage.SaveAsFile(Layers_Handler.instance().LogsDirectory+  @"\WebPage.png",ImageFormat.Png);

                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            DisplayEventArgs args = new DisplayEventArgs();
            args.EndRunSetTest = true;
            Layers_Handler.instance().SendDisplayEvent(this,args);
            FLogAddLine("***   Termination completed ***", LogType.ltGeneral, "");
            Layers_Handler.instance().sendMessageBox("End of Test !!!", null, "Test Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Assert.AreEqual("", verificationErrors.ToString());

        }
        #endregion

        #region Tests
        #region TestCase_Login
        /// <summary>
        /// Open login form and enter usern name and password
        /// </summary>
        [Test]
        public void TestCase_Login()
        {
            Layers_Handler.instance().Test = false;
            try
            {
                //waitforstring(driver, "<h3>Sign In</h3>");
                this.Update_Status("Login to Site");
                FT.CommonDll.Pages.HomePage home = new HomePage(driver);
                home.goToPage();
                home.LogInToController();
                if (waitforstring(driver, "Welcome to TrioAir") != true)
                {
                    FLogAddLine("Test Case Login Fail", LogType.ltError, "");
                    Layers_Handler.instance().Test = true;
                    Debug.Assert(false, "Fail to get Controller Site");
                }
                else
                    FLogAddLine("Test Case Login Pass", LogType.ltBlue, "");
            }
            catch (Exception ex)
            {
                Layers_Handler.instance().Test = true;
                Layers_Handler.instance().sendMessageBox("Problem in " + this.ToString() + " method TestCase_Login", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region  TestCase_GoToMenueTemperatureCurve
        /// <summary>
        /// Got To Menue Temperature Curve
        /// Press the menue button and press the Temperature Curve button
        /// </summary>
        [Test]
        public void TestCase_GoToMenueTemperatureCurve()
        {
            try
            {
                Layers_Handler.instance().Test = false;
                this.Update_Status("press menu button");
                var menus = driver.FindElements(By.XPath("//li[@role=\"treeitem\"]"));
                // IWebElement menu2 = driver.FindElement(By.XPath("//li[@role=\"treeitem\"]"));
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
                }

            }
            catch (Exception ex)
            {
                Layers_Handler.instance().Test = true;
                Layers_Handler.instance().sendMessageBox("Problem in " + this.ToString() + " method TestCase_GoToMenueTemperatureCurve", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region TestCase_OpenTemperatureGurveGrid
        /// <summary>
        /// open Temperature Gurve form
        /// wait for grid to load the data
        /// </summary>
        [Test]
        public void TestCase_OpenTemperatureGurveGrid()
        {
            try
            {
                Layers_Handler.instance().Test = false;
                int count = 0;
                this.Update_Status("Open Temperature Gurve Grid");
                waitForIfram(driver);
                var frams = driver.FindElements(By.TagName("iframe"));
                if (frams.Count > 0)
                {
                    driver.SwitchTo().Frame(0);
                }
                var btns = driver.FindElements(By.XPath("//div[@class=\"menu-table-name\"]"));
                foreach (var mnu in btns)
                {
                    if (mnu.Text.StartsWith("Climate"))
                    {
                        mnu.Click();
                    }
                    if (mnu.Text.StartsWith("Temperature Curve"))
                    {
                        mnu.Click();
                        this.waitforstring(driver, "Temperature Curve</h1>");
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
                                frams = driver.FindElements(By.TagName("iframe"));
                                if (frams.Count > 0)
                                {
                                    driver.SwitchTo().Frame(0);
                                }
                                this.waitforstring(driver, "app-keypad-action");
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
            catch (Exception ex)
            {
                Layers_Handler.instance().Test = true;
                Layers_Handler.instance().sendMessageBox("Problem in " + this.ToString() + " method TestCase_OpenTemperatureGurveGrid", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                if (Layers_Handler.instance().Test)
                    FLogAddLine("Test Case - Open Temperature Gurve Grid Fail", LogType.ltError, "");
                else
                    FLogAddLine("Test Case - Open Temperature Gurve Grid Pass", LogType.ltBlue, "");
            }
        }
        #endregion

        #region TestCase_FillTheGrid
        /// <summary>
        /// Fill the Grid with data
        /// Select Target temperature arount 16 c
        /// Then read the range of each cell and send the mid value
        /// </summary>
        [Test]
        public void TestCase_FillTheGrid()
        {
            try
            {
                Layers_Handler.instance().Test = false;
                this.Update_Status("Fill Grid in first row");
                HtmlNode[] nodes = null;

                int count = 0;
                HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                var frams = driver.FindElements(By.TagName("iframe"));
                for (int i = 0; i < frams.Count; i++)
                {
                    driver.SwitchTo().Frame(i);
                    break;
                }
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
                                if (!fill_data_in_first_row(item, driver))
                                {
                                    Layers_Handler.instance().Test = true;
                                }

                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Layers_Handler.instance().Test = true;
                Layers_Handler.instance().sendMessageBox("Problem in " + this.ToString() + " method TestCase_FillTheGrid", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Layers_Handler.instance().Test)
                    FLogAddLine("Test Case - Fill the Grid Fail", LogType.ltError, "");
                else
                    FLogAddLine("Test Case - Fill the Grid Pass", LogType.ltBlue, "");
            }
        }
        #endregion

        #region TestSave
        /// <summary>
        /// Test The save button senario
        /// Wait for status from the frame
        /// if we see that the button is in disable mod then we fail the test
        /// or we read from the page that there is success answer
        /// </summary>
        [Test]
        public void TestSave()
        {
            try
            {
                HtmlNode[] nodes = null;
                this.Update_Status("Test Save Button");
                Layers_Handler.instance().Test = false;
                HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();

                IWebElement save = null;
                string xpath = "", classname = "";

                var frams = driver.FindElements(By.TagName("iframe"));
                for (int i = 0; i < frams.Count; i++)
                {
                    driver.SwitchTo().Frame(i);
                    break;
                }
                html.LoadHtml(driver.PageSource);
                nodes = html.DocumentNode.SelectNodes("//div").Where(n => n.GetAttributeValue("class", "").ToLower().StartsWith("toolbar-button label-button button-save")).ToArray();
                //driver.SwitchTo().DefaultContent();
                foreach (var item in nodes)
                {
                    xpath = item.ParentNode.XPath;
                    classname = item.ParentNode.GetAttributeValue("class", "");

                }
                var toolbar = driver.FindElement(By.ClassName("hosted-toolbar"));

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
            catch (Exception ex)
            {
                Layers_Handler.instance().Test = true;
                Layers_Handler.instance().sendMessageBox("Problem in " + this.ToString() + " method TestCase_FillTheGrid", ex, "Error Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #endregion

        #region private methods
        #region IsElementPresent
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        #endregion

        #region IsAlertPresent
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        #endregion

        #region Read_Data_From_ini_File
        /// <summary>
        /// Read User name and password from ini file
        /// </summary>
        /// <param name="setup_File_name"></param>
        private void Read_Data_From_ini_File(string setup_File_name)
        {
            IniFile ini = new IniFile(setup_File_name);
            try
            {
                this.user = ini.ReadString("General", "User", this.user);
                this.password = ini.ReadString("General", "Password", this.password);
            }
            catch {; }
        }
        #endregion

        #region wait for string
        /// <summary>
        /// Wait for string that is locate in the page sorce
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="waitfor"></param>
        /// <returns></returns>
        private bool waitforstring(IWebDriver driver, string waitfor)
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
        private void waitforxpath(IWebDriver driver, string xpath)
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
                var frams = driver.FindElements(By.TagName("iframe"));
                for (int i = 0; i < frams.Count; i++)
                {
                    driver.SwitchTo().Frame(i);
                    break;
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
            waitforstring(driver, "Temperature Curve");
            //< button _ngcontent - c4 = "" class="header-item header-button"><div _ngcontent-c4="" class="button-icon-24 icon-small icon-x"></div></button>

            //<div _ngcontent-c4="" class="button-icon-24 icon-small icon-x"></div>
            //<div _ngcontent-c4="" class="button-icon-24 icon-small icon-menu"></div>
            //Debug.Assert(btn != null, "The Menue button is Missing!");
        }
        #endregion

        #region WaitForPageToLoad
        /// <summary>
        /// Wait for the page to load in status complete
        /// </summary>
        /// <param name="driver">The working web driver</param>
        /// <returns>Return if status is complete</returns>
        public bool WaitForPageToLoad(IWebDriver driver)
        {
            try
            {
                TimeSpan timeout = new TimeSpan(0, 0, 30);
                WebDriverWait wait = new WebDriverWait(driver, timeout);

                IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
                if (javascript == null)
                    throw new ArgumentException("driver", "Driver must support javascript execution");
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
            for(int i = 0; i < newData.Length;i++)
            {
                if (newData[i] != -100)
                {
                    switch(i)
                    {
                        //target, cool, tunnel, lowalarm, fhighalarm
                        case 1: this.FLogAddLine(String.Format("Target       : {0:f1}", newData[i]), LogType.ltMagenta, "");break;
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

        #region move_to_element
        /// <summary>
        /// Move to Input element and make mouse over
        /// </summary>
        /// <param name="driver1">The web drive</param>
        /// <param name="input">The input web element</param>
        /// <returns></returns>        
        private bool move_to_element(RemoteWebDriver driver1, IWebElement input)
        {
            bool find_value = false;
            Actions builder = new Actions(driver1);
            try
            {
                if (input != null)
                {
                    builder.MoveToElement(input).Perform();
                    Thread.Sleep(300);
                    builder.Release();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            return find_value;
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
        private bool send_value_to_filed(RemoteWebDriver driver, IWebElement input, ref float val,int target,float[] defaultrange)
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
            HtmlNode[] nodesinputs = null;
            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();

            float[] vals = new float[2];
            string[] inputs;
            int count = 0;
            Thread.Sleep(500);
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
        #endregion

        #region Properties
        // PROPERTIES
        // ----------
        private RemoteWebDriver driver_m
        {
            get { return Layers_Handler.instance().Driver; }
            set { Layers_Handler.instance().Driver = value; }
        }
        private bool stopRun
        {
            get { return Layers_Handler.instance().StopRun; }
        }
        #endregion

        #region Fields
        // FIELDS
        // ------
        private string user = "";
        private string password = "";
        private Dictionary<int, string> line_teturn = new Dictionary<int, string>();
        private RemoteWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private IniFile ini = new IniFile(Layers_Handler.instance().RunDir + @"\Setup.ini");
        private Dictionary<int, string> comments = new Dictionary<int, string>();
        private List<string> txtMaxKeyWordsSearch = new List<string>();
        #endregion

    }
}
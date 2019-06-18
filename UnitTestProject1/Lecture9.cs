using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLectures
{
    [TestClass]
    public class Lecture9
    {
        IWebDriver driver;
        WebDriverWait wait;
        [TestMethod]
        public void TestMethod17()
        {
            driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 15));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=2";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            var list = driver.FindElements(By.CssSelector("table.dataTable tr[class^=row] td:nth-child(3)"));
            int loglist = driver.Manage().Logs.GetLog("browser").Count;
            bool curr = true;
            for (int i = 3; i < list.Count; i++)
            {
                list = driver.FindElements(By.CssSelector("table.dataTable tr[class^=row] td:nth-child(3)"));
                list[i].FindElement(By.CssSelector("a")).Click();
                driver.Navigate().Back();

                curr = loglist == driver.Manage().Logs.GetLog("browser").Count;
                loglist = driver.Manage().Logs.GetLog("browser").Count;
                if (!curr)
                    break;
                list = driver.FindElements(By.CssSelector("table.dataTable tr[class^=row] td:nth-child(3)"));
            }

            try
            {
                Assert.IsTrue(curr);
            }
            catch (Exception)
            {

            }
            finally
            {
                driver.Quit();
                driver = null;
            }

            //последовательно проверять, кликая на каждый объект, что сообщений в логах не появилось (любого уровня)

            //var list = driver.FindElements(By.CssSelector("table.dataTable tr[class^=row] td:nth-child(3)"));
        }
    }
}

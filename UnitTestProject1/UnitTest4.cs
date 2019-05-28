using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest4
    {
        IWebDriver driver;
        [TestMethod]
        public void TestFirefoxNigtly()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.BrowserExecutableLocation = @"C:\Program Files\Firefox Nightly\firefox.exe";
            driver = new FirefoxDriver(firefoxOptions);
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            //driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.Quit();
            driver = null;
        }
        [TestMethod]
        public void TestFirefoxStable()
        {
             FirefoxOptions firefoxOptions = new FirefoxOptions();
             firefoxOptions.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
             driver = new FirefoxDriver(firefoxOptions);
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            //driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.Quit();
            driver = null;
        }
        [TestMethod]
        public void TestFirefoxESR()
        {
            return;
        }

    }
}

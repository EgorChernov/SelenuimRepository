using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace SeleniumLectures
{
    
    [TestClass]
    public class Lecture3
    {
        IWebDriver driver;

        [TestMethod]
        public void TestMethod4Chrome()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            //driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.Quit();
            driver = null;
        }
        [TestMethod]
        public void TestMethod4Firefox()
        {
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            //driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
           driver.FindElement(By.Name("login")).Click();
           driver.Quit();
            driver = null;
        }

        [TestMethod]
        public void TestMethod4IE()
        {
            driver = new InternetExplorerDriver();
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
           // driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.Quit();
            driver = null;
        }

        [TestMethod]
        public void TestMethod5()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.UseLegacyImplementation = true;
            options.BrowserExecutableLocation = @"C:\Program Files\Mozilla45\firefox.exe";
            driver = new FirefoxDriver(options);
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            //driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.Quit();
            driver = null;
        }

        [TestMethod]
        public void TestMethod6()
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
    }
}

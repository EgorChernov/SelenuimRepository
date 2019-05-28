using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace UnitTestProject1
{
    
    [TestClass]
    public class UnitTest3
    {
        IWebDriver driver;

        
        [TestMethod]
        public void TestMethodChrome()
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
        public void TestMethodFirefox()
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
        public void TestMethodIE()
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
    }
}

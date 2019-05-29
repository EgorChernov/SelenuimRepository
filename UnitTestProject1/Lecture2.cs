using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLectures
{
    [TestClass]
    public class Lecture2
    {
        IWebDriver driver;
 
        [TestMethod]
        public void TestMethod3()
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
    }
}

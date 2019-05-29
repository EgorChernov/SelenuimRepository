using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumLectures
{
    [TestClass]
    public class Lecture1
    {
        IWebDriver driver = new ChromeDriver();
        [TestMethod]
        public void TestMethod1()
        {
            driver.Url = "https://www.google.com/";
            driver.FindElement(By.Name("q")).SendKeys("google");
            driver.FindElement(By.Name("btnK")).Click();
            driver.Quit();
            driver = null;
        }
    }
}

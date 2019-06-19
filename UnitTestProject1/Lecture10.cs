using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumLectures
{
    [TestClass]
    public class Lecture10
    {
        IWebDriver driver;
        [TestMethod]
        public void TestMethod1()
        {
            Proxy proxy = new Proxy();
            proxy.HttpProxy = "localhost:8888";
            proxy.Kind = ProxyKind.Manual;
           
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.Proxy = proxy;

            driver = new ChromeDriver(chromeOptions);

            driver.Navigate().GoToUrl("https:\\google.com");
        }
    }
}

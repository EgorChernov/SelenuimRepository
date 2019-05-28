﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1
{
    /// <summary>
    /// Summary description for UnitTest2
    /// </summary>
    [TestClass]
    public class UnitTest2
    {        
        IWebDriver driver;
        WebDriverWait wait;

        [TestMethod]
        [Obsolete]
        public void TestMethod1()
        {            
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = "http://localhost/litecart/admin";
            //driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs(title: "My Store"));
            driver.Quit();
            driver = null;
        }
    }
}

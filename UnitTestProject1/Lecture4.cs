using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumLectures
{
    [TestClass]
    public class Lecture4
    {
        IWebDriver driver;
        [TestMethod]
        public void TestMethod7()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            try
            {
            //   Assert.AreNotEqual(driver.FindElements(By.CssSelector("h1")).Count, 0);
            }
            catch (Exception)
            {
                driver.Quit();
                driver = null;
            }
            ReadOnlyCollection<IWebElement> _parentLinkToClick = driver.FindElements(By.CssSelector("ul#box-apps-menu li#app-"));
            for(int i=0;i<_parentLinkToClick.Count;i++)
            {
                _parentLinkToClick[i].Click();
                try
                {
                    Assert.AreNotEqual(driver.FindElements(By.CssSelector("h1")).Count, 0);
                }
                catch(Exception)
                {
                    driver.Quit();
                    driver = null;
                }
                _parentLinkToClick = driver.FindElements(By.CssSelector("ul#box-apps-menu li#app-"));
                ReadOnlyCollection<IWebElement> _childLinkToClick = _parentLinkToClick[i].FindElements(By.CssSelector("[id^=doc-]"));
                if (_childLinkToClick.Count < 2)
                    //при клике переходит на первый внутренний
                    //или единственный, которого нет в списке
                    continue;
                for (int j = 1; j < _childLinkToClick.Count; j++)
                {
                    //уже в 0 потомке
                    _childLinkToClick[j].Click();
                    try
                    {
                        Assert.AreNotEqual(driver.FindElements(By.CssSelector("h1")).Count, 0);
                    }
                    catch (Exception)
                    {
                        driver.Quit();
                        driver = null;
                    }
                    _parentLinkToClick = driver.FindElements(By.CssSelector("ul#box-apps-menu li#app-"));
                    _childLinkToClick = _parentLinkToClick[i].FindElements(By.CssSelector("[id^=doc-]"));
                }
              //  _parentLinkToClick = driver.FindElements(By.CssSelector("ul#box-apps-menu li#app-"));
            }
            driver.Quit();
            driver = null;
        }

        [TestMethod]
        public void TestMethod8()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.Url = "http://localhost/litecart";

            ReadOnlyCollection<IWebElement> _linkToClick = driver.FindElements(By.CssSelector("li[class^=product] div.image-wrapper"));
            try
            {
                Assert.AreNotEqual(_linkToClick.Count, 0);
            }
            catch (Exception)
            {
                driver.Quit();
                driver = null;
            }

            for (int i = 0; i < _linkToClick.Count; i++)
            {
                int _stickerNew = _linkToClick[i].FindElements(By.CssSelector("div.sticker.new")).Count;
                int _stickerSale = _linkToClick[i].FindElements(By.CssSelector("div.sticker.sale")).Count;
                //понятия не имею почему, но в консоли разработчика пробел заменяется на точки и это работает
                try
                {
                    Assert.IsTrue(
                        (_stickerNew + _stickerSale == 1) &&
                        (_stickerNew * _stickerSale == 0)
                        );
                }
                catch (Exception)
                {
                    driver.Quit();
                    driver = null;
                }
                _linkToClick = driver.FindElements(By.CssSelector("li[class^=product] div.image-wrapper"));
            }
            driver.Quit();
            driver = null;
        }
    }
}

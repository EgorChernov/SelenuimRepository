using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLectures
{
    [TestClass]

    public class Lecture5
    {
        IWebDriver driver;
        [TestMethod]
        public void TestMethod9()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            var list = driver.FindElements(By.CssSelector("tr.row td:nth-child(5)"));

            //в countries сортировка по алфавиту
           // Assert.IsTrue(comparator(driver.FindElements(By.CssSelector("tr.row td:nth-child(5)"))));

            //var list = driver.FindElements(By.CssSelector("tr.row td:nth-child(6)"));
            list = driver.FindElements(By.CssSelector("tr.row"));
            for (int i = 0; i < list.Count; i++)
            {
                //количество зон для страны
                if (list[i].FindElement(By.CssSelector("td:nth-child(6)")).Text.Equals("0"))
                    continue;
                
                //если не 0, то проверяем по алфавиту

                list[i].FindElement(By.CssSelector("td a")).Click();
                bool isSort = comparator(driver.FindElements(By.CssSelector("table.dataTable td:nth-child(3) input[type=hidden]")));

                if (!isSort)
                {
                    Assert.IsTrue(isSort);
                    driver.Quit();
                    driver = null;

                }
                driver.Navigate().Back();
                list = driver.FindElements(By.CssSelector("tr.row"));
            }
            driver.Quit();
            driver = null;
        }


    bool comparator(ReadOnlyCollection<IWebElement> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            if (list[i + 1].Text.CompareTo(list[i].Text) < 0)
                return false;
        }
        return true;
    }


    [TestMethod]
        public void TestMethod10()
        {
            driver = new ChromeDriver();
            TestCampaigns(driver);
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }

            driver = new FirefoxDriver();
            TestCampaigns(driver);
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }

            driver = new InternetExplorerDriver();
            TestCampaigns(driver);
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }

        }

        


        public void TestCampaigns(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));
            CheckNameEquals(driver);
            CheckPriceEquals(driver);
            CheckRegularPrice(driver);
            CheckCampaignPrice(driver);
            CompareCampaignWithRegular(driver);
        }

        void CheckNameEquals(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));
            driver.Manage().Window.Maximize();
            driver.Url = "http://localhost/litecart";

            var box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));

            //проверка совпадения названия

            string name = box.FindElement(By.CssSelector("div.name")).Text;
            if (driver.GetType().Name.Equals("InternetExplorerDriver"))
                driver.Navigate().GoToUrl(driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).GetAttribute("href"));
            else
                driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).Click();
            try
            {
                string x = driver.FindElement(By.CssSelector("div[id=box-product] h1.title")).Text;
                Assert.AreEqual(name, x);
            }
            catch (Exception)
            {
                driver.Quit();
                driver = null;
                return;
            }
            driver.Navigate().Back();
            
        }

        void CheckPriceEquals(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));
            // var box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            wait.Until(ExpectedConditions.TitleContains("Online"));
            string RegularPrice = driver.FindElement(By.CssSelector("s.regular-price")).Text;
            string CampaignPrice = driver.FindElement(By.CssSelector("strong.campaign-price")).Text;
            if (driver.GetType().Name.Equals("InternetExplorerDriver"))
                driver.Navigate().GoToUrl(driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).GetAttribute("href"));
            else
                driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).Click();
            try
            {
                Assert.AreEqual(RegularPrice, driver.FindElement(By.CssSelector("div.content s.regular-price")).Text);
                Assert.AreEqual(CampaignPrice, driver.FindElement(By.CssSelector("div.content strong.campaign-price")).Text);
            }
            catch (Exception)
            {
                driver.Quit();
                driver = null;
                return;
            }
            driver.Navigate().Back();

        }

        void CheckRegularPrice(IWebDriver driver)
        {
            //обычная цена зачеркнутая и серая
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));
            wait.Until(ExpectedConditions.TitleContains("Online"));
            string RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("text-decoration");

            string color = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color");
            Regex regex = new Regex(@"\d{1,3}");
            var rgb = regex.Matches(color);
            bool isgrey = rgb[0].Value.Equals(rgb[1].Value) && rgb[1].Value.Equals(rgb[2].Value);
            isgrey = isgrey && (RegularPricetext.Contains("line-through"));
            try
            {
                Assert.IsTrue(isgrey);
            }
            catch (Exception)
            {
                driver.Quit();
                driver = null;
                return;
            }
            if (driver.GetType().Name.Equals("InternetExplorerDriver"))
                driver.Navigate().GoToUrl(driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).GetAttribute("href"));
            else
                driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).Click();


            RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("text-decoration");
            color = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color");
            //Regex regex = new Regex(@"\d{1,3}");
            rgb = regex.Matches(color);
            isgrey = rgb[0].Value.Equals(rgb[1].Value) && rgb[1].Value.Equals(rgb[2].Value);
            isgrey = isgrey && (RegularPricetext.Contains("line-through"));
            try
            {
                Assert.IsTrue(isgrey);
            }
            catch (Exception)
            {
                driver.Quit();
                driver = null;
                return;
            }
            driver.Navigate().Back();
        }

        void CheckCampaignPrice(IWebDriver driver)
        {
            //акционная жирная и красная
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));
            wait.Until(ExpectedConditions.TitleContains("Online"));
            string CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-weight");
            string color = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color");
            color = color.Substring(color.IndexOf('(') + 1, color.IndexOf(')') - color.IndexOf('(') - 1);

            Regex regex = new Regex(@"\d{1,3}");
            var rgb = regex.Matches(color);
            bool isgrey = rgb[1].Value.Equals("0") && rgb[2].Value.Equals("0");
            isgrey = isgrey && (CampaignPricetext.CompareTo("700") >= 0);
            try
            {
                Assert.IsTrue(isgrey);
            }
            catch (Exception)
            {
                driver.Quit();
                driver = null;
                return;
            }


            if (driver.GetType().Name.Equals("InternetExplorerDriver"))
                driver.Navigate().GoToUrl(driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).GetAttribute("href"));
            else
                driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).Click();

            CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-weight");
            rgb = regex.Matches(color);
            isgrey = rgb[1].Value.Equals("0") && rgb[2].Value.Equals("0");
            isgrey = isgrey && (CampaignPricetext.CompareTo("700") >= 0);
            try
            {
                Assert.IsTrue(isgrey);
            }
            catch (Exception)
            {
                driver.Quit();
                driver = null;
            }

            driver.Navigate().Back();
        }

        void CompareCampaignWithRegular(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));
            wait.Until(ExpectedConditions.TitleContains("Online"));

            //акционная цена крупнее, чем обычная
            // box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            string RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("font-size");
            string CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-size");
            int temp = RegularPricetext.CompareTo(CampaignPricetext);

            try
            {
                Assert.IsTrue(temp < 0);
            }
            catch (Exception)
            {
                driver.Quit();
                driver = null;
            }
            if (driver.GetType().Name.Equals("InternetExplorerDriver"))
                driver.Navigate().GoToUrl(driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).GetAttribute("href"));
            else
                driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).Click();


            RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("font-size");
            CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-size");
            temp = RegularPricetext.CompareTo(CampaignPricetext);

            try
            {
                Assert.IsTrue(temp < 0);
            }
            catch (Exception)
            {
                driver.Quit();
                driver = null;
            }

            driver.Quit();
            driver = null;
            return;
        }
    }
}

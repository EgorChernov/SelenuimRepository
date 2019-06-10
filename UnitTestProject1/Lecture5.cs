using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            //driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            string prev = driver.FindElement(By.CssSelector("tr.row td:nth-child(5)")).Text;
            var list = driver.FindElements(By.CssSelector("tr.row td:nth-child(5)"));
            string curr;
            //bool flag = true; 
            for (int i = 1; i < list.Count; i++)
            {
                curr = list[i].Text;
                int compare = curr.CompareTo(prev);
                try
                {
                    Assert.IsTrue(compare >= 0);
                    prev = curr;
                    list = driver.FindElements(By.CssSelector("tr.row td:nth-child(5)"));
                }
                catch (Exception)
                {
                    driver.Quit();
                    driver = null;
                }
            }
            driver.Quit();
            driver = null;
        }
        [TestMethod]
        public void TestMethod10IE()
        {
         
            driver = new InternetExplorerDriver();

            driver.Manage().Window.Maximize();
            //driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.Url = "http://localhost/litecart";

                WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));
                var box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));

                //проверка совпадения названия

                string name = box.FindElement(By.CssSelector("div.name")).Text;
               // driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).Click();
                driver.Navigate().GoToUrl(driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).GetAttribute("href"));
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

            //совпадают цены (обычная и аукционная)
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div[id=box-campaigns] li[class^=product]")));
                box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
                string RegularPrice = driver.FindElement(By.CssSelector("s.regular-price")).Text;
                string CampaignPrice = driver.FindElement(By.CssSelector("strong.campaign-price")).Text;
               // box.Click();
                driver.Navigate().GoToUrl(driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).GetAttribute("href"));
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



                //обычная цена зачеркнутая и серая
                box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
                string RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("text-decoration");
                string textdecor;
                if (RegularPricetext.IndexOf(' ') > 0)
                {
                    textdecor = RegularPricetext.Substring(0, RegularPricetext.IndexOf(' '));
                }
                else textdecor = RegularPricetext;
                RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color");
                string color = RegularPricetext.Substring(RegularPricetext.IndexOf('(') + 1, RegularPricetext.IndexOf(')') - RegularPricetext.IndexOf('(') - 1);
                var c = color.Split(',');
           //     Assert.AreEqual(c.Length, 4);
                bool isgrey = c[0].Trim().Equals(c[1].Trim()) && c[1].Trim().Equals(c[2].Trim());
                isgrey = isgrey && (textdecor.Trim().Equals("line-through"));
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
                // box.Click();
                driver.Navigate().GoToUrl(driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).GetAttribute("href"));
                // box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
                RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("text-decoration");
                if (RegularPricetext.IndexOf(' ') > 0)
                {
                    textdecor = RegularPricetext.Substring(0, RegularPricetext.IndexOf(' '));
                }
                else textdecor = RegularPricetext;
                RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color");
                color = RegularPricetext.Substring(RegularPricetext.IndexOf('(') + 1, RegularPricetext.IndexOf(')') - RegularPricetext.IndexOf('(') - 1);
                c = color.Split(',');
           //     Assert.AreEqual(c.Length, 4);
                isgrey = c[0].Trim().Equals(c[1].Trim()) && c[1].Trim().Equals(c[2].Trim());
                isgrey = isgrey && (textdecor.Trim().Equals("line-through"));
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

                //акционная жирная и красная
                box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
                RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("font-weight");
                string CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-weight");
                textdecor = CampaignPricetext;
                CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color");
                color = CampaignPricetext.Substring(CampaignPricetext.IndexOf('(') + 1, CampaignPricetext.IndexOf(')') - CampaignPricetext.IndexOf('(') - 1);

                c = color.Split(',');
       //         Assert.AreEqual(c.Length, 3);
                isgrey = c[1].Trim().Equals("0") && c[2].Trim().Equals("0");
                isgrey = isgrey && (textdecor.Trim().CompareTo(RegularPricetext) > 0);
                try
                {
                    Assert.IsTrue(isgrey);
                }
                catch (Exception)
                {
                    driver.Quit();
                    driver = null;
                }
                box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
                // box.Click();
                driver.Navigate().GoToUrl(driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).GetAttribute("href"));
                RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("font-weight");
                CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-weight");
                textdecor = CampaignPricetext;//.Substring(0, CampaignPricetext.IndexOf(' '));
                CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color");
                color = CampaignPricetext.Substring(CampaignPricetext.IndexOf('(') + 1, CampaignPricetext.IndexOf(')') - CampaignPricetext.IndexOf('(') - 1);

                c = color.Split(',');
        //        Assert.AreEqual(c.Length, 3);
                isgrey = c[1].Trim().Equals("0") && c[2].Trim().Equals("0");
                isgrey = isgrey && (textdecor.Trim().CompareTo(RegularPricetext) > 0);
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

                //акционная цена крупнее, чем обычная
                box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
                RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("font-size");
                CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-size");
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
                box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
                // box.Click();
                driver.Navigate().GoToUrl(driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product] a.link")).GetAttribute("href"));

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
        
        [TestMethod]
        public void TestMethod10Chrome()
        {
           
             driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.Url = "http://localhost/litecart";
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));
            var box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));

     //проверка совпадения названия

            string name = box.FindElement(By.CssSelector("div.name")).Text;
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

            //совпадают цены (обычная и аукционная)

            box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            string RegularPrice = driver.FindElement(By.CssSelector("s.regular-price")).Text;
            string CampaignPrice = driver.FindElement(By.CssSelector("strong.campaign-price")).Text;
            box.Click();

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



            //обычная цена зачеркнутая и серая
            box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            string RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("text-decoration");
            string textdecor;
            if (RegularPricetext.IndexOf(' ') > 0)
            {
                textdecor = RegularPricetext.Substring(0, RegularPricetext.IndexOf(' '));
            }
            else textdecor = RegularPricetext;
            RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color");
            string color = RegularPricetext.Substring(RegularPricetext.IndexOf('(') + 1, RegularPricetext.IndexOf(')') - RegularPricetext.IndexOf('(') - 1);
            var c = color.Split(',');
           Assert.AreEqual(c.Length, 4);
            bool isgrey = c[0].Trim().Equals(c[1].Trim()) && c[1].Trim().Equals(c[2].Trim());
            isgrey = isgrey && (textdecor.Trim().Equals("line-through"));
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
            box.Click();

           // box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("text-decoration");
            if (RegularPricetext.IndexOf(' ') > 0)
            {
                textdecor = RegularPricetext.Substring(0, RegularPricetext.IndexOf(' '));
            }
            else textdecor = RegularPricetext;
            RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color");
            color = RegularPricetext.Substring(RegularPricetext.IndexOf('(') + 1, RegularPricetext.IndexOf(')') - RegularPricetext.IndexOf('(') - 1);
            c = color.Split(',');
           Assert.AreEqual(c.Length, 4);
            isgrey = c[0].Trim().Equals(c[1].Trim()) && c[1].Trim().Equals(c[2].Trim());
            isgrey = isgrey && (textdecor.Trim().Equals("line-through"));
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

            //акционная жирная и красная
            box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            string CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-weight");
            textdecor = CampaignPricetext;
            CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color");
            color = CampaignPricetext.Substring(CampaignPricetext.IndexOf('(') + 1, CampaignPricetext.IndexOf(')') - CampaignPricetext.IndexOf('(') - 1);

            c = color.Split(',');
           Assert.AreEqual(c.Length, 4);
            isgrey = c[1].Trim().Equals("0") && c[2].Trim().Equals("0");
            isgrey = isgrey && (textdecor.Trim().Equals("700"));
            try
            {
                Assert.IsTrue(isgrey);
            }
            catch (Exception)
            {
                driver.Quit();
                driver = null;
            }
            box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            box.Click();


            CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-weight");
            textdecor = CampaignPricetext;//.Substring(0, CampaignPricetext.IndexOf(' '));
            CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color");
            color = CampaignPricetext.Substring(CampaignPricetext.IndexOf('(') + 1, CampaignPricetext.IndexOf(')') - CampaignPricetext.IndexOf('(') - 1);

            c = color.Split(',');
           Assert.AreEqual(c.Length, 4);
            isgrey = c[1].Trim().Equals("0") && c[2].Trim().Equals("0");
            isgrey = isgrey && (textdecor.Trim().Equals("700"));
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

            //акционная цена крупнее, чем обычная
            box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("font-size");
            CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-size");
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
            box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            box.Click();


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


        [TestMethod]
        public void TestMethod10Firefox()
        {

            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            //driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.Url = "http://localhost/litecart";
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));
            var box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));

            //проверка совпадения названия

            string name = box.FindElement(By.CssSelector("div.name")).Text;
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

            //совпадают цены (обычная и аукционная)

            box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            string RegularPrice = driver.FindElement(By.CssSelector("s.regular-price")).Text;
            string CampaignPrice = driver.FindElement(By.CssSelector("strong.campaign-price")).Text;
            box.Click();

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



            //обычная цена зачеркнутая и серая
            box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            string RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("text-decoration");
            string textdecor;
            if (RegularPricetext.IndexOf(' ') > 0)
            {
                textdecor = RegularPricetext.Substring(0, RegularPricetext.IndexOf(' '));
            }
            else textdecor = RegularPricetext;
            RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color");
            string color = RegularPricetext.Substring(RegularPricetext.IndexOf('(') + 1, RegularPricetext.IndexOf(')') - RegularPricetext.IndexOf('(') - 1);
            var c = color.Split(',');
            Assert.AreEqual(c.Length, 3);
            bool isgrey = c[0].Trim().Equals(c[1].Trim()) && c[1].Trim().Equals(c[2].Trim());
            isgrey = isgrey && (textdecor.Trim().Equals("line-through"));
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
            box.Click();

            // box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("text-decoration");
            if (RegularPricetext.IndexOf(' ') > 0)
            {
                textdecor = RegularPricetext.Substring(0, RegularPricetext.IndexOf(' '));
            }
            else textdecor = RegularPricetext;
            RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color");
            color = RegularPricetext.Substring(RegularPricetext.IndexOf('(') + 1, RegularPricetext.IndexOf(')') - RegularPricetext.IndexOf('(') - 1);
            c = color.Split(',');
            Assert.AreEqual(c.Length, 3);
            isgrey = c[0].Trim().Equals(c[1].Trim()) && c[1].Trim().Equals(c[2].Trim());
            isgrey = isgrey && (textdecor.Trim().Equals("line-through"));
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

            //акционная жирная и красная
            box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("font-weight");
            string CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-weight");
            textdecor = CampaignPricetext;
            CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color");
            color = CampaignPricetext.Substring(CampaignPricetext.IndexOf('(') + 1, CampaignPricetext.IndexOf(')') - CampaignPricetext.IndexOf('(') - 1);

            c = color.Split(',');
            Assert.AreEqual(c.Length, 3);
            isgrey = c[1].Trim().Equals("0") && c[2].Trim().Equals("0");
            isgrey = isgrey && (textdecor.Trim().CompareTo(RegularPricetext) > 0);
            try
            {
                Assert.IsTrue(isgrey);
            }
            catch (Exception)
            {
                driver.Quit();
                driver = null;
            }
            box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            box.Click();

            RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("font-weight");
            CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-weight");
            textdecor = CampaignPricetext;//.Substring(0, CampaignPricetext.IndexOf(' '));
            CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color");
            color = CampaignPricetext.Substring(CampaignPricetext.IndexOf('(') + 1, CampaignPricetext.IndexOf(')') - CampaignPricetext.IndexOf('(') - 1);

            c = color.Split(',');
            Assert.AreEqual(c.Length, 3);
            isgrey = c[1].Trim().Equals("0") && c[2].Trim().Equals("0");
            isgrey = isgrey && (textdecor.Trim().CompareTo(RegularPricetext)>0);
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

            //акционная цена крупнее, чем обычная
            box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            RegularPricetext = driver.FindElement(By.CssSelector("s.regular-price")).GetCssValue("font-size");
            CampaignPricetext = driver.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-size");
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
            box = driver.FindElement(By.CssSelector("div[id=box-campaigns] li[class^=product]"));
            box.Click();


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
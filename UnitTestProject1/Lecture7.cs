using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace SeleniumLectures
    {
    [TestClass]
    public class Lecture6
    {
        IWebDriver driver;
        WebDriverWait wait;
        string email = "sriaadya.sabeen@99cows.com";
        string password = "password";
        string path = @"../../../root/testimage.jpg";
        //   TimeSpan defaultTimeSpan;
        [TestMethod]
        public void TestMethod11()
        {
            //создайте сценарий регистрации пользователя
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost/litecart");

            driver.FindElement(By.CssSelector("[name=login_form] td a")).Click();
            if (driver.Title.Contains("Create Account"))
                Assert.IsTrue(driver.Title.Contains("Create Account"));


            // создание аккаунта


            driver.FindElement(By.Name("firstname")).SendKeys("firstnameTest");
            driver.FindElement(By.Name("lastname")).SendKeys("lastnameTest");
            driver.FindElement(By.Name("address1")).SendKeys("address1Test");
            driver.FindElement(By.Name("postcode")).SendKeys("12345");
            driver.FindElement(By.Name("city")).SendKeys("cityTest");

            //В качестве страны выбирайте United States, штат произвольный

            var selectElement = new SelectElement(driver.FindElement(By.Name("country_code")));
            selectElement.SelectByValue("US");
            driver.FindElement(By.Name("phone")).SendKeys("+123456");

            //вводим email

            driver.FindElement(By.Name("email")).SendKeys(email);

            //ввели пароль
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("confirmed_password")).SendKeys(password);
            driver.FindElement(By.Name("create_account")).Click();

            //необходимо выбрать штат
            selectElement = new SelectElement(driver.FindElement(By.Name("zone_code")));
            selectElement.SelectByIndex(3);


            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("confirmed_password")).SendKeys(password);

            driver.FindElement(By.Name("create_account")).Click();

            //logout
            var boxlist = driver.FindElements(By.CssSelector("div#box-account li"));
            bool flag = false;
            for (int i = 0; i < boxlist.Count; i++)
            {

                if (boxlist[i].Text.Equals("Logout"))
                {
                    boxlist = driver.FindElements(By.CssSelector("div#box-account li"));
                    boxlist[i].FindElement(By.CssSelector("a")).Click();
                    flag = true;
                    break;
                }
            }
            Assert.IsTrue(flag);

            //login
            Assert.IsTrue(driver.Title.Contains("Online Store"));
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("password")).SendKeys(password);

            driver.FindElement(By.Name("login")).Click();
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));

        }


        [TestMethod]
        public void TestMethod12()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            var list = driver.FindElements(By.CssSelector("ul[class=list-vertical] li[id=app-]"));

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].FindElement(By.CssSelector("span.name")).Text.Equals("Catalog"))
                {
                    list[i].Click();
                    break;
                }
                list = driver.FindElements(By.CssSelector("ul[class=list-vertical] li[id=app-]"));
            }

            //кнопка добавить новый продукт

            list = driver.FindElements(By.CssSelector("a.button"));
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Text.Contains("New Product"))
                {
                    list[i].Click();
                    break;
                }
                list = driver.FindElements(By.CssSelector("a.button"));
            }

            // ввод информации
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            driver.FindElement(By.CssSelector("input[type=radio]")).Click();

            driver.FindElement(By.CssSelector("input[name^=name]")).SendKeys("TestName");
            driver.FindElement(By.CssSelector("input[name=code]")).SendKeys("TestCode");
            driver.FindElement(By.CssSelector("input[name^=new_images")).SendKeys(Path.GetFullPath(path));


            list = driver.FindElements(By.CssSelector("div.tabs li a"));

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Text.Contains("Information"))
                {
                    list[i].Click();
                    break;
                }
                list = driver.FindElements(By.CssSelector("div.tabs li a"));
            }

            //в Information изменим производителя

            var option = new SelectElement(driver.FindElement(By.Name("manufacturer_id")));
            option.SelectByValue("1");


            // изменим Prices
            list = driver.FindElements(By.CssSelector("div.tabs li a"));

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Text.Contains("Prices"))
                {
                    list[i].Click();
                    break;
                }
                list = driver.FindElements(By.CssSelector("div.tabs li a"));
            }

            driver.FindElement(By.Name("purchase_price")).Clear();
            driver.FindElement(By.Name("purchase_price")).SendKeys("20");

            option = new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code")));
            option.SelectByValue("USD");

            list = driver.FindElements(By.CssSelector("input[name^=prices]"));
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Clear();
                list[i].SendKeys("1");
                list = driver.FindElements(By.CssSelector("input[name^=prices]"));
            }

            driver.FindElement(By.CssSelector("button[name=save]")).Click();
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            list = driver.FindElements(By.CssSelector("table.dataTable tr[class^=row] td:nth-child(3)"));
            bool flag = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Text.Contains("TestName"))
                {
                    flag = true;
                    break;
                }
            }
            try
            {
                Assert.IsTrue(flag);
            }
            catch (Exception)
            {

            }
            finally
            {
                driver.Quit();
                driver = null;
            }
        }
    }
    }


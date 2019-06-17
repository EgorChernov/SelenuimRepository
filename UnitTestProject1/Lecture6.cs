using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLectures
{
    [TestClass]
    public class Lecture7
    {
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait;

        string email = "sriaadya.sabeen@99cows.com";
        string password = "password";
        string path = @"../../../root/testimage.jpg";
        
        [TestMethod]
        public void TestMethod13()
        {
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            driver.Manage().Window.Maximize();
            //   driver.Navigate().GoToUrl("http://localhost/litecart");
            driver.Url = "http://litecart.stqa.ru/en/";

            //запомнили текущее количество товаров в корзине
            var current_value = driver.FindElement(By.CssSelector("span.quantity")).Text;
           // SelectElement option;
            for(int i = 0; i < 3; i++)
            {
                wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
                driver.FindElement(By.CssSelector("div.content a.link")).Click();
                //может быть несколько размеров

                try
                {
                    
                    SelectElement selectElement = new SelectElement(driver.FindElement(By.CssSelector("select")));
                    selectElement.SelectByIndex(2);
                }
                catch(Exception)
                {

                }
                
                current_value = driver.FindElement(By.CssSelector("span.quantity")).Text;
                
                driver.FindElement(By.CssSelector("button[name=add_cart_product]")).Click();
                wait.Until(d => d.FindElement(By.CssSelector("span.quantity")).Text.CompareTo(current_value) != 0);


            
                driver.Navigate().Back();
                wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            }
            //добавлено 3 товара в корзину, далее удаляем их 

            driver.FindElement(By.CssSelector("div#cart a.link")).Click();
            var buttonlist = driver.FindElements(By.CssSelector("button[name=remove_cart_item]"));
            do
            {
                buttonlist = driver.FindElements(By.CssSelector("button[name=remove_cart_item]"));
                for(int i = 0; i < buttonlist.Count; i++)
                {
                    buttonlist = driver.FindElements(By.CssSelector("button[name=remove_cart_item]"));

                    try
                    {
                        if (buttonlist[i].Displayed)
                            buttonlist[i].Click();
                    }
                    catch (Exception) { }
                }
            }
            while (buttonlist.Count > 0);
            driver.Navigate().Back();
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            try
            {
                Assert.AreEqual(driver.FindElement(By.CssSelector("span.quantity")).Text, "0");
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
        [TestMethod]
        public void TestMethod14()
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 15));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();


            var list = driver.FindElements(By.CssSelector("tr.row td:nth-child(7)"));
            Random random = new Random();
            list[random.Next(list.Count)].FindElement(By.CssSelector("a")).Click();

            list = driver.FindElements(By.CssSelector("form a[target=_blank"));

            for(int i = 0; i < list.Count; i++)
            {
                list = driver.FindElements(By.CssSelector("form a[target=_blank"));
                list[i].Click();
                var mainwindow = driver.CurrentWindowHandle;
                wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                var test = driver.WindowHandles;
                bool flag = false;
    
                for (int j = 0; j < test.Count; j++)
                {
                    if (!(test[j].Equals(mainwindow))){
                        flag = true;
                        driver.SwitchTo().Window(test[j]);
                        break;
                    }
                }
                Assert.IsTrue(flag, "Не обнаружили новую вкладку");

                driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 0, 0, 400);
                driver.Close();
               driver.SwitchTo().Window(mainwindow);

                
            }
            driver.Quit();
            driver = null;
        }
    }
}
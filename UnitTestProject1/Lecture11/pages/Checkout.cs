using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture11
{
    internal class Checkout : Page
    {
        public Checkout(IWebDriver driver) : base(driver) { }

        internal Checkout Open()
        {
            driver.FindElement(By.CssSelector("div#cart a.link")).Click();
            return this;
        }

    //    [FindsBy(How = How.Name, Using = "remove_cart_item")]
    //    private IWebElement removeButton;

        internal void RemoveItems()
        {
            var button = driver.FindElements(By.Name("remove_cart_item"));
            while (button.Count > 0) 
            {
                button = driver.FindElements(By.CssSelector("button[name=remove_cart_item]"));
                for (int i = 0; i < button.Count; i++)
                {
                    button = driver.FindElements(By.CssSelector("button[name=remove_cart_item]"));

                    try
                    {
                        if (button[i].Displayed)
                            button[i].Click();
                    }
                    catch (Exception) { }
                }
            }
            
        }



        internal void BackToStore()
        {
            driver.Navigate().Back();
            wait.Until(d => d.Title.Contains("Online Store"));
        }
    }


}

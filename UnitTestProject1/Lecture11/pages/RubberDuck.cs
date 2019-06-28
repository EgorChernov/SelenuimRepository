using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture11
{
    internal class RubberDuck : Page
    {
        private string currentItemsCount;
        public RubberDuck(IWebDriver driver) : base(driver) { }

        internal bool selectFound()
        {
            return driver.FindElements(By.CssSelector("select")).Count > 0;
        }

        internal void SelectByIndex(int index)
        {
            //  SelectElement selectElement = new SelectElement(driver.FindElement(By.CssSelector("select")));
            //   selectElement.SelectByIndex(index);

            new SelectElement(driver.FindElement(By.CssSelector("select"))).SelectByIndex(index);
        }

    //    [FindsBy(How = How.Name, Using = "add_cart_product")]
    //    internal IWebElement AddButton;

        internal void AddCart()
        {
            if (selectFound())
                SelectByIndex(2);
            currentItemsCount = CountItems();

            driver.FindElement(By.Name("add_cart_product")).Click();
            //AddButton.Click();
        }

        internal void BackToStore()
        {
            wait.Until(d => 
            driver.FindElement(By.CssSelector("span.quantity"))
            .Text.CompareTo(currentItemsCount) != 0);
            driver.Navigate().Back();

            wait.Until(d => d.Title.Contains("Online Store"));
        }
     //   [FindsBy(How = How.CssSelector, Using = "span.quantity")]
     //   IWebElement itemCount;

        private String CountItems()
        {
            return driver.FindElement(By.CssSelector("span.quantity")).Text;
          //  return itemCount.Text;
        }

    }
}

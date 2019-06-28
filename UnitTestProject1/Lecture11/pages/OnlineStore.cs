using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture11
{
 

    internal class OnlineStore : Page
    {
        public OnlineStore(IWebDriver driver) : base(driver) { }

        internal OnlineStore Open()
        {
            driver.Url = "http://litecart.stqa.ru/en/";
            return this;
        }

        internal bool IsOnThisPage()
        {
            return driver.FindElements(By.CssSelector("li[class^=product]")).Count > 0;
        }

    //    [FindsBy(How = How.CssSelector, Using = "li[class^=product]")]
   //     internal IWebElement FirstDuck;


        internal void SubmitChoose()
        {
           
           // var test = FirstDuck;
           // FirstDuck.FindElements(By.CssSelector(""));
            driver.FindElement(By.CssSelector("li[class^=product]")).Click();
            wait.Until(d => d.FindElement(By.Name("add_cart_product")));
        }

        public String CountItems()
        {
            return driver.FindElement(By.CssSelector("span.quantity")).Text;
        }
    }
    

}

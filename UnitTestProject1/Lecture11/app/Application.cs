using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture11
{
    public class Application
    {
        private IWebDriver driver;

        private Checkout checkout;
        private OnlineStore onlineStore;
        private RubberDuck rubberDuck;

        public Application()
        {
            driver = new ChromeDriver();
            checkout = new Checkout(driver);
            onlineStore = new OnlineStore(driver);
            rubberDuck = new RubberDuck(driver);

        }

        private void ChooseItems()
        {
            onlineStore.Open();
            if (onlineStore.IsOnThisPage())
                
                onlineStore.SubmitChoose();
        }

        private void AddToCart()
        {
            rubberDuck.AddCart();
            rubberDuck.BackToStore();
        }

        internal void RemoveAllItems()
        {
            checkout.Open();
            checkout.RemoveItems();
            checkout.BackToStore();
        }

        internal void FillItems(int value)
        {
            Assert.IsTrue(value > 0);

            for (int i = 0; i < value; i++)
            {
                ChooseItems();
                AddToCart();
            }
        }

        public void Quit()
        {
            driver.Quit();
        }

        internal string ItemsCount()
        {
            return onlineStore.CountItems();
        }
    }

;



}

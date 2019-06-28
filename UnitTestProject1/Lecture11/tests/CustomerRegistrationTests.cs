using System;
using NUnit.Framework;

namespace Lecture11
{
    [TestFixture]
    public class CustomerRegistrationTests : TestBase
    {
        [Test]
        public void WorkCart()
        {
           // app = new Application();
            app.FillItems(3);
            Assert.IsFalse(app.ItemsCount().Equals("0"));
            app.RemoveAllItems();
            Assert.IsTrue(app.ItemsCount().Equals("0"));
        }
    }
}

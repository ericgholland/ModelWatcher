//This is a test header
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelWatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    [TestClass]
    public class Testing
    {
        [TestMethod]
        public void NewContextHasNoResults()
        {
            Customer customer = new Customer();
            ModelWatcherContext context = new ModelWatcherContext();
            Assert.IsTrue(context.GetResults().Count() == 0);
        }

        [TestMethod]
        public void InvalidCustomerNameGeneratesError()
        {
            Customer customer = new Customer();
            ModelWatcherContext context = new ModelWatcherContext();
            context.Watch(customer, x => x.FirstName).ErrorWhen(x => String.IsNullOrEmpty(x.FirstName));
            context.CheckAllRules();
            Assert.IsTrue(context.GetResults().Count() == 1);
        }

        [TestMethod]
        public void ValidCustomerNameGeneratesNoError()
        {
            Customer customer = new Customer();
            customer.FirstName = "Ted";
            ModelWatcherContext context = new ModelWatcherContext();
            context.Watch(customer, x => x.FirstName).ErrorWhen(x => String.IsNullOrEmpty(x.FirstName));
            context.CheckAllRules();
            Assert.IsTrue(context.GetResults().Count() == 0);
        }
    }
}
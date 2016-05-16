using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Remoting.Proxies;
using System.Text.RegularExpressions;
using LgLcd.Adapter;
using LgLcdG13.Adapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LgLcd.Adapter.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            string friendlyName = "testapp";

            var connection = new Connection();
            var success = connection.Success;
            LcdProxy.LogiLcdMonoSetText(0, "this is a test");
            LcdProxy.LogiLcdUpdate();

            Console.WriteLine(success);

            Assert.IsTrue(success);

        }

        [TestMethod]
        public void MyTestMethod()
        {

        }
    }
}

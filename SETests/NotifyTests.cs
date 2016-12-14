using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Tests
{
    [TestClass()]
    public class NotifyTests
    {
        /// <summary>
        /// Test
        /// </summary>
        [TestMethod()]
        public void checkErrorTest()
        {
            Notify test = new Notify();
            bool r = test.checkError("123a", null);
            Assert.IsFalse(r);
        }
    }
}
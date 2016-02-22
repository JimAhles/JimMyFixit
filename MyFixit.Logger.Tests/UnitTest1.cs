using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFixIt.Logging;

namespace MyFixIt.Logging.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestCategory("Controller"), TestMethod]
        public void LogWarningTest()
        {
            Logger log = new Logger();
            try
            {
                log.Warning("this is a meangingless test!");
            }
            catch (Exception ex)
            {
                Assert.Fail("LogWarningTest blew up with excetpion " + ex.Message, null);
            }
            Assert.IsTrue(true); // always pass if not blow up
        }
    }
}

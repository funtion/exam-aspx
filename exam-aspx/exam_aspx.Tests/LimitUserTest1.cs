using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Models;
using exam_aspx.Entity;


namespace exam_aspx.Tests
{
    [TestClass]
    public class LimitUserTest1
    {
        LimitUserModel modle = new LimitUserModel();
        [TestMethod]
        public void TestIsAllowed()
        {
            Assert.IsTrue(modle.isAllowed("12345678"));
            Assert.IsFalse(modle.isAllowed("87654321"));
        }
    }
}

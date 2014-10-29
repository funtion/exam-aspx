using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Models;
using exam_aspx.Entity;


namespace exam_aspx.Tests
{
    [TestClass]
    public class LimitUserTest
    {
        LimitUserModel modle = new LimitUserModel();
        [TestMethod]
        public void TestIsAllowed()
        {
            Assert.IsTrue(modle.isAllowed("12345678"));
            Assert.IsFalse(modle.isAllowed("87654321"));
        }
        [TestMethod]
        public void TestAddLimitUser()
        {
            Assert.IsTrue(modle.addLimitUser("09012434")>0);
            
        }
        [TestMethod]
        public void TestGetAllLimitUser()
        {
            Assert.IsTrue(modle.getAllLimitUser().Count > 0);
        }
        [TestMethod]
        public void TestDelLimitUser()
        {
            Assert.IsTrue(modle.delLimitUser(1) ==1 );
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Models;


namespace exam_aspx.Tests
{
    [TestClass]
    public class StudentTest
    {
        [TestMethod]
        public void TestLogin()
        {
            StudentModel model = new StudentModel();
            Assert.IsTrue(model.login("12345678", "abc"));
            Assert.IsFalse(model.login(" Or 1 -- ;", "nonono"));
        }
        
    }
}

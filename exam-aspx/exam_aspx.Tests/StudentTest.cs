using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Models;
using exam_aspx.Entity;

namespace exam_aspx.Tests
{
    [TestClass]
    public class StudentTest
    {
        StudentModel model = new StudentModel();
        [TestMethod]
        public void TestLogin()
        {
            
            Assert.AreEqual (model.login("12345678", "abc"),1);
            Assert.AreEqual(model.login(" Or 1=1 -- ;", "nonono"),-1);
        }

        [TestMethod]
        public void TestRegister()
        {
            string sid = "123", name = "aaa", password = "bbbb";
            StudentEntity student = new StudentEntity(sid,name,password);
            model.register(student);
            Assert.AreEqual( model.login(sid, password) ,2);
        }
        [TestMethod]
        public void TestIsRegisted()
        {
            Assert.IsTrue(model.isRegisted("12345678"));
            Assert.IsFalse(model.isRegisted("987654321"));
        }
        
    }
}

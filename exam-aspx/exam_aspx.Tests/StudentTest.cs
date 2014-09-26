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
            
            Assert.IsTrue(model.login("12345678", "abc"));
            Assert.IsFalse(model.login(" Or 1 -- ;", "nonono"));
        }

        [TestMethod]
        public void TestRegister()
        {
            string sid = "123", name = "aaa", password = "bbbb";
            StudentEntity student = new StudentEntity(sid,name,password);
            model.register(student);
            Assert.IsTrue(model.login(sid, password));

        }
        
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Models;

namespace exam_aspx.Tests
{
    [TestClass]
    public class TeacherTest
    {
        public TeacherModel model = new TeacherModel();
        [TestMethod]
        public void TestGetTeacher()
        {
            Assert.IsNotNull(model.getTeacher("admin", "admin"));
        }
    }
}

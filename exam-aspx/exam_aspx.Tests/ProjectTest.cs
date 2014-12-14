using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Models;
using exam_aspx.Entity;

namespace exam_aspx.Tests
{
    [TestClass]
    public class ProjectTest
    {
        public ProjectModel model = new ProjectModel();
        [TestMethod]
        public void TestAddProject()
        {
            var row = model.AddProject("1", "1", "1", "1", "1", "1", "1", "1");
            Assert.AreEqual(row, 1);

        }
        [TestMethod]
        public void TestGetAllProject()
        {
            var project = model.getAllProject("course1", "2014", "homework1", "x");
            Assert.IsTrue(project.id > 0);
        }
    }
}

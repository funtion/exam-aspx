using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Entity;
using exam_aspx.Models;

namespace exam_aspx.Tests
{
    [TestClass]
    public class FileTest
    {
        FileModel model = new FileModel();
        [TestMethod]
        public void TestGetFiles()
        {
            var res = model.getFiles();
            Assert.AreEqual(1, res.Length);
            Assert.AreEqual("f", res[0].name);
            Assert.AreEqual("f", res[0].path);
            Assert.AreEqual(2334, res[0].size);
            Assert.AreEqual(new DateTime(2014,10,4,17,15,49), res[0].time);
        }
        [TestMethod]
        public void TestAddFile()
        {
            Assert.AreEqual(1, model.addFile("f2", "f2", 1234));
        }
        [TestMethod]
        public void TestDelFile()
        {
            Assert.AreEqual(1, model.deleFile(2));
        }
        [TestMethod]
        public void TestGetNumberOfFile()
        {
            Assert.AreEqual(1, model.getTheNumberOfFile());
        }
    }
}

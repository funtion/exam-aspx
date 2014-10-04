using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Entity;
using exam_aspx.Models;

namespace exam_aspx.Tests
{
    [TestClass]
    public class AnnouncementTest
    {
        AnnouncementModel model = new AnnouncementModel();
        [TestMethod]
        public void TestGetAnnouncement()
        {
            var res = model.getAnnouncements();
            Assert.AreEqual(1, res.Length);
            Assert.AreEqual("tit", res[0].title);
            Assert.AreEqual("announce", res[0].content);
            Assert.AreEqual(1, res[0].display);
            Assert.AreEqual(new DateTime(2014, 10, 4, 15, 45, 41), res[0].time);
        }
        [TestMethod]
        public void TestAddAnouncement()
        {
            int result = model.addAnnouncement("test", "test");
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void TestDelAnnouncement()
        {
            Assert.AreEqual(1,model.delAnnouncement(3));
        }

    }
}

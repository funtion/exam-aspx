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
            var res = model.getAnnouncements(0,5);
            Assert.AreEqual(2, res.Length);
            Assert.AreEqual("test", res[0].title);
            Assert.AreEqual("test", res[0].content);
            Assert.AreEqual(1, res[0].display);
            Assert.AreEqual(new DateTime(2014, 10, 6, 14, 5, 35), res[0].time);
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
        [TestMethod]
        public void TestAnnouncementNumber()
        {
            Assert.AreEqual(2, model.getNumberOfDisplayAnnouncement());
        }
        [TestMethod]
        public void TestUpdateAnnouncement()
        {   
            
            Assert.AreEqual(1,model.updateAnnouncement(1,"test1","this is aa a test"));
        }

    }
}

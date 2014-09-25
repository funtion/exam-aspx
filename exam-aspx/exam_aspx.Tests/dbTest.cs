using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Odbc;
using System.Configuration;
using exam_aspx.Models;
namespace exam_aspx.Tests
{
    [TestClass]
    public class dbTest
    {
        [TestMethod]
        public void TestDbConnection()
        {
            try 
            {
                BaseModel model = new BaseModel();
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}

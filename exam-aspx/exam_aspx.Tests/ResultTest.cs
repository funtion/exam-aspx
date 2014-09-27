using System;
using exam_aspx.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
namespace exam_aspx.Tests
{
    [TestClass]
    public class ResultTest
    {
        [TestMethod]
        public void TestGetResultByStudentId()
        {
            
            ResultModel model = new ResultModel();
            List<ResultEntity> result = model.getResultByStudentId(11);
            Assert.IsTrue(result.Count>0);
            Console.WriteLine(result[0].mScore);

        }
        [TestMethod]
        public void TestGetResultByExaminationId()
        {
            ResultModel model = new ResultModel();
            List<ResultEntity> result = model.getResultByExaminationId(11);
            Assert.IsTrue(result.Count > 0);
            Console.WriteLine(result[0].mScore);
        }
        [TestMethod]
        public void TestGetResultById()
        {
            ResultModel model = new ResultModel();
            ResultEntity result = model.getResultById(1);
            Assert.IsNotNull(result);
            Console.WriteLine(result.mScore);
        }
       
    }
}

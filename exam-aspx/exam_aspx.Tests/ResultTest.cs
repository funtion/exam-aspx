using System;
using exam_aspx.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace exam_aspx.Tests
{
    [TestClass]
    public class ResultTest
    {
        [TestMethod]
        public void TestGetResultById()
        {
            
            ResultModel model = new ResultModel();
            ResultEntity result = model.getResultById(1);
            Assert.IsNotNull(result);
            //[{"test":"test","name":"medition"},{"test":"test1","name":"medition1"}]
            Console.WriteLine(result.id);
            JArray resArray =(JArray) JsonConvert.DeserializeObject(result.sQuestion);
            for (int i = 0; i < resArray.Count; i++)
            {
                Console.WriteLine(resArray[i]["test"].ToString());
                Console.WriteLine(resArray[i]["name"].ToString());

            }           
        }
       
    }
}

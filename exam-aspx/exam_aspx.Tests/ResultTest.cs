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
        public void TestgetResultById()
        {
            
            ResultModel model = new ResultModel();
            ResultEntity result = model.getResultById(1);
            Assert.IsNotNull(result);
            Console.WriteLine(result.id);
            JArray resArray =(JArray) JsonConvert.DeserializeObject(result.mQuestion);
            for (int i = 0; i < resArray.Count; i++)
            {
                Console.WriteLine(resArray[i]["test"].ToString());
                Console.WriteLine(resArray[i]["name"].ToString());

            }
            
            
            
            
        }
    }
}

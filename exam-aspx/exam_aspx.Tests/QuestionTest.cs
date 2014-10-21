using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Models;

namespace exam_aspx.Tests
{
    [TestClass]
    public class QuestionTest
    {
        [TestMethod]
        public void testAddQuestion()
        {   
            QuestionModel model = new QuestionModel();
            int i = model.addQuesiton(0, "test", "test", "test", "test", 1);
            Assert.IsTrue(i > 0);

        }
        [TestMethod]
          public void testDeleteQuestion()
         {
            QuestionModel model = new QuestionModel();
            Assert.IsTrue(model.deleteQuestion(14)==1);
            
           
         }
        [TestMethod]
        public void testDeleteQuestionByExamId()
        {
            QuestionModel model = new QuestionModel();
            Assert.IsTrue(model.deleteQuestionByExamId(13)>0);


        }

    }
}

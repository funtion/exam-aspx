using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Models;
using exam_aspx.Entity;
using System.Drawing;
namespace exam_aspx.Tests
{
    [TestClass]
    public class ExamTest1
    {
        [TestMethod]
        public void TestParse()
        {
            ExamModel model = new ExamModel();
            ExamEntity exam = model.praseFromDoc("doc/chapter1.docx");
            Assert.IsNotNull(exam);
            Assert.AreEqual(20, exam.time);
            Assert.AreEqual(2, exam.tNumber);
            Assert.AreEqual(2, exam.tScore);
            Assert.AreEqual(1, exam.sNumber);
            Assert.AreEqual(3, exam.sScore);
            Assert.AreEqual(0, exam.mNumber);
            Assert.AreEqual(4, exam.mScore);

            Assert.AreEqual(3, exam.tf.Count);
            Assert.AreEqual(1, exam.sc.Count);
            Assert.AreEqual(0, exam.mc.Count);

            Question tf0 = exam.tf[0] as Question;
            Question tf1 = exam.tf[1] as Question;
            Question tf2 = exam.tf[2] as Question;

            
            Assert.AreEqual(tf0.ans, "F");
            Assert.AreEqual(tf1.ans, "T");
            Assert.AreEqual(tf2.ans, "T");
            System.Console.WriteLine (tf0.statement);
            System.Console.WriteLine (tf1.statement);
            System.Console.WriteLine (tf2.statement);

            Assert.IsNull(tf0.image);
            Assert.IsNotNull(tf1.image);
            Assert.IsNull(tf2.image);

            


            Question sc0 = exam.sc[0] as Question;
            Assert.AreEqual(sc0.ans, "A");
            System.Console.WriteLine(sc0.statement);
            foreach(String choice in sc0.choices)
            {
                System.Console.WriteLine(choice);
            }

        }
    }
}

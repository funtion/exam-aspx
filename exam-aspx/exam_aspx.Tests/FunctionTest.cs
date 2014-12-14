using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using exam_aspx.Controllers;

namespace exam_aspx.Tests
{
    [TestClass]
    public class FunctionTest
    {
        [TestMethod]
        public void TestExecute()
        {
            var cmd = "javac.exe  F:\\Server\\C#\\exam-aspx\\exam-aspx\\exam-aspx\\upload\\project\\course1\\2014\\homeowrk1\\xx\\*.java";
            //var cmd = "java -vesion";
            Function.Execute(cmd);
            cmd = "jar cf  xxx.jar  F:\\Server\\C#\\exam-aspx\\exam-aspx\\exam-aspx\\upload\\project\\course1\\2014\\homeowrk1\\xx\\*.class";
            Function.Execute(cmd);
        }
    }
}

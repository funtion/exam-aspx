using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exam_aspx.Entity
{
    public class ResultEntity
    {
        public int id { get; set; }
        public int studentId { get; set; }
        public int examinationId { get; set; }
        public string sQuestion  { get; set; }
        public string mQuestion  { get; set; }
        public string tQuestion  { get; set; }

        public double sScore { get; set; }
        public double mScore  { get; set; }
        public double tScore  { get; set; }

    }

    public class ExamResultEntity
    {
        public string sid;
        public string name;
        public double sScore;
        public double mScore;
        public double tfScore;
        public double totalScore;

    }

}
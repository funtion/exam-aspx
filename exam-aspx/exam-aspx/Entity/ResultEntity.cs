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
        public float sScore { get; set; }
        public float mScore  { get; set; }
        public float tScore  { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Drawing;

namespace exam_aspx.Entity
{
    public class Question
    {
        public string statement;
        public string ans;
        public ArrayList choices = new ArrayList();
        public string type;
        public Image image;
    }
    public class ExamEntity
    {
        public int time;
        public int ready;
        public int sNumber, tNumber, mNumber;
        public float sScore, tScore, mScore;
        public ArrayList tf, mc, sc;
        public ExamEntity() 
        {
            tf = new ArrayList();
            mc = new ArrayList();
            sc = new ArrayList();
        }
    }
}
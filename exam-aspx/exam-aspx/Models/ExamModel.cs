using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using exam_aspx.Entity;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Core;
using Spire.Doc.Collections;
using Spire.Doc.Converters;
using Spire.Doc.Fields;
using System.Data.Odbc;
namespace exam_aspx.Models
{
    public class ExamModel:BaseModel
    {
        public ExamEntity praseFromDoc(String filePath)
        {
            ExamEntity exam = new ExamEntity();
            Document doc = new Document(filePath);
            Question currentQuestion=null;
            foreach(Section section in doc.Sections)
            {
                foreach(Paragraph paragraph in section.Paragraphs)
                {
                    var text = paragraph.Text.Trim();

                    
                    if(text.StartsWith("##"))//Configration
                    {
                        var list = text.Substring(3, text.Length - 4).Split(':');
                        if(list[0] == "time")
                        {
                            exam.time = int.Parse(list[1]);
                        }
                        else if(list[0] == "TF")
                        {
                            exam.tNumber = int.Parse(list[1]);
                            exam.tScore  = float.Parse(list[2]);
                        }
                        else if (list[0] == "SC")
                        {
                            exam.sNumber = int.Parse(list[1]);
                            exam.sScore  = float.Parse(list[2]);
                        }
                        else if (list[0] == "MC")
                        {
                            exam.mNumber = int.Parse(list[1]);
                            exam.mScore  = float.Parse(list[2]);
                        }
                    }
                    else if(text.StartsWith("$$"))//Start of a question 
                    {
                        var list = text.Substring(3, text.Length - 4).Split(':');
                        string problemType = list[0];
                        string ans = list[2];
                        currentQuestion = new Question();
                        currentQuestion.type = problemType;
                        currentQuestion.ans = ans;
                        
                    }
                    else if(text.Length==0) //End of a question
                    {
                        DocPicture pic = null;
                        foreach (DocumentObject docObject in paragraph.ChildObjects)
                        {
                            if (docObject.DocumentObjectType == DocumentObjectType.Picture)
                            {
                                pic = docObject as DocPicture;
                            }
                        }
                        if(pic != null ) // a line with picture but no text
                        {
                            currentQuestion.image = pic.Image;
                        }
                        else // end of a question
                        {
                            if (currentQuestion != null)
                            {
                                if (currentQuestion.type.Equals("TF"))
                                {
                                    exam.tf.Add(currentQuestion);
                                }
                                else if (currentQuestion.type.Equals("SC"))
                                {
                                    exam.sc.Add(currentQuestion);
                                }
                                else// MC
                                {
                                    exam.mc.Add(currentQuestion);
                                }
                            }
                            currentQuestion = null;
                        }
                        
                    }
                    else 
                    {
                        if( currentQuestion.type=="TF")
                        {
                            
                            foreach (DocumentObject docObject in paragraph.ChildObjects)
                            {
                                
                                if (docObject.DocumentObjectType == DocumentObjectType.TextRange)
                                {
                                    TextRange info = docObject as TextRange;
                                    
                                    currentQuestion.statement += info.Text;
                                }
                                else if(docObject.DocumentObjectType == DocumentObjectType.Picture)
                                {
                                    DocPicture picture = docObject as DocPicture;
                                    
                                    currentQuestion.image = picture.Image;
                                }
                            }
                        }
                        else// SC &&　MC
                        {
                            foreach (DocumentObject docObject in paragraph.ChildObjects)
                            {
                                if (docObject.DocumentObjectType == DocumentObjectType.TextRange)
                                {
                                    var info = (docObject as TextRange).Text;
                                    if (info.StartsWith("A：")) 
                                    {
                                        var choiceList = info.Split('；');
                                        currentQuestion.choices.AddRange(choiceList);
                                    }
                                    else
                                    {
                                        currentQuestion.statement += info;
                                    }
                                    
                                }
                                else if (docObject.DocumentObjectType == DocumentObjectType.Picture)
                                {
                                    DocPicture picture = docObject as DocPicture;
                                    currentQuestion.image = picture.Image;
                                }
                            }
                        }
                    }
                }

            }
            return exam;
        }
        /// <summary>
        ///    插入考试
        /// </summary>
        /// <param name="time">考试时间</param>
        /// <param name="sNum">单选数量</param>
        /// <param name="mNum">多选数量</param>
        /// <param name="tfNum">判断数量</param>
        /// <param name="sScore">单选分数</param>
        /// <param name="mScore">多选分数</param>
        /// <param name="tfScore">判断分数</param>
        /// <param name="ready">时候可见，0为不可见，1为课件，默认为0</param>
        /// <returns>插入成功返回插入的id，-1插入失败，-2获取last_insert_id失败</returns>
        public int addExam(int time, int sNum, int mNum, int tfNum, double sScore, double mScore, double tfScore, int ready = 0)//ready 初始默认为0
        {
            
            OdbcCommand command = new OdbcCommand("insert into examination(time,sNumber,mNumber,tNumber,sQuestion,mQuestion,tQuestion,ready) values (?,?,?,?,?,?,?,?)", connection);
            command.Parameters.Add(new OdbcParameter("time", OdbcType.Int)).Value = time;
            command.Parameters.Add(new OdbcParameter("sNumber", OdbcType.Int)).Value = sNum;
            command.Parameters.Add(new OdbcParameter("mNumber", OdbcType.Int)).Value = mNum;
            command.Parameters.Add(new OdbcParameter("tNumber", OdbcType.Int)).Value = tfNum;
            command.Parameters.Add(new OdbcParameter("sQuestion", OdbcType.Double)).Value = sScore;
            command.Parameters.Add(new OdbcParameter("mQuestion", OdbcType.Double)).Value = mScore;
            command.Parameters.Add(new OdbcParameter("tQuestion", OdbcType.Double)).Value = tfScore;
            command.Parameters.Add(new OdbcParameter("ready", OdbcType.Int)).Value = ready;
            command.Prepare();

            if (command.ExecuteNonQuery() > 0)
            {
                command = new OdbcCommand("select last_insert_id()",connection);
                command.Prepare();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
                return -2;
            }
            return -1;

        }
    }
}
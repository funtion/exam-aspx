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
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;
using exam_aspx.Entity;
using System.Web.Script.Serialization;
using System.Collections;

namespace exam_aspx.Models
{
    public class QuestionModel : BaseModel
    {
        public int addQuesiton(int type,string answer,string options,string picture,string description,int examinationId)
        {
            OdbcCommand command = new OdbcCommand("insert into question(type,answer,options,picture,discription,examinationId) values (?,?,?,?,?,?)",connection);
            command.Parameters.Add(new OdbcParameter("type", OdbcType.Int)).Value = type;
            command.Parameters.Add(new OdbcParameter("anwser", OdbcType.VarChar)).Value = answer;
            command.Parameters.Add(new OdbcParameter("options", OdbcType.VarChar)).Value = options;
            command.Parameters.Add(new OdbcParameter("picture", OdbcType.VarChar)).Value = picture;
            command.Parameters.Add(new OdbcParameter("description", OdbcType.VarChar)).Value = description;
            command.Parameters.Add(new OdbcParameter("examinationId", OdbcType.Int)).Value = examinationId;
            command.Prepare();
            return command.ExecuteNonQuery();
        }
        public string getTypeString(int type)
        {
            switch (type)
            {
                case 0: return "SC";
                case 1: return "MC";
                case 2: return "TF";
                default: return null;
            }
        }
        public int deleteQuestion(int id)
        {
            OdbcCommand command = new OdbcCommand("delete from question where id=?", connection);
            command.Parameters.Add(new OdbcParameter("id", OdbcType.Int)).Value = id;
            command.Prepare();
            return command.ExecuteNonQuery();
        }
        public int deleteQuestionByExamId(int examinationId)
        {
            OdbcCommand command = new OdbcCommand("delete from question where examinationId=?", connection);
            command.Parameters.Add(new OdbcParameter("examinationId", OdbcType.Int)).Value = examinationId;
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public QuestionEntity read(OdbcDataReader reader)
        {
            var res = new QuestionEntity();
            res.ans = reader.GetString(2);


            var choicejson = reader.GetString(3);
            var decoder = new JavaScriptSerializer();
            res.choices = decoder.Deserialize< ArrayList >(choicejson);

            res.imageURL = reader.GetString(4);
            res.statement = reader.GetString(5);
            res.type = getTypeString( reader.GetInt32(1) );

            return res;

        }

        public List<QuestionEntity> getQuestionByExamAndType(int examinationId,int type)
        {
            var cmd = buildCommand("select * from question where examinationId=? and type=?");
            cmd.AddIntParam("examinationId", examinationId);
            cmd.AddIntParam("type", type);
            var reader = cmd.ExecuteReader();
            var res = new List<QuestionEntity>();
            while (reader.Read() )
            {
                res.Add(read(reader));
            }
            return res;
        }

        public List<QuestionEntity> getSCQuestionByExam(int examid)
        {
            return getQuestionByExamAndType(examid, 0);
        }

        public List<QuestionEntity> getMCQuestionByExam(int examid)
        {
            return getQuestionByExamAndType(examid, 1);
        }
        public List<QuestionEntity> getTFQuestionByExam(int examid)
        {
            return getQuestionByExamAndType(examid, 2);
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;
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
    }
}
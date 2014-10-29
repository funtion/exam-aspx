using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;
using exam_aspx.Entity;

namespace exam_aspx.Models
{
    public class ResultModel : BaseModel
    {
        public List<ResultEntity> getResultByStudentId(int studentId)
        {
            List<ResultEntity> resultList = new List<ResultEntity>();
            OdbcCommand command = new OdbcCommand("select * from result where studentId=?", connection);
            command.Parameters.Add(new OdbcParameter("studentId", OdbcType.Int)).Value = studentId;
            command.Prepare();
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ResultEntity result = new ResultEntity();
                result.id = reader.GetInt32(0);
                result.studentId = reader.GetInt32(1);
                result.examinationId = reader.GetInt32(2);
                result.sQuestion = reader.GetString(3);
                result.mQuestion = reader.GetString(4);
                result.tQuestion = reader.GetString(5);
                result.sScore = reader.GetDouble(6);
                result.mScore = reader.GetDouble(7);
                result.tScore = reader.GetDouble(8);
                resultList.Add(result);
            }
            return resultList;
        }


        public List<ResultEntity> getResultByExaminationId(int examinationId)
        {
            List<ResultEntity> resultList = new List<ResultEntity>();
            OdbcCommand command = new OdbcCommand("select * from result where examinationId=?", connection);
            command.Parameters.Add(new OdbcParameter("examinationId", OdbcType.Int)).Value = examinationId;
            command.Prepare();
            OdbcDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ResultEntity result = new ResultEntity();

                result.id = reader.GetInt32(0);
                result.studentId = reader.GetInt32(1);
                result.examinationId = reader.GetInt32(2);
                result.sQuestion = reader.GetString(3);
                result.mQuestion = reader.GetString(4);
                result.tQuestion = reader.GetString(5);
                result.sScore = reader.GetDouble(6);
                result.mScore = reader.GetDouble(7);
                result.tScore = reader.GetDouble(8);
                resultList.Add(result);
            }
            return resultList;

        }
        public ResultEntity getResultById(int id)
        {
            ResultEntity result = null;
            OdbcCommand command = new OdbcCommand("select * from result where id=?", connection);
            command.Parameters.Add(new OdbcParameter("id", OdbcType.Int)).Value = id;
            command.Prepare();
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                result = new ResultEntity();
                result.id = reader.GetInt32(0);
                result.studentId = reader.GetInt32(1);
                result.examinationId = reader.GetInt32(2);
                result.sQuestion = reader.GetString(3);
                result.mQuestion = reader.GetString(4);
                result.tQuestion = reader.GetString(5);
                result.sScore = reader.GetDouble(6);
                result.mScore = reader.GetDouble(7);
                result.tScore = reader.GetDouble(8);
            }
            return result;
        }
        
        public int insertResult(int studentId,int examinationId,string sQuestion,string mQuestion,string tQuestion,double sScore,double mScore,double tScore )
        {
            OdbcCommand command = new OdbcCommand("insert into result(studentId,examinationId,sQuestion,mQuestion,tQuestion,sScore,mScore,tScore,time) values (?,?,?,?,?,?,?,?,now())", connection);
            command.Parameters.Add(new OdbcParameter("studentId", OdbcType.Int)).Value = studentId;
            command.Parameters.Add(new OdbcParameter("examinationId", OdbcType.Int)).Value = examinationId;
            command.Parameters.Add(new OdbcParameter("sQuestion", OdbcType.VarChar)).Value = sQuestion;
            command.Parameters.Add(new OdbcParameter("mQuestion", OdbcType.VarChar)).Value = mQuestion;
            command.Parameters.Add(new OdbcParameter("tQuestion", OdbcType.VarChar)).Value = tQuestion;
            command.Parameters.Add(new OdbcParameter("sScore", OdbcType.Double)).Value = sScore;
            command.Parameters.Add(new OdbcParameter("mScore", OdbcType.Double)).Value = mScore;
            command.Parameters.Add(new OdbcParameter("tScore", OdbcType.Double)).Value = tScore;     
            return command.ExecuteNonQuery();
        }
        public int lastId()
        {
            var cmd = buildCommand("select max(id) from result");
            var reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }
        public int deleteResultById(int id)
        {
            OdbcCommand command = new OdbcCommand("delete from result where id=?", connection);
            command.Parameters.Add(new OdbcParameter("id", OdbcType.Int)).Value = id;
            return command.ExecuteNonQuery();
        }

        public List<ExamResultEntity> getExamResultByExamId(int examId)
        {
            List<ExamResultEntity> list = new List<ExamResultEntity>();
            var cmd = buildCommand("select  student.`name`,student.sid,result.mSCore,result.mSCore,result.tScore from result,student where result.studentId=student.id and examinationId=?");
            cmd.AddIntParam("examinationId", examId);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ExamResultEntity tmp = new ExamResultEntity();
                tmp.name = reader.GetString(0);
                tmp.sid = reader.GetString(1);
                tmp.sScore = reader.GetDouble(2);
                tmp.mScore = reader.GetDouble(3);
                tmp.tfScore = reader.GetDouble(4);
                tmp.totalScore = tmp.mScore + tmp.sScore + tmp.tfScore;
                list.Add(tmp);
            }
            return list;

        }
        public int delResultByExamId(int examId){
            var cmd = buildCommand("delete from result where examinationId=?");
            cmd.AddIntParam("examinationId",examId);
            return cmd.ExecuteNonQuery();

        }
        
    }
}
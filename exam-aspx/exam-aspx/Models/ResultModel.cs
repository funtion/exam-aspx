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
                result.sScore = reader.GetFloat(6);
                result.mScore = reader.GetFloat(7);
                result.tScore = reader.GetFloat(8);
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
                result.sScore = reader.GetFloat(6);
                result.mScore = reader.GetFloat(7);
                result.tScore = reader.GetFloat(8);
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
                result.sScore = reader.GetFloat(6);
                result.mScore = reader.GetFloat(7);
                result.tScore = reader.GetFloat(8);
                
            }
            return result;
        }
        /*
        public int updateResultById(int id)
        {
            OdbcCommand command = new OdbcCommand("update result values (?,?,)")
        }
         * */
    }
}
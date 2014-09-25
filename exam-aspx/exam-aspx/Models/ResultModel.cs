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
        public ResultEntity getResultById(int uid)
        {
            ResultEntity result = null;
            OdbcCommand command = new OdbcCommand("select * from result where id=?", connection);
            command.Parameters.Add(new OdbcParameter("id", OdbcType.Int)).Value = uid;
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
                result.sScore = reader.GetInt32(6);
                result.mScore = reader.GetInt32(7);
                result.tScore = reader.GetInt32(8);
          
            }
            return result;
        }
    }
}
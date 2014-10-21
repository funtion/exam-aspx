using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exam_aspx.Models
{
    public class LimitUserModel:BaseModel 
    {
        public bool isAllowed(string sid)
        {
            var cmd = buildCommand("select * from limituser where sid=?");
            cmd.AddParam("sid", System.Data.Odbc.OdbcType.VarChar, sid);
            return cmd.ExecuteReader().HasRows;
        }
        public int addLimitUser(string sid)
        {
            var cmd = buildCommand("insert into limituser(sid) values(?)");
            cmd.AddVarcharParam("sid", sid);
            return cmd.ExecuteNonQuery();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using exam_aspx.Entity;

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
        public List<LimitUserEntity> getAllLimitUser()
        {
            List<LimitUserEntity> list = new List<LimitUserEntity>();
            var cmd = buildCommand("select * from limituser");
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                LimitUserEntity user = new LimitUserEntity();
                user.id = reader.GetInt32(0);
                user.sid = reader.GetString(1);
                list.Add(user);

            }
            return list;
        }
        public int delLimitUser(int id)
        {
            var cmd = buildCommand("delete from limituser where id=?");
            cmd.AddIntParam("id", id);
            return cmd.ExecuteNonQuery();
        }
    }
}

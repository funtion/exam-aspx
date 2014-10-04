using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using exam_aspx.Entity;


namespace exam_aspx.Models
{
    public class AnnouncementModel:BaseModel
    {
        public AnnouncementEntity[] getAnnouncements(int limit=5)
        {
            var cmd = buildCommand("select * from announcement where display=1 order by time desc");
            var reader = cmd.ExecuteReader();
            var result = new List<AnnouncementEntity>();
            while(reader.Read())
            {
                AnnouncementEntity tmp = new AnnouncementEntity();
                tmp.id      = reader.GetInt32(0);
                tmp.content = reader.GetString(1);
                tmp.title   = reader.GetString(2);
                tmp.display = reader.GetInt32(3);
                tmp.time = reader.GetDateTime(4);
                result.Add(tmp);
            }
            return result.ToArray();
        }

        public int addAnnouncement(string title,string content,int display=1)
        {
            var cmd = buildCommand("insert into announcement(title,content,display,time) values(?,?,?,now())");
            cmd.AddParam("title", System.Data.Odbc.OdbcType.VarChar, title);
            cmd.AddParam("content", System.Data.Odbc.OdbcType.VarChar, content);
            cmd.AddParam("display", System.Data.Odbc.OdbcType.Int, display);
            return cmd.ExecuteNonQuery();
        }

        public int delAnnouncement(int id)
        {
            var cmd = buildCommand("delete from announcement where id = ?");
            cmd.AddParam("id", System.Data.Odbc.OdbcType.Int,id);
            return cmd.ExecuteNonQuery();
        }
        public AnnouncementEntity getAnnouncementByID(int id)
        {
            var cmd = buildCommand("select * from announcement where id=?");
            cmd.AddIntParam("id", id);
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                return null;
            reader.Read();
            AnnouncementEntity tmp = new AnnouncementEntity();
            tmp.id = reader.GetInt32(0);
            tmp.content = reader.GetString(1);
            tmp.title = reader.GetString(2);
            tmp.display = reader.GetInt32(3);
            tmp.time = reader.GetDateTime(4);
            return tmp;
        }
    }
}
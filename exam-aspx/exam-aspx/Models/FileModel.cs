using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using exam_aspx.Entity;
using System.Data.Odbc;


namespace exam_aspx.Models
{
    public class FileModel:BaseModel
    {
        public FileEntity[] getFiles()
        {
            var cmd = buildCommand("select * from file order by time desc");
            var result = new List<FileEntity>();
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                FileEntity tmp = new FileEntity();
                tmp.name = reader.GetString(1);
                tmp.path = reader.GetString(2);
                tmp.time = reader.GetDateTime(3);
                tmp.size = reader.GetInt32(4);
                result.Add(tmp);
            }
            return result.ToArray();
        }

        public int addFile(string name,string path,int size)
        {
            var cmd = buildCommand("insert into file(name,path,size,time) values(?,?,?,now())");
            cmd.AddVarcharParam("name", name);
            cmd.AddVarcharParam("path", path);
            cmd.AddIntParam("size", size);
            return cmd.ExecuteNonQuery();
        }

        public int deleFile(int id)
        {
            var cmd = buildCommand("delete from file where id-?");
            cmd.AddIntParam("id", id);
            return cmd.ExecuteNonQuery();
        }
    }
}
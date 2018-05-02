using SQLGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class Datarecord
    {

        /// <summary>
        /// 创建表语句的生成
        /// </summary>
        public void create()
        {
            var str = File.ReadAllText("json.txt");
            var readSps = Newtonsoft.Json.JsonConvert.DeserializeObjectAsync<List<Workaction>>(str).Result;
            //string sql = "";
            //string ss = "(";
            int index = 0;
            //遍历list
            StringBuilder strsql = new StringBuilder();
            StringBuilder sssql = new StringBuilder();
           
            //strsql.Append();
            foreach (var item in readSps)
            {
                 strsql = strsql.Append( "create table ");

                sssql.Clear();
                sssql.Append("(");
                foreach (var items in item.DataSheets)
                {

                    //循环list在首字段添加逗号最后截取ss从第二位到最后
                    sssql =sssql.Append( "\r\n").Append(items.Location).Append("   ").Append(items.Dataformat).Append( ",");
                    if (items.IsKey == true)
                    {
                        //如果iskey是true就为这个字段添加主键
                        sssql = sssql.Append (" primary key ,");
                    }


                }

                //组合sql语句因为截取是从第二位开始所以要加（
                strsql =strsql.Append( "").Append(item.Name).Append("\r\n").Append(sssql.Remove(sssql.Length-1,1)).Append( "\r\n)\r\n");

            }

            Console.WriteLine(strsql);
            File.WriteAllText("Sqlspcreate.txt", strsql.ToString());


        }

        public void Insert()
        {
            var strj = File.ReadAllText("json.txt");
            var readSp = Newtonsoft.Json.JsonConvert.DeserializeObjectAsync<List<Workaction>>(strj).Result;
            StringBuilder insql = new StringBuilder(); ;
            StringBuilder sql = new StringBuilder();
            StringBuilder values = new StringBuilder();

            foreach (var item in readSp)
            {
                insql =insql.Append( "  if (object_id('sp_ins").Append(item.Name).Append("', 'P') is not null)  drop proc  sp_ins").Append(item.Name).Append(" \r\ngo \r\ncreate proc  sp_ins").Append( item.Name ).Append( "");
                values.Clear();
                values =values.Append( "insert into ").Append( item.Name) .Append (" values(");
                sql.Clear();
                sql = sql.Append("(");
                
                foreach (var items in item.DataSheets)
                {
                    sql =sql.Append( " @" ).Append( items.Location) .Append( "  ").Append ( items.Dataformat ).Append( ", ");
                    values = values.Append ( "@" ).Append( items.Location) .Append( ",");
                }
                insql = insql.Append(sql.Remove(sql.Length-2,1)) .Append( ")\r\n as\r\n ").Append ( values.Remove(values.Length-1,1)) .Append( ")\r\n go\r\n ");


            }

            Console.WriteLine(insql);
            File.WriteAllText("Sqlspinsert.txt", insql.ToString());
        }
        /// <summary>
        ///生成修改的存储过程 
        /// </summary>
        public void Update()
        {
            var strj = File.ReadAllText("json.txt");
            var readSp = Newtonsoft.Json.JsonConvert.DeserializeObjectAsync<List<Workaction>>(strj).Result;
           

            string upsql = "";
            string sql = "(";
            string values = "";


            foreach (var item in readSp)
            {
                upsql += "  if (object_id('sp_upd" + item.Name + "', 'P') is not null)  drop proc  sp_upd" + item.Name + " \r\ngo\r\n create proc  sp_upd" + item.Name + "";
                values = "update  " + item.Name + " set ";
                string iskey = "";
                sql = "(";

                foreach (var items in item.DataSheets)
                {
                    sql += " @" + items.Location + "  " + items.Dataformat + ", ";
                    if (items.IsKey == true)
                    {
                        iskey += "where  " + items.Location + " = @" + items.Location + "";

                    }
                    else
                    {
                       
                        values = values + "  " + items.Location + "  = @" + items.Location + " ,";


                    }
                    
                    
                }
                upsql += sql.Substring(0, sql.Length - 2) + ") \r\nas\r\n " + values.Substring(0, values.Length - 1) +iskey+ " \r\ngo\r\n ";


            }

            Console.WriteLine(upsql);
            File.WriteAllText("Sqlspupdate.txt", upsql);



        }

        public void select()
        {

            var strj = File.ReadAllText("json.txt");
            var readSp = Newtonsoft.Json.JsonConvert.DeserializeObjectAsync<List<Workaction>>(strj).Result;


            StringBuilder selsql = new StringBuilder();
            StringBuilder sql = new  StringBuilder();
            StringBuilder values = new StringBuilder();


            foreach (var item in readSp)
            {
                selsql =selsql.Append( "  if (object_id('sp_sel") .Append( item.Name ).Append( "', 'P') is not null)  drop proc  sp_sel").Append( item.Name) .Append( " \r\ngo\r\n create proc  sp_sel") .Append( item.Name) .Append( "");
                
                string iskey = "";
                sql.Clear();
                sql = sql.Append("(");
                values.Clear();
                values = values.Append ( "select * from ").Append ( item.Name ).Append( " ");
                foreach (var items in item.DataSheets)
                {


                    
                    if (items.IsKey == true)
                    {
                        sql =sql.Append( " @") .Append( items.Location) .Append( "  ") .Append( items.Dataformat) .Append( ", ");
                        iskey += "where  " + items.Location + " = @" + items.Location + "";

                    }
                  
            


                }
                selsql =selsql.Append(sql.Remove(sql.Length-2,1)).Append (")").Append( " \r\nas\r\n ") .Append( values) .Append(iskey) .Append( " \r\ngo\r\n ");


            }

            Console.WriteLine(selsql);
            File.WriteAllText("Sqlspselect.txt", selsql.ToString());


        }

    }
    
}

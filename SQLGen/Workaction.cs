using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SQLGen
{
   public class Show
    {

        static void Print(Workaction sp)
        {
            Console.WriteLine(sp.Name);
            Console.WriteLine(sp.Discription);
            Console.WriteLine(sp.Ip);


            foreach (var item in sp.DataSheets)
            {
                Console.WriteLine(item.Contents);
                Console.WriteLine(item.Location);
                Console.WriteLine(item.Dataformat);
                Console.WriteLine(item.Lenth);
                Console.WriteLine(item.Databool);
                Console.WriteLine(item.IsKey);

            }
        }

        /// <summary>
        /// 把A30数据表写入文本然后在转json的方法
        /// </summary>
        public void Writejson()
        {



            List<Workaction> sps = new List<Workaction>();
            List<DataSheet> DataSheets = new List<DataSheet>();
            Workaction sp = new Workaction()
            {
                Name = "A30",//工位名称
                Ip = "192.168.1.101",//Ip地址
                Discription = "马达锁附",//功能
                DataSheets = DataSheets,
            };


            DataSheets.Add(new DataSheet()
            {

                Contents = "扫码",//A30数据内容
                Location = "D110",//A30数据位置
                Databool = "M581",//A30状态
                Dataformat = "varchar(20)",//A30数据格式
                IsKey = true,//是否主键
                Lenth = 20,//A30数据长度

            });

            DataSheets.Add(new DataSheet()
            {

                Contents = "1#螺丝扭矩",//A30数据内容
                Location = "D130",//A30数据位置
                Databool = "M525",//A30状态
                Dataformat = "int",//A30数据格式
                IsKey = false,//是否主键
                Lenth = 2,//A30数据长度

            });
            DataSheets.Add(new DataSheet()
            {

                Contents = "2#螺丝扭矩",//A30数据内容
                Location = "D132",//A30数据位置
                Databool = "M526",//A30状态
                Dataformat = "int",//A30数据格式
                IsKey = false,//是否主键
                Lenth = 2,//A30数据长度

            });
            DataSheets.Add(new DataSheet()
            {

                Contents = "3#螺丝扭矩",//A30数据内容
                Location = "D134",//A30数据位置
                Databool = "M527",//A30状态
                Dataformat = "int",//A30数据格式
                IsKey = false,//是否主键
                Lenth = 2,//A30数据长度

            });
            DataSheets.Add(new DataSheet()
            {

                Contents = "4#螺丝扭矩",//A30数据内容
                Location = "D136",//A30数据位置
                Databool = "M528",//A30状态
                Dataformat = "int",//A30数据格式
                IsKey = false,//是否主键
                Lenth = 2,//A30数据长度

            });
            DataSheets.Add(new DataSheet()
            {

                Contents = "1#螺丝角度",//A30数据内容
                Location = "D138",//A30数据位置
                Databool = "",//A30状态
                IsKey = false,//是否主键
                Dataformat = "int",//A30数据格式
                Lenth = 2,//A30数据长度

            });
            DataSheets.Add(new DataSheet()
            {

                Contents = "2#螺丝角度",//A30数据内容
                Location = "D140",//A30数据位置
                Databool = "",//A30状态
                IsKey = false,//是否主键
                Dataformat = "int",//A30数据格式
                Lenth = 2,//A30数据长度

            });
            DataSheets.Add(new DataSheet()
            {

                Contents = "3#螺丝角度",//A30数据内容
                Location = "D142",//A30数据位置
                Databool = "",//A30状态
                IsKey = false,//是否主键
                Dataformat = "int",//A30数据格式
                Lenth = 2,//A30数据长度

            });
            DataSheets.Add(new DataSheet()
            {

                Contents = "4#螺丝角度",//A30数据内容
                Location = "D144",//A30数据位置
                Databool = "",//A30状态
                IsKey = false,//是否主键
                Dataformat = "int",//A30数据格式
                Lenth = 2,//A30数据长度

            });


            sps.Add(sp);


            File.WriteAllText("json.txt", Newtonsoft.Json.JsonConvert.SerializeObjectAsync(sps).Result);
            Print(sp);

        }
    }

  
    public   class Workaction
    {
        public string Name { get; set; }
        public string Discription { get; set; }
        public string Ip { get; set; }

        public List<DataSheet> DataSheets { get; set; }//A30数据类
    }
    public class DataSheet
    {
        public string Contents { get; set; }//数据内容
        public string Location { get; set; }//数据位置
        public int Lenth { get; set; }//数据长度
        public string Dataformat { get; set; }//数据格式
        public string Databool { get; set; }//状态
        public bool IsKey { get; set; }

    }

}

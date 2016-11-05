// 由Msdn5 玄机宝盒 生成Json对象
// http://www.msdn5.com

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Msdn5Json;

namespace Msdn5Json
{

    public class Msdn5JsonObject
    {
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);//返回对象的Json字符串
        }
        public Msdn5JsonObject JsonTo(string Json)
        {
           return (Msdn5JsonObject)Newtonsoft.Json.JsonConvert.DeserializeObject(Json);//Json字符串转对象
        }
        public List<Msdn5JsonObject> JsonToList(string Json)
        {
            try
            {
                
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Msdn5JsonObject>>(Json);//字符串转Json对象集合
            }
            catch (Exception)
            {

                return null;
            }
          
        }

        public Data_ data;
    }

}

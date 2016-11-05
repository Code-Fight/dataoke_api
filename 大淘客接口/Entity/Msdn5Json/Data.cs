// 由Msdn5 玄机宝盒 生成Json对象
// http://www.msdn5.com

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Msdn5Json;

namespace Msdn5Json
{

    public class Data_
    {
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);//返回对象的Json字符串
        }
        public Data_ JsonTo(string Json)
        {
           return (Data_)Newtonsoft.Json.JsonConvert.DeserializeObject(Json);//Json字符串转对象
        }
        public List<Data_> JsonToList(string Json)
        {
            try
            {
                
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Data_>>(Json);//字符串转Json对象集合
            }
            catch (Exception)
            {

                return null;
            }
          
        }

        public string api_type;
        public string update_time;
        public int total_num;
        public string api_content;
        public IList<Result_> result;
    }

}

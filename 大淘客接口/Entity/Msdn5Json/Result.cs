// 由Msdn5 玄机宝盒 生成Json对象
// http://www.msdn5.com

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Msdn5Json;

namespace Msdn5Json
{

    public class Result_
    {
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);//返回对象的Json字符串
        }
        public Result_ JsonTo(string Json)
        {
           return (Result_)Newtonsoft.Json.JsonConvert.DeserializeObject(Json);//Json字符串转对象
        }
        public List<Result_> JsonToList(string Json)
        {
            try
            {
                
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Result_>>(Json);//字符串转Json对象集合
            }
            catch (Exception)
            {

                return null;
            }
          
        }

        public string GoodsID;
        public string Title;
        public string D_title;
        public string Pic;
        public string Cid;
        public string Org_Price;
        public double Price;
        public string IsTmall;
        public string Sales_num;
        public string Dsr;
        public string SellerID;
        public string Commission_jihua;
        public string Commission_queqiao;
        public string Jihua_link;
        public string Introduce;
        public string Quan_id;
        public string Quan_price;
        public string Quan_time;
        public string Quan_surplus;
        public string Quan_receive;
        public string Quan_condition;
        public string Quan_link;
        public string Quan_m_link;
        public string ali_click;
    }

}

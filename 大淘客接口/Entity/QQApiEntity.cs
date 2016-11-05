using System.Collections.Generic;
using System.Drawing;
using FTools.HTTP;

public class ResultItem
{
    /// <summary>
    /// 
    /// </summary>
    public string GoodsID { get; set; }

    /// <summary>
    /// 巧迪尚惠睫毛膏 晶钻摩翘睫毛膏纤长浓密券翘不晕染防水不易脱妆
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 巧迪尚惠睫毛膏
    /// </summary>
    public string D_title { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Pic { get; set; }


    public Image _Image { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Cid { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Org_Price { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Price { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string IsTmall { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Sales_num { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Dsr { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string SellerID { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Commission_jihua { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Commission_queqiao { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Jihua_link { get; set; }

    /// <summary>
    /// 晶钻摩翘睫毛膏 纤长浓密  券翘不晕染 防水不易脱妆
    /// </summary>
    public string Introduce { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Quan_id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Quan_price { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Quan_time { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Quan_surplus { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Quan_receive { get; set; }

    /// <summary>
    /// 单笔满48元可用，每人限领1 张
    /// </summary>
    public string Quan_condition { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Quan_link { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Quan_m_link { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ali_click { get; set; }

    
}



public class Data
{
    /// <summary>
    /// 领券优惠v1.4
    /// </summary>
    public string api_type { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string update_time { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int total_num { get; set; }

    /// <summary>
    /// QQ群发专用API数据接口
    /// </summary>
    public string api_content { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<ResultItem> result { get; set; }

}



public class QQApiEntity
{
    /// <summary>
    /// 
    /// </summary>
    public Data data { get; set; }

}


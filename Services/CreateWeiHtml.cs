using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Newtonsoft.Json;

namespace Services
{
    public class CreateWeiHtml
    {
        /// <summary>
        /// 生成网页
        /// </summary>
        /// <param name="resultItems">选中的宝贝</param>
        /// <param name="weiHtmlTemplate">微页模板</param>
        /// <param name="weiHtmlContentTemplate">微页内容模板</param>
        /// <param name="urlTemplate">2合一转换模板</param>
        /// <param name="pid">pid</param>
        /// <param name="weiHtmltitle">微页标题</param>
        /// <returns></returns>
        public static string Get(List<ResultItem> resultItems, string weiHtmlTemplate, string weiHtmlContentTemplate, string urlTemplate, string pid, string weiHtmltitle = "帮帮熊淘宝内部优惠券推荐")
        {
            string _content = string.Empty;
            foreach (ResultItem r in resultItems)
            {
                string url_2to1 = Tk2To1.GetUrl(urlTemplate, r.Quan_id, r.GoodsID, pid);
                if (url_2to1.Length<=0)
                {
                    throw new Exception("2和1链接转换失败");
                }
                string tpwd =JsonConvert.DeserializeObject<TpwdEntity>(TkTpwd.ConvertTpwd(r.Title, r.Pic, url_2to1)).wireless_share_tpwd_create_response.model;
                if (tpwd.Length<=0)
                {
                    throw new Exception("淘口令转换失败");
                }
                TkmShortEntity link = (JsonConvert.DeserializeObject<TkmShortEntity>(TkmShortUrl.GetShort(url_2to1)));
               if (link == null || link.status.Length<=0 )
               {
                   throw new Exception( "短链接转换错误");
               }
                //赋值短连接
               url_2to1 = link.link;
                string contentTemplate = weiHtmlContentTemplate;

                _content +=contentTemplate.Replace("{$二合一链接$}", url_2to1)
                    .Replace("{$主图$}", r.Pic)
                    .Replace("{$券后价$}", r.Price)
                    .Replace("{$标题$}", r.D_title)
                    .Replace("{$文案$}", r.Introduce)
                    .Replace("{$淘口令$}", tpwd);
            }
            return weiHtmlTemplate.Replace("{$content$}", _content).Replace("{$title$}", weiHtmltitle);
        }
    }
}

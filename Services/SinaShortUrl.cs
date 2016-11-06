using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTools.Encode;
using FTools.HTTP;

namespace Services
{
    public class SinaShortUrl
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="longurl">长链接</param>
        public static string GetShort(string longurl)
        {
            /*
            Cookie常用的几种处理方式：
            new XJHTTP().UpdateCookie("旧Cookie", "请求后的Cookie");//合并Cookie，将cookie2与cookie1合并更新 返回字符串类型Cookie
            new XJHTTP().StringToCookie("网站Domain", "字符串Cookie内容");//将文字Cookie转换为CookieContainer 对象
            new XJHTTP().CookieTostring("CookieContainer 对象");//将 CookieContainer 对象转换为字符串类型
            new XJHTTP().GetAllCookie("CookieContainer 对象");//得到CookieContainer中的所有Cookie对象集合,返回List<Cookie>
            new XJHTTP().GetCookieByWininet("网站Url");//从Wininet 中获取字符串Cookie 可获取IE与Webbrowser控件中的Cookie
            new XJHTTP().GetAllCookieByHttpItems("请求设置对象");//从请求设置对象中获取Cookie集合,返回List<Cookie>
            new XJHTTP().ClearCookie("需要处理的字符串Cookie");//清理string类型Cookie.剔除无用项返回结果为null时遇见错误.
            new XJHTTP().SetIeCookie("设置Cookie的URL", "需要设置的Cookie");//可设置IE/Webbrowser控件Cookie
            new XJHTTP().CleanAll();//清除IE/Webbrowser所有内容 (注意,调用本方法需要管理员权限运行) CleanHistory();清空历史记录  CleanCookie(); 清空Cookie CleanTempFiles(); 清空临时文件
            */
            string url = "http://api.t.sina.com.cn/short_url/shorten.json?source=4257120060&url_long=" +Encode.UrlEncode(longurl);//请求地址
            string res = string.Empty;//请求结果,请求类型不是图片时有效
            System.Net.CookieContainer cc = new System.Net.CookieContainer();//自动处理Cookie对象
            HttpHelpers helper = new HttpHelpers();//发起请求对象
            HttpItems items = new HttpItems();//请求设置对象
            HttpResults hr = new HttpResults();//请求结果
            items.URL = url;//设置请求地址
            items.Container = cc;//自动处理Cookie时,每次提交时对cc赋值即可
            hr = helper.GetHtml(items);//发起请求并得到结果
            res = hr.Html;//得到请求结果
            return res;
        }

    }


}

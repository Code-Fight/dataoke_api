using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Top.Api.Request;

namespace Services
{
    public class TkTpwd
    {
        public static string ConvertTpwd(string tips,string pic,string url)
        {
            WirelessShareTpwdCreateRequest.IsvTpwdInfoDomain obj1 = new WirelessShareTpwdCreateRequest.IsvTpwdInfoDomain();
            WirelessShareTpwdCreateRequest req = new WirelessShareTpwdCreateRequest();
            obj1.Ext = "{\"xx\":\"xx\"}";
            obj1.Logo = pic;
            obj1.Text = tips;
            obj1.Url = url;
            obj1.UserId = 24234234234;
            req.TpwdParam_ = obj1;
            string ret = Util.Post("http://gw.api.taobao.com/router/rest",
                "23493845", 
                "bd0af6f3badd3721152b139910bb5124",
                "taobao.wireless.share.tpwd.create", 
                "", 
                new Dictionary<string, string>() { { "tpwd_param", req.TpwdParam } });
            return ret.Contains("wireless_share_tpwd_create_response") ? ret : String.Empty;
        }
    }


    class Util
    {
        /// <summary>
        ///     给TOP请求签名 API v2.0
        /// </summary>
        /// <param name="parameters">所有字符型的TOP请求参数</param>
        /// <param name="secret">签名密钥</param>
        /// <returns>签名</returns>
        protected static string CreateSign(IDictionary<string, string> parameters, string secret)
        {
            parameters.Remove("sign");


            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);

            var dem = sortedParams.GetEnumerator();


            var query = new StringBuilder(secret);

            while (dem.MoveNext())
            {
                var key = dem.Current.Key;

                var value = dem.Current.Value;

                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                    query.Append(key).Append(value);
            }

            query.Append(secret);


            var md5 = MD5.Create();

            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));


            var result = new StringBuilder();

            for (var i = 0; i < bytes.Length; i++)
            {
                var hex = bytes[i].ToString("X");

                if (hex.Length == 1)
                    result.Append("0");

                result.Append(hex);
            }


            return result.ToString();
        }


        /// <summary>
        ///     组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        protected static string PostData(IDictionary<string, string> parameters)
        {
            var postData = new StringBuilder();

            var hasParam = false;


            var dem = parameters.GetEnumerator();

            while (dem.MoveNext())
            {
                var name = dem.Current.Key;

                var value = dem.Current.Value;

                // 忽略参数名或参数值为空的参数

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                {
                    if (hasParam)
                        postData.Append("&");


                    postData.Append(name);

                    postData.Append("=");

                    postData.Append(Uri.EscapeDataString(value));

                    hasParam = true;
                }
            }


            return postData.ToString();
        }


        /// <summary>
        ///     TOP API POST 请求
        /// </summary>
        /// <param name="url">请求容器URL</param>
        /// <param name="appkey">AppKey</param>
        /// <param name="appSecret">AppSecret</param>
        /// <param name="method">API接口方法名</param>
        /// <param name="session">调用私有的sessionkey</param>
        /// <param name="param">请求参数</param>
        /// <returns>返回字符串</returns>
        public static string Post(string url, string appkey, string appSecret, string method, string session,
            IDictionary<string, string> param)
        {
            #region -----API系统参数----

            param.Add("app_key", appkey);

            param.Add("method", method);

            param.Add("session", session);

            param.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            param.Add("format", "json");

            param.Add("v", "2.0");

            param.Add("sign_method", "md5");

            param.Add("sign", CreateSign(param, appSecret));

            #endregion

            var result = string.Empty;

            #region ---- 完成 HTTP POST 请求----

            var req = (HttpWebRequest)WebRequest.Create(url);

            req.Method = "POST";

            req.KeepAlive = true;

            req.Timeout = 300000;

            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            var postData = Encoding.UTF8.GetBytes(PostData(param));

            var reqStream = req.GetRequestStream();

            reqStream.Write(postData, 0, postData.Length);

            reqStream.Close();

            var rsp = (HttpWebResponse)req.GetResponse();

            var encoding = Encoding.GetEncoding(rsp.CharacterSet);

            Stream stream = null;

            StreamReader reader = null;

            stream = rsp.GetResponseStream();

            reader = new StreamReader(stream, encoding);

            result = reader.ReadToEnd();

            if (reader != null) reader.Close();

            if (stream != null) stream.Close();

            if (rsp != null) rsp.Close();

            #endregion

            return Regex.Replace(result, @"[\x00-\x08\x0b-\x0c\x0e-\x1f]", "");
        }
    }
}

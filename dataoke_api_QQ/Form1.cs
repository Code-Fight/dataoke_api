using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraExport.Helpers;
using FTools.HTTP;
using Newtonsoft.Json;

namespace dataoke_api_QQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string _ = Environment.NewLine;
        private void button1_Click(object sender, EventArgs e)
        {
            SetData(Get());
        }

        private void DealToWaibu(List<ResultItem> _resultItems)
        {

            // Root d = JsonConvert.DeserializeAnonymousType<Root>(result, new QQApiEntity());
            StringBuilder str=new StringBuilder();
            if (_resultItems != null)
            {
                str.Append(DateTime.Now.ToString("yyyy-M-d HH:mm") + " 新一波福利，手慢无！" + _ + "别说我没告诉你!" + _);
                foreach (ResultItem r in _resultItems)
                {
                    str.Append("--------------------------------------" + _);
                    str.Append((r.IsTmall == "1" ? "【天猫】" : "") + r.D_title + "【券后：" + r.Price + "】" + _);
                    str.Append(r.Quan_price + "元券：" + r.Quan_m_link + _);
                    str.Append("下单：" + r.ali_click + _);
                    str.Append(r.Introduce + _);
                }
                str.Append("--------------------------------------" + _);
                str.Append("实时更新群：278973739 " + _);
            }
            Clipboard.SetDataObject(str.ToString());
        }


        private void SetData(string result)
        {
            QQApiEntity d = JsonConvert.DeserializeObject<QQApiEntity>(result);
            foreach (ResultItem item in d.data.result)
            {
                new Thread(delegate() { item._Image = GetImage(item.Pic); }) {IsBackground = true}.Start();
            }
            gridControl1.DataSource = d.data.result;
        }

        private string Get()
        {
            string url = "http://api.dataoke.com/index.php?r=goodsLink/qq&type=qq_quan&appkey=soa3o485ec&v=2";//请求地址
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

        public Image GetImage(string url)
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
            //string url = "http://img.ithome.com/images/v2.1/noavatar.png";//请求地址
                    string res = string.Empty;//请求结果,请求类型不是图片时有效
                    HttpHelpers helper = new HttpHelpers();//发起请求对象
                    HttpItems items = new HttpItems();//请求设置对象
                    HttpResults hr = new HttpResults();//请求结果
                    items.URL = url;//设置请求地址
                    items.ResultType = ResultType.Byte;//设置请求返回值类型为Byte
                    return helper.GetImg(helper.GetHtml(items));//picImage图像控件Name
            //return null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DealToWaibu(GetSelectRow());
            MessageBox.Show("生成完成");
        }

        private List<ResultItem> GetSelectRow()
        {
            var selectRows = gridView1.GetSelectedRows().ToList();
            if (selectRows.Count <= 0)
                return null;
            //dt = gridView1.GetDataRow(0).Table;
            return selectRows.Select(i => ((List<ResultItem>) gridView1.DataSource)[i]).ToList();
        }
    }
}

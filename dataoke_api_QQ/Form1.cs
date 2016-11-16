using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraExport.Helpers;
using FTools.Encode;
using FTools.HTTP;
using FTools.ImgTool;
using Newtonsoft.Json;
using Services;

namespace dataoke_api_QQ
{
    public partial class Form1 : Form
    {

        private NetDimension.OpenAuth.Sina.SinaWeiboClient openAuth;
        private Sina _sina = null;
        public Form1(NetDimension.OpenAuth.Sina.SinaWeiboClient client)
        {
            InitializeComponent();
            openAuth = client;
        }

        private string _ = Environment.NewLine;
        private void button1_Click(object sender, EventArgs e)
        {
            SetData(Get());
        }

        private void DealToWaibu(List<ResultItem> _resultItems)
        {

            // Root d = JsonConvert.DeserializeAnonymousType<Root>(result, new QQApiEntity());
            StringBuilder str = new StringBuilder();
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
                new Thread(delegate() { item._Image = GetImage(item.Pic); }) { IsBackground = true }.Start();
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
            return selectRows.Select(i => ((List<ResultItem>)gridView1.DataSource)[i]).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<ResultItem> r = GetSelectRow();
            if (r == null || r.Count <= 0)
            {
                MessageBox.Show("请先选择产品");
                return;
            }
            button3.Enabled = false;
            new Thread(delegate()
            {
                try
                {
                    string html = CreateWeiHtml.Get(r, dataoke_api.weiHtml, dataoke_api.weiHtmlContent, dataoke_api.quan_template,
                  "mm_27864031_11496121_48984395");
                    WriteHtml(html);
                    MessageBox.Show("生成网页完成!");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                button3.Invoke(new Action(delegate
                {
                    button3.Enabled = true;
                }));
            }) { IsBackground = true }.Start();
        }

        public void WriteHtml(string html)
        {
            string path = System.Windows.Forms.Application.StartupPath + "/html";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileStream fs = new FileStream(path + "/tk_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".html", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(html);
            sw.Close();
            fs.Close();
        }

        public void ToWeiBo(ResultItem item)
        {
            if (_sina == null)
            {
                _sina = new Sina(openAuth);
                _sina.UpdateUIEvent += _sina_UpdateUIEvent;
            }
            _sina.PostMsg(new FileInfo(System.Windows.Forms.Application.StartupPath + "/1.png"),
                string.Format(item.Title + Encode.UrlDecode("%0A") +
                              "现价：{0} 【用券价：{1}】" + Encode.UrlDecode("%0A") +
                              "{2}" + Encode.UrlDecode("%0A") +
                              "优惠券地址：{3}" + Encode.UrlDecode("%0A") +
                              "购买地址：{4}" + Encode.UrlDecode("%0A") + txt_sina_ht.Text, item.Org_Price, item.Price, item.Introduce, item.Quan_link, item.ali_click));
        }

        private void _sina_UpdateUIEvent(object a)
        {
            UpdateUi(a.ToString());
        }

        /// <summary>
        /// 发布微博
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            List<ResultItem> ritem = GetSelectRow();
            if (ritem == null || ritem.Count <= 0)
            {
                MessageBox.Show("请先选择产品");
                return;
            }

            //              File.Delete(System.Windows.Forms.Application.StartupPath + "/1.png");
            //
            //                r[0]._Image.Save(System.Windows.Forms.Application.StartupPath+"/1.png", System.Drawing.Imaging.ImageFormat.Png);


            new Thread(delegate()
            {
                foreach (ResultItem r in ritem)
                {
                    Bitmap img = new Bitmap(r._Image);
                    img.Save(System.Windows.Forms.Application.StartupPath + "/1.png",
                        System.Drawing.Imaging.ImageFormat.Png);
                    ImageHelper.CompressJpeg(System.Windows.Forms.Application.StartupPath + "/1.png", img.Width - 1,
                        (long)50);
                    ToWeiBo(r);
                    Thread.Sleep(60 * 1000 * 4);
                }
            }) { IsBackground = true }.Start();



        }


        public void UpdateUi(string msg)
        {
            richTextBox1.Invoke(new Action(delegate
            {
                richTextBox1.Text += string.Format("{0} {1}" + _, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg);
            }));
        }

    }
}

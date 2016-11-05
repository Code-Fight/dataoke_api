using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTools.Encode;
using FTools.HTTP;
using KASX.Comm;
using Msdn5Json;
using Newtonsoft.Json;

namespace 大淘客接口
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string _ = "\n";
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Invoke(new Action(delegate
            {
                richTextBox1.Text = DealToWaibu(Get());
            }));
        }

        private string DealToNeibu(string result)
        {
            StringBuilder str = new StringBuilder();
            Data_ d= new Data_().JsonTo(result);
            if (d.result != null)
            {
                str.Append(DateTime.Now.ToString("yyyy-M-d HH:mm") + " 新一波福利，手慢无！" + _ + "别说我没告诉你!" + _);
                foreach (Result_ r in d.result)
                {
                    str.Append("--------------------------------------" + _);
                    str.Append((r.IsTmall == "1" ? "【天猫】 " : "") + r.D_title + " 【券后价：" + r.Price + "】" + _);
                    str.Append(r.Quan_price + "元券：" + r.Quan_m_link + _);
                    str.Append("下单：" + r.ali_click + _);
                    
                }
                str.Append("--------------------------------------" + _);
                str.Append("实时更新群：278973739 " + _);
            }
            return str.ToString();
        }
        private string DealToWaibu(string result)
        {

           // Root d = JsonConvert.DeserializeAnonymousType<Root>(result, new QQApiEntity());
            StringBuilder str = new StringBuilder();
            //result=result.Replace("\\","");
          //  result = Encode.UnicodeDe(result);
            QQApiEntity d = JsonConvert.DeserializeObject<QQApiEntity>(result);
            if (d.data.result!=null)
            {
                str.Append(DateTime.Now.ToString("yyyy-M-d HH:mm") + " 新一波福利，手慢无！" + _ + "别说我没告诉你!" + _);
                foreach (ResultItem r in d.data.result)
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
            return str.ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Invoke(new Action(delegate
            {
                richTextBox1.Text = DealToNeibu(Get());
            }));
        }
    }
}

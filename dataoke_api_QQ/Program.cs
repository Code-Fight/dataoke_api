using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NetDimension.OpenAuth.Sina;
using NetDimension.OpenAuth.Winform;

namespace dataoke_api_QQ
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //请自行修改appKey，appSecret和回调地址。winform的回调地址可以是一个随便可以访问的地址，貌似不可以访问的地址也是可以的，只要URL中带着Code就行
            var client = new SinaWeiboClient("4257120060", "ad556a1a628e118e14ff19f00f1266c5", "http://www.zfcode.com");

            //NetDimension.OpenAuth.Winform封装的一个登录窗口，主要远离就是个WebBrowser控件，然后在该控件的导航事件里面监测Url是不是带有Code，如果有就调用GetAccessTokenByCode
            var authForm = client.GetAuthenticationForm();

            authForm.StartPosition = FormStartPosition.CenterScreen;
            //authForm.Icon = Properties.Resources.icon_form;

            if (authForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Form1(client));
            }
           // Application.Run(new Form1());
        }
    }
}

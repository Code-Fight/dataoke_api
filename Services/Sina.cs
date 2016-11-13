using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Services
{

    public class Sina
    {
        public delegate void UpdateUIDelegate(object a);

        public event UpdateUIDelegate UpdateUIEvent;
        public Sina(NetDimension.OpenAuth.Sina.SinaWeiboClient client)
        {
            openAuth = client;
        }
        private NetDimension.OpenAuth.Sina.SinaWeiboClient openAuth;
        public void PostMsg(FileInfo imageFile,string msg)
        {

            
            //statusPanel.Enabled = false;

            //发微博


            //带图的情况
            if (imageFile != null)
            {
                // 调用发图片微博api
                // 参考：http://open.weibo.com/wiki/2/statuses/upload
                openAuth.HttpPostAsync("statuses/upload.json", new Dictionary<string, object> 
				//当然，这里用匿名类也是可以的
				/*
					匿名类传参方式：
				 * new { status = txtStatus.Text, pic = imageFile }
				 */
				{
					
					{"status" ,msg},
					{"pic" , imageFile} //imgFile: 对于文件上传，这里可以直接传FileInfo对象
				}).ContinueWith(task =>
                {
                    //这里用了个异步方法，发微博不阻塞主线程，任务完成后调用处理方法
                    StatusPosted(task);
                });



            }
            else
            {
                // 调用发微博api
                // 参考：http://open.weibo.com/wiki/2/statuses/update
                openAuth.HttpPostAsync("statuses/update.json", new
                {
                    status = msg
                }).ContinueWith(task =>
                {
                    StatusPosted(task);
                });
            }
        }

        //处理微博发送后的一些事情
        private void StatusPosted(Task<HttpResponseMessage> task)
        {
            var result = task.Result;
            Debug.WriteLine(result);

            if (result.IsSuccessStatusCode)
            {
                if (UpdateUIEvent != null) UpdateUIEvent("发布成功");
            }
            else
            {
                if (UpdateUIEvent != null) UpdateUIEvent("发布失败："+result.StatusCode);
            }
        }


    }
}

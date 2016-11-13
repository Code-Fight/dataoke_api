using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace FTools.ImgTool
{
    public class ImageHelper
    {
        /// <summary>
        /// 图片压缩
        /// </summary>
        /// <param name="strPath">图片路径</param>
        /// <param name="intWidth">图片宽度</param>
        /// <param name="lngQuality">图片质量</param>
        public static void CompressJpeg(string strPath, int intWidth, long lngQuality)
        {
            
            try
            {
                var img = Image.FromFile(strPath);        // 如果不是图片会出错。

                if (img.Width <= intWidth)
                {
                    img.Dispose();
                    return;
                }
                var intHeight = img.Height * intWidth / img.Width;
                // 创建位图及相关联的图形处理工具，在位图上画缩略图
                var thm = new Bitmap(intWidth, intHeight);
                var grp = Graphics.FromImage(thm);
                grp.DrawImage(img, 0, 0, intWidth, intHeight);
                // 释放占用的图片文件
                img.Dispose();
                grp.Dispose();
                // 设置图片质量
                var ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(Encoder.Quality, lngQuality);
                // 保存缩略图
                // thm.Save(strPath);        // 如果不设置图片质量，可直接保存
                thm.Save(strPath, ImageCodecInfo.GetImageEncoders().FirstOrDefault(i => i.MimeType == "image/jpeg"), ep);
                thm.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Tk2To1
    {
        /// <summary>
        /// 淘宝客链接二合一
        /// </summary>
        /// <param name="template"></param>
        /// <param name="activityId"></param>
        /// <param name="itemId"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static string GetUrl(string template, string activityId, string itemId, string pid)
        {
            if (string.IsNullOrWhiteSpace(template) || string.IsNullOrWhiteSpace(activityId) || string.IsNullOrWhiteSpace(itemId) || string.IsNullOrWhiteSpace(pid))
            {
                return string.Empty;
            }
            string url = template.Replace("{$activityId$}", activityId)
                .Replace("{$itemId$}", itemId)
                .Replace("{$pid$}", pid);
            //TODO:应该加入验证链接是否有效
            return url;
        }
    }
}

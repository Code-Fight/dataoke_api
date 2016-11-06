using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Wireless_share_tpwd_create_response
    {
        /// <summary>
        /// 
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string request_id { get; set; }
    }

    public class TpwdEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Wireless_share_tpwd_create_response wireless_share_tpwd_create_response { get; set; }
    }
}
